////========================================Start Common Script============================================
//var fullOrigin = window.location.href;
//var linkProc;
//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();
//};

var isValidItem;
var msgValidItem = "";



function Refresh() {
    //$.ajax({
    //    cache: false,
    //    url: fullOrigin + '/List',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    window.location.reload(true);
}
//========================================End Common Script================================================



//========================================Start Setup FST Script===========================================
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                if (IsOnlyNumberInput() == 0) {
                    if (IsValidChecked() == 0) {
                        var id = $.xResponse(linkProc + '/FORM_SUB_TYPE/IsInActive/', { value: $("#txt_form_sub_type_name").text() });
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
    }
}

function CheckData(action) {
    txt_form_sub_type_name = $("#txt_form_sub_type_name").text();
    txt_id = 0;

    var txt_popup_account;
    $("#cb_popup_account").is(":checked") ? txt_popup_account = 1 : txt_popup_account = 0;

    txt_budget_code = $("#txt_budget_code").text();
    txt_account_code_start = $("#txt_account_code_start").text();
    txt_account_code_end = $("#txt_account_code_end").text();

    if (txt_popup_account == 1 && (txt_budget_code == 0 || txt_budget_code == "" || txt_account_code_start == 0 || txt_account_code_start == "" || txt_account_code_end == 0 || txt_account_code_end == "")) {
        alert("budget code & account code start & account code end must be filled")
    } else {
        if (action == "edit") {
            txt_id = $("#txt_id").text();
        };
        $.ajax({
            url: linkProc + '/FORM_SUB_TYPE/CheckData',
            type: "Post",
            data: {
                id: txt_id,
                form_sub_type_name: txt_form_sub_type_name,
            },
            cache: false,
            traditional: true,
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                var field = document.getElementById('required_txt_form_sub_type_name');
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
}

function ActionCreate() {
    var f_sub_type_detail = new Array();
    f_sub_type_detail = GetDataTable();

    var msg_error = document.getElementById('msg_error');

    if (isValidItem) {
        txt_form_type_id = $("#txt_form_type_id").text();
        txt_form_sub_type_name = $("#txt_form_sub_type_name").text();
        txt_form_sub_type_description = $("#txt_form_sub_type_description").text();
        txt_sla = $("#txt_sla").text();

        var txt_popup_account;
        $("#cb_popup_account").is(":checked") ? txt_popup_account = 1 : txt_popup_account = 0;

        //if (txt_popup_account == 1) {
        //    txt_budget_code = $("#txt_budget_code").text();
        //    txt_account_code_start = $("#txt_account_code_start").text();
        //    txt_account_code_end = $("#txt_account_code_end").text();
        //} else {
        //    txt_budget_code = "";
        //    txt_account_code_start = "";
        //    txt_account_code_end = "";
        //}

        txt_budget_code = $("#txt_budget_code").text();
        txt_account_code_start = $("#txt_account_code_start").text();
        txt_account_code_end = $("#txt_account_code_end").text();

        var f_sub_type_bc = new Array();
        if (txt_popup_account == 1) {
            f_sub_type_bc = GetDataTableBc();
        }

        $.ajax({
            url: linkProc + '/FORM_SUB_TYPE/ActionCreate',
            type: 'Post',
            data: {
                form_type_id: txt_form_type_id,
                form_sub_type_name: txt_form_sub_type_name,
                form_sub_type_description: txt_form_sub_type_description,
                sla: txt_sla,
                fom_sub_type_detail: f_sub_type_detail,
                popup_account: txt_popup_account,
                budget_code: txt_budget_code,
                account_code_start: txt_account_code_start,
                account_code_end: txt_account_code_end,
                form_sub_type_bc: f_sub_type_bc,
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
    } else {
        msg_error.style.display = '';
        msg_error.innerHTML = "Information : " + "<br />" + msgValidItem;
    }

}

function ActionEdit() {
    txt_id = $("#txt_id").text();
    txt_form_type_id = $("#txt_form_type_id").text();
    txt_form_sub_type_name = $("#txt_form_sub_type_name").text();
    txt_form_sub_type_description = $("#txt_form_sub_type_description").text();
    txt_sla = $("#txt_sla").text();

    var txt_popup_account;
    $("#cb_popup_account").is(":checked") ? txt_popup_account = 1 : txt_popup_account = 0;

    txt_budget_code = $("#txt_budget_code").text();
    txt_account_code_start = $("#txt_account_code_start").text();
    txt_account_code_end = $("#txt_account_code_end").text();

    var f_sub_type_detail = new Array();
    f_sub_type_detail = GetDataTable();

    var f_sub_type_bc = new Array();
    if (txt_popup_account == 1) {
        f_sub_type_bc = GetDataTableBc();
    }

    var msg_error = document.getElementById('msg_error');

    if (isValidItem) {
        $.ajax({
            url: linkProc + '/FORM_SUB_TYPE/ActionEdit',
            type: 'Post',
            data: {
                id: txt_id,
                form_type_id: txt_form_type_id,
                form_sub_type_name: txt_form_sub_type_name,
                form_sub_type_description: txt_form_sub_type_description,
                sla: txt_sla,
                fom_sub_type_detail: f_sub_type_detail,
                popup_account: txt_popup_account,
                budget_code: txt_budget_code,
                account_code_start: txt_account_code_start,
                account_code_end: txt_account_code_end,
                form_sub_type_bc: f_sub_type_bc,
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
    } else {
        msg_error.style.display = '';
        msg_error.innerHTML = "Information : " + "<br />" + msgValidItem;
    }
}

function ActionDelete() {
    txt_id = $("#txt_id").text();
    $.ajax({
        url: linkProc + '/FORM_SUB_TYPE/ActionDelete',
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
}

function GetDataTable() {
    var oTable = document.getElementById('dataTable');
    var rowLength = oTable.rows.length;
    var temp = new Array();
    for (i = 0; i < rowLength; i++) {
        var oCells = oTable.rows.item(i).cells;
        var cellLength = oCells.length - 1;
        var columnNumber = i + 1; // + 1 for to get datatable number1

        var cellVal0 = "";
        var cellVal1 = "";
        for (var j = 0; j < cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;
            if (j == 0) {
                cellVal0 = value;
            }
            else {
                if (value == "") {
                    value = 0;
                }
                cellVal1 = value;
            }
        }

        if (cellVal0 != "0" && cellVal1 != 0) {
            temp.push(cellVal0 + "|" + cellVal1);
            isValidItem = true;
            msgValidItem = "";
        } else if ((cellVal0 == "" && cellVal1 != 0) || (cellVal0 != "0" && cellVal1 == 0) || (cellVal0 == "0" && cellVal1 != 0)) {
            isValidItem = false;
            msgValidItem = msgValidItem + "Item number " + columnNumber + " is not complete. Please fill RelDept. Name and Flow Number <br />";
            break;
        } else if (cellVal0 == "0" && cellVal1 == 0) {
            isValidItem = true;
            msgValidItem = "";
        }
    }

    //if (rowLength != temp.length) {
    //    temp = new Array();
    //}

    return temp;
}

function GetDataTableBc() {
    var oTable = document.getElementById('dataTableBc');
    var rowLength = oTable.rows.length;
    var tempBc = new Array();
    for (i = 0; i < rowLength; i++) {
        var oCells = oTable.rows.item(i).cells;
        var cellLength = oCells.length - 1;
        var columnNumber = i + 1; // + 1 for to get datatable number1

        var cellVal0 = "";
        var cellVal1 = "";
        var cellVal2 = "";
        for (var j = 0; j < cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;
            if (j == 0) {
                cellVal0 = value;
            }
            if (j == 1) {
                cellVal1 = value;
            }
            if (j == 2) {
                cellVal2 = value;
            }

        }

        if (cellVal0 != "0" && cellVal1 != "0" && cellVal2 != "0") {
            tempBc.push(cellVal0 + "|" + cellVal1 + "|" + cellVal2);
            isValidItem = true;
            msgValidItem = "";
        } else if (cellVal0 == "0" || cellVal1 == "0" || cellVal2 == "0") {
            isValidItem = false;
            msgValidItem = msgValidItem + "Item number " + columnNumber + " is not complete. Please fill Budget, start and end code <br />";
            break;
        }
    }


    return tempBc;
}

//create
$(function () {
    $('#dropdownList').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_form_type_id').text(optionSelected);
    });
})

$(function () {
    $('#dropdownList_bc').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_budget_code').text(optionSelected);
    });
})

$(function () {
    $('#dropdownList_acs').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_account_code_start').text(optionSelected);
    });
})

$(function () {
    $('#dropdownList_ace').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_account_code_end').text(optionSelected);
    });
})

function PopChecked() {
    var budget_area = document.getElementById('budget_area');
    if ($("#cb_popup_account").is(":checked")) {
        $("#budget_area").toggle(500);
    } else {
        budget_area.style.display = "none";
    }
}

//edit
$(function () {
    $('#TPROC_FORM_SUBTYPE_GR_FORM_TYPE_ID').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_form_type_id').text(optionSelected);
    });
});

$(function () {
    $('#TPROC_FORM_SUBTYPE_GR_BUDGET_CODE').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_budget_code').text(optionSelected);
    });
});

$(function () {
    $('#TPROC_FORM_SUBTYPE_GR_ACCOUNT_CODE_START').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_account_code_start').text(optionSelected);
    });
});

$(function () {
    $('#TPROC_FORM_SUBTYPE_GR_ACCOUNT_CODE_END').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_account_code_end').text(optionSelected);
    });
});

function ActionActivating(txt_id) {
    $.ajax({
        url: linkProc + '/FORM_SUB_TYPE/ActionActiviting',
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
//========================================End Setup FST Script=============================================




//========================================Start Request Script=============================================
function BtnActionRequestFST(action) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                if (IsValidTextbox() == 0) {
                    CheckDataRequestFST(action);
                }
            }
        }
    }
}

function CheckDataRequestFST(action) {
    var txt_id = $("#txt_id").text();
    if (action == "create") {
        txt_id = 0;
    }
    var urlLink = linkProc + '/REQUEST/CheckData';

    $.ajax({
        url: urlLink,
        type: "Get",
        data: {
            id: txt_id,
            rel_flag: $("#txt_form_sub_type_name").text(),
            control: 'FST',
            actions: $("#txt_action").text(),
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_form_sub_type_name');
            if (data == 1) {
                field.innerHTML = 'data already exist ';
                field.style.color = 'red';
            } else if (data == 2) {
                field.innerHTML = 'request already exist ';
                field.style.color = 'blue';
            } else if (data == 3) {
                field.innerHTML = 'data already exist but is not active. please call eproc staff to re-activate. +62 21 2555 7788';
                field.style.color = 'red';
            } else {
                field.innerHTML = '';
                ReadyToExec();

                if (IsValidApproval() == 0) {
                    if (action == "create") {
                        ActionRequestFSTCreate();
                    } else if (action == "edit") {
                        ActionRequestFSTEdit();
                    } else {
                        ActionRequestFSTDelete();
                    }
                }

            };
        }
    })

}

function ActionRequestFSTCreate() {
    var f_sub_type_detail = new Array();
    f_sub_type_detail = GetDataTable();

    var msg_error = document.getElementById('msg_error');
    var txt_desc = $("#txt_desc").val();
    txt_appr_nm = $("#txt_user_name").val();
    txt_appr_email = $("#txt_mail").val();

    if (isValidItem) {
        txt_form_type_id = $("#txt_form_type_id").text();
        txt_form_sub_type_name = $("#txt_form_sub_type_name").text();
        txt_form_sub_type_description = $("#txt_form_sub_type_description").text();
        txt_sla = $("#txt_sla").text();

        var txt_popup_account;
        $("#cb_popup_account").is(":checked") ? txt_popup_account = 1 : txt_popup_account = 0;

        //if (txt_popup_account == 1) {
        //    txt_budget_code = $("#txt_budget_code").text();
        //    txt_account_code_start = $("#txt_account_code_start").text();
        //    txt_account_code_end = $("#txt_account_code_end").text();
        //} else {
        //    txt_budget_code = "";
        //    txt_account_code_start = "";
        //    txt_account_code_end = "";
        //}

        txt_budget_code = $("#txt_budget_code").text();
        txt_account_code_start = $("#txt_account_code_start").text();
        txt_account_code_end = $("#txt_account_code_end").text();

        var f_sub_type_bc = new Array();
        if (txt_popup_account == 1) {
            f_sub_type_bc = GetDataTableBc();
        }

        $.ajax({
            url: linkProc + '/FORM_SUB_TYPE/ActionRequestFSTCreate',
            type: 'Post',
            data: {
                form_type_id: txt_form_type_id,
                form_sub_type_name: txt_form_sub_type_name,
                form_sub_type_description: txt_form_sub_type_description,
                sla: txt_sla,
                fom_sub_type_detail: f_sub_type_detail,
                popup_account: txt_popup_account,
                budget_code: txt_budget_code,
                account_code_start: txt_account_code_start,
                account_code_end: txt_account_code_end,
                desc: txt_desc,
                appr_nm: txt_appr_nm,
                appr_email: txt_appr_email,
                form_sub_type_bc: f_sub_type_bc,
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
                    alert('Request has been sent');
                    Refresh();
                },
        });
    } else {
        msg_error.style.display = '';
        msg_error.innerHTML = "Information : " + "<br />" + msgValidItem;
    }

}

function ActionRequestFSTComplete() {
    txt_id = $("#txt_id").text();
    //txt_form_type_id = $("#txt_form_type_id").text();
    //txt_form_sub_type_name = $("#txt_form_sub_type_name").text();
    //txt_form_sub_type_description = $("#txt_form_sub_type_description").text();
    //txt_sla = $("#txt_sla").text();

    //var txt_popup_account;
    //$("#cb_popup_account").is(":checked") ? txt_popup_account = 1 : txt_popup_account = 0;

    //txt_budget_code = $("#txt_budget_code").text();
    //txt_account_code_start = $("#txt_account_code_start").text();
    //txt_account_code_end = $("#txt_account_code_end").text();

    //var msg_error = document.getElementById('msg_error');
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    //if (isValidItem) {
    var rs = $.xResponse(linkProc + '/FORM_SUB_TYPE/ActionRequestFSTComplete',
     {
         id: txt_id,
         //form_type_id: txt_form_type_id,
         //form_sub_type_name: txt_form_sub_type_name,
         //form_sub_type_description: txt_form_sub_type_description,
         //sla: txt_sla,
         //fom_sub_type_detail: f_sub_type_detail,
         //popup_account: txt_popup_account,
         //budget_code: txt_budget_code,
         //account_code_start: txt_account_code_start,
         //account_code_end: txt_account_code_end,
         request_no: txt_request_no,
         actions: txt_actions,
     });

    if (rs == -1) {
        alert('Sent email failed');
    } else {
        alert('Request has been completed');
        Refresh();
        //document.getElementById('complete_request').style.visibility = 'hidden';
    }
    //} else {
    //    msg_error.style.display = '';
    //    msg_error.innerHTML = "Information : " + "<br />" + msgValidItem;
    //}
}

function ActionRequestFSTApprove() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    var rs = $.xResponse(linkProc + '/FORM_SUB_TYPE/ActionRequestFSTApprove',
    {
        id: txt_id,
        request_no: txt_request_no,
        actions: txt_actions,
    });

    if (rs == -1) {
        alert('Sent email failed');
    } else {
        alert('Request has been approved');
        Refresh();
    }
}

function ActionRequestFSTEdit() {
    txt_id = $("#txt_id").text();
    txt_form_type_id = $("#txt_form_type_id").text();
    txt_form_sub_type_name = $("#txt_form_sub_type_name").text();
    txt_form_sub_type_description = $("#txt_form_sub_type_description").text();
    txt_sla = $("#txt_sla").text();

    var txt_popup_account;
    $("#cb_popup_account").is(":checked") ? txt_popup_account = 1 : txt_popup_account = 0;

    txt_budget_code = $("#txt_budget_code").text();
    txt_account_code_start = $("#txt_account_code_start").text();
    txt_account_code_end = $("#txt_account_code_end").text();

    var f_sub_type_detail = new Array();
    f_sub_type_detail = GetDataTable();
    
    var f_sub_type_bc = new Array();
    if (txt_popup_account == 1) {
        f_sub_type_bc = GetDataTableBc();
    }

    var msg_error = document.getElementById('msg_error');

    txt_desc = $("#txt_desc").val();
    txt_appr_nm = $("#txt_user_name").val();
    txt_appr_email = $("#txt_mail").val();

    if (isValidItem) {
        $.ajax({
            url: linkProc + '/FORM_SUB_TYPE/ActionRequestFSTEdit',
            type: 'Post',
            data: {
                id: txt_id,
                form_type_id: txt_form_type_id,
                form_sub_type_name: txt_form_sub_type_name,
                form_sub_type_description: txt_form_sub_type_description,
                sla: txt_sla,
                fom_sub_type_detail: f_sub_type_detail,
                popup_account: txt_popup_account,
                budget_code: txt_budget_code,
                account_code_start: txt_account_code_start,
                account_code_end: txt_account_code_end,
                desc: txt_desc,
                appr_nm: txt_appr_nm,
                appr_email: txt_appr_email,
                form_sub_type_bc: f_sub_type_bc,
            },
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function () {
                    alert('Request has been sent');
                    Refresh();
                },
        });
    } else {
        msg_error.style.display = '';
        msg_error.innerHTML = "Information : " + "<br />" + msgValidItem;
    }
}

function ActionRequestFSTDelete() {
    txt_id = $("#txt_id").text();
    txt_desc = $("#txt_desc").val();
    txt_appr_nm = $("#txt_user_name").val();
    txt_appr_email = $("#txt_mail").val();

    $.ajax({
        url: linkProc + '/FORM_SUB_TYPE/ActionRequestFSTDelete',
        type: 'Post',
        data: {
            id: txt_id,
            desc: txt_desc,
            appr_nm: txt_appr_nm,
            appr_email: txt_appr_email,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                alert('Request has been sent');
                Refresh();
            },
    });
}

//========================================End Request Script===============================================




