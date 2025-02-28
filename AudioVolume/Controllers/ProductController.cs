using AudioVolume.DAL;
using AudioVolume.Models;
using AudioVolume.ViewModel;
using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Drawing;
using AudioVolume.Migrations;

namespace AudioVolume.Controllers
{
    public class ProductController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        private IEnumerable<ProductCategory> ProductCategories =>
            _unitOfWork.ProductCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));

        private SelectList ParentProductCategoryList => new SelectList(ProductCategories.Where(a => a.ParentId == null), "Id", "CategoryName");

        #region ProductCategory
        [ChildActionOnly]
        public ActionResult ListCategory()
        {
            var allcats = _unitOfWork.ProductCategoryRepository.Get(orderBy: q => q.OrderBy(a => a.CategorySort));
            return PartialView(allcats);
        }
        public ActionResult ProductCategory(string result = "")
        {
            ViewBag.ProductCat = result;
            ViewBag.RootCats = new SelectList(_unitOfWork.ProductCategoryRepository.Get(a => a.ParentId == null, q => q.OrderBy(a => a.CategorySort)), "Id", "CategoryName");

            var model = new InsertProductCatViewModel
            {
                ProductCategory = new ProductCategory { CategorySort = 1 },
            };

            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductCategory(InsertProductCatViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                    if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                    if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                    var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                        "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                    var imgPath = "/images/productCategory/" + DateTime.Now.ToString("yyyy/MM/dd");
                    HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                    var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                    var newImage = System.Drawing.Image.FromStream(Request.Files[i].InputStream);
                    var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                    HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                    if (Request.Files.Keys[i] == "ProductCategory.Image")
                    {
                        model.ProductCategory.Image = imgFile;
                    }
                    if (Request.Files.Keys[i] == "ProductCategory.ImageSale")
                    {
                        model.ProductCategory.ImageSale = imgFile;
                    }
                }

                model.ProductCategory.Url = HtmlHelpers.ConvertToUnSign(null, model.ProductCategory.Url ?? model.ProductCategory.CategoryName);
                model.ProductCategory.ParentId = model.ParentId;

                _unitOfWork.ProductCategoryRepository.Insert(model.ProductCategory);
                _unitOfWork.Save();
                return RedirectToAction("ProductCategory", new { result = "success" });
            }
            ViewBag.RootCats = new SelectList(_unitOfWork.ProductCategoryRepository.Get(a => a.ParentId == null, q => q.OrderBy(a => a.CategorySort)), "Id", "CategoryName");
            return View(model);
        }
        public ActionResult UpdateCategory(int catId = 0)
        {
            var category = _unitOfWork.ProductCategoryRepository.GetById(catId);
            if (category == null)
            {
                return RedirectToAction("ProductCategory");
            }

            ViewBag.RootCats = new SelectList(_unitOfWork.ProductCategoryRepository.Get(a => a.ParentId == null, q => q.OrderBy(a => a.CategorySort)), "Id", "CategoryName");
            var model = new InsertProductCatViewModel
            {
                ProductCategory = category,
            };

            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateCategory(InsertProductCatViewModel model, FormCollection fc)
        {
            var category = _unitOfWork.ProductCategoryRepository.GetById(model.ProductCategory.Id);
            if (ModelState.IsValid)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                    if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                    if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                    var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                        "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                    var imgPath = "/images/productCategory/" + DateTime.Now.ToString("yyyy/MM/dd");
                    HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                    var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                    var newImage = System.Drawing.Image.FromStream(Request.Files[i].InputStream);
                    var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                    HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                    if (Request.Files.Keys[i] == "ProductCategory.Image")
                    {
                        category.Image = imgFile;
                    }
                    if (Request.Files.Keys[i] == "ProductCategory.ImageSale")
                    {
                        category.ImageSale = imgFile;
                    }
                }

                var file = Request.Files["ProductCategory.CoverImage"];
                var file2 = Request.Files["ProductCategory.Image"];

                if (file2 != null && file2.ContentLength == 0)
                {
                    category.Image = fc["CurrentFile2"] == "" ? null : fc["CurrentFile2"];
                }

                category.Url = HtmlHelpers.ConvertToUnSign(null, model.ProductCategory.Url ?? model.ProductCategory.CategoryName);
                category.CategoryName = model.ProductCategory.CategoryName;
                category.CategorySort = model.ProductCategory.CategorySort;
                category.Body = model.ProductCategory.Body;
                category.CategoryActive = model.ProductCategory.CategoryActive;
                category.ParentId = model.ParentId;
                category.ShowMenu = model.ProductCategory.ShowMenu;
                category.ShowFooter = model.ProductCategory.ShowFooter;
                category.TitleMeta = model.ProductCategory.TitleMeta;
                category.DescriptionMeta = model.ProductCategory.DescriptionMeta;
                category.HomeName = model.ProductCategory.HomeName;
                category.Home = model.ProductCategory.Home;
                _unitOfWork.Save();
                return RedirectToAction("ProductCategory", new { result = "update" });
            }
            ViewBag.RootCats = new SelectList(_unitOfWork.ProductCategoryRepository.Get(a => a.ParentId == null, q => q.OrderBy(a => a.CategorySort)), "Id", "CategoryName");
            return View(category);
        }
        [HttpPost]
        public bool DeleteCategory(int catId = 0)
        {
            var category = _unitOfWork.ProductCategoryRepository.GetById(catId);
            if (category == null)
            {
                return false;
            }
            _unitOfWork.ProductCategoryRepository.Delete(category);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateProductCat(int sort = 1, bool active = false, bool footer = false, bool menu = false, int productCatId = 0)
        {
            var productCat = _unitOfWork.ProductCategoryRepository.GetById(productCatId);
            if (productCat == null)
            {
                return false;
            }
            productCat.CategorySort = sort;
            productCat.CategoryActive = active;
            productCat.ShowMenu = menu;
            productCat.ShowFooter = footer;

            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Product
        public ActionResult ListProduct(int? page, string name, int? childId, int rootId = 0, string sort = "date-desc", string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var products = _unitOfWork.ProductRepository.GetQuery().AsNoTracking();

            if (rootId > 0)
            {
                products = products.Where(a => a.ProductCategoryId == rootId);
            }
            else if (childId > 0)
            {
                products = products.Where(a => a.ProductCategoryId == childId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(l => l.Name.Contains(name));
            }

            switch (sort)
            {
                case "sort-asc":
                    products = products.OrderBy(a => a.Sort);
                    break;
                case "sort-desc":
                    products = products.OrderByDescending(a => a.Sort);
                    break;
                case "hot":
                    products = products.OrderByDescending(a => a.Sort);
                    break;
                case "date-asc":
                    products = products.OrderBy(a => a.CreateDate);
                    break;
                default:
                    products = products.OrderByDescending(a => a.CreateDate);
                    break;
            }
            var model = new ListProductViewModel
            {
                SelectCategories = new SelectList(ProductCategories.Where(a => a.ParentId == null), "Id", "CategoryName"),
                Products = products.ToPagedList(pageNumber, pageSize),
                ChildId = childId,
                RootId = rootId,
                Name = name,
                Sort = sort
            };
            if (childId.HasValue)
            {
                model.ChildCategoryList = new SelectList(ProductCategories.Where(a => a.ParentId == childId), "Id", "Categoryname");
            }
            return View(model);
        }
        public ActionResult Product()
        {
            var model = new InsertProductViewModel
            {
                Product = new Product { Sort = 1, Active = true },
                Categories = ProductCategories,
                Brands = _unitOfWork.BrandRepository.GetQuery(a => a.Active, o => o.OrderBy(a => a.Sort))
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Product(InsertProductViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (model.Price != null)
                {
                    model.Product.Price = Convert.ToDecimal(model.Price.Replace(",", ""));
                }
                if (model.PriceSale != null)
                {
                    model.Product.PriceSale = Convert.ToDecimal(model.PriceSale.Replace(",", ""));
                }


                for (var i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                    if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                    if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                    var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                        "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                    var imgPath = "/images/products/" + DateTime.Now.ToString("yyyy/MM/dd");
                    HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                    var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                    var newImage = Image.FromStream(Request.Files[i].InputStream);
                    var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                    HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                    if (Request.Files.Keys[i] == "Product.Image")
                    {
                        model.Product.Image = imgFile;
                    }
                    else if (Request.Files.Keys[i] == "Product.CoverImage")
                    {
                        model.Product.CoverImage = imgFile;
                    }
                }

                model.Product.Feedback = fc["Pictures"];
                model.Product.ProductCategoryId = model.CategoryId;
                model.Product.BrandId = model.BrandId;
                model.Product.Url = HtmlHelpers.ConvertToUnSign(null, model.Product.Url ?? model.Product.Name);

                var count = _unitOfWork.ProductRepository.GetQuery(a => a.Url == model.Product.Url).Count();
                if (count > 1)
                {
                    model.Product.Url += "-" + DateTime.Now.Millisecond;
                    _unitOfWork.Save();
                }

                _unitOfWork.ProductRepository.Insert(model.Product);
                _unitOfWork.Save();

                if (model.Options != null)
                {
                    foreach (var option in model.Options)
                    {
                        option.ProductId = model.Product.Id;
                        _unitOfWork.ProductOptionRepository.Insert(option);
                    }
                    _unitOfWork.Save();
                }
                if (model.ProductInfos != null)
                {
                    foreach (var info in model.ProductInfos)
                    {
                        info.ProductId = model.Product.Id;
                        _unitOfWork.ProductInfoRepository.Insert(info);
                    }
                    _unitOfWork.Save();
                }
                return RedirectToAction("ListProduct", new { result = "success" });
            }

            model.Categories = ProductCategories;
            model.Brands = _unitOfWork.BrandRepository.GetQuery(a => a.Active, o => o.OrderBy(a => a.Sort));
            return View(model);
        }
        public ActionResult UpdateProduct(int proId = 0)
        {
            var product = _unitOfWork.ProductRepository.GetById(proId);
            if (product == null)
            {
                return RedirectToAction("ListProduct");
            }

            var category = _unitOfWork.ProductCategoryRepository.GetById(product.ProductCategoryId);

            var model = new InsertProductViewModel
            {
                Product = product,
                Categories = ProductCategories,
                Brands = _unitOfWork.BrandRepository.GetQuery(a => a.Active, o => o.OrderBy(a => a.Sort)),  
                Price = product.Price?.ToString("N0"),
                PriceSale = product.PriceSale?.ToString("N0"),
                Options = _unitOfWork.ProductOptionRepository.GetQuery(a => a.ProductId == proId).ToList(),
                ProductInfos = _unitOfWork.ProductInfoRepository.GetQuery(a => a.ProductId == proId).ToList(),
            };

            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateProduct(InsertProductViewModel model, FormCollection fc)
        {
            var product = _unitOfWork.ProductRepository.GetById(model.Product.Id);
            if (product == null)
            {
                return RedirectToAction("ListProduct");
            }

            if (ModelState.IsValid)
            {
                if (model.Price != null)
                {
                    product.Price = Convert.ToDecimal(model.Price.Replace(",", ""));
                }
                else
                {
                    product.Price = null;
                }
                if (model.PriceSale != null)
                {
                    product.PriceSale = Convert.ToDecimal(model.PriceSale.Replace(",", ""));
                }
                else
                {
                    product.PriceSale = null;
                }
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                    if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                    if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                    var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                        "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                    var imgPath = "/images/products/" + DateTime.Now.ToString("yyyy/MM/dd");
                    HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                    var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                    var newImage = Image.FromStream(Request.Files[i].InputStream);
                    var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                    HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                    if (Request.Files.Keys[i] == "Product.Image")
                    {
                        product.Image = imgFile;
                    }
                    else if (Request.Files.Keys[i] == "Product.CoverImage")
                    {
                        product.CoverImage = imgFile;
                    }
                }

                var file = Request.Files["Product.CoverImage"];

                if (file != null && file.ContentLength == 0)
                {
                    product.CoverImage = fc["CurrentFile"] == "" ? null : fc["CurrentFile"];
                }

                product.Feedback = fc["Pictures"] == "" ? null : fc["Pictures"];
                product.Url = HtmlHelpers.ConvertToUnSign(null, model.Product.Url ?? model.Product.Name);
                product.ProductCategoryId = model.CategoryId;
                product.Name = model.Product.Name;
                product.Body = model.Product.Body;
                product.Active = model.Product.Active;
                product.Home = model.Product.Home;
                product.TitleMeta = model.Product.TitleMeta;
                product.DescriptionMeta = model.Product.DescriptionMeta;
                product.Sort = model.Product.Sort;
                product.GiaSoc = model.Product.GiaSoc;
                product.QuaTo = model.Product.QuaTo;
                product.BanChay = model.Product.BanChay;
                product.New = model.Product.New;
                product.TroGia = model.Product.TroGia;
                product.KichThuoc= model.Product.KichThuoc;
                product.Donvi  = model.Product.Donvi;
                product.BaoHanh = model.Product.BaoHanh;
                product.Nguongoc = model.Product.Nguongoc;
                product.BrandId = model.BrandId;
                product.Top = model.Product.Top;
                //Chi tiêt
                product.Model = model.Product.Model;
                product.Loai = model.Product.Loai;
                product.AmThanh = model.Product.AmThanh;
                product.TanSoDapUng = model.Product.TanSoDapUng;
                product.SignalToNoiseRatio = model.Product.SignalToNoiseRatio;
                product.CongSuat = model.Product.CongSuat;
                product.Bluetooth = model.Product.Bluetooth;
                product.MicroThoai = model.Product.MicroThoai;
                product.KetNoiCoDay = model.Product.KetNoiCoDay;
                product.TinhNang = model.Product.TinhNang;
                product.PinSac = model.Product.PinSac;
                product.SacNguocThietBiDiDong = model.Product.SacNguocThietBiDiDong;
                product.NguonDien = model.Product.NguonDien;
                product.KichThuocCt = model.Product.KichThuocCt;
                product.TrongLuong = model.Product.TrongLuong;
                product.MauSac = model.Product.MauSac;
                product.HuongDanSuDung = model.Product.HuongDanSuDung;
                product.NhapKhauPhanPhoi = model.Product.NhapKhauPhanPhoi;

                // **Xóa tất cả Options cũ**
                var oldOptions = _unitOfWork.ProductOptionRepository.GetQuery(o => o.ProductId == product.Id).ToList();
                foreach (var option in oldOptions)
                {
                    _unitOfWork.ProductOptionRepository.Delete(option);
                }

                // **Thêm Options mới**
                if (model.Options != null)
                {
                    foreach (var option in model.Options)
                    {
                        option.ProductId = product.Id;
                        _unitOfWork.ProductOptionRepository.Insert(option);
                    }
                }

                // **Xóa tất cả Options cũ**
                var oldInfos = _unitOfWork.ProductInfoRepository.GetQuery(o => o.ProductId == product.Id).ToList();
                foreach (var info in oldInfos)
                {
                    _unitOfWork.ProductInfoRepository.Delete(info);
                }

                // **Thêm Options mới**
                if (model.ProductInfos != null)
                {
                    foreach (var info in model.ProductInfos)
                    {
                        info.ProductId = product.Id;
                        _unitOfWork.ProductInfoRepository.Insert(info);
                    }
                }

                _unitOfWork.Save();


                var count = _unitOfWork.ProductRepository.GetQuery(a => a.Url == product.Url).Count();
                if (count > 1)
                {
                    product.Url += "-" + DateTime.Now.Millisecond;
                    _unitOfWork.Save();
                }

                return RedirectToAction("ListProduct", new { result = "update" });
            }

            model.Categories = ProductCategories;
            model.Brands = _unitOfWork.BrandRepository.GetQuery(a => a.Active, o => o.OrderBy(a => a.Sort));
            return View(model);
        }
        [HttpPost]
        public bool DeleteProduct(int proId = 0)
        {
            var product = _unitOfWork.ProductRepository.GetById(proId);
            if (product == null)
            {
                return false;
            }
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();
            return true;
        }
        [HttpPost]
        public bool QuickUpdate(int? quantity, bool? status, bool active, int sort = 0, int proId = 0)
        {
            var product = _unitOfWork.ProductRepository.GetById(proId);
            if (product == null)
            {
                return false;
            }
            if (status != null)
            {
                product.Active = Convert.ToBoolean(status);
            }
            if (sort >= 0)
            {
                product.Sort = sort;
            }
            product.Active = active;
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Brand

        [ChildActionOnly]
        public ActionResult ListBrand()
        {
            var brands = _unitOfWork.BrandRepository.Get(orderBy: q => q.OrderBy(a => a.Sort));
            return PartialView(brands);
        }
        public ActionResult Brand(string result = "")
        {
            ViewBag.ProductCat = result;

            var model = new Brand
            {
               Sort  = 1
            };

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Brand(Brand model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                    if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                    if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                    var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                        "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                    var imgPath = "/images/brand/" + DateTime.Now.ToString("yyyy/MM/dd");
                    HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                    var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                    var newImage = System.Drawing.Image.FromStream(Request.Files[i].InputStream);
                    var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                    HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                    if (Request.Files.Keys[i] == "Image")
                    {
                        model.Image = imgFile;
                    }
                }

                //model.ProductCategory.Url = HtmlHelpers.ConvertToUnSign(null, model.ProductCategory.Url ?? model.ProductCategory.CategoryName);
                //model.ProductCategory.ParentId = model.ParentId;

                _unitOfWork.BrandRepository.Insert(model);
                _unitOfWork.Save();
                return RedirectToAction("Brand", new { result = "success" });
            }
            return View(model);
        }
        public ActionResult UpdateBrand(int id = 0)
        {
            var brand = _unitOfWork.BrandRepository.GetById(id);
            if (brand == null)
            {
                return RedirectToAction("Brand");
            }

            return View(brand);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateBrand(Brand model, FormCollection fc)
        {
            var brand = _unitOfWork.BrandRepository.GetById(model.Id);
            if (ModelState.IsValid)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                    if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                    if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                    var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                        "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                    var imgPath = "/images/brand/" + DateTime.Now.ToString("yyyy/MM/dd");
                    HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                    var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                    var newImage = System.Drawing.Image.FromStream(Request.Files[i].InputStream);
                    var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                    HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                    if (Request.Files.Keys[i] == "Image")
                    {
                        brand.Image = imgFile;
                    }
                }

                var file2 = Request.Files["Image"];

                if (file2 != null && file2.ContentLength == 0)
                {
                    brand.Image = fc["CurrentFile2"] == "" ? null : fc["CurrentFile2"];
                }

                brand.Name = model.Name;
                brand.Sort = model.Sort;
                brand.Active = model.Active;
                _unitOfWork.Save();
                return RedirectToAction("Brand", new { result = "update" });
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteBrand(int id = 0)
        {
            var brand = _unitOfWork.BrandRepository.GetById(id);
            if (brand == null)
            {
                return false;
            }
            _unitOfWork.BrandRepository.Delete(brand);
            _unitOfWork.Save();
            return true;
        }
        //public bool UpdateProductCat(int sort = 1, bool active = false, bool footer = false, bool menu = false, int productCatId = 0)
        //{
        //    var productCat = _unitOfWork.ProductCategoryRepository.GetById(productCatId);
        //    if (productCat == null)
        //    {
        //        return false;
        //    }
        //    productCat.CategorySort = sort;
        //    productCat.CategoryActive = active;
        //    productCat.ShowMenu = menu;
        //    productCat.ShowFooter = footer;

        //    _unitOfWork.Save();
        //    return true;
        //}

        #endregion

        public JsonResult GetChildCategory(int? parentId)
        {
            var categories = ProductCategories.Where(a => a.ParentId == parentId);
            return Json(categories.Select(a => new { a.Id, Name = a.CategoryName }), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}