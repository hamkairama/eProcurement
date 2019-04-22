function InsRowApprovalWa() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0];
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('select')[0];
    inp3.id += len;
    var inp4 = new_row.cells[4].getElementsByTagName('input')[0];
    inp4.id += len;
    inp4.value = '';
    //inp2.setAttribute('style', 'visibility:visible')
    var inp5 = new_row.cells[5].getElementsByTagName('button')[0];
    inp5.id += len;
    inp5.value = '';
    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function InsRowApprovalRelDept() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0];
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('select')[0];
    inp3.id += len;
    inp3.value = '';
    var inp4 = new_row.cells[4].getElementsByTagName('button')[0];
    inp4.id += len;
    //inp2.setAttribute('style', 'visibility:visible')
    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function InsRowRelDept() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('select')[0];
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0];
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('button')[0];
    inp2.id += len;
    //inp2.setAttribute('style', 'visibility:visible')
    x.appendChild(new_row);

    RefreshGetRowIndex();
}


function InsRowFstBudgetAdditional() {
    var x = document.getElementById('dataTableBc');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('select')[0];
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('select')[0];
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('select')[0];
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('button')[0];
    inp3.id += len;
    //inp2.setAttribute('style', 'visibility:visible')
    x.appendChild(new_row);

    RefreshGetRowIndex();
}


//=========================================================Start Public=====================================================
var iRow;

function DeleteRow(row) {
    var x = GetRowIndex(row);
    if (x > 1) {
        document.getElementById('dataTable').deleteRow(x - 1);
    }
    if (x == 1) {
        var y = document.getElementById('dataTable');

        var y = document.getElementById('dataTable');
        var rowy = y.rows[0];

        for (var i = 0; i < rowy.cells.length; i++) {
            var type = rowy.cells[i].getElementsByTagName('input')[0];
            if (type != undefined && type.nodeName == "INPUT") {
                type.value = "";
            }
        }
    }
}

function DeleteRowFstBudget(row) {
    var x = GetRowIndex(row);
    if (x > 1) {
        document.getElementById('dataTableBc').deleteRow(x - 1);
    }
    if (x == 1) {
        var y = document.getElementById('dataTableBc');

        var y = document.getElementById('dataTableBc');
        var rowy = y.rows[0];

        for (var i = 0; i < rowy.cells.length; i++) {
            var type = rowy.cells[i].getElementsByTagName('input')[0];
            if (type != undefined && type.nodeName == "INPUT") {
                type.value = "";
            }
        }
    }
}

$(document).ready(function () {
    RefreshGetRowIndex();
})

function RefreshGetRowIndex() {
    $("table tbody tr a").on('click', function (e) {
        iRow = $(this).closest('td').parent()[0].sectionRowIndex;
    });
}

function hasil() {
    var x = eval($("#satu").val());
    var y = eval($("#dua").val());

    var w = document.getElementById("hasil")
    w.innerHTML = x + y;
    w.innerText = x + y;
    w.value = x + y;
}
//=========================================================End Public=====================================================



//=========================================================Start Purchasing Request=====================================================
function DeleteRowPRItem(row) {
    var x = GetRowIndex(row);
    if (x > 1) {
        document.getElementById('dataTable').deleteRow(x - 1);
    }
    if (x == 1) {
        var y = document.getElementById('dataTable');

        var y = document.getElementById('dataTable');
        var rowy = y.rows[0];

        for (var i = 0; i < rowy.cells.length; i++) {
            var type = rowy.cells[i].getElementsByTagName('input')[0];
            if (type != undefined && type.nodeName == "INPUT") {
                type.value = "";
            }
        }
    }

    var subtot = CalcAll();
    $('#txt_sub_total_price').text(Comma(subtot.toString()));
}

function InsRowItem() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //ITEM_NAME
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];//SPECIFICATION
    inp2.id += len;
    inp2.value = '';

    var inp3 = new_row.cells[3].getElementsByTagName('select')[0];//CURRENCY
    inp3.id += len;
    //var optionSelected = $(inp3).find('option:selected').attr('value');
    //inp3.value = '';
    var inp4 = new_row.cells[4].getElementsByTagName('input')[0];//QTY
    inp4.id += len;
    inp4.value = '';
    //inp4.readOnly = true;
    var inp5 = new_row.cells[5].getElementsByTagName('input')[0];//USER_MEASUREMENT
    inp5.id += len;
    inp5.value = '';

    var inp6 = new_row.cells[6].getElementsByTagName('select')[0];//wa 
    inp6.id += len;
    inp6.value = '';
    var inp7 = new_row.cells[7].getElementsByTagName('input')[0];//WA_ORIN
    inp7.id += len;
    inp7.value = '';

    var inp8 = new_row.cells[8].getElementsByTagName('input')[0];//REVISED_QTY
    inp8.id += len;
    inp8.value = '';
    var inp9 = new_row.cells[9].getElementsByTagName('input')[0];//PRICE
    inp9.id += len;
    inp9.value = '';
    var inp10 = new_row.cells[10].getElementsByTagName('input')[0];//TOTAL_PRICE
    inp10.id += len;
    inp10.value = '';
    var inp11 = new_row.cells[11].getElementsByTagName('input')[0];//REMARK
    inp11.id += len;
    inp11.value = '';

    var inp12 = new_row.cells[12].getElementsByTagName('input')[0];//PO_NAME_SUPPNAME
    inp12.id += len;
    inp12.value = '';
    var inp13 = new_row.cells[13].getElementsByTagName('input')[0];//WA_ID
    inp13.id += len;
    inp13.value = '';

    var inp14 = new_row.cells[14].getElementsByTagName('input')[0];//item_cd
    inp14.id += len;
    inp14.value = '';

    var inp15 = new_row.cells[15].getElementsByTagName('input')[0];//convertion
    inp15.id += len;
    inp15.value = '1';

    var inp16 = new_row.cells[16].getElementsByTagName('input')[0];//item_id
    inp16.id += len;
    inp16.value = '';

    var inp17 = new_row.cells[17].getElementsByTagName('input')[0];//qty_storage
    inp17.id += len;
    inp17.value = '';

    var inp18 = new_row.cells[18].getElementsByTagName('input')[0];//qty_storage
    inp18.id += len;
    inp18.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function CreateNewRowItem() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    //new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];
    inp0.id;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //ITEM_NAME
    inp1.id;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];//SPECIFICATION
    inp2.id;
    inp2.value = '';


    //var inp3 = new_row.cells[3].getElementsByTagName('select')[0];//CURRENCY
    //inp3.id;
    //inp3.value = '';
    var inp4 = new_row.cells[4].getElementsByTagName('input')[0];//QTY
    inp4.id;
    inp4.value = '';
    //inp4.readOnly = true;
    var inp5 = new_row.cells[5].getElementsByTagName('input')[0];//USER_MEASUREMENT
    inp5.id;
    inp5.value = '';


    var inp6 = new_row.cells[6].getElementsByTagName('select')[0];//wa 
    inp6.id;
    inp6.value = '';
    var inp7 = new_row.cells[7].getElementsByTagName('input')[0];//WA_ORIN
    inp7.id;
    inp7.value = '';


    var inp8 = new_row.cells[8].getElementsByTagName('input')[0];//REVISED_QTY
    inp8.id;
    inp8.value = '';
    var inp9 = new_row.cells[9].getElementsByTagName('input')[0];//PRICE
    inp9.id;
    inp9.value = '';
    var inp10 = new_row.cells[10].getElementsByTagName('input')[0];//TOTAL_PRICE
    inp10.id;
    inp10.value = '';
    var inp11 = new_row.cells[11].getElementsByTagName('input')[0];//REMARK
    inp11.id;
    inp11.value = '';

    var inp12 = new_row.cells[12].getElementsByTagName('input')[0];//PO_NAME_SUPPNAME
    inp12.id;
    inp12.value = '';
    var inp13 = new_row.cells[13].getElementsByTagName('input')[0];//WA_ID
    inp13.id;
    inp13.value = '';

    var inp14 = new_row.cells[14].getElementsByTagName('input')[0];//ITEM_CODE
    inp14.id;
    inp14.value = '';

    var inp15 = new_row.cells[15].getElementsByTagName('input')[0];//CONVERTION
    inp15.id;
    inp15.value = '1';

    var inp16 = new_row.cells[16].getElementsByTagName('input')[0];//item_id
    inp16.id;
    inp16.value = '';

    var inp17 = new_row.cells[17].getElementsByTagName('input')[0];//qty_storage
    inp17.id;
    inp17.value = '';

    var inp18 = new_row.cells[18].getElementsByTagName('input')[0];//qty_storage
    inp18.id;
    inp18.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function RefreshTable(tableName) {
    CreateNewRowItem();
    var x = document.getElementById(tableName);
    var length = x.rows.length;
    var i = 0;
    while (i < length - 1) {
        document.getElementById(tableName).deleteRow(0);
        i++;
    }
}
//=========================================================End Purchasing Request=====================================================



//=========================================================Start Purchase Order=====================================================
function DeleteRowPODetail(row) {
    var x = GetRowIndex(row);
    var y = document.getElementById('dataTablePODetail');
    var len = y.rows.length;
    if (x = 1 && len == 1) {
        InsRowItemPODetail();
        document.getElementById('dataTablePODetail').deleteRow(x - 1);
    } else {
        document.getElementById('dataTablePODetail').deleteRow(x - 1);
    }
    var subtot = CalTotalPricePO();
    $('#txt_sub_total_price_po').text(Comma(subtot.toString()));
}

function InsRowItemPO() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];//ITEM
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //U/M
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];//QUANTITY
    inp2.id += len;
    inp2.value = '';

    var inp3 = new_row.cells[3].getElementsByTagName('input')[0];//PRICE
    inp3.id += len;
    inp3.value = '';
    var inp4 = new_row.cells[4].getElementsByTagName('input')[0];//TOTAL
    inp4.id += len;
    inp4.value = '';
    var inp5 = new_row.cells[5].getElementsByTagName('input')[0];//TEMP
    inp5.id += len;
    inp5.value = '';
    var inp6 = new_row.cells[6].getElementsByTagName('input')[0];//ITEM_ID
    inp6.id += len;
    inp6.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function CreateNewRowItemPO() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    //new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];//ITEM
    inp0.id;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //U/M
    inp1.id;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];//QUANTITY
    inp2.id;
    inp2.value = '';

    var inp3 = new_row.cells[3].getElementsByTagName('input')[0];//PRICE
    inp3.id;
    inp3.value = '';
    var inp4 = new_row.cells[4].getElementsByTagName('input')[0];//TOTAL
    inp4.id;
    inp4.value = '';
    var inp5 = new_row.cells[5].getElementsByTagName('input')[0];//TEMP
    inp5.id;
    inp5.value = '';
    var inp6 = new_row.cells[6].getElementsByTagName('input')[0];//ITEM_ID
    inp6.id;
    inp6.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function InsRowItemPODetail() {
    var x = document.getElementById('dataTablePODetail');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    //<<zoer20170925
    var selected = [];
    for (var i = 0; i < len ; i++) {
        var select = x.rows[i].cells[0].getElementsByTagName('select')[0];
        selected[i] = select.options[select.selectedIndex].index;
    }
    //>>zoer20170925

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('select')[0];//PRNumber
    inp0.id += len;
    inp0.value = '';

    //<<zoer20170925
    for (var j = 0; j < selected.length ; j++) {
        inp0.remove(selected[j]);
    }
    //>>zoer20170925

    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //U/M
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0];//QUANTITY
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('input')[0];//PRICE
    inp3.id += len;
    inp3.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function RefreshTablePO(tableName) {
    CreateNewRowItemPO();
    var x = document.getElementById(tableName);
    var length = x.rows.length;
    var i = 0;
    while (i < length - 1) {
        document.getElementById(tableName).deleteRow(0);
        i++;
    }
}
//=========================================================End Purchase Order=====================================================



//=========================================================Start Price Comparison=====================================================

function deleteColumn(tblId) {
    var tbl = document.getElementById(tblId);

    var allRows = tbl.rows;
    for (var i = 0; i < allRows.length; i++) {
        if (allRows[i].cells.length > 8) {
            allRows[i].deleteCell(-1);
        }
    }

    var tblFootObj = tbl.tFoot.rows;
    for (var i = 0; i < tblFootObj.length; i++) {
        if (tblFootObj[i].cells.length > 2) {
            tblFootObj[i].deleteCell(-1);
        }
    }
}

function addColumn(tblId) {
    var tbl = document.getElementById(tblId);
    
    var tblHeadObj = tbl.tHead;   
    var tr = tblHeadObj.getElementsByTagName("tr");
    for (var h = 0; h < tblHeadObj.rows.length; h++) {
        var new_col = tblHeadObj.rows[h].cells[7].cloneNode(true);
        var input = new_col;
        input.type = 'select';
        input.children.txt_cb_supplier_id.textContent = ""; 
        input.children.txt_ck_supplier.checked = false;
        tr[h].appendChild(input);
    }

    var tblBodyObj = tbl.tBodies[0];
    var tr_body = tblBodyObj.getElementsByTagName("tr");
    for (var j = 0; j < tblBodyObj.rows.length; j++) {
        var new_col_body = tblBodyObj.rows[j].cells[7].cloneNode(true);
        var input_body = new_col_body;
        input_body.type = 'table';
        input_body.children[0].cells[0].children.txt_each_price_.value = "";
        input_body.children[0].cells[1].children.txt_total_price_.value = ""
        tr_body[j].appendChild(input_body);
        //tblBodyObj.rows[j].insertCell(-1);
    }

    
    //var tblBodyObj = tbl.tBodies[0];
    //for (var i = 0; i < tblBodyObj.rows.length; i++) {
    //    var newCellBody = tblBodyObj.rows[i].insertCell(-1);

    //    newCellBody.innerHTML =
    //        " <table> \
    //                                <tr>\
    //                                    <td><input size=6 type='text' id='txt_each_price_' onkeyup='CalcTotalPC(this)' style='text-align:right' placeholder='each price' /></td>\
    //                                    <td><input size=10 type='text' id='txt_total_price_' style='text-align:right' placeholder='total price' readonly='readonly' /> </td>\
    //                                </tr>\
    //                            </table>  \
    //        "
    //}

    var tblFootObj = tbl.tFoot;
    for (var i = 0; i < tblFootObj.rows.length; i++) {
        if (i == 0) {
            var newCellFoot = tblFootObj.rows[i].insertCell(-1);
            newCellFoot.innerHTML =
                " <table> \
                                     <tr>\
                                        <td>Sub Total</td>\
                                        <td><input size=10 type='text' id='txt_sub_total_' style='text-align:right' placeholder='sub total' readonly='readonly' /> </td>\
                                    </tr>\
                                    <tr>\
                                        <td><input size=6 type='text' id='txt_disc_per_' style='text-align:right' placeholder='discount %' onkeyup='CalcDisc(this)'/></td>\
                                        <td><input size=10 type='text' id='txt_disc_rp_' style='text-align:right' placeholder='discount Rp' readonly='readonly' /> </td>\
                                    </tr>\
                                    <tr>\
                                        <td><input size=6 type='text' id='txt_vat_per_' style='text-align:right' placeholder='vat %' onkeyup='CalcVat(this)'/></td>\
                                        <td><input size=10 type='text' id='txt_vat_rp_' style='text-align:right' placeholder='vat Rp' readonly='readonly' /> </td>\
                                    </tr>\
                                    <tr>\
                                        <td><input size=6 type='text' id='txt_pph_per_' style='text-align:right' placeholder='pph %' onkeyup='CalcPph(this)' /></td>\
                                        <td><input size=10 type='text' id='txt_pph_rp_' style='text-align:right' placeholder='pph Rp' readonly='readonly' /> </td>\
                                    </tr>\
                                    <tr>\
                                        <td><b>Grand Total</b></td>\
                                        <td><input size=10 type='text' id='txt_grand_total_' style='text-align:right' placeholder='grand total' readonly='readonly' /> </td>\
                                    </tr>\
                                </table>  \
            "
        } else if (i == 1) {
            var newCellFoot = tblFootObj.rows[i].insertCell(-1);
            newCellFoot.innerHTML =
            "<textarea id='txt_desc' name='textarea' style='width:200px;height:80px;'></textarea>  \
            "
        }
        
    }
}

function DeleteRowAcknowledgeUser(row) {
    var x = GetRowIndex(row);
    var y = document.getElementById('dataTableAcknowledgeUser');
    var len = y.rows.length;
    if (x = 1 && len == 1) {
        var y = document.getElementById('dataTableAcknowledgeUser');

        var y = document.getElementById('dataTableAcknowledgeUser');
        var rowy = y.rows[0];

        for (var i = 0; i < rowy.cells.length; i++) {
            var type = rowy.cells[i].getElementsByTagName('input')[0];
            if (type != undefined && type.nodeName == "INPUT") {
                type.value = "";
            }
        }
    } else {
        document.getElementById('dataTableAcknowledgeUser').deleteRow(x - 1);
    }
}

function InsRowAcknowledgeUser() {
    var x = document.getElementById('dataTableAcknowledgeUser');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];//Wa_id
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //wa_numb
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('select')[0]; //wa_dropdown
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('input')[0]; //Approval
    inp3.id += len;
    inp3.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function DeleteRowChooseOneApproval(row) {
    var x = GetRowIndex(row);
    var y = document.getElementById('dataTableChooseOneApproval');
    var len = y.rows.length;
    if (x = 1 && len == 1) {
        var y = document.getElementById('dataTableChooseOneApproval');

        var y = document.getElementById('dataTableChooseOneApproval');
        var rowy = y.rows[0];

        for (var i = 0; i < rowy.cells.length; i++) {
            var type = rowy.cells[i].getElementsByTagName('input')[0];
            if (type != undefined && type.nodeName == "INPUT") {
                type.value = "";
            }
        }
    } else {
        document.getElementById('dataTableChooseOneApproval').deleteRow(x - 1);
    }
}

function InsRowChooseOneApproval() {
    var x = document.getElementById('dataTableChooseOneApproval');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];//Wa_id
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0]; //wa_numb
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('select')[0]; //wa_dropdown
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('input')[0]; //Approval
    inp3.id += len;
    inp3.value = '';

    x.appendChild(new_row);

    RefreshGetRowIndex();
}

function InsRowItemPC() {
    var x = document.getElementById('dataTable');
    var new_row = x.rows[0].cloneNode(true);
    var len = x.rows.length;

    new_row.id = len + 1;
    var inp0 = new_row.cells[0].getElementsByTagName('input')[0];//item_id numpang on button delete row
    inp0.id += len;
    inp0.value = '';
    var inp1 = new_row.cells[1].getElementsByTagName('input')[0];//ITEM
    inp1.id += len;
    inp1.value = '';
    var inp2 = new_row.cells[2].getElementsByTagName('input')[0]; //U/M
    inp2.id += len;
    inp2.value = '';
    var inp3 = new_row.cells[3].getElementsByTagName('input')[0];//QUANTITY
    inp3.id += len;
    inp3.value = '';

    var inp4 = new_row.cells[4].getElementsByTagName('input')[0];//PRICE
    inp4.id += len;
    inp4.value = '';
    var inp5 = new_row.cells[5].getElementsByTagName('input')[0];//TOTAL
    inp5.id += len;
    inp5.value = '';
    var inp6 = new_row.cells[6].getElementsByTagName('input')[0];//TEMP
    inp6.id += len;
    inp6.value = '';

    var inp7 = new_row.cells[7].getElementsByTagName('input')[0];//EACH PRICE FROM SUPPLIER
    inp7.id += len;
    inp7.value = '';

    var inp_each_price = new_row.cells['7'].firstElementChild.cells['0'].firstElementChild
    //inp_each_price.id += len;
    inp_each_price.value = '';
    var inp_total_price = new_row.cells['7'].firstElementChild.cells['1'].firstElementChild
    //inp_total_price.id += len;
    inp_total_price.value = '';




    //var new_rowx = x.rows[0];
    if (x.rows[0].cells.length > 8) {
        var start = 8;
        var parent = [];
        var child1 = []
        var child2 = []
        for (var i = 0; i < (x.rows[0].cells.length - 8) ; i++) {

            parent[i] = new_row.cells[start].getElementsByTagName('input')[0];//EACH PRICE FROM SUPPLIER
            parent[i].id += len;
            parent[i].value = '';

            child1[i] = new_row.cells[start].firstElementChild.cells['0'].firstElementChild
            //inp_each_price.id += len;
            child1[i].value = '';

            child2[i] = new_row.cells[start].firstElementChild.cells['1'].firstElementChild
            //inp_total_price.id += len;
            child2[i].value = '';

            start++;

        }

    }

    x.appendChild(new_row);
    RefreshGetRowIndex();

    //hide butn add column on price compare    
    $("#btn_add_col").hide();
}

function DeleteRowPC(row) {
    var x = GetRowIndex(row);
    if (x > 1) {
        document.getElementById('dataTable').deleteRow(x - 1);
    }

    //show butn add column on price compare
    if (x == 2) {
        $("#btn_add_col").show();
    }
}

//=========================================================End Price Comparison=====================================================