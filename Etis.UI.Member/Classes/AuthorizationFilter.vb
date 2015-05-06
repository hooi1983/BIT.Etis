Public Class AuthorizationFilter
    Inherits AuthorizeAttribute

    Protected Overrides Sub HandleUnauthorizedRequest(filterContext As AuthorizationContext)
        Dim loginUrl = "~/account/login"

        ' Check User
        If Not String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name) Then
            If filterContext.HttpContext.Request.IsAuthenticated Then
                Return
            End If
        End If

        filterContext.Result = New RedirectResult(loginUrl & "?returnurl=" & Convert.ToString(filterContext.HttpContext.Request.Url.PathAndQuery))
    End Sub
End Class