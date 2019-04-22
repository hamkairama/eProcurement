@ModelType eProcurementApps.Models.TPROC_FORM_SUB_TYPE
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Delete Form Sub Type"
        ViewBag.Setup = "active open"
        ViewBag.IndexFormSubType = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Delete FST"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestFST = "active"
    End If

End Code

@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>
End If

@*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
<div class="modal-body no-padding">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Form Sub Type Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <span id="txt_action" class="hidden">Delete</span>
            </div>
            <div class="widget-body" id="testing">
                <div class="profile-user-info">

                    <div class="profile-info-row  hidden">
                        <div class="profile-info-name "> iD </div>
                        <div class="profile-info-value ">
                            <span id="txt_id">@Model.ID</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> F. Type </div>
                        <div class="profile-info-value ">
                            <span>@Model.TPROC_FORM_SUBTYPE_GR.TPROC_FORM_TYPE.FORM_TYPE_NAME </span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> F. SubType </div>
                        <div class="profile-info-value">
                            <span id="txt_form_sub_type_name">@Model.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME</span>
                            <label id="required_txt_form_sub_type_name"></label>
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

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Account Code </div>
                        <div class="profile-info-value">
                            @if Model.TPROC_FORM_SUBTYPE_GR.POPUP_ACCOUNT = 1 Then
                            @<input type="checkbox" id="cb_popup_account" checked="checked" />
                            Else
                            @<input type="checkbox" id="cb_popup_account" />
                            End If

                            <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Mandatory. Check will be displayed the account code when create PR">?</span>

                        </div>
                    </div>

                </div>

                <div style="" id="budget_area">
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

<div class='modal-footer no-margin-top'>
    @If ViewBag.flag = 0 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
    ElseIf ViewBag.flag = 1 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("SendRequestFSTDelete"))
    End If
    
    @If Model.ROW_STATUS = ListEnum.RowStat.Delete Then
        @<a Class="red" href="@Url.Action("Index", "FORM_SUB_TYPE", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    Else
        @<a Class="red" href="@Url.Action("Index", "FORM_SUB_TYPE", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
            @Html.Raw(Labels.ButtonForm("No"))
        </a>
    End If
</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Controllers/FORM_SUB_TYPEController.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>

