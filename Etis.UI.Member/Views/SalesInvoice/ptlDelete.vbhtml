@ModelType Core.Entities.Members.Document

<div class="modal-dialog modal-md">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <h4 class="modal-title">@Me.ViewData.Item("Title")</h4>
        </div>

        @Using Html.BeginForm("Delete", Me.ViewData.Item("Controller").ToString, Nothing, FormMethod.Post, New With {.id = "frmDelete", .class = "form-horizontal", .role = "form"})
            @Html.AntiForgeryToken
            @Html.ValidationSummary(True)
            @Html.HiddenFor(Function(m) m.MemberDocumentID)

            @<div class="modal-body">
                Do you sure want to delete this record?
            </div>

            @<div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                <button type="submit" class="btn btn-danger">Delete</button>
            </div>
        End Using
    </div>
</div>