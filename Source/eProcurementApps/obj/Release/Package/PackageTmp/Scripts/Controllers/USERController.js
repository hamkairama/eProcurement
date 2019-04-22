//========================================Start Common Script=============================================
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
//========================================End Common Script==================================================



//========================================Start Setup User Script===========================================
function BtnAction(action) {
    //testing
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        //cek select
        if (IsValidSelect() == 0) {
            if (IsValidDate() == 0) {
                if (IsValidLength() == 0) {
                    var id = $.xResponse(linkProc + '/USER/IsInActive/', { value: $("#txt_user_id2").text() });
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

function CheckData(action) {
    var txt_user_id2 = $("#txt_user_id2").text();
    var txt_id = 0;
    //var urlLink = '../USER/CheckData';
    var urlLink = linkProc + '/REQUEST/CheckData';

    if (action == "create") {
        $.ajax({
            url: urlLink,
            type: "Get",
            data: {
                id: txt_id,
                user_id: txt_user_id2,
            },
            cache: false,
            traditional: true,
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                var field = document.getElementById('required_txt_user_id2');
                if (data == 1) {
                    field.innerHTML = 'data already exist';
                    field.style.color = 'red';
                } else {
                    field.innerHTML = '';
                    ReadyToExec();
                    ActionCreate();
                };
            }
        })
    } else {
        ActionEdit();
    }
}

function ActionCreate() {
    var txt_user_id2 = $("#txt_user_id2").text();
    var txt_user_name = $("#txt_user_name").text();
    var txt_mail = $("#txt_mail").text();
    var txt_password = $("#txt_password").text();

    var txt_is_super_admin;
    $("#cb_is_super_admin").is(":checked") ? txt_is_super_admin = 1 : txt_is_super_admin = 0;

    var txt_is_eproc_admin;
    $("#cb_is_eproc_admin").is(":checked") ? txt_is_eproc_admin = 1 : txt_is_eproc_admin = 0;

    var txt_comp_cd;
    $("#rb_mami").is(":checked") ? txt_comp_cd = 1 : txt_comp_cd = 0;

    var txt_role_id = $("#txt_role_id").text();

    var wa_detail = new Array();
    wa_detail = GetDataWA();

    var txt_division_id = $("#txt_division_id").text();

    //GenerateImage();

    $.ajax({
        url: linkProc + '/USER/ActionCreate',
        type: 'Post',
        data: {
            user_id: txt_user_id2,
            user_name: txt_user_name,
            user_mail: txt_mail,
            is_super_admin: txt_is_super_admin,
            is_eproc_admin: txt_is_eproc_admin,
            role_id: txt_role_id,
            comp_cd: txt_comp_cd,
            lwa_number: wa_detail,
            division_id: txt_division_id,
            password: txt_password,
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                alert('Data has been created');
                Refresh();
            },
    });
}

function ActionEdit() {
    txt_id = $("#txt_id").text();
    var txt_user_id2 = $("#txt_user_id2").text();
    var txt_user_name = $("#txt_user_name").text();
    var txt_mail = $("#txt_mail").text();

    var txt_is_super_admin;
    $("#cb_is_super_admin").is(":checked") ? txt_is_super_admin = 1 : txt_is_super_admin = 0;

    var txt_is_eproc_admin;
    $("#cb_is_eproc_admin").is(":checked") ? txt_is_eproc_admin = 1 : txt_is_eproc_admin = 0;

    var txt_comp_cd;
    $("#rb_mami").is(":checked") ? txt_comp_cd = 1 : txt_comp_cd = 0;

    var txt_role_id = $("#txt_role_id").text();

    var wa_detail = new Array();
    wa_detail = GetDataWA();

    var txt_division_id = $("#txt_division_id").text();

    //GenerateImage();

    $.ajax({
        url: linkProc + '/USER/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            user_id: txt_user_id2,
            user_name: txt_user_name,
            user_mail: txt_mail,
            is_super_admin: txt_is_super_admin,
            is_eproc_admin: txt_is_eproc_admin,
            role_id: txt_role_id,
            comp_cd: txt_comp_cd,
            lwa_number: wa_detail,
            division_id: txt_division_id,
        },
        cache: false,
        traditional: true,

        error: function (response) {
            alert(response.responseText);
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                alert('Data has been edited');
                Refresh();
            },
    });
}

function ActionDelete() {
    txt_id = $("#txt_id").text();
    $.ajax({
        url: linkProc + '/USER/ActionDelete',
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
                alert('Data has been Deleted');
                Refresh();
            },
    });
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

//create
$(function () {
    $('#dropdownList_role').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_role_id').text(optionSelected);
    });
})

$(function () {
    $('#dropdownList_div').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_division_id').text(optionSelected);
    });
})

//edit
$(function () {
    $('#TPROC_USER_DT_ROLE_ID').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_role_id').text(optionSelected);
    });
})

$(function () {
    $('#TPROC_USER_DT_DIVISION_ID').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_division_id').text(optionSelected);
    });
})

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

//function GenerateImage() {
//    var formData = new FormData();
//    var filex = document.getElementById("id-input-file-3").files[0];
//    formData.append("FileUpload", filex);

//    $.ajax({
//        url: "/User/GetFormData",
//        type: 'Post',
//        data: {
//            file: formData,
//        },
//        cache: false,
//        traditional: true,
//        beforeSend:
//            function () {
//                $("#loadingRole").toggle()
//            },
//        success:
//            function () {
//                $(".dialogForm").dialog("close");
//                alert('Data has been created');
//                Refresh();
//            },
//    });
//}

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
        url: '../USER/SendEmailCreateUser',
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
        success:
            function () {
                document.getElementById("loader").className = "";
                document.getElementById("email_sent").style.visibility = "visible";
                document.getElementById("input_wa").style.display = "none";
                document.getElementById("action_send_email").style.display = "none";
            },
    });
}

function DeleteSelected() {
    var item_detail = new Array();
    item_detail = GetDataTable();

    if (item_detail.length > 0) {
        $.ajax({
            url: fullOrigin + '/ActionDeleteSelected',
            type: 'Post',
            data: {
                ids: item_detail,
            },
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function () {

                    alert('Data has been Deleted');
                    Refresh();
                },
        });
    } else {
        alert("Please select data that want to delete")
    }

}

function GetDataTable() {
    var temp = new Array();
    $('#dynamic-table tbody tr  input:checkbox').each(function () {
        if (this.checked) {
            temp.push(this.id);
        }
    });

    return temp;
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
//})

function ActionActivating(txt_id) {
    $.ajax({
        url: linkProc + '/USER/ActionActiviting',
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
                alert('Data has been re-activated');
                Refresh();
            },
    });
}

function GetDataByRowStat() {
    var txt_row_stat;
    if ($("#rb_active").is(":checked")) {
        txt_row_stat = 0
    } else if ($("#rb_inactive").is(":checked")) {
        txt_row_stat = -1
    } else if ($("#rb_hibernate").is(":checked")) {        
        txt_row_stat = -2
    }

    $.ajax({
        url: linkProc + '/USER/GetDataByRowStat',
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

function UpdateHibernate() {
    $.ajax({
        url: linkProc + '/USER/UpdateHibernate',
        type: 'Post',
        data: {
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
//========================================End Setup User Script=============================================




//========================================Start Request Script==========================================
function BtnActionRequestUser(action) {
    if (IsValidForm() == 0) {
        //cek select
        if (IsValidSelect() == 0) {
            if (IsValidDate() == 0) {
                if (IsValidLength() == 0) {
                    if (IsValidTextbox() == 0) {
                        CheckDataRequestUser(action);
                    }
                }
            }
        }
    }
}

function CheckDataRequestUser(action) {
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
            rel_flag: $("#txt_user_id2").text(),
            control: 'User',
            actions: $("#txt_action").text(),
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_user_id2');
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

                if (action == "create") {
                    ActionRequestUserCreate();
                } else if (action == "edit") {
                    ActionRequestUserEdit();
                } else {
                    ActionRequestUserDelete();
                }                
            };
        }
    })   
}

function ActionRequestUserCreate() {
    var txt_user_id2 = $("#txt_user_id2").text();
    var txt_user_name = $("#txt_user_name").text();
    var txt_mail = $("#txt_mail").text();

    var txt_is_super_admin;
    $("#cb_is_super_admin").is(":checked") ? txt_is_super_admin = 1 : txt_is_super_admin = 0;

    var txt_is_eproc_admin;
    $("#cb_is_eproc_admin").is(":checked") ? txt_is_eproc_admin = 1 : txt_is_eproc_admin = 0;

    var txt_comp_cd;
    $("#rb_mami").is(":checked") ? txt_comp_cd = 1 : txt_comp_cd = 0;

    var txt_role_id = $("#txt_role_id").text();

    var wa_detail = new Array();
    wa_detail = GetDataWA();

    var txt_wa = $("#form-field-select-4").val();

    var txt_division_id = $("#txt_division_id").text();

    var txt_desc = $("#txt_desc").val();


    $.ajax({
        url: linkProc + '/USER/ActionRequestUserCreate',
        type: 'Post',
        data: {
            user_id: txt_user_id2,
            user_name: txt_user_name,
            user_mail: txt_mail,
            is_super_admin: txt_is_super_admin,
            is_eproc_admin: txt_is_eproc_admin,
            role_id: txt_role_id,
            comp_cd: txt_comp_cd,
            lwa_number: wa_detail,
            wa: txt_wa,
            division_id: txt_division_id,
            desc: txt_desc,
        },
        cache: false,
        traditional: true,
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success:
            function () {
                Refresh();
            },
    });
}

function ActionRequestUserEdit() {
    txt_id = $("#txt_id").text();
    var txt_user_id2 = $("#txt_user_id2").text();
    var txt_user_name = $("#txt_user_name").text();
    var txt_mail = $("#txt_mail").text();

    var txt_is_super_admin;
    $("#cb_is_super_admin").is(":checked") ? txt_is_super_admin = 1 : txt_is_super_admin = 0;

    var txt_is_eproc_admin;
    $("#cb_is_eproc_admin").is(":checked") ? txt_is_eproc_admin = 1 : txt_is_eproc_admin = 0;

    var txt_comp_cd;
    $("#rb_mami").is(":checked") ? txt_comp_cd = 1 : txt_comp_cd = 0;

    var txt_role_id = $("#txt_role_id").text();

    var wa_detail = new Array();
    wa_detail = GetDataWA();

    var txt_division_id = $("#txt_division_id").text();

    var txt_desc = $("#txt_desc").val();

    $.ajax({
        url: linkProc + '/USER/ActionRequestUserEdit',
        type: 'Post',
        data: {
            id: txt_id,
            user_id: txt_user_id2,
            user_name: txt_user_name,
            user_mail: txt_mail,
            is_super_admin: txt_is_super_admin,
            is_eproc_admin: txt_is_eproc_admin,
            role_id: txt_role_id,
            comp_cd: txt_comp_cd,
            lwa_number: wa_detail,
            division_id: txt_division_id,
            desc: txt_desc,
        },
        cache: false,
        traditional: true,
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

function ActionRequestUserComplete() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    var rs = $.xResponse(linkProc + '/USER/ActionRequestUserComplete',
    {
        id: txt_id,
        request_no: txt_request_no,
        actions: txt_actions,
    });

    if (rs == -1) {
        alert('Sent email failed');
    } else {
        alert('Request has been completed');
        Refresh();
    }
}

function ActionRequestUserDelete() {
    var txt_id = $("#txt_id").text();
    var txt_user_id = $("#txt_user_id2").text();
    var txt_desc = $("#txt_desc").val();

    $.ajax({
        url: linkProc + '/User/ActionRequestUserDelete',
        type: 'Post',
        data: {
            id: txt_id,
            user_id: txt_user_id,
            desc: txt_desc,
        },
        beforeSend: function () {
            $("#loadingRole").toggle()
        },
        success: function () {
            //$(".dialogForm").dialog("close");
            alert('Request has been sent');
            Refresh();
        },
    });
}


//========================================End Request Script===========================================

