@ModelType eProcurementApps.Models.TPROC_PR_HEADER
@Imports eProcurementApps.Helpers
@imports eprocurementApps.Facade

<link href="~/Ace/fileInput/fileinput.css" rel="stylesheet" />
<link href="~/Ace/fileInput/theme.css" rel="stylesheet" />
<script src="~/Ace/fileInput/fileinput.js"></script>

@Code
    ViewBag.Breadcrumbs = "Purchasing Request"
    ViewBag.Title = "Details PR"
    ViewBag.PurchasingRequest = "active open"
    ViewBag.ListPurchasingRequest = "active"
End Code

@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>  End If

@If ViewBag.MessageError IsNot Nothing Then
    @<div Class="alert alert-danger">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message 2
        <br />
    </div>
End If

<div Class="warning" id="input_reason_pr" style="display:none">
    <div class="col-lg-12">
        <b>Reason of reject :</b>
        <input type="text" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject_pr" />
    </div>
</div>

<div Class="alert alert-info" id="msg_item" style="display:none">
    <button type="button" class="close" data-dismiss="alert" id="close_msg_item">
        <i class="ace-icon fa fa-times"></i>
    </button>
    msg item
    <br />
</div>

@*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@

<div class="main-container" id="main-container">
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <div class="row">
                    <table>
                        <tr>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Request No : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.PR_NO</span> <span id="txt_pr_header_id" class="hidden">@Model.ID</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Form Type </div>
                                        <div class="profile-info-value">
                                            <span>@Model.TPROC_FORM_TYPE.FORM_TYPE_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Delivery Days : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.DELIVERY_DAYS</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Account Code : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.ACCOUNT_CODE</span>
                                        </div>
                                    </div>
                                </div>
                            </td>

                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> User Name : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.TPROC_USER.USER_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Sub Type </div>
                                        <div class="profile-info-value">
                                            <span>@Model.TPROC_FORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Exp. Dev Dt : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.EXP_DEV_DT.ToString("dd-MM-yyyy")</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Status : </div>
                                        <div class="profile-info-value">
                                            @code
                                                Dim status As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(Model.PR_STATUS)).ToString()
                                                @<span>@status</span>
                                            End Code
                                        </div>
                                    </div>
                                </div>
                            </td>

                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Request Date : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.PR_DATE.ToString("dd-MM-yyyy") </span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Good type </div>
                                        <div class="profile-info-value">
                                            <span>@Model.TPROC_GOOD_TYPE.GOOD_TYPE_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Req. Indicator : </div>
                                        <div class="profile-info-value">
                                            <span>@Model.PR_INDICATOR</span>
                                        </div>
                                    </div>

                                    @If Session("IS_EPROC_ADMIN") = 1 Then
                                        @<div Class="profile-info-row">
                                            <div Class="profile-info-name"> For Storage </div>
                                            <div Class="profile-info-value">
                                                @if Model.FOR_STORAGE = 1 Then
                                                    @<input type="checkbox" id="cb_for_storage" checked="checked" disabled="disabled" />
                                                Else
                                                    @<input type="checkbox" id="cb_for_storage" disabled="disabled" />
                                                End If

                                                <span Class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be pr storage and will be created po, good match, etc">?</span>
                                            </div>
                                        </div>
                                    Else
                                        @<div class="profile-info-row">
                                            <div class="profile-info-name">.</div>
                                            <div class="profile-info-value">
                                                <span Class=""></span>
                                            </div>
                                        </div>
                                    End If

                                </div>
                            </td>
                        </tr>
                    </table>
                    
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Reject Reason </div>
                            <div class="profile-info-value">
                                <span>@Model.REJECT_REASON</span> 
                            </div>
                        </div>
                    </div>

                    <div class="space-4"></div>

                    <div Class="clearfix">
                        <div Class="pull-right tableTools-container"></div>
                    </div>

                    <div class="form-group">
                        <div class="table-header" style="color:white">
                            Input Item
                        </div>
                        <div>
                            <table id="table_input" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th class="center">
                                            <label class="pos-rel">
                                                <input type="checkbox" id="select_all" class="ace" />
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th>No</th>
                                        <th>Item Name</th>
                                        <th>Item Code</th>
                                        <th>Specification</th>
                                        <th>Currency</th>
                                        <th>Qty</th>
                                        <th>Measurement</th>
                                        <th>WA No</th>
                                        <th>WA Desc</th>
                                        <th>Revise Qty</th>
                                        <th>Price (RP)</th>
                                        <th>Total Price</th>
                                        <th>Remark</th>
                                        <th>SuppName</th>
                                        <th>Convrt(RP)</th>
                                        <th>PO No</th>
                                        <th style="color:red">Approval</th>
                                    </tr>
                                </thead>

                                <tbody id="dataTable">
                                    @code
                                        Dim no As Integer = 1
                                        @For Each item In Model.TPROC_PR_DETAIL
                                            @<tr style="background-color: @CommonFunction.ConvertStatusItem(item.PR_DETAIL_STATUS)">
                                                <td class="center">
                                                    <label class="pos-rel">
                                                    @if item.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete Or item.PR_DETAIL_STATUS = ListEnum.ItemStatus.Rejected Then
                                                        @<input type="checkbox" Class="ace" id=" @item.ID" disabled="disabled" />
                                                    Else
                                                        @<input type="checkbox" Class="ace" id=" @item.ID"  />
                                                    End If  
                                                    <span Class="lbl"></span>
                                                    </label>
                                                </td>
                                                <td>@no</td>
                                                <td>@item.ITEM_NAME</td>
                                                 @If item.ITEM_ID IsNot Nothing Or item.ITEM_ID > 0 Then
                                                     @<td>@CommonFunction.GetItemCd(item.ITEM_ID.Value)</td>
                                                 Else
                                                     @<td>@item.ITEM_CD</td>
                                                 End If
                                                @*<td>@item.ITEM_CD</td>*@
                                                <td>@item.SPECIFICATION</td>
                                                <td>@item.CURRENCY</td>
                                                <td style="text-align:right">@item.QTY</td>
                                                <td>@item.USER_MEASUREMENT</td>
                                                <td>@item.TPROC_WA.WA_NUMBER</td>
                                                <td>@item.TPROC_WA.TPROC_APPROVAL_GR.DEPARTMENT_NAME</td>
                                                <td style="text-align:right">
                                                    @If item.REVISED_QTY IsNot Nothing Then
                                                        @item.REVISED_QTY.Value
                                                    End If
                                                </td>
                                                <td style="text-align:right">@item.PRICE.ToString("###,###")</td>
                                                <td style="text-align:right">                                                      
                                                    @If item.TOTAL_PRICE > 0 Then
                                                        @item.TOTAL_PRICE.ToString("###,###")
                                                    Else
                                                        @item.TOTAL_PRICE
                                                    End If
                                                </td>
                                                <td>@item.REMARK</td>
                                                <td>@item.SUPPLIER_NAME</td>
                                                <td style="">@item.CONVERSION_RP.Value.ToString("###,###")</td>
                                                <td>@item.PO_NUMBER</td>
                                                <td>
                                                    <div class="hidden-sm hidden-xs action-buttons">
                                                        @if Model.PR_STATUS <> ListEnum.PRStatus.PrRejected Then
                                                            @If ViewBag.FlagAction = ListEnum.ItemStatus.ReadyToApprove Or ViewBag.FlagAction = ListEnum.FlagInbox.ApprWA Then
                                                                Dim lappr_wa = item.TPROC_PR_APPR_WA.ToList().Where(Function(x) x.USER_ID = CurrentUser.GetCurrentUserId()) '"fauzana")
                                                                For Each waApprName In lappr_wa
                                                                    If waApprName.APPR_WA_STATUS = "Waiting for review" Then
                                                                        @<a Class="green" href="#" onclick="ModalDetailItem('/PURCHASING_REQUEST/DetailItemReview/','@item.ID','.dialogForm')" data-toggle="modal" title="Click to be reviewed">
                                                                            @Html.Raw(Labels.ButtonForm("Details"))
                                                                        </a>
                                                                    ElseIf (CommonFunction.IsReadyToApprove(item.TPROC_PR_APPR_WA.ToList()) = True And waApprName.APPR_WA_STATUS = "Waiting for approve") Then
                                                                        @<a Class="blue" href="#" onclick="ModalDetailItem('/PURCHASING_REQUEST/DetailItemApprove/','@item.ID','.dialogForm')" data-toggle="modal" title="Click to be approved">
                                                                            @Html.Raw(Labels.ButtonForm("Details"))
                                                                        </a>
                                                                    End If
                                                                Next
                                                            Else
                                                                @<a Class="blue" href="#" onclick="ModalDetailItem('/PURCHASING_REQUEST/DetailItem/','@item.ID','.dialogForm')" data-toggle="modal" title="Click to view detail">
                                                                    @Html.Raw(Labels.ButtonForm("Details"))
                                                                </a>
                                                            End If
                                                        End If                                                       
                                                    </div>
                                                    <div Class="hidden-md hidden-lg">
                                                        <div Class="inline pos-rel">
                                                            <Button Class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">
                                                                <i Class="ace-icon fa fa-caret-down icon-only bigger-120"></i>
                                                            </button>

                                                            <ul Class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
                                                                <li>
                                                                    <a Class="blue" href="#" onclick="ModalDetailItem('/PURCHASING_REQUEST/DetailItem/','@item.ID','.dialogForm')" data-toggle="modal" title="Click to view detail">
                                                                        @Html.Raw(Labels.IconAction("Details"))
                                                                    </a>
                                                                </li>
                                                                 @if Model.PR_STATUS <> ListEnum.PRStatus.PrRejected Then
                                                                      @If ViewBag.FlagAction = ListEnum.ItemStatus.ReadyToApprove Or ViewBag.FlagAction = ListEnum.FlagInbox.ApprWA Then
                                                                          For Each waApprName In item.TPROC_PR_APPR_WA.ToList().Where(Function(x) x.USER_ID = CurrentUser.GetCurrentUserId())
                                                                              If waApprName.APPR_WA_STATUS = "Waiting for review" Then
                                                                            @<li>
                                                                                <a Class="green" href="#" onclick="ModalDetailItem('/PURCHASING_REQUEST/DetailItemReview/','@item.ID','.dialogForm')" data-toggle="modal" title="Click to be reviewed">
                                                                                    @Html.Raw(Labels.IconAction("Details"))
                                                                                </a>
                                                                            </li>
                                                                              ElseIf waApprName.APPR_WA_STATUS = "Waiting for approve" Then
                                                                            @<li>
                                                                                <a Class="blue" href="#" onclick="ModalDetailItem('/PURCHASING_REQUEST/DetailItemApprove/','@item.ID','.dialogForm')" data-toggle="modal" title="Click to be approved">
                                                                                    @Html.Raw(Labels.IconAction("Details"))
                                                                                </a>
                                                                            </li>
                                                                              End If
                                                                          Next
                                                                      End If
                                                                 End If                                                               
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            no += 1
                                        Next
                                                                End Code
                                </tbody>

                            </table>

                        </div>
                    </div>

                    <div Class="row">
                        <div Class="col-xs-8">
                            <div>
                                                                        Note :   &nbsp;&nbsp;&nbsp;&nbsp;
                                <span Class="blue">@Html.Raw(Labels.IconAction("Details"))</span>  <b>For view the detail</b> &nbsp;&nbsp;&nbsp;&nbsp;
                                @*<span class="green"> @Html.Raw(Labels.IconAction("Review"))</span> : <b>for review the item</b> &nbsp;&nbsp;&nbsp;&nbsp;
                                <span class="blue"> @Html.Raw(Labels.IconAction("Approve"))</span> : <b>for approve the item</b> &nbsp;&nbsp;&nbsp;&nbsp;*@
                            </div>
                        </div>

                        <div Class="col-xs-2" style="text-align:right">
                            Total Price :  
                        </div>

                        <div Class="col-xs-2" style="font-weight:bold">
                            <div>
                                @If Model.SUB_TOTAL > 0 Then
                                Dim rp As String = "Rp."
                                    @rp @<span>@Model.SUB_TOTAL.ToString("###,###")</span>
                                Else
                                    @Model.SUB_TOTAL
                                End If
                            </div>
                        </div>
                    </div>
                    <div Class="hr hr-18 dotted hr-double"></div>
                    <div Class="space-4"></div>
                    
                    @If Model.PR_STATUS = ListEnum.PRStatus.Submitted And ViewBag.SubTitle = "Approval/Review WA of each item" Then
                        @<button class="btn btn-mini btn-success btn-sm" onclick="ActionApproveReviewItemSelected()" title="Approver/Review selected">
                            <i class="ace-icon fa fa-check bigger-200"></i>
                            Approver/Review selected
                        </button>

                        @<button class="btn btn-mini btn-danger btn-sm" onclick="ActionRejectItemSelected('@Session("USER_ID").ToString()')" title="Reject selected">
                            <i class="ace-icon fa fa-trash-o bigger-200"></i>
                            Reject selected
                        </button>

                        @<div class="row top-left" id="input_reason_selected" style="display:none">
                            <div class="col-lg-12">
                                Reason of reject :
                                <textarea type="text" style="height:100px" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject"></textarea>
                            </div>
                        </div>

                        @<div class="hr hr-18 dotted hr-double"></div>
                        @<div class="space-4"></div>
                    End If

                    @if Session("IS_EPROC_ADMIN") = 1 And Model.PR_STATUS <> ListEnum.PRStatus.PrRejected Then
                        @<button class="btn btn-mini btn-warning btn-sm" onclick="ActionRejectByEproc('@Session("USER_ID").ToString()')" title="Reject PR by Admin Eproc">
                            <i class="ace-icon fa fa-trash-o bigger-200"></i>
                            Reject PR by Admin Eproc
                        </button>

                        @<div class="row top-left" id="input_reason_by_eproc" style="display:none">
                            <div class="col-lg-12">
                                Reason of reject by eproc :
                                <textarea type="text" style="height:100px" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject_by_eproc"></textarea>
                            </div>
                        </div>

                        @<div class="hr hr-18 dotted hr-double"></div>
                        @<div class="space-4"></div>
                    End If


                    <div Class="alert alert-danger" id="msg_error" style="display:none">
                        <button type = "button" Class="close" data-dismiss="alert" id="close_msg_error">
                            <i Class="ace-icon fa fa-times"></i>
                        </button>
                        msg error
                        <br />
                    </div>

                    <b> Related Department</b>
                    @If (ViewBag.PRStatus = 0 And ViewBag.PRStatus IsNot Nothing) And Facade.PurchasingRequestFacade.CheckIsAllGRItemNotOutStanding(Model) And Model.USER_ID_ID = Session("USER_ID_ID") Then
                        @Html.Raw(Labels.ButtonForm("PushEmailByRD"))
                    End If
                    <div Class="form-group">
                        <Table Class="table table-striped table-bordered table-hover">
                            <tr>
                                @For Each item_gr In Model.TPROC_PR_APPR_RELDEPT_GR
                                    @<td>
                                        <b>@item_gr.RELDEPT_NAME</b>
                                        <table>
                                            @For Each item_dt In item_gr.TPROC_PR_APPR_RELDEPT_DT
                                                @<tr>
                                                    @if item_gr.RELDEPT_GR_STATUS = ListEnum.RDStatus.Complete And Model.PR_STATUS <> Convert.ToInt32(ListEnum.PRStatus.PrRejected) Then
                                                        @if item_dt.APPR_RELDEPT_STATUS.Equals("Approved") Then
                                                            @<td>@item_dt.NAME &nbsp; &nbsp;</td>
                                                                @<td style="color:blue">
                                                                    @item_dt.APPR_RELDEPT_STATUS  &nbsp; &nbsp;
                                                                    @item_dt.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")
                                                                </td>
                                                        End If
                                                    ElseIf item_gr.RELDEPT_GR_STATUS = ListEnum.RDStatus.Rejected Then
                                                        @if item_dt.APPR_RELDEPT_STATUS.Equals("Rejected") Then
                                                            @<td>@item_dt.NAME &nbsp; &nbsp;</td>
                                                                @<td style="color:red">
                                                                    @item_dt.APPR_RELDEPT_STATUS  &nbsp; &nbsp;
                                                                    @item_dt.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")
                                                                </td>
                                                        End If
                                                    ElseIf item_gr.RELDEPT_GR_STATUS <> ListEnum.RDStatus.Rejected And item_gr.RELDEPT_GR_STATUS <> ListEnum.RDStatus.Complete And Model.PR_STATUS <> Convert.ToInt32(ListEnum.PRStatus.PrRejected) Then
                                                        @<td>@item_dt.NAME &nbsp; &nbsp;</td>
                                                            @<td style="color:green">
                                                                @item_dt.APPR_RELDEPT_STATUS  &nbsp; &nbsp;
                                                            </td>
                                                    End If
                                                </tr>
                                            Next
                                        </table>
                                        <hr />
                                        <span>Reject reason : @item_gr.REJECT_REASON</span>
                                    </td>
                                Next
                            </tr>
                        </Table>
                    </div>

                    <div Class="space-4"></div>

                    <b> File Attachment</b>                    
                    @If Model.TPROC_PR_ATTACHMENT.Count > 0 Then
                        @<div Class="form-group">
                            <Table Class="table table-striped table-bordered table-hover">
                                <tr>
                                    <th> No</th>
                                    <th> File Name</th>
                                    <th> Action</th>
                                </tr>
                                @code
                                    Dim no_item As Integer = 1
                                    @For Each item In Model.TPROC_PR_ATTACHMENT
                                        @<tr>
                                            <td>@no_item</td>
                                            <td>@item.FILE_NAME</td>
                                            <td>@Html.ActionLink("Download", "Download", New With {.id = item.ID}) </td>
                                        </tr>
                                        no_item += 1
                                    Next
                                End Code
                            </Table>
                        </div>
                                    Else
                        @<div Class="col-sm-12">
                            @Using (Html.BeginForm("InsertDocuments", "PURCHASING_REQUEST", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
                                @<div class="col-xs-6">
                                    <input type="file" id="id-input-file-2" name="UploadedFile" />                                                   
                                </div>
                                @Html.TextBox("pr_header_id", Convert.ToDecimal(Model.ID), New With {.class = "hidden"})
                                @<div class="col-xs-3">
                                    @Html.Raw(Labels.ButtonForm("Upload"))
                                        <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="You can add attachment. Max file name is 200 characters and max size is 5 MB ">?</span>
                                </div>
                            End Using
                        </div>
                                    End If

                    <div class="space-4"></div>

                    <b>Information Budget</b>
                    <Table>
                        <tr>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Usage Code </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_usage_code">@ViewBag.Bc</span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Budget Cd Start </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_budget_code_start">@ViewBag.Bcs</span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Budget Cd End </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_budget_code_end">@ViewBag.Bce</span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </Table>

                    <div class="hr hr-18 dotted hr-double"></div>

                    <div class="widget-body">
                        <div class="widget-main">
                            <div id="fuelux-wizard-container">
                                <div>
                                    <ul class="steps">
                                        @For Each item In Model.TPROC_PR_HISTORICAL.OrderBy(Function(x) x.CREATED_TIME)
                                            @<li Class="active">
                                                @If item.HISTORICAL_STATUS = ListEnum.PRStatus.PrRejected.ToString() Then
                                                    @<span Class="step"><i class="ace-icon red fa fa-times"></i></span>
                                                Else
                                                    @<span Class="step"><i class="ace-icon green fa fa-check"></i></span>
                                                End If
                                                <span Class="title" style="font-size:12px;">@item.HISTORICAL_STATUS</span>
                                                <span Class="title" style="font-size:12px;">@item.HISTORICAL_BY</span>
                                                <span Class="title" style="font-size:12px;">@item.HISTORICAL_DT.ToString("dd-MM-yyyy HH:mm") </span>
                                            </li>
                                        Next
                                    </ul>
                                </div>
                            </div>

                            <hr />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>

<Script src="~/Scripts/Standard/StandardProfile.js"></Script>
<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<Script src="~/Scripts/Controllers/PR_DETAIL_HEADERController.js"></Script>

<script>
    jQuery(function ($) {
        $('#id-input-file-2').ace_file_input({
            no_file: 'No File ...',
            btn_choose: 'Choose',
            btn_change: 'Change',
            droppable: false,
            onchange: null,
            thumbnail: false //| true | large
            //whitelist:'gif|png|jpg|jpeg'
            //blacklist:'exe|php'
            //onchange:''
            //
        });
    });
</script>
