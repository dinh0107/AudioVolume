﻿using AudioVolume.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AudioVolume.ViewModel
{
    public class InsertArticleCatViewModel
    {
        public SelectList RootCats { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
    }

    public class ListArticleViewModel
    {
        public PagedList.IPagedList<Article> Articles { get; set; }
        public SelectList SelectCategories { get; set; }
        public SelectList ChildCategoryList { get; set; }
        public int? CatId { get; set; }
        public int? ChildId { get; set; }
        public string Name { get; set; }

        public ListArticleViewModel()
        {
            ChildCategoryList = new SelectList(new List<ArticleCategory>(), "Id", "CategoryName");
        }
    }

    public class InsertArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<ArticleCategory> Categories { get; set; }
        public SelectList SelectCategories { get; set; }
        public int CategoryId { get; set; }
    }
}
