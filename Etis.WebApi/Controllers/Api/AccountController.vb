Imports System.Net
Imports System.Web.Http

Namespace Api
    Public Class AccountController
        Inherits ApiController

        <HttpPost>
        Public Function Login(LoginName As String, LoginPassword As String) As Etis.Core.Entities.Members.Member
            Return New Etis.Core.Entities.Members.Member
        End Function
    End Class
End Namespace