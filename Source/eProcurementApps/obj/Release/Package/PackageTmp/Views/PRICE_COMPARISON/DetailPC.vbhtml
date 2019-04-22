@ModelType eProcurementApps.Models.TPROC_PC
@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Models

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

    .accordion:hover {
      cursor: pointer;
    }
   
    .hidden-row td {
      border: none;
    }
</style>

@Code
    ViewBag.Breadcrumbs = "Price Comparison"
    ViewBag.Title = "Detail Price Comparison"
    ViewBag.PriceComparison = "active open"
    ViewBag.IndexListPriceCom = "active"
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

<div Class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">PriceCom</h4> 
                <span id="txt_pc_id" class="hidden">@Model.ID</span>
                <div style="float: right;">
                    <div Class="profile-info-name"> For Storage </div>
                    <div Class="profile-info-value">
                        @if Model.FOR_STORAGE = 1 Then
                            @<input type="checkbox" id="cb_for_storage" checked="checked" disabled="disabled" />
                        Else
                            @<input type="checkbox" id="cb_for_storage" disabled="disabled" />
                        End If

                        <span Class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be pr storage and will be good match, etc">?</span>
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
                                    <div class="profile-info-name"> PC. Number : </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_number">@Model.PC_NUM</span> <span Class="hidden" id="txt_base_from">1</span> @*do not erase this. 1 for to know where is base on*@
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Type </div>
                                    <div class="profile-info-value">
                                        <span id="txt_potype_nm">@Model.PO_TYPE_NM</span>
                                        @*<span id="txt_cb_potype_id" hidden=""></span>*@
                                        @*@Html.DropDownList("dropdownList_potyp", Dropdown.POTypeId, New With {.style = "width:  200px;", .onchange = "javascript:GetSubPOType(this.value);"})*@
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Notes : </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_notes">@Model.NOTES</span>
                                    </div>
                                </div>
                            </div>
                        </td>

                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> PO. Nunmber </div>
                                    <div class="profile-info-value">
                                        <span id="txt_po_number"><a href="@Url.Action("DetailHeaderOnlyView", "PURCHASE_ORDER", New With {.id = ViewBag.POID})" target="_blank">@ViewBag.PONumber</a> </span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name "> Currency </div>
                                    <div class="profile-info-value ">
                                        <span id="txt_currency">@Model.CURRENCY</span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name "> Status </div>
                                    <div class="profile-info-value">
                                        <span id="txt_status">@Model.STATUS</span> 
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
                                    <div class="profile-info-name "> Name </div>
                                    <div class="profile-info-value">
                                        <span id="txt_cb_delivery_id">@Model.DELIVERY_ID</span>
                                        <span id="txt_cb_delivery_nm">@Model.DELIVERY_NM</span>
                                        @*@Html.DropDownList("dropdownList_delivery_nm", Dropdown.ListDeliveryId, New With {.style = "width:  200px;", .onchange = "javascript:GetDeliveryNm(this.value);"})*@
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_phone_delivery">@Model.DELIVERY_PHONE</span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_address_delivery">@Model.DELIVERY_ADDRESS</span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Date </div>
                                    <div class="profile-info-value">
                                        <label hidden="hidden" id="txt_delivery_dt_orin"></label>
                                        <span Class="" id="txt_delivery_dt_new">@Model.DELIVERY_DATE.ToString("dd-MM-yyyy")</span>
                                        @*<label id="required_txt_delivery_dt_new"></label>*@
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_fax_delivery">@Model.DELIVERY_FAX</span>
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

    <div class="space-4"></div>

    <div Class="clearfix">
        <div Class="pull-right tableTools-container"></div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Details</h4>
                @*<div style="float: right;">
                        <a class="blue" href="#" onclick="addColumn('table_input')" id="btn_add_col" title="Add column">
                            <i class='ace-icon fa fa-plus bigger-130'></i>
                        </a>
                        <a class="red" href="#" onclick="deleteColumn('table_input')" id="btn_del_col" title="Delete column">
                            <i class='ace-icon fa fa-minus bigger-130'></i>
                        </a>
                    </div>*@

                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table id="table_input" class="table table-striped table-bordered table-hover research">
                    <thead>
                        <tr id="headeTable">
                            <th class="">No</th>
                            <th>Item </th>
                            <th>U/M</th>
                            <th>Qty</th>
                            <th>Price</th>
                            <th>Total</th>
                            <th class="hidden">Detail</th>

                            @code
                                For Each item In Model.TPROC_PC_SP_GR
                                    @<th>
                                        <span id="txt_cb_supplier_id" class="fhead hidden">@item.SUPP_ID</span>
                                        <span id="txt_supplier_nm" class="fhead">@item.SUPP_NM</span>
                                        @if item.IS_USED = 1 Then
                                            @<input type="checkbox" id="txt_ck_supplier" onclick="" Class="fhead" checked="checked" disabled />
                                        Else
                                            @<input type="checkbox" id="txt_ck_supplier" onclick="" Class="fhead" disabled />
                                        End If

                                    </th>
                                Next

                            End Code

                        </tr>
                    </thead>

                    <tbody id="dataTable">
                        @code
                            Dim lPc_dt As List(Of TPROC_PC_DT) = CommonFunction.GetPcDtByPcId(Model.ID)
                            Dim no As Integer = 1
                            For Each item In lPc_dt
                                @<tr >
                                    <td class="">@no</td>
                                    <td><input value="@item.ITEM_NAME" size=10 type="text" id="txt_item_po_" name="test1" readonly="readonly" style="text-align:left" onclick="" class="fbody" /></td>
                                    <td><input value="@item.UNIT_MEASUREMENT" size=5 type="text" id="txt_um_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                                    <td><input value="@item.QUANTITY" size=2 type="text" id="txt_quantity_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                                    <td><input value="@item.PRICE.ToString("###,###")" size=5 type="text" id="txt_price_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                                    <td><input value="@item.TOTAL.Value.ToString("###,###")" size=5 type="text" id="txt_total_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                                    <td class="hidden"><input type="text" id="txt_id_detail_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>

                                    @for Each item_price_dt In item.TPROC_PC_SP_DT
                                        @<td>
                                            <table>
                                                <tr>
                                                    <td><input value="@item_price_dt.PRICE_PER_PIECE.ToString("###,###")" size=6 type='text' id='txt_each_price_' onkeyup='' style='text-align:right' placeholder='each price' class="fbody" readonly="readonly"/></td>
                                                    <td><input value="@item_price_dt.PRICE_TOTAL.ToString("###,###")" size=10 type='text' id='txt_total_price_' style='text-align:right' placeholder='total price' readonly='readonly' class="fbody" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    Next
                                </tr>
                                no += 1
                            Next

                        End Code
                        
                    </tbody>

                    <tfoot id="footTable">
                        <tr>
                            <td colspan="6" align="right">
                                <table>
                                    <tr>
                                        <td>Sub Total</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="radio">
                                                Disc
                                                @if Model.IS_DISC_PERC Then
                                                    @<label>
                                                        <input name="disc" type="radio" Class="ace" id="rb_disc_perc" checked="checked" />
                                                        <span Class="lbl">perc(%)</span>
                                                    </label>
                                                Else
                                                    @<Label>
                                                        <input name="disc" type="radio" Class="ace" id="rb_disc_rp" checked="checked" />
                                                        <span Class="lbl">exact(Rp)</span>
                                                    </Label>
                                                End If


                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div Class="radio">
                                                Vat
                                                @if Model.IS_VAT_PERC Then
                                                    @<Label>
                                                        <input name="vat" type="radio" Class="ace" id="rb_vat_perc" checked="checked" />
                                                        <span Class="lbl">perc(%)</span>
                                                    </Label>
                                                Else
                                                    @<Label>
                                                        <input name="vat" type="radio" Class="ace" id="rb_vat_rp" checked="checked" />
                                                        <span Class="lbl">exact(Rp)</span>
                                                    </Label>
                                                End If

                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div Class="radio">
                                                Pph

                                                @if Model.IS_PPH_PERC Then
                                                    @<Label>
                                                        <input name="pph" type="radio" Class="ace" id="rb_pph_perc" checked="checked" />
                                                        <span Class="lbl">perc(%)</span>
                                                    </Label>
                                                Else
                                                    @<Label>
                                                        <input name="pph" type="radio" Class="ace" id="rb_pph_rp" checked="checked" />
                                                        <span Class="lbl">exact(Rp)</span>
                                                    </Label>
                                                End If

                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <button class="btn btn-sm btn-success btn-white btn-round accordion" onclick="Accourdionclick()">
                                                Show row description
                                            </button>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            @For Each item In Model.TPROC_PC_SP_GR
                                @<td>
                                    <Table>
                                        <tr>
                                            <td> Sub Total</td>
                                            <td><input value="@item.SUB_TOTAL.Value.ToString("###,###")" size=10 type='text' id='txt_sub_total_' style='text-align:right' placeholder='sub total' readonly='readonly' class="ffood" /> </td>
                                        </tr>
                                        <tr>
                                            <td><input value="@item.DISCOUNT_TEMP.Value.ToString("###,###")" size=6 type='text' id='txt_disc_per_' onkeyup='' style='text-align:right' placeholder='discount' class="ffood" readonly="readonly" /></td>
                                            <td><input value="@item.DISCOUNT.Value.ToString("###,###")" size=10 type='text' id='txt_disc_rp_' style='text-align:right' placeholder='discount Rp' readonly='readonly' class="ffood" /> </td>
                                        </tr>
                                        <tr>
                                            <td><input value="@item.VAT_TEMP.Value.ToString("###,###")" size=6 type='text' id='txt_vat_per_' onkeyup='' style='text-align:right' placeholder='vat' class="ffood" readonly="readonly" /></td>
                                            <td><input value="@item.VAT.Value.ToString("###,###")" size=10 type='text' id='txt_vat_rp_' style='text-align:right' placeholder='vat Rp' readonly='readonly' class="ffood" /> </td>
                                        </tr>
                                        <tr>
                                            <td><input value="@item.PPH_TEMP.Value.ToString("###,###")" size=6 type='text' id='txt_pph_per_' onkeyup='' style='text-align:right' placeholder='pph' class="ffood" readonly="readonly" /></td>
                                            <td><input value="@item.PPH.Value.ToString("###,###")" size=10 type='text' id='txt_pph_rp_' style='text-align:right' placeholder='pph Rp' readonly='readonly' class="ffood" /> </td>
                                        </tr>
                                        <tr>
                                            <td><b>Grand Total</b></td>
                                            <td><input value="@item.GRAND_TOTAL.ToString("###,###")" size=10 type='text' id='txt_grand_total_' style='text-align:right' placeholder='grand total' readonly='readonly' class="ffood" /> </td>
                                        </tr>
                                    </Table>
                                </td>
                            Next
                        </tr>

                        <tr class="hidden-row">
                            <td colspan="6" align="right"></td>
                            @for Each itemx In Model.TPROC_PC_SP_GR
                                @<td>
                                    <textarea id='txt_desc' name='textarea' style='width:200px;height:80px;' readonly>@itemx.DESCRIPTION</textarea>
                                </td>
                            Next
                           
                       </tr>
                    </tfoot>

                </table>

                <div Class="space-4"></div>

                <div Class="row">
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

                    <div class="col-sm-6">
                        <div class="profile-info-row">
                            <div class="profile-info-name">Comments (User) : </div>
                            <div class="profile-info-value">
                                <span Class="" id="txt_note_by_user">@Model.NOTE_BY_USER</span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name">Comments (Procurement Dept.) :</div>
                            <div class="profile-info-value">
                                <span Class="" id="txt_note_by_eproc">@Model.NOTE_BY_EPROC</span>
                            </div>
                        </div>
                    </div>

                    <div Class="col-sm-6">
                        <h3><b>Recommendation Supplier</b></h3>
                        <div Class="profile-info-row">
                            <div Class="profile-info-name">Name</div>
                            <div Class="profile-info-value">
                                <span Class="" id="txt_cb_supplier_nm">@Model.RECOM_SUPPLIER_NM</span>
                                <span Class="hidden" id="txt_supp_id_recomm">@Model.RECOM_SUPPLIER_ID</span>
                            </div>
                        </div>
                        <div Class="profile-info-row">
                            <div Class="profile-info-name">Address</div>
                            <div Class="profile-info-value">
                                <span Class="" id="txt_address_supplier">@Model.RECOM_SUPPLIER_ADDRESS</span>
                            </div>
                        </div>
                        <div Class="profile-info-row">
                            <div Class="profile-info-name">Phone</div>
                            <div Class="profile-info-value">
                                <span Class="" id="txt_phone_supplier">@Model.RECOM_SUPPLIER_PHONE</span>
                            </div>
                        </div>
                        <div Class="profile-info-row">
                            <div Class="profile-info-name">Fax</div>
                            <div Class="profile-info-value">
                                <span Class="" id="txt_fax_supplier">@Model.RECOM_SUPPLIER_FAX</span>
                            </div>
                        </div>
                        <div Class="profile-info-row">
                            <div Class="profile-info-name">Contact Person</div>
                            <div Class="profile-info-value">
                                <span Class="" id="txt_contact_person_supplier">@Model.RECOM_SUPPLIER_CP</span>
                            </div>
                        </div>

                        @code
                            Dim supp_selected = Model.TPROC_PC_SP_GR.Where(Function(x) x.IS_USED = 1).FirstOrDefault()

                            @<div Class="profile-info-row">
                                <div Class="profile-info-name">Sub Total</div>
                                <div Class="profile-info-value">
                                    <div class="col-xs-1" style="text-align:left">
                                        Rp.
                                    </div>
                                    <div class="col-xs-2" style="text-align:right">
                                        <span>@supp_selected.SUB_TOTAL.Value.ToString("###,###")</span>
                                    </div>
                                </div>
                            </div>
                            @<div Class="profile-info-row">
                                <div Class="profile-info-name">Discount</div>
                                <div Class="profile-info-value">
                                    <div class="col-xs-1" style="text-align:left">
                                        Rp.
                                    </div>
                                    <div class="col-xs-2" style="text-align:right">
                                        <span>@supp_selected.DISCOUNT.Value.ToString("###,###")</span>
                                    </div>
                                </div>
                            </div>
                            @<div Class="profile-info-row">
                                <div Class="profile-info-name">Vat</div>
                                <div Class="profile-info-value">
                                    <div class="col-xs-1" style="text-align:left">
                                        Rp.
                                    </div>
                                    <div class="col-xs-2" style="text-align:right">
                                        <span>@supp_selected.VAT.Value.ToString("###,###")</span>
                                    </div>
                                </div>
                            </div>
                            @<div Class="profile-info-row">
                                <div Class="profile-info-name">Pph</div>
                                <div Class="profile-info-value">
                                    <div class="col-xs-1" style="text-align:left">
                                        Rp.
                                    </div>
                                    <div class="col-xs-2" style="text-align:right">
                                        <span>@supp_selected.PPH.Value.ToString("###,###")</span>
                                    </div>
                                </div>
                            </div>
                        End Code

                        <div Class="profile-info-row">
                            <div Class="profile-info-name">Grand Total</div>
                            <div Class="profile-info-value"> 
                                <div class="col-xs-1" style="text-align:left">
                                    Rp.
                                </div>
                                <div class="col-xs-2" style="text-align:right">
                                    <span>@Model.GRAND_TOTAL.ToString("###,###")</span>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

    <div Class="space-4"></div>

    @code
        @if Model.STATUS = ListEnum.PriceCom.Submitted.ToString()  Then
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
                            @*@if Model.STATUS = ListEnum.PriceCom.Verified.ToString() Then
                                @If Model.TPROC_VRF_PC IsNot Nothing Then
                                    @<ul>
                                        @for Each item In Model.TPROC_VRF_PC

                                            If item.STATUS = ListEnum.PriceCom.Verified.ToString() Then
                                                @<li>
                                                    @item.NAME - @item.STATUS
                                                </li>
                                            End If
                                        Next
                                    </ul>
                                End If
                            Else
                                @If Model.TPROC_VRF_PC IsNot Nothing Then
                                    @<ul>
                                        @for Each item In Model.TPROC_VRF_PC
                                            @<li>
                                                @item.NAME - @item.STATUS
                                            </li>
                                        Next
                                    </ul>
                                End If
                            End If*@

                            <ul>
                                @for Each item In Model.TPROC_VRF_PC
                                    @<li>
                                        @item.NAME - @item.STATUS
                                    </li>
                                Next
                            </ul>

                        </div>
                    </div>
                </div>
            </div>
        End If

        @if Model.STATUS = ListEnum.PriceCom.Submitted.ToString() Or Model.STATUS = ListEnum.PriceCom.Verified.ToString() Then
            @<div class="col-sm-12">
              <div class="widget-box widget-color-orange ui-sortable-handle collapsed">
                    <div class="widget-header widget-header-small">
                        <h6 class="widget-title">
                            Acknowledge User
                            @If Model.IS_ACKNOWLEDGE_USER = "1" Then
                                @<input type="checkbox" onclick="CheckAcknowledgeUser()" id="cb_is_acknowledge_user" checked="checked" disabled="disabled" />
                            Else
                                @<input type="checkbox" onclick="CheckAcknowledgeUser()" id="cb_is_acknowledge_user" disabled="disabled" />
                            End If
                            <span Class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be need approve by acknowledge user before this goes to approval of related procurement">?</span>
                        </h6>
                        <div class="widget-toolbar">
                            <a href="#" data-action="collapse">
                                <i class="ace-icon fa fa-chevron-down" data-icon-show="fa-chevron-down" data-icon-hide="fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="widget-body" style="display: none;">
                        <div class="widget-main">
                            @code
                                Dim lacknow_appr As New List(Of TPROC_ACKNOW_APPR)
                                Dim status As String = ""
                                If Model.IS_ACKNOWLEDGE_USER = "1" Then
                                    lacknow_appr = CommonFunction.GetAknowApprByPcId(Model.ID)
                                    For Each item In lacknow_appr
                                        @<div>
                                            <ul>
                                                Wa number : @item.WA_NUMBER
                                                @For Each itemx In item.TPROC_ACKNOW_APPR_DT
                                                    @<li>
                                                        @*@[Enum].GetName(GetType(ListEnum.PriceCom), Int32.Parse(item.STATUS)).ToString()*@
                                                        @itemx.USER_NAME - @itemx.STATUS
                                                    </li>
                                                Next
                                            </ul>
                                        </div>
                                    Next
                                End If
                            End Code

                        </div>
                    </div>
                </div>
            </div>
         End If

        @if Model.STATUS = ListEnum.PriceCom.Submitted.ToString() Or Model.STATUS = ListEnum.PriceCom.Verified.ToString() Or Model.STATUS = ListEnum.PriceCom.ApprovedByAcknowledge.ToString() Then
            @<div class="col-sm-12">
                <div class="widget-box widget-color-orange ui-sortable-handle collapsed">
                    <div class="widget-header widget-header-small">
                        <h6 class="widget-title">
                            Approval
                        </h6>

                        <div style="float: right;">
                            @if Model.STATUS = ListEnum.PriceCom.Verified.ToString() Or Model.STATUS = ListEnum.PriceCom.ApprovedByAcknowledge.ToString() Then
                                @Html.Raw(Labels.ButtonForm("PushEmailByPC"))
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
                            @*@if Model.STATUS = ListEnum.PriceCom.Approved.ToString() Then
                                @If Model.TPROC_APPR_PC IsNot Nothing Then
                                    @<ul>
                                        @for Each item In Model.TPROC_APPR_PC

                                            If item.STATUS = ListEnum.PriceCom.Reviewed.ToString() Then
                                                @<li>
                                                    @item.NAME - @item.STATUS
                                                </li>
                                            End If

                                            If item.STATUS = ListEnum.PriceCom.Approved.ToString() Then
                                                @<li>
                                                    @item.NAME - @item.STATUS
                                                </li>
                                            End If

                                        Next
                                    </ul>
                                End If
                            Else
                                @If Model.TPROC_APPR_PC IsNot Nothing Then
                                    @<ul>
                                        @for Each item In Model.TPROC_APPR_PC
                                            @<li>
                                                @item.NAME - @item.STATUS
                                            </li>
                                        Next
                                    </ul>
                                End If
                            End If*@
                            <ul>
                                @For Each item In Model.TPROC_APPR_PC
                                    @<li>
                                        @item.NAME - @item.STATUS
                                    </li>
                                Next
                            </ul>
                    
                        </div>
                    </div>
                </div>
            </div>
        End If
    End Code
          

    <div class="alert alert-danger" id="msg_error" style="display:none">
        <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
            <i class="ace-icon fa fa-times"></i>
        </button>
        msg error
        <br />
    </div>

    <div class="hr hr-18 dotted hr-double"></div>

    <div class="widget-body">
        <div class="widget-main">
            <div id="fuelux-wizard-container">
                <div>
                    <ul class="steps">
                        @For Each item In Model.TPROC_PC_HISTORICAL.OrderBy(Function(x) x.CREATED_TIME)
                        @<li Class="active">
                            @If item.HISTORICAL_STATUS = ListEnum.PriceCom.Rejected.ToString() Then
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
    
    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                @If (Session("IS_EPROC_ADMIN") = 1 Or (ViewBag.FlagInbox <> ListEnum.FlagInbox.ApprAckPC And ViewBag.FlagInbox <> ListEnum.FlagInbox.ApprPC And ViewBag.FlagInbox <> ListEnum.FlagInbox.VrfPC)) And ViewBag.ViewOnly = "" Then
                    @<a Class="red" href="@Url.Action("_ListPC", "PRICE_COMPARISON")" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                End If                

                @if ViewBag.FlagInbox = ListEnum.FlagInbox.ApprAckPC And Model.STATUS <> ListEnum.PriceCom.Rejected.ToString() Then
                    @<a Class="blue" href="@Url.Action("ApproveAcknowledge", "PRICE_COMPARISON", New With {.pc_id = Model.ID, .pc_dt = Model.CREATED_TIME, .user_id = Session("USER_ID"), .grand_total = Model.GRAND_TOTAL})" title="Approve Acknowledge">
                        @Html.Raw(Labels.ButtonForm("Approve"))
                    </a>
                ElseIf ViewBag.FlagInbox = ListEnum.FlagInbox.ApprPC And Model.STATUS <> ListEnum.PriceCom.Rejected.ToString() Then

                    Dim asIsStatus = Facade.PriceComparisonFacade.GetStatusApprovalAs(Model.ID, Session("USER_ID").ToString())
                    If asIsStatus = "Waiting for review" Then
                        @<a Class="blue" href="@Url.Action("Review", "PRICE_COMPARISON", New With {.pc_id = Model.ID, .pc_dt = Model.CREATED_TIME, .user_id = Session("USER_ID").ToString(), .status = ListEnum.PriceCom.Reviewed.ToString()})" title="Review">
                            @Html.Raw(Labels.ButtonForm("Review"))
                        </a>

                         @<a Class="red" href="#" onclick="ActionRejectPC('RejectByReviewer')" title="Reject By Reviewer">
                            @Html.Raw(Labels.ButtonForm("RejectByReviewer"))
                        </a>
                    ElseIf asIsStatus = "Waiting for approve" And Model.STATUS <> ListEnum.PriceCom.Approved.ToString() Then
                        @<a Class="blue" href="@Url.Action("Approve", "PRICE_COMPARISON", New With {.pc_id = Model.ID, .pc_dt = Model.CREATED_TIME, .user_id = Session("USER_ID").ToString(), .status = ListEnum.PriceCom.Approved.ToString()})" title="Approve">
                            @Html.Raw(Labels.ButtonForm("Approve"))
                        </a>

                         @<a Class="red" href="#" onclick="ActionRejectPC('RejectedByApprover')" title="Reject By Approver">
                            @Html.Raw(Labels.ButtonForm("RejectByApprover"))
                        </a>
                    End If
                ElseIf ViewBag.FlagInbox = ListEnum.FlagInbox.VrfPC And Model.STATUS <> ListEnum.PriceCom.Rejected.ToString() And Model.STATUS <> ListEnum.PriceCom.Verified.ToString() Then
                     @<a Class="blue" href="@Url.Action("Verify", "PRICE_COMPARISON", New With {.pc_id = Model.ID, .pc_dt = Model.CREATED_TIME, .user_id = Session("USER_ID").ToString(), .status = ListEnum.PriceCom.Verified.ToString()})" title="Verify">
                            @Html.Raw(Labels.ButtonForm("Verify"))
                        </a>
                         @<a Class="red" href="#" onclick="ActionRejectPC('RejectedByVerifier')" title="Reject By Verifier">
                            @Html.Raw(Labels.ButtonForm("RejectByVerifier"))
                        </a>

                End If

                @if Model.STATUS = ListEnum.PriceCom.Approved.ToString() And Session("IS_EPROC_ADMIN") = 1 And Model.STATUS <> ListEnum.PriceCom.Rejected.ToString() Then
                    @<a Class="blue" href="@Url.Action("Complete", "PRICE_COMPARISON", New With {.pc_id = Model.ID, .pc_dt = Model.CREATED_TIME, .user_id = Session("USER_ID").ToString(), .status = ListEnum.PriceCom.Completed.ToString()})" title="Complete">
                        @Html.Raw(Labels.ButtonForm("Completing"))
                    </a>
                End If


                @If Session("IS_EPROC_ADMIN") = 1 And Model.STATUS <> ListEnum.PriceCom.Rejected.ToString() Then
                    @<a Class="red" href="#" onclick="ActionRejectPC('RejectedByEproc')" title="Reject By Admin Eproc">
                        @Html.Raw(Labels.ButtonForm("RejectByEproc"))
                    </a>
                End If
               

            </div>
        </div>
    </div>
</div>

@If Model.STATUS.Equals(ListEnum.PriceCom.Rejected.ToString()) Then
    @<b>Reason Of reject :</b>
    @<span>@Model.REJECT_REASON</span>
End If

<div Class="warning" id="input_reason_pc" style="display:none">
    <div class="col-lg-12">
        <b>Reason of reject :</b>
        <input type="text" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject_pc" />
    </div>
</div>


<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/PRICE_COMPARISONController.js"></script>
<script src="~/Scripts/Controllers/PURCHASING_ORDERController.js"></script>

