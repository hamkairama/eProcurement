function BtnAction(action) {
    if (action == "create") {
        ActionCreate();
        return false;
    } else if (action == "edit") {
        ActionEdit();
        return false;
    } else if (action == "delete") {
        ActionDelete();
        return false;
    } else if (action == "submit") {
        ActionSubmit();
        return false;
    } else if (action == "Verified") {
        ActionVerified();
        return false;
    } else if (action == "approve") {
        ActionApprove();
        return false;
    } else if (action == "Rejected") {
        ActionRejected();
        return false;
    } else if (action == "ExportPdf") {
        ActionExportPdf();
        return false;
    } else if (action == "received") {
        ActionReceived();
        return false;
    } else if (action == "paid") {
        ActionPaid();
        return false;
    }
}

function ActionCreate() {
    if ($("#txt_crvnum").text().trim() == "" || $("#txt_crvnum").text().trim() == "Empty") {
        alert("Please fill crv number ");
        return false
    } else if ($("#txt_ponumber").text().trim() == "") {
        alert("Please choose po number ");
        return false
    };

    txt_crvnum = $("#txt_crvnum").text().trim();
    txt_poid = $("#txt_poid").text().trim();
    txt_gmnumber = $("#txt_gmnumber").text().trim();
    txt_supplierid = $("#txt_supplierid").text().trim();
    txt_reftaxno = $("#txt_reftaxno").text().trim();
    txt_paymentmethod = $("#txt_paymentmethod").text().trim();
    txt_bankname = $("#txt_bankname").text().trim();
    txt_bankbranch = $("#txt_bankbranch").text().trim();
    txt_bankaccountnumber = $("#txt_bankaccountnumber").text().trim();
    txt_kliringno = $("#txt_kliringno").text().trim();
    txt_sub_total = $("#txt_sub_total").text().trim();
    txt_discount = $("#txt_discount").text().trim();
    txt_vat = $("#txt_vat").text().trim();
    txt_wth_tax_pph = $("#txt_wth_tax_pph").text().trim();
    txt_grand_total = $("#txt_grand_total").text().trim();
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            var rowName = header[(i % 16)];
            if ((i % 16) == 0) {
                row = {};
            }
            if ((i % 16) == 15) {
                rows.push(row);
            }
            row[rowName] = $(this).find("input").val();
        });
        return rows;
    }).get();
    $.ajax({
        url: linkProc + '/CRV/ActionCreate',
        type: 'Post',
        data: {
            _CRV_NUMBER: txt_crvnum,
            _PO_ID: txt_poid,
            _GMNUMBER: txt_gmnumber,
            _BANK_ACCOUNT_NUMBER: txt_bankaccountnumber,
            _BANK_NAME: txt_bankname,
            _SUB_TOTAL: txt_sub_total,
            _DSCNT_AMT: txt_discount,
            _VAT: txt_vat,
            _WTH_TAX: txt_wth_tax_pph,
            _GRAND_TOTAL: txt_grand_total,
            _SUPPLIER_ID: txt_supplierid,
            _BANK_BRANCH: txt_bankbranch,
            _REFTAXNO: txt_reftaxno,
            _PAYMENTMETHOD: txt_paymentmethod,
            _KLIRINGNO: txt_kliringno,
            _JSONDetailDataTable: JSON.stringify(tableObj),
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
                if (result == "Data has been created") {
                    Refresh();
                } 
            },
    });
}

function ActionEdit() {
    if ($("#txt_crvnum").text().trim() == "" || $("#txt_crvnum").text().trim() == "Empty") {
        alert("Please fill crv number ");
        return false
    } else if ($("#txt_ponumber").text().trim() == "") {
        alert("Please choose po number ");
        return false
    };

    CRV_Id = $("#CRV_Id").text().trim();
    txt_crvnum = $("#txt_crvnum").text().trim();
    txt_poid = $("#txt_poid").text().trim();
    txt_supplierid = $("#txt_supplierid").text().trim();
    txt_reftaxno = $("#txt_reftaxno").text().trim();
    txt_paymentmethod = $("#txt_paymentmethod").text().trim();
    txt_bankname = $("#txt_bankname").text().trim();
    txt_bankbranch = $("#txt_bankbranch").text().trim();
    txt_bankaccountnumber = $("#txt_bankaccountnumber").text().trim();
    txt_kliringno = $("#txt_kliringno").text().trim();
    txt_sub_total = $("#txt_sub_total").text().trim();
    txt_discount = $("#txt_discount").text().trim();
    txt_vat = $("#txt_vat").text().trim();
    txt_wth_tax_pph = $("#txt_wth_tax_pph").text().trim();
    txt_grand_total = $("#txt_grand_total").text().trim();
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var tableObj = $('#table_ap').map(function () {
        var row = {};
        $(this).find('td').each(function (i) {
            var rowName = header[i];
            row[rowName] = $(this).find("input").val();
        });
        return row;
    }).get();
    $.ajax({
        url: linkProc + '/CRV/ActionEdit',
        type: 'Post',
        data: {
            _ID: CRV_Id,
            _CRV_NUMBER: txt_crvnum,
            _PO_ID: txt_poid,
            _BANK_ACCOUNT_NUMBER: txt_bankaccountnumber,
            _BANK_NAME: txt_bankname,
            _SUB_TOTAL: txt_sub_total,
            _DSCNT_AMT: txt_discount,
            _VAT: txt_vat,
            _WTH_TAX: txt_wth_tax_pph,
            _GRAND_TOTAL: txt_grand_total,
            _SUPPLIER_ID: txt_supplierid,
            _BANK_BRANCH: txt_bankbranch,
            _REFTAXNO: txt_reftaxno,
            _PAYMENTMETHOD: txt_paymentmethod,
            _KLIRINGNO: txt_kliringno,
            _JSONDetailDataTable: JSON.stringify(tableObj),
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
                if (result == "Data has been created") {
                    Refresh();
                }
            },
    });
}

function ActionDelete() {
    txt_id = $("#CRV_Id").text();
    $.ajax({
        url: linkProc + '/CRV/ActionDelete',
        type: 'Post',
        data: {
            id: txt_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function (result) {
                $(".dialogForm").dialog("close");
                alert(result);
                if (result == "Data has been deleted") {
                    Refresh();
                }
            },
    });
};

function ActionApprove() {
    txt_id = $("#CRV_Id").text().trim(); 
    $.ajax({
        url: linkProc + '/CRV/ActionApprove',
        type: 'Post',
        data: {
            _ID: txt_id, 
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
                if (result == "Data has been approved") {
                    Refresh();
                }
            },
    });
};

function ActionExportPdf() {
    txt_id = $("#CRV_Id").text().trim();
    $.ajax({
        url: linkProc + '/CRV/ActionExportPdf',
        type: 'Post',
        data: {
            _ID: txt_id,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function (result) {
                
            },
    });
};

function ActionVerified() {
    txt_id = $("#CRV_Id").text().trim();
    $.ajax({
        url: linkProc + '/CRV/ActionVerified',
        type: 'Post',
        data: {
            _ID: txt_id,
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
                if (result == "Data has been verified") {
                    Refresh();
                }
            },
    });
};

function ActionRejected() {
    if ($("#txt_reason_reject").text().trim() == "" || $("#txt_reason_reject").text().trim() == "Empty") {
        alert("Please fill reason of reject ");
        return false
    }
    txt_id = $("#CRV_Id").text().trim();
    txt_reason_reject = $("#txt_reason_reject").text().trim();
    $.ajax({
        url: linkProc + '/CRV/ActionRejected',
        type: 'Post',
        data: {
            _ID: txt_id,
            _RejectedNote: txt_reason_reject,
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
                if (result == "Data has been rejected") {
                    Refresh();
                }
            },
    });
};

function ActionSubmit() {
    if ($("#txt_crvnum").text().trim() == "" || $("#txt_crvnum").text().trim() == "Empty") {
        alert("Please fill crv number ");
        return false
    } else if ($("#txt_ponumber").text().trim() == "") {
        alert("Please choose po number ");
        return false
    };

    CRV_Id = $("#CRV_Id").text().trim();
    txt_crvnum = $("#txt_crvnum").text().trim();
    txt_poid = $("#txt_poid").text().trim();
    txt_gmnumber = $("#txt_gmnumber").text().trim();
    txt_supplierid = $("#txt_supplierid").text().trim();
    txt_reftaxno = $("#txt_reftaxno").text().trim();
    txt_paymentmethod = $("#txt_paymentmethod").text().trim();
    txt_bankname = $("#txt_bankname").text().trim();
    txt_bankbranch = $("#txt_bankbranch").text().trim();
    txt_bankaccountnumber = $("#txt_bankaccountnumber").text().trim();
    txt_kliringno = $("#txt_kliringno").text().trim();
    txt_sub_total = $("#txt_sub_total").text().trim();
    txt_discount = $("#txt_discount").text().trim();
    txt_vat = $("#txt_vat").text().trim();
    txt_wth_tax_pph = $("#txt_wth_tax_pph").text().trim();
    txt_grand_total = $("#txt_grand_total").text().trim();
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            var rowName = header[(i % 16)];
            if ((i % 16) == 0) {
                row = {};
            }
            if ((i % 16) == 15) {
                rows.push(row);
            }
            row[rowName] = $(this).find("input").val();
        });
        return rows;
    }).get();
    
    $.ajax({
        url: linkProc + '/CRV/ActionSubmit',
        type: 'Post',
        data: {
            _ID: CRV_Id,
            _CRV_NUMBER: txt_crvnum,
            _PO_ID: txt_poid,
            _GMNUMBER: txt_gmnumber,
            _BANK_ACCOUNT_NUMBER: txt_bankaccountnumber,
            _BANK_NAME: txt_bankname,
            _SUB_TOTAL: txt_sub_total,
            _DSCNT_AMT: txt_discount,
            _VAT: txt_vat,
            _WTH_TAX: txt_wth_tax_pph,
            _GRAND_TOTAL: txt_grand_total,
            _SUPPLIER_ID: txt_supplierid,
            _BANK_BRANCH: txt_bankbranch,
            _REFTAXNO: txt_reftaxno,
            _PAYMENTMETHOD: txt_paymentmethod,
            _KLIRINGNO: txt_kliringno,
            _JSONDetailDataTable: JSON.stringify(tableObj),
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
                if (result == "Data has been submited") {
                    Refresh();
                }
            },
    });
    
}

function ActionReceived() {
    txt_id = $("#CRV_Id").text().trim(); 
    $.ajax({
        url: linkProc + '/CRV/ActionReceived',
        type: 'Post',
        data: {
            _ID: txt_id 
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
                if (result == "Data has been received") {
                    Refresh();
                }
            },
    });
};

function ActionPaid() {
    txt_id = $("#CRV_Id").text().trim();
    FileDoc = $("#File_Doc").text().trim();
    FileName = document.getElementById("txt_attach_doc").value;
    $.ajax({
        url: linkProc + '/CRV/ActionPaid',
        type: 'Post',
        data: {
            _ID: txt_id,
            _FileName: FileName,
            _FileDoc: FileDoc,
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
                if (result == "Data has been paid") {
                    Refresh();
                }
            },
    });
};

function Refresh() {
    ID = $("#CRV_Id").text().trim();
    FLAGFORM = $("#FLAGFORM").text().trim();
    if (FLAGFORM == "1") {
        if (ID == "") {
            this.document.location.href = linkProc + '/CRV/FormInput';
        } else {
            this.document.location.href = linkProc + '/CRV';
        }
    } else {
        this.document.location.href = linkProc + '/DASHBOARD/IndexJobLists?_FlagInbox=' + FLAGFORM;
    }
}

function RefreshPage() {
    window.location.reload(true);
}

$(function () {
    $('#dropdownList_status_crv').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_status_crv_val').val(optionSelected);
    });
});

function add_filter() {
    $("#add_filter").toggle(500);
}

function GetData() {
    var txt_status_crv_val_r = document.getElementById("txt_status_crv_val").value;

    var txt_date_from_r = $("#txt_date_from").text().trim();
    var txt_date_to_r = $("#txt_date_to").text().trim();

    //var txt_date_from_r = ConvertDate($('#txt_date_from').text());
    //var txt_date_to_r = ConvertDate($('#txt_date_to').text());

    if (RegexDate(txt_date_from_r) == 1) {
        txt_date_from_r = "01-01-0001";
    }

    if (RegexDate(txt_date_to_r) == 1) {
        txt_date_to_r = "01-01-0001";
    }

    $.ajax({
        url: linkProc + '/CRV/ListCrvByStatus/',
        cache: false,
        traditional: true,
        "_": $.now(),
        data: {
            txt_status_crv_val: txt_status_crv_val_r,
            txt_date_from: txt_date_from_r,
            txt_date_to: txt_date_to_r,
        },
        success: function (data) {
            RefreshPage();
        }
    });
}

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}


$(function () {
    $('#txt_suppliername').on('DOMSubtreeModified', function () {
        _SupplierID = $("#txt_supplierid").text().trim();
        _CRV_ID = $("#CRV_ID").text().trim();
        $.ajax({
            url: linkProc + '/CRV/GetPO',
            type: 'Post',
            data: {
                SupplierID: _SupplierID,
                CRV_ID: _CRV_ID,
            },
            cache: false,
            traditional: true,
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function (JSONPOTableData) {
                    document.getElementById("txt_poid").innerHTML = "";
                    document.getElementById("txt_ponumber").innerHTML = "";
                    document.getElementById("posearch").onclick = function () {
                        ModalSearch('/Search/Index', JSONPOTableData, 'Search Good Match', 'txt_poid:Y|txt_gmnumber:N|txt_ponumber:N', '.dialogForm');
                    };
                },
        });
    })
});

$(function () {
    $('#txt_gmnumber').on('DOMSubtreeModified', function () {
        txt_poid = $("#txt_poid").text().trim();
        txt_gmnumber = $("#txt_gmnumber").text().trim();
        $.ajax({
            url: linkProc + '/CRV/GetDetail',
            type: 'Post',
            data: {
                REF_ID: txt_poid,
                REF_NUMBER: txt_gmnumber
            },
            cache: false,
            traditional: true,
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function (JSONDataTableDetail) {
                    var Table = document.getElementById("myTable");

                    var strInnerHTML = "";
                    var parsed = JSON.parse(JSONDataTableDetail);
                    var i = 0;
                    var a = 0;
                    $("#table_ap").empty();
                    for (var x in parsed) {
                        i = i + 1;
                        strInnerHTML = strInnerHTML + "<tr id=" + "" + i + "" + ">";
                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + "_" + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=2 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["NO"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "hidden" + "" + " id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["ITEM_ID"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "hidden" + "" + " id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["UNITMEASUREMENT"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "" + "" + " id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["QUANTITY"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "" + "" + " class=" + "" + "" + "" + " id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["PRICE"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=20 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["DESCRIPTION"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=3 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["CURR"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["ACCOUNT_DEBIT"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=10 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + formatNumber(parsed[x]["OTH_AMOUNT"]) + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=10 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + formatNumber(parsed[x]["RUPIAH_AMOUNT"]) + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + parsed[x]["FUND_T1"] + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + parsed[x]["LOB1_T2"] + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + parsed[x]["PLAN_T3"] + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + parsed[x]["WA_T4"] + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + parsed[x]["LOB2_T5"] + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "hidden" + "" + " id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value=" + "" + parsed[x]["CURRENCY"] + "" + " />";
                        strInnerHTML = strInnerHTML + "</td>";

                        document.getElementById("txt_sub_total").innerHTML = formatNumber(parsed[x]["SUB_TOTAL"]);
                        document.getElementById("txt_discount").innerHTML = formatNumber(parsed[x]["DSCNT_AMT"]);
                        document.getElementById("txt_vat").innerHTML = formatNumber(parsed[x]["VAT"]);
                        document.getElementById("txt_wth_tax_pph").innerHTML = formatNumber(parsed[x]["WTH_TAX"]);
                        document.getElementById("txt_grand_total").innerHTML = formatNumber(parsed[x]["GRAND_TOTAL"]);
                    }
                    if (strInnerHTML != "") {
                        $("#myTable").find('tbody').append(strInnerHTML);
                    }
                },
        });

    })
});

function encode(input) {
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var output = "";
    var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
    var i = 0;

    while (i < input.length) {
        chr1 = input[i++];
        chr2 = i < input.length ? input[i++] : Number.NaN; // Not sure if the index 
        chr3 = i < input.length ? input[i++] : Number.NaN; // checks are needed here

        enc1 = chr1 >> 2;
        enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
        enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
        enc4 = chr3 & 63;

        if (isNaN(chr2)) {
            enc3 = enc4 = 64;
        } else if (isNaN(chr3)) {
            enc4 = 64;
        }
        output += keyStr.charAt(enc1) + keyStr.charAt(enc2) +
                  keyStr.charAt(enc3) + keyStr.charAt(enc4);
    }
    return output;
}

function GetFile() {
    $('#postedFile').trigger('click');
    var FileName = document.getElementById("postedFile").value;
    FileName = FileName.split(/[\\ ]+/).pop();
    document.getElementById("txt_attach_doc").setAttribute("value", FileName);
    var fileList = document.getElementById("postedFile").files;
    var file = fileList[0];
    var fileReader = new FileReader();
    if (fileReader && fileList && fileList.length) {
        fileReader.readAsArrayBuffer(fileList[0]);
        fileReader.onload = function () {
            var arrayBuffer = fileReader.result;
            var bytes = new Uint8Array(arrayBuffer);
            document.getElementById("File_Doc").innerHTML = encode(bytes)
        };
    }
}