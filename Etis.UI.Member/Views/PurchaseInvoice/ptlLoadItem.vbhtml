@ModelType IEnumerable(of Core.Entities.Members.DocumentItem)

@If Model IsNot Nothing AndAlso Not Model.Count = 0 Then
    For Each p In Model
        Dim guid = Left(System.Guid.NewGuid.ToString, 8)
        
        @<tr>
            <td><input type="hidden" name="Guid" value="@guid" />@Html.TaxItemTypeDdl(Value:=p.TaxItemType, ObjectName:=String.Format("TaxItemType_{0}", guid), ObjectClass:="form-control")</td>
            <td>@Html.TaxTypeDdl(Value:=p.TaxType, ObjectName:=String.Format("TaxType_{0}", guid), ObjectClass:="form-control")</td>
            <td>@Html.TaxObjectDdl(p.TaxItemType, p.TaxType, Value:=p.TaxObjectID, ObjectName:=String.Format("TaxObjectID_{0}", guid), ObjectClass:="form-control")</td>
            <td><input type="text" name="@String.Format("Description_{0}", guid)" value="@p.Description" class="form-control" /></td>
            <td><input type="text" name="@String.Format("Quantity_{0}", guid)" value="@p.Quantity" class="form-control" /></td>
            <td><input type="text" name="@String.Format("UnitPrice_{0}", guid)" value="@p.UnitPrice" class="form-control" /></td>
            <td class="text-right"><div name="@String.Format("SubTotalAmount_{0}", guid)" class="form-control-static">@p.SubGrandTotalAmount</div></td>
            <td><a href="#deleteitem" class="btn btn-sm btn-danger"><i class="fa fa-times"></i></a></td>
        </tr>
Next
End If