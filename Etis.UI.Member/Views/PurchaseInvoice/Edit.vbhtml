@ModelType Core.Entities.Members.Document

<div class="row">
    <div class="col-md-12">
        <h3>@Me.ViewData.Item("Title")</h3>
    </div>
</div>

@Using Html.BeginForm("Edit", Me.ViewData.Item("Controller").ToString, Nothing, FormMethod.Post, New With {.id = "frmEdit", .class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken
    @Html.ValidationSummary(True)
    @Html.HiddenFor(Function(m) m.MemberDocumentID)

    @<div class="row">
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>Detail Faktur</h4>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="MemberPartnerID" class="col-md-4 control-label">Penjual <span class="required">*</span></label>
                        <div class="col-md-8">
                            @Html.SellerPartnerDdl(MemberID:=Model.MemberID, Value:=Model.MemberPartnerID, ObjectClass:="form-control")
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="DocumentNo" class="col-md-4 control-label">Nomor Invoice <span class="required">*</span></label>
                        <div class="col-md-8">
                            @Html.TextBoxFor(Function(m) m.DocumentNo, New With {.class = "form-control", .required = "required"})
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="DocumentDate" class="col-md-4 control-label">Tanggal Invoice <span class="required">*</span></label>
                        <div class="col-md-8">
                            <div class="input-group date">
                                @Html.TextBoxFor(Function(m) m.DocumentDate, "{0:dd-MMM-yyyy}", New With {.class = "form-control", .required = "required"})
                                <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="DueDate" class="col-md-4 control-label">Jatuh Tempo Pembayaran <span class="required">*</span></label>
                        <div class="col-md-8">
                            <div class="input-group date">
                                @Html.TextBoxFor(Function(m) m.DueDate, "{0:dd-MMM-yyyy}", New With {.class = "form-control", .required = "required"})
                                <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="PaymentDate" class="col-md-4 control-label">Tanggal Pembayaran</label>
                        <div class="col-md-8">
                            <div class="input-group date">
                                @Html.TextBoxFor(Function(m) m.PaymentDate, "{0:dd-MMM-yyyy}", New With {.class = "form-control", .required = "required"})
                                <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>Detail PPN</h4>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="TransactionTypeID" class="col-md-4 control-label">Jenis Transaksi</label>
                        <div class="col-md-8">
                            @Html.TransactionTypeDdl(Value:=Model.TransactionTypeID, ShowDefault:=True, ObjectClass:="form-control")
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label">Detail Transaksi</label>
                        <div class="col-md-8">
                            @Html.TransactionTypeSubDdl(ParentID:=Model.TransactionTypeID, Value:=Model.TransactionTypeSubID, ObjectClass:="form-control")
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    @<div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-md-6">
                            <h4>Item Faktur</h4>
                        </div>
                        <div class="col-md-6 text-right">
                            <a id="createitem" href="#" class="btn btn-sm btn-primary"><i class="fa fa-plus"></i> Tambah Item Baru</a>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <table id="tblitem" class="table table-bordered table-hover table-responsive table-striped">
                        <thead>
                            <tr>
                                <th>ITEM TYPE</th>
                                <th>PAJAK TYPE</th>
                                <th>OBJEK PAJAK</th>
                                <th>DESKRIPSI</th>
                                <th>KUANTITAS</th>
                                <th>DPP</th>
                                <th>JUMLAH BRUTO (Rp)</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            @Html.Partial("ptlLoadItem", Model.ItemList)
                        </tbody>
                    </table>
                </div>
                <div class="panel-footer">
                    <div class="row text-right">
                        <div class="col-md-5 col-md-offset-7">
                            <div class="row">
                                <div class="col-md-5">
                                    <strong>Jumlah Bruto :</strong>
                                </div>
                                <div class="col-md-7">
                                    <span id="GrandTotalAmount">@Model.GrandTotalAmount</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <strong>PPN :</strong>
                                </div>
                                <div class="col-md-7">
                                    (+<span id="VatTotalAmount">@Model.VatTotalAmount</span>)
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12"><hr /></div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <strong>Harus dibayar :</strong>
                                </div>
                                <div class="col-md-7">
                                    <strong><span id="TotalAmount">@Model.TotalAmount</span></strong>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    @<div class="row">
        <div class="col-md-12 text-right">
            <button class="btn btn-lg btn-danger"><i class="fa fa-times"></i> Batal</button> <button type="submit" class="btn btn-lg btn-success"><i class="fa fa-save"></i> Simpan</button>
        </div>
    </div>
End Using

@Section scripts
    <script>
        $(function () {
            $(".input-group.date").datepicker({
                format: "dd-M-yyyy"
            });

            $("#TransactionTypeID").change(function () {
                var id = $(this).val();

                $.ajax({
                    url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptlTransactionTypeSub",
                    type: "GET",
                    data: { id: id }
                }).done(function (data) {
                    $("#TransactionTypeSubID").html(data);
                });
            });

            $("#createitem").click(function () {
                $.ajax({
                    url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptlCreateItem",
                    type: "GET"
                }).done(function (data) {
                    $("#tblitem > tbody:last").append(data);
                });

                return false;
            });

            $("#tblitem tbody").on("click", "a[href='#deleteitem']", function () {
                var res = confirm("Are you want to remove this row?");

                if (res) {
                    $(this).closest("tr").remove();
                    calc();
                }

                return false;
            });

            $("#tblitem tbody").on("change", "[name^='TaxItemType_'],[name^='TaxType_']", function () {
                var tr = $(this).closest("tr");

                var objTaxItemType = tr.find("[name^='TaxItemType_']");
                var objTaxType = tr.find("[name^='TaxType_']");
                var objTaxObject = tr.find("[name^='TaxObjectID_']");

                var taxItemType = objTaxItemType.val();
                var taxType = objTaxType.val();

                $.ajax({
                    url: "/@Me.ViewData.Item("Controller").ToString.ToLower/ptlTaxObject",
                    type: "GET",
                    data: { taxItemType: taxItemType, taxType: taxType }
                }).done(function (data) {
                    objTaxObject.html(data);

                    calc();
                });
            });

            $("#tblitem tbody").on("change", "[name^='TaxObjectID_']", function () {
                var tr = $(this).closest("tr");

                calc();
            });

            $("#tblitem tbody").on("input", "[name^='Quantity_'],[name^='UnitPrice_']", function () {
                var tr = $(this).closest("tr");

                calc();
            });

            function calcRow(tr) {
                var objTaxObject = tr.find("[name^='TaxObjectID_']");
                var objQuantity = tr.find("[name^='Quantity_']");
                var objUnitPrice = tr.find("[name^='UnitPrice_']");
                var objSubTotalAmount = tr.find("[name^='SubTotalAmount_']");

                var taxPercent = parseFloat(objTaxObject.children("option:selected").data("taxpercent")) || 0;
                var quantity = parseFloat(objQuantity.val()) || 0;
                var unitPrice = parseFloat(objUnitPrice.val()) || 0;
                var subTotalAmount = quantity * unitPrice * (100 + taxPercent) / 100;

                objSubTotalAmount.text(subTotalAmount.toFixed(2));
            };

            function calc() {
                var objGrandTotalAmount = $("#GrandTotalAmount");
                var objVatTotalAmount = $("#VatTotalAmount");
                var objTotalAmount = $("#TotalAmount");

                var grandTotalAmount = 0;
                var vatTotalAmount = 0;
                var totalAmount = 0;

                $("#tblitem tbody tr").each(function () {
                    tr = $(this);

                    var objTaxObject = tr.find("[name^='TaxObjectID_']");
                    var objQuantity = tr.find("[name^='Quantity_']");
                    var objUnitPrice = tr.find("[name^='UnitPrice_']");
                    var objSubTotalAmount = tr.find("[name^='SubTotalAmount_']");

                    var taxPercent = parseFloat(objTaxObject.children("option:selected").data("taxpercent")) || 0;
                    var quantity = parseFloat(objQuantity.val()) || 0;
                    var unitPrice = parseFloat(objUnitPrice.val()) || 0;
                    var subTotalAmount = quantity * unitPrice;
                    var vatSubTotalAmount = quantity * unitPrice * taxPercent / 100;

                    grandTotalAmount += subTotalAmount;
                    vatTotalAmount += vatSubTotalAmount;
                    totalAmount += (subTotalAmount + vatSubTotalAmount);

                    objSubTotalAmount.text(subTotalAmount.toFixed(2));
                });

                objGrandTotalAmount.text(grandTotalAmount.toFixed(2));
                objVatTotalAmount.text(vatTotalAmount.toFixed(2));
                objTotalAmount.text(totalAmount.toFixed(2));
            };
        })
    </script>
End Section