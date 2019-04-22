@ModelType eProcurementApps.Models.TPROC_WA
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Delete Work Area"
        ViewBag.Setup = "active open"
        ViewBag.IndexWA = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Delete WA"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestWA = "active"
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
<span id="txt_action" class="hidden">Delete</span>
<div class="modal-body no-padding">
    <div class="profile-user-info">
        <div class="profile-info-row hidden">
            <div class="profile-info-name"> ID </div>
            <div class="profile-info-value">
                <span id="txt_id">@Model.ID.ToString("0")</span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> WA Number </div>
            <div class="profile-info-value">
                <span Class="" id="txt_wa_number" >@Model.WA_NUMBER</span>
                <label id="required_txt_wa_number"></label>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Department </div>
            <div class="profile-info-value">
                <span>@Model.TPROC_APPROVAL_GR.DEPARTMENT_NAME</span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Division </div>
            <div class="profile-info-value">
                <span>@Model.TPROC_APPROVAL_GR.TPROC_DIVISION.DIVISION_NAME</span>
            </div>
        </div>

        <div Class="profile-info-row">
            <div Class="profile-info-name"> Created Time </div>
            <div Class="profile-info-value">
                <span>@Model.CREATED_TIME.ToString("dd-MM-yyy HH:mm") </span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Created By </div>
            <div class="profile-info-value">
                <span> @Model.CREATED_BY </span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Last Modified Time </div>
            <div class="profile-info-value">
                @If Model.LAST_MODIFIED_TIME.HasValue Then  @<span>@Model.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")</span> End If
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Last Modified By </div>
            <div class="profile-info-value">
                <span>  @Model.LAST_MODIFIED_BY </span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Row Status </div>
            <div class="profile-info-value">
                <span> @Model.ROW_STATUS.ToString("0") </span>
            </div>
        </div>

    </div>
</div>

<div class="col-sm-12">
    <div class="widget-box">
        <div class="widget-header">
            <h4 class="widget-title">Approval</h4>
        </div>
        <div class="widget-body" id="testing">
            <table id="simple-table" class="table table-striped table-bordered table-hover">
                <tbody>
                    <tr>
                        <th>User Id</th>
                        <th>User Name</th>
                        <th>Email</th>
                        <th>Limit Approval</th>
                        <th>Flow</th>
                        <th>Action</th>
                    </tr>
                </tbody>
                <tbody id="dataTable">
                    @code
                        Dim no As String = ""
                        Dim flag As Integer = 0
                        @For Each item In Model.TPROC_APPROVAL_GR.TPROC_APPROVAL_DT
                            @<tr id="0">
                                <td>
                                    <input size=10 type="text" id="txt_user_id2_@no" value="@item.APPROVAL_NAME" readonly="readonly" />
                                </td>
                                <td>
                                    <input size=15 type="text" id="txt_user_name2_@no" value="@item.USER_NAME" readonly="readonly" />
                                </td>
                                <td>
                                    <input size=30 type="text" id="txt_user_email2_@no" value="@item.EMAIL" readonly="readonly" />
                                    <a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                        @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                    </a>
                                </td>
                                <td>
                                    @*@Html.DropDownList("dropdownList", Dropdown.Level)*@
                                    @Html.DropDownList("dropdownList", Dropdown.LevelApproval(item.LEVEL_ID & ""), New With {.style = "width:  200px;"})
                                    @*@item.TPROC_LEVEL.INDONESIAN_LEVEL*@
                                </td>
                                <td><input size=5 type="text" id="txt_flow_@no" value="@item.FLOW_NUMBER" /></td>
                                <td>
                                    @*<div>
                                        @Html.Raw(Labels.ButtonForm("Remove"))
                                    </div>*@
                                </td>
                            </tr>
                            flag += 1
                            no = flag
                        Next
                    End code

                </tbody>
            </table>
            @*@Html.Raw(Labels.ButtonForm("AddRowApprovalWa"))*@

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





<div class='modal-footer no-margin-top'>
    @If ViewBag.flag = 0 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
    ElseIf ViewBag.flag = 1 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("SendRequestWADelete"))
    End If


    @If Model.ROW_STATUS = ListEnum.RowStat.Delete Then
        @<a Class="red" href="@Url.Action("Index", "WA", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    Else
        @<a Class="red" href="@Url.Action("Index", "WA", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
            @Html.Raw(Labels.ButtonForm("No"))
        </a>
    End If
</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Controllers/WAController.js"></script>


