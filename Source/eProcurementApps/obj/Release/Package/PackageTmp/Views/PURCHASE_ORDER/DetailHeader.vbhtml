@ModelType eProcurementApps.Models.TPROC_PO_HEADERS
@Imports eProcurementApps.Helpers

<link href="~/Ace/fileInput/fileinput.css" rel="stylesheet" />
<link href="~/Ace/fileInput/theme.css" rel="stylesheet" />
<script src="~/Ace/fileInput/fileinput.js"></script>

<style>
    .spr {
        text-align: left;
        width: 100px;
    }
</style>

@Code
    ViewBag.Breadcrumbs = "Purchase Order"
    ViewBag.Title = "Detail Purchase Order"
    ViewBag.PurchaseOrder = "active open"
    ViewBag.IndexListPO = "active"
End Code

@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>  End If

<div Class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Purchase Order</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                <div style="float: right;">
                    <div Class="profile-info-name"> For Storage </div>
                    <div Class="profile-info-value">
                        @if Model.FOR_STORAGE = 1 Then
                            @<input type="checkbox" id="cb_for_storage" checked="checked" disabled="disabled" />
                        Else
                            @<input type="checkbox" id="cb_for_storage" disabled="disabled" />
                        End If

                        <span Class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be pr storage and will be created po, good match, etc">?</span>
                    </div>

                    <div class="profile-info-name"> Created Date </div>
                    <div class="profile-info-value">
                        <span Class="" id="txt_created_dt">@Model.CREATED_TIME.ToString("dd-MM-yyyy") </span>
                    </div>
                </div>
            </div>
            <div class="widget-body" id="">
                <Table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Number : </div>
                                    <div class="profile-info-value">
                                        <span class="" id="txt_number_po">@Model.PO_NUMBER</span>
                                        <span class="hidden" id="txt_number_id">@Model.ID</span>
                                        <span class="hidden" id="txt_pc_id">@Model.PC_ID</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Type </div>
                                    <div class="profile-info-value">
                                        <span id="po_type">@Model.TPROC_PO_TYPE.PO_TYPE_NAME</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Notes : </div>
                                    <div class="profile-info-value">
                                        <span class="" id="txt_notes">@Model.NOTES</span>
                                    </div>
                                </div>
                            </div>
                        </td>

                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Currency </div>
                                    <div class="profile-info-value">
                                        <span>@Model.TPROC_CURRENCY.CURRENCY_NAME</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Status </div>
                                    <div class="profile-info-value">
                                        @code
                                            Dim status As String = [Enum].GetName(GetType(ListEnum.PO), Int32.Parse(Model.PO_STATUS)).ToString()
                                            @<span id="txt_po_status_changed">@status</span>
                                        End Code
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"></div>
                                    <div class="profile-info-value">
                                    </div>
                                </div>
                            </div>
                        </td>

                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"></div>
                                    <div class="profile-info-value">
                                        <div class="hidden-sm hidden-xs action-buttons">
                                            <a Class="green" href="#" onclick="ModalDetailItem('/PURCHASE_ORDER/PopCRVDetails/','@Model.ID','.dialogForm')" data-toggle="modal" title="Detail">
                                                @Html.Raw(Labels.ButtonForm("OpenCrvDetail"))
                                            </a>
                                        </div>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"></div>
                                    <div class="profile-info-value">
                                        <div class="hidden-sm hidden-xs action-buttons">
                                            <a Class="green" href="#" onclick="ModalDetailItem('/PURCHASE_ORDER/PopPCDetails/','@Model.PC_ID','.dialogForm')" data-toggle="modal" title="Detail">
                                                @Html.Raw(Labels.ButtonForm("OpenPCDetail"))
                                            </a>
                                        </div>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"></div>
                                    <div class="profile-info-value">
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </Table>

            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Details</h4>
                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Item</th>
                            <th>U/M</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th>Total</th>
                            <th style="color:red">
                                View
                            </th>
                        </tr>
                    </thead>

                    <tbody>
                        @code
                            @For Each item In Model.TPROC_PO_DETAILS_ITEM
                                @<tr>
                                    <td>@item.ITEM_NAME</td>
                                    <td>@item.UNITMEASUREMENT</td>
                                    <td>@item.QUANTITY</td>
                                    <td style="text-align:right">
                                        @if item.TPROC_PO_DETAILS(0).PRICE <> Nothing Then
                                            @item.TPROC_PO_DETAILS(0).PRICE.Value.ToString("###,###")
                                        End If                                        
                                    </td>
                                    <td style="text-align:right">@item.PRICE.Value.ToString("###,###")</td>
                                    <td>
                                        <div class="hidden-sm hidden-xs action-buttons">
                                            <a Class="green" href="#" onclick="ModalDetailItem('/PURCHASE_ORDER/DetailItemPO/','@item.ID','.dialogForm')" data-toggle="modal" title="Detail">
                                                @Html.Raw(Labels.IconAction("Details"))
                                            </a>
                                        </div>
                                    </td>
                                </tr>
                            Next
                        End Code
                    </tbody>
                </table>

            </div>
        </div>
    </div> 

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Supplier</h4>
                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <span id="txt_cb_supplier_nm" hidden="hidden"></span>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Name </div>
                                    <div class="profile-info-value">
                                        <span>@Model.SUPPLIER_NAME</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span>@Model.SUPPLIER_PHONE</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span>@Model.SUPPLIER_ADDTRSS</span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Contact Person </div>
                                    <div class="profile-info-value">
                                        <span>@Model.CONTACT_PERSON</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span>@Model.SUPPLIER_FAX</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"></div>
                                    <div class="profile-info-value">
                                        <span Class="" id=""></span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Delivery to</h4>
                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Name </div>
                                    <div class="profile-info-value">
                                        <span>@Model.DELIVERY_NAME</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span>@Model.DELIVERY_PHONE</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span>@Model.DELIVERY_ADDRESS</span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Date </div>
                                    <div class="profile-info-value">
                                        <span>@Model.DELIVERY_DATE.Value.ToString("dd-MM-yyyy")</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span>@Model.DELIVERY_FAX</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"></div>
                                    <div class="profile-info-value">
                                        <span Class="" id=""></span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>   

    @If ViewBag.OnlyView Is Nothing Then
        @<div Class="col-sm-12">
            @Using (Html.BeginForm("InsertDocuments", "PURCHASE_ORDER", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
                @<div class="col-xs-6">
                    <input type="file" id="id-input-file-2" name="UploadedFile" />
                </div>
                @<div class="col-xs-3">
                    @Html.Raw(Labels.ButtonForm("Upload"))
                </div>
            End Using
        </div>
    End If
    
    <div class="col-sm-12">
        @If Model.FILE_NAME IsNot Nothing Then
            @<div Class="col-sm-12">
                <Table Class="table table-striped table-bordered table-hover">
                    <tr>
                        <th> File Name</th>
                        <th> Action</th>
                    </tr>
                    <tr>
                        <td>@Model.FILE_NAME</td>
                        <td>@Html.ActionLink("Download", "Download", New With {.id = Model.ID}) </td>
                    </tr>
                </Table>
            </div>
        End If
    </div>

    @code
        @if Model.PO_STATUS = ListEnum.PO.Submitted Then
            @<div Class="col-sm-12">
                <div Class="widget-box widget-color-orange ui-sortable-handle collapsed">
                    <div Class="widget-header widget-header-small">
                        <h6 Class="widget-title">
                            Verify
                        </h6>
                        <div Class="widget-toolbar">
                            <a href = "#" data-action="collapse">
                                <i Class="ace-icon fa fa-chevron-down" data-icon-show="fa-chevron-down" data-icon-hide="fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div Class="widget-body" style="display: none;">
                        <div Class="widget-main">
                            @*@If Model.TPROC_VRF_PO IsNot Nothing Then
                                If Model.PO_STATUS = ListEnum.PO.Verified Then
                                    @<ul>
                                        @for Each item In Model.TPROC_VRF_PO
                                            If item.STATUS = ListEnum.PO.Verified.ToString() Then
                                                @<li>
                                                    @item.NAME - @item.STATUS
                                                </li>
                                            End If
                                        Next
                                    </ul>
                                Else
                                     @<ul>
                                        @for Each item In Model.TPROC_VRF_PO
                                            @<li>
                                                @item.NAME - @item.STATUS
                                            </li>
                                        Next
                                    </ul>
                                End If
                            End If*@

                            @If Model.TPROC_VRF_PO IsNot Nothing Then
                                 @<ul>
                                    @for Each item In Model.TPROC_VRF_PO
                                        @<li>
                                            @item.NAME - @item.STATUS
                                        </li>
                                    Next
                                </ul>
                            End If              
                    
                        </div>
                    </div>
                </div>
            </div>
        End If

        @if Model.PO_STATUS = ListEnum.PO.Submitted Or Model.PO_STATUS = ListEnum.PO.Verified Then
            @<div class="col-sm-12">
                <div class="widget-box widget-color-orange ui-sortable-handle collapsed">
                    <div class="widget-header widget-header-small">
                        <h6 class="widget-title">
                            Approval
                        </h6>
                        <div style="float: right;">
                            @if Model.PO_STATUS = ListEnum.PO.Verified Then
                                @Html.Raw(Labels.ButtonForm("PushEmailByPO"))
                            End If
                                                        
                            <div class="widget-toolbar">
                                <a href="#" data-action="collapse">
                                    <i class="ace-icon fa fa-chevron-down" data-icon-show="fa-chevron-down" data-icon-hide="fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        
                    </div>
                    <div class="widget-body" style="display: none;">
                        <div class="widget-main">
                            @*@If Model.TPROC_APPR_PO IsNot Nothing Then
                                If Model.PO_STATUS = ListEnum.PO.Approved Or Model.PO_STATUS = ListEnum.PO.Completed Then
                                     @<ul>
                                         @for Each item In Model.TPROC_APPR_PO
                                             If item.STATUS = ListEnum.PO.Reviewed.ToString() Then
                                                 @<li>
                                                     @item.NAME - @item.STATUS
                                                 </li>
                                             End If

                                             If item.STATUS = ListEnum.PO.Approved.ToString() Then
                                                 @<li>
                                                     @item.NAME - @item.STATUS
                                                 </li>
                                             End If
                                         Next
                                     </ul>
                                    Else
                                     @<ul>
                                         @for Each item In Model.TPROC_APPR_PO
                                             @<li>
                                                 @item.NAME - @item.STATUS
                                             </li>
                                         Next
                                     </ul>
                                    End If
                                End If*@

                            @If Model.TPROC_APPR_PO IsNot Nothing Then
                                @<ul>
                                    @for Each item In Model.TPROC_APPR_PO
                                        @<li>
                                            @item.NAME - @item.STATUS
                                        </li>
Next
                                </ul>
                            End If
                        </div>
                    </div>
                </div>
            </div>
        End If
    End Code

</div>

<div Class="alert alert-danger" id="msg_error" style="display:none">
    <Button type = "button" Class="close" data-dismiss="alert" id="close_msg_error">
        <i Class="ace-icon fa fa-times"></i>
    </Button>
    msg error
    <br />
</div>

<div Class="hr hr-18 dotted hr-double"></div>

<div Class="col-sm-6">
    <table id="apprvol_group_table">
        <tr>
            <td>Prepared By </td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr"><span Class="" id="txt_prepared_by_po">@Model.PREPARED_BY</span></td>
            <td class="spr"><span Class="" id="txt_prepared_dt">@Model.CREATE_DATE.ToString("dd-MM-yyyy")</span></td>
        </tr>

        <tr>
            <td>Verified By</td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr">
                <span id="txt_verify_by_po">
                    @Select Case Model.PO_STATUS
                        Case 2
                            @Session("USER_ID")
                        Case Else
                            @Model.VERIFIED_BY
                    End Select
                </span>
            </td>
            <td class="spr">
                <span id="txt_verify_date_po">
                    @Select Case Model.PO_STATUS
                Case 2
                            @*@Date.Now.ToString("dd-MM-yyyy")*@
                    Case Else
                        @If Model.VERIFIED_DATE IsNot Nothing Then
                            @Model.VERIFIED_DATE.Value.ToString("dd-MM-yyyy")
                        End If
                    End Select
                </span>
            </td>
        </tr>

        <tr>
            <td>Reviewed By</td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr">
                <span id="txt_review_by_po">
                    @Select Case Model.PO_STATUS
                        Case 2
                            @Session("USER_ID")
                        Case Else
                            @Model.REVIEWED_BY
                    End Select
                </span>
            </td>
            <td class="spr">
                <span id="txt_review_date_po">
                    @Select Case Model.PO_STATUS
                Case 2
                            @*@Date.Now.ToString("dd-MM-yyyy")*@
                    Case Else
                        @If Model.REVIEWED_DATE IsNot Nothing Then
                            @Model.REVIEWED_DATE.Value.ToString("dd-MM-yyyy")
                        End If
                    End Select
                </span>
            </td>
        </tr>
        <tr>
            <td> Approved By </td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr">
                <span Class="" id="txt_approved_by_po">
                    @Select Case Model.PO_STATUS
                        Case 1
                            @Session("USER_ID")
                        Case Else
                            @Model.APPOROVED_BY
                    End Select
                </span>
            </td>
            <td class="spr">
                <span id="txt_approved_date_po">
                    @Select Case Model.PO_STATUS
                        Case 1
                            @*@Date.Now.ToString("dd-MM-yyyy")*@
                    Case Else
                        @If Model.APPOROVED_DATE IsNot Nothing Then
                            @Model.APPOROVED_DATE.Value.ToString("dd-MM-yyyy")
                        End If
                    End Select
                </span>
            </td>
        </tr>

        <tr>
            <td> Completed By </td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr">
                <span id="txt_completed_by_po">
                    @Select Case Model.PO_STATUS
                        Case 3
                            @Session("USER_ID")
                        Case Else
                            @Model.COMPLETED_BY
                    End Select
                </span>
            </td>
            <td class="spr">
                <span id="txt_completed_date_po">
                    @Select Case Model.PO_STATUS
                Case 3
                            @*@Date.Now.ToString("dd-MM-yyyy")*@
                    Case Else
                        @If Model.COMPLETED_DATE IsNot Nothing Then
                            @Model.COMPLETED_DATE.Value.ToString("dd-MM-yyyy")
                        End If
                    End Select
                </span>
            </td>
        </tr>

        <tr>
            <td> Rejected By </td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr">
                <span id="txt_rejected_by_po">                    
                    @Model.REJECT_BY               
                </span>
            </td>
            <td class="spr">
                <span id="txt_rejected_date_po">
                    @Select Case Model.PO_STATUS
                Case 3
                            @*@Date.Now.ToString("dd-MM-yyyy")*@
                    Case Else
                        @If Model.REJECT_DATE IsNot Nothing Then
                            @Model.REJECT_DATE.Value.ToString("dd-MM-yyyy")
                        End If
                    End Select
                </span>
            </td>
        </tr>
        <tr>
            <td> Rejected Reason </td>
            <td>: &nbsp; &nbsp;</td>
            <td class="spr" colspan="2">
                <span id="txt_rejected_reason">
                   @Model.REJECT_REASON
                </span>
            </td>           
        </tr>
    </table>
</div>

<div class="col-sm-6">
    <div class="row">
        <div class="col-xs-4" style="text-align:right">
            Sub Total :
        </div>

        <div class="col-xs-4" style="font-weight:bold;text-align:right">
            <div class="col-xs-1">
                Rp.
            </div>
            <div style="text-align:right">
                <span>@Model.SUB_TOTAL.Value.ToString("###,###")</span>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-4" style="text-align:right">
            Discount
            @If Model.IS_DISC_PERC Then
                @<label>
                    <input name="disc" type="radio" Class="ace" id="rb_disc_perc" checked="checked" />
                    <span Class="lbl">perc(@Model.DISCOUNT_TEMP.Value.ToString("###,###"))</span>
                </label>
            Else
                @<Label>
                    <input name="disc" type="radio" Class="ace" id="rb_disc_rp" checked="checked" />
                    <span Class="lbl">exact(Rp)</span>
                </Label>
            End If
        </div>

        <div class="col-xs-4" style="font-weight:bold;text-align:right">
            <div class="col-xs-1">
                Rp.
            </div>
            <div style="text-align:right">
                <span>@Model.DSCNT_AMT.Value.ToString("###,###")</span>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-4" style="text-align:right">
            VAT
            @If Model.IS_VAT_PERC Then
                @<Label>
                    <input name="vat" type="radio" Class="ace" id="rb_vat_perc" checked="checked" />
                    <span Class="lbl">perc(@Model.VAT_TEMP.Value.ToString("###,###"))</span>
                </Label>
            Else
                @<Label>
                    <input name="vat" type="radio" Class="ace" id="rb_vat_rp" checked="checked" />
                    <span Class="lbl">exact(Rp)</span>
                </Label>
            End If
        </div>

        <div class="col-xs-4" style="font-weight:bold;text-align:right">
            <div class="col-xs-1">
                Rp.
            </div>
            <div style="text-align:right">
                <span>@Model.VAT.Value.ToString("###,###")</span>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-4" style="text-align:right">
            Wth tax / PPH
            @If Model.IS_PPH_PERC Then
                @<Label>
                    <input name="pph" type="radio" Class="ace" id="rb_pph_perc" checked="checked" />
                    <span Class="lbl">perc(@Model.PPH_TEMP.Value.ToString("###,###"))</span>
                </Label>
            Else
                @<Label>
                    <input name="pph" type="radio" Class="ace" id="rb_pph_rp" checked="checked" />
                    <span Class="lbl">exact(Rp)</span>
                </Label>
            End If
        </div>

        <div class="col-xs-4" style="font-weight:bold;text-align:right">
            <div class="col-xs-1">
                Rp.
            </div>
            <div style="text-align:right">
                <span>@Model.WTH_TAX.Value.ToString("###,###")</span>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-4" style="text-align:right">
            Grand Total :
        </div>

        <div class="col-xs-4" style="font-weight:bold;text-align:right">
            <div class="col-xs-1">
                Rp.
            </div>
            <div style="text-align:right">
                <span>@Model.GRAND_TOTAL.Value.ToString("###,###")</span>
            </div>
        </div>
    </div>

</div>

<div class="hr hr-18 dotted hr-double"></div>

<div class="clearfix form-action">
    <div class="col-lg-12">
        <div class="modal-footer no-margin-top">
            @If (Session("IS_EPROC_ADMIN") = 1 Or (ViewBag.AsIsVrf <> "Verifier" And ViewBag.AsIsVrf <> "Reviewer" And ViewBag.AsIsVrf <> "Approver")) And ViewBag.OnlyView <> "1" Then
               @<a Class="red" href="@Url.Action("_ListPO", "PURCHASE_ORDER")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If Model.PO_STATUS = ListEnum.PO.Submitted And ViewBag.AsIsVrf.Equals("Verifier") And ViewBag.StatusAsVrf.Equals("Waiting for verify") And Model.PO_STATUS <> ListEnum.PO.Rejected Then
                @Html.Raw(Labels.ButtonForm("Verified"))
                @Html.Raw(Labels.ButtonForm("RejectedByVerifier"))
                 @<a Class="blue" href="@Url.Action("ActionExportToPdf", "PURCHASE_ORDER", New With {._ID = Model.ID, ._PO_NUMB = Model.PO_NUMBER})" target="_blank" title="Export to pdf">
                    @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                </a>
            End If

            @If Model.PO_STATUS <> ListEnum.PO.Submitted And ViewBag.AsIsAppr.Equals("Reviewer") And ViewBag.StatusAsAppr.Equals("Waiting for review") And Model.PO_STATUS <> ListEnum.PO.Rejected Then
                @Html.Raw(Labels.ButtonForm("Reviewed"))

                 @<a Class="blue" href="@Url.Action("ActionExportToPdf", "PURCHASE_ORDER", New With {._ID = Model.ID, ._PO_NUMB = Model.PO_NUMBER})" target="_blank" title="Export to pdf">
                    @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                </a>
            End If

            @If Model.PO_STATUS <> ListEnum.PO.Submitted And ViewBag.AsIsAppr.Equals("Approver") And ViewBag.StatusAsAppr.Equals("Waiting for approve") And Model.PO_STATUS <> ListEnum.PO.Rejected And Model.PO_STATUS <> ListEnum.PO.Approved And Model.PO_STATUS <> ListEnum.PO.Completed And Model.PO_STATUS <> ListEnum.PO.Closed Then
                @Html.Raw(Labels.ButtonForm("Approved"))
                @Html.Raw(Labels.ButtonForm("RejectedByApprover"))
                 @<a Class="blue" href="@Url.Action("ActionExportToPdf", "PURCHASE_ORDER", New With {._ID = Model.ID, ._PO_NUMB = Model.PO_NUMBER})" target="_blank" title="Export to pdf">
                    @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                </a>
            End If

            @If Model.PO_STATUS = ListEnum.PO.Approved And Session("IS_EPROC_ADMIN") = 1 And Model.PO_STATUS <> ListEnum.PO.Rejected Then
                @Html.Raw(Labels.ButtonForm("Completed"))
            End If

            @If Model.PO_STATUS = ListEnum.PO.Completed And Model.PO_STATUS <> ListEnum.PO.Rejected Then
                @<a Class="blue" href="@Url.Action("ActionExportToPdf", "PURCHASE_ORDER", New With {._ID = Model.ID, ._PO_NUMB = Model.PO_NUMBER})" target="_blank" title="Export to pdf">
                    @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                </a>

                @<a Class="blue" href="@Url.Action("ViewPdf", "PURCHASE_ORDER", New With {.po_number = Model.PO_NUMBER})" target="_blank" title="View Pdf">
                   @Html.Raw(Labels.ButtonForm("ViewPdf"))
                </a>
            End If

            @If Session("IS_EPROC_ADMIN") = 1 And Model.PO_STATUS <> ListEnum.PO.Rejected And Model.PO_STATUS <> ListEnum.PO.Closed Then
                @Html.Raw(Labels.ButtonForm("RejectedByAdminEproc"))
            End If

        </div>
    </div>
</div>

<div Class="warning" id="input_reason_po" style="display:none">
    <div class="col-lg-12">
        <b>Reason of reject :</b>
        <input type="text" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject_po" />
    </div>
</div>

<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/PURCHASING_ORDERController.js"></script>


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