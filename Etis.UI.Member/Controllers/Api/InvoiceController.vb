Imports System.Net
Imports System.Web.Http

Namespace Api
    <RoutePrefix("api/v1/invoice")>
    Public Class InvoiceController
        Inherits ApiController

        <Route("~/api/v1/invoices")>
        Public Function GetInvoices(MemberID As Integer, Optional Page As Integer = 1) As IEnumerable(Of Core.Entities.Members.Document)
            Try
                Using biz As New Core.Business.MemberDocument
                    Return biz.GetSalesList(MemberID, Page)
                End Using
            Catch ex As Exception
                Throw
            End Try
        End Function
    End Class
End Namespace