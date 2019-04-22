@Imports eProcurementApps.Helpers 
@Imports eProcurementApps.Controllers.CRVController 

@Code
    ViewBag.Breadcrumbs = "CRV"
    ViewBag.Title = "CRV List"
    ViewBag.CRV = "active open"
    ViewBag.IndexCreateCRV = "active"
End Code
 
<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-sm-12">
                <div id="CRV_Id" class="hidden">
                    @PrintID
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Directory Path </div>
                    <div class="profile-info-value item-required">
                        <span Class="freeText" id="txt_directorypath" maxlenght="50"></span>
                        <Label id="required_txt_directorypath"></Label>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Export to </div>
                    <div class="profile-info-value item-required">
                        <span id="txt_exportto" class="hidden">PDF</span>
                        <select id="dropdownlist_exportto">
                            <option value="PDF">PDF</option>
                            <option value="Excel">EXCEL</option>
                            <option value="JPEG">JPEG</option>
                        </select>
                    </div>
                    @Html.Raw(Labels.ButtonForm("SaveExport"))
                </div>
            </div>
        </div>
    </div>
</div>  
<script src="~/Scripts/Standard/StandardProfile.js"></script>  
<script src="~/Scripts/Controllers/CRVController.js"></script> 