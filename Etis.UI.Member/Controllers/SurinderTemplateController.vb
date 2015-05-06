Imports System.Web.Mvc

Namespace Controllers
    <AuthorizationFilter>
    Public Class SurinderTemplateController
        Inherits Controller

        ' GET: SurinderTemplate
        Function Index() As ActionResult
            Me.ViewData.Item("Title") = "Home Page"

            Return View()
        End Function
    End Class
End Namespace