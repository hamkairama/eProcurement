//start public parameters
var iRowItemDt;
var isValidItem = true;
var msgValidItem = "";
var fData = new FormData();

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

function GetRowIndexDt(el) {
    while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

    return el.rowIndex;
}

$(function () {
    $('#dropdownList_itempo_nm').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        var arry_result = optionSelected.split('|');
        document.getElementById("txt_cb_itempo_nm").innerHTML = arry_result[2];
        document.getElementById("txt_cb_itempo_id").innerHTML = arry_result[0];
        //window.alert(arry_result[2]);
        var base_from = $("#txt_base_from").text();
        GetItemsPOPC(arry_result[2], base_from);
    });
});

function GetItemsPOPC(_itemPo, flag) {
    //window.alert("====Masuk GetItemPO============== "+_itemPo);
    var cb_itempo = document.getElementById('dropdownList_itempo_nm');
    var cb_prnumber = document.getElementById('dropdownList_prnumber');
    var txt_itempo = document.getElementById("txt_cb_itempo_nm");

    if (_itemPo > 0 || _itemPo != null) {
        //window.alert("====Masuk GetItemPO IF==============");
        cb_itempo.disabled = false;

        //item
        var result = $.xResponse(linkProc + '/PURCHASE_ORDER/getValT/', {
            list_param1: "FUND_T1", list_param2: "LOB1_T2",
            list_param3: "PLAN_T3", list_param5: "LOB2_T5"
        });
        if (0 < result.length) {
            var arry_result = result.split('|');
            document.getElementById("txt_fund_t1_id").innerHTML = arry_result[0];
            document.getElementById("txt_fund_t1_nm").innerHTML = arry_result[1];
            document.getElementById("txt_lob1_t2_id").innerHTML = arry_result[2];
            document.getElementById("txt_lob1_t2_nm").innerHTML = arry_result[3];
            document.getElementById("txt_plan_t3_id").innerHTML = arry_result[4];
            document.getElementById("txt_plan_t3_nm").innerHTML = arry_result[5];
            document.getElementById("txt_lob2_t5_id").innerHTML = arry_result[6];
            document.getElementById("txt_lob2_t5_nm").innerHTML = arry_result[7];
        } else {
            document.getElementById("txt_fund_t1_id").innerHTML = "";
            document.getElementById("txt_fund_t1_nm").innerHTML = "";
            document.getElementById("txt_lob1_t2_id").innerHTML = "";
            document.getElementById("txt_lob1_t2_nm").innerHTML = "";
            document.getElementById("txt_plan_t3_id").innerHTML = "";
            document.getElementById("txt_plan_t3_nm").innerHTML = "";
            document.getElementById("txt_lob2_t5_id").innerHTML = "";
            document.getElementById("txt_lob2_t5_nm").innerHTML = "";
        }

        //po_number
        //window.alert("====Masuk GetItemPO po_number==============");
        var procemessage = "<option value='0'> Please wait...</option>";
        $("#dropdownList_prnumber").html(procemessage).show();
        cb_prnumber.disabled = false;
        var result = $.xResponseDataPrNumber(linkProc + '/PURCHASE_ORDER/GetListPrNumberId/', { pr_id: _itemPo }, flag);

    } else {
        //window.alert("====Masuk GetItemPO Else==============");
        cb_itempo.selectedIndex = "0";
        cb_itempo.disabled = false;
        cb_prnumber.disabled = true;
        document.getElementById("txt_fund_t1_id").innerHTML = "";
        document.getElementById("txt_fund_t1_nm").innerHTML = "";
        document.getElementById("txt_lob1_t2_id").innerHTML = "";
        document.getElementById("txt_lob1_t2_nm").innerHTML = "";
        document.getElementById("txt_plan_t3_id").innerHTML = "";
        document.getElementById("txt_plan_t3_nm").innerHTML = "";
        document.getElementById("txt_lob2_t5_id").innerHTML = "";
        document.getElementById("txt_lob2_t5_nm").innerHTML = "";
    }

    //txt_itempo.innerText = _itemPo;
    //GetRelDeptList(0);//passing 0 for nothing return
    ClearValueInput(); //clear the value input item (all) still not work
}

function CalTotalPricePO() {
    var x = document.getElementById('dataTablePODetail');
    var result = 0;
    var valQty = 0;
    var valPrice = 0;
    var valSum = 0;
    for (var i = 0; i < x.rows.length; i++) {
        var q = x.rows[i].cells[2].childNodes[0].value.replace(/,/g, "");
        var r = x.rows[i].cells[3].childNodes[0].value.replace(/,/g, "");

        if (q > 0) {
            valPrice = parseInt(q);
        }
        if (r > 0) {
            valQty = parseInt(r);
        }
        valSum = valPrice * valQty;
        result = result + valSum;
    }
    return result;
}

function PassingDataItemDetail(row) {
    iRowItemDt = GetRowIndexDt(row) - 1;
    var pr_number = row.value;

    var r_um, r_qty, r_price;
    var select_item = $("#dropdownList_itempo_nm :selected").text();
    var x = document.getElementById('dataTablePODetail');
    var len = x.rows.length - 1;

    if (iRowItemDt == 0) {
        r_um = document.getElementById("txt_um_detailpo_");
        r_qty = document.getElementById("txt_quantity_detailpo_");
        r_price = document.getElementById("txt_price_detailpo_");
        r_wa_number = document.getElementById("txt_wa_number_");

        if (pr_number != null && pr_number != "0") {
            var result = $.xResponse(linkProc + '/PURCHASE_ORDER/GetValPrDetailId/', {
                txt_prnumber_id: pr_number,
                txt_item_nm: select_item
            });

            var arry_result = result.split('|');
            if (arry_result[0] != null && arry_result[0] != "") {
                r_um.value = arry_result[1];
                r_qty.value = arry_result[2];
                r_price.value = arry_result[3];
                r_wa_number.value = arry_result[4];

                var subtot = CalTotalPricePO();
                $('#txt_sub_total_price_po').text(Comma(subtot.toString()));
            } else {
                r_um.value = "";
                r_qty.value = "";
                r_price.value = "";
                r_wa_number.value = "";
                //msg_error.style.display = 'none';
                Refresh();
            }
        } else {
            r_um.value = "";
            r_qty.value = "";
            r_price.value = "";
            r_wa_number.value = "";
            //msg_error.style.display = 'none';
            Refresh();
        };

    } else {
        r_um = document.getElementById("txt_um_detailpo_" + iRowItemDt);
        r_qty = document.getElementById("txt_quantity_detailpo_" + iRowItemDt);
        r_price = document.getElementById("txt_price_detailpo_" + iRowItemDt);
        r_wa_number = document.getElementById("txt_wa_number_" + iRowItemDt);

        if (pr_number != null && pr_number != "0") {
            var result = $.xResponse(linkProc + '/PURCHASE_ORDER/GetValPrDetailId/', {
                txt_prnumber_id: pr_number,
                txt_item_nm: select_item
            });

            var arry_result = result.split('|');
            if (arry_result[0] != null && arry_result[0] != "") {
                //window.alert("======arry_result[1] ======= " + arry_result[1] +"==r_um==== "+r_um)
                r_um.value = arry_result[1];
                r_qty.value = arry_result[2];
                r_price.value = arry_result[3];
                r_wa_number.value = arry_result[4];
                //window.alert("======Hasil R_ ======= " + r_um.value + " ====== " + r_qty.value + " ====== " + r_price.value)
                //msg_error.style.display = '';
                //calculation total price in PO
                var subtot = CalTotalPricePO();
                $('#txt_sub_total_price_po').text(Comma(subtot.toString()));
            } else {
                r_um.value = "";
                r_qty.value = "";
                r_price.value = "";
                r_wa_number.value = "";
                //msg_error.style.display = 'none';
                Refresh();
            }
        } else {
            //window.alert("======ELSE======= " + optionSelected)
            r_um.value = "";
            r_qty.value = "";
            r_price.value = "";
            r_wa_number.value = "";
            //msg_error.style.display = 'none';
            Refresh();
        };
    }
};

function GetData() {
    var txt_status_po_val_r = document.getElementById("txt_status_po_val").value;

    var txt_date_from_r = ConvertDate($('#txt_date_from').text());
    var txt_date_to_r = ConvertDate($('#txt_date_to').text());

    if (RegexDate(txt_date_from_r) == 1) {
        txt_date_from_r = "01-01-0001";
    }

    if (RegexDate(txt_date_to_r) == 1) {
        txt_date_to_r = "01-01-0001";
    }

    $.ajax({
        url: linkProc + '/PURCHASE_ORDER/ListPOByStatus/',
        cache: false,
        traditional: true,
        "_": $.now(),
        data: {
            txt_status_po_val: txt_status_po_val_r,
            txt_date_from: txt_date_from_r,
            txt_date_to: txt_date_to_r,
        },
        success: function (data) {
            Refresh();
        }
    });
}









