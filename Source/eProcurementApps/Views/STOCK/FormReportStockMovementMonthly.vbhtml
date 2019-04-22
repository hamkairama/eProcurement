@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.STOCKController
 
@Code
    ViewBag.Breadcrumbs = "Stockmovement Report Monthly"
    ViewBag.Title = "Stockmovement Report Monthly"
    ViewBag.Report = "active open"
    ViewBag.IndexReportTrans = "active open"
    ViewBag.IndexReportStockmovementMonthly = "active"
End Code

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-sm-12">
                <div class="profile-info-row">
                    <div class="profile-info-name"> Periode </div>
                    <div class="profile-info-value">
                        <label hidden="hidden" id="lbl_dt_from"></label>
                        <span Class="dateText" id="txt_dt_from"></span>
                        <label id="required_txt_dt_from"></label>
                        &nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;&nbsp;
                        <label hidden="hidden" id="lbl_dt_to"></label>
                        <span Class="dateText" id="txt_dt_to"></span>
                        <label id="required_txt_dt_to"></label>
                        &nbsp;&nbsp;
                        <button type="submit" id="btnExport" name="Export" onclick="SearchData()">Search</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div> 

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-xs-12">
                <div Class="row">
                    <div Class="col-xs-12">
                        <div Class="clearfix">
                            <div Class="pull-right tableTools-container">
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'CRV')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                        </div>

                        <table id="myTable" class="table table-striped table-bordered table-hover">
                            <thead id="table_th">
                                <tr>
                                    @for i = 0 To dtTable.Columns.Count - 1
                                        @<th> @dtTable.Columns(i).Caption </th>
                                    Next
                                </tr>
                            </thead>
                            <tbody id="table_ap" >
                                @For i = 0 To dtTable.Rows.Count - 1
                                    @<tr>
                                        @For a = 0 To dtTable.Columns.Count - 1
                                            @if Not (dtTable.Columns(a).Caption = "STOCK_CURRENT" OrElse
                    dtTable.Columns(a).Caption = "STOCK_IN" OrElse
                    dtTable.Columns(a).Caption = "STOCK_OUT" OrElse
                    dtTable.Columns(a).Caption = "BALANCE_STOCK" OrElse
                    dtTable.Columns(a).Caption = "PRICE" OrElse
                    dtTable.Columns(a).Caption = "TOTALPRICE") Then
                                                @<td> @dtTable.Rows(i).Item(a) </td>
                                            Else
                                                @<td> @Format(dtTable.Rows(i).Item(a), "###,##0.###0") </td>
                                            End If
                                        Next
                                    </tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script> 
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Controllers/STOCKController.js"></script>

<script type="text/javascript">
    var oTable1 =
    $('#myTable').DataTable({
        bAutoWidth: true,
        "aaSorting": [],
    });
</script>