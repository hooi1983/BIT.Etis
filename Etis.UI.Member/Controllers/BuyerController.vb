Imports System.Web.Mvc

Namespace Controllers
    <AuthorizationFilter>
    Public Class BuyerController
        Inherits Controller

        Private ReadOnly Controller As String = "Buyer"
        Private ReadOnly Title As String = "Buyer"

        ' GET: Buyer
        Function Index() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = Me.Title

            Dim Model = New Members.PartnerVM
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New Core.Business.MemberPartner
                    Model = biz.GetBuyerRecords(MemberID)
                End Using

                Return View(Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: Buyer/ptlList
        <HttpGet>
        Function ptlList() As ActionResult
            Dim Model = New Members.PartnerVM
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New Core.Business.MemberPartner
                    Model = biz.GetBuyerRecords(MemberID)
                End Using

                Return PartialView("ptlList", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: Buyer/ptlCreate
        <HttpGet>
        Function ptlCreate() As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Add {0}", Me.Title)

            Return PartialView("ptlCreate", New Members.Partner With {.MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)})
        End Function

        ' Partial View
        ' GET: Buyer/ptlEdit/{id}
        <HttpGet>
        Function ptlEdit(id As Integer) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Edit {0}", Me.Title)

            Dim Model = New Members.Partner
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberPartner
                    Model = biz.GetBuyerRecord(MemberID, id)
                End Using

                Return PartialView("ptlEdit", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Partial View
        ' GET: Buyer/ptlDelete/{id}
        <HttpGet>
        Function ptlDelete(id As Integer) As ActionResult
            Me.ViewData.Item("Controller") = Me.Controller
            Me.ViewData.Item("Title") = String.Format("Delete {0}", Me.Title)

            Dim Model = New Members.Partner
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberPartner
                    Model = biz.GetBuyerRecord(MemberID, id)
                End Using

                Return PartialView("ptlDelete", Model)
            Catch ex As Exception
                Throw
            End Try
        End Function

        ' Web API
        ' POST: Buyer/Create
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
                    Status = biz.AddBuyerRecord(p)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function

        ' Web API
        ' POST: Buyer/Edit
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
                    Status = biz.EditBuyerRecord(p)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function

        ' Web API
        ' POST: Buyer/Delete
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Delete(p As Members.Partner) As ActionResult
            Dim Status = New Status
            Dim MemberID = Core.Common.GetValue.IdentityID(Me.User.Identity)

            Try
                Using biz As New MemberPartner
                    Status = biz.DeleteBuyerRecord(MemberID, p.MemberPartnerID)
                End Using

                Return Json(Status, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New Status("0", ex.Message), JsonRequestBehavior.AllowGet)
            End Try
        End Function
    End Class
End Namespace