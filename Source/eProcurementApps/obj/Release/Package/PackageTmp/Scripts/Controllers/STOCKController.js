function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                var id = $.xResponse(fullOrigin + '/IsInActive/', { value: $("#txt_item_code").text() });
                if (id > 0) {
                    if (confirm("Data already exist, but its inactive. Do you want to re-activate ?") == true) {
                        ActionActivating(id);
                    }
                } else {
                    CheckData(action);
                }
            }
        }
    }
}

function CheckData(action) {
    txt_item_code = $("#txt_item_code").text();
    txt_id = 0;
    if (action == "edit") {
        txt_id = $("#txt_id").text();
    };
    $.ajax({
        url: fullOrigin + '/CheckData',
        type: "Post",
        data: {
            id: txt_id,
            item_code: txt_item_code,
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_item_code');
            if (data == 1) {
                field.innerHTML = 'data already exist';
                field.style.color = 'red';
            } else {
                field.innerHTML = '';
                ReadyToExec();
                if (action == "create") {
                    ActionCreate();
                }
                else {
                    ActionEdit();
                }
            };
        }
    })
}

function ActionCreate() {
    txt_item_code = $("#txt_item_code").text();
    txt_item_description = $("#txt_item_description").text();
    txt_lookup_code = $("#txt_lookup_code").text();
    txt_unit_of_stock = $("#txt_unit_of_stock").text();
    txt_latest_cost = $("#txt_latest_cost").text();
    txt_average_cost = $("#txt_average_cost").text();
    txt_purchase_account = $("#txt_purchase_account").text();
    txt_stock_expenses = $("#txt_stock_expenses").text();
    txt_good_type_id = $("#txt_good_type_id").text(); 
    txt_supplier_id = $("#txt_supplier_id").text() == "" ? 0 : $("#txt_supplier_id").text();
    txt_qty = $("#txt_qty").text() == "" ? 0 : $("#txt_qty").text();
    txt_form_type_id = $("#txt_form_type_id").text();
    txt_qty_min = $("#txt_qty_min").text() == "" ? 0 : $("#txt_qty_min").text();

    $.ajax({
        url: fullOrigin + '/ActionCreate',
        type: 'Post',
        data: {
            item_code: txt_item_code,
            item_description: txt_item_description,
            lookup_code: txt_lookup_code,
            unit_of_stock: txt_unit_of_stock,
            latest_cost: txt_latest_cost,
            average_cost: txt_average_cost,
            purchase_account: txt_purchase_account,
            stock_expenses: txt_stock_expenses,
            good_type_id: txt_good_type_id,
            supplier_id: txt_supplier_id,
            qty: txt_qty,
            form_type_id: txt_form_type_id,
            qty_min: txt_qty_min,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                $(".dialogForm").dialog("close");
                alert('Data has been created');
                Refresh();
            },
    });
}

function ActionEdit() {
    txt_id = $("#txt_id").text();
    txt_item_code = $("#txt_item_code").text();
    txt_item_description = $("#txt_item_description").text();
    txt_lookup_code = $("#txt_lookup_code").text();
    txt_unit_of_stock = $("#txt_unit_of_stock").text();
    txt_latest_cost = $("#txt_latest_cost").text();
    txt_average_cost = $("#txt_average_cost").text();
    txt_purchase_account = $("#txt_purchase_account").text();
    txt_stock_expenses = $("#txt_stock_expenses").text();
    txt_good_type_id = $("#txt_good_type_id").text();
    txt_supplier_id = $("#txt_supplier_id").text();
    txt_qty = $("#txt_qty").text() == "" ? 0 : $("#txt_qty").text();
    txt_form_type_id = $("#txt_form_type_id").text();
    txt_qty_min = $("#txt_qty_min").text() == "" ? 0 : $("#txt_qty_min").text();

    $.ajax({
        url: fullOrigin + '/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            item_code: txt_item_code,
            item_description: txt_item_description,
            lookup_code: txt_lookup_code,
            unit_of_stock: txt_unit_of_stock,
            latest_cost: txt_latest_cost,
            average_cost: txt_average_cost,
            purchase_account: txt_purchase_account,
            stock_expenses: txt_stock_expenses,
            good_type_id: txt_good_type_id,
            supplier_id: txt_supplier_id,
            qty: txt_qty,
            form_type_id: txt_form_type_id,
            qty_min: txt_qty_min,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                $(".dialogForm").dialog("close");
                alert('Data has been Edited');
                Refresh();
            },
    });
};


function ActionDelete() {
    txt_id = $("#txt_id").text();
    $.ajax({
        url: fullOrigin + '/ActionDelete',
        type: 'Post',
        data: {
            id: txt_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                $(".dialogForm").dialog("close");
                alert('Data has been Deleted');
                Refresh();
            },
    });
};

function Refresh() {
    $.ajax({
        cache: false,
        url: fullOrigin + '/List',
        success: function (data) {
            $("#renderBody").html(data);
        }
    });
}


//$.extend({
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
//});

function ActionActivating(txt_id) {
    $.ajax({
        url: fullOrigin + '/ActionActiviting',
        type: 'Post',
        data: {
            id: txt_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                $(".dialogForm").dialog("close");
                alert('Data has been re-activated');
                Refresh();
            },
    });
};

function SearchData() {
    if ($("#txt_dt_from").text().trim() == "") {
        alert("Please fill date from ");
        return false
    };
    if ($("#txt_dt_to").text().trim() == "") {
        alert("Please fill date to ");
        return false
    };

    txt_dt_from = $("#txt_dt_from").text().trim();
    txt_dt_to = $("#txt_dt_to").text().trim();
    $.ajax({
        url: linkProc + '/STOCK/ActionReportMonthly',
        type: 'Post',
        data: {
            _PeriodeFrom: txt_dt_from,
            _PeriodeTo: txt_dt_to,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function (result) {
                $("#renderBody").html(result);
                $("#loadingRole").toggle();
                /*var Table = document.getElementById("myTable");

                var strInnerHTML = "";
                var parsed = JSON.parse(result); 
                $("#table_ap").empty();
                for (var x in parsed) {
                    strInnerHTML = strInnerHTML + "<tr>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["ITEM"] + "</td>"; 
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["UNIT OF MEASURE"] + "</td>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["CURRENT STOCK"] + "</td>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["STOCK IN"] + "</td>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["STOCK OUT"] + "</td>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["STOCK LAST"] + "</td>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["REF NO"] + "</td>";
                    strInnerHTML = strInnerHTML + "<td>" + parsed[x]["REF DATE"] + "</td>";
                    strInnerHTML = strInnerHTML + "</tr>";
                }
                if (strInnerHTML != "") {
                    $("#myTable").find('tbody').append(strInnerHTML);
                } */
            },
    });
}