Imports System.Web.Mvc

Namespace Controllers
    <AuthorizationFilter>
    Public Class AccountController
        Inherits Controller

        Private ReadOnly Controller As String = "Account"
        Private ReadOnly Title As String = "Account"

        ' GET: Account
        Function Index() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = Me.Title

            Return View()
        End Function

        ' GET: Account/Login
        <AllowAnonymous>
        Function Login() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = "Login"

            Return View()
        End Function

        ' POST: Account/Login
        <AllowAnonymous>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Login(p As Members.MemberLogin, Optional ReturnUrl As String = Nothing) As ActionResult
            Dim Model = New Members.Member

            Try
                If ModelState.IsValid Then
                    Using biz As New Core.Business.Member
                        If biz.IsValidLogin(p) Then
                            With p
                                .UserAgent = Me.Request.UserAgent
                                .IpAddress = Me.Request.UserHostAddress
                            End With

                            Model = biz.Login(p)
                        End If
                    End Using

                    If Model IsNot Nothing AndAlso Not Model.MemberID = 0 Then
                        Dim Cookie = String.Format("{0}|{1}|{2}", Model.MemberID, Model.LoginName, Model.FullName)

                        FormsAuthentication.SetAuthCookie(Cookie, False)
                    Else
                        Return View()
                    End If

                    If Not String.IsNullOrWhiteSpace(returnurl) AndAlso Url.IsLocalUrl(returnurl) AndAlso returnurl.StartsWith("/") AndAlso
                        Not returnurl.StartsWith("//") AndAlso Not returnurl.StartsWith("/\\") Then

                        'Return Redirect(returnurl)
                        Return RedirectToAction("Index", "SurinderTemplate")
                    End If

                    'Return RedirectToAction("Index", "Home")
                    Return RedirectToAction("Index", "SurinderTemplate")
                End If

                Return View()
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' GET: Account/Logout
        Function Logout() As ActionResult
            Try
                FormsAuthentication.SignOut()
                Return RedirectToAction("Index", "Home")
            Catch ex As Exception
                Throw
            End Try
        End Function
    End Class
End Namespace