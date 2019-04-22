@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_STOCK)
@Imports eProcurementApps.Helpers

@code
    Dim no As Integer = 1
    @If Model.Count() = 0 Then
        @<tr style='color:red'><td colspan='10'> No records founds</td></tr>
    Else
        @For Each item In Model
            @<tr>
                <td>@no</td>
                <td id="txt_id_stk_@no">
                    @item.ID
                </td>
                <td id="txt_item_cd_@no">
                    @item.ITEM_CODE
                </td>
                <td id="txt_item_desc_@no">
                    @item.ITEM_DESCRIPTION
                </td>
                <td id="txt_lookup_cd_@no">
                    @item.LOOKUP_CODE
                </td>
                <td id="txt_uos_@no">
                    @item.UNIT_OF_STOCK
                </td>
                <td id="txt_lcost_@no">
                    @item.LATEST_COST
                </td>
                <td id="txt_gt_@no">
                    @item.TPROC_GOOD_TYPE.GOOD_TYPE_NAME
                </td>
                 <td id="txt_qty_storage_@no">                     
                     @CommonFunction.GetBalanceQtyMinItem(Convert.ToDecimal(item.QUANTITY.Value), Convert.ToDecimal(item.QUANTITY_MIN.Value))
                 </td>
                <td><a onclick="PassingDataItem('@no')" title="Select" href="#">@Html.Raw(Labels.ButtonForm("Select"))</a></td>
            </tr>
            no = no + 1
        Next
     End If
End Code
