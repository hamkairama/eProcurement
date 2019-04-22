@ModelType eProcurementApps.Models.TPROC_FORM_TYPE
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Create Form Sub Type"
        ViewBag.Setup = "active open"
        ViewBag.IndexFormSubType = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Create FST"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestFST = "active"
    End If
End Code

<div class="alert alert-danger" id="msg_error" style="display:none">
    <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
        <i class="ace-icon fa fa-times"></i>
    </button>
    Error :
    <br />
    msg error
    <br />
</div>


@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>
End If

<div class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Form Sub Type Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <span id="txt_action" class="hidden">Create</span>
            </div>
            <div class="widget-body" id="testing">
                <div class="profile-user-info">

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> F. Type </div>
                        <div class="profile-info-value item-required">
                            <span id="txt_form_type_id" class="hidden"></span>
                            @Html.DropDownList("dropdownList", Dropdown.FormType)
                            <label id="required_txt_form_type_id"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> F. SubType </div>
                        <div class="profile-info-value item-required">
                            <span Class="freeText" id="txt_form_sub_type_name" maxlenght="50"></span>
                            <label id="required_txt_form_sub_type_name"></label>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name"> Description </div>
                        <div Class="profile-info-value">
                            <span Class="freeText" id="txt_form_sub_type_description"></span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> SLA </div>
                        <div class="profile-info-value item-required">
                            <span Class="freeText onlyNumber" id="txt_sla"></span>
                            <label id="required_txt_sla"></label>
                            <label id="only_number_txt_sla"></label>
                        </div>
                    </div>                   
                </div>

                <div>
                    <div class="profile-info-row">
                        <div class="profile-info-name"> Budget Code </div>
                        <div class="profile-info-value">
                            <span Class="hidden" id="txt_budget_code"></span>
                            @Html.DropDownList("dropdownList_bc", Dropdown.AccountCode, New With {.style = "width:  200px; "})
                        </div>

                        <div class="profile-info-name"> Account Code Start </div>
                        <div class="profile-info-value">
                            <span Class="hidden" id="txt_account_code_start"></span>
                            @Html.DropDownList("dropdownList_acs", Dropdown.AccountCode, New With {.style = "width:  200px; "})
                        </div>

                        <div class="profile-info-name"> Account Code End </div>
                        <div class="profile-info-value">
                            <span Class="hidden" id="txt_account_code_end"></span>
                            @Html.DropDownList("dropdownList_ace", Dropdown.AccountCode, New With {.style = "width:  200px; "})
                        </div>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Account Code Add </div>
                    <div class="profile-info-value">
                        <input type="checkbox" class="mandatoryChecked" id="cb_popup_account" onclick="PopChecked()" />
                        <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Adding other budget code">?</span>
                        <label id="mandatory_checked_cb_popup_account"></label>
                    </div>
                </div>

            </div>
        </div>
    </div>
    
    <div style="display:none" id="budget_area">
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
                                <th>Action</th>
                            </tr>
                        </tbody>
                        <tbody id="dataTableBc">
                            <tr id="0">
                                <td>
                                    @Html.DropDownList("dropdownList_bc_add", Dropdown.AccountCode, New With {.style = "width:  200px; "})
                                </td>
                                <td>
                                    @Html.DropDownList("dropdownList_acs_add", Dropdown.AccountCode, New With {.style = "width:  200px; "})
                                </td>
                                <td>
                                    @Html.DropDownList("dropdownList_ace_add", Dropdown.AccountCode, New With {.style = "width:  200px; "})
                                </td>
                                <td>
                                    <div>
                                        @Html.Raw(Labels.ButtonForm("RemoveRowFstBudget"))
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    @Html.Raw(Labels.ButtonForm("AddRowFstBudgetAdditional"))
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
                                <th>Action</th>
                            </tr>
                        </tbody>
                        <tbody id="dataTable">
                            <tr id="0">
                                <td>@Html.DropDownList("dropdownList", Dropdown.RelatedDepartment)</td>
                                <td><input size=10 type="text" id="txt_flow_" /></td>
                                <td>
                                    <div>
                                        @Html.Raw(Labels.ButtonForm("Remove"))
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    @Html.Raw(Labels.ButtonForm("AddRowRelDept"))

                </div>
            </div>
        </div>

        @If ViewBag.flag = 1 Then
        @<div Class="col-sm-12">
            <div Class="widget-box">
                <div Class="widget-header">
                    <h4 Class="widget-title">Description And approval</h4>
                </div>
                <div Class="widget-body" id="testing">
                    <label id="required_txt_approval"></label>
                    <Table id="simple-table" Class="table table-striped table-bordered table-hover">
                        <tbody>
                            <tr>
                                <th> Approval User Id</th>
                                <th> Approval Name</th>
                                <th> Approval Email</th>
                            </tr>
                        </tbody>
                        <tbody id="dataTable">
                            <tr>
                                <td>
                                    <input size=10 type="text" id="txt_user_id2" value="" readonly="readonly" />
                                </td>
                                <td>
                                    <input size=20 type="text" id="txt_user_name" value="" readonly="readonly" />
                                </td>
                                <td>
                                    <input size=30 type="text" id="txt_mail" value="" readonly="readonly" />
                                    <a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                        @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </Table>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Description </div>
                        <div class="profile-info-value item-required">
                            <input type="text" id="txt_desc" style="width:500px" maxlenght="200" />
                            <label id="required_txt_desc"></label>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        End If

        <div class="clearfix form-action">
            <div class="col-lg-12">
                <div class="modal-footer no-margin-top">
                    <a class="red" href="@Url.Action("Index", "FORM_SUB_TYPE", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                    @If ViewBag.flag = 0 Then
                @Html.Raw(Labels.ButtonForm("SaveCreate"))
                    Else
                @Html.Raw(Labels.ButtonForm("SendRequestFSTCreate"))
                    End If
                </div>
            </div>
        </div>

    </div>

<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/FORM_SUB_TYPEController.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>

