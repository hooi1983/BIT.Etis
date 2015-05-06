@ModelType Core.Entities.Members.PartnerVM

<div class="col-md-12">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-4">
                    <p class="panel-title">@Me.ViewData.Item("Title")</p>
                </div>
                <div class="col-md-8 text-right">
                    <div class="btn-group">
                        <a href="#create" data-toggle="modal" class="btn btn-sm btn-success"><i class="fa fa-plus"></i> Create New</a>
                    </div>
                </div>
            </div>
        </div>
        <div id="ptlList" class="panel-body">
            @Html.Partial("ptlList", Model)
        </div>
    </div>
</div>

<div id="modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="label" aria-hidden="true"></div>

@Section scripts
<script>
    $(function () {
        $("a[href='#create']").click(function () {
            $.ajax({
                url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptlCreate",
                type: "GET"
            }).done(function (data) {
                $("#modal").html(data).modal("show");
            });
        });

        /* call Edit modal */
        $("#ptlList").on("click", "a[href='#edit']", function () {
            var id = $(this).data("id") || 0;

            $.ajax({
                url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptlEdit/" + id,
                type: "GET"
            }).done(function (data) {
                $("#modal").html(data).modal("show");
            });
        });

        /* call Delete modal */
        $("#ptlList").on("click", "a[href='#delete']", function () {
            var id = $(this).data("id") || 0;

            $.ajax({
                url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptlDelete/" + id,
                type: "GET"
            }).done(function (data) {
                $("#modal").html(data).modal("show");
            });
        });

        /* submit Create form */
        $("#modal").on("submit", "#frmCreate", function (e) {
            e.preventDefault();

            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize()
            }).done(function (data) {
                if (data.Code === "1") {
                    $("#modal").modal("hide");

                    $.ajax({
                        url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptllist",
                        type: "GET",
                        dataType: "html"
                    }).done(function (data) {
                        $("#ptlList").html(data);
                    });
                } else {
                    alert(data.Message)
                }
            }).fail(function (e) {
                alert("Cannot perform at this time.")
            });
        });

        /* submit Edit form */
        $("#modal").on("submit", "#frmEdit", function (e) {
            e.preventDefault();

            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize()
            }).done(function (data) {
                if (data.Code === "1") {
                    $("#modal").modal("hide");

                    $.ajax({
                        url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptllist",
                        type: "GET",
                        dataType: "html"
                    }).done(function (data) {
                        $("#ptlList").html(data);
                    });
                } else {
                    alert(data.Message)
                }
            }).fail(function (e) {
                alert("Cannot perform at this time.")
            });
        });

        /* submit Delete form */
        $("#modal").on("submit", "#frmDelete", function (e) {
            e.preventDefault();

            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize()
            }).done(function (data) {
                if (data.Code === "1") {
                    $("#modal").modal("hide");

                    $.ajax({
                        url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptllist",
                        type: "GET",
                        dataType: "html"
                    }).done(function (data) {
                        $("#ptlList").html(data);
                    });
                } else {
                    alert(data.Message)
                }
            }).fail(function (e) {
                alert("Cannot perform at this time.")
            });
        });
    });
</script>
End Section