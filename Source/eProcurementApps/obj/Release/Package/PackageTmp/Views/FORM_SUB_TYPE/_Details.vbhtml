@ModelType eProcurementApps.Models.TPROC_FORM_SUB_TYPE
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Form Sub Type
            </div>
        </div>
        <div class="modal-body no-padding">

            <div class="col-sm-12">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="widget-title">Form Sub Type Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                    </div>
                    <div class="widget-body" id="testing">
                        <div class="profile-user-info">

                            <div class="profile-info-row">
                                <div class="profile-info-name "> F. Type </div>
                                <div class="profile-info-value ">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.TPROC_FORM_TYPE.FORM_TYPE_NAME</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> F. SubType </div>
                                <div class="profile-info-value">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME</span>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Description </div>
                                <div Class="profile-info-value">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_DESCRIPTION</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name"> SLA </div>
                                <div class="profile-info-value">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.SLA</span>
                                </div>
                            </div>
                        </div>

                        <div style="" id="">
                            <div class="profile-info-row">
                                <div class="profile-info-name"> Budget Code </div>
                                <div class="profile-info-value">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.BUDGET_CODE</span>
                                </div>

                                <div class="profile-info-name"> Start </div>
                                <div class="profile-info-value">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_START</span>
                                </div>

                                <div class="profile-info-name"> End </div>
                                <div class="profile-info-value">
                                    <span>@Model.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_END</span>
                                </div>
                            </div>
                        </div>


                        <div class="profile-info-row">
                            <div class="profile-info-name"> Account Code Add</div>
                            <div class="profile-info-value">
                                @if Model.TPROC_FORM_SUBTYPE_GR.POPUP_ACCOUNT = 1 Then
                                    @<input type="checkbox" id="cb_popup_account" checked="checked" />
                                Else
                                    @<input type="checkbox" id="cb_popup_account" />
                                End If

                                <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Adding other budget code">?</span>

                            </div>
                        </div>

                    </div>

                </div>
            </div>


            <div id="budget_area">
                <div class="col-sm-12">
                    <div class="widget-box">
                        <div class="widget-header">
                            <h4 class="widget-title">Additional Budget code</h4>
                        </div>
                        <div class="widget-body" id="testing">
                            <table id="simple-table" class="table table-striped table-bordered table-hover">
                                <tbody>
                                    <tr>
                                        <th>Budget Code</th>
                                        <th>Account Code Start</th>
                                        <th>Account Code End</th>
                                    </tr>
                                </tbody>
                                <tbody id="dataTableBc">
                                @For Each item In Model.TPROC_FORM_SUBTYPE_GR.TPROC_FST_BUDGET_CD_ADD
                                    @<tr id="0">
                                        <td>
                                            @item.BUDGET_CODE
                                        </td>
                                        <td>
                                            @item.ACCOUNT_CODE_START
                                        </td>
                                        <td>
                                           @item.ACCOUNT_CODE_END
                                        </td>
                                    </tr>
                                Next
                                    
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="widget-title">Related Department</h4>
                    </div>
                    <div class="widget-body" id="testing">
                        <table id="simple-table" class="table table-striped table-bordered table-hover">
                            <tbody>
                                <tr>
                                    <th>RelDept. Name</th>
                                    <th>Flow Number</th>
                                </tr>
                            </tbody>
                            <tbody id="dataTable">
                                @for Each item In Model.TPROC_FORM_SUBTYPE_GR.TPROC_FORM_SUBTYPE_DT
                                    @<tr>
                                        <td>@item.TPROC_REL_DEPT.RELATED_DEPARTMENT_NAME</td>
                                        <td>@item.FLOW_NUMBER</td>
                                    </tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>


        </div>
        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
        </div>
    </div>
</div>
<script src="~/Scripts/Standard/StandardModal.js"></script>


