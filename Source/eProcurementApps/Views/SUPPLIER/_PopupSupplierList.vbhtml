@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_SUPPLIER)
@Imports eProcurementApps.Helpers

@code
    Dim no As Integer = 1
    @If Model.Count() = 0 Then
        @<tr style='color:red'><td colspan='9'> No records founds</td></tr>
    Else
        @For Each item In Model
            @<tr>
                <td>@no</td>
                <td id="txt_id_supp_@no" style="display:none">
                    @item.ID
                </td>
                <td id="txt_supp_cd_@no">
                    @item.VENDOR_CODE
                </td>
                <td id="txt_supp_name_@no">
                    @item.SUPPLIER_NAME
                </td>
                <td id="txt_supp_alias_@no" style="display:none">
                    @item.SUPPLIER_ALIAS_NAME
                </td>
                <td id="txt_address_@no" style="display:none">
                    @item.SUPPLIER_ADDRESS
                </td>
                <td id="txt_cp_@no" style="display:none">
                    @item.CONTACT_PERSON
                </td>
                <td id="txt_email_@no" style="display:none">
                    @item.EMAIL_ADDRESS
                </td>
                <td><a onclick="PassingDataSupplier('@no')" title="Select" href="#">@Html.Raw(Labels.ButtonForm("Select"))</a></td>
            </tr>
            no = no + 1
        Next
     End If
End Code
