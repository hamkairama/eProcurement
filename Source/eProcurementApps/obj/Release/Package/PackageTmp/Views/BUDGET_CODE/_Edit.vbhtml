@ModelType eProcurementApps.Models.TPROC_BUDGET_CODE
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Edit SUN Budget"
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
    </div>
End If

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-sm-5">
                <div Class="widget-box">
                    <div Class="widget-header">
                        <h4 Class="widget-title">SUN Budget Table</h4> <span class="hidden" id="txt_id">@Model.ID</span> <span class="hidden" id="txt_linkProc">@CommonFunction.GetLinkEproc</span>
                    </div>
                    <div Class="widget-body" id="testing">
                        <div class="profile-user-info">
                            <div class="profile-info-row">
                                <div class="profile-info-name"> Table Budget </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_table_budget">@Model.TABLE_BUDGET</span>
                                    <label id="required_txt_table_budget"></label>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Table for Usage </div>
                                <div Class="profile-info-value">
                                    <span class="freeText" id="txt_table_budget_usage">@Model.TABLE_BUDGET_USAGE</span>
                                    <label id="required_txt_table_budget_usage"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name"> Is Active </div>
                                <div class="profile-info-value">
                                    @if Model.IS_ACTIVE = 1 Then
                                        @<input type="checkbox" id="txt_is_active" checked="checked" />
                                    Else
                                        @<input type="checkbox" id="txt_is_active" />
                                    End If
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
                                <div class="profile-info-name"> Table T1 </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_table_t1">@Model.TABLE_T1</span>
                                    <label id="required_txt_table_t1"></label>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Table T2 </div>
                                <div Class="profile-info-value">
                                    <span class="freeText" id="txt_table_t2"> @Model.TABLE_T2</span>
                                    <label id="required_txt_table_t2"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name"> Table T3 </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_table_t3"> @Model.TABLE_T3</span>
                                    <label id="required_txt_table_t2"></label>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Table T5 </div>
                                <div Class="profile-info-value">
                                    <span class="freeText" id="txt_table_t5">@Model.TABLE_T5</span>
                                    <label id="required_txt_table_t5"></label>
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
                                    <td><span class="freeText" id="txt_cd_office_supplie_usage">@Model.CD_OFFICE_SUPPLIE_USAGE</span> </td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_mt">@Model.CD_OFFICE_SUPPLIE_MT</span></td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_start">@Model.CD_OFFICE_SUPPLIE_START</span></td>
                                    <td><span class="freeText" id="txt_cd_office_supplie_end">@Model.CD_OFFICE_SUPPLIE_END</span></td>
                                </tr>
                                <tr>
                                    <td>Printing</td>
                                    <td><span class="freeText" id="txt_cd_printing_usage">@Model.CD_PRINTING_USAGE</span></td>
                                    <td><span class="freeText" id="txt_cd_printing_mt">@Model.CD_PRINTING_MT</span></td>
                                    <td><span class="freeText" id="txt_cd_printing_start">@Model.CD_PRINTING_START</span></td>
                                    <td><span class="freeText" id="txt_cd_printing_end">@Model.CD_PRINTING_END</span></td>
                                </tr>
                                <tr>
                                    <td>Issued</td>
                                    <td><span class="freeText" id="txt_cd_issued_usage">@Model.CD_ISSUED_USAGE</span></td>
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
                        @Html.Raw(Labels.ButtonForm("SaveEdit"))
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/BUDGET_CODEController.js"></script>
