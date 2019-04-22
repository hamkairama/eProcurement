@ModelType eProcurementApps.Models.TPROC_USER
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
                <h4 class="widget-title">Work Area Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
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
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Requestor Detail</h4>
            </div>
            <div class="widget-body" id="testing">
                <div class="widget-main">
                    <table>
                        <tr>
                            <td width="500px">
                                <div class="profile-info-row hidden">
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


                                <div class="profile-info-row hidden">
                                    <div class="profile-info-name"> Is SuperAdmin </div>
                                    <div class="profile-info-value">
                                        @If Model.TPROC_USER_DT.IS_SUPER_ADMIN = 1 Then
                                            @<input type="checkbox" id="cb_is_super_admin" checked="checked" />
                                        Else
                                            @<input type="checkbox" id="cb_is_super_admin" />
                                        End If
                                    </div>
                                </div>

                                <div class="profile-info-row hidden">
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

                            </td>
                            @*<td width="500px">
                                    <div> Work Area </div>
                                    <div class="col-sm-12">
                                        <div class="space-2"></div>
                                        <ul>
                                            @For Each item In Model.TPROC_WA_ALLOWED_GR.TPROC_WA_ALLOWED_DT
                                                @<li>
                                                    @item.TPROC_WA.WA_NUMBER : @item.TPROC_WA.DEPARTMENT_NAME
                                                </li>
                                            Next
                                        </ul>
                                    </div>
                                </td>*@
                        </tr>
                    </table>

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

                </div>
            </div>
        </div>
    </div>

    @code
    Dim usr_dt_tobe As New TPROC_USER_DT
    Dim getWA2 As New List(Of String)
    Dim wa_all2 = Dropdown.GetWorkArea()

    usr_dt_tobe = Facade.UserFacade.GetUserDtToBe(Model.ID, ListEnum.RowStat.Edit)

    If usr_dt_tobe IsNot Nothing Then
        @<div class="col-sm-12">
            <div class="widget-box">
                <div class="widget-header">
                    <h4 class="widget-title">Requestor Detail To be</h4>
                </div>
                <div class="widget-body" id="testing">
                    <div class="widget-main">
                        <table>
                            <tr>
                                <td width="500px">
                                    <div class="profile-info-row hidden">
                                        <div class="profile-info-name"> ID </div>
                                        <div class="profile-info-value">
                                            <span id="txt_id">@Model.ID</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> User Id </div>
                                        <div class="profile-info-value">
                                            <span class="" id="" maxlenght="50">@Model.USER_ID</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> User Name </div>
                                        <div class="profile-info-value">
                                            <span class="" id="">@Model.USER_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Email </div>
                                        <div class="profile-info-value ">
                                            <span class="" id="">@Model.USER_MAIL</span>
                                        </div>
                                    </div>


                                    <div class="profile-info-row hidden">
                                        <div class="profile-info-name"> Is SuperAdmin </div>
                                        <div class="profile-info-value">
                                            @If Model.TPROC_USER_DT.IS_SUPER_ADMIN = 1 Then
                                            @<input type="checkbox" id="cb_is_super_admin" checked="checked" />
                                            Else
                                            @<input type="checkbox" id="cb_is_super_admin" />
                                            End If
                                        </div>
                                    </div>

                                    <div class="profile-info-row hidden">
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
                                        <div class="profile-info-name "> Role</div>
                                        <div class="profile-info-value ">
                                            <span id="txt_role_id" class="hidden">@usr_dt_tobe.ROLE_ID</span>
                                            @*@Html.DropDownListFor(Function(x) x.TPROC_USER_DT.ROLE_ID, ViewBag.RoleUser)*@
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Comp Cd </div>
                                        <div class="profile-info-value">
                                            <div class="radio">
                                                @If usr_dt_tobe.COMP_CD = 0 Then
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
                                        <div class="profile-info-name "> Division </div>
                                        <div class="profile-info-value ">
                                            <span id="" class="hidden">@usr_dt_tobe.DIVISION_ID</span>
                                            @*@Html.DropDownListFor(Function(x) x.TPROC_USER_DT.DIVISION_ID, ViewBag.Division)*@
                                            @Html.DropDownList("dropdownList_division", Dropdown.Division(usr_dt_tobe.DIVISION_ID & ""), New With {.style = "width:  200px;"})
                                        </div>
                                    </div>

                                </td>
                                @*<td width="500px">
                                        <div> Work Area </div>
                                        <div class="col-sm-12">
                                            <div class="space-2"></div>
                                            <ul>
                                                @For Each item In Model.TPROC_WA_ALLOWED_GR.TPROC_WA_ALLOWED_DT
                                                    @<li>
                                                        @item.TPROC_WA.WA_NUMBER : @item.TPROC_WA.DEPARTMENT_NAME
                                                    </li>
                                                Next
                                            </ul>
                                        </div>
                                    </td>*@
                            </tr>
                        </table>

                        <hr />

                        <div class="required"> Work Area <label id="required_form-field-select-4"></label></div>
                        <div class="col-sm-12 item-required-select">
                            <div class="space-2"></div>
                            <select multiple="" class="chosen-select form-control" id="form-field-select-4" data-placeholder="Choose work area...">

                                @For Each item In usr_dt_tobe.TPROC_WA_ALLOWED_GR.TPROC_WA_ALLOWED_DT
                                    getWA2.Add(item.TPROC_WA.WA_NUMBER)
                                Next

                                @For Each item In wa_all2
                                    If getWA2.Contains(item.WA_NUMBER) Then
                                @<option selected="selected">@item.WA_NUMBER - @item.TPROC_APPROVAL_GR.DEPARTMENT_NAME</option>
                                    Else
                                @<option>@item.WA_NUMBER - @item.TPROC_APPROVAL_GR.DEPARTMENT_NAME</option>
                                    End If
                                Next
                            </select>
                        </div>

                        <hr />

                    </div>
                </div>
            </div>
        </div>
        End If

End code


<div Class="clearfix form-action">
    <div Class="col-lg-12">
        <div Class="modal-footer no-margin-top">
            @If ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedComplete) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                @Html.Raw(Labels.ButtonForm("ActionRequestUserComplete"))
                @*ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                    @Html.Raw(Labels.ButtonForm("ActionRequestWAApprove"))*@
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

<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/USERController.js"></script>

