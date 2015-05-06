@ModelType Core.Entities.Members.Partner

<div class="modal-dialog modal-md">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <h4 class="modal-title">@Me.ViewData.Item("Title")</h4>
        </div>

        @Using Html.BeginForm("Edit", Me.ViewData.Item("Controller").ToString, Nothing, FormMethod.Post, New With {.id = "frmEdit", .class = "form-horizontal", .role = "form"})
            @Html.AntiForgeryToken
            @Html.ValidationSummary(True)
            @Html.HiddenFor(Function(m) m.MemberPartnerID)

            @<div class="modal-body">
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.FullName, New With {.class = "col-md-4 control-label"})
                    <div class="col-md-8">
                        @Html.TextBoxFor(Function(m) m.FullName, New With {.class = "form-control", .placeholder = "Name"})
                        @Html.ValidationMessageFor(Function(m) m.FullName)
                    </div>
                </div>
            </div>

            @<div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                <button type="submit" class="btn btn-primary">Save</button>
            </div>
        End Using
    </div>
</div>

<script>
    $(function () {
        $(".input-group.date").datepicker({
            format: "dd-M-yyyy"
        });
    })
</script>