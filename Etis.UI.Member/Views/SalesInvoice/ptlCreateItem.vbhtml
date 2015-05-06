@Code
    Dim guid = Left(System.Guid.NewGuid.ToString, 8)
End Code

<tr>
    <td><input type="hidden" name="Guid" value="@guid" />@Html.TaxItemTypeDdl(ObjectName:=String.Format("TaxItemType_{0}", guid), ObjectClass:="form-control")</td>
    <td>@Html.TaxTypeDdl(ObjectName:=String.Format("TaxType_{0}", guid), ObjectClass:="form-control")</td>
    <td>@Html.TaxObjectDdl("BR", "KP", ObjectName:=String.Format("TaxObjectID_{0}", guid), ObjectClass:="form-control")</td>
    <td><input type="text" name="@String.Format("Description_{0}", guid)" class="form-control" /></td>
    <td><input type="text" name="@String.Format("Quantity_{0}", guid)" class="form-control" /></td>
    <td><input type="text" name="@String.Format("UnitPrice_{0}", guid)" class="form-control" /></td>
    <td class="text-right"><div name="@String.Format("SubTotalAmount_{0}", guid)" class="form-control-static"></div></td>
    <td><a href="#deleteitem" class="btn btn-sm btn-danger"><i class="fa fa-times"></i></a></td>
</tr>