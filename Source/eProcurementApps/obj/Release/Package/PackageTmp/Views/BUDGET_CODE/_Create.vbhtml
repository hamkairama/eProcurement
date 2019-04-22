@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Create SUN Budget"
    ViewBag.Setup = "active open"
    ViewBag.IndexBudgetCode = "active"
End Code

@If ViewBag.Message <> "" Then
    @<div Class="alert alert-block alert-error">
        <Button Class="close" data-dismiss="alert" type="button">
            <i Class="icon-remove"></i>
        </Button>
        <i Class="icon-warning-sign red"></i>
        @ViewBag.Message
    </div> End If

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-sm-5">
                <div Class="widget-box">
                    <div Class="widget-header">
                        <h4 Class="widget-title">SUN Budget Table</h4>
                    </div>
                    <div Class="widget-body" id="testing">
                        <div class="profile-user-info">
                            <div class="profile-info-row">
                                <div class="profile-info-name"> Table Budget </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_table_budget"></span>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Table for Usage </div>
                                <div Class="profile-info-value">
                                    <span class="freeText" id="txt_table_budget_usage"></span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name"> Is Active </div>
                                <div class="profile-info-value">
                                    <input type="checkbox" id="txt_is_active" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div Class="col-sm-7">
                <div Class="widget-box">
                    <div Class="widget-header">
                        <h4 Class="widget-title">T. Table</h4>
                    </div>
                    <div Class="widget-body" id="testing">
                        <div class="profile-user-info">
                            <div class="profile-info-row">
                                <div class="profile-info-name "> Table T1 </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_table_t1"></span>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Table T2 </div>
                                <div Class="profile-info-value ">
                                    <span class="freeText" id="txt_table_t2"></span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> Table T3 </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_table_t3"></span>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Table T5 </div>
                                <div Class="profile-info-value">
                                    <span class="freeText" id="txt_table_t5"></span>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div Class="col-sm-12">
                <div Class="widget-box">
                    <div Class="widget-header">
                        <h4 Class="widget-title">Export Purpose</h4>
                    </div>
                    <div Class="widget-body" id="testing">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Item</th>
                                    <th>Acc Code</th>
                                    <th>Move Type</th>
                                    <th>Start</th>
                                    <th>End</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Office Supplies</td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_usage"></span> </td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_mt"></span></td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_start"></span></td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_end"></span></td>
                                </tr>
                                <tr>
                                    <td>Printing</td>
                                    <td><span class="freeText" id="txt_cd_printing_usage"></span></td>
                                    <td><span class="freeText" id="txt_cd_printing_mt"></span></td>
                                    <td><span class="freeText" id="txt_cd_printing_start"></span></td>
                                    <td><span class="freeText" id="txt_cd_printing_end"></span></td>
                                </tr>
                                <tr>
                                    <td>Issued</td>
                                    <td><span class="freeText" id="txt_cd_issued_usage"></span></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>

                        </table>
                    </div>
                </div>
            </div>

            <div Class="clearfix form-action">
                <div Class="col-lg-12">
                    <div Class="modal-footer no-margin-top">
                        <a Class="red" href="@Url.Action("Index", "BUDGET_CODE")" title="Create">
                            @Html.Raw(Labels.ButtonForm("Close"))
                        </a>
                        @Html.Raw(Labels.ButtonForm("SaveCreate"))
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/BUDGET_CODEController.js"></script>