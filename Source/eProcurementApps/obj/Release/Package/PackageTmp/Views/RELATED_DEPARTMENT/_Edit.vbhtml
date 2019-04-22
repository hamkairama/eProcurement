@ModelType eProcurementApps.Models.TPROC_REL_DEPT
@Imports eProcurementApps.Helpers

@Code
    @if ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Edit Related Department"
        ViewBag.Setup = "active open"
        ViewBag.IndexRelatedDepartment = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Edit RD"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestRD = "active"
    End If
End Code

<div class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Approval</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <span id="txt_action" class="hidden">Edit</span>
            </div>
            <div class="widget-body" id="testing">
                <table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row hidden">
                                    <div class="profile-info-name "> ID  </div>
                                    <div class="profile-info-value ">
                                        <span id="txt_id" >@Model.ID</span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> RelDept.  </div>
                                    <div class="profile-info-value item-required">
                                        <span Class="" id="txt_related_department_name" maxlenght="50">@Model.RELATED_DEPARTMENT_NAME</span>
                                        <label id="required_txt_related_department_name"></label>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>


                <table id="simple-table" Class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th>User Id</th>
                            <th>User Name</th>
                            <th>Email</th>
                            <th>Limit Approval</th>
                            <th>Action</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTable">
                        @code
                            Dim no As String = ""
                            Dim flag As Integer = 0

                        @For Each item In Model.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT
                        @<tr id="0">
                            <td>
                                <input size=10 type="text" id="txt_user_id2_@no" value="@item.APPROVAL_NAME"  />
                            </td>
                            <td>
                                <input size=20 type="text" id="txt_user_name2_@no" value="@item.USER_NAME" />
                            </td>
                            <td>
                                <input size=30 type="text" id="txt_user_email2_@no" value="@item.EMAIL" />
                                <a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>
                            </td>
                            <td>
                                @Html.DropDownList("dropdownList", Dropdown.LevelApproval(item.LEVEL_ID & ""), New With {.style = "width:  200px;"})
                            </td>
                            <td>
                                <div>
                                    @Html.Raw(Labels.ButtonForm("Remove"))
                                </div>
                            </td>
                        </tr>
                            flag += 1
                            no = flag
                        Next
                        End code
                    </tbody>
                </table>

                @Html.Raw(Labels.ButtonForm("AddRowApprovalRelDept"))

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

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                <a class="red" href="@Url.Action("Index", "RELATED_DEPARTMENT", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>

                @if ViewBag.flag = 0 Then
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                Else
                    @Html.Raw(Labels.ButtonForm("SendRequestRDEdit"))
                End If

            </div>
        </div>
    </div>



</div>

<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Controllers/RELATED_DEPARTMENTController.js"></script>
