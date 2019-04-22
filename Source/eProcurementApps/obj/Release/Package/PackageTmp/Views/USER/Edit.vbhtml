@ModelType eProcurementApps.Models.TPROC_USER
@Imports eProcurementApps.Helpers
@imports eProcurementApps.Models

@Code
    If ViewBag.Flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Edit User"
        ViewBag.Setup = "active open"
        ViewBag.IndexUser = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Edit User"
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

<div class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">User Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <span id="txt_action" class="hidden">Edit</span>
            </div>
            <div class="widget-body" id="testing">
                <div class="widget-main">
                    <div class="profile-info-row  hidden">
                        <div class="profile-info-name"> ID </div>
                        <div class="profile-info-value">
                            <span id="txt_id">@Model.ID</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> User Id </div>
                        <div class="profile-info-value item-required">
                            <span class="" id="txt_user_id2" maxlenght="50">@Model.USER_ID</span>
                            <label id="required_txt_user_id2"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> User Name </div>
                        <div class="profile-info-value item-required">
                            <span class="" id="txt_user_name">@Model.USER_NAME</span>
                            <label id="required_txt_user_name"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Email </div>
                        <div class="profile-info-value item-required">
                            <span class="" id="txt_mail">@Model.USER_MAIL</span>
                            <label id="required_txt_mail"></label>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name"> Is SuperAdmin </div>
                        <div Class="profile-info-value">
                            @If Model.TPROC_USER_DT.IS_SUPER_ADMIN = 1 Then
                                @<input type="checkbox" id="cb_is_super_admin" checked="checked" />
                            Else
                                @<input type="checkbox" id="cb_is_super_admin" />
                            End If
                        </div>
                    </div>

                      <div class="profile-info-row">
                        <div class="profile-info-name"> Is EprocAdmin </div>
                        <div class="profile-info-value">
                            @If Model.TPROC_USER_DT.IS_EPROC_ADMIN = 1 Then
                                @<input type="checkbox" id="cb_is_eproc_admin" checked="checked" />
                            Else
                                @<input type="checkbox" id="cb_is_eproc_admin" />
                            End If
                        </div>
                    </div>
              

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Role</div>
                        <div class="profile-info-value item-required">
                            <span id="txt_role_id" class="hidden">@Model.TPROC_USER_DT.ROLE_ID</span>
                            @Html.DropDownListFor(Function(x) x.TPROC_USER_DT.ROLE_ID, ViewBag.RoleUser)
                            <label id="required_txt_role_id"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Comp Cd </div>
                        <div class="profile-info-value">
                            <div class="radio">
                                @If Model.TPROC_USER_DT.COMP_CD = 0 Then
                                    @<label>
                                        <input name="form-field-radio" type="radio" Class="ace" checked="checked" id="rb_eproc" />
                                        <span Class="lbl">eProc</span>
                                    </label>
                                    @<Label>
                                        <input name="form-field-radio" type="radio" Class="ace" id="rb_mami" />
                                        <span Class="lbl">MAMI</span>
                                    </Label>
                                Else
                                    @<label>
                                        <input name="form-field-radio" type="radio" Class="ace" id="rb_eproc" />
                                        <span Class="lbl">eProc</span>
                                    </label>
                                    @<Label>
                                        <input name="form-field-radio" type="radio" Class="ace" checked="checked" id="rb_mami" />
                                        <span Class="lbl">MAMI</span>
                                    </Label>
                                End If

                            </div>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Division </div>
                        <div class="profile-info-value item-required">
                            <span id="txt_division_id" class="hidden">@Model.TPROC_USER_DT.DIVISION_ID</span>
                            @Html.DropDownListFor(Function(x) x.TPROC_USER_DT.DIVISION_ID, ViewBag.Division)
                            <label id="required_txt_division_id"></label>
                        </div>
                    </div>

                    <hr />

                    <div class="required"> Work Area <label id="required_form-field-select-4"></label></div>
                    <div class="col-sm-12 item-required-select">
                        <div class="space-2"></div>
                        <select multiple="" class="chosen-select form-control" id="form-field-select-4" data-placeholder="Choose work area...">
                            @code

                                Dim getWA As New List(Of String)
                                @For Each item In Model.TPROC_USER_DT.TPROC_WA_ALLOWED_GR.TPROC_WA_ALLOWED_DT
                                    getWA.Add(item.TPROC_WA.WA_NUMBER)
                                Next

                                Dim wa_all = Dropdown.GetWorkArea()
                                @For Each item In wa_all
                                    If getWA.Contains(item.WA_NUMBER) Then
                                        @<option selected="selected">@item.WA_NUMBER - @item.TPROC_APPROVAL_GR.DEPARTMENT_NAME</option>
                                    Else
                                        @<option>@item.WA_NUMBER - @item.TPROC_APPROVAL_GR.DEPARTMENT_NAME</option>
                                    End If
                                Next
                            End Code

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
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                    @<a Class="red" href="@Url.Action("Index", "User", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                Else
                    @Html.Raw(Labels.ButtonForm("SendRequestUserEdit"))
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

