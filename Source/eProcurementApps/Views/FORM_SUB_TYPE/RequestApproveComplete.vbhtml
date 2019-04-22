@ModelType eProcurementApps.Models.TPROC_FORM_SUB_TYPE
@Imports eProcurementApps.Helpers
@imports eProcurementApps.Models

@Code
    ViewBag.Breadcrumbs = "Request"
    ViewBag.Request = "active open"
    ViewBag.ListRequest = "active open"

    If ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.Submitted) Then
        ViewBag.IndexListOutstanding = "active"
        ViewBag.Title = "Request Outstanding"
    ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) Then
        ViewBag.IndexListNeedApprove = "active"
        ViewBag.Title = "Request Need Approve"
    ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedComplete) Then
        ViewBag.IndexListNeedComplete = "active"
        ViewBag.Title = "Request Need Completed"
    ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.Completed) Then
        ViewBag.IndexListAlreadyCompleted = "active"
        ViewBag.Title = "Request Already Completed"
    End If
End Code

<style>
    #BUDGET_CODE {
        width: 200px;
    }

    #ACCOUNT_CODE_START {
        width: 200px;
    }

    #ACCOUNT_CODE_END {
        width: 200px;
    }
</style>

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
                <h4 class="widget-title">Requestor Detail</h4>@*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
            </div>
            <div class="widget-body" id="">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name"> Request No. </div>
                        <div class="profile-info-value">
                            <span id="txt_request_no">@ViewBag.ReqNo</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Request By </div>
                        <div class="profile-info-value ">
                            <span Class="" id="">@ViewBag.RequestBy</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Request Date </div>
                        <div class="profile-info-value">
                            <span Class="" id="" maxlenght="10">@ViewBag.RequestDt</span>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name "> Subject </div>
                        <div Class="profile-info-value ">
                            <span Class="" id="txt_actions">@ViewBag.RequestAction</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Description </div>
                        <div class="profile-info-value ">
                            <span id="" class="">@ViewBag.RequestDesc</span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    
    <div class="col-sm-12">
        <div class="col-sm-6">
            <div class="widget-box">
                <div class="widget-header">
                    <h4 class="widget-title">Form Sub Type Application</h4>
                </div>
                <div class="widget-body" id="testing">
                    <div class="profile-user-info">

                        <div class="profile-info-row" hidden="">
                            <div class="profile-info-name" id="dataID"> ID </div>
                            <div class="profile-info-value">
                                <span id="txt_id">@Model.ID.ToString("0")</span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name required"> F. Type </div>
                            <div class="profile-info-value item-required">
                                <span id="" class="">@Model.TPROC_FORM_SUBTYPE_GR.TPROC_FORM_TYPE.FORM_TYPE_NAME</span>
                                <span id="txt_form_type_id" class="hidden">@Model.TPROC_FORM_SUBTYPE_GR.FORM_TYPE_ID</span>
                                @*@Html.DropDownListFor(Function(x) x.FORM_TYPE_ID, ViewBag.FormType)*@
                                <label id="required_txt_form_type_id"></label>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name required"> F. SubType </div>
                            <div class="profile-info-value item-required">
                                <span class="freeText change" id="txt_form_sub_type_name" maxlenght="50">@Model.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME</span>
                                <label id="required_txt_form_sub_type_name"></label>
                            </div>
                        </div>

                        <div Class="profile-info-row">
                            <div Class="profile-info-name"> Description </div>
                            <div Class="profile-info-value">
                                <span class="freeText" id="txt_form_sub_type_description">@Model.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_DESCRIPTION</span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name required"> SLA </div>
                            <div class="profile-info-value item-required">
                                <span class="freeText onlyNumber" id="txt_sla">@Model.TPROC_FORM_SUBTYPE_GR.SLA</span>
                                <label id="required_txt_sla"></label>
                                <label id="only_number_txt_sla"></label>
                            </div>
                        </div>                      

                    </div>

                    <div style="" id="budget_area">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Budget Code </div>
                            <div class="profile-info-value">
                                <span Class="hidden" id="txt_budget_code">@Model.TPROC_FORM_SUBTYPE_GR.BUDGET_CODE</span>
                                @Html.DropDownListFor(Function(x) x.TPROC_FORM_SUBTYPE_GR.BUDGET_CODE, ViewBag.AccountCode)
                            </div>
                        </div>
                    </div>

                    <div style="" id="budget_area">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Account Code Start </div>
                            <div class="profile-info-value">
                                <span Class="hidden" id="txt_account_code_start">@Model.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_START</span>
                                @Html.DropDownListFor(Function(x) x.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_START, ViewBag.AccountCode)
                            </div>
                        </div>
                    </div>

                    <div style="" id="budget_area">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Account Code End </div>
                            <div class="profile-info-value">
                                <span Class="hidden" id="txt_account_code_end">@Model.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_END</span>
                                @Html.DropDownListFor(Function(x) x.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_END, ViewBag.AccountCode)
                            </div>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Account Code</div>
                        <div class="profile-info-value">
                            @if Model.TPROC_FORM_SUBTYPE_GR.POPUP_ACCOUNT = 1 Then
                                @<input type="checkbox" class="mandatoryChecked" id="cb_popup_account" onclick="PopChecked()" checked="checked" />
                            Else
                                @<input type="checkbox" class="mandatoryChecked" id="cb_popup_account" onclick="PopChecked()" />
                            End If
                            <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Mandatory. Check will be displayed the account code when create PR">?</span>
                            <label id="mandatory_checked_cb_popup_account"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>            

        @If ViewBag.RequestAction = ListEnum.RowStat.Edit.ToString() Then
            @<div class="col-sm-6">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="widget-title">Form Sub Type Application To Be</h4>
                    </div>
                    <div class="widget-body" id="testing">
                        <div class="profile-user-info">

                            <div class="profile-info-row" hidden="">
                                <div class="profile-info-name" id="dataID"> ID </div>
                                <div class="profile-info-value">
                                    <span id="txt_id2">@Model.ID.ToString("0")</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> F. Type </div>
                                <div class="profile-info-value item-required">
                                    <span id="" class="">@ViewBag.FormTypeName</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> F. SubType </div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText change" id="txt_form_sub_type_name2" maxlenght="50">@ViewBag.FormSubTypeName</span>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Description </div>
                                <div Class="profile-info-value">
                                    <span class="freeText" id="txt_form_sub_type_description2">@ViewBag.Desc</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> SLA </div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText onlyNumber" id="txt_sla2">@ViewBag.Sla</span>
                                    <label id="required_txt_sla2"></label>
                                    <label id="only_number_txt_sla2"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> Account Code</div>
                                <div class="profile-info-value">
                                    @if ViewBag.PopUp = 1 Then
                                        @<input type="checkbox" class="mandatoryChecked" id="cb_popup_account2" onclick="PopChecked()" checked="checked" />
                                    Else
                                        @<input type="checkbox" class="mandatoryChecked" id="cb_popup_account2" onclick="PopChecked()" />
                                    End If
                                    <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Mandatory. Check will be displayed the account code when create PR">?</span>
                                    <label id="mandatory_checked_cb_popup_account2"></label>
                                </div>
                            </div>

                        </div>

                        <div style="" id="budget_area">
                            <div class="profile-info-row">
                                <div class="profile-info-name"> Budget Code </div>
                                <div class="profile-info-value">
                                    <span Class="" id="txt_budget_code2">@ViewBag.BudgetCode</span>
                                </div>
                            </div>
                        </div>

                        <div style="" id="budget_area">
                            <div class="profile-info-row">
                                <div class="profile-info-name"> Account Code Start </div>
                                <div class="profile-info-value">
                                    <span Class="" id="txt_account_code_start2">@ViewBag.BudgetCodeStart</span>
                                </div>
                            </div>
                        </div>

                        <div style="" id="budget_area">
                            <div class="profile-info-row">
                                <div class="profile-info-name"> Account Code End </div>
                                <div class="profile-info-value">
                                    <span Class="" id="txt_account_code_end2">@ViewBag.BudgetCodeEnd</span>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        End If
    </div>
    
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Additional Account Code</h4>
            </div>
            <div class="widget-body" id="">
                <table id="simple-table" class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th>Budget Code</th>
                            <th>Account Code Start</th>
                            <th>Account Code End</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTableBc">
                        @For Each item In CommonFunction.GetAdditionalBc(Model.ID, ListEnum.RowStat.Edit)
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

    <div class="col-sm-6">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Related Department</h4>
            </div>
            <div class="widget-body" id="testing">

                <table id="simple-table" class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th class="hidden">RelDept. ID</th>
                            <th>Flow Number</th>
                            <th>RelDept. Name</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTable">
                        @For Each item In Model.TPROC_FORM_SUBTYPE_GR.TPROC_FORM_SUBTYPE_DT
                        @<tr id="0">
                            <td class="hidden"><input size=10 type="text" id="txt_flow_" value="@item.REL_DEPT_ID" /></td>
                            <td><input size=10 type="text" id="txt_flow_" value="@item.FLOW_NUMBER" readonly="readonly" /></td>
                            <td><input size=20 type="text" id="txt_flow_" value="@item.TPROC_REL_DEPT.RELATED_DEPARTMENT_NAME" readonly="readonly" /></td>
                        </tr>
                        Next
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    @code
        Dim fst_dt As New List(Of TPROC_FORM_SUBTYPE_DT)
        fst_dt = Facade.FormSubTypeFacade.GetFstDtToBe(Model.ID, ListEnum.RowStat.Edit)

        If fst_dt.Count > 0 Then
            @<div class="col-sm-6">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="widget-title">Related Department To be</h4>
                    </div>
                    <div class="widget-body" id="testing">

                        <table id="simple-table" class="table table-striped table-bordered table-hover">
                            <tbody>
                                <tr>
                                    <th class="hidden">RelDept. ID</th>
                                    <th>Flow Number</th>
                                    <th>RelDept. Name</th>
                                </tr>
                            </tbody>
                            <tbody id="dataTable">
                                @For Each item2 In fst_dt
                                @<tr id="0">
                                    <td class="hidden"><input size=10 type="text" id="txt_flow_" value="@item2.REL_DEPT_ID" /></td>
                                    <td><input size=10 type="text" id="txt_flow_" value="@item2.FLOW_NUMBER" readonly="readonly" /></td>
                                    @*<td><input size=20 type="text" id="txt_flow_" value="@item2.TPROC_REL_DEPT.RELATED_DEPARTMENT_NAME" /></td>*@
                                    <td><input size=20 type="text" id="" value="@CommonFunction.GetRelDeptNameById(item2.REL_DEPT_ID)" readonly="readonly" /></td>
                                </tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        End If
    End code

    <div Class="clearfix form-action">
        <div Class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                @If ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedComplete) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                @Html.Raw(Labels.ButtonForm("ActionRequestFSTComplete"))
                ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                @Html.Raw(Labels.ButtonForm("ActionRequestFSTApprove"))
                End If

                @If ViewBag.access_from = "0" Then '0 from menu request
                    @<a Class="red" href="@Url.Action("IndexRequestList", "REQUEST", New With {.status = Convert.ToDecimal(ViewBag.data_flag)})" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                Else
                    @<a Class="red" href="@Url.Action("IndexJobLists", "DASHBOARD")" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                End If  

            </div>
        </div>
    </div>

</div>

<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/FORM_SUB_TYPEController.js"></script>


<script>
    //edit
    $(function () {
        $('#FORM_TYPE_ID').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_form_type_id').text(optionSelected);
        });
    });

    $(function () {
        $('#BUDGET_CODE').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_budget_code').text(optionSelected);
        });
    });

    $(function () {
        $('#ACCOUNT_CODE_START').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_account_code_start').text(optionSelected);
        });
    });

    $(function () {
        $('#ACCOUNT_CODE_END').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_account_code_end').text(optionSelected);
        });
    });

</script>

