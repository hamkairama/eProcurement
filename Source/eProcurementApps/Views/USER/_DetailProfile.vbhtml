@ModelType eProcurementApps.Models.TPROC_USER_DT
@Imports eProcurementApps.Helpers


<div Class="modal-dialog">
    @If Model.ID > 0 Then
        @<div Class="modal-content">
            <div Class="modal-header no-padding">
                <div Class="table-header">
                    @Html.Raw(Labels.ButtonForm("Exit"))
                    Profile
                </div>
            </div>
            <div class="col-sm-12">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="widget-title">User Application</h4>
                    </div>
                    <div class="widget-body" id="testing">
                        <div class="widget-main">
                            <table>
                                <tr>
                                    <td width="500px">
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> User Id </div>
                                            <div class="profile-info-value">
                                                <span>@Model.TPROC_USER.USER_ID</span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> User Name </div>
                                            <div class="profile-info-value">
                                                <span>@Model.TPROC_USER.USER_NAME</span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Email </div>
                                            <div class="profile-info-value">
                                                <span>@Model.TPROC_USER.USER_MAIL</span>
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Is SuperAdmin </div>
                                            <div class="profile-info-value">
                                                @If Model.TPROC_USER.IS_SUPER_ADMIN = 1 Then
                                                    @<input type="checkbox" id="cb_is_super_admin" checked="checked" />
                                                Else
                                                    @<input type="checkbox" id="cb_is_super_admin" />
                                                End If
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Is EprocAdmin </div>
                                            <div class="profile-info-value">
                                                @If Model.TPROC_USER.IS_EPROC_ADMIN = 1 Then
                                                    @<input type="checkbox" id="cb_is_eproc_admin" checked="checked" />
                                                Else
                                                    @<input type="checkbox" id="cb_is_eproc_admin" />
                                                End If
                                            </div>
                                        </div>
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Role </div>
                                            <div class="profile-info-value">
                                                <span>@Model.TPROC_ROLE.ROLE_NAME</span>
                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Comp Cd </div>
                                            <div class="profile-info-value">
                                                @If Model.TPROC_USER.COMP_CD = 0 Then
                                                    @<span>eProc</span>
                                                Else
                                                    @<span>MAMI</span>
                                                End If

                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Division </div>
                                            <div class="profile-info-value">
                                                <span>@Model.TPROC_USER.TPROC_DIVISION.DIVISION_NAME</span>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="500px">
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
                                    </td>
                                </tr>
                            </table>

                            <hr />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    Else
        @<div class="error-container">
            <div class="well">
                <h1 class="grey lighter smaller">
                    <span class="blue bigger-125">
                        <i class="ace-icon fa fa-random"></i>
                        007
                    </span>
                    You don't have the profile
                </h1>

                <hr />
                <h3 class="lighter smaller">
                    But you can request using menu request.
                </h3>

                @*<div class="space"></div>

                    <div id="input_wa">
                        <div class="required"> Work Area <label id="required_form-field-select-4"></label></div>
                        <div class="col-lg-12 item-required-select">
                            <div class="space-2"></div>
                            <select multiple="" class="chosen-select form-control" id="form-field-select-4" data-placeholder="Choose work area...">
                                @For Each item In Dropdown.WorkAreaForCreateUser()
                                @<option>@item</option>
                                Next
                            </select>
                        </div>
                    </div>
                    <hr />


                    <div id="action_send_email">
                        <ul class="list-unstyled spaced inline bigger-110 margin-15">
                            <li>
                                <i class="ace-icon fa fa-hand-o-right blue"></i>
                                <a class="red" href="#" onclick="ActionEmailCreateUser()" data-toggle="modal" title="Sent the email to Hamka Irama">
                                    Sent the email to administrator
                                </a>
                            </li>
                        </ul>
                    </div>

                    <div class="" id="loader"></div>
                    <div style="visibility:hidden" id="email_sent">
                        Email has been sent
                        <br /> For the detail information, please contact eprocurement administrator.
                    </div>
                    <hr />*@

                <div class="space"></div>
                <div class="center">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </div>

            </div>
        </div>
    End If
</div>

<Script src="~/Scripts/Standard/StandardProfile.js"></Script>
<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Controllers/USERController.js"></script>

