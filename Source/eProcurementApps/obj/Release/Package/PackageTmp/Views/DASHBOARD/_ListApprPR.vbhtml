@ModelType IEnumerable(Of eProcurementApps.Models.INBOX_JOB_LIST)
@imports eProcurementApps.Helpers

@code

    If ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprReq) Or ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.CompReq) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("RequestApproveComplete", item.CONTROL, New With {.reqno = item.REQUEST_NO, .rel_flag = item.RELATION_FLAG, .control = item.CONTROL, .actions = item.ACTION, .data_flag = Convert.ToDecimal(item.STATUS), .access_from = "1"})">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>                    
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a class="blue" data-href="@Url.Action("RequestApproveComplete", item.CONTROL, New With {.reqno = item.REQUEST_NO, .rel_flag = item.RELATION_FLAG, .control = item.CONTROL, .actions = item.ACTION, .data_flag = Convert.ToDecimal(item.STATUS), .access_from = "1"})">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next
    ElseIf ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprWA) Or ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprRD) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.JOB_ID, .flag = ViewBag.FlagInbox})">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a class="blue" href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.JOB_ID, .flag = ViewBag.FlagInbox})" title="Detail">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next

    ElseIf ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrfPC) Or ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprAckPC) Or ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprPC) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("DetailPc", "PRICE_COMPARISON", New With {.id = item.JOB_ID, .flag = ViewBag.FlagInbox})">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a Class="blue" href="@Url.Action("DetailPc", "PRICE_COMPARISON", New With {.id = item.JOB_ID, .flag = ViewBag.FlagInbox})" title="Detail">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next
    ElseIf ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrfPO) Or ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprPO) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("DetailHeader", "PURCHASE_ORDER", New With {.id = item.JOB_ID, .flag = ViewBag.FlagInbox})">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a class="blue" href="@Url.Action("DetailHeader", "PURCHASE_ORDER", New With {.id = item.JOB_ID, .flag = ViewBag.FlagInbox})" title="Detail">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next
    ElseIf ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprGM) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("FormInput", "GM")?_Id=@item.JOB_ID&_Action=Approve&_FlagForm=@Convert.ToInt32(ListEnum.FlagInbox.ApprGM)">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a class="blue" href="@Url.Action("FormInput", "GM")?_Id=@item.JOB_ID&_Action=Approve&_FlagForm=@Convert.ToInt32(ListEnum.FlagInbox.ApprGM)" title="Approve">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next
    ElseIf ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrCRV) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("FormInput", "CRV")?_Id=@item.JOB_ID&_Action=Verified&_FlagForm=@Convert.ToInt32(ListEnum.FlagInbox.VrCRV)">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a class="blue" href="@Url.Action("FormInput", "CRV")?_Id=@item.JOB_ID&_Action=Verified&_FlagForm=@Convert.ToInt32(ListEnum.FlagInbox.VrCRV)" title="Verify">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next
    ElseIf ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprCRV) Then
        @for Each item In Model
            @<tr class='clickable-row' data-href="@Url.Action("FormInput", "CRV")?_Id=@item.JOB_ID&_Action=Approve&_FlagForm=@Convert.ToInt32(ListEnum.FlagInbox.ApprCRV)">
                <td>
                    <a href="#">
                        @item.REQUEST_NO
                    </a>
                </td>
                <td>
                    @item.REQUEST_BY
                </td>
                <td align="right">
                    @item.REQUEST_DATE.ToString("d-MMM-yy")
                </td>
                <td>
                    <a class="blue" href="@Url.Action("FormInput", "CRV")?_Id=@item.JOB_ID&_Action=Approve&_FlagForm=@Convert.ToInt32(ListEnum.FlagInbox.ApprCRV)" title="Approve">
                        @Html.Raw(Labels.IconAction("Details"))
                    </a>
                </td>
            </tr>
        Next
    End If



End Code

<script>
    jQuery(document).ready(function ($) {
        $(".clickable-row").click(function () {
            window.location = $(this).data("href");
        });
    });
</script>




