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

    .spr {
        text-align: left;
        width: 100px;
    }
</style>

@Code
    ViewBag.Breadcrumbs = "Purchase Order"
    ViewBag.Title = "Create Purchase Order"
    ViewBag.PurchaseOrder = "active open"
    ViewBag.IndexCreatePO = "active"
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
                    @If Session("IS_EPROC_ADMIN") = 1 Then
                        @<div Class="profile-info-name"> For Storage </div>
                        @<div Class="profile-info-value">
                            <input type="checkbox" id="cb_for_storage" onclick="RefreshScreen()" />
                            <span Class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be pr storage and will be created po, good match, etc">?</span>
                        </div>
                    End If

                    <div class="profile-info-name"> Created Date </div>
                    <div class="profile-info-value">
                        <span Class="" id="txt_created_dt">@Date.Now.ToString("dd-MM-yyyy") </span>
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
                                        <span Class="" id="txt_number"></span> <span Class="hidden" id="txt_base_from">0</span> @*do not erase this. 0 for to know where is base on*@
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <span id="txt_form_type_id" class="hidden"></span>
                                    <div class="profile-info-name required"> Type </div>
                                    <div class="profile-info-value item-required">
                                        <span id="txt_cb_potype_id" class="hidden"></span>
                                        @Html.DropDownList("dropdownList_potyp", Dropdown.POTypeId, New With {.style = "width:  200px;", .onchange = "javascript:GetSubPOType(this.value);"})
                                        <label id="required_txt_cb_potype_id"></label>
                                    </div>
                                </div>
                            </div>
                        </td>

                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Notes : </div>
                                    <div class="profile-info-value">
                                        <span Class="freeText" id="txt_notes"></span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> Currency </div>
                                    <div class="profile-info-value item-required">
                                        <span id="txt_cb_currency_id" class="hidden"></span>
                                        @Html.DropDownList("dropdownList_currency", Dropdown.ListCurrencyId, New With {.style = "width:  200px;", .onchange = "javascript:GetCurrency(this.value);"})
                                        <label id="required_txt_cb_currency_id"></label>
                                    </div>
                                </div>
                            </div>
                        </td>

                        <td width="500px">
                            <div class="profile-user-info">

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
                                <span id="txt_cb_supplier_nm" class="hidden"></span>
                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> Name </div>
                                    <div class="profile-info-value item-required">
                                        <span id="txt_cb_supplier_id" class="hidden"></span>
                                        @Html.DropDownList("dropdownList_supplier_nm", Dropdown.ListSupplierId, New With {.style = "width:  200px;", .onchange = "javascript:GetSupplierNm(this.value);"})
                                        <label id="required_txt_cb_supplier_id"></label>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_phone_supplier"></span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_address_supplier"></span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Contact Person </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_contact_person_supplier"></span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_fax_supplier"></span>
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
                                <span id="txt_cb_delivery_nm" class="hidden"></span>
                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> Name </div>
                                    <div class="profile-info-value item-required">
                                        <span id="txt_cb_delivery_id" hidden="hidden"></span>
                                        @Html.DropDownList("dropdownList_delivery_nm", Dropdown.ListDeliveryId, New With {.style = "width:  200px;", .onchange = "javascript:GetDeliveryNm(this.value);"})
                                        <label id="required_txt_cb_delivery_id"></label>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_phone_delivery"></span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_address_delivery"></span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> Date </div>
                                    <div class="profile-info-value item-required">
                                        <label id="txt_delivery_dt_orin"></label>
                                        <span Class="dateText" id="txt_delivery_dt_new"></span>
                                        <label id="required_txt_delivery_dt_new"></label>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_fax_delivery"></span>
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
                <h4 class="widget-title">Details</h4>
                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table id="table_input" class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Item </th>
                            <th>U/M</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th>Total</th>
                            <th class="">DetailPo</th>
                            <th>ItemId</th>
                            <th>Action</th>
                        </tr>
                    </thead>

                    <tbody id="dataTable">
                        <tr>
                            <td>
                                <input size=50 type="text" id="txt_item_po_" readonly="" style="text-align:left" onclick="getItemPO(this, 0)" />
                            </td>
                            <td><input size=10 type="text" id="txt_um_po_" readonly="readonly" style="text-align:left" /></td>
                            <td><input size=10 type="text" id="txt_quantity_po_" readonly="readonly" style="text-align:left" /></td>
                            <td><input size=10 type="text" id="txt_price_po_" readonly="readonly" style="text-align:left" /></td>
                            <td><input size=15 type="text" id="txt_total_po_" readonly="readonly" style="text-align:right" /></td>
                            <td class=""><input type="text" id="txt_id_detail_po_" readonly="readonly" style="text-align:right" class="" /></td>
                            <td>
                                <input size=10 type="text" id="txt_item_id_po_" readonly="" style="text-align:left" />
                            </td>
                            <td>
                                <a class="red" href="#" onclick="DeleteRow(this)" data-toggle="modal" title="Delete Row">
                                    @Html.Raw(Labels.IconAction("Delete"))
                                </a>
                            </td>
                    </tbody>

                </table>

                @Html.Raw(Labels.ButtonForm("AddRowItemPO"))

            </div>
        </div>
    </div>

    <div class="alert alert-danger" id="msg_error" style="display:none">
        <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
            <i class="ace-icon fa fa-times"></i>
        </button>
        msg error
        <br />
    </div>

    <div class="hr hr-18 dotted hr-double"></div>


    <div class="col-sm-6">
        <table id="apprvol_group_table">
            <tr>
                <td>Prepared By </td>
                <td>: &nbsp; &nbsp;</td>
                <td class="spr"><span Class="" id="txt_prepared_by_po">@Session("USER_ID")</span></td>
                <td class="spr"><span Class="" id="txt_prepared_dt">@Date.Now.ToString("dd-MM-yyyy")</span></td>
            </tr>
            @*<tr>
                    <td> Approved By </td>
                    <td>: &nbsp; &nbsp;</td>
                    <td class="spr"><span Class="" id="txt_approved_by"></span></td>
                    <td class="spr"><span Class="" id="txt_approved_dt"></span></td>
                </tr>
                <tr>
                    <td>Handle By</td>
                    <td>: &nbsp; &nbsp;</td>
                    <td class="spr"><span Class="" id="txt_handle_by"></span></td>
                    <td class="spr"><span Class="" id="txt_handle_dt"></span></td>
                </tr>
                <tr>
                    <td> Completed By </td>
                    <td>: &nbsp; &nbsp;</td>
                    <td class="spr"><span Class="" id="txt_completed_by"></span></td>
                    <td class="spr"><span Class="" id="txt_completed_dt"></span></td>
                </tr>*@
        </table>

    </div>

    <div class="col-sm-6">
        <div class="row">
            <div class="col-xs-6" style="text-align:right">
                Sub Total :
            </div>

            <div class="col-xs-6" style="font-weight:bold">
                <div style="">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Rp. <input id="txt_sub_total" text="text" style="text-align:right" readonly='readonly' />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6" style="text-align:right">
                Discount
                <label>
                    <input name="disc" type="radio" class="ace" id="rb_disc_perc" />
                    <span class="lbl">perc(%)</span>
                </label>
                <label>
                    <input name="disc" type="radio" class="ace" id="rb_disc_rp" />
                    <span class="lbl">exact(Rp)</span>
                </label>
            </div>
            <div class="radio">

            </div>
            <div class="col-xs-6" style="font-weight:bold">
                <div>
                    <input size=6 id="txt_disc_per_" text="text" style="text-align:right" onkeyup="CalcDiscPo(this)" />
                    <input id="txt_discount" text="text" style="text-align:right" readonly='readonly' />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6" style="text-align:right">
                VAT
                <label>
                    <input name="vat" type="radio" class="ace" id="rb_vat_perc" />
                    <span class="lbl">perc(%)</span>
                </label>
                <label>
                    <input name="vat" type="radio" class="ace" id="rb_vat_rp" />
                    <span class="lbl">exact(Rp)</span>
                </label>
            </div>

            <div class="col-xs-6" style="font-weight:bold">
                <div>
                    <input size=6 id="txt_vat_per_" text="text" onkeyup="CalcVatPo(this)" style="text-align:right" />
                    <input id="txt_vat" text="text" style="text-align:right" readonly='readonly' />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6" style="text-align:right">
                Wth tax / PPH
                <label>
                    @*<input name="pph" type="radio" class="ace" id="rb_pph_perc" />*@
                    @*<span class="lbl">perc(%)</span>*@
                    <span class="lbl">--</span>
                </label>
                <label>
                    <input name="pph" type="radio" class="ace" id="rb_pph_rp" />
                    <span class="lbl">exact(Rp)</span>
                </label>
            </div>

            <div class="col-xs-6" style="font-weight:bold">
                <div>
                    <input size=6 id="txt_pph_per_" text="text" onkeyup="CalcPphPo(this)" style="text-align:right" />
                    <input id="txt_wth_tax_pph" text="text" style="text-align:right" readonly='readonly' />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6" style="text-align:right">
                <button class='btn btn-sm btn-success btn-white btn-round' onclick="CalcGrandTotalPO()" type='submit'>
                    <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                    Grand Total :
                </button>
            </div>

            <div class="col-xs-6" style="font-weight:bold;">
                <div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Rp. <input id="txt_grand_total" text="text" style="text-align:right" readonly='readonly' />
                </div>
            </div>
        </div>

        <div class="col-xs-12" style="text-align:right">
            @*<a href="#"><i class='ace-icon fa fa-plus icon-on-right bigger-110' onclick="ShowAddCost()" title="click for additional cost"></i></a>*@            
        </div>

        <div id="input_add_cost" style="display:none">
            <div class="row">
                <div class="col-xs-6" style="text-align:right">
                    Additional Cost for
                    <input size=15 id="txt_add_cost_desc" text="text" placeholder="input description additional" />
                </div>

                <div class="col-xs-6" style="font-weight:bold">
                    <div>
                        <input size=6 id="txt_add_cost_per_" text="text" onkeyup="CalcAddCostPo(this)" style="text-align:right" />
                        <input id="txt_add_cost" text="text" style="text-align:right" readonly='readonly' />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-6" style="text-align:right">
                    <button class='btn btn-sm btn-success btn-white btn-round' onclick="CalcFinalTotalPO()" type='submit'>
                        <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                        Final Total :
                    </button>
                </div>

                <div class="col-xs-6" style="font-weight:bold;">
                    <div>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Rp. <input id="txt_final_total" text="text" style="text-align:right" readonly='readonly' />
                    </div>
                </div>
            </div>
        </div>
       

    </div>

    <div class="hr hr-18 dotted hr-double"></div>

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                <a class="red" href="@Url.Action("Create", "PURCHASE_ORDER")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                @Html.Raw(Labels.ButtonForm("Submit"))
            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/PURCHASING_ORDERController.js"></script>

