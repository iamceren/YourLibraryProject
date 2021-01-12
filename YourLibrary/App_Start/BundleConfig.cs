using System.Web;
using System.Web.Optimization;

namespace YourLibrary
{
    public class BundleConfig
    {
        // Paketleme hakkında daha fazla bilgi için lütfen https://go.microsoft.com/fwlink/?LinkId=301862 adresini ziyaret edin
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Geliştirme yapmak ve öğrenmek için Modernizr'ın geliştirme sürümünü kullanın. Daha sonra,
            // üretim için hazır. https://modernizr.com adresinde derleme aracını kullanarak yalnızca ihtiyacınız olan testleri seçin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region Template Design

            bundles.Add(new ScriptBundle("~/template/js").Include(
                          "~/Scripts/jquery.min.js",
                          "~/Scripts/bootstrap.bundle.min.js",
                          "~/Scripts/bootstrap.js",
                          "~/Scripts/bootstrap.min.js",
                          "~/Scripts/custom.js",
                          "~/Scripts/jquery-3.0.0.min.js",
                          "~/Scripts/jquery-3.4.1.intellisense.js",
                          "~/Scripts/jquery-3.4.1.js",
                          "~/Scripts/jquery-3.4.1.min.js",
                          "~/Scripts/jquery-3.4.1.slim.js",
                          "~/Scripts/jquery-3.4.1.slim.min.js",
                          "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                          "~/Scripts/jquery.validate-vsdoc.js",
                          "~/Scripts/jquery.validate.js",
                          "~/Scripts/jquery.validate.min.js",
                          "~/Scripts/jquery.validate.unobtrusive.js",
                          "~/Scripts/jquery.validate.unobtrusive.min.js",
                          "~/Scripts/modernizr-2.8.3.js",
                          "~/Scripts/plugin.js",
                          "~/Scripts/popper.min.js"
                          ));
            bundles.Add(new StyleBundle("~/template/css").Include(
                          "~/Content/css/bootstrap.min.css",
                          "~/Content/css/style.css",
                          "~/Content/css/responsive.css",
                          "~/Content/css/jquery.mCustomScrollbar.min.css",
                          "~/Content/css/meanmenu.css",
                          "~/Content/css/nice-select.css",
                          "~/Content/css/normalize.css",
                          "~/Content/css/o0wl.carousel.min.css",
                          "~/Content/css/slick.css",
                          "~/Content/css/jquery.fancybox.min.css",
                          "~/Content/css/jquery-ui.css",
                          "~/Content/css/font-awesome.min.css",
                          "~/Content/css/animate.min.css"
                          ));
            bundles.Add(new StyleBundle("~/login/css").Include(
                          "~/Content/Login/Login_v15/css/main.css",
                          "~/Content/Login/Login_v15/css/util.css"
                ));
            bundles.Add(new StyleBundle("~/login/js").Include(
              "~/Content/Login/Login_v15/js/main.js",
              "~/Content/Login/Login_v15/js/map-custom.js"
                ));
            #endregion

        }
    }
}
