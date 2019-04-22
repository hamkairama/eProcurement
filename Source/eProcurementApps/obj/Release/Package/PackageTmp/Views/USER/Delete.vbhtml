@ModelType eProcurementApps.Models.TPROC_USER
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.Flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Delete User"
        ViewBag.Setup = "active open"
        ViewBag.IndexUser = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Delete User"
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

@*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
<span id="txt_action" class="hidden">Delete</span>
<div class="modal-body no-padding">
    <div class="profile-user-info">
        <div class="profile-info-row  hidden">
            <div class="profile-info-name"> ID </div>
            <div class="profile-info-value">
                <span id="txt_id">@Model.ID.ToString("0")</span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> User ID </div>
            <div class="profile-info-value">
                <span id="txt_user_id2">@Model.USER_ID</span>
                <label id="required_txt_user_id2"></label>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Name </div>
            <div class="profile-info-value">
                <span>@Model.USER_NAME</span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Mail </div>
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
                    <input type="text" id="txt_desc" style="width:500px"  maxlenght="200" />
                    <label id="required_txt_desc"></label>
                </div>
            </div>
     End If

</div>

@*<div class='modal-footer no-margin-top'>
    Are you sure want to delete ?
    @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
    @Html.Raw(Labels.ButtonForm("No"))
</div>*@


<div class='modal-footer no-margin-top'>
    @If ViewBag.flag = 0 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
    ElseIf ViewBag.flag = 1 And Model.ROW_STATUS = ListEnum.RowStat.Live Then
        @<span>Are you sure want to delete ?</span>
        @Html.Raw(Labels.ButtonForm("SendRequestUserDelete"))
    End If
    
    @If (ViewBag.flag = 1 And Model.ROW_STATUS = ListEnum.RowStat.Delete) Or (ViewBag.flag = 1 And Model.ROW_STATUS = ListEnum.RowStat.Live) Then
        @<a Class="red" href="@Url.Action("IndexRequestUser", "User")" title="Close">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    Else
        @<a Class="red" href="@Url.Action("Index", "User", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    End If
</div>

<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/USERController.js"></script>



