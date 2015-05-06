@ModelType Core.Entities.Members.DocumentVM

@If Model IsNot Nothing AndAlso Model.ItemList IsNot Nothing AndAlso Not Model.ItemList.Count = 0 Then
    @<table class="table table-hover table-striped">
        <thead>
            <tr>
                <th class="rowNum"></th>
                <th>Nomor Invoice</th>
                <th>Tanggal Invoice</th>
                <th class="text-right">Jumlah Bruto</th>
                <th class="text-right">PPN</th>
                <th class="text-right">Harus Dibayar</th>
                <th width="149"></th>
            </tr>
        </thead>
        <tbody>
            @For Each p In Model.ItemList
                @<tr>
                    <td>@String.Format("{0}.", p.RowNum)</td>
                    <td>@p.DocumentNo</td>
                    <td>@Core.Common.BindingHelper.BindDate(p.DocumentDate)</td>
                     <td class="text-right">@Core.Common.BindingHelper.BindAmount(p.GrandTotalAmount)</td>
                     <td class="text-right">@Core.Common.BindingHelper.BindAmount(p.VatTotalAmount)</td>
                     <td class="text-right">@Core.Common.BindingHelper.BindAmount(p.TotalAmount)</td>
                    <td>
                        <div class="btn-group">
                            <a href="@Url.Action("Edit", New With {.id = p.MemberDocumentID})" class="btn btn-sm btn-primary"><i class="fa fa-edit"></i> Edit</a>
                        </div>
                        <div class="btn-group">
                            <a href="#delete" data-toggle="modal" class="btn btn-sm btn-danger" data-id="@p.MemberDocumentID"><i class="fa fa-remove"></i> Delete</a>
                        </div>
                    </td>
                </tr>
            Next
        </tbody>
    </table>
    
    @Html.Partial("_pagination")
Else
    @<div>No records found.</div>
End If