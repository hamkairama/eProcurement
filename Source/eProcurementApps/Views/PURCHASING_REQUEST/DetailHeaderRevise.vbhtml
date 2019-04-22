@ModelType eProcurementApps.Models.TPROC_PR_HEADER
@Imports eProcurementApps.Helpers

<link href="~/Ace/fileInput/fileinput.css" rel="stylesheet" />
<link href="~/Ace/fileInput/theme.css" rel="stylesheet" />
<script src="~/Ace/fileInput/fileinput.js"></script>

<style>
    .table-striped > tbody > tr:nth-child(odd) > td,
    .table-striped > tbody > tr:nth-child(odd) > th {
        white-space: nowrap;
    }

    #table_input {
        overflow-x: scroll;
        max-width: 100%;
        display: block;
        white-space: nowrap;
    }
</style>

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
        @ViewBag.MessageError
        <br />
    </div>
End If

<div Class="warning" id="input_reason_pr" style="display:none">
    <div class="col-lg-12">
        <b>Reason of reject :</b>
        <input type="text" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject_pr" />
    </div>
</div>

<div class="alert alert-info" id="msg_item" style="display:none">
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
                                        <th>No</th>
                                        <th>Item Name</th>
                                        <th>Specification</th>
                                        <th>Currency</th>
                                        <th>Qty</th>
                                        <th>Measurement</th>
                                        <th>WA</th>
                                        <th>Revise Qty</th>
                                        <th>Price (RP)</th>
                                        <th>Total Price</th>
                                        <th>Remark</th>
                                        <th>SuppName</th>
                                        <th>Convrt(RP)</th>
                                        <th>PO No</th>
                                        <th style="color:red">Approver</th>
                                        <th style="color:red; display:none">Id</th>
                                    </tr>
                                </thead>

                                <tbody id="dataTable">
                                    @code
                                        Dim no As Integer = 1
                                        Dim appvr As String = ""
                                        @For Each item In Model.TPROC_PR_DETAIL
                                            @<tr style="background-color: @CommonFunction.ConvertStatusItem(item.PR_DETAIL_STATUS)">
                                                <td>@no</td>
                                                <td>@item.ITEM_NAME</td>
                                                <td>@item.SPECIFICATION</td>
                                                <td>@item.CURRENCY</td>
                                                <td style="text-align:right"><input size=2 type="text" id="txt_qty_@no" readonly="readonly" value="@item.QTY" /></td>
                                                <td>@item.USER_MEASUREMENT</td>
                                                <td>@item.TPROC_WA.WA_NUMBER</td>
                                                
                                                 @if item.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Rejected Then
                                                    @<td style = "text-align:right" >
                                                        @If item.REVISED_QTY IsNot Nothing Then
                                                            @<input size=5 type="text" id="txt_revise_qty_@no" onkeyup="CalcItem(this)" value="@item.REVISED_QTY.Value" />
                                                        Else
                                                            @<input size=5 type="text" id="txt_revise_qty_@no" onkeyup="CalcItem(this)" value="" />
                                                        End If
                                                    </td>
                                                 Else
                                                     @<td><input size=5 type="text" id="txt_revise_qty_@no" value="" readonly="readonly" /></td>
                                                 End If
                                    
                                                <td style="text-align:right"><input size=10 type="text" readonly="readonly" id="txt_price_@no" value="@item.PRICE.ToString("###,###")" /></td>
                                                <td style="text-align:right"><input size=10 type="text" readonly="readonly" id="txt_total_price_@no" value="@item.TOTAL_PRICE.ToString("###,###")" /></td>
                                                @if item.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Rejected Then
                                                    @<td><input size=10 type="text" id="" value="@item.REMARK" /></td>
                                                Else
                                                     @<td><input size=10 type="text" id="" value="@item.REMARK" readonly="readonly" /></td>
                                                End If                                   
                                               
                                                <td>@item.SUPPLIER_NAME</td>
                                                <td style="">@item.CONVERSION_RP.Value.ToString("###,###")</td>
                                                <td style="">@item.PO_NUMBER</td>
                                                <td>
                                                    @for Each approver In item.TPROC_PR_APPR_WA
                                                        @if item.PR_DETAIL_STATUS = ListEnum.ItemStatus.Rejected Then
                                                            If approver.APPR_WA_STATUS = ListEnum.ApprItemStatus.Rejected.ToString() Then
                                                                @approver.NAME
                                                                Exit For
                                                            End If
                                                        ElseIf item.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete Then
                                                            If approver.APPR_WA_STATUS = ListEnum.ApprItemStatus.Approved.ToString() Then
                                                                @approver.NAME
                                                                Exit For
                                                            End If
                                                        End If
                                                    Next
                                                </td>
                                                <td style="display:none"><input size=2 type="text" id="txt_item_id_@no" value="@item.ID" /></td>
                                            </tr>
                                            no += 1
                                        Next
                                    End Code
                                </tbody>

                            </table>

                        </div>
                    </div>

                    <div Class="row">
                        <div Class="col-xs-10" style="text-align:right">
                            Total Price :
                        </div>

                        <div Class="col-xs-2" style="font-weight:bold">
                            <div>
                                Rp. <span id="txt_sub_total_price">@Model.SUB_TOTAL.ToString("###,###")</span>
                            </div>
                        </div>
                    </div>
                    <div class="hr hr-18 dotted hr-double"></div>

                    <div class="space-4"></div>

                    <div class="alert alert-danger" id="msg_error" style="display:none">
                        <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
                            <i class="ace-icon fa fa-times"></i>
                        </button>
                        msg error
                        <br />
                    </div>

                    <b>Related Department</b>
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
                                            <span Class="" id="txt_usage_code">@Model.TPROC_FORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.BUDGET_CODE</span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Budget Cd Start </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_budget_code_start">@Model.TPROC_FORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_START</span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Budget Cd End </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_budget_code_end">@Model.TPROC_FORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_END</span>
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


