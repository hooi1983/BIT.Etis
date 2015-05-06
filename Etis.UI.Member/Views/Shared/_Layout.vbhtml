<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <link href="~/favicon.png" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - ETIS</title>
    @Styles.Render("~/bundles/css")
    @Scripts.Render("~/bundles/modernizr")
    @RenderSection("style", required:=False)
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("ETIS", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("Sales Invoice", "Index", "SalesInvoice")</li>
                    <li>@Html.ActionLink("Puchase Invoice", "Index", "PurchaseInvoice")</li>
                    <li>@Html.ActionLink("Buyer", "Index", "Buyer")</li>
                    <li>@Html.ActionLink("Seller", "Index", "Seller")</li>
                    <li>@Html.ActionLink("Logout", "Logout", "Account")</li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - ETIS</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>