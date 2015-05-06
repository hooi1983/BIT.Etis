@ModelType Core.Entities.Members.PartnerVM

@If Model IsNot Nothing AndAlso Not Model.ItemList.Count = 0 Then
    @<table class="table table-hover table-striped">
        <thead>
            <tr>
                <th class="rowNum"></th>
                <th>Name</th>
                <th width="149"></th>
            </tr>
        </thead>
        <tbody>
            @For Each p In Model.ItemList
                @<tr>
                    <td>@String.Format("{0}.", p.RowNum)</td>
                    <td>@p.FullName</td>
                    <td>
                        <div class="btn-group">
                            <a href="#edit" data-toggle="modal" class="btn btn-sm btn-primary" data-id="@p.MemberPartnerID"><i class="fa fa-edit"></i> Edit</a>
                        </div>
                        <div class="btn-group">
                            <a href="#delete" data-toggle="modal" class="btn btn-sm btn-danger" data-id="@p.MemberPartnerID"><i class="fa fa-remove"></i> Delete</a>
                        </div>
                    </td>
                </tr>
            Next
        </tbody>
    </table>
Else
    @<div>No records found.</div>
End If