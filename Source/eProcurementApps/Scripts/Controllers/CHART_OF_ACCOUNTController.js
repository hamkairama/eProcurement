////========================================Start Common Script============================================
//var fullOrigin = window.location.href;
//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function Refresh() {
    window.location.reload(true);
}

//========================================End Common Script================================================



//========================================Start Setup COA Script===========================================
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLengthNonMandatory() == 0) {
                //var id = $.xResponse(fullOrigin + '/IsInActive/', { value: $("#txt_acct_num").text() });
                var id = $.xResponse(linkProc + '/CHART_OF_ACCOUNT/IsInActive/', { value: $("#txt_acct_num").text() });
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
    var txt_coa_number = $("#txt_acct_num").text();
    var txt_id = 0;
    if (action == "create") {
        $.ajax({
            url: '../CHART_OF_ACCOUNT/CheckData',
            type: "Post",
            data: {
                id: txt_id,
                acct_num: txt_coa_number,
            },
            cache: false,
            traditional: true,
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                var field = document.getElementById('required_txt_acct_num');
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
    txt_crcy_cd = $("#txt_crcy_cd").text();
    txt_acct_num = $("#txt_acct_num").text();
    txt_acct_desc = $("#txt_acct_desc").text();
    txt_converted_acct_num = $("#txt_converted_acct_num").text();

    $.ajax({
        url: '../CHART_OF_ACCOUNT/ActionCreate',
        type: 'Post',
        data: {
            crcy_cd: txt_crcy_cd,
            acct_num: txt_acct_num,
            acct_desc: txt_acct_desc,
            converted_acct_num: txt_converted_acct_num,
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
    txt_crcy_cd = $("#txt_crcy_cd").text();
    txt_acct_num = $("#txt_acct_num").text();
    txt_acct_desc = $("#txt_acct_desc").text();
    txt_converted_acct_num = $("#txt_converted_acct_num").text();

    $.ajax({
        url: linkProc + '/CHART_OF_ACCOUNT/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            crcy_cd: txt_crcy_cd,
            acct_num: txt_acct_num,
            acct_desc: txt_acct_desc,
            converted_acct_num: txt_converted_acct_num,
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
    $.ajax({
        url: linkProc + '/CHART_OF_ACCOUNT/ActionDelete',
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
        url: linkProc + '/CHART_OF_ACCOUNT/ActionActiviting',
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

//========================================End Setup COA Script=============================================




//========================================Start Request Script=============================================
function BtnActionRequestCOA(action) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                if (IsOnlyNumberInput() == 0) {
                    if (IsValidTextbox() == 0) {
                        CheckDataRequestCOA(action);
                    }
                }
            }
        }
    }
}

function CheckDataRequestCOA(action) {
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
            rel_flag: $("#txt_acct_num").text(),
            control: 'CHART_OF_ACCOUNT',
            actions: $("#txt_action").text(),
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_acct_num');
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
                        ActionRequestCOACreate();
                    } else if (action == "edit") {
                        ActionRequestCOAEdit();

                    } else {
                        ActionRequestCOADelete();
                    }
                }


            };
        }
    })

}

function ActionRequestCOACreate() {
    txt_crcy_cd = $("#txt_crcy_cd").text();
    txt_acct_num = $("#txt_acct_num").text();
    txt_acct_desc = $("#txt_acct_desc").text();
    txt_desc = $("#txt_desc").val();
    txt_appr_nm = $("#txt_user_name").val();
    txt_appr_email = $("#txt_mail").val();
    txt_converted_acct_num = $("#txt_converted_acct_num").text();

    $.ajax({
        url: linkProc + '/CHART_OF_ACCOUNT/ActionRequestCOACreate',
        type: 'Post',
        data: {
            crcy_cd: txt_crcy_cd,
            acct_num: txt_acct_num,
            acct_desc: txt_acct_desc,
            desc: txt_desc,
            appr_nm: txt_appr_nm,
            appr_email: txt_appr_email,
            converted_acct_num: txt_converted_acct_num,
        },
        cache: false,
        traditional: true,
        beforeSend:
        function () {
            $("#loadingRole").toggle()
        },
        success:
    function () {
        //$(".dialogForm").dialog("close");
        alert('Request has been sent');
        Refresh();
    },
    });
}

function ActionRequestCOAComplete() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    //if (app_detail.length == 0) {
    //    alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    //} else {
    var rs = $.xResponse(linkProc + '/CHART_OF_ACCOUNT/ActionRequestCOAComplete',
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
        //document.getElementById('complete_request').style.visibility = 'hidden';
    }
    //};
}

function ActionRequestCOAApprove() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    var rs = $.xResponse(linkProc + '/CHART_OF_ACCOUNT/ActionRequestCOAApprove',
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

function ActionRequestCOADelete() {
    txt_id = $("#txt_id").text();
    txt_acct_num = $("#txt_acct_num").text();
    txt_desc = $("#txt_desc").val();
    txt_appr_nm = $("#txt_user_name").val();
    txt_appr_email = $("#txt_mail").val();

    $.ajax({
        url: linkProc + '/CHART_OF_ACCOUNT/ActionRequestCOADelete',
        type: 'Post',
        data: {
            id: txt_id,
            acct_num: txt_acct_num,
            desc: txt_desc,
            appr_nm: txt_appr_nm,
            appr_email: txt_appr_email,
        },
        beforeSend: function () {
            $("#loadingRole").toggle()
        },
        success: function () {
            alert('Request has been sent');
            Refresh();
        },
    });
}

function ActionRequestCOAEdit() {
    txt_id = $("#txt_id").text();
    txt_crcy_cd = $("#txt_crcy_cd").text();
    txt_acct_num = $("#txt_acct_num").text();
    txt_acct_desc = $("#txt_acct_desc").text();
    txt_desc = $("#txt_desc").val();
    txt_appr_nm = $("#txt_user_name").val();
    txt_appr_email = $("#txt_mail").val();
    txt_converted_acct_num = $("#txt_converted_acct_num").text();

    $.ajax({
        url: linkProc + '/CHART_OF_ACCOUNT/ActionRequestCOAEdit',
        type: 'Post',
        data: {
            id: txt_id,
            crcy_cd: txt_crcy_cd,
            acct_num: txt_acct_num,
            acct_desc: txt_acct_desc,
            desc: txt_desc,
            appr_nm: txt_appr_nm,
            appr_email: txt_appr_email,
            converted_acct_num: txt_converted_acct_num,
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