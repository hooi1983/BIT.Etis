Imports System.Web.Mvc

Namespace Controllers
    <AuthorizationFilter>
    Public Class SalesInvoiceController
        Inherits Controller

        Private ReadOnly Controller As String = "SalesInvoice"
        Private ReadOnly Title As String = "Faktur Penyerahan"

        ' GET: SalesInvoice
        Function Index() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = Me.Title

            Dim Model = New Members.DocumentVM
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New Core.Business.MemberDocument
                    Model = biz.GetSalesRecords(MemberID)
                End Using

                Return View(Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: SalesInvoice/ptlList
        <Route("SalesInvoice/ptlList/{pg:int}")>
        <HttpGet>
        Function ptlList(Optional pg As Integer = 1, Optional s As String = "") As ActionResult
            Dim Model = New Members.DocumentVM
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New Core.Business.MemberDocument
                    Model = biz.GetSalesRecords(MemberID, pg, s)
                End Using

                Return PartialView("ptlList", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' GET: SalesInvoice/Create
        <HttpGet>
        Function Create() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Add {0}", Me.Title)

            Return View(New Members.Document With {.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)})
        End Function

        ' GET: SalesInvoice/Edit/{id}
        <HttpGet>
        Function Edit(id As Integer) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Edit {0}", Me.Title)

            Dim Model = New Members.Document
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberDocument
                    Model = biz.GetSalesRecord(MemberID, id)
                End Using

                Return View(Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: SalesInvoice/ptlDelete/{id}
        <HttpGet>
        Function ptlDelete(id As Integer) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Delete {0}", Me.Title)

            Dim Model = New Members.Document
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberDocument
                    Model = biz.GetSalesRecord(MemberID, id)
                End Using

                Return PartialView("ptlDelete", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: SalesInvoice/ptlCreateItem
        <HttpGet>
        Function ptlCreateItem() As ActionResult
            Try
                Return PartialView("ptlCreateItem")
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: SalesInvoice/ptlTransactionTypeSub
        <HttpGet>
        Function ptlTransactionTypeSub(id As Integer) As ActionResult
            Try
                Return PartialView("ptlTransactionTypeSub", id)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: SalesInvoice/ptlTaxObject
        <HttpGet>
        Function ptlTaxObject(p As Core.Entities.TaxObjects.TaxObject) As ActionResult
            Try
                Return PartialView("ptlTaxObject", p)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' POST: SalesInvoice/Create
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Create(p As Members.Document) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller

            Dim Status = New Status

            Try
                With p
                    p.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.ByID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.UserAgent = Me.Request.UserAgent
                    p.IpAddress = Me.Request.UserHostAddress
                End With

                If Me.Request.Form.Item("Guid") IsNot Nothing Then
                    Dim lst = New List(Of Core.Entities.Members.DocumentItem)()

                    For Each guid In Me.Request.Form.Item("Guid").Split(CChar(","))
                        Dim ItemModel = New Core.Entities.Members.DocumentItem

                        With ItemModel
                            .TaxItemType = Me.Request.Form.Item(String.Format("TaxItemType_{0}", guid))
                            .TaxType = Me.Request.Form.Item(String.Format("TaxType_{0}", guid))
                            .TaxObjectID = CInt(Me.Request.Form.Item(String.Format("TaxObjectID_{0}", guid)))
                            .Description = Me.Request.Form.Item(String.Format("Description_{0}", guid))

                            If IsNumeric(Me.Request.Form.Item(String.Format("Quantity_{0}", guid))) Then
                                .Quantity = CInt(Me.Request.Form.Item(String.Format("Quantity_{0}", guid)))
                            Else
                                .Quantity = 0
                            End If

                            If IsNumeric(Me.Request.Form.Item(String.Format("UnitPrice_{0}", guid))) Then
                                .UnitPrice = CDec(Me.Request.Form.Item(String.Format("UnitPrice_{0}", guid)))
                            Else
                                .UnitPrice = 0
                            End If
                        End With

                        If ItemModel.Quantity > 0 AndAlso ItemModel.UnitPrice > 0 Then
                            lst.Add(ItemModel)
                        End If
                    Next

                    p.ItemList = lst
                End If

                Using biz As New MemberDocument
                    Status = biz.AddSalesRecord(p)
                End Using

                Return RedirectToAction("Index")
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' POST: SalesInvoice/Edit
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Edit(p As Members.Document) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller

            Dim Status = New Status

            Try
                With p
                    p.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.ByID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.UserAgent = Me.Request.UserAgent
                    p.IpAddress = Me.Request.UserHostAddress
                End With

                If Me.Request.Form.Item("Guid") IsNot Nothing Then
                    Dim lst = New List(Of Core.Entities.Members.DocumentItem)()

                    For Each guid In Me.Request.Form.Item("Guid").Split(CChar(","))
                        Dim ItemModel = New Core.Entities.Members.DocumentItem

                        With ItemModel
                            .TaxItemType = Me.Request.Form.Item(String.Format("TaxItemType_{0}", guid))
                            .TaxType = Me.Request.Form.Item(String.Format("TaxType_{0}", guid))
                            .TaxObjectID = CInt(Me.Request.Form.Item(String.Format("TaxObjectID_{0}", guid)))
                            .Description = Me.Request.Form.Item(String.Format("Description_{0}", guid))

                            If IsNumeric(Me.Request.Form.Item(String.Format("Quantity_{0}", guid))) Then
                                .Quantity = CInt(Me.Request.Form.Item(String.Format("Quantity_{0}", guid)))
                            Else
                                .Quantity = 0
                            End If

                            If IsNumeric(Me.Request.Form.Item(String.Format("UnitPrice_{0}", guid))) Then
                                .UnitPrice = CDec(Me.Request.Form.Item(String.Format("UnitPrice_{0}", guid)))
                            Else
                                .UnitPrice = 0
                            End If
                        End With

                        If ItemModel.Quantity > 0 AndAlso ItemModel.UnitPrice > 0 Then
                            lst.Add(ItemModel)
                        End If
                    Next

                    p.ItemList = lst
                End If

                Using biz As New MemberDocument
                    Status = biz.EditSalesRecord(p)
                End Using

                Return RedirectToAction("Index")
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Web API
        ' POST: SalesInvoice/Delete
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Delete(p As Members.Document) As ActionResult
            Dim Status = New Status
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberDocument
                    Status = biz.DeleteSalesRecord(MemberID, p.MemberDocumentID)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function
    End Class
End Namespace