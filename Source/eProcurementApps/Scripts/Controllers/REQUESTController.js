
//----------------------Star Clean and fixing-----------------------------
//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function Refresh() {
    //$.ajax({
    //    cache: false,
    //    url: linkProc + '/REQUEST/Index/0',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    window.location.reload(true);
}


$(function () {
    $('#dropdownList_user_del').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#user_id').text(optionSelected);
    });
})

function InputDelete() {
    var delete_area = document.getElementById('delete_area');
    //if ($("#cb_popup_account").is(":checked")) {
    $("#delete_area").toggle(500);
    //} else {
    //    budget_area.style.display = "none";
    //}
}


function GoDelete() {
    txt_id = $("#user_id").text();
    document.location.href = linkProc + '/USER/Delete/' + txt_id + "?flag=" + 1;
}


//----------------------End-----------------------------------------------






//function ActionRequestUserCreate() {
//    var txt_user_id2 = $("#txt_user_id2").text();
//    var txt_user_name = $("#txt_user_name").text();
//    var txt_mail = $("#txt_mail").text();

//    var txt_is_super_admin;
//    $("#cb_is_super_admin").is(":checked") ? txt_is_super_admin = 1 : txt_is_super_admin = 0;

//    var txt_is_eproc_admin;
//    $("#cb_is_eproc_admin").is(":checked") ? txt_is_eproc_admin = 1 : txt_is_eproc_admin = 0;

//    var txt_comp_cd;
//    $("#rb_mami").is(":checked") ? txt_comp_cd = 1 : txt_comp_cd = 0;

//    var txt_role_id = $("#txt_role_id").text();

//    var wa_detail = new Array();
//    wa_detail = GetDataWAUser();

//    var txt_wa = $("#form-field-select-4").val();

//    var txt_division_id = $("#txt_division_id").text();

//    var txt_desc = $("#txt_desc").val();


//    $.ajax({
//        url: linkProc + '/REQUEST/ActionRequestUserCreate',
//        type: 'Post',
//        data: {
//            user_id: txt_user_id2,
//            user_name: txt_user_name,
//            user_mail: txt_mail,
//            is_super_admin: txt_is_super_admin,
//            is_eproc_admin: txt_is_eproc_admin,
//            role_id: txt_role_id,
//            comp_cd: txt_comp_cd,
//            lwa_number: wa_detail,
//            wa: txt_wa,
//            division_id: txt_division_id,
//            desc: txt_desc,
//        },
//        cache: false,
//        traditional: true,
//        beforeSend:
//            function () {
//                $("#loadingRole").toggle()
//            },
//        success:
//            function () {
//                Refresh();
//            },
//    });
//}

//function ActionRequestUserEdit() {
//    txt_id = $("#txt_id").text();
//    var txt_user_id2 = $("#txt_user_id2").text();
//    var txt_user_name = $("#txt_user_name").text();
//    var txt_mail = $("#txt_mail").text();

//    var txt_is_super_admin;
//    $("#cb_is_super_admin").is(":checked") ? txt_is_super_admin = 1 : txt_is_super_admin = 0;

//    var txt_is_eproc_admin;
//    $("#cb_is_eproc_admin").is(":checked") ? txt_is_eproc_admin = 1 : txt_is_eproc_admin = 0;

//    var txt_comp_cd;
//    $("#rb_mami").is(":checked") ? txt_comp_cd = 1 : txt_comp_cd = 0;

//    var txt_role_id = $("#txt_role_id").text();

//    var wa_detail = new Array();
//    wa_detail = GetDataWAUser();

//    var txt_wa = $("#form-field-select-4").val();

//    var txt_division_id = $("#txt_division_id").text();

//    $.ajax({
//        url: linkProc + '/Request/ActionRequestUserEdit',
//        type: 'Post',
//        data: {
//            id: txt_id,
//            user_id: txt_user_id2,
//            user_name: txt_user_name,
//            user_mail: txt_mail,
//            is_super_admin: txt_is_super_admin,
//            is_eproc_admin: txt_is_eproc_admin,
//            role_id: txt_role_id,
//            comp_cd: txt_comp_cd,
//            lwa_number: wa_detail,
//            wa: txt_wa,
//            division_id: txt_division_id,
//        },
//        cache: false,
//        traditional: true,

//        error: function (response) {
//            alert(response.responseText);
//        },
//        beforeSend:
//            function () {
//                $("#loadingRole").toggle()
//            },
//        success:
//            function () {
//                $(".dialogForm").dialog("close");
//                alert('Request has been send');
//                Refresh();
//            },
//    });
//}

//function GetUserToBeDelete(id) {
//    $.ajax({
//        cache: false,
//        url: linkProc + '/REQUEST/GetUSerToBeDelete/' + id,
//        success: function (data) {
//            $("#renderBody").html(data);
//        }
//    });
//}   

//function ActionRequestUserDelete() {
//    txt_id = $("#txt_id").text();
//    $.ajax({
//        url: linkProc + '/Request/ActionRequestUserDelete',
//        type: 'Post',
//        data: {
//            id: txt_id,
//        },
//        beforeSend:
//            function () {
//                $("#loadingRole").toggle()
//            },
//        success:
//            function () {
//                $(".dialogForm").dialog("close");
//                alert('Request has been send');
//                Refresh();
//            },
//    });
//}



















