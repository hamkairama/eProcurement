@Imports eProcurementApps.Helpers 
@Imports eProcurementApps.Controllers.CRVController 

@Code
    ViewBag.Breadcrumbs = "CRV"
    ViewBag.Title = "CRV Report"
    ViewBag.Report = "active open"
    ViewBag.IndexReportTrans = "active"
    ViewBag.IndexReportCRV = "active"
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
                        <button type="submit" id="btnExport" name="Export" onclick="ExportFile()">Export</button>
                    </div>
                </div> 
            </div>
        </div>
    </div>
</div>  
<script src="~/Scripts/Standard/StandardProfile.js"></script>  
<script src="~/Scripts/Controllers/CRVController.js"></script> 
<script>
    function ExportFile() {
        txt_dt_from = $("#txt_dt_from").text().trim();
        txt_dt_to = $("#txt_dt_to").text().trim();
        $.ajax({
            url: linkProc + '/CRV/ActionReport',
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
                    $(".dialogForm").dialog("close");
                    alert(result);
                },
        });
    }
</script>