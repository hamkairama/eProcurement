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



    /*table, td, th {
      border: 1px solid black;
    }*/
    /*tr:hover {
      background: #ddd;
    }*/
    .accordion:hover {
      cursor: pointer;
    }
    /*table {
      width: 100%;
    }*/
    .hidden-row td {
      border: none;
    }


</style>

@Code
    ViewBag.Breadcrumbs = "Price Comparison"
    ViewBag.Title = "Create Price Comparison"
    ViewBag.PriceComparison = "active open"
    ViewBag.IndexCreateComparison = "active"
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
                <h4 class="widget-title">PriceCom</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
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
                        <span Class="hidden" id="txt_prepared_by_po">@Session("USER_ID")</span>
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
                                        <span Class="" id="txt_number"></span> <span Class="hidden" id="txt_base_from">1</span> @*do not erase this. 1 for to know where is base on*@
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

                                @*<div class="profile-info-row">
                                    <div class="profile-info-name"> . </div>
                                    <div class="profile-info-value">
                                      .
                                    </div>
                                </div>*@

                                @*<div class="profile-info-row">
                                    <div class="profile-info-name required"> Status </div>
                                    <div class="profile-info-value item-required">
                                        <span id="txt_cb_status_id" class="hidden"></span>
                                        @Html.DropDownList("dropdownList_status", Dropdown.SetStatusSubmitPoPc, New With {.style = "width:  200px;", .onchange = "javascript:GetStatus(this.value);"})
                                        <label id="required_txt_cb_status_id"></label>
                                    </div>
                                </div>*@
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
                                <span id="txt_cb_delivery_nm" hidden="hidden"></span>
                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> Name </div>
                                    <div class="profile-info-value item-required">
                                        <span id="txt_cb_delivery_id" hidden=""></span>
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
                                    <div class="profile-info-name"> Date </div>
                                    <div class="profile-info-value">
                                        <label hidden="hidden" id="txt_delivery_dt_orin"></label>
                                        <span Class="dateText" id="txt_delivery_dt_new"></span>
                                        @*<label id="required_txt_delivery_dt_new"></label>*@
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

    <div class="space-4"></div>

    <div Class="clearfix">
        <div Class="pull-right tableTools-container"></div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Details</h4>
                <div class="widget-toolbar">
                    <a class="blue" href="#" onclick="addColumn('table_input')" id="btn_add_col" title="Add column">
                        <i class='ace-icon fa fa-plus bigger-130'></i>
                    </a>
                    <a class="red" href="#" onclick="deleteColumn('table_input')" id="btn_del_col" title="Delete column">
                        <i class='ace-icon fa fa-minus bigger-130'></i>
                    </a>
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
                <div style="float: right;">

                </div>
            </div>
            <div class="widget-body" id="">
                <table id="table_input" class="table table-striped table-bordered table-hover research">
                    <thead>
                        <tr id="headeTable">
                            <th style="width:2px">Act</th>
                            <th>Item </th>
                            <th>U/M</th>
                            <th>Qty</th>
                            <th>Price</th>
                            <th>Total</th>
                            <th class="hidden">Detail</th>
                            <th>
                                <span id="txt_cb_supplier_id" class="fhead" style="display:none"></span>
                                @*dont use PO function. use new function. allright*@
                                @*@Html.DropDownList("dropdownList_supplier_nm", Dropdown.ListSupplierId, New With {.style = "width:  160px;", .onchange = "javascript:GetSubPOType(this.value);"})*@
                                @Html.DropDownList("dropdownList_supplier_nm_", Dropdown.Supplier, New With {.style = "width:  160px;", .onchange = "GetSupplierList(this)", .class = "fhead"})
                                <input type="checkbox" id="txt_ck_supplier" onclick="GetCheckSupplier(this)" class="fhead" />
                            </th>
                        </tr>
                    </thead>

                    <tbody id="dataTable">
                        <tr>
                            <td style="width:2px">
                                <a class="red" href="#" onclick='DeleteRow(this)' title="Delete Row">
                                    @Html.Raw(Labels.IconAction("Delete"))
                                </a>
                                <input size=1 type="text" id="txt_item_id_po_" readonly="readonly" style="text-align:left;display:none" class="fbody" />
                            </td>
                            <td><input size=10 type="text" id="txt_item_po_" name="test1" readonly="readonly" style="text-align:left" onclick="getItemPO(this, 1)" class="fbody" /></td>
                            <td><input size=5 type="text" id="txt_um_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                            <td><input size=2 type="text" id="txt_quantity_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                            <td><input size=5 type="text" id="txt_price_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                            <td><input size=5 type="text" id="txt_total_po_" readonly="readonly" style="text-align:left" class="fbody" /></td>
                            <td hidden="hidden"><input type="text" id="txt_id_detail_po_" readonly="readonly" style="text-align:left" hidden="hidden" class="fbody" /></td>
                            <td>
                                <table>
                                    <tr>
                                        <td><input size=6 type='text' id='txt_each_price_' onkeyup='CalcTotalPC(this)' style='text-align:right' placeholder='each price' class="fbody" /></td>
                                        <td><input size=10 type='text' id='txt_total_price_' style='text-align:right' placeholder='total price' readonly='readonly' class="fbody" /> </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
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
                                                <label>
                                                    <input name="disc" type="radio" class="ace" id="rb_disc_perc" />
                                                    <span class="lbl">perc(%)</span>
                                                </label>
                                                <label>
                                                    <input name="disc" type="radio" class="ace" id="rb_disc_rp" />
                                                    <span class="lbl">exact(Rp)</span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="radio">
                                                <label>
                                                    <input name="vat" type="radio" class="ace" id="rb_vat_perc" />
                                                    <span class="lbl">perc(%)</span>
                                                </label>
                                                <label>
                                                    <input name="vat" type="radio" class="ace" id="rb_vat_rp" />
                                                    <span class="lbl">exact(Rp)</span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="radio">
                                                <label>
                                                    @*<input name="pph" type="radio" class="ace" id="rb_pph_perc" />*@
                                                    @*<span class="lbl">perc(%)</span>*@
                                                </label>
                                                <label>
                                                    <input name="pph" type="radio" class="ace" id="rb_pph_rp" />
                                                    <span class="lbl">exact(Rp)</span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <button class="btn btn-sm btn-success btn-white btn-round accordion" onclick="Accourdionclick()">
                                                Show row description
                                            </button>
                                            <button class='btn btn-sm btn-success btn-white btn-round' onclick="CalcGrandTotal()" type='submit'>
                                                <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                                                Grand Total
                                            </button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>Sub Total</td>
                                        <td><input size=10 type='text' id='txt_sub_total_' style='text-align:right' placeholder='sub total' readonly='readonly' class="ffood" /> </td>
                                    </tr>
                                    <tr>
                                        <td><input size=6 type='text' id='txt_disc_per_' onkeyup='CalcDisc(this)' style='text-align:right' placeholder='discount' class="ffood" /></td>
                                        <td><input size=10 type='text' id='txt_disc_rp_' style='text-align:right' placeholder='discount Rp' readonly='readonly' class="ffood" /> </td>
                                    </tr>
                                    <tr>
                                        <td><input size=6 type='text' id='txt_vat_per_' onkeyup='CalcVat(this)' style='text-align:right' placeholder='vat' class="ffood" /></td>
                                        <td><input size=10 type='text' id='txt_vat_rp_' style='text-align:right' placeholder='vat Rp' readonly='readonly' class="ffood" /> </td>
                                    </tr>
                                    <tr>
                                        <td><input size=6 type='text' id='txt_pph_per_' onkeyup='CalcPph(this)' style='text-align:right' placeholder='pph' class="ffood" /></td>
                                        <td><input size=10 type='text' id='txt_pph_rp_' style='text-align:right' placeholder='pph Rp' readonly='readonly' class="ffood" /> </td>
                                    </tr>
                                    <tr>
                                        <td><b>Grand Total</b></td>
                                        <td><input size=10 type='text' id='txt_grand_total_' style='text-align:right' placeholder='grand total' readonly='readonly' class="ffood" /> </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>                        

                        <tr class="hidden-row">
                            <td colspan="6" align="right"></td>
                            <td>
                                <textarea id='txt_desc' name='textarea' style='width:200px;height:80px;'></textarea>
                            </td>
                        </tr>
                    </tfoot>

                </table>

                <input type="file" id="file-1" name="UploadedFile" onchange="GetImg()" />

                @Html.Raw(Labels.ButtonForm("AddRowItemPC"))
                
                @*<div class="frow">
                    <div class="col-xs-6">
                        <input type="file" id="file-1" name="UploadedFile" onchange="GetImg()" />
                    </div>
                </div>*@

                <div class="space-4"></div>

                <div class="row">
                    <div class="col-sm-6">
                        <div class="profile-info-row">
                            <div class="profile-info-name">Comments (User) :</div>
                            <div class="profile-info-value">
                                <span Class="freeText" id="txt_note_by_user"></span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name">Comments (Procurement Dept.) :</div>
                            <div class="profile-info-value">
                                <span Class="freeText" id="txt_note_by_eproc"></span>
                            </div>
                        </div>
                    </div>

                    @Html.Partial("_RecommSupp")

                </div>

            </div>
        </div>
    </div>

    <div class="space-4"></div>

    <div Class="profile-info-row">
        <div Class="profile-info-name"> Acknowledge User </div>
        <div Class="profile-info-value">
            <input type="checkbox" onclick="CheckAcknowledgeUser()" id="cb_is_acknowledge_user" />
            <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be need approve by acknowledge user before this goes to approval of related procurement">?</span>
        </div>
    </div>    

    <div class="alert alert-danger" id="msg_error" style="display:none">
        <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
            <i class="ace-icon fa fa-times"></i>
        </button>
        msg error
        <br />
    </div>


    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                <a class="red" href="@Url.Action("Create", "PRICE_COMPARISON")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                @Html.Raw(Labels.ButtonForm("Submit"))
                @Html.Raw(Labels.ButtonForm("Save"))
            </div>
        </div>
    </div>
</div>


<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/PRICE_COMPARISONController.js"></script>
<script src="~/Scripts/Controllers/PURCHASING_ORDERController.js"></script>

<script>
    jQuery(function ($) {
        $('#file-1').ace_file_input({
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
