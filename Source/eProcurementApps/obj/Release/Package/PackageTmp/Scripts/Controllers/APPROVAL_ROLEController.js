var fullOrigin = window.location.origin + "/" + window.location.pathname.split("/")[1];

function BtnAction(action) {
    fullOrigin = window.location.origin + "/" + window.location.pathname.split("/")[1];
    if (action == "create") {
        ActionCreate();
        return false;
    } else if (action == "edit") {
        ActionEdit();
        return false;
    } else if (action == "delete") {
        ActionDelete();
        return false;
    } 
}

function ActionCreate() {
    if ($("#txt_userid").text().trim() == "") {
        alert("Please choose user id ");
        return false 
    } else if ($("#txt_levelid").text().trim() == "" || $("#txt_levelid").text().trim() == "0") {
        alert("Please choose level ");
        return false
    };

    txt_approval_role_userid = $("#txt_userid").text().trim();
    txt_approval_role_username = $("#txt_username").text().trim();
    txt_approval_role_email = $("#txt_email").text().trim();
    txt_approval_role_levelid = $("#txt_levelid").text().trim();
    txt_Signature = $('#txt_SIGNATURE_IMAGE').attr('src');

    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            var rowName;
            if ((i % 5) == 0) {
                row = {};
                rowName = header[0];
                row[rowName] = $(this).text().trim();
            } else if ((i % 5) == 1) {
                rowName = header[1];
                row[rowName] = $(this).find("input").is(":checked");
            } else if ((i % 5) == 2) {
                rowName = header[2];
                row[rowName] = $(this).find("input").is(":checked");
            } else if ((i % 5) == 3) {
                rowName = header[3];
                row[rowName] = $(this).find("input").is(":checked"); 
            } else if ((i % 5) == 4) {
                rowName = header[4];
                row[rowName] = $(this).find("input").is(":checked");
                rows.push(row);
            }
        });
        return rows;
    }).get();

    $.ajax({
        url: linkProc + '/APPROVAL_ROLE/ActionCreate',
        type: 'Post',
        data: {
            USER_ID: txt_approval_role_userid,
            USER_NAME: txt_approval_role_username,
            EMAIL: txt_approval_role_email,
            LEVEL_ID: txt_approval_role_levelid,
            SIGNATURE_IMAGE: txt_Signature,
            _JSONDetailDataTable: JSON.stringify(tableObj),
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
    txt_approval_role_id = $("#Approval_Role_Id").text();
    txt_approval_role_userid = $("#txt_userid").text().trim();
    txt_approval_role_username = $("#txt_username").text().trim();
    txt_approval_role_email = $("#txt_email").text().trim(); 
    txt_approval_role_levelid = $("#txt_levelid").text().trim();
    txt_Signature = $('#txt_SIGNATURE_IMAGE').attr('src');
    
    var header = $('table thead tr th').map(function () {
        return $(this).text();
    });

    var rows = [];
    var tableObj = $('#table_ap').map(function () {
        var row;
        $(this).find('td').each(function (i) {
            var rowName;
            if ((i % 5) == 0) {
                row = {};
                rowName = header[0];
                row[rowName] = $(this).text().trim();
            } else if ((i % 5) == 1) {
                rowName = header[1];
                row[rowName] = $(this).find("input").is(":checked");
            } else if ((i % 5) == 2) {
                rowName = header[2];
                row[rowName] = $(this).find("input").is(":checked");
            } else if ((i % 5) == 3) {
                rowName = header[3];
                row[rowName] = $(this).find("input").is(":checked"); 
            } else if ((i % 5) == 4) {
                rowName = header[4];
                row[rowName] = $(this).find("input").is(":checked");
                rows.push(row);
            }
        });
        return rows;
    }).get();

    $.ajax({
        url: linkProc + '/APPROVAL_ROLE/ActionEdit',
        type: 'Post',
        data: {
            ID: txt_approval_role_id,
            USER_ID: txt_approval_role_userid,
            USER_NAME: txt_approval_role_username,
            EMAIL: txt_approval_role_email, 
            LEVEL_ID: txt_approval_role_levelid,
            SIGNATURE_IMAGE: txt_Signature,
            _JSONDetailDataTable: JSON.stringify(tableObj),
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
    txt_id = $("#Approval_Role_Id").text();
    $.ajax({
        url: linkProc + '/APPROVAL_ROLE/ActionDelete',
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

function Refresh() {
    this.document.location.href = linkProc + '/APPROVAL_ROLE';
}
 
$(function () {
    $('#dropdownList_levelid_div').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_levelid').text(optionSelected);
    });
}) 

