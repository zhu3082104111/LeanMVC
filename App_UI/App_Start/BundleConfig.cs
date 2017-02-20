using System.Web;
using System.Web.Optimization;

namespace App_UI
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/modernizr-2.6.2.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui.unobtrusive-{version}.js",
                        "~/Scripts/jquery-ui-i18n.js",
                        "~/Scripts/jquery-TimePicker-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerylayout").Include("~/Scripts/jquery.layout-{version}.js", "~/Scripts/jquery.layout.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include("~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/uploadify").Include("~/Scripts/uploadify/jquery.uploadify-{version}.js"));
            bundles.Add(new StyleBundle("~/Content/uploadify").Include("~/Scripts/uploadify/uploadify.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/normalize.css", "~/Content/site.css"));

        }
    }
}