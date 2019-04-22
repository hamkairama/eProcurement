//=======================================================================start public parameters=======================================================
var iRowItem;
//=======================================================================end==========================================================================



$(function () {
    $('#dropdownList_status_pc').change(function () {
        var optionSelected = $(this).find('option:selected').text();
        $('#txt_status_pc_val').val(optionSelected);
    });   
});

function CheckAcknowledgeUser() {
    //var cb_is_acknowledge_user;
    if ($("#cb_is_acknowledge_user").is(":checked")) {
        
        var url = linkProc +  '/PRICE_COMPARISON/PopupAcknowledgeUser/';
        var form = '.dialogForm';

        ModalPop2(url, form);   
    } else {
        //alert("no");
    };
}

function GetWaId(row) {
    iRowItem = GetRowIndex(row) - 1;
    var val = row.value;
    var dt = val.split("-");

    GetApprovalWa(dt[0]);
    SetWaIdNWaNumb(dt[0], dt[1])
}

function GetApprovalWa(wa_id) {
    $.ajax({
        data: {
            wa_id: wa_id
        },
        cache: false,
        url: linkProc + '/PRICE_COMPARISON/ListApprovalWa',
        success: function (data) {
            $("#wa_id_table").html(data);
        }
    });
}

function PassingApprovalToRow() {
    var concat = GetMenusChecked();

    var r_appr
    if (iRowItem == 0) {
        r_appr = document.getElementById("txt_approval_");
       
    } else {
        r_appr = document.getElementById("txt_approval_" + iRowItem);
        
    }

    r_appr.value = concat;
}

function SetWaIdNWaNumb(wa_id, wa_numb) {
    var r_wa_id, r_wa_numb;
    if (iRowItem == 0) {
        r_wa_id = document.getElementById("txt_wa_id_");
        r_wa_numb = document.getElementById("txt_wa_numb_");

    } else {
        r_wa_id = document.getElementById("txt_wa_id_" + iRowItem);
        r_wa_numb = document.getElementById("txt_wa_numb_" + iRowItem);

    }

    r_wa_id.value = wa_id;
    r_wa_numb.value = wa_numb;
}


function GetMenusChecked() {
    var fields = $(".appx").find("input");

    var apprls = "";

    for (i = 0; i < fields.length; ++i) {
        var item = fields[i]
        if (item.checked) {
            var y = item.id;
            apprls = apprls + "|" + y;
            //menus.push(item.id);
            //menus.push(item.className)
        }

    };

    return apprls;
}

var listOfObjectsWa = [];
//var listOfObjectsApproval = [];

function GetWaApproval() {
    var y = document.getElementById('tableAcknowledgeUser');
    var wa_apprv_empty = 0;

    listOfObjectsWa = [];
    for (var i = 1; i < y.rows.length; i++) {

        var singleObj = {}
        singleObj['Wa_id'] = y.rows[i].cells['0'].childNodes['0'].value;
        singleObj['Wa_number'] = y.rows[i].cells['1'].childNodes['0'].value;
        singleObj['Wa_approval'] = y.rows[i].cells['3'].childNodes['0'].value;

        //<<zoer20170925
        if (singleObj['Wa_approval'] == "" || singleObj['Wa_approval'] == null) {
            wa_apprv_empty = 1;
        } else {
            listOfObjectsWa.push(singleObj);
        }
        //>>zoer20170925
    }

    //<<zoer20170925
    if (wa_apprv_empty == 0) {
        $(".dialogForm").dialog("close");
    } else {
        alert("Approval cannot be empty");
    }
    //>>zoer20170925
}

//<<zoer20170925
function ClosePopupAck() {
    $("#cb_is_acknowledge_user").prop("checked", false)
    $(".dialogForm").dialog("close");
}
//>>zoer20170925


$(function () {
    var $research = $('.research');
    $research.find('.hidden-row').hide();

    //$research.find('.accordion').click(function () {
    //    $research.find('.accordion').not(this).nextAll('.hidden-row:first').fadeOut(500);
    //    $(this).nextAll('.hidden-row:first').fadeToggle(500);
    //});
});

function Accourdionclick() {
    var $research = $('.research');
    $research.find('.hidden-row').fadeToggle(500);
    //$(this).nextAll('.hidden-row:first').fadeToggle(500);
}

function ActionRejectPC(by_on) {
    var reason = $("#txt_reason_reject_pc").val();
    if (reason == "") {
        $("#input_reason_pc").toggle(500);
        $("#txt_reason_reject_pc").val("").focus();
        alert("Please insert the reason");
    } else {
        SaveRejectPc(by_on);
    }
}

function SaveRejectPc(r_by_on) {
    txt_pc_id = $("#txt_pc_id").text();
    txt_pc_date = ConvertDate($("#txt_created_dt").text());
    txt_reason = $("#txt_reason_reject_pc").val();
    txt_by_on = r_by_on;
    result = $.xResponsePO(linkProc + '/PRICE_COMPARISON/Reject/', {
        pc_id: txt_pc_id,
        pc_dt: txt_pc_date,
        reason: txt_reason,
        by_on: txt_by_on,
    });

    if (result != undefined || result != null) {
        var arry_result = result.split('|');
        if (arry_result[0] == 'False') {
            msg_error.style.display = '';
            msg_error.textContent = arry_result[1];
            alert("fail");
        } else {
            msg_error.style.display = 'none';
            Refresh();
        }
    } else {
        alert("error system");
    }
}


function PushEmailByPC() {
    var txt_pc_id = $("#txt_pc_id").text();
    var txt_pc_number = $("#txt_number").text();
    var txt_pc_date = $("#txt_created_dt").text();
    var txt_pc_type = $("#txt_created_dt").text();

    var urlMethod = '/PRICE_COMPARISON/SentPushEmailByPC/';

    $.ajax({
        url: linkProc + urlMethod,
        type: 'Post',
        cache: false,
        traditional: true,
        data: {
            pc_id: txt_pc_id,
            pc_number: txt_pc_number,
            pc_date: txt_pc_date,
            pc_type: txt_pc_type,
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
                alert("Email has been pushed");
            },
    });
}

//================================================================================= start testing =====================================================================

//============================================================================ end testing ===============================================================================