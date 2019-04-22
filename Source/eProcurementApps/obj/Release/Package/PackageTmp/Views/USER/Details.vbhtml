@ModelType eProcurementApps.Models.TPROC_USER
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Details User"
    ViewBag.Setup = "active open"
    ViewBag.IndexUser = "active"
End Code


<div class="row">
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
                                        <span>@Model.USER_ID</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> User Name </div>
                                    <div class="profile-info-value">
                                        <span>@Model.USER_NAME</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Email </div>
                                    <div class="profile-info-value">
                                        <span>@Model.USER_MAIL</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Is SuperAdmin </div>
                                    <div class="profile-info-value">
                                        @If Model.TPROC_USER_DT.IS_SUPER_ADMIN = 1 Then
                                           @<span>Yes</span>
                                        Else
                                            @<span>No</span>
                                        End If
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Is EprocAdmin </div>
                                    <div class="profile-info-value">
                                        @If Model.TPROC_USER_DT.IS_EPROC_ADMIN = 1 Then
                                            @<span>Yes</span>
                                        Else
                                             @<span>No</span>
                                        End If
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Role </div>
                                    <div class="profile-info-value">
                                        <span>@Model.TPROC_USER_DT.TPROC_ROLE.ROLE_NAME</span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Comp Cd </div>
                                    <div class="profile-info-value">
                                        @If Model.TPROC_USER_DT.COMP_CD = 0 Then
                                            @<span>eProc</span>
                                        Else
                                            @<span>MAMI</span>
                                        End If
                                        
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Division </div>
                                    <div class="profile-info-value">
                                        <span>@Model.TPROC_USER_DT.TPROC_DIVISION.DIVISION_NAME</span>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div> Work Area </div>
                                <div class="col-sm-12">
                                    <div class="space-2"></div>
                                    <ul>
                                        @For Each item In Model.TPROC_USER_DT.TPROC_WA_ALLOWED_GR.TPROC_WA_ALLOWED_DT
                                            @<li>
                                                @item.TPROC_WA.WA_NUMBER : @item.TPROC_WA.TPROC_APPROVAL_GR.DEPARTMENT_NAME
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

    <div Class="clearfix form-action">
        <div Class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                <a Class="red" href="@Url.Action("Index", "User", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Create">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            </div>
        </div>
    </div>

</div>

<Script src="~/Scripts/Standard/StandardProfile.js"></Script>

