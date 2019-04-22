@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_WA)
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.Flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Create User"
        ViewBag.Setup = "active open"
        ViewBag.IndexUser = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Create User"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestUser = "active"
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

@If ViewBag.flag = 1 Then
    @<h4>only already registered can create purchase request.</h4>
    @<h6>If you have not registered yet, then fill in your data. Then the data will be sent to admin eproc to be registered</h6>
End If

<div Class="row">
    <div Class="col-sm-12">
        <div Class="widget-box">
            <div Class="widget-header">
                <h4 Class="widget-title">User Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <span id="txt_action" class="hidden">Create</span>
            </div>
            <div class="widget-body" id="testing">
                <div class="widget-main">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> User Id </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText" id="txt_user_id2" maxlenght="50"></span>
                            <label id="required_txt_user_id2"></label>
                        </div>
                        <a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/flag', '.dialogForm')" data-toggle="modal" title="Active Directory">
                            @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                        </a>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> User Name </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText" id="txt_user_name"></span>
                            <label id="required_txt_user_name"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Email </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText" id="txt_mail"></span>
                            <label id="required_txt_mail"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Password </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText password" id="txt_password"></span>
                            <label id="required_txt_password"></label>
                        </div>
                    </div>

                    @if ViewBag.flag = 0 Then
                        @<div Class="profile-info-row">
                            <div Class="profile-info-name"> Is SuperAdmin </div>
                            <div Class="profile-info-value">
                                <input type="checkbox" id="cb_is_super_admin" />
                            </div>
                        </div>

                        @<div Class="profile-info-row">
                            <div Class="profile-info-name"> Is EprocAdmin </div>
                            <div Class="profile-info-value">
                                <input type="checkbox" id="cb_is_eproc_admin" />
                            </div>
                        </div>
                    End If

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> Role </div>
                        <div Class="profile-info-value item-required">
                            @If ViewBag.flag = 0 Then
                                @<span id="txt_role_id" Class="hidden"></span>
                                @Html.DropDownList("dropdownList_role", Dropdown.Role)
                            Else
                                @<span id="txt_role_id" Class="hidden">@CommonFunction.GetDefaultSelectedRole()</span>
                                @Html.DropDownList("dropdownList_role", Dropdown.RoleDefaultSelected)
                            End If
                            <label id="required_txt_role_id"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Comp Cd </div>
                        <div class="profile-info-value">
                            <div class="radio">
                                <label>
                                    <input name="form-field-radio" type="radio" class="ace" id="rb_eproc" />
                                    <span class="lbl">eProc</span>
                                </label>
                                <label>
                                    <input name="form-field-radio" type="radio" class="ace" id="rb_mami" />
                                    <span class="lbl">MAMI</span>
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Division </div>
                        <div class="profile-info-value item-required">
                            <span id="txt_division_id" class="hidden"></span>
                            @Html.DropDownList("dropdownList_div", Dropdown.Division)
                            <label id="required_txt_division_id"></label>
                        </div>
                    </div>

                    <hr />

                    <div class="required"> Work Area <label id="required_form-field-select-4"></label></div>
                    <div class="col-sm-12 item-required-select">
                        <div class="space-2"></div>
                        <select multiple="" class="chosen-select form-control" id="form-field-select-4" data-placeholder="Choose work area...">
                            @For Each item In Model
                                @<option>@item.WA_NUMBER - @item.TPROC_APPROVAL_GR.DEPARTMENT_NAME</option>
                            Next
                        </select>
                    </div>

                    <hr />

                    @If ViewBag.flag = 1 Then
                        @<div class="profile-info-row">
                            <div class="profile-info-name required"> Description </div>
                            <div class="profile-info-value item-required">
                                <input type="text" id="txt_desc" style="width:500px" maxlenght="200" />
                                <label id="required_txt_desc"></label>
                            </div>
                        </div>
                    End If

                </div>
            </div>
        </div>
    </div>

    <div Class="clearfix form-action">
        <div Class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                @If ViewBag.flag = 0 Then
                    @<a Class="red" href="@Url.Action("Index", "User", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                    @Html.Raw(Labels.ButtonForm("SaveCreate"))
                Else
                    @Html.Raw(Labels.ButtonForm("SendRequestUserCreate"))
                    @<a Class="red" href="@Url.Action("IndexRequestUser", "User")" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                End If
            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/USERController.js"></script>

