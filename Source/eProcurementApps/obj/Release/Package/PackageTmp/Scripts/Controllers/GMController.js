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
    } else if (action == "approve") {
        ActionApprove();
        return false;
    } else if (action == "submit") {
        ActionSubmit();
        return false;
    } else if (action == "Rejected") {
        ActionRejected();
        return false;
    }
}

function ActionCreate() {
    if ($("#txt_gmnum").text().trim() == "" || $("#txt_gmnum").text().trim() == "Empty") {
        alert("Please fill gm number ");
        return false
    } else if ($("#txt_ponum").text().trim() == "") {
        alert("Please choose po number ");
        return false
    };
    txt_gmnum = $("#txt_gmnum").text().trim();
    PO_TYPE = $("#PO_TYPE").text().trim();
    txt_poid = $("#txt_poid").text().trim();
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var i = 0;
    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            i = i + 1;
            var rowName = header[(i % 7)];
            if ((i % 7) == 0) {
                row = {};
            }
            if ((i % 7) == 5) {
                rows.push(row);
            }
            if ((i % 7) != 6) {
                row[rowName] = $(this).find("input").val();
            }

        });
        return rows;
    }).get();
    
    if (i == 0) {
        alert("PO Number " + $("#txt_ponum").text().trim() + " on going to complete. Please approve good match with that's po number ");
        return false
    }
    $.ajax({
        url: linkProc + '/GM/ActionCreate',
        type: 'Post',
        data: {
            _GM_NUMBER: txt_gmnum,
            _PO_TYPE: PO_TYPE,
            _PO_ID: txt_poid,
            _JSONDetailDataTable: JSON.stringify(tableObj)
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
    if ($("#txt_gmnum").text().trim() == "" || $("#txt_gmnum").text().trim() == "Empty") {
        alert("Please fill gm number ");
        return false
    } else if ($("#txt_ponum").text().trim() == "") {
        alert("Please choose po number ");
        return false
    };

    ID = $("#GM_Id").text().trim();
    txt_gmnum = $("#txt_gmnum").text().trim();
    txt_poid = $("#txt_poid").text().trim();
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            var rowName = header[(i % 7)];
            if ((i % 7) == 0) {
                row = {};
            }
            if ((i % 7) == 5) {
                rows.push(row);
            }
            if ((i % 7) != 6) {
                row[rowName] = $(this).find("input").val();
            }
            
        });
        return rows;
    }).get();

    $.ajax({
        url: linkProc + '/GM/ActionEdit',
        type: 'Post',
        data: {
            _ID: ID,
            _GM_NUMBER: txt_gmnum,
            _PO_ID: txt_poid,
            _JSONDetailDataTable: JSON.stringify(tableObj)
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
                if (result == "Data has been edited") {
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

function ActionRejected() {
    if ($("#txt_reason_reject").text().trim() == "" || $("#txt_reason_reject").text().trim() == "Empty") {
        alert("Please fill reason of reject ");
        return false
    }
    txt_id = $("#GM_Id").text().trim();
    txt_reason_reject = $("#txt_reason_reject").text().trim();
    $.ajax({
        url: linkProc + '/GM/ActionRejected',
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

function ActionApprove() {
    txt_id = $("#GM_Id").text().trim();
    $.ajax({
        url: linkProc + '/GM/ActionApprove',
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

function ActionSubmit() {
    if ($("#txt_gmnum").text().trim() == "" || $("#txt_gmnum").text().trim() == "Empty") {
        alert("Please fill gm number ");
        return false
    } else if ($("#txt_ponum").text().trim() == "") {
        alert("Please choose po number ");
        return false
    };

    ID = $("#GM_Id").text().trim();
    txt_gmnum = $("#txt_gmnum").text().trim();
    txt_poid = $("#txt_poid").text().trim();
    PO_TYPE = $("#PO_TYPE").text().trim();
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            var rowName = header[(i % 7)];
            if ((i % 7) == 0) {
                row = {};
            }
            if ((i % 7) == 5) {
                rows.push(row);
            }
            if ((i % 7) != 6) {
                row[rowName] = $(this).find("input").val();
            }

        });
        return rows;
    }).get();

    $.ajax({
        url: linkProc + '/GM/ActionSubmit',
        type: 'Post',
        data: {
            _ID: ID,
            _GM_NUMBER: txt_gmnum,
            _PO_TYPE: PO_TYPE,
            _PO_ID: txt_poid,
            _JSONDetailDataTable: JSON.stringify(tableObj)
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

function Refresh() {
    ID = $("#GM_Id").text().trim();
    FLAGFORM = $("#FLAGFORM").text().trim();
    if (FLAGFORM == "1") {
        if (ID == "") {
            this.document.location.href = linkProc + '/GM/FormInput';
        } else {
            this.document.location.href = linkProc + '/GM';
        }
    } else {
        this.document.location.href = linkProc + '/DASHBOARD/IndexJobLists?_FlagInbox=' + FLAGFORM;
    }
}

function RefreshPage() {
    window.location.reload(true);
}

$(function () {
    $('#dropdownList_status_gm').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_status_gm_val').val(optionSelected);
    });
});

function add_filter() {
    $("#add_filter").toggle(500);
}

function GetData() {
    var txt_status_gm_val_r = document.getElementById("txt_status_gm_val").value;

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
        url: linkProc + '/GM/ListGmByStatus/',
        cache: false,
        traditional: true,
        "_": $.now(),
        data: {
            txt_status_gm_val: txt_status_gm_val_r,
            txt_date_from: txt_date_from_r,
            txt_date_to: txt_date_to_r,
        },
        success: function (data) {
            RefreshPage();
        }
    });
}

$(function () {
    $('#txt_ponum').on('DOMSubtreeModified', function () {
        GM_Id = $("#GM_Id").text().trim();
        txt_poid = $("#txt_poid").text().trim();
        $.ajax({
            url: linkProc + '/GM/GetDetail',
            type: 'Post',
            data: {
                GM_ID: GM_Id,
                PO_ID: txt_poid
            },
            cache: false,
            traditional: true,
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function (JSONDataTableDetail) {
                    if (JSONDataTableDetail == "[]" && $("#txt_ponum").text().trim() != "") {
                        alert("PO Number " + $("#txt_ponum").text().trim() + " on going to complete. Please approve good match with po number " + $("#txt_ponum").text().trim());
                        document.getElementById("txt_poid").innerHTML = "";
                        document.getElementById("txt_ponum").innerHTML = "";
                        document.getElementById("PO_TYPE").innerHTML = "";
                        document.getElementById("txt_suppliername").innerHTML = "";
                        document.getElementById("txt_phone_supplier").innerHTML = "";
                        document.getElementById("txt_address_supplier").innerHTML = "";
                        document.getElementById("txt_contact_person_supplier").innerHTML = "";
                        document.getElementById("txt_fax_supplier").innerHTML = "";
                        document.getElementById("txt_deliveryname").innerHTML = "";
                        document.getElementById("txt_phone_delivery").innerHTML = "";
                        document.getElementById("txt_address_delivery").innerHTML = "";
                        document.getElementById("txt_fax_delivery").innerHTML = "";
                        return false;
                    }
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
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "hidden" + "" + " id=" + "" + i + "_" + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=5 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["ITEM_ID"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=40 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["ITEM_DESCRIPTION"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=40 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["UNITMEASUREMENT"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=20 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["OUTSTANDING"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "freeText" + "" + " id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=20 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " value='" + parsed[x]["QUANTITY"] + "' onkeyup='CalcItem(this)' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td class=" + "" + "hidden" + "" + " id=" + "" + i + "_" + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<input size=20 type=" + "" + "text" + "" + " id=" + "" + i + a + "" + " readonly=" + "" + "readonly" + "" + " value='" + parsed[x]["PRICE"] + "' />";
                        strInnerHTML = strInnerHTML + "</td>";

                        a = a + 1;
                        strInnerHTML = strInnerHTML + "<td id=" + "" + i + a + "" + ">";
                        strInnerHTML = strInnerHTML + "<a class=" + "red" + " href=" + "#" + " onclick=" + "DeleteRow(this)" + " data-toggle=" + "modal" + " title=" + "Delete Row" + ">";
                        strInnerHTML = strInnerHTML + "<i class='ace-icon fa fa-trash-o bigger-130'></i>";
                        strInnerHTML = strInnerHTML + "</a>";
                        strInnerHTML = strInnerHTML + "</td>";

                        strInnerHTML = strInnerHTML + "</tr>";
                    }
                    if (strInnerHTML != "") {
                        $("#myTable").find('tbody').append(strInnerHTML);
                    }
                },
        });
    })
});

function CalcItem(row) {
    iRowItem = GetRowIndex(row) ;
    var qty = row.value;
    var outstanding;

    //outstanding = document.getElementById(iRowItem + "4");
    outstanding = row.parentNode.parentNode.childNodes['3'].firstChild.value;

    if (parseFloat(qty) > parseFloat(outstanding)) {
        row.value = 0;
        qty = row.value;
        alert("Please input same or less than oustanding");

        return false;
    }
}