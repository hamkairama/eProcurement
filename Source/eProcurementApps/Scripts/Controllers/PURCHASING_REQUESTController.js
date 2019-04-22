//=============================================================start public parameters==========================
var iRowItem;
var isValidItem;
var msgValidItem = "";
var fData = new FormData();
//=====================================================================end=====================================

function BtnAction(action) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                ActionCreate();
            }
        }
    }
}

function CheckData(action) {
    txt_is_popup_coa = $("#txt_is_popup_coa").text();
    txt_count_code = $("#txt_count_code").text();
    if ((txt_is_popup_coa == 1 && txt_count_code == 0) || (txt_is_popup_coa == 1 && txt_count_code == "")) {
        alert("select the account code");
    } else {
        msgValidItem = "";
        var txt_user_id2 = $("#txt_user_id2").text();
        var txt_id = 0;
        if (action == "edit") {
            txt_id = $("#txt_id").text();
        };
        $.ajax({
            url: '../USER/CheckData',
            type: "Post",
            data: {
                id: txt_id,
                user_id: txt_user_id2,
            },
            cache: false,
            traditional: true,
            beforeSend: function () {
                document.getElementById("loader").className = "loader";
            },
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                if (action == "create") {
                    ActionCreate();
                    document.getElementById("loader").className = "";
                }
            }
        })
    }
}

function ActionCreate() {
    txt_cb_form_type_id = $("#txt_cb_form_type_id").text();
    txt_delivery_days = $("#txt_delivery_days").text();
    txt_count_code = $("#txt_count_code").text();
    txt_user_id_id = $("#txt_user_id_id").text();
    txt_cb_sub_type_id = $("#txt_cb_sub_type_id").text();
    txt_request_dt = ConvertDate($("#txt_request_dt").text());
    txt_cb_gt_id = $("#txt_cb_gt_id").text();
    txt_req_indicator = $("#txt_req_indicator").text();
    txt_dev_dt_new = ConvertDate($("#txt_dev_dt_new").text());

    sub_total_price = $("#txt_sub_total_price").text();
    txt_sub_total_price = sub_total_price.replace(/,/g, ''); //delete comma to passing integer

    var msg_error = document.getElementById('msg_error');
    var item_detail = new Array();
    item_detail = GetDataTable();

    var txt_for_storage;
    $("#cb_for_storage").is(":checked") ? txt_for_storage = 1 : txt_for_storage = 0;

    var txt_count_code_start = $('#txt_budget_code_start').text()  ;
    var txt_count_code_end = $('#txt_budget_code_end').text();

    if (isValidItem) {
        var result = $.xResponse('../PURCHASING_REQUEST/ActionCreate/', {
            user_id_id: txt_user_id_id,
            request_dt: txt_request_dt,
            cb_form_type_id: txt_cb_form_type_id,
            cb_sub_type_id: txt_cb_sub_type_id,
            cb_gt_id: txt_cb_gt_id,
            delivery_days: txt_delivery_days,
            dev_dt_new: txt_dev_dt_new,
            req_indicator: txt_req_indicator,
            count_code: txt_count_code,
            sub_total_price: txt_sub_total_price,

            litem_detail: item_detail,
            for_storage: txt_for_storage,
            count_code_start: txt_count_code_start , 
            count_code_end: txt_count_code_end,
        });

        var arry_result = result.split('|');
        if (arry_result[0] == 'False') {
            msg_error.style.display = '';
            msg_error.innerHTML = "Information : " + "<br />" + arry_result[1];
        } else {
            msg_error.style.display = 'none';
            Refresh();
        }
    } else {
        msg_error.style.display = '';
        msg_error.innerHTML = "Information : " + "<br />" + msgValidItem;
    }
}

$(function () {
    $('input[type="file"][multiple]').change(
         function (e) {
             var files = jQuery("#file-1").get(0).files[0];

             if (files != null) {
                 fData.append("file", files);
             } else {
                 fData = new FormData();
             }

             $.ajax({
                 cache: false,
                 url: "../PURCHASING_REQUEST/GetFormData",
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

         });
});

function GenerateFile() {
    //$.ajax({
    //    cache: false,
    //    url: "../PURCHASING_REQUEST/GetFormData",
    //    type: "POST",
    //data: function () {
    //    var data = new FormData();
    //    data = fData
    //    return data;
    //}(),
    //    contentType: false,
    //    processData: false,
    //    success: function (response) {
    //    },
    //    error: function (jqXHR, textStatus, errorMessage) {
    //        console.log(errorMessage);
    //    }
    //});

    result = $.xResponseDataFile(linkProc + '/PURCHASING_REQUEST/GetFormData');

    //var result = $.xResponse(linkProc + '/PURCHASING_REQUEST/GetFormData',
    //    {
    //        function () {
    //            var data = new FormData();
    //            data = fData;
    //            return data
    //        }
    //    });
    return result;
}

function GetDataTable() {
    var oTable = document.getElementById('dataTable');
    var rowLength = oTable.rows.length;
    var temp = new Array();
    for (i = 0; i < rowLength; i++) {
        var oCells = oTable.rows.item(i).cells;
        var cellLength = oCells.length - 1;
        var columnNumber = i + 1; // + 1 for to get datatable number1

        var cellItemName = "";//ITEM_NAME
        var cellSpecification = "";//SPECIFICATION
        var cellCurrency = "";//CURRENCY
        var cellQty = "";//QTY
        var cellUserMeasurement = "";//USER_MEASUREMENT
        var cellRevisedQty = "";//REVISED_QTY
        var cellPrice = "";//PRICE
        var cellTotalPrice = "";//TOTAL_PRICE
        var cellRemark = "";//REMARK
        var cellPoNameSuppName = "";//PO_NAME_SUPPNAME
        var cellWaId = "";//WA_ID
        var cellWaName = "";//WA_NAME
        var cellWaNameFor199 = ""//WA_NAME_FOR_199
        var cellItemCode = "";//ITEM_CODE
        var cellConvertion = "";//CONVERTION
        var cellItemId = "";//WA_ITEM_ID
        var cellChooseOneApproval = "";//CHOOSE_ONE_APPROVAL

        for (var j = 0; j < cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;

            if (j == 1) {
                cellItemName = value;
            }
            else if (j == 2) {
                cellSpecification = value;
            }
            else if (j == 3) {
                var selectTextId = document.getElementById(oCells.item(j).firstElementChild.id);
                cellCurrency = selectTextId.options[selectTextId.selectedIndex].text;
            }
            else if (j == 4) {
                cellQty = value == '' ? '0' : value;
            }
            else if (j == 5) {
                cellUserMeasurement = value;
            }

            else if (j == 8) {
                //cellRevisedQty = value == '' ? '0' : value;
                cellRevisedQty = value;
            }
            else if (j == 9) {
                cellPrice = value == '' ? '0' : value;
            }
            else if (j == 10) {
                cellTotalPrice = value;
            }
            else if (j == 11) {
                cellRemark = value;
            }
            else if (j == 12) {
                cellPoNameSuppName = value;
            }
            else if (j == 13) {
                cellWaId = value == '' ? '0' : value;
            }
                //passing wa name for check budget code param of wa
            else if (j == 7) {
                cellWaName = value;
            }
                //passing wa name for check budget code param of wa 199
            else if (j == 6) {
                //using for get 199
                var selectTextId = document.getElementById(oCells.item(j).firstElementChild.id);
                cellWaNameFor199 = selectTextId.options[selectTextId.selectedIndex].text;
            }
            else if (j == 14) {
                cellItemCode = value;
            }
            else if (j == 15) {
                cellConvertion = value;
            }
            else if (j == 16) {
                cellItemId = value;
            }
            else if (j == 18) {
                cellChooseOneApproval = value;
            }
        }

        if (cellItemName != "" && cellQty != "0" && cellPrice != "0" && cellTotalPrice != 0 && cellWaId != 0 && cellConvertion != 0 && cellChooseOneApproval != "") {
            temp.push(cellItemName + "|" + cellSpecification + "|" + cellCurrency + "|" + cellQty.replace(/,/g, '') + "|" + cellUserMeasurement.trim() + "|" + cellRevisedQty.replace(/,/g, '')
           + "|" + cellPrice.replace(/,/g, '') + "|" + cellTotalPrice.replace(/,/g, '') + "|" + cellRemark + "|" + cellPoNameSuppName + "|" + cellWaId + "|" + cellWaName + "|"
           + cellWaNameFor199 + "|" + cellItemCode + "|" + cellConvertion.replace(/,/g, '') + "|" + cellItemId + "|" + cellChooseOneApproval + "|");
            isValidItem = true;
            msgValidItem = "";
        } else {
            isValidItem = false;
            msgValidItem = msgValidItem + "Item number " + columnNumber + " is not complete. Field with asterik (*) is mandatory. For set approval of WA in each of input item, please use button '<a class='red' href='#' onclick='ChooseOneApproval()' title='Choose One Approval'>Choose One Approval'</a>. <br />";
            break;
        }

        if (cellItemName.length > 50 || cellSpecification.length > 100) {
            isValidItem = false;
            msgValidItem = msgValidItem + "Item number " + columnNumber + " is not complete. Max item name is 50 Chars & max specification is 100 chars. <br />";
            break;
        }

    }
    return temp;
}

function Refresh() {
    //$.ajax({
    //    cache: false,
    //    url: '../USER/Index',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    window.location.reload(true);
}

function Generate() {
    var input = [{ key: "key1", value: "value1" }, { key: "key2", value: "value2" }];
    var result = {};

    for (var i = 0; i < input.length; i++) {
        result[input[i].key] = input[i].value;
    }
}

function GetDataWA() {
    var fields = $(".chosen-choices").find(".search-choice span");
    var temp = new Array();

    for (i = 0; i < fields.length; ++i) {
        var field = fields[i];
        temp.push(parseInt(field.innerHTML));
    };

    return temp;
}

$(function () {
    $('#dropdownList_fst').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        var arry = optionSelected.split("|");

        $('#txt_cb_sub_type_id').text(arry[0]);
        $('#txt_delivery_days').text(arry[1]);
        $('#txt_is_popup_coa').text(arry[2]);

        //$('#txt_count_code').text(arry[3]);
        //$('#txt_usage_code').text(arry[3]);
        //$('#txt_budget_code_start').text(arry[4]);
        //$('#txt_budget_code_end').text(arry[5]);
     
        var result = $.xResponse('../PURCHASING_REQUEST/GetExpectedDt/', { count_days: arry[1] });

        $('#txt_dev_dt_orin').text(result);
        $('#txt_dev_dt_new').text(result);

        BtnGetReqIndicator();
        GetRelDeptList(arry[0]);

        var fstx = arry[0];
        if (fstx != 0){
            var result2 = $.xResponseDataFstBc('../PURCHASING_REQUEST/GetFstBcFromid/', { form_sub_type_id: fstx });
        }
    });
});


$(function () {
    $('#dropdownList_ft').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        GetSubType(optionSelected)
    });
});


$(function () {
    $('#dropdownList_coa').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_count_code').text(optionSelected);
    });
});


$.extend({
    //    xResponse: function (url, data) {
    //        // local var
    //        var theResponse = null;
    //        // jQuery ajax
    //        $.ajax({
    //            url: url,
    //            cache: false,
    //            traditional: true,
    //            type: 'POST',
    //            data: data,
    //            dataType: "html",
    //            async: false,
    //            success: function (respText) {
    //                theResponse = respText;
    //            }
    //        });
    //        // Return the response text
    //        return theResponse;
    //    },

    xResponseDataFst: function (url, data) {
        $.ajax({
            url: url,
            data: data,
            cache: false,
            traditional: true,
            type: "POST",
            success:
                function (data) {
                    var markup;
                    for (var x = 0; x < data.length; x++) {
                        markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                    }
                    $("#dropdownList_fst").html(markup).show();
                },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    },

    xResponseDataFstBc: function (url, data) {
        $.ajax({
            url: url,
            data: data,
            cache: false,
            traditional: true,
            type: "POST",
            success:
                function (data) {
                    var markup;
                    for (var x = 0; x < data.length; x++) {
                        markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                    }
                    $("#dropdownList_fst_bc").html(markup).show();
                },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    },

    xResponseDataFile: function (url) {
        // local var
        var theResponse = null;
        // jQuery ajax
        $.ajax({
            url: url,
            cache: false,
            traditional: true,
            type: 'POST',
            data: fData,
            //data: function () {
            //    var data = new FormData();
            //    data = fData
            //    return data;
            //}(),
            contentType: false,
            processData: false,
            success: function (respText) {
                theResponse = respText;
            }
        });
        // Return the response text
        return theResponse;
    },

    //$.ajax({
    //    cache: false,
    //    url: "../PURCHASING_REQUEST/GetFormData",
    //    type: "POST",
    //    data: function () {
    //        var data = new FormData();
    //        data = fData
    //        return data;
    //    }(),
    //    contentType: false,
    //    processData: false,
    //    success: function (response) {
    //    },
    //    error: function (jqXHR, textStatus, errorMessage) {
    //        console.log(errorMessage);
    //    }
    //});

});

$(function () {
    $('#txt_dev_dt_new').on('DOMSubtreeModified', function () {
        BtnGetReqIndicator();
    })
});


$(function () {
    $('#dropdownList_gt').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_cb_gt_id').text(optionSelected);
        RefreshTable('dataTable');
        $('#txt_sub_total_price').text("");

        //validation for promotional Item non stock. can freetext
        var ft = $("#dropdownList_ft :selected").text();
        var gt = $("#dropdownList_gt :selected").text();

        if (ft == "Promotional Item" && gt == "Non Stock") {
            TextboxReadOnly(false);
        }

    });
});

function getWaOrigin(row) {
    iRowItem = GetRowIndex(row) - 1;
    var r_wa, r_wa_id;

    var optionSelected = $(row).find('option:selected').text();
    var optionSelectedVal = row.value;

    if (optionSelected == "199") {
        var url = '../WA/PopWA/';
        var form = '.dialogForm';
        var id = document.getElementById("txt_user_id_id").innerText;

        ModalPop(url, parseInt(id), form);
    } else {
        if (iRowItem == 0) {
            r_wa = document.getElementById("wa_orin_");
            r_wa_id = document.getElementById("txt_id_wa_");
        }
        else {
            r_wa = document.getElementById("wa_orin_" + iRowItem);
            r_wa_id = document.getElementById("txt_id_wa_" + iRowItem);
        }
        r_wa.value = optionSelected;
        r_wa_id.value = optionSelectedVal;
    }

    CleanOneApproval();
}

function CleanOneApproval() {
    var r_appr;
    if (iRowItem == 0) {
        r_appr = document.getElementById("txt_approval_pr_");
    } else {
        r_appr = document.getElementById("txt_approval_pr_" + iRowItem);
    }

    r_appr.value = "";
}

function PassingDataWAOrigin(no) {
    var wa_pass = document.getElementById("txt_wa_" + no).innerText
    var wa_id_pass = document.getElementById("txt_wa_id_" + no).innerText;
    var r_wa, r_wa_id;

    if (iRowItem == 0) {
        r_wa = document.getElementById("wa_orin_");
        r_wa_id = document.getElementById("txt_id_wa_");
    }
    else {
        r_wa = document.getElementById("wa_orin_" + iRowItem);
        r_wa_id = document.getElementById("txt_id_wa_" + iRowItem);
    }
    r_wa.value = wa_pass;
    r_wa_id.value = wa_id_pass;
    $(".dialogForm").dialog("close");
}

function getItem(row) {
    iRowItem = GetRowIndex(row) - 1;
    var id_gtx = document.getElementById("txt_cb_gt_id").innerText;
    var id_ftx = document.getElementById("txt_cb_form_type_id").innerText;
    if (id_gtx > 0) {
        $.ajax({
            url: linkProc + '/STOCK/PopStock/',
            cache: false,
            traditional: true,
            "_": $.now(),
            data: {
                id_gt: id_gtx,
                id_ft: id_ftx,
            },
            success: function (data) {
                $(".dialogForm").html(data).dialog("open");
                $(".dialogForm").html(data).dialog({ draggable: false }).parent().draggable();
            }
        });
    }
}

function PassingDataItem(no) {
    var item_cd = document.getElementById("txt_item_cd_" + no).innerText;
    var item_desc = document.getElementById("txt_item_desc_" + no).innerText;
    var item_uos = document.getElementById("txt_uos_" + no).innerText;
    var item_lcost = document.getElementById("txt_lcost_" + no).innerText;
    var item_id = document.getElementById("txt_id_stk_" + no).innerText;
    var qty_storage = document.getElementById("txt_qty_storage_" + no).innerText;

    var r_item_cd, r_item_name, r_qty, r_unit_measurement, r_price, r_total_price, r_item_id, r_qty_storage;

    if (iRowItem == 0) {
        r_item_cd = document.getElementById("txt_item_cd_");
        r_item_name = document.getElementById("txt_item_name_");
        r_unit_measurement = document.getElementById("txt_unit_measurement_");
        r_price = document.getElementById("txt_price_");
        r_total_price = document.getElementById("txt_total_price_");
        r_qty = document.getElementById("txt_qty_");
        r_item_id = document.getElementById("txt_item_id_");
        r_qty_storage = document.getElementById("txt_qty_storage_");
    }
    else {
        r_item_cd = document.getElementById("txt_item_cd_" + iRowItem);
        r_item_name = document.getElementById("txt_item_name_" + iRowItem);
        r_unit_measurement = document.getElementById("txt_unit_measurement_" + iRowItem);
        r_price = document.getElementById("txt_price_" + iRowItem);
        r_total_price = document.getElementById("txt_total_price_" + iRowItem);
        r_qty = document.getElementById("txt_qty_" + iRowItem);
        r_item_id = document.getElementById("txt_item_id_" + iRowItem);
        r_qty_storage = document.getElementById("txt_qty_storage_" + iRowItem);
    }

    //validation if good type : stock <= 0. return false
    var gt = $("#dropdownList_gt :selected").text();

    //validation if item code is false, because data already passing in other row
    //if (item_cd == "") {
    //    $(".dialogForm").dialog("close");
    //    alert("Record not have item code or record has been input");
    //    return false;
    //}

    //if (gt == "Stock" && parseFloat(qty_storage) <= 0) {
    //    $(".dialogForm").dialog("close");
    //    alert("Quantity of storage is unavailable");
    //    return false;
    //}

    r_item_cd.value = item_cd;
    r_item_name.value = item_desc;
    r_unit_measurement.value = item_uos;
    r_price.value = Comma(item_lcost);
    r_total_price.value = 0;
    r_qty.value = 0;
    r_item_id.value = item_id;
    r_qty_storage.value = qty_storage;

    //refresh subtotal
    var subtot = CalcAll();
    $('#txt_sub_total_price').text(Comma(subtot.toString()));

    $(".dialogForm").dialog("close");
}

function GenerateImage() {
    $.ajax({
        cache: false,
        url: "/User/GetFormData",
        type: "POST",
        data: function () {
            var data = new FormData();
            data.append("file", jQuery("#id-input-file-3").get(0).files[0]);
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

function GetSubType(_formId) {
    RefreshTable('dataTable');
    RefreshBudgetInformation();
    $('#txt_sub_total_price').text("");

    var cb_fst = document.getElementById('dropdownList_fst');
    var cb_gt = document.getElementById('dropdownList_gt');
    var id_gt = document.getElementById("txt_cb_gt_id");
    var id_ft = document.getElementById("txt_cb_form_type_id");

    if (_formId > 0) {
        //fst
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#dropdownList_fst").html(procemessage).show();
        cb_fst.disabled = false;
        var result = $.xResponseDataFst('../PURCHASING_REQUEST/GetFormSubTypeFromId/', { form_type_id: _formId });       

        //gt
        var is_gt = $.xResponse('../PURCHASING_REQUEST/GetIsGoodType/', { id_ft: _formId });
        if (is_gt == 0) {
            cb_gt.disabled = true;
            cb_gt.selectedIndex = 0;
            id_gt.innerText = 0;
            //show_hide_column(true);
            TextboxReadOnly(false)
        } else {
            cb_gt.disabled = false;
            //show_hide_column(false);
            TextboxReadOnly(true)
        }
    } else {
        cb_fst.selectedIndex = "0";
        cb_gt.selectedIndex = "0";
        cb_fst.disabled = true;
        cb_gt.disabled = true;
        id_gt.innerText = 0;
    }

    id_ft.innerText = _formId;
    GetRelDeptList(0);//passing 0 for nothing return
    ClearValueInput(); //clear the value input item (all) still not work
}

function TextboxReadOnly(isReadOnly) {
    $("#txt_price_").attr("readonly", isReadOnly);
    $("#txt_specification_").attr("readonly", isReadOnly);
    $("#txt_item_name_").attr("readonly", isReadOnly);

    if (isReadOnly) {
        document.getElementById("dropdownList_gt").style.display = "";
    } else {
        document.getElementById("dropdownList_gt").style.display = "none";
    }

    $("#txt_qty_").attr("readonly", false);
}

function RefreshBudgetInformation() {
    $('#txt_usage_code').text('');
    $('#txt_budget_code_start').text('');
    $('#txt_budget_code_end').text('');
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

function show_hide_column(isShow) {
    var rows = document.getElementById('table_input').rows;

    for (var row = 0; row < rows.length; row++) {
        var cols = rows[row].cells;
        cols[2].style.display = isShow ? '' : 'none';
        cols[3].style.display = isShow ? '' : 'none';
        cols[4].style.display = isShow ? '' : 'none';
    }
}

window.onload = function () {
    var cb_fst = document.getElementById('dropdownList_fst');
    var cb_gt = document.getElementById('dropdownList_gt');
    cb_fst.disabled = true;
    cb_gt.disabled = true;
};

function GetRelDeptList(form_sub_type_id) {
    $.ajax({
        data: {
            form_sub_type_id: form_sub_type_id
        },
        cache: false,
        url: '../PURCHASING_REQUEST/ListRelDept',
        success: function (data) {
            $("#dept_id_table").html(data);
        }
    });
}

function CalcItem(row) {
    iRowItem = GetRowIndex(row) - 1;
    var qty = row.value;
    var result, pricerr, priceTotal, convert;
    var qty_storage, item_id, approval;

    if (iRowItem == 0) {
        pricerr = document.getElementById("txt_price_");
        priceTotal = document.getElementById('txt_total_price_');
        qty_storage = document.getElementById("txt_qty_storage_");
        item_id = document.getElementById("txt_item_id_");
        convert = document.getElementById("txt_convertion_");
        approval = document.getElementById("txt_approval_pr_");
    }
    else {
        pricerr = document.getElementById("txt_price_" + iRowItem);
        priceTotal = document.getElementById('txt_total_price_' + iRowItem);
        qty_storage = document.getElementById("txt_qty_storage_" + iRowItem);
        item_id = document.getElementById("txt_item_id_" + iRowItem);
        convert = document.getElementById("txt_convertion_" + iRowItem);
        approval = document.getElementById("txt_approval_pr_" + iRowItem);
    }


    //return false if input quantity more than storage quantity
    var txt_for_storage;
    $("#cb_for_storage").is(":checked") ? txt_for_storage = 1 : txt_for_storage = 0;
    cb_dropdownList_gt = $("#dropdownList_gt :selected").text();


    var qty_serv = $.xResponse(linkProc + '/PURCHASING_REQUEST/GetQtyItem/', {
        id: item_id.value
    });


    if ((parseFloat(qty) > parseFloat(qty_serv)) && txt_for_storage == 0 && cb_dropdownList_gt == "Stock") {
        row.value = 0;
        qty = row.value;
        alert("Quantity of storage is " + qty_serv + ". Please input less than quantity of Storage");

        var r = parseFloat(qty) * parseFloat(pricerr.value.replace(/,/g, "")) * parseFloat(convert.value.replace(/,/g, ""));

        result = r.toString().replace(/,/g, "")
        result += '';
        x = result.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1))
            x1 = x1.replace(rgx, '$1' + ',' + '$2');

        x3 = x1 + x2;
        priceTotal.value = x3 == 'NaN' ? 0 : x3;


        var subtot = CalcAll();
        $('#txt_sub_total_price').text(Comma(subtot.toString()));

        return false;
    }


    var r = parseFloat(qty) * parseFloat(pricerr.value.replace(/,/g, "")) * parseFloat(convert.value.replace(/,/g, ""));;

    result = r.toString().replace(/,/g, "")
    result += '';
    x = result.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');

    x3 = x1 + x2;
    priceTotal.value = x3 == 'NaN' ? 0 : x3;


    var subtot = CalcAll();
    $('#txt_sub_total_price').text(Comma(subtot.toString()));
    approval.value = "";
}

function CalcItemByPrice(row) {
    iRowItem = GetRowIndex(row) - 1;
    var price = row.value;
    var result, qty, priceTotal, convertion, approval;

    if (iRowItem == 0) {
        qty = document.getElementById("txt_qty_");
        priceTotal = document.getElementById('txt_total_price_');
        convertion = document.getElementById("txt_convertion_"); 
        approval = document.getElementById("txt_approval_pr_");
    }
    else {
        qty = document.getElementById("txt_qty_" + iRowItem);
        priceTotal = document.getElementById('txt_total_price_' + iRowItem);
        convertion = document.getElementById("txt_convertion_" + iRowItem);
        approval = document.getElementById("txt_approval_pr_" + iRowItem);
    }

    var r = parseFloat(price) * parseFloat(qty.value.replace(/,/g, "")) * parseFloat(convertion.value.replace(/,/g, ""));

    result = r.toString().replace(/,/g, "")
    result += '';
    x = result.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');

    x3 = x1 + x2;
    priceTotal.value = x3 == 'NaN' ? 0 : x3;


    var subtot = CalcAll();
    $('#txt_sub_total_price').text(Comma(subtot.toString()));
    approval.value = "";
}

function CalcAll() {
    var x = document.getElementById('dataTable');
    var result = 0;
    for (var i = 0; i < x.rows.length; i++) {
        var qty = x.rows[i].cells[4].childNodes[0].value.replace(/,/g, ""); //qty
        var price = x.rows[i].cells[9].childNodes[0].value.replace(/,/g, ""); //@price
        var converter = x.rows[i].cells[15].childNodes[0].value.replace(/,/g, ""); //converter
        if (qty > 0 && price > 0 && converter != 0 && converter != "") {
            rc = parseFloat(qty) * parseFloat(price) * parseFloat(converter);
            result = result + rc;
        }
    }
    return result;
}

function BtnGetReqIndicator() {
    var dt_origin = $('#txt_dev_dt_orin').text();
    var dt_new = $('#txt_dev_dt_new').text();

    if (dt_new != 'Empty') {
        GetReqIndicator(dt_origin, dt_new);
    }
}

function GetReqIndicator(dt_origin, dt_new) {
    var new_dt_origin = ConvertDateToJsFormat(dt_origin);
    var new_dt_new = ConvertDateToJsFormat(dt_new);

    if (new_dt_origin > new_dt_new) {
        $('#txt_req_indicator').text('ExtraMiles')
    } else if ((new_dt_origin.getDate() == new_dt_new.getDate()) && (new_dt_origin.getMonth() == new_dt_new.getMonth()) && (new_dt_origin.getYear() == new_dt_new.getYear())) {
        $('#txt_req_indicator').text('Standard')
    } else {
        $('#txt_req_indicator').text('Ideal')
    }
}

function SearchStock() {
    txt_item_desc = $("#txt_search_stock").val();
    txt_id_gt = $("#txt_id_gt").text(); //from pop up
    txt_id_ft = $("#txt_id_ft").text(); //from pop up
    $.ajax({
        url: linkProc + '/STOCK/PopSearch',
        cache: false,
        traditional: true,
        "_": $.now(),
        data: {
            item_desc: txt_item_desc,
            id_gt: parseInt(txt_id_gt),
            id_ft: parseInt(txt_id_ft),
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success: function (data) {
            $("#table_ad").html(data);
            $("#loadingRole").toggle()
        }
    });
}

function txtSearchStock(event) {
    if (event.keyCode == 13) {
        SearchStock();
    };
};

$('.toolTips').keyup(function () {
    $(this).attr('title', $(this).val());
});

function ActionEmailCreateUser() {
    var wa = $("#form-field-select-4").val();
    if (wa == null) {
        $("#form-field-select-4").val("").focus();
    } else {
        SendEmailCreateUser(wa.toString());
    }
}

function SendEmailCreateUser(txt_wa) {
    $.ajax({
        url: '../PURCHASING_REQUEST/SendEmailCreateUser',
        type: 'Post',
        data: {
            wa: txt_wa,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                document.getElementById("loader").className = "loader";
            },
        error: function (response) {
            alert("Failed to send email");
        },
        success:
            function () {
                document.getElementById("loader").className = "";
                document.getElementById("email_sent").style.visibility = "visible";
                document.getElementById("input_wa").style.display = "none";
                document.getElementById("action_send_email").style.display = "none";
            },
    });
}

function getConvertionCurr(row) {
    iRowItem = GetRowIndex(row) - 1;
    var r_qty, r_pricerr, r_priceTotal, r_convertion, approval;

    var optionSelected = $(row).find('option:selected').text();
    var optionSelectedVal = row.value;

    if (iRowItem == 0) {
        r_qty = document.getElementById("txt_qty_");
        r_pricerr = document.getElementById("txt_price_");
        r_priceTotal = document.getElementById('txt_total_price_');
        r_convertion = document.getElementById("txt_convertion_");
        approval = document.getElementById("txt_approval_pr_");
    }
    else {
        r_qty = document.getElementById("txt_qty_" + iRowItem);
        r_pricerr = document.getElementById("txt_price_" + iRowItem);
        r_priceTotal = document.getElementById('txt_total_price_' + iRowItem);
        r_convertion = document.getElementById("txt_convertion_" + iRowItem);
        approval = document.getElementById("txt_approval_pr_" + iRowItem);
    }

    if (optionSelectedVal == 0 || optionSelectedVal == "") {
        r_qty.value = "";
        r_qty.readOnly = true;
    } else {
        r_qty.readOnly = false;
    }


    var r = parseFloat(r_qty.value) * parseFloat(r_pricerr.value.replace(/,/g, "")) * parseFloat(optionSelectedVal.replace(/,/g, ""));;

    result = r.toString().replace(/,/g, "")
    result += '';
    x = result.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');

    x3 = x1 + x2;
    r_priceTotal.value = x3 == 'NaN' ? 0 : x3;


    r_convertion.value = Comma(optionSelectedVal);
    var subtot = CalcAll();
    $('#txt_sub_total_price').text(Comma(subtot.toString()));
    approval.value = "";
}

function ChooseOneApproval() {
    var url = linkProc + '/PURCHASING_REQUEST/PopupChooseOneApproval/';
    var form = '.dialogForm';

    ModalPop2(url, form);
}

function GetWaId(row) {
    iRowItem = GetRowIndex(row) - 1;
    var val = row.value;
    var dt = val.split("-");

    GetApprovalWa(dt[0]);
    SetWaIdNWaNumb(dt[0], dt[1])
}

function ClosePopupAck() {
    //$("#cb_is_acknowledge_user").prop("checked", false)
    $(".dialogForm").dialog("close");
}

var listOfObjectsWa = [];
function GetWaApproval() {
    var y = document.getElementById('tableChooseOneApproval');
    var wa_apprv_empty = 0;

    listOfObjectsWa = [];
    for (var i = 1; i < y.rows.length; i++) {

        var singleObj = {}
        singleObj['Wa_id'] = y.rows[i].cells['0'].childNodes['0'].value;
        singleObj['Wa_number'] = y.rows[i].cells['1'].childNodes['0'].value;
        singleObj['Wa_approval'] = y.rows[i].cells['3'].childNodes['0'].value;

        //<<zoer20170925
        if (singleObj['Wa_approval'] == "" || singleObj['Wa_approval'] == null) {
            wa_apprv_empty = 1;
        } else {
            listOfObjectsWa.push(singleObj);
        }
        //>>zoer20170925
    }

    //<<zoer20170925
    if (wa_apprv_empty == 0) {
        MappingApprovalWithItem();
        $(".dialogForm").dialog("close");
    } else {
        alert("Approval cannot be empty");
    }
    //>>zoer20170925
}


function MappingApprovalWithItem() {
    var oTable = document.getElementById('dataTable');
    var rowLength = oTable.rows.length;
   
    for (var j = 0; j < listOfObjectsWa.length; j++) {
        var wa_id = listOfObjectsWa[j].Wa_id;
        var app_r = listOfObjectsWa[j].Wa_approval;

        for (i = 0; i < rowLength; i++) {
            var oCells = oTable.rows.item(i).cells;
            var cellLength = oCells.length - 1;
            var columnNumber = i + 1; // + 1 for to get datatable number1

            var value_wa_id = oCells.item(13).firstElementChild.value; //wa_ID
            if (value_wa_id.trim() == wa_id.trim()) {
                var approval = oCells.item(18).firstElementChild; //total_price
                approval.value = app_r;
            }
        }
    }

    
}

function GetApprovalWa(wa_id) {
    var r_total_price_wa = GetTotalPriceWA(wa_id)
    if (r_total_price_wa > 0) {
        $.ajax({
            data: {
                wa_id: wa_id,
                total_price_wa: r_total_price_wa
            },
            cache: false,
            url: linkProc + '/PURCHASING_REQUEST/ListApprovalWa',
            success: function (data) {
                $("#wa_id_table").html(data);
            }
        });
    } else {
        $("#wa_id_table").html("");
    }
}

function GetTotalPriceWA(wa_id) {
    var oTable = document.getElementById('dataTable');
    var rowLength = oTable.rows.length;
    var total_price_all_wa = 0;

    for (i = 0; i < rowLength; i++) {
        var oCells = oTable.rows.item(i).cells;
        var cellLength = oCells.length - 1;
        var columnNumber = i + 1; // + 1 for to get datatable number1
        
        var value_wa_id = oCells.item(13).firstElementChild.value; //wa_ID
        if (value_wa_id.trim() == wa_id.trim()) {
            var value_total_price = oCells.item(10).firstElementChild.value; //total_price
            total_price_all_wa = total_price_all_wa + parseFloat(value_total_price.replace(/,/g, ''));
        }
    }

    return total_price_all_wa;
}


function SetWaIdNWaNumb(wa_id, wa_numb) {
    var r_wa_id, r_wa_numb;
    if (iRowItem == 0) {
        r_wa_id = document.getElementById("txt_wa_id_");
        r_wa_numb = document.getElementById("txt_wa_numb_");

    } else {
        r_wa_id = document.getElementById("txt_wa_id_" + iRowItem);
        r_wa_numb = document.getElementById("txt_wa_numb_" + iRowItem);

    }

    r_wa_id.value = wa_id;
    r_wa_numb.value = wa_numb;
}

function PassingApprovalToRow() {
    var concat = GetMenusChecked();

    var r_appr
    if (iRowItem == 0) {
        r_appr = document.getElementById("txt_approval_");

    } else {
        r_appr = document.getElementById("txt_approval_" + iRowItem);

    }

    r_appr.value = concat;
}

function GetMenusChecked() {
    var fields = $(".appx").find("input");

    var apprls = "";

    for (i = 0; i < fields.length; ++i) {
        var item = fields[i]
        if (item.checked) {
            var y = item.id;
            apprls = y;
            //menus.push(item.id);
            //menus.push(item.className)
        }

    };

    return apprls;
}

$(function () {
    $('#dropdownList_fst_bc').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        var arry = optionSelected.split("^");

        if (optionSelected == "0") {
            $('#txt_count_code').text("");
            $('#txt_usage_code').text("");
            $('#txt_budget_code_start').text("");
            $('#txt_budget_code_end').text("");
        } else {
            $('#txt_count_code').text(arry[0]);
            $('#txt_usage_code').text(arry[0]);
            $('#txt_budget_code_start').text(arry[1]);
            $('#txt_budget_code_end').text(arry[2]);
        }
    });
});