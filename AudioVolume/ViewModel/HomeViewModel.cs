﻿using AudioVolume.Models;
using PagedList;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AudioVolume.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<CategoryItem> CategoryItems { get; set; }
        public IEnumerable<ProductCategory> ChildCat { get; set; }
        public class CategoryItem
        {
            public ProductCategory ProductCategory { get; set; }
            public IEnumerable<Product> Products { get; set; }
        }
    }

    public class HeaderViewModel
    {
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public Banner Banner { get; set; }
    }

    public class SearchViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public string Keysearch { get; set; }
    }
    public class SearchQuickViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string Keysearch { get; set; }
    }

    public class FooterViewModel
    {
        public Contact Contact { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
        public IEnumerable<Banner> ImageFooter { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<ShowRoom> ShowRooms { get; set; }
    }

    public class AllArticleViewModel
    {
        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
    }

    public class ArticleCategoryViewModel
    {
        public ArticleCategory Category { get; set; }
        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
    }

    public class ArticleDetailViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> PostNews { get; set; }
    }

    public class ArticleSearchViewModel
    {
        public string Keywords { get; set; }
        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
    }

    public class MenuArticleViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }
    }

    public class ProductDetailViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public Banner Banner { get; set; }
        [Display(Name = "Tiet kiem"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Tru { get; set; }
    }

    public class CategoryProductViewModel
    {
        public ProductCategory Category { get; set; }
        public IPagedList<Product> Products { get; set; }
        public string Url { get; set; }
        public int BeginCount { get; set; }
        public int EndCount { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> HotProduct { get; set; }
    }

    public class ProductSearchViewModel
    {
        public string Keywords { get; set; }
        public IPagedList<Product> Products { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }

    public class GetProductViewModel
    {
        public string Keywords { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class IntroduceViewModel
    {
        public Banner Banner { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
    }
}
