//=======================================================================start public parameters=======================================================
var iRowItem;
var isValidItem = true;
var msgValidItem = "";
var fData = new FormData();

//public param for pricecom
var ObjectsPC = [];
var listOfObjectsHeader = [];
var listOfObjectsBody = [];
var listMappingSuppItem = [];
var listOfObjectsFooter = [];

//generate
function GenerateFile() {
    $.ajax({
        cache: false,
        url: linkProc + "/PURCHASE_ORDER/GetFormData",
        type: "POST",
        data: function () {
            var data = new FormData();
            data = fData
            return data;
        }(),
        contentType: false,
        processData: false,
        success: function (response) {
        },
        error: function (jqXHR, textStatus, errorMessage) {
            console.log(errorMessage);
        }
    });
}

function GetImg() {
    var files = jQuery("#file-1").get(0).files[0];

    fData.append("file", files);

    $.ajax({
        cache: false,
        url: linkProc + "/PURCHASE_ORDER/GetFormData",
        type: "POST",
        data: function () {
            var data = new FormData();
            data = fData
            return data;
        }(),
        contentType: false,
        processData: false,
        success: function (response) {
        },
        error: function (jqXHR, textStatus, errorMessage) {
            console.log(errorMessage);
        }
    });
}

function ActionExportToPdf() {
    txt_id = $("#txt_number_id").text().trim();
    txt_po_numb = $("#txt_number_po").text().trim();
    $.ajax({
        url: linkProc + '/PURCHASE_ORDER/ActionExportToPdf',
        type: 'Post',
        data: {
            _ID: txt_id,
            _PO_NUMB: txt_po_numb,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function (result) {
                $(".dialogForm").dialog("close");
                alert(result);
            },
    });
}

function ActionExportToTxt() {
    txt_id = $("#txt_number_id").text().trim();
    txt_po_numb = $("#txt_number_po").text().trim();
    $.ajax({
        url: linkProc + '/PURCHASE_ORDER/ActionExportToTxt',
        type: 'Post',
        data: {
            _ID: txt_id,
            _PO_NUMB: txt_po_numb,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function (result) {
                $(".dialogForm").dialog("close");
                alert(result);
            },
    });
}

function SentEmailToSupplier() {
    txt_id = $("#txt_number_id").text().trim();
    txt_po_numb = $("#txt_number_po").text().trim();
    $.ajax({
        url: linkProc + '/PURCHASE_ORDER/SentEmailToSupplier',
        type: 'Post',
        data: {
            _ID: txt_id,
            _PO_NUMB: txt_po_numb,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function (result) {
                $(".dialogForm").dialog("close");
                alert(result);
            },
    });
}

function ShowAddCost() {
    $("#input_add_cost").toggle(500);
}
//=======================================================================end==============================================================




//======================================================================Start Purchase Order =========================================================
$.extend({
    xResponsePO: function (url, data) {
        // local var
        var theResponse = null;
        // jQuery ajax
        $.ajax({
            url: url,
            data: data,
            cache: false,
            traditional: true,
            type: 'POST',
            async: false,
            success: function (data) {
                theResponse = data;
            }
        });
        // Return the response text
        return theResponse;
    },

    xResponseDataPrNumber: function (url, data, flag) {
        $.ajax({
            url: url,
            data: data,
            cache: false,
            traditional: true,
            type: "POST",
            success:
                function (data) {
                    var markup;

                    var s = 1;
                    for (var x = 0; x < data.length; x++) {
                        var y = document.getElementById('dataTable');
                        var rowLength = y.rows.length;
                        for (var i = 0; i < rowLength; i++) {
                            var oCells = y.rows.item(i).cells;
                            var cellLength = oCells.length - 1;
                            var cellItemName = "";//ITEM_NAME
                            var cellPrNumber = "";//PR Number
                            cellItemName = oCells.item(parseInt(flag)).firstElementChild.value;

                            var arry_result = oCells.item(5 + parseInt(flag)).firstElementChild.value.split('|');
                            cellPrNumber = arry_result[0];
                            var item_name = $("#dropdownList_itempo_nm :selected").text();

                            if (data[x].Value == cellPrNumber && item_name == cellItemName) {
                                s = 0;
                                break;
                            }

                        }
                        if (s == 1) {
                            markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                        }
                        s = 1;
                    }
                    $("#dropdownList_prnumber").html(markup).show();
                },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    },

    xResponseDataJsonPoPc: function (url, data) {
        var theResponse = null;
        $.ajax({
            url: url,
            data: data,
            cache: false,
            traditional: true,
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",

            async: false,
            success: function (respText) {
                theResponse = respText;
            }
        });
        return theResponse;
    },

});

function GetSubPOType(_poTypId) {
    var cb_potyp = document.getElementById('dropdownList_potyp');
    var txt_type_id = document.getElementById("txt_cb_potype_id");
    var txt_form_type_id = document.getElementById("txt_form_type_id");

    var arry;
    var poTypeId = "", formTypeId = "";

    if (_poTypId != "") {
        arry = _poTypId.split("|");
        poTypeId = arry[0];
        formTypeId = arry[1];
        cb_potyp.disabled = false;
        ActiveFieldUM(arry[2]);
    } else {
        cb_potyp.selectedIndex = "0";
        cb_potyp.disabled = false;
    }

    txt_type_id.innerText = poTypeId;
    txt_form_type_id.innerText = formTypeId;
    RefreshScreen();
}

function RefreshScreen() {
    RefreshTablePO('dataTable');
    ClearValueInput(); //clear the value input item (all) still not work
    ClearInputCalc();
}

function ClearInputCalc() {
    $("#txt_sub_total").val("");
    $("#txt_discount").val("");
    $("#txt_vat").val("");
    $("#txt_wth_tax_pph").val("");
    $("#txt_grand_total").val("");

    $("#txt_disc_per_").val("");
    $("#txt_vat_per_").val("");
    $("#txt_pph_per_").val("");

}

function ActiveFieldUM(ft) {
    if (ft == "PRINTING" || ft == "OFFICESUPPLIES") {
        //var um = document.getElementById('txt_um_po_');
        $("input[id='txt_um_po_']").attr("readonly", true);
    } else {
        $("input[id='txt_um_po_']").attr("readonly", false);
    }
}

function GetSupplierNm(_poSupplierNm) {
    var cb_suppliernm = document.getElementById('dropdownList_supplier_nm');
    var txt_supplier = document.getElementById("txt_cb_supplier_id");

    if (_poSupplierNm > 0 || _poSupplierNm != null) {
        //fst
        //var procemessage = "<option value='0'> Please wait...</option>";
        //$("#dropdownList_supplier_nm").html(procemessage).show();
        cb_suppliernm.disabled = false;
        //var result = $.xResponseDataFst(linkProc + '/PURCHASING_ORDER/GetPOTypeId/', { po_type_id: _poTypId });

    } else {
        cb_suppliernm.selectedIndex = "0";
        cb_suppliernm.disabled = false;
    }

    txt_supplier.innerText = _poSupplierNm;
    //GetRelDeptList(0);//passing 0 for nothing return
    ClearValueInput(); //clear the value input item (all) still not work
}

$(function () {
    $('#dropdownList_supplier_nm').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        if (optionSelected != null && optionSelected != "0") {
            var arry = optionSelected.split("|");

            $('#txt_cb_supplier_id').text(arry[0]);
            $('#txt_phone_supplier').text(arry[1]);
            $('#txt_address_supplier').text(arry[2]);
            $('#txt_contact_person_supplier').text(arry[3]);
            $('#txt_fax_supplier').text(arry[4]);
            $('#txt_cb_supplier_nm').text(arry[5]);
        } else {
            $('#txt_cb_supplier_id').text("0");
            $('#txt_phone_supplier').text("");
            $('#txt_address_supplier').text("");
            $('#txt_contact_person_supplier').text("");
            $('#txt_fax_supplier').text("");
            $('#txt_cb_supplier_nm').text("");
        };

    });
});

function GetDeliveryNm(_poDeliveryNm) {
    var cb_deliverynm = document.getElementById('dropdownList_delivery_nm');
    var txt_delivery = document.getElementById("txt_cb_delivery_id");

    if (_poDeliveryNm > 0 || _poDeliveryNm != null) {
        cb_deliverynm.disabled = false;

    } else {
        cb_deliverynm.selectedIndex = "0";
        cb_deliverynm.disabled = false;
    }

    txt_delivery.innerText = _poDeliveryNm;
    ClearValueInput(); //clear the value input item (all) still not work
}

$(function () {
    $('#dropdownList_delivery_nm').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        if (optionSelected != null && optionSelected != "0") {
            var arry = optionSelected.split("|");

            $('#txt_cb_delivery_id').text(arry[0]);
            $('#txt_phone_delivery').text(arry[1]);
            $('#txt_address_delivery').text(arry[2]);
            $('#txt_delivery_dt_new').text(arry[3]);
            $('#txt_fax_delivery').text(arry[4]);
            $('#txt_cb_delivery_nm').text(arry[5]);
        } else {
            $('#txt_cb_delivery_id').text("0");
            $('#txt_phone_delivery').text("");
            $('#txt_address_delivery').text("");
            $('#txt_delivery_dt_new').text("");
            $('#txt_fax_delivery').text("");
            $('#txt_cb_delivery_nm').text("");
        }
    });
});

function GetCurrency(_poCurrency) {
    var cb_currency = document.getElementById('dropdownList_currency');
    var txt_currency = document.getElementById("txt_cb_currency_id");

    if (_poCurrency > 0) {
        //fst
        //var procemessage = "<option value='0'> Please wait...</option>";
        //$("#dropdownList_currency").html(procemessage).show();
        cb_currency.disabled = false;
        //var result = $.xResponseDataFst(linkProc + '/PURCHASING_ORDER/GetPOTypeId/', { po_type_id: _poTypId });

    } else {
        cb_currency.selectedIndex = "0";
        cb_currency.disabled = false;
    }

    txt_currency.innerText = _poCurrency;
    //GetRelDeptList(0);//passing 0 for nothing return
    ClearValueInput(); //clear the value input item (all) still not work
}

function ClearValueInput() {
    var x = document.getElementById('table_input');

    for (var i = 1; i < x.rows.length; i++) {
        var itemRow = x.rows[i].cloneNode(true);
        for (var j = 0; j < itemRow.cells.length; j++) {
            var inp = itemRow.cells[j].getElementsByTagName('input')[0];
            if (typeof (inp) != 'undefined') {
                inp.value = '';
            }
        }
    }
}

function BtnAction(action) {
    if (action == "RejectedByVerifier" || action == "RejectedByApprover" || action == "RejectedByAdminEproc") {
        ActionRejectPO(action);
        return false;
    }
    
    if (IsValidForm() == 0) {
        if (IsValidLength() == 0) {
            if (action == "create") {
                ActionCreate();
            } else {
                ActionUpdateStatus(action);
            }
        }
    }    
}

function ActionRejectPO(reject_by) {
    var reason = $("#txt_reason_reject_po").val();
    if (reason == "") {
        $("#input_reason_po").toggle(500);
        $("#txt_reason_reject_po").val("").focus();
        alert("Please insert the reason");
    } else {
        ActionReject(reject_by);
    }
}

function ActionReject(reject_by) {
    txt_po_id = $("#txt_number_id").text();
    txt_po_number = $("#txt_number_po").text();
    txt_pc_id = $("#txt_pc_id").text() == "" ? 0 : $("#txt_pc_id").text();
    txt_reason = $("#txt_reason_reject_po").val();
    txt_by_on = reject_by;
    result = $.xResponsePO(linkProc + '/PURCHASE_ORDER/ActionRejectPO/', {
        po_id: txt_po_id,
        po_number: txt_po_number,
        pc_id: txt_pc_id,
        reason: txt_reason,
        by_on: txt_by_on,
    });

    if (result != undefined || result != null) {
        var arry_result = result.split('|');
        if (arry_result[0] == 'False') {
            msg_error.style.display = '';
            msg_error.textContent = arry_result[1];
            alert("fail");
        } else {
            msg_error.style.display = 'none';
            alert("success");
            Refresh();
        }
    } else {
        alert("error system");
    }
}

function ActionCreate() {
    var msg_error = document.getElementById('msg_error');

    //base from is to know where is from accessed. 0 from po screen, 1 from pc screen
    txt_base_from = $("#txt_base_from").text();

    //validation for pc
    if (txt_base_from == 1 && !IsValidSuppAndTotal(txt_base_from)) {
        msg_error.style.display = '';
        msg_error.textContent = "please select supplier";
        return false;
    }

    cb_dropdownList_potyp = $("#txt_cb_potype_id").text();
    cb_dropdownList_potyp_nmx = $("#dropdownList_potyp :selected").text();
    txt_prepared_by = $("#txt_prepared_by_po").text();
    txt_notes = $("#txt_notes").text();
    txt_created_dt = ConvertDate($("#txt_created_dt").text());
    cb_dropdownList_currency = $("#txt_cb_currency_id").text();
    txt_approved_by = $("#txt_approved_by").text();
    //cb_dropdownList_status = $("#txt_cb_status_id").text();

    txt_supplier_nm = $("#txt_cb_supplier_nm").text();
    txt_phone_supplier = $("#txt_phone_supplier").text();
    txt_address_supplier = $("#txt_address_supplier").text();
    txt_contact_person_supplier = $("#txt_contact_person_supplier").text();
    txt_fax_supplier = $("#txt_fax_supplier").text();

    cb_dropdownList_delivery_nm = $("#txt_cb_delivery_id").text();
    txt_delivery_nm = $("#txt_cb_delivery_nm").text();
    txt_phone_delivery = $("#txt_phone_delivery").text();
    txt_address_delivery = $("#txt_address_delivery").text();
    txt_delivery_dt_new = ConvertDate($("#txt_delivery_dt_new").text());
    txt_fax_delivery = $("#txt_fax_delivery").text();

    txt_sub_total = $("#txt_sub_total").val().replace(/,/g, "");
    txt_discount = $("#txt_discount").val().replace(/,/g, "");
    txt_vat = $("#txt_vat").val().replace(/,/g, "");
    txt_wth_tax_pph = $("#txt_wth_tax_pph").val().replace(/,/g, "");
    txt_grand_total = $("#txt_grand_total").val().replace(/,/g, "");

    var item_detail = new Array();

    if (!isValidItem) {
        msg_error.style.display = '';
        msg_error.textContent = "Item number " + msgValidItem + " is not valid";
    }

    if (txt_grand_total == "" || txt_grand_total == "NaN") {
        msg_error.style.display = '';
        msg_error.textContent = "Grand total must be filled and is not NaN";
        return false;
    }

    var txt_for_storage;
    $("#cb_for_storage").is(":checked") ? txt_for_storage = 1 : txt_for_storage = 0;

    var result;

    if (txt_base_from == 0) {
        item_detail = getDataTableArray();
        cb_dropdownList_supplier_nm = $("#txt_cb_supplier_id").text();
        result = $.xResponsePO(linkProc + '/PURCHASE_ORDER/ActionCreatePO/', {
            cb_dropdownList_potyp_id: cb_dropdownList_potyp,
            cb_dropdownList_potyp_nm: cb_dropdownList_potyp_nmx,
            prepared_by: txt_prepared_by,
            notes: txt_notes,
            created_dt: txt_created_dt,
            cb_dropdownList_currency_id: cb_dropdownList_currency,
            approved_by: txt_approved_by,
            //cb_dropdownList_status_id: cb_dropdownList_status,

            cb_dropdownList_supplier_nm_id: cb_dropdownList_supplier_nm,
            supplier_nm: txt_supplier_nm,
            phone_supplier: txt_phone_supplier,
            address_supplier: txt_address_supplier,
            contact_person_supplier: txt_contact_person_supplier,
            fax_supplier: txt_fax_supplier,

            cb_dropdownList_delivery_nm_id: cb_dropdownList_delivery_nm,
            delivery_nm: txt_delivery_nm,
            phone_delivery: txt_phone_delivery,
            address_delivery: txt_address_delivery,
            delivery_dt_new: txt_delivery_dt_new,
            fax_delivery: txt_fax_delivery,

            sub_total: parseInt(txt_sub_total),
            discount: parseInt(txt_discount),
            vat: parseInt(txt_vat),
            wth_tax_pph: parseInt(txt_wth_tax_pph),
            grand_total: parseInt(txt_grand_total),

            litem_detail: item_detail,
            base_from: txt_base_from,
            for_storage: txt_for_storage,

            is_disc_perc: $("#rb_disc_perc").is(":checked") ? 1 : 0,
            is_vat_perc: $("#rb_vat_perc").is(":checked") ? 1 : 0,
            is_pph_perc: $("#rb_pph_perc").is(":checked") ? 1 : 0,
            discount_temp: parseInt($("#txt_disc_per_").val()),
            vat_temp: parseInt($("#txt_vat_per_").val()),
            pph_temp: parseInt($("#txt_pph_per_").val()),
        });

        if (result != undefined || result != null) {
            var arry_result = result.split('|');
            if (arry_result[0] == 'False') {
                msg_error.style.display = '';
                msg_error.textContent = arry_result[1];
                alert("fail");
            } else {
                msg_error.style.display = 'none';
                alert("success");
                Refresh();
            }
        } else {
            alert("error system");
        }

    } else if (txt_base_from == 1) {
        item_detail = getDataTableArrayPC()
        cb_dropdownList_supplier_nm = $("#txt_supp_id_recomm").text();
        GetJsonObjectPriceCom();
        //GenerateFile(); //get attachment

        var discount_t, vat_t, pph_t;
        for (var i = 0; i < listOfObjectsFooter.length; i++) {
            if (listOfObjectsFooter[i].Is_used == "1") {
                discount_t = listOfObjectsFooter[i].Disc_temp;
                vat_t = listOfObjectsFooter[i].Vat_temp;
                pph_t = listOfObjectsFooter[i].Pph_temp;
            }
        }

        result = $.xResponseDataJsonPoPc(linkProc + '/PURCHASE_ORDER/ActionCreatePC/', JSON.stringify({
            cb_dropdownList_potyp_id: cb_dropdownList_potyp,
            cb_dropdownList_potyp_nm: cb_dropdownList_potyp_nmx,
            prepared_by: txt_prepared_by,
            notes: txt_notes,
            created_dt: txt_created_dt,
            cb_dropdownList_currency_id: cb_dropdownList_currency,
            approved_by: txt_approved_by,

            cb_dropdownList_supplier_nm_id: cb_dropdownList_supplier_nm,
            supplier_nm: txt_supplier_nm,
            phone_supplier: txt_phone_supplier,
            address_supplier: txt_address_supplier,
            contact_person_supplier: txt_contact_person_supplier,
            fax_supplier: txt_fax_supplier,

            cb_dropdownList_delivery_nm_id: cb_dropdownList_delivery_nm,
            delivery_nm: txt_delivery_nm,
            phone_delivery: txt_phone_delivery,
            address_delivery: txt_address_delivery,
            delivery_dt_new: txt_delivery_dt_new,
            fax_delivery: txt_fax_delivery,

            sub_total: parseInt(txt_sub_total),
            discount: parseInt(txt_discount),
            vat: parseInt(txt_vat),
            wth_tax_pph: parseInt(txt_wth_tax_pph),
            grand_total: parseInt(txt_grand_total),
            litem_detail: item_detail,

            base_from: txt_base_from,
            for_storage: txt_for_storage,
            ObjectsPCx: ObjectsPC, listOfObjectsHeaderx: listOfObjectsHeader, listOfObjectsBodyx: listOfObjectsBody, listMappingSuppItemx: listMappingSuppItem, listOfObjectsFooterx: listOfObjectsFooter,
            listOfObjectsWax: listOfObjectsWa,

            is_disc_perc: $("#rb_disc_perc").is(":checked") ? 1 : 0,
            is_vat_perc: $("#rb_vat_perc").is(":checked") ? 1 : 0,
            is_pph_perc: $("#rb_pph_perc").is(":checked") ? 1 : 0,
            discount_temp: parseInt(discount_t),
            vat_temp: parseInt(vat_t),
            pph_temp: parseInt(pph_t),
        }));
        Refresh();
    }
}

function IsValidSuppAndTotal(base_from) {
    var result = true;

    var supplierId = document.getElementById("txt_supp_id_recomm");
    //var subTotal = document.getElementById("txt_sub_total").val(); 

    if (base_from = 1 && supplierId != null) {
        result = true;
    } else if (base_from = 1 && supplierId == null) {
        result = false;
    }

    return result;
}

function ActionUpdateStatus(status) {
    //cb_dropdownList_potyp = $("#txt_po_status_changed").text();
    var msg_error = document.getElementById('msg_error');

    if (isValidItem) {
        var result = $.xResponse(linkProc + '/PURCHASE_ORDER/ActionUpdateStatus/', {
            //status_current: cb_dropdownList_potyp,
            status_new: status
        });

        if (result != undefined || result != null) {
            var arry_result = result.split('|');
            if (arry_result[0] == 'False') {
                msg_error.style.display = '';
                msg_error.textContent = arry_result[1];
            } else {
                msg_error.style.display = 'none';
                Refresh();
            }
        } else {
            msg_error.style.display = 'Error Submit';
        }

    } else {
        msg_error.style.display = '';
        msg_error.textContent = "Item number " + msgValidItem + " is not valid";
    }
}

function Refresh() {
    window.location.reload(true);
}

function getItemPO(row, flag) {
    iRowItem = GetRowIndex(row) - 1;

    //get for storage
    var txt_for_storage;
    $("#cb_for_storage").is(":checked") ? txt_for_storage = 1 : txt_for_storage = 0;

    //get form type id
    form_type_id = $("#txt_form_type_id").text();

    var url = '../PURCHASE_ORDER/PopDetailPO/' + "?flag=" + flag + "&for_storage=" + txt_for_storage + "&form_type_id=" + form_type_id;
    var form = '.dialogForm';

    ModalPop2(url, form);
}

function OpenCrvDetails() {
    var url = '../PURCHASE_ORDER/PopCRVDetails/';
    var form = '.dialogForm';

    ModalPop2(url, form);
}

function OpenPcDetails() {
    var url = '../PURCHASE_ORDER/PopPCDetails/';
    var form = '.dialogForm';

    ModalPop2(url, form);
}

function CalSubTotalPO() {
    var x = document.getElementById('dataTable');
    var result = 0;
    for (var i = 0; i < x.rows.length; i++) {
        var r = x.rows[i].cells[4].childNodes[0].value.replace(/,/g, "");
        //window.alert("======r======= " + r)
        if (r > 0) {
            result = result + parseInt(r);
        }
    }
    return result;
}

function getDataTableArray() {
    var x = document.getElementById('dataTable');
    var rowLength = x.rows.length;
    var temp = new Array();
    //var temp = "" ;
    for (var i = 0; i < rowLength; i++) {
        var oCells = x.rows.item(i).cells;
        var cellLength = oCells.length - 1;

        var cellItemName = "";//ITEM_NAME
        var cellUM = "";//UM
        var cellQty = "";//QTY
        var cellPrice = "";//PRICE
        var cellTotalPrice = "";//TOTALPRICE
        var cellDetailPo = "";//Temp_DETAIL_PO
        var cellItemId = "";//ITEM_ID

        for (j = 0; j < cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;
            //alert("===getDataTableArray== " + value); 
            switch (j) {
                case 0:
                    cellItemName = value;
                    break;
                case 1:
                    cellUM = value;
                    break;
                case 2:
                    cellQty = value;
                    break;
                case 3:
                    cellPrice = value;
                    break;
                case 4:
                    cellTotalPrice = value;
                    break;
                case 5:
                    cellDetailPo = value;
                    break;
                case 6:
                    cellItemId = value;
                    break;
            }
        }
        //alert("==temp.push==" + cellItemName + ";" + cellPrNo + ";" + cellUM + ";" + cellQty + ";" + cellPrice + ";" + cellDetailPo);
        //temp.push(cellItemName + ";" + cellPrNo + ";" + cellUM + ";" + cellQty + ";" + cellPrice + ";" + cellDetailPo + ";" + cellItemId);
        temp.push(cellItemName + ";" + cellUM + ";" + cellQty + ";" + cellPrice + ";" + cellTotalPrice + ";" + cellDetailPo + ";" + cellItemId);
        //temp = temp + cellItemName + ";" + cellUM + ";" + cellQty + ";" + cellPrice + ";" + cellTotalPrice + ";" + cellDetailPo + ";" + cellItemId ;
        //if (j < cellLength) {
        //    temp = temp + ">]";
        //}
    }
    //alert("=temp= " + temp.length);
    return temp;
}

//method for get data base on price comparison
function getDataTableArrayPC() {
    var x = document.getElementById('dataTable');
    var rowLength = x.rows.length;
    var temp = new Array();
    for (var i = 0; i < rowLength; i++) {
        var oCells = x.rows.item(i).cells;
        var cellLength = 6;

        var cellItemId = "";//ITEM_ID
        var cellItemName = "";//ITEM_NAME
        var cellPrNo = "";//PR_NO
        var cellUM = "";//UM
        var cellQty = "";//QTY
        var cellPrice = "";//PRICE
        var cellDetailPo = "";//Temp_DETAIL_PO

        for (j = 0; j <= cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;
            //alert("===getDataTableArray== " + value);
            switch (j) {
                case 0:
                    cellItemId = oCells.item(j).lastElementChild.value;
                    break;
                case 1:
                    cellItemName = value;
                    break;
                case 2:
                    cellPrNo = value;
                    break;
                case 3:
                    cellUM = value;
                    break;
                case 4:
                    cellQty = value;
                    break;
                case 5:
                    cellPrice = value;
                    break;
                case 6:
                    cellDetailPo = value;
                    break;
            }
        }
        //alert("==temp.push==" + cellItemName + ";" + cellPrNo + ";" + cellUM + ";" + cellQty + ";" + cellPrice + ";" + cellDetailPo);
        temp.push(cellItemName + ";" + cellPrNo + ";" + cellUM + ";" + cellQty + ";" + cellPrice + ";" + cellDetailPo + ";" + cellItemId);
    }
    //alert("=temp= " + temp.length);
    return temp;
}

function BtnActionPO(action) {
    //if (IsValidForm() == 0) {
    //if (IsValidDate() == 0) {
    //if (IsValidLength() == 0) {
    if (action == "temp") {
        TempPoDetail(action)
    }
    //}
    //}
    //}
}

function TempPoDetail(_param) {
    if (_param == "temp") {
        var PoTableDetail = document.getElementById('dataTablePODetail');
        var rowLength = PoTableDetail.rows.length;
        var item_id = $("#txt_cb_itempo_id").text();
        var item_nm = $("#txt_cb_itempo_nm").text();
        var total_price = $("#txt_sub_total_price_po").text();
        var fundt1 = $("#txt_fund_t1_id").text();
        var lob1t2 = $("#txt_lob1_t2_id").text();
        var plant3 = $("#txt_plan_t3_id").text();
        var wat4 = $("#txt_wa").text();
        var lob2t5 = $("#txt_lob2_t5_id").text();

        var cellItemId = "";//ITEM_ID
        var cellItem = "";//ITEM
        var cellPrNumber = "";//PRNUMBER
        var cellUM = "";//UM
        var cellQty = 0;//Quantity
        var cellQty2 = 0;//Quantity
        var cellPrice = 0;//Price
        var cellPrice2 = 0;//Price
        var cellTotal = 0;//Total
        //var tempDetailPo = new Array();
        var cellWANumber = "";//WA Number
        var tempDetailPo = "";

        cellItemId = item_id
        cellItem = item_nm;
        cellTotal = total_price;
        for (var i = 0; i < rowLength; i++) {
            var oCells = PoTableDetail.rows.item(i).cells;
            var cellLength = oCells.length - 1;

            for (j = 0; j < cellLength; j++) {
                var value = oCells.item(j).firstElementChild.value;
                switch (j) {
                    case 0:
                        cellPrNumber = value;
                        break;
                    case 1:
                        cellUM = value;
                        break;
                    case 2:
                        cellQty = parseFloat(value);
                        cellQty2 = cellQty + cellQty2
                        break;
                    case 3:
                        cellPrice = parseFloat(value);
                        cellPrice2 = cellPrice + cellPrice2
                        break;
                    case 4:
                        cellWANumber = value;
                        break;
                }
            }
            tempDetailPo = tempDetailPo + cellPrNumber + "|" + cellItem + "|" + cellUM + "|" + cellQty + "|" + cellPrice + "|" + cellTotal.replace(/,/g, "") + "|" +
                fundt1 + "|" + lob1t2 + "|" + plant3 + "|" + cellWANumber + "|" + lob2t5;
            if (i < rowLength - 1) {
                tempDetailPo = tempDetailPo + "^";
            }

            isValidItem = true;
            msgValidItem = "";
        }

        var r_item_id, r_item, r_um, r_qty, r_price, r_total, r_tempDetailPo;

        if (iRowItem == 0) {
            r_item_id = document.getElementById("txt_item_id_po_");
            r_item = document.getElementById("txt_item_po_");
            r_um = document.getElementById("txt_um_po_");
            r_qty = document.getElementById("txt_quantity_po_");
            r_price = document.getElementById("txt_price_po_");
            r_total = document.getElementById("txt_total_po_");
            r_tempDetailPo = document.getElementById("txt_id_detail_po_");

        } else {
            r_item_id = document.getElementById("txt_item_id_po_" + iRowItem);
            r_item = document.getElementById("txt_item_po_" + iRowItem);
            r_um = document.getElementById("txt_um_po_" + iRowItem);
            r_qty = document.getElementById("txt_quantity_po_" + iRowItem);
            r_price = document.getElementById("txt_price_po_" + iRowItem);
            r_total = document.getElementById("txt_total_po_" + iRowItem);
            r_tempDetailPo = document.getElementById("txt_id_detail_po_" + iRowItem);

        }

        r_item_id.innerText = cellItemId
        r_item.innerText = cellItem;
        r_um.innerText = cellUM;
        r_qty.innerText = cellQty2;
        r_price.innerText = cellPrice;
        r_total.innerText = cellTotal;
        r_tempDetailPo.innerText = tempDetailPo;
    }
    var cal_sub_total = CalSubTotalPO();
    $('#txt_sub_total').val(Comma(cal_sub_total.toString()));

    //Close pop up PO Detail
    $(".dialogForm").dialog("close");
}

function CalcDiscPo(row) {
    var disc_ = row.value;

    var subtotal = document.getElementById("txt_sub_total");
    var disc_rp = document.getElementById("txt_discount");

    var is_disc_perc;
    $("#rb_disc_perc").is(":checked") ? is_disc_perc = 1 : is_disc_perc = 0;

    var r;
    if (is_disc_perc == 1) {
        r = (parseInt(disc_) * parseInt(subtotal.value.replace(/,/g, ""))) / 100;
    } else {
        r = disc_;
    }

    disc_rp.value = Comma(r.toString());
}

function CalcVatPo(row) {
    var vat_ = row.value;

    var subtotal = document.getElementById("txt_sub_total");
    var disctotal = document.getElementById("txt_discount");
    var vat_rp = document.getElementById("txt_vat");

    var is_vat_perc;
    $("#rb_vat_perc").is(":checked") ? is_vat_perc = 1 : is_vat_perc = 0;

    var r;
    if (is_vat_perc == 1) {
        //r = (parseInt(vat_) * parseInt(disctotal.value.replace(/,/g, ""))) / 100;
        r = ((parseInt(subtotal.value.replace(/,/g, "")) - parseInt(disctotal.value.replace(/,/g, ""))) * parseInt(vat_)) / 100;
    } else {
        r = vat_;
    }

    vat_rp.value = Comma(r.toString());
}

function CalcPphPo(row) {
    var pph_ = row.value;

    var vat_rp = document.getElementById("txt_wth_tax_pph");

    var r = pph_;

    vat_rp.value = Comma(r.toString());
}

function CalcAddCostPo(row) {
    var add_cost_ = row.value;

    var add_cost_rp = document.getElementById("txt_add_cost");

    var r = add_cost_;

    add_cost_rp.value = Comma(r.toString());
}

function CalcGrandTotalPO() {
    txt_sub_total = $("#txt_sub_total").val().replace(/,/g, "");
    txt_discount = $("#txt_discount").val().replace(/,/g, "");
    txt_vat = $("#txt_vat").val().replace(/,/g, "");
    txt_wth_tax_pph = $("#txt_wth_tax_pph").val().replace(/,/g, "");


    var result = ((parseInt(txt_sub_total) - parseInt(txt_discount)) + parseInt(txt_vat)) - parseInt(txt_wth_tax_pph)

    //set grand total
    var grand_total = document.getElementById('txt_grand_total');
    grand_total.value = Comma(result.toString());
}

function CalcFinalTotalPO() {
    txt_grand_total = $("#txt_grand_total").val().replace(/,/g, "");
    txt_add_cost = $("#txt_add_cost").val().replace(/,/g, "");


    var result = parseInt(txt_grand_total) - parseInt(txt_add_cost);

    //set final total
    var final_total = document.getElementById('txt_final_total');
    final_total.value = Comma(result.toString());
}

$(function () {
    $('#txt_delivery_dt_new').on('DOMSubtreeModified', function () {
        BtnGetReqIndicator();
    })
});

function BtnGetReqIndicator() {
    var nowDate = new Date();
    var dt_origin = nowDate.getDate() + '-' + (nowDate.getMonth() + 1) + '-' +  nowDate.getFullYear() ;
    var dt_new = $('#txt_delivery_dt_new').text();

    if (dt_new != 'Empty') {
        GetReqIndicator(dt_origin, dt_new);
    }
}

function GetReqIndicator(dt_origin, dt_new) {
    var new_dt_origin = ConvertDateToJsFormat(dt_origin);
    var new_dt_new = ConvertDateToJsFormat(dt_new);

    if (new_dt_origin > new_dt_new) {
        alert("Date can't be lower date now");
        $('#txt_delivery_dt_new').text("Empty");
    }
}

function PushEmailByPO() {
    var txt_po_header_id = $("#txt_number_id").text();
    var txt_po_number = $("#txt_number_po").text();
    var txt_po_date = $("#txt_created_dt").text();
    var txt_po_type = $("#po_type").text();

    var urlMethod = '/PURCHASE_ORDER/SentPushEmailByPO/';

    $.ajax({
        url: linkProc + urlMethod,
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            po_header_id: txt_po_header_id,
            po_number: txt_po_number,
            po_date: txt_po_date,
            po_type: txt_po_type,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to push email");
        },
        success:
            function () {
                alert("Email has been pushed");
                //Refresh();
            },
    });
}

//=======================================================================end==========================================================================




//======================================================================Start Price Comparison =========================================================
function CalcTotalPC(row) {
    iRowItem = GetRowIndexQty(row);
    var each_price = row.value;
    var result, qty, priceTotal;

    if (iRowItem == "") {
        qty = document.getElementById("txt_quantity_po_");
        priceTotal = document.getElementById('txt_total_price_');
    }
    else {
        qty = document.getElementById("txt_quantity_po_" + iRowItem);
        priceTotal = document.getElementById('txt_total_price_' + iRowItem);
    }

    var r = parseInt(each_price) * parseInt(qty.value.replace(/,/g, ""));

    result = r.toString().replace(/,/g, "")
    result += '';
    x = result.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');

    x3 = x1 + x2;

    var priceTotal = x3 == 'NaN' ? 0 : x3;
    SetValueTotalPrice(row, priceTotal);

    CalcSubTotal()
}

function SetValueTotalPrice(en, values) {
    while ((en = en.parentNode) && en.nodeName.toLowerCase() !== 'tr');

    en.cells[1].firstElementChild.value = values;
}

function GetRowIndexQty(el) {
    while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

    var xx = el.cells[0].childNodes['0'].id;
    var fields = xx.split('_');

    var len = fields.length - 1

    return fields[len];
}

function CalcSubTotal() {
    var x = document.getElementById('dataTable');
    var y = document.getElementById('footTable');

    var each_price_index = 7;
    for (var h = 0; h < y.firstElementChild.cells.length - 1; h++) {
        var result = 0;
        for (var i = 0; i < x.rows.length ; i++) {
            var r = x.rows[i].cells[3].childNodes[0].value.replace(/,/g, "");

            var c = x.rows[i].cells[each_price_index].firstElementChild.cells[0].firstElementChild.value.replace(/,/g, "");
            if (r > 0 && c != 0 && c != "") {
                rc = parseInt(r) * parseInt(c);
                result = result + rc;
            }
        }

        var xxx = y.firstElementChild.cells[h + 1].firstElementChild.cells[1].childNodes[0];
        xxx.value = Comma(result.toString());

        each_price_index += 1;
    }
}

function LoadSupplierRecomm(id, sub_total, disc, vat, pph, grand_total) {
    $.ajax({
        cache: false,
        url: linkProc + '/PRICE_COMPARISON/RecommSupp/' + id + '?sub_total=' + sub_total + '&disc=' + disc + '&vat=' + vat + '&pph=' + pph + '&grand_total=' + grand_total,
        success: function (data) {
            $("#recomm_supp").html(data);
        }
    });
}

function GetCheckSupplier(row) {
    var index_col_supp = row.parentNode.cellIndex;

    while ((row = row.parentNode) && row.nodeName.toLowerCase() !== 'tr');

    var id_supp = row.cells[index_col_supp].children.txt_cb_supplier_id.textContent;

    var detail_value = GetDetailTotalValue(index_col_supp);
    var l_detail_value = detail_value.split("|");

    var sub_total = l_detail_value[0];
    var disc = l_detail_value[1];
    var vat = l_detail_value[2];
    var pph = l_detail_value[3];
    var grand_total = l_detail_value[4];

    LoadSupplierRecomm(id_supp, sub_total, disc, vat, pph, grand_total);

    SetOtherUncheck(index_col_supp);
}

function GetDetailTotalValue(index_col_supp) {
    var y = document.getElementById('footTable');

    var index_col_foot = index_col_supp - 7
    var data_table_detail_value = y.firstElementChild.cells[index_col_foot + 1].firstElementChild;

    var col_sub_total = data_table_detail_value.cells[1].childNodes[0].value;
    var col_disc = data_table_detail_value.cells[3].childNodes[0].value;
    var col_vat = data_table_detail_value.cells[5].childNodes[0].value;
    var col_pph = data_table_detail_value.cells[7].childNodes[0].value;
    var col_grand_total = data_table_detail_value.cells[9].childNodes[0].value;

    var result = Comma(col_sub_total.toString()) + "|" + Comma(col_disc.toString()) + "|" + Comma(col_vat.toString()) + "|" + Comma(col_pph.toString()) + "|" + Comma(col_grand_total.toString());

    return result;
}

function GetSupplierList(row) {
    iRowItem = GetRowIndex(row) - 1;
    var r_qty, r_convertion;

    var optionSelected = $(row).find('option:selected').text();
    var optionSelectedVal = row.value;

    var index_col_supp = row.parentNode.cellIndex;


    var y = document.getElementById('headeTable');

    var xxx = y.cells[index_col_supp].children.txt_cb_supplier_id;
    xxx.textContent = optionSelectedVal;
}

function SetOtherUncheck(index_col_supp_chk) {
    var y = document.getElementById('headeTable');

    var start = 7;
    for (var i = 0; i < y.cells.length - 7; i++) {
        y.cells[start].children.txt_ck_supplier.checked = false;
        start++;
    }

    y.cells[index_col_supp_chk].children.txt_ck_supplier.checked = true;
}

function CalcDisc(row) {
    var disc_ = row.value;

    var iRow = GetRow(row);
    var subtotal = iRow.parentNode.children[0].cells[1].children.txt_sub_total_;
    var disc_rp = iRow.cells[1].children.txt_disc_rp_;

    var is_disc_perc;
    $("#rb_disc_perc").is(":checked") ? is_disc_perc = 1 : is_disc_perc = 0;

    var r;
    if (is_disc_perc == 1) {
        r = (parseInt(disc_) * parseInt(subtotal.value.replace(/,/g, ""))) / 100;
    } else {
        r = disc_;
    }

    disc_rp.value = Comma(r.toString());
}

function CalcVat(row) {
    var vat_ = row.value;

    var iRow = GetRow(row);
    var subtotal = iRow.parentNode.children[0].cells[1].children.txt_sub_total_;
    var disctotal = iRow.parentNode.children[1].cells[1].children.txt_disc_rp_;
    var vat_rp = iRow.cells[1].children.txt_vat_rp_;

    var is_vat_perc;
    $("#rb_vat_perc").is(":checked") ? is_vat_perc = 1 : is_vat_perc = 0;

    var r;
    if (is_vat_perc == 1) {
        //r = ((parseInt(vat_) - parseInt(vat_)) * parseInt(disctotal.value.replace(/,/g, ""))) / 100;
        r = ((parseInt(subtotal.value.replace(/,/g, "")) - parseInt(disctotal.value.replace(/,/g, ""))) * parseInt(vat_)) / 100;
    } else {
        r = vat_;
    }

    vat_rp.value = Comma(r.toString());
}

function CalcPph(row) {
    var pph_ = row.value;

    var iRow = GetRow(row);
    //var disctotal = iRow.parentNode.children[1].cells[1].children.txt_disc_rp_;
    var vat_rp = iRow.cells[1].children.txt_pph_rp_;

    //var is_vat_perc;
    //$("#rb_vat_perc").is(":checked") ? is_vat_perc = 1 : is_vat_perc = 0;

    //var r;
    //if (is_vat_perc == 1) {
    //    r = (parseInt(pph_) * parseInt(disctotal.value.replace(/,/g, ""))) / 100;
    //} else {
    //    r = pph_;
    //}

    var r = pph_;

    vat_rp.value = Comma(r.toString());
}

function CalcGrandTotal() {
    //var x = document.getElementById('dataTable');
    var y = document.getElementById('footTable');

    //var each_price_index = 7;
    for (var h = 0; h < y.firstElementChild.cells.length - 1; h++) {
        //var result = 0;
        //for (var i = 0; i < x.rows.length ; i++) {
        //    var r = x.rows[i].cells[3].childNodes[0].value.replace(/,/g, "");

        //    var c = x.rows[i].cells[each_price_index].firstElementChild.cells[0].firstElementChild.value.replace(/,/g, "");
        //    if (r > 0 && c != 0 && c != "") {
        //        rc = parseInt(r) * parseInt(c);
        //        result = result + rc;
        //    }
        //}

        var sub_total = y.firstElementChild.cells[h + 1].firstElementChild.cells[1].childNodes[0].value.replace(/,/g, "");
        var disc = y.firstElementChild.cells[h + 1].firstElementChild.cells[3].childNodes[0].value.replace(/,/g, "");
        var vat = y.firstElementChild.cells[h + 1].firstElementChild.cells[5].childNodes[0].value.replace(/,/g, "");
        var pph = y.firstElementChild.cells[h + 1].firstElementChild.cells[7].childNodes[0].value.replace(/,/g, "");

        var result = ((parseInt(sub_total) - parseInt(disc)) + parseInt(vat)) - parseInt(pph)

        //set grand total
        var grand_total = y.firstElementChild.cells[h + 1].firstElementChild.cells[9].childNodes[0];
        grand_total.value = Comma(result.toString());

        //each_price_index += 1;
    }
}

function GetJsonObjectPriceCom() {
    //SAVE PC INTO CLASS OBJECT JAVASCRIPT
    var singleObjPC = {}
    singleObjPC['Type'] = 'Pc';
    //singleObjPC['Po_type_id'] = $("#txt_cb_potype_id").text();
    singleObjPC['Po_type_nm'] = $("#dropdownList_potyp :selected").text();
    singleObjPC['Recom_supplier_nm'] = $("#txt_cb_supplier_nm").text();
    singleObjPC['Recom_supplier_id'] = $("#txt_supp_id_recomm").text();
    singleObjPC['Recom_supplier_cp'] = $("#txt_contact_person_supplier").text();
    singleObjPC['Recom_supplier_phone'] = $("#txt_phone_supplier").text();
    singleObjPC['Recom_supplier_fax'] = $("#txt_fax_supplier").text();
    singleObjPC['Recom_supplier_address'] = $("#txt_address_supplier").text();

    singleObjPC['Delivery_nm'] = $("#txt_cb_delivery_nm").text();
    singleObjPC['Delivery_id'] = $("#txt_cb_delivery_id").text();
    singleObjPC['Delivery_date'] = ConvertDate($("#txt_delivery_dt_new").text());
    singleObjPC['Delivery_phone'] = $("#txt_phone_delivery").text();
    singleObjPC['Delivery_fax'] = $("#txt_fax_delivery").text();
    singleObjPC['Delivery_address'] = $("#txt_address_delivery").text();

    singleObjPC['Note_by_user'] = $("#txt_note_by_user").text();
    singleObjPC['Note_by_eproc'] = $("#txt_note_by_eproc").text();

    singleObjPC['Is_disc_perc'] = $("#rb_disc_perc").is(":checked") ? "1" : "0";
    singleObjPC['Is_vat_perc'] = $("#rb_vat_perc").is(":checked") ? "1" : "0";
    singleObjPC['Is_pph_perc'] = $("#rb_pph_perc").is(":checked") ? "1" : "0";

    singleObjPC['Grand_total'] = $("#txt_grand_total").val();

    singleObjPC['Notes'] = $("#txt_notes").text();
    singleObjPC['Currency'] = $("#dropdownList_currency :selected").text();
    //singleObjPC['Status'] = $("#dropdownList_status :selected").text();

    singleObjPC['Is_acknowledge_user'] = $("#cb_is_acknowledge_user").is(":checked") ? "1" : "0";

    ObjectsPC.push(singleObjPC);


    //SAVE HEADER INTO CLASS OBJECT JAVASCRIPT
    var y = document.getElementById('headeTable');

    var start = 7;
    for (var i = 0; i < y.cells.length - 7; i++) {

        var singleObj = {}
        singleObj['Type'] = 'header';
        singleObj['Supp_id'] = y.cells[start].children.txt_cb_supplier_id.firstChild.textContent;
        singleObj['Is_check'] = y.cells[start].children.txt_ck_supplier.checked == true ? "1" : "0";
        singleObj['Supp_nm'] = $(y.cells[start].children.dropdownList_supplier_nm_).find('option:selected').text();

        listOfObjectsHeader.push(singleObj);

        start++;
    }

    //SAVE FOOTER INTO CLASS OBJECT JAVASCRIPT
    var y = document.getElementById('footTable');

    for (var h = 0; h < y.firstElementChild.cells.length - 1; h++) {
        var singleObj = {}
        singleObj['Type'] = 'footer';

        singleObj['Sub_total'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[1].childNodes[0].value.replace(/,/g, "");
        singleObj['Disc_temp'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[2].childNodes[0].value.replace(/,/g, "");
        singleObj['Disc'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[3].childNodes[0].value.replace(/,/g, "");
        singleObj['Vat_temp'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[4].childNodes[0].value.replace(/,/g, "");
        singleObj['Vat'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[5].childNodes[0].value.replace(/,/g, "");
        singleObj['Pph_temp'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[6].childNodes[0].value.replace(/,/g, "");
        singleObj['Pph'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[7].childNodes[0].value.replace(/,/g, "");
        singleObj['Grand_total'] = y.firstElementChild.cells[h + 1].firstElementChild.cells[9].childNodes[0].value.replace(/,/g, "");
        singleObj['Supp_id'] = listOfObjectsHeader[h].Supp_id;
        singleObj['Is_used'] = listOfObjectsHeader[h].Is_check;
        singleObj['Col_num'] = h;
        singleObj['Supp_nm'] = listOfObjectsHeader[h].Supp_nm;
        singleObj['Desc'] = y.rows[1].cells[h+1].children.textarea.value;

        listOfObjectsFooter.push(singleObj)
    }

    //SAVE BODY INTO CLASS OBJECT JAVASCRIPT
    var x = document.getElementById('dataTable');
    for (var i = 0; i < x.rows.length; i++) {
        var singleObj = {}
        singleObj['Type'] = 'body';
        singleObj['Row'] = i;

        singleObj['Item'] = x.rows[i].cells[1].children[0].value;
        singleObj['Um'] = x.rows[i].cells[2].children[0].value;
        singleObj['Qty'] = x.rows[i].cells[3].children[0].value;
        singleObj['Price'] = x.rows[i].cells[4].children[0].value;
        singleObj['Total'] = x.rows[i].cells[5].children[0].value;


        listOfObjectsBody.push(singleObj)

        for (var j = 0; j < listOfObjectsHeader.length; j++) {
            var singleObjMap = {}
            singleObjMap['Type'] = 'map';
            singleObjMap['Row'] = i;

            singleObjMap['Supp_each_price'] = x.rows[i].cells[7 + j].children[0].cells[0].children[0].value;
            singleObjMap['Supp_total_price'] = x.rows[i].cells[7 + j].children[0].cells[1].children[0].value;
            singleObjMap['Header'] = listOfObjectsHeader[j];

            listMappingSuppItem.push(singleObjMap)
        }
    }
}

function CalcFinalTotal() {
    txt_grand_total = $("#txt_grand_total").val().replace(/,/g, "");
    txt_add_cost = $("#txt_add_cost").val().replace(/,/g, "");

    var result = parseInt(txt_grand_total) - parseInt(txt_add_cost);

    //set final total
    var final_total = document.getElementById('txt_final_total');
    final_total.value = Comma(result.toString());
}

function CalcAddCost(row) {
    var add_cost_ = row.value;

    var add_cost_rp = document.getElementById("txt_add_cost");

    var r = add_cost_;

    add_cost_rp.value = Comma(r.toString());
}


//=======================================================================end==========================================================================

