@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.STOCKController
 
@Code
    ViewBag.Breadcrumbs = "Stockmovement"
    ViewBag.Title = "Stockmovement Report"
    ViewBag.Report = "active open"
    ViewBag.IndexReportTrans = "active open"
    ViewBag.IndexReportStockMovement = "active"
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
                                            @<td> @dtTable.Rows(i).Item(a) </td>
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

<script type="text/javascript">
    var oTable1 =
    $('#myTable').DataTable({
        bAutoWidth: true,
        "aaSorting": [],
    });

    function SearchData() {
        if ($("#txt_dt_from").text().trim() == "") {
            alert("Please fill date from ");
            return false
        };
        if ($("#txt_dt_to").text().trim() == "") {
            alert("Please fill date to ");
            return false
        };
       
        txt_dt_from = $("#txt_dt_from").text().trim();
        txt_dt_to = $("#txt_dt_to").text().trim();
        $.ajax({
            url: linkProc + '/STOCK/ActionReport',
            type: 'Post',
            data: {
                _PeriodeFrom: txt_dt_from,
                _PeriodeTo: txt_dt_to,
            }, 
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function (result) {
                    $("#renderBody").html(result);
                    $("#loadingRole").toggle();
                    /*var Table = document.getElementById("myTable");

                    var strInnerHTML = "";
                    var parsed = JSON.parse(result); 
                    $("#table_ap").empty();
                    for (var x in parsed) {
                        strInnerHTML = strInnerHTML + "<tr>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["ITEM"] + "</td>"; 
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["UNIT OF MEASURE"] + "</td>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["CURRENT STOCK"] + "</td>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["STOCK IN"] + "</td>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["STOCK OUT"] + "</td>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["STOCK LAST"] + "</td>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["REF NO"] + "</td>";
                        strInnerHTML = strInnerHTML + "<td>" + parsed[x]["REF DATE"] + "</td>";
                        strInnerHTML = strInnerHTML + "</tr>";
                    }
                    if (strInnerHTML != "") {
                        $("#myTable").find('tbody').append(strInnerHTML);
                    } */
                },
        });
    }
</script>