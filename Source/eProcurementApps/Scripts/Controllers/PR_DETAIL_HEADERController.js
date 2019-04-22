//========================================Start Common Script=============================================
var iRowItem;

function Refresh() {
    window.location.reload(true);
}

function GetDataTable() {
    var temp = new Array();
    $('#table_input tbody tr  input:checkbox').each(function () {
        if (this.checked) {
            temp.push(this.id);
        }
    });

    return temp;
}
//========================================End Common Script==================================================

//========================================Start item =============================================
function ActionApproveItem() {
    var txt_item_id = $("#txt_item_id").text();
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionItemApprove/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            id: txt_item_id,
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to approve");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                //msg_item.style.display = '';
                //msg_item.textContent = "Item has been approved";
                Refresh();
            },
    });
}

function ActionReviewItem() {
    var txt_item_id = $("#txt_item_id").text();
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionItemReview/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            id: txt_item_id,
            pr_header_id: txt_pr_header_id, 
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to review");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                //msg_item.style.display = '';
                //msg_item.textContent = "Item has been reviewed";
                Refresh();
            },
    });
}

function ActionApproveReviewItemSelected() {
    var item_detail = new Array();
    item_detail = GetDataTable();
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    if (item_detail.length > 0) {
        $.ajax({
            url: linkProc + '/PURCHASING_REQUEST/ActionApproveReviewItemSelected/',
            type: 'Post',
            cache: false,
            traditional: true,
            data: {
                ids: item_detail,
                pr_header_id: txt_pr_header_id,
            },
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            error: function (response) {
                alert("Data failed to update");
            },
            success:
                function () {
                    $(".dialogForm").dialog("close");
                    Refresh();
                },
        });
    } else {
        alert("Please select data that want to approve or review")
    }
}

function ActionRejectItem(status_appr_wa) {
    var reason = $("#txt_reason_reject_1").val();
    if (reason == "                    " || reason == "") {
        $("#input_reason_1").toggle(500);
        $("#txt_reason_reject_1").val("").focus();
    } else {
        SaveRejectItem(reason, status_appr_wa);
    }
}

function SaveRejectItem(txt_reason, txt_status_appr_wa) {
    var txt_item_id = $("#txt_item_id").text();
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionItemReject/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            id: txt_item_id,
            reason: txt_reason,
            status_appr_wa: txt_status_appr_wa,
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to reject");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
            },
    });
}

function ActionRejectItemSelected() {
    var reason = $("#txt_reason_reject").val();
    if (reason == "                            " || reason == "") {
        $("#input_reason_selected").toggle(500);
        $("#txt_reason_reject").val("").focus();
    } else {
        SaveRejectItemSelected(reason);
    }
}

function SaveRejectItemSelected(txt_reason) {
    var item_detail = new Array();
    item_detail = GetDataTable();
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    if (item_detail.length > 0) {
        $.ajax({
            url: linkProc + '/PURCHASING_REQUEST/ActionItemRejectSelected/',
            type: 'Post',
            cache: false,
            traditional: true,
            data: {
                ids: item_detail,
                reject_reason: txt_reason,
                pr_header_id: txt_pr_header_id,
            },
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            error: function (response) {
                alert("Failed to reject");
            },
            success:
                function () {
                    $(".dialogForm").dialog("close");
                    Refresh();
                },
        });
    } else {
        alert("Please select data that want to rejected")
    }
}

function ActionRejectByEproc() {
    var reason = $("#txt_reason_reject_by_eproc").val();
    if (reason == "                            " || reason == "") {
        $("#input_reason_by_eproc").toggle(500);
        $("#txt_reason_reject_by_eproc").val("").focus();
    } else {
        SaveRejectByEproc(reason);
    }
}

function SaveRejectByEproc(txt_reason) {
    var txt_pr_header_id = $("#txt_pr_header_id").text();
   
    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionRejectPRByEproc/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            reject_reason: txt_reason,
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to reject");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                alert("PR has been rejected by eproc admin")
                Refresh();
            },
    });
}

//========================================end item=============================================


//========================================Start rd =============================================
function ActionReviewRD() {
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionRDReview/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to review");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "RD has been reviewed";
            },
    });
}

function ActionApproveRD() {
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionRDApprove/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to approve");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "RD has been approved";
            },
    });
}

function ActionRejectRD(status_appr_rd) {
    var reason = $("#txt_reason_reject_pr").val();
    if (reason == "") {
        $("#input_reason_pr").toggle(500);
        $("#txt_reason_reject_pr").val("").focus();
    } else {
        SaveRejectRD(reason, status_appr_rd);
    }
}

function SaveRejectRD(txt_reason, txt_status_appr_rd) {
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionRDReject/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            reason: txt_reason,
            status_appr_rd: txt_status_appr_rd,
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to reject");
        },
        success:
            function () {
                $("#input_reason_pr").toggle(500);
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "PR has been reject";
            },
    });
}
//========================================end rd =============================================



function PushEmail(form) {
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    var urlMethod;
    if (form == "WA") {
        urlMethod = '/PURCHASING_REQUEST/SentPushEmailByWaItem/'

    } else {
        urlMethod = '/PURCHASING_REQUEST/SentPushEmailByRD/'
    }
    var txt_item_id = $("#txt_item_id").text();
    $.ajax({
        url: linkProc + urlMethod,
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            id: txt_item_id,
            pr_header_id: txt_pr_header_id,
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
                $(".dialogForm").dialog("close");
                msg_item.style.display = '';
                msg_item.textContent = "Email has been pushed";
                //Refresh();
            },
    });
}

function ActionHandlePR() {
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionHandlePR/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to handle");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "PR has been handled";
            },
    });
}

function ActionReadyToCreatePO() {
    var txt_pr_header_id = $("#txt_pr_header_id").text();
    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionReadyToCreatePO/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to handle");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "PR has been Create PO";
            },
    });
}

function ActionReadyToComplete() {
    var txt_pr_header_id = $("#txt_pr_header_id").text();

    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionReadyToComplete/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            pr_header_id: txt_pr_header_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to handle");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "PR has been Create PO";
            },
    });
}

function ActionReadyToSignOff() {
    $.ajax({
        url: linkProc + '/PURCHASING_REQUEST/ActionReadyToSignOff/',
        type: 'Post',
        cache: false,
        traditional: true,
        data: {},
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        error: function (response) {
            alert("Failed to handle");
        },
        success:
            function () {
                $(".dialogForm").dialog("close");
                Refresh();
                msg_item.style.display = '';
                msg_item.textContent = "PR has been Create PO";
            },
    });
}

function ActionSaveRevise() {
    txt_pr_header_id = $("#txt_pr_header_id").text();
    sub_total_price = $("#txt_sub_total_price").text();
    txt_sub_total_price = sub_total_price.replace(/,/g, ''); //delete comma to passing integer

    var msg_error = document.getElementById('msg_error');
    var item_detail = new Array();
    item_detail = GetDataTableRevise();

    var result = $.xResponse(linkProc + '/PURCHASING_REQUEST/ActionSaveRevise/', {
        pr_header_id: txt_pr_header_id,
        sub_total_price: txt_sub_total_price,
        litem_detail: item_detail,
    });

    Refresh();
}

function GetDataTableRevise() {
    var oTable = document.getElementById('dataTable');
    var rowLength = oTable.rows.length;
    var temp = new Array();
    for (i = 0; i < rowLength; i++) {
        var oCells = oTable.rows.item(i).cells;
        var cellLength = oCells.length - 1;
        var columnNumber = i + 1; // + 1 for to get datatable number1

        var cellRevise = "";//Revise
        var cellTotalPrice = "";//TOTAL_PRICE
        var cellRemark = "";//REMARK
        var cellItemId = "";//Item ID

        for (var j = 0; j <= cellLength; j++) {
            if (j == 7 || j == 9 || j == 10) {
                var value = oCells.item(j).firstElementChild.value;
                if (j == 7) {
                    //cellRevisedQty = value == '' ? '0' : value;
                    cellRevisedQty = value;
                }
                else if (j == 9) {
                    cellTotalPrice = value;
                }
                else if (j == 10) {
                    cellRemark = value;
                }
            }

            if (j == 15) {
                var value = oCells.item(j).firstElementChild.value;
                cellItemId = value;
            }
        }

        if (cellRevisedQty != "") {
            temp.push(cellRevisedQty.replace(/,/g, '') + "|" + cellTotalPrice.replace(/,/g, '') + "|" + cellRemark + "|" + cellItemId);
        }
    }
    return temp;
}

function CalcItem(row) {
    iRowItem = GetRowIndex(row);
    var reviseQty = row.value;
    var result, pricerr, priceTotal, qty;

    qty = document.getElementById('txt_qty_' + iRowItem);
    pricerr = document.getElementById("txt_price_" + iRowItem);
    priceTotal = document.getElementById('txt_total_price_' + iRowItem);
    
    if (IsReviseMoreThanQty(parseFloat(reviseQty), parseFloat(qty.value))) {
        row.value = "";
        CalcRevise(parseFloat(qty.value), parseFloat(pricerr.value.replace(/,/g, "")));
        alert("Revise quantity can't be more than quantity");        
    } else if (reviseQty == "") {
        row.value = "";
        CalcRevise(parseFloat(qty.value), parseFloat(pricerr.value.replace(/,/g, "")));
        //alert("Revise quantity can't be 0 or null");
    } else {
        CalcRevise(parseFloat(reviseQty), parseFloat(pricerr.value.replace(/,/g, "")));
    }
}

function CalcAll() {
    var x = document.getElementById('dataTable');
    var result = 0;
    for (var i = 0; i < x.rows.length; i++) {
        if (!(x.rows[i].cells[7].childNodes[0].readOnly)) {
            var r = x.rows[i].cells[9].childNodes[0].value.replace(/,/g, "");
            if (r > 0) {
                result = result + parseInt(r);
            }
        }        
    }
    return result;
}

function CalcRevise(reviseOrQty, pricerr) {
    var priceTotal = document.getElementById('txt_total_price_' + iRowItem);
    var r = reviseOrQty * pricerr;
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
}

function IsReviseMoreThanQty(reviseQty, qty) {
    if (reviseQty > qty) {
        return true
    } else {
        return false
    }
}

function ActionSaveEditPRRemarkOrSupplier() {
    txt_pr_header_id = $("#txt_pr_header_id").text();
    sub_total_price = $("#txt_sub_total_price").text();
    txt_sub_total_price = sub_total_price.replace(/,/g, ''); //delete comma to passing integer

    var msg_error = document.getElementById('msg_error');
    var item_detail = new Array();
    item_detail = GetDataTableEdit();

    var result = $.xResponse('../PURCHASING_REQUEST/ActionSaveEditPRRemarkOrSupplier/', {
        pr_header_id: txt_pr_header_id,
        litem_detail: item_detail,
    });

    Refresh();
}

function GetDataTableEdit() {
    var oTable = document.getElementById('dataTable');
    var rowLength = oTable.rows.length;
    var temp = new Array();
    for (i = 0; i < rowLength; i++) {
        var oCells = oTable.rows.item(i).cells;
        var cellLength = oCells.length - 1;
        var columnNumber = i + 1; // + 1 for to get datatable number1

        var cellRemark = "";//REMARK
        var cellPoSupplier = "";//suppliername
        var cellItemId = "";//Item ID

        for (var j = 0; j <= cellLength; j++) {
            if (j == 10 || j == 11 || j == 16) {
                var value = oCells.item(j).firstElementChild.value;
                if (j == 10) {
                    cellRemark = value;
                }
                else if (j == 11) {
                    cellPoSupplier = value;
                }
                else if (j == 16) {
                    cellItemId = value;
                }
            }
        }

        temp.push(cellRemark + "|" + cellPoSupplier + "|" + cellItemId);
    }
    return temp;
}

function getSupplier(row) {
    var idx = "";
    iRowItem = GetRowIndex(row);
    var r_wa;

    var url = '../SUPPLIER/PopSupplier/';
    var form = '.dialogForm';

    ModalPop(url, idx, form);
}

function txtSearchSsupplier(event) {
    if (event.keyCode == 13) {
        SearchSupplier();
    };
};

function SearchSupplier() {
    txt_supp_name = $("#txt_search_supplier").val();
    $.ajax({
        url: '../SUPPLIER/PopSearch',
        data: {
            supp_name: txt_supp_name,
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

function PassingDataSupplier(no) {
    var supplier_id = document.getElementById("txt_id_supp_" + no).innerText;
    var supplier_name = document.getElementById("txt_supp_name_" + no).innerText;

    var r_supplier_id, r_supplier_name;

    r_supplier_id = document.getElementById("txt_supplier_id_" + iRowItem);
    r_supplier_name = document.getElementById("txt_supplier_name_" + iRowItem);


    r_supplier_id.value = supplier_id;
    r_supplier_name.value = supplier_name;

    $(".dialogForm").dialog("close");
}

$(function () {
    $('#select_all').click(function (event) {
        if (this.checked) {
            $(':checkbox:enabled').each(function () {
                this.checked = true;
            });
        }
        else {
            $(':checkbox').each(function () {
                this.checked = false;
            });
        }
    });
})

