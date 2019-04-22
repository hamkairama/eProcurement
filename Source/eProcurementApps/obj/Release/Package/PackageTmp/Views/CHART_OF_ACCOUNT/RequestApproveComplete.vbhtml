@ModelType eProcurementApps.Models.TPROC_CHART_OF_ACCOUNTS
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
                <h4 class="widget-title">Requestor Detail</h4>
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

    <div class="col-sm-6">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Chart Of Account</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
            </div>
            <div class="widget-body" id="">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name "> ID </div>
                        <div class="profile-info-value ">
                            <span Class="" id="txt_id">@Model.ID</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Crcy Cd </div>
                        <div class="profile-info-value">
                            <span Class="" id="txt_crcy_cd" maxlenght="10">@Model.TPROC_CHART_OF_ACCOUNT_GR.CRCY_CD</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Acct Num </div>
                        <div class="profile-info-value">
                            <span Class="freeText" id="txt_acct_num" maxlenght="8">@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM</span>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name "> Acct Desc </div>
                        <div Class="profile-info-value ">
                            <span Class="freeText" id="txt_acct_desc" maxlenght="100">@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC</span>
                        </div>
                    </div>

                     <div class="profile-info-row">
                        <div class="profile-info-name "> Converted Acct Num </div>
                        <div class="profile-info-value">
                            <span Class="freeText" id="txt_converted_acct_num" maxlenght="8">@Model.TPROC_CHART_OF_ACCOUNT_GR.CONVERTED_ACCT_NUM</span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    @code
        Dim coa_gr_tobe As New TPROC_CHART_OF_ACCOUNT_GR
        coa_gr_tobe = Facade.COAFacade.GetCoaGrToBe(Model.ID, ListEnum.RowStat.Edit)

        If coa_gr_tobe IsNot Nothing Then
            @<div class="col-sm-6">
                <div class="widget-box">
                    <div class="widget-header">
                        <h4 class="widget-title">Chart Of Account To be</h4>
                    </div>
                    <div class="widget-body" id="">
                        <div class="profile-user-info">
                            <div class="profile-info-row">
                                <div class="profile-info-name "> ID </div>
                                <div class="profile-info-value ">
                                    <span Class="" id="txt_id">@Model.ID</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> Crcy Cd </div>
                                <div class="profile-info-value">
                                    <span Class="" id="txt_crcy_cd" maxlenght="10">@coa_gr_tobe.CRCY_CD</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> Acct Num </div>
                                <div class="profile-info-value">
                                    <span Class="freeText" id="txt_acct_num" maxlenght="8">@coa_gr_tobe.ACCT_NUM</span>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name "> Acct Desc </div>
                                <div Class="profile-info-value ">
                                    <span Class="freeText" id="txt_acct_desc" maxlenght="100">@coa_gr_tobe.ACCT_DESC</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> Converted Acct Num </div>
                                <div class="profile-info-value">
                                    <span Class="freeText" id="txt_converted_acct_num" maxlenght="8">@coa_gr_tobe.CONVERTED_ACCT_NUM</span>
                                </div>
                            </div>

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
                @Html.Raw(Labels.ButtonForm("ActionRequestCOAComplete"))
                ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                @Html.Raw(Labels.ButtonForm("ActionRequestCOAApprove"))
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


<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Controllers/CHART_OF_ACCOUNTController.js"></script>

