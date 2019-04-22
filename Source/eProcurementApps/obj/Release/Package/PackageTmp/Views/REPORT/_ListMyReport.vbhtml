@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PR_DETAIL)
@Imports eProcurementApps.Helpers

@If ViewBag.Message <> "" Then
    @<div Class="alert alert-block alert-error">
        <Button Class="close" data-dismiss="alert" type="button">
            <i Class="icon-remove"></i>
        </Button>
        <i Class="icon-warning-sign red"></i>
        @ViewBag.Message
    </div>
End If


<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-xs-12">
                <div Class="row">
                    <div Class="col-xs-12">
                        <div Class="clearfix">
                            <div Class="pull-right tableTools-container">
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'MyListReport')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                             <div class="profile-info-row">
                                <div Class="profile-info-name required"> Date From </div>
                                <div Class="profile-info-value item-required">
                                    <span Class="dateText" id="txt_date_from"></span>
                                    <label id="required_txt_date_from"></label>
                                </div>

                                <div Class="profile-info-name required"> Date To</div>
                                <div Class="profile-info-value item-required">
                                    <span Class="dateText" id="txt_date_to"></span>
                                    <label id="required_txt_date_to"></label>
                                </div>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 @Html.Raw(Labels.ButtonForm("GetMyReportPrList"))

                                 @if Session("IS_SUPER_ADMIN") = 1 Then
                                    @Html.Raw(Labels.ButtonForm("GetAllReportPrList"))
                                 End If
                
                                @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                            </div>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>PR No</th>
                                        <th>User Name</th>
                                        <th>PR Date</th>
                                        <th>Form Type</th>
                                        <th>Sub Type</th>
                                        <th>Good Type</th>
                                        <th>Dev Days</th>
                                        <th>Exp Dev Date</th>
                                        <th>PR Indicator</th>
                                        <th>Account Code</th>
                                        <th>PR Status</th>
                                        <th>Sub Total (Rp)</th>
                                        <th>Item Name</th>
                                        <th>Specification</th>
                                        <th>Currency</th>
                                        <th>Quantity</th>
                                        <th>User Measurement</th>
                                        <th>WA</th>
                                        <th>Revise Qty</th>
                                        <th>Price</th>
                                        <th>Total Price</th>
                                        <th>Remark</th>
                                        <th>Supp Name</th>
                                        <th>PO No</th>
                                        <th>Reject Reason</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr>
                                            <td>
                                                @item.TPROC_PR_HEADER.PR_NO
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.TPROC_USER.USER_NAME
                                            </td>
                                            <td align="right">
                                                @item.TPROC_PR_HEADER.PR_DATE.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.TPROC_FORM_TYPE.FORM_TYPE_NAME
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.TPROC_FORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.TPROC_GOOD_TYPE.GOOD_TYPE_NAME
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.DELIVERY_DAYS
                                            </td>
                                            <td align="right">
                                                @item.TPROC_PR_HEADER.EXP_DEV_DT.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.PR_INDICATOR
                                            </td>
                                            <td>
                                                @item.TPROC_PR_HEADER.ACCOUNT_CODE
                                            </td>

                                            @code
                                                Dim status As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(item.TPROC_PR_HEADER.PR_STATUS)).ToString()
                                                @<td>@status</td>
                                            End Code

                                            <td style="text-align:right">
                                                @item.TPROC_PR_HEADER.SUB_TOTAL.ToString("###,###")
                                            </td>

                                            <td>
                                                @item.ITEM_NAME
                                            </td>
                                            <td>
                                                @item.SPECIFICATION
                                            </td>
                                            <td>
                                                @item.CURRENCY
                                            </td>
                                            <td>
                                                @item.QTY
                                            </td>
                                            <td>
                                                @item.USER_MEASUREMENT
                                            </td>
                                            <td>
                                                @item.TPROC_WA.WA_NUMBER
                                            </td>
                                            <td>
                                                @item.REVISED_QTY
                                            </td>
                                            <td>
                                                @item.PRICE.ToString("###,###")
                                            </td>
                                            <td>
                                                @item.TOTAL_PRICE.ToString("###,###")
                                            </td>
                                            <td>
                                                @item.REMARK
                                            </td>
                                            <td>
                                                @item.SUPPLIER_NAME
                                            </td>
                                             <td>
                                                 @item.PO_NUMBER
                                             </td>
                                            <td>
                                                @item.REJECT_REASON
                                            </td>
                                        </tr>
                                                Next
                                </tbody>
                            </table>

                            @*<div class="space-4"></div>
                          
                           @Html.Raw(Labels.ButtonForm("GetMyReport"))*@
                      
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Standard/StandardTable.js"></script>

<script type="text/javascript">
    //initiate dataTables plugin
    var oTable1 =
    $('#dynamic-table')
    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
    .dataTable({
        bAutoWidth: false,
        "aoColumns": [
          { "bSortable": false },
          null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
          { "bSortable": false }
        ],
        "aaSorting": [],

        //,
        //"sScrollY": "200px",
        //"bPaginate": false,

        "sScrollX": "100%",
        //"sScrollXInner": "120%",
        //"bScrollCollapse": true,
        //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

        //"iDisplayLength": 50
    });
</script>



