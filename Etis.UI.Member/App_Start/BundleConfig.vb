Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.min.js",
                  "~/Scripts/bootstrap-datepicker.min.js",
                  "~/Scripts/respond.min.js"))

        bundles.Add(
            New StyleBundle("~/bundles/css") _
                .Include("~/Content/bootstrap.min.css", New CssRewriteUrlTransform) _
                .Include("~/Content/bootstrap-datepicker.min.css", New CssRewriteUrlTransform) _
                .Include("~/Content/css/font-awesome.min.css", New CssRewriteUrlTransform) _
                .Include("~/Content/site.css", New CssRewriteUrlTransform)
        )

        ' Surinder Template
        bundles.Add(
            New ScriptBundle("~/bundles.JsThemeFirst").Include(
                "~/Content/Theme/assets/js/jquery.js",
                "~/Content/Theme/assets/js/bootstrap.min.js",
                "~/Content/Theme/assets/third/knob/jquery.knob.js",
                "~/Content/Theme/assets/third/slimscroll/jquery.slimscroll.min.js"
            )
        )

        bundles.Add(
            New ScriptBundle("~/bundles.JsThemeSecond").Include(
                "~/Content/Theme/assets/third/morris/morris.js",
                "~/Content/Theme/assets/third/nifty-modal/js/classie.js",
                "~/Content/Theme/assets/third/nifty-modal/js/modalEffects.js",
                "~/Content/Theme/assets/third/sortable/sortable.min.js",
                "~/Content/Theme/assets/third/select/bootstrap-select.min.js",
                "~/Content/Theme/assets/third/summernote/summernote.js",
                "~/Content/Theme/assets/third/magnific-popup/jquery.magnific-popup.min.js",
                "~/Content/Theme/assets/third/pace/pace.min.js",
                "~/Content/Theme/assets/third/input/bootstrap.file-input.js",
                "~/Content/Theme/assets/third/datepicker/js/bootstrap-datepicker.js",
                "~/Content/Theme/assets/third/icheck/icheck.min.js",
                "~/Content/Theme/assets/third/wizard/jquery.snippet.min.js",
                "~/Content/Theme/assets/third/wizard/jquery.easyWizard.js",
                "~/Content/Theme/assets/third/wizard/scripts.js",
                "~/Content/Theme/assets/js/lanceng.js"
            )
        )

        bundles.Add(
            New StyleBundle("~/Content/CssTheme").Include(
                "~/Content/Theme/assets/css/bootstrap.min.css",
                "~/Content/Theme/assets/third/font-awesome/css/font-awesome.min.css",
                "~/Content/Theme/assets/css/style.css",
                "~/Content/Theme/assets/css/style-responsive.css",
                "~/Content/Theme/assets/css/animate.css",
                "~/Content/Theme/assets/third/morris/morris.css",
                "~/Content/Theme/assets/third/nifty-modal/css/component.css",
                "~/Content/Theme/assets/third/sortable/sortable-theme-bootstrap.css",
                "~/Content/Theme/assets/third/icheck/skins/minimal/grey.css",
                "~/Content/Theme/assets/third/select/bootstrap-select.min.css",
                "~/Content/Theme/assets/third/summernote/summernote.css",
                "~/Content/Theme/assets/third/magnific-popup/magnific-popup.css"
            )
        )

        BundleTable.EnableOptimizations = False
    End Sub
End Module