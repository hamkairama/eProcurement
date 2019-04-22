//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();
//};

function Show(flag) {
    if (flag == 0) {
        div_user_email = document.getElementById("div_user_email");
        div_user_email.style.display = 'none';
        $("#txt_user_email").val("");
        $("#div_user_id").toggle(500);
    } else {
        div_user_id = document.getElementById("div_user_id");
        div_user_id.style.display = 'none';
        $("#txt_user_id").val("");
        $("#div_user_email").toggle(500);
    }
}

function Search(flag) {
    if (flag == 0) {
        SearchByUserId();
    } else {
        SearchByEmail();
    }
}

function SearchByUserId() {
    user_id = $("#txt_user_id").val();
    $.ajax({
        url: linkProc + '/ACTIVE_DIRECTORY/FindByUserId',
        cache: false,
        traditional: true,
        data: {
            user_id: user_id,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success: function (data) {
            $("#table_ad").html(data);
            $("#loadingRole").toggle()
        }
    });
}

function SearchByEmail() {
    email = $("#txt_user_email").val();
    $.ajax({
        url: linkProc + '/ACTIVE_DIRECTORY/FindByEmail',
        cache: false,
        traditional: true,
        data: {
            email: email,
        },
        beforeSend:
            function () {
                $("#loadingRole").toggle()
            },
        success: function (data) {
            $("#table_ad").html(data);
            $("#loadingRole").toggle()
        }
    });
}

function SearchKey(event, flag) {
    if (event.keyCode == 13) {
        if (flag == 0) {
            SearchByUserId();
        } else {
            SearchByEmail();
        }
    }
}

function Passing(flag, no) {
    if (flag == "User") {
        PassingDataUser(no);
    }
    else if (flag == "WA") {
        PassingDataWA(no);
    }
    else if (flag == "RelDept") {
        PassingDataRelDept(no);
    }
    else if (flag == "Request") {
        PassingDataApprReq(no);
    }
    else if (flag == "APPROVAL_ROLE") {
        PassingDataApprovalRole(no);
    }
}

function PassingDataUser(no) {
    var user_id = $("#txt_user_" + no).text();
    var name = $("#txt_name_" + no).text();
    var email = $("#txt_email_" + no).text();
    
    var r_user_id = document.getElementById("txt_user_id2");
    var r_user_name = document.getElementById("txt_user_name");
    var r_mail = document.getElementById("txt_mail");

    r_user_id.innerHTML = user_id;
    r_user_name.innerHTML = name;
    r_mail.innerHTML = email;
    r_user_id.style.color = 'black';
    r_user_name.style.color = 'black';
    r_mail.style.color = 'black';
    $(".dialogForm").dialog("close");
}

function PassingDataRelDept(no) {
    var user_id = $("#txt_user_" + no).text();
    var user_name = $("#txt_name_" + no).text();
    var email = $("#txt_email_" + no).text();

    var r_iRow = iRow;
    var r_user_id, r_user_name, r_email;

    if (r_iRow == 0) {
        r_user_id = document.getElementById("txt_user_id2_");
        r_user_name = document.getElementById("txt_user_name2_");
        r_email = document.getElementById("txt_user_email2_");
    }
    else {
        r_user_id = document.getElementById("txt_user_id2_" + r_iRow);
        r_user_name = document.getElementById("txt_user_name2_" + r_iRow);
        r_email = document.getElementById("txt_user_email2_" + +r_iRow);
    }

    r_user_id.value = user_id;
    r_user_name.value = user_name;
    r_email.value = email;
    r_user_id.style.color = 'black';
    r_email.style.color = 'black';
    $(".dialogForm").dialog("close");
}

function PassingDataWA(no) {
    var user_id = $("#txt_user_" + no).text();
    var user_name = $("#txt_name_" + no).text();
    var email = $("#txt_email_" + no).text();

    var r_iRow = iRow;
    var r_user_id, r_user_name, r_email;

    if (r_iRow == 0) {
        r_user_id = document.getElementById("txt_user_id2_");
        r_user_name = document.getElementById("txt_user_name2_");
        r_email = document.getElementById("txt_user_email2_");
    }
    else {
        r_user_id = document.getElementById("txt_user_id2_" + r_iRow);
        r_user_name = document.getElementById("txt_user_name2_" + r_iRow);
        r_email = document.getElementById("txt_user_email2_" + +r_iRow);
    }

    r_user_id.value = user_id;
    r_user_name.value = user_name;
    r_email.value = email;
    r_user_id.style.color = 'black';
    r_email.style.color = 'black';
    $(".dialogForm").dialog("close");
}

function PassingDataApprovalRole(no) {
    var user_id = $("#txt_user_" + no).text();
    var user_name = $("#txt_name_" + no).text();
    var email = $("#txt_email_" + no).text(); 
    var r_user_id, r_user_name, r_email;

    r_user_id = document.getElementById("txt_userid");
    r_user_name = document.getElementById("txt_username");
    r_email = document.getElementById("txt_email");

    r_user_id.innerHTML = user_id;
    r_user_name.innerHTML = user_name;
    r_email.innerHTML = email;
    r_user_id.style.color = 'black';
    r_email.style.color = 'black';
    $(".dialogForm").dialog("close");
}

function GetData() {
    var table = document.getElementById('#table_ad');
    for (var i = 0, row; row = table.rows[1]; i++) {
        //iterate through rows
        //rows would be accessed using the "row" variable assigned in the for loop
        for (var j = 0, col; col = row.cells[j]; j++) {
            //iterate through columns
            //columns would be accessed using the "col" variable assigned in the for loop
        }
    }
}

function PassingDataApprReq(no) {
    var user_id = $("#txt_user_" + no).text();
    var user_name = $("#txt_name_" + no).text();
    var email = $("#txt_email_" + no).text();

    var r_user_id, r_user_name, r_email;

    var r_user_id = document.getElementById("txt_user_id2");
    var r_user_name = document.getElementById("txt_user_name");
    var r_email = document.getElementById("txt_mail");

    r_user_id.value = user_id;
    r_user_name.value = user_name;
    r_email.value = email;
    r_user_id.style.color = 'black';
    r_email.style.color = 'black';
    $(".dialogForm").dialog("close");
}


