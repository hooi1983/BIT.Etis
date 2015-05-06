@ModelType Core.Entities.Members.DocumentVM

<div class="col-md-12">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-4">
                    <p class="panel-title">@Me.ViewData.Item("Title")</p>
                </div>
                <div class="col-md-8 text-right">
                    <div class="btn-group">
                        <a href="@Url.Action("Create", "SalesInvoice")" class="btn btn-sm btn-success"><i class="fa fa-plus"></i> Create New</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="row">
                    <div class="col-md-5 col-md-offset-7">
                        <div class="form-group">
                            <div class="col-md-8 col-md-offset-4">
                                <div class="input-group">
                                    <input id="txtSearch" type="text" class="form-control input-sm" />
                                    <span class="input-group-btn">
                                        <button id="btnSearch" class="btn btn-sm">
                                            <i class="fa fa-search"></i> Search
                                        </button>
                                        <button id="btnReset" class="btn btn-sm">
                                            <i class="fa fa-refresh"></i> Reset
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
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
        /* search */
        $("#btnSearch").click(function () {
            var s = $("#txtSearch").val().trim();

            if (s.length !== 0)
            {
                $.ajax({
                    url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptllist/1?s=" + s,
                    type: "GET",
                    dataType: "html"
                }).done(function (data) {
                    $("#ptlList").html(data);
                });
            }
        });

        /* reset */
        $("#btnReset").click(function () {
            $("#txtSearch").val("");

            $.ajax({
                url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptllist/1",
                type: "GET",
                dataType: "html"
            }).done(function (data) {
                $("#ptlList").html(data);
            });
        });

        /* pagination */
        $("#ptlList").on("click", "ul.pagination > li > a", function () {
            var page = $(this).data("page");
            var s = $(this).data("search");

            $.ajax({
                url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptllist/" + page + "?s=" + s,
                type: "GET",
                dataType: "html"
            }).done(function (data) {
                $("#ptlList").html(data);
            });

            return false;
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