@ModelType eProcurementApps.Models.TPROC_CHART_OF_ACCOUNTS
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Edit Chart Of Account"
        ViewBag.Setup = "active open"
        ViewBag.IndexCOA = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Edit Chart Of Account"
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

<div class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Chart Of Account Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <span id="txt_action" class="hidden">Edit</span>
            </div>
            <div class="widget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row hidden">
                        <div class="profile-info-name "> ID </div>
                        <div class="profile-info-value ">
                            <span Class="" id="txt_id">@Model.ID</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> crcy cd </div>
                        <div class="profile-info-value">
                            <span class="" id="txt_crcy_cd" maxlenght="2">@Model.TPROC_CHART_OF_ACCOUNT_GR.CRCY_CD</span>
                            <label id="required_txt_crcy_cd"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> acct num </div>
                        <div class="profile-info-value item-required">
                            <span class="" id="txt_acct_num" maxlenght="8">@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM</span>
                            <label id="required_txt_acct_num"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> acct desc </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText" id="txt_acct_desc" maxlenght="100">@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC</span>
                            <label id="required_txt_acct_desc"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> converted acct num </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText" id="txt_converted_acct_num" maxlenght="8">@Model.TPROC_CHART_OF_ACCOUNT_GR.CONVERTED_ACCT_NUM</span>
                            <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Converted acc num for report budget. If there is no converter, please input same with Acct Num">?</span>
                            <label id="required_txt_converted_acct_num"></label>
                        </div>
                    </div>
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

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                <a Class="red" href="@Url.Action("Index", "CHART_OF_ACCOUNT", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>

                @If ViewBag.flag = 0 Then
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                Else
                    @Html.Raw(Labels.ButtonForm("SendRequestCOAEdit"))
                End If


            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Controllers/CHART_OF_ACCOUNTController.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>



