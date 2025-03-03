using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using AudioVolume.DAL;
using AudioVolume.Models;
using AudioVolume.ViewModel;
using System.IO;
namespace AudioVolume.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private static string Email => WebConfigurationManager.AppSettings["email"];
        private static string Password => WebConfigurationManager.AppSettings["password"];
        public ConfigSite ConfigSite => (ConfigSite)HttpContext.Application["ConfigSite"];

        private IEnumerable<ArticleCategory> ArticleCategories =>
            _unitOfWork.ArticleCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));
        private IEnumerable<ProductCategory> ProductCategories =>
            _unitOfWork.ProductCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));

        #region Home
        public ActionResult Index()
        {
            var categories = ProductCategories.Where(a => a.Home && a.ParentId == null);
            var products = _unitOfWork.ProductRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort));
            var categoryItems = categories.Select(a => new HomeViewModel.CategoryItem
            {
                ProductCategory = a,
                Products = products.Where(p => p.ProductCategoryId == a.Id || p.ProductCategory.ParentId == a.Id).Take(10)
            });

            var model = new HomeViewModel
            {
                CategoryItems = categoryItems,
                Banners = _unitOfWork.BannerRepository.GetQuery(a => a.Active && a.GroupId == 1, o => o.OrderBy(a => a.Sort)),
                Products = _unitOfWork.ProductRepository.GetQuery(a => a.Active && a.Top , o => o.OrderByDescending(a => a.CreateDate)),
                ChildCat  = ProductCategories.Where(a => a.Home),
                Articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active && (a.Home && a.ArticleCategory.TypePost == TypePost.Article) , o => o.OrderByDescending(a => a.CreateDate))
            };
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult Header()
        {
            var model = new HeaderViewModel
            {
                ProductCategories = ProductCategories.Where(a => a.ShowMenu),
                ArticleCategories = ArticleCategories.Where(a => a.ShowMenu && (a.ParentId == null && a.TypePost ==TypePost.Article)),
            };
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var model = new FooterViewModel
            {
                Articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active , o => o.OrderByDescending(a => a.CreateDate)),
                ProductCategories = ProductCategories.Where(a => a.ShowFooter),
                ArticleCategories = ArticleCategories.Where(a => a.ShowFooter),
                ShowRooms = _unitOfWork.ShowRoomRepository.Get(),
                Banners = _unitOfWork.BannerRepository.GetQuery(a => a.Active  , o => o.OrderBy(a => a.Sort)),
                ImageFooter = _unitOfWork.BannerRepository.GetQuery(a => a.Active && a.GroupId == 3 , o => o.OrderByDescending(a => a.Sort)).Take(1)
            };
            return PartialView(model);
        }
        #endregion


        public PartialViewResult ContactForm()
        {
            return PartialView();
        }
        [Route("lien-he")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ContactForm(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
            }

            _unitOfWork.ContactRepository.Insert(model);
            _unitOfWork.Save();

            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Họ và tên: {model.Fullname},</p>" +
                       $"<p>Điện thoại: {model.Mobile},</p>" +
                       $"<p>Email: {model.Email},</p>" +
                       $"<p>Sản phẩm: {model.ProductName},</p>" +
                       $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";

            Task.Run(() => HtmlHelpers.SendEmail("gmail", subject, body, ConfigSite.Email, Email, Email, Password, ConfigSite.Title));
            return Json(new { status = true, msg = "Gửi liên hệ thành công.\nChúng tôi sẽ liên lạc với bạn sớm nhất có thể." });
        }


        #region Product
        [Route("dich-vu")]
        public ActionResult AllProduct(int? page)
        {
            var pageNumber = page ?? 1;
            var products = _unitOfWork.ProductRepository.GetQuery(a => a.Active, o => o.OrderByDescending(a => a.CreateDate));
            return View();
        }
        [Route("{url:regex(^(?!.*(mms|uploader|article|banner|contact|product)).*$)}", Order = 2)]
        public ActionResult ProductCategory(int? page, string url)
        {
            var category = ProductCategories.Where(a => a.Url == url).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var pageNumber = page ?? 1;
            var pageSize = 20;

            var products = _unitOfWork.ProductRepository.GetQuery(a => a.Active &&
                (a.ProductCategoryId == category.Id || a.ProductCategory.ParentId == category.Id), o => o.OrderByDescending(a => a.CreateDate));

            var model = new CategoryProductViewModel
            {
                Category = category,
                Products = products.ToPagedList(pageNumber, pageSize),
                BeginCount = (pageNumber - 1) * pageSize + 1,
                EndCount = pageNumber * pageSize,
                Brands = _unitOfWork.BrandRepository.GetQuery(a => a.Active , o => o.OrderBy(a => a.Sort)),
                HotProduct = products.Where(a =>  a.Top)
            };

            return View(model);
        }
        [Route("{url}.html", Order = 1)]
        public ActionResult ProductDetail(string url)
        {
            var product = _unitOfWork.ProductRepository.GetQuery(a => a.Active && a.Url == url).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var products = _unitOfWork.ProductRepository.GetQuery(a => a.Active && a.Id != product.Id && a.ProductCategoryId == product.ProductCategoryId, o => o.OrderByDescending(a => a.CreateDate), 8);
            

            var model = new ProductDetailViewModel
            {
                Product = product,
                Products = products,
                Brands = _unitOfWork.BrandRepository.GetQuery(a => a.Active , o => o.OrderBy(a => a.Sort)), 
                Banner = _unitOfWork.BannerRepository.GetQuery(a => a.Active && a.Image != null && a.GroupId == 7).FirstOrDefault(),
            };

            if (product.PriceSale > 0)
            {
                model.Tru = (decimal)product.Price - (decimal)product.PriceSale;
            }
            return View(model);
        }
        public PartialViewResult Rating()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult RatingProduct(ProductRating model , FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var files = Request.Files;

                if (files.Count > 0)
                {
                    var imagePaths = new List<string>();
                    var path = Server.MapPath("~/images/comment/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    foreach (string file in files)
                    {
                        var postedFile = Request.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(postedFile.FileName);
                            var filePath = Path.Combine(path, fileName);
                            postedFile.SaveAs(filePath);
                            imagePaths.Add(fileName);
                        }
                    }

                    if (imagePaths.Count > 0)
                    {
                        model.Image = string.Join(",", imagePaths);
                    }
                    else
                    {
                        model.Image = null;
                    }
                }
                else
                {
                    model.Image = null;
                }
                _unitOfWork.ProductRatingRepository.Insert(model);
                _unitOfWork.Save();
                return Json(new { status = true, msg = "Đánh giá thành công" });
            }
            return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
        }


        #endregion

        #region Article
        [Route("blogs")]
        public ActionResult AllArticle(int? page)
        {
            var pageNumber = page ?? 1;
            var articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active && a.ArticleCategory.TypePost == TypePost.Article, o => o.OrderByDescending(a => a.CreateDate));

            var model = new AllArticleViewModel()
            {
                Articles = articles.ToPagedList(pageNumber, 18),
                Categories = ArticleCategories.Where(a => a.TypePost == TypePost.Article),
            };
            return View(model);
        }
        [Route("blogs/{url:regex(^(?!.*(mms|uploader|article|banner|contact|product)).*$)}", Order = 2)]
        public ActionResult ArticleCategory(int? page, string url)
        {
            var category = _unitOfWork.ArticleCategoryRepository.GetQuery(a => a.CategoryActive && a.Url == url).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active &&
                (a.ArticleCategoryId == category.Id || a.ArticleCategory.ParentId == category.Id), q => q.OrderByDescending(a => a.CreateDate));
            var pageNumber = page ?? 1;

            if (articles.Count() == 1)
            {
                var fi = articles.First();
                return RedirectToAction("ArticleDetail", new { url = fi.Url });
            }
            var products = _unitOfWork.ProductRepository.GetQuery(a => a.Active && a.Top, o => o.OrderByDescending(a => a.CreateDate));
            var model = new ArticleCategoryViewModel
            {
                Category = category,
                Articles = articles.ToPagedList(pageNumber, 18),
                Categories = ArticleCategories.Where(a => a.TypePost == TypePost.Article),
                Products = products
            };

            return View(model);
        }
        [Route("blogs/{url}.html", Order = 1)]
        public ActionResult ArticleDetail(string url)
        {
            var article = _unitOfWork.ArticleRepository.GetQuery(a => a.Url == url && a.Active).FirstOrDefault();
            if (article == null)
            {
                return RedirectToAction("Index");
            }

            var articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active && a.Id != article.Id, o => o.OrderByDescending(a => a.CreateDate));

            var model = new ArticleDetailViewModel
            {
                Article = article,
                Articles = articles.Where(a => a.ArticleCategoryId == article.ArticleCategoryId).Take(8),
                PostNews = articles.Where(a => a.ArticleCategory.TypePost == TypePost.Article).Take(8)
            };
            return View(model);
        }
        #endregion

        [HttpPost]
        public PartialViewResult SearchProduct(string keysearch)
        {
            if (string.IsNullOrEmpty(keysearch))
            {
                ViewBag.Message = "Vui lòng nhập từ khóa tìm kiếm.";
            }
            var products = _unitOfWork.ProductRepository.GetQuery(
                   a => a.Active && a.Name.Contains(keysearch),
                   o => o.OrderByDescending(a => a.CreateDate)
               );

            if (!products.Any())
            {
                ViewBag.Message = "Không tìm thấy sản phẩm nào phù hợp.";
            }
            var model = new SearchQuickViewModel
            {
                Keysearch = keysearch,
                Products = products
            };
            return PartialView("_ProductSearchResults", model);
        }
        [Route("tim-kiem")]
        public ActionResult Search(string keysearch , int? page)
        {
            var pageNumber = page ?? 1;
            var products = _unitOfWork.ProductRepository.GetQuery(
                     a => a.Active && a.Name.Contains(keysearch),
                     o => o.OrderByDescending(a => a.CreateDate)
                 );
            var model = new SearchViewModel
            {
                Keysearch = keysearch,
                Products = products.ToPagedList(pageNumber, 20),
            };
            return View(model);
        }
    }
}