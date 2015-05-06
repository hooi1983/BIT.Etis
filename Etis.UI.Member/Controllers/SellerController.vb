Imports System.Web.Mvc

Namespace Controllers
    <AuthorizationFilter>
    Public Class SellerController
        Inherits Controller

        Private ReadOnly Controller As String = "Seller"
        Private ReadOnly Title As String = "Seller"

        ' GET: Seller
        Function Index() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = Me.Title

            Dim Model = New Members.PartnerVM
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New Core.Business.MemberPartner
                    Model = biz.GetSellerRecords(MemberID)
                End Using

                Return View(Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: Seller/ptlList
        <HttpGet>
        Function ptlList() As ActionResult
            Dim Model = New Members.PartnerVM
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New Core.Business.MemberPartner
                    Model = biz.GetSellerRecords(MemberID)
                End Using

                Return PartialView("ptlList", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: Seller/ptlCreate
        <HttpGet>
        Function ptlCreate() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Add {0}", Me.Title)

            Return PartialView("ptlCreate", New Members.Partner With {.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)})
        End Function

        ' Partial View
        ' GET: Seller/ptlEdit/{id}
        <HttpGet>
        Function ptlEdit(id As Integer) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Edit {0}", Me.Title)

            Dim Model = New Members.Partner
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberPartner
                    Model = biz.GetSellerRecord(MemberID, id)
                End Using

                Return PartialView("ptlEdit", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: Seller/ptlDelete/{id}
        <HttpGet>
        Function ptlDelete(id As Integer) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Delete {0}", Me.Title)

            Dim Model = New Members.Partner
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberPartner
                    Model = biz.GetSellerRecord(MemberID, id)
                End Using

                Return PartialView("ptlDelete", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Web API
        ' POST: Seller/Create
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Create(p As Members.Partner) As ActionResult
            Dim Status = New Status

            Try
                With p
                    p.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.ByID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.UserAgent = Me.Request.UserAgent
                    p.IpAddress = Me.Request.UserHostAddress
                End With

                Using biz As New MemberPartner
                    Status = biz.AddSellerRecord(p)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function

        ' Web API
        ' POST: Seller/Edit
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Edit(p As Members.Partner) As ActionResult
            Dim Status = New Status

            Try
                With p
                    p.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.ByID = Core.Common.GetValue.IdentityID(Me.User.Identity)
                    p.UserAgent = Me.Request.UserAgent
                    p.IpAddress = Me.Request.UserHostAddress
                End With

                Using biz As New MemberPartner
                    Status = biz.EditSellerRecord(p)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function

        ' Web API
        ' POST: Seller/Delete
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Delete(p As Members.Partner) As ActionResult
            Dim Status = New Status
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberPartner
                    Status = biz.DeleteSellerRecord(MemberID, p.MemberPartnerID)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function
    End Class
End Namespace