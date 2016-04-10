using System.Web.Optimization;

namespace SmartAnalytics.Report
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //script
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        //"~/Scripts/jquery.signalR-{version}.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/jquery.tongjiapi.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/locales/bootstrap-datepicker.zh-CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/Prettify/prettify.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/base.js"));

            //css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Prettify/Prettify.css",
                      "~/Content/Prettify/Themes/google-code-light.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/base.css"));
        }
    }
}