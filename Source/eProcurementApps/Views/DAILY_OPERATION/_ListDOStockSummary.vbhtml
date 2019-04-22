@ModelType IEnumerable(Of eProcurementApps.Models.DAILY_STOCK_SUMMARY)
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'DOStockSummary')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> Form Type </div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_form_type_id" class="hidden"></span>
                                    @Html.DropDownList("dropdownList", Dropdown.FormTypeStock)
                                    <label id="required_txt_form_type_id"></label>
                                </div>

                                <div Class="profile-info-name required"> Date </div>
                                <div Class="profile-info-value item-required">
                                    <span Class="dateText" id="txt_date"></span>
                                    <label id="required_txt_date"></label>
                                </div>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                @Html.Raw(Labels.ButtonForm("GetDataDOStockSummary"))
                                @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                            </div>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Type</th>
                                        <th>Account Code</th>
                                        <th>Desc./Periode</th>
                                        <th>Amount</th>
                                        <th>WA</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @code
                                        @For Each item In Model
                                            @<tr>
                                                <td>@item.TYPE</td>
                                                <td>@item.ACCOUNT_CODE</td>
                                                <td>@ViewBag.dt</td>
                                                <td style="text-align:right">@item.AMOUNT.ToString("###,###")</td>
                                                <td>@item.WA</td>
                                            </tr>
                                        Next
                                    End Code
                                </tbody>

                                <tfoot>
                                    <tr>
                                        <td colspan="3" style="font-weight:bold; background-color:yellow">Sub Total</td>
                                        <td style="text-align:right">
                                            @if IsNothing(ViewBag.GrandTotal) = False Then
                                                @ViewBag.GrandTotal
                                            End If
                                        </td>
                                        <td></td>
                                    </tr>
                                </tfoot>
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
          null, null, null,
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


