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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'DONonStock')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> Form Type </div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_form_type_id" class="hidden"></span>
                                    @Html.DropDownList("dropdownList", Dropdown.FormTypeNonStock)
                                    <label id="required_txt_form_type_id"></label>
                                </div>

                                <div Class="profile-info-name required"> Date </div>
                                <div Class="profile-info-value item-required">
                                    <span Class="dateText" id="txt_date"></span>
                                    <label id="required_txt_date"></label>
                                </div>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                @Html.Raw(Labels.ButtonForm("GetDataDONonStock"))
                                @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                            </div>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Req. No</th>
                                        <th>Req. By</th>
                                        <th>Req. Date</th>
                                        <th>Department</th>
                                        <th>Item Cd</th>
                                        <th>Item Name</th>
                                        <th>WA</th>
                                        <th>Qty</th>
                                        <th>Revise Qty</th>
                                        <th>Price</th>
                                        <th>Acc Cd</th>
                                        <th>Remark</th>
                                        <th>Vendor</th>
                                    </tr>
                                </thead>

                                @code
                                    Dim sun = CommonFunction.GetSunBudgetCode()
                                    Dim mt As String = ""
                                    Dim loc As String = "SSS"
                                    Dim itm_cd As String = ""

                                    If CommonFunction.GetFormTypeName(Convert.ToInt32(ViewBag.ft)) = "PRINTING" Then
                                        mt = sun.CD_PRINTING_MT
                                    ElseIf CommonFunction.GetFormTypeName(Convert.ToInt32(ViewBag.ft)) = "OFFICE SUPPLIES" Then
                                        mt = sun.CD_OFFICE_SUPPLIE_MT
                                    End If

                                    @<tbody>
                                        @For Each item In Model
                                            If item.ITEM_ID IsNot Nothing Then
                                                If item.ITEM_ID.Value <> Nothing Or item.ITEM_ID > 0 Then
                                                    itm_cd = CommonFunction.GetItemCd(item.ITEM_ID.Value)
                                                End If
                                            Else
                                                itm_cd = ""
                                            End If


                                            @<tr>
                                                <td><a href="@Url.Action("DetailHeaderByPrNo", "PURCHASING_REQUEST", New With {.pr_no = item.TPROC_PR_HEADER.PR_NO})" target="_blank">@item.TPROC_PR_HEADER.PR_NO</a>  </td>
                                                <td>@item.TPROC_PR_HEADER.TPROC_USER.USER_NAME</td>
                                                <td>@item.TPROC_PR_HEADER.PR_DATE.ToString("dd-MM-yyyy")</td>
                                                <td>@item.TPROC_WA.TPROC_APPROVAL_GR.DEPARTMENT_NAME</td>
                                                <td>@itm_cd</td>
                                                <td>@item.ITEM_NAME</td>
                                                <td>@item.TPROC_WA.WA_NUMBER</td>
                                                <td>@item.QTY</td>
                                                <td>@item.REVISED_QTY</td>
                                                <td style="text-align:right">@item.PRICE.ToString("###,###")</td>
                                                <td>@item.TPROC_PR_HEADER.ACCOUNT_CODE</td>
                                                <td>@item.REMARK</td>
                                                <td>@item.SUPPLIER_NAME</td>
                                            </tr>
                                        Next
                                    </tbody>
                                End Code
                            </table>
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
          null, null, null, null, null, null, null, null, null, null, null, 
          { "bSortable": false }
        ],
        "aaSorting": [],

        //,
        //"sScrollY": "200px",
        //"bPaginate": false,

        //"sScrollX": "100%",
        //"sScrollXInner": "120%",
        //"bScrollCollapse": true,
        //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

        //"iDisplayLength": 50
    });
</script>

<script>
    $(function () {
        $('#dropdownList').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_form_type_id').text(optionSelected);
        });
    });
</script>


