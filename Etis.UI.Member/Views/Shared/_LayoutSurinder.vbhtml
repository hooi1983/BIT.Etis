<!DOCTYPE html>
<html>
<head>
    <title>Lanceng Admin</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="description" content="">
    <meta name="keywords" content="admin, bootstrap,admin template, bootstrap admin, simple, awesome">
    <meta name="author" content="">

    <!-- Bootstrap -->
    @Styles.Render("~/Content/CssTheme")
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <link rel="shortcut icon" href="assets/img/favicon.ico">
</head>
<body class="tooltips full-content login" id="login">
    <!-- Begin page -->
    <!--header begin -->
    <header id="header">
        <!--<span id="logo"></span>-->

        <div id="logo-group">
            <span id="logo"><a href="#"><img src="~/Content/Theme/assets/img/logo-login.jpg" class="logo-login" alt="Logo"></a></span>
            @*<span id="logo"><a href="#"><img src="~/Content/Theme/assets/img/logo-login.png" class="logo-login img-circle" alt="Logo"></a></span>*@
            <ul>
                <li><span class="text-center logo-corp">Ministry of Finance</span></li>
                <li><span id="logo-head">DIRECTORATE GENERAL OF TAXES</span></li>
                <li><span class="text-center logo_slogan">Tax Uniting Hearts, Developing the Country</span></li>
            </ul>
            <!-- END AJAX-DROPDOWN -->
        </div>
        <div class="header-right-wrap">
            <ul>
                <li><span id="login-header-space"> <span class="hidden-mobile">Need a login?</span> </span></li>
                <li><a href="register.html" id="register-header" class="btn btn-info">New User Registration</a> </li>
            </ul>
        </div>
        <div class="row language-head"><ul><li><a href="#"><span id="flag-one"><img src="~/Content/Theme/assets/img/Flag-icon-india.jpg" alt="flag" /></span>Bahasa Indonesia</a></li><li><a href="#"><span id="flag-two"><img src="~/Content/Theme/assets/img/spr_flag_malaysia.png" alt="flag" /></span>English</a></li></ul></div>
    </header>
    <!--header-end---->
    <div id="main" role="main">
        <!-- MAIN CONTENT -->
        @RenderBody()
    </div>
    <!-- End of page -->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    @Scripts.Render("~/bundles/JsThemeFirst")
    <script src="http://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    @Scripts.Render("~/bundles/JsThemeSecond")
</body>
</html>