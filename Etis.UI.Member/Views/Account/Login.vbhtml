@ModelType Etis.Core.Entities.Members.MemberLogin

@Code
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>ETIS Login</title>
    <link href="~/favicon.png" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
    @Styles.Render("~/bundles/css")

    <style>
        .panel-heading {
            padding: 5px 15px;
        }

        .panel-footer {
	        padding: 1px 15px;
	        color: #A0A0A0;
        }

        .profile-img {
	        width: 96px;
	        height: 96px;
	        margin: 0 auto 10px;
	        display: block;
	        -moz-border-radius: 50%;
	        -webkit-border-radius: 50%;
	        border-radius: 50%;
        }
    </style>
</head>
<body>
    <div class="row" style="margin-top: 100px;">
        <div class="col-sm-6 col-md-4 col-md-offset-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <strong> Sign in to continue</strong>
                </div>
                <div class="panel-body">
                    @Using Html.BeginForm("Login", "Account", Nothing, FormMethod.Post, New With {.id = "frmCreate", .class = "form-horizontal", .role = "form"})
                        @Html.AntiForgeryToken
                        @<input type="hidden" name="ReturnUrl" value="@Me.Request.QueryString("returnurl")" />
                        
                        @<fieldset>
                            <div class="row">
                                <div class="center-block">
                                    <img class="profile-img"
                                         src="~/Content/logo/logo.png" alt="ETIS">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-10  col-md-offset-1 ">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-user"></i>
                                            </span>
                                            @Html.TextBoxFor(Function(m) m.LoginName, New With {.class = "form-control", .placeholder = "Login Name"})
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="glyphicon glyphicon-lock"></i>
                                            </span>
                                            @Html.PasswordFor(Function(m) m.LoginPassword, New With {.class = "form-control", .placeholder = "Password"})
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <input type="submit" class="btn btn-lg btn-primary btn-block" value="Sign in">
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    End Using
                </div>
            </div>
        </div>
    </div>
</body>
</html>