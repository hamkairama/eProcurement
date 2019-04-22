@ModelType eProcurementApps.Models.TPROC_REL_DEPT
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Delete Related Department"
        ViewBag.Setup = "active open"
        ViewBag.IndexRD = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Delete RD"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestRD = "active"
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
                <span id="txt_id" class="">@Model.ID.ToString("0")</span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> RelDept. Name </div>
            <div class="profile-info-value">
                <span id="txt_related_department_name" >@Model.RELATED_DEPARTMENT_NAME</span>
                <label id="required_txt_related_department_name"></label>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Created Time </div>
            <div class="profile-info-value">
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
                        <input size=10 type="text" id="txt_user_id2_@no" value="@item.APPROVAL_NAME" readonly="readonly" />
                    </td>
                    <td>
                        <input size=20 type="text" id="txt_user_name2_@no" value="@item.USER_NAME" readonly="readonly" />
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

@If ViewBag.flag = 1 Then
    @<div class="profile-info-row">
        <div class="profile-info-name required"> Description </div>
        <div class="profile-info-value item-required">
            <input type="text" id="txt_desc" style="width:500px" maxlenght="200" />
            <label id="required_txt_desc"></label>
        </div>
    </div>
End If

<div class='modal-footer no-margin-top'>
    @If ViewBag.flag = 0 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
    ElseIf ViewBag.flag = 1 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("SendRequestRDDelete"))
    End If


    @If Model.ROW_STATUS = ListEnum.RowStat.Delete Or Model.ROW_STATUS = ListEnum.RowStat.InActive Then
        @<a Class="red" href="@Url.Action("Index", "RELATED_DEPARTMENT", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    Else
        @<a Class="red" href="@Url.Action("Index", "RELATED_DEPARTMENT", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
            @Html.Raw(Labels.ButtonForm("No"))
        </a>
    End If
</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Controllers/RELATED_DEPARTMENTController.js"></script>


