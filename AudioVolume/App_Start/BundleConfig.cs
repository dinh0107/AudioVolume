﻿using System.Web;
using System.Web.Optimization;

namespace AudioVolume
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            //swiper-bundle.min.css
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery.fancybox.js",
                      "~/Scripts/slick.js",
                      "~/Scripts/jquery.toast.js",
                       "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/aos.js",
                      "~/Scripts/lazysizes.min.js",
                      "~/Scripts/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/PagedList.css",
                      "~/Content/all.css",
                      "~/Content/slick.css",
                      "~/Content/jquery.toast.css",
                      "~/Content/jquery.fancybox.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/all.css",
                      "~/Content/jquery.toast.css",
                      "~/Content/PagedList.css",
                      "~/Content/jquery.fancybox.css",
                      "~/Content/tagmanager.css",
                      "~/Content/select2.css",
                      "~/Content/daterangepicker.css",
                      "~/Content/adminSite.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                      "~/Content/themes/base/*.css"));
        }
    }

}
