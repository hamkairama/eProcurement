@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PR_HEADER)
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'TATReport')">
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
                                @if ViewBag.IndexList = ListEnum.FlagReport.TatComplete Then
                                    @Html.Raw(Labels.ButtonForm("GetTatComplete"))
                                ElseIf ViewBag.IndexList = ListEnum.FlagReport.TatSignOff Then
                                    @Html.Raw(Labels.ButtonForm("GetTatSignOff"))
                                ElseIf ViewBag.IndexList = ListEnum.FlagReport.TatUnComplete Then
                                    @Html.Raw(Labels.ButtonForm("GetTatUnComplete"))
                                End If

                                @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                            </div>
                        </div>
                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>PR No</th>
                                        <th>Form Type</th>
                                        <th>Req. By</th>
                                        <th>Req. Date</th>
                                        <th>Appr. Date</th>
                                        <th>Handle Date</th>
                                        <th>Complete Date</th>
                                        <th>Items Name</th>
                                        <th>SLA</th>
                                        <th>Req. Indicator</th>
                                        <th>Queue Handle</th>
                                        <th>Queue Complete</th>
                                        <th>Late/Match</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each pr In Model
                                        @<tr>
                                             <td><a href="@Url.Action("DetailHeaderByPrNo", "PURCHASING_REQUEST", New With {.pr_no = pr.PR_NO})" target="_blank">@pr.PR_NO</a>  </td>
                                            @*<td>
                                                @pr.PR_NO
                                            </td>*@
                                            <td>
                                                @pr.TPROC_FORM_TYPE.FORM_TYPE_NAME
                                            </td>
                                            <td>
                                                @pr.TPROC_USER.USER_NAME
                                            </td>
                                            <td align="right">
                                                @pr.PR_DATE.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @if IsNothing(pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.PrApprovedComplete.ToString()).FirstOrDefault()) = False Then
                                                    @pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.PrApprovedComplete.ToString()).FirstOrDefault().HISTORICAL_DT.ToString("dd-MM-yyyy hh:mm")
                                                End If                                               
                                            </td>
                                            <td>
                                                @if IsNothing(pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.PrHandled.ToString()).FirstOrDefault()) = False Then
                                                    @pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.PrHandled.ToString()).FirstOrDefault().HISTORICAL_DT.ToString("dd-MM-yyyy hh:mm")
                                                End If
                                                
                                            </td>
                                            <td>
                                                @if IsNothing(pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.Complete.ToString()).FirstOrDefault()) = False Then
                                                     @pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.Complete.ToString()).FirstOrDefault().HISTORICAL_DT.ToString("dd-MM-yyyy hh:mm")
                                                End If
                                            </td>
                                            <td>
                                                @code
                                                    Dim r_item As String = ""
                                                    For i As Integer = 0 To pr.TPROC_PR_DETAIL.Count - 1
                                                        r_item = r_item + pr.TPROC_PR_DETAIL(i).ITEM_NAME
                                                        If i < pr.TPROC_PR_DETAIL.Count - 1 Then
                                                            r_item = r_item + ", "
                                                        End If
                                                    Next
                                                    @r_item
                                                End Code
                                            </td>

                                            <td>
                                                @pr.DELIVERY_DAYS
                                            </td>
                                            <td>
                                                @pr.PR_INDICATOR
                                            </td>
                                            <td>
                                                @if IsNothing(pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.PrHandled.ToString()).FirstOrDefault()) = False Then
                                                    @pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.PrHandled.ToString()).FirstOrDefault().QUEUE
                                                End If                                               
                                            </td>
                                            <td>
                                                @if IsNothing(pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.Complete.ToString()).FirstOrDefault()) = False Then
                                                    @pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.Complete.ToString()).FirstOrDefault().QUEUE
                                                End If
                                            </td>
                                            <td>
                                                @code
                                                    @if ViewBag.IndexList <> ListEnum.FlagReport.TatUnComplete Then
                                                        Dim r As String = ""
                                                        Dim c_sla = pr.DELIVERY_DAYS
                                                        Dim c_complete = pr.TPROC_PR_HISTORICAL.Where(Function(x) x.HISTORICAL_STATUS = ListEnum.PRStatus.Complete.ToString()).FirstOrDefault().QUEUE
                                                        If c_complete <= c_sla Then
                                                            r = "Match"
                                                        Else
                                                            r = "Late"
                                                        End If
                                                        @r
                                                    End If
                                                End Code
                                            </td>

                                        </tr>
                                                    Next
                                </tbody>
                            </table>

                            @*<div class="space-4"></div>*@                           

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
<script src="~/Scripts/Controllers/REPORTController.js"></script>

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



