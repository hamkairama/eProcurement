@ModelType eProcurementApps.Models.TPROC_CHART_OF_ACCOUNTS
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Delete Chart Of Account"
        ViewBag.Setup = "active open"
        ViewBag.IndexCOA = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Delete Chart Of Account"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestCOA = "active"
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
            <div class="profile-info-name"> crcy cd </div>
            <div class="profile-info-value">
                <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.CRCY_CD </span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> acct num </div>
            <div class="profile-info-value">
                <span id="txt_acct_num">@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM </span>
                <label id="required_txt_acct_num"></label>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> acct desc </div>
            <div class="profile-info-value">
                <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC </span>
            </div>
        </div>

        <div class="profile-info-row">
            <div class="profile-info-name"> Converted acct num </div>
            <div class="profile-info-value">
                <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.CONVERTED_ACCT_NUM</span>
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
        @Html.Raw(Labels.ButtonForm("SendRequestCOADelete"))
    End If

    @If Model.ROW_STATUS = ListEnum.RowStat.Delete Then
        @<a Class="red" href="@Url.Action("Index", "CHART_OF_ACCOUNT", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    Else
        @<a Class="red" href="@Url.Action("Index", "CHART_OF_ACCOUNT", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="No">
            @Html.Raw(Labels.ButtonForm("Close"))
        </a>
    End If

</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Controllers/CHART_OF_ACCOUNTController.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>


