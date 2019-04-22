//var fullOrigin = window.location.href;
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                var id = $.xResponse(fullOrigin + '/IsInActive/', { value: $("#txt_supplier_name").text() });
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

function GetDataByRowStat() {
    var txt_row_stat;
    if ($("#rb_legal_list").is(":checked")) {
        txt_row_stat = 0
    } else if ($("#rb_ilegal_list").is(":checked")) {
        txt_row_stat = -3
    }

    $.ajax({
        url: linkProc + '/SUPPLIER/GetDataByRowStat',
        type: 'Post',
        data: {
            row_stat: txt_row_stat
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle();
            },
        success: function (data) {
            $("#renderBody").html(data);
            $("#loadingRole").toggle();
        },
    });
}

function CheckData(action) {
    txt_supplier_name = $("#txt_supplier_name").text();
    txt_id = 0;
    if (action == "edit") {
        txt_id = $("#txt_id").text();
    };
    $.ajax({
        url: fullOrigin + '/CheckData',
        type: "Post",
        data: {
            id: txt_id,
            supplier_name: txt_supplier_name,
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_supplier_name');
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
    txt_supplier_name = $("#txt_supplier_name").text();
    txt_supplier_alias_name = $("#txt_supplier_alias_name").text();
    txt_supplier_address = $("#txt_supplier_address").text();
    //txt_vendor_code = $("#txt_vendor_code").text();
    txt_bank_name = $("#txt_bank_name").text();
    txt_bank_branch = $("#txt_bank_branch").text();
    txt_bank_account_number = $("#txt_bank_account_number").text();
    txt_contact_person = $("#txt_contact_person").text();
    txt_email_address = $("#txt_email_address").text();
    txt_mobile_number = $("#txt_mobile_number").text();
    txt_office_number = $("#txt_office_number").text();
    txt_fax_number = $("#txt_fax_number").text();
    txt_tax_number = $("#txt_tax_number").text();
    txt_website = $("#txt_website").text();
    txt_npwp = $("#txt_npwp").text();
    txt_description = $("#txt_description").text();
    txt_b_unit_owner = $("#txt_b_unit_owner").text();
    txt_city = $("#txt_city").text();
    txt_core_business = $("#txt_core_business").text();
    txt_nama_barang = $("#txt_nama_barang").text();
    txt_efective_dt = $("#txt_efective_dt").text();
    txt_schedule_eval = $("#txt_schedule_eval").text();

    arry_doc_checking = getElementDoc('check-doc');
    arry_supp_qual = getElementQual('qual');
    arry_supp_eval = getElementEval('eval');

    var txt_legal;
    $("#rb_ilegal").is(":checked") ? txt_legal = -3 : txt_legal = 0;

    $.ajax({
        url: fullOrigin + '/ActionCreate',
        type: 'Post',
        data: {
            supplier_name: txt_supplier_name,
            supplier_alias_name: txt_supplier_alias_name,
            supplier_address: txt_supplier_address,
            //vendor_code: txt_vendor_code,
            bank_name: txt_bank_name,
            bank_branch: txt_bank_branch,
            bank_account_number: txt_bank_account_number,
            contact_person: txt_contact_person,
            email_address: txt_email_address,
            mobile_number: txt_mobile_number,
            office_number: txt_office_number,
            fax_number: txt_fax_number,
            tax_number: txt_tax_number,
            website: txt_website,
            npwp: txt_npwp,
            description: txt_description,
            b_unit_owner: txt_b_unit_owner,
            city: txt_city,
            core_business :txt_core_business,
            nama_barang :txt_nama_barang,
            effective_dt: txt_efective_dt,
            schedule_eval: txt_schedule_eval,
            doc_checking: arry_doc_checking,
            supp_qual: arry_supp_qual,
            supp_eval: arry_supp_eval,
            legalisation: txt_legal,
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
    txt_supplier_name = $("#txt_supplier_name").text();
    txt_supplier_alias_name = $("#txt_supplier_alias_name").text();
    txt_supplier_address = $("#txt_supplier_address").text();
    //txt_vendor_code = $("#txt_vendor_code").text();
    txt_bank_name = $("#txt_bank_name").text();
    txt_bank_branch = $("#txt_bank_branch").text();
    txt_bank_account_number = $("#txt_bank_account_number").text();
    txt_contact_person = $("#txt_contact_person").text();
    txt_email_address = $("#txt_email_address").text();
    txt_mobile_number = $("#txt_mobile_number").text();
    txt_office_number = $("#txt_office_number").text();
    txt_fax_number = $("#txt_fax_number").text();
    txt_tax_number = $("#txt_tax_number").text();
    txt_website = $("#txt_website").text();
    txt_npwp = $("#txt_npwp").text();
    txt_description = $("#txt_description").text();
    txt_b_unit_owner = $("#txt_b_unit_owner").text();
    txt_city = $("#txt_city").text();
    txt_core_business = $("#txt_core_business").text();
    txt_nama_barang = $("#txt_nama_barang").text();
    txt_efective_dt = $("#txt_efective_dt").text();
    txt_schedule_eval = $("#txt_schedule_eval").text();

    arry_doc_checking = getElementDoc('check-doc');
    arry_supp_qual = getElementQual('qual');
    arry_supp_eval = getElementEval('eval');

    var txt_legal;
    $("#rb_ilegal").is(":checked") ? txt_legal = -3 : txt_legal = 0;

    $.ajax({
        url: fullOrigin + '/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            supplier_name: txt_supplier_name,
            supplier_alias_name: txt_supplier_alias_name,
            supplier_address: txt_supplier_address,
            //vendor_code: txt_vendor_code,
            bank_name: txt_bank_name,
            bank_branch: txt_bank_branch,
            bank_account_number: txt_bank_account_number,
            contact_person: txt_contact_person,
            email_address: txt_email_address,
            mobile_number: txt_mobile_number,
            office_number: txt_office_number,
            fax_number: txt_fax_number,
            tax_number: txt_tax_number,
            website: txt_website,
            npwp: txt_npwp,
            description: txt_description,
            b_unit_owner: txt_b_unit_owner,
            city: txt_city,
            core_business: txt_core_business,
            nama_barang: txt_nama_barang,
            effective_dt: txt_efective_dt,
            schedule_eval: txt_schedule_eval,
            doc_checking: arry_doc_checking,
            supp_qual: arry_supp_qual,
            supp_eval: arry_supp_eval,
            legalisation: txt_legal,

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
}

function ActionDelete() {
    txt_id = $("#txt_id").text();
    var result = $.xResponse(fullOrigin + '/ActionDelete', { id: txt_id });
    if (result == "0") {
        alert("Supplier is being use by the another table");
    } else {
        $(".dialogForm").dialog("close");
        alert('Data has been Deleted');
        Refresh();
    }
}

function Refresh() {
    $.ajax({
        cache: false,
        url: fullOrigin + '/List',
        success: function (data) {
            $("#renderBody").html(data);
        }
    });
}

function Generate() {
    var input = [{ key: "key1", value: "value1" }, { key: "key2", value: "value2" }];
    var result = {};

    for (var i = 0; i < input.length; i++) {
        result[input[i].key] = input[i].value;
    }
}

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
}

function RefreshTotScoreQual() {
    //refresh totalscore
    var totScore = CalcScoreQual();
    $('#txt_qual_total_score').val(Comma(totScore.toString()));
}

function CalcScoreQual() {
    var x = document.getElementById('dataTableQual');
    var result = 0;
    for (var i = 0; i < x.rows.length; i++) {
        if (x.rows[i].cells.length == 5) {
            if (x.rows[i].cells[4].childNodes.length > 0) {
                var r = x.rows[i].cells[4].childNodes[0].value;
                if (r != undefined) {
                    result = parseInt(result) + parseInt(r);
                }
            }
        }
    }
    return result;
}

function RefreshTotScoreEval() {
    //refresh totalscore
    var totScore = CalcScoreEval();
    $('#txt_eval_total_score').val(Comma(totScore.toString()));
}

function CalcScoreEval() {
    var x = document.getElementById('dataTableEval');
    var result = 0;
    for (var i = 0; i < x.rows.length; i++) {
        if (x.rows[i].cells.length == 5) {
            if (x.rows[i].cells[4].childNodes.length > 0) {
                var r = x.rows[i].cells[4].childNodes[0].value;
                if (r != undefined) {
                    result = parseInt(result) + parseInt(r);
                }
            }
        }
    }
    return result;
}

function getElementQual(str) {
    var x = document.getElementsByClassName(str)
    var a = "";
    for (var i = 0; i < x.length; i++) {
        var r = x[i].value;

        a = a + r;
        a = a + "|";
    };

    var komentar = $("#komentar_" + str).text();    
    var dibuat = $("#txt_dibuat_oleh_" + str).text() == "Empty" ? "" : $("#txt_dibuat_oleh_" + str).text();
    var diperiksa = $("#txt_diperiksa_oleh_" + str).text() == "Empty" ? "" : $("#txt_diperiksa_oleh_" + str).text();
    var disetujui = $("#txt_disetujui_oleh_" + str).text() == "Empty" ? "" : $("#txt_disetujui_oleh_" + str).text();

    a = a + komentar + "|" + dibuat + "|" + diperiksa + "|" + disetujui;

    return a;
}

function getElementEval(str) {
    var x = document.getElementsByClassName(str)
    var a = "";
    for (var i = 0; i < x.length; i++) {
        var r = x[i].value;

        a = a + r
        a = a + "|"
    };

    var check;
    $("#rb_ya_" + str).is(":checked") ? check = "1" : check = "0";

    var komentar = $("#komentar_" + str).text();
    var disiapkan = $("#txt_disiapkan_oleh_" + str).text() == "Empty" ? "" : $("#txt_disiapkan_oleh_" + str).text();
    var disetujui = $("#txt_disetujui_oleh_" + str).text() == "Empty" ? "" : $("#txt_disetujui_oleh_" + str).text();


    a = a + check + "|" + komentar + "|" + disiapkan + "|" + disetujui;

    return a;
}

function getElementDoc(str) {
    var x = document.getElementsByClassName(str)
    var b = "";
    for (var i = 0; i < x.length; i++) {
        //var r = x[i].value;
        var check;
        $(x[i]).is(":checked") ? check = "1" : check = "0";
        
        b = b + check

        if (i < x.length - 1) {
            b = b + "|"
        }        
    };

    return b;
}

function GetSupplierQualEval() {
    var supp_name = $("#txt_supplier_name").text();
    var cp = $("#txt_contact_person").text();
    var core_business = $("#txt_core_business").text();
    var nama_barang = $("#txt_nama_barang").text();

    $('#qual_nama_supplier').text(supp_name);
    $('#eval_nama_supplier').text(supp_name);

    $('#qual_cp').text(cp);
    $('#eval_cp').text(cp);

    $('#qual_core_business').text(core_business);
    $('#eval_core_business').text(core_business);

    $('#qual_nama_barang').text(nama_barang);
    $('#eval_nama_barang').text(nama_barang);
}