using System.Web;
using System.Web.Optimization;

namespace ThesesSystem.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundels(bundles);
            RegisterScriptBundels(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterScriptBundels(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/maps")
                .Include("~/Scripts/Custom/google-maps.js",
                      "~/Scripts/Custom/google-maps-handler.js"));

            bundles.Add(new ScriptBundle("~/bundles/parallax")
                .Include("~/Scripts/Custom/simple-parallax.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr")
                .Include("~/Scripts/jquery.signalR-2.2.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui")
                .Include("~/Scripts/jquery-ui-1.11.4.min.js"));
            
        }

        private static void RegisterStyleBundels(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.cosmo.css",
                        "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/custom")
                .Include("~/Content/site.css"));
        }
    }
}
