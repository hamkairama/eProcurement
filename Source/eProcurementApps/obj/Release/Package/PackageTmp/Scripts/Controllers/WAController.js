////========================================Start Common Script============================================
//var linkProc;
//var fullOrigin = window.location.href;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function Refresh() {
    //$.ajax({
    //    cache: false,
    //    url: '../WA/Index',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    //location.reload();
    window.location.reload(true);
}

//========================================End Common Script==============================================



//========================================Start Setup WA Script===========================================
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                if (IsOnlyNumberInput() == 0) {
                    var id = $.xResponse(linkProc + '/WA/IsInActive/', {
                        value: $("#txt_wa_number").text()
                    });
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
    var txt_wa_number = $("#txt_wa_number").text();
    var txt_id = 0;

    if (action == "create") {
        $.ajax({
            url: '../WA/CheckData',
            type: "Post",
            data: {
                id: txt_id,
                wa_number: txt_wa_number,
            },
            cache: false,
            traditional: true,
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                var field = document.getElementById('required_txt_wa_number');
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
    var txt_wa_number = $("#txt_wa_number").text();
    var txt_dept_name = $("#txt_dept_name").text();
    var txt_division_id = $("#txt_division_id").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    } else {
        $.ajax({
            url: '../WA/ActionCreate',
            type: 'Post',
            data: {
                wa_number: txt_wa_number,
                dept_name: txt_dept_name,
                division_id: txt_division_id,
                lapp_detail: app_detail,
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
                    alert('Data has been created');
                    Refresh();
                },
        });
    };
}

function ActionEdit() {
    var txt_id = $("#txt_id").text();
    var txt_wa_number = $("#txt_wa_number").text();
    var txt_dept_name = $("#txt_dept_name").text();
    var txt_division_id = $("#txt_division_id").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    } else {
        $.ajax({
            url: linkProc + '/WA/ActionEdit',
            type: 'Post',
            data: {
                id: txt_id,
                wa_number: txt_wa_number,
                dept_name: txt_dept_name,
                division_id: txt_division_id,
                lapp_detail: app_detail,
            },
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function () {
                    //$(".dialogForm").dialog("close");
                    alert('Data has been Edited');
                    Refresh();
                },
        });
    };

}

function ActionDelete() {
    var txt_id = $("#txt_id").text();
    $.ajax({
        url: linkProc + '/WA/ActionDelete',
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
                //$(".dialogForm").dialog("close");
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

        var cellVal0 = "";
        var cellVal1 = "";
        var cellVal2 = "";
        var cellVal3 = "";
        var cellVal4 = "";
        for (var j = 0; j < cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;
            if (j == 0) {
                cellVal0 = value; //approval name
            }
            else if (j == 1) {
                cellVal1 = value; //user name
            }
            else if (j == 2) {
                cellVal2 = value; //email
            }
            else if (j == 3) {
                cellVal3 = value; //level id
            }
            else {
                cellVal4 = value; //flow number
            }
        }

        if (cellVal0 != "" && cellVal1 != "" && cellVal2 != "" && cellVal3 != 0 && cellVal4 !="") {
            temp.push(cellVal0 + "|" + cellVal1 + "|" + cellVal2 + "|" + cellVal3 + "|" + cellVal4);
        }
    }

    if (rowLength != temp.length) {
        temp = new Array();
    }

    return temp;
}

//create
$(function () {
    $('#dropdownList_div').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_division_id').text(optionSelected);
    });
})

//edit
$(function () {
    $('#TPROC_APPROVAL_GR_DIVISION_ID').change(function () {
        var optionSelected = $(this).find('option:selected').attr('value');
        $('#txt_division_id').text(optionSelected);
    });
})

function DeleteSelected() {
    var item_detail = new Array();
    item_detail = GetDataTableCheck();

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
                    //$(".dialogForm").dialog("close");
                    alert('Data has been Deleted');
                    Refresh();
                },
        });
    } else {
        alert("Please select data that want to delete")
    }
}

function GetDataTableCheck() {
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
//});

function ActionActivating(txt_id) {
    $.ajax({
        url: linkProc + '/WA/ActionActiviting',
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
//========================================End Setup WA Script=============================================




//========================================Start Request Script=============================================
function BtnActionRequestWA(action) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                if (IsOnlyNumberInput() == 0) {
                    if (IsValidTextbox() == 0) {
                        CheckDataRequestWA(action);
                    }
                }
            }
        }
    }
}

function CheckDataRequestWA(action) {
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
            rel_flag: $("#txt_wa_number").text(),
            control: 'WA',
            actions: $("#txt_action").text(),
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_wa_number');
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
                    ActionRequestWACreate();
                } else if (action == "edit") {
                    ActionRequestWAEdit();
                } else {
                    ActionRequestWADelete();
                }
            };
        }
    })

   
}

function ActionRequestWACreate() {
    var txt_wa_number = $("#txt_wa_number").text();
    var txt_dept_name = $("#txt_dept_name").text();
    var txt_division_id = $("#txt_division_id").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    var txt_desc = $("#txt_desc").val();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    } else {
        $.ajax({
            url: linkProc + '/WA/ActionRequestWACreate',
            type: 'Post',
            data: {
                wa_number: txt_wa_number,
                dept_name: txt_dept_name,
                division_id: txt_division_id,
                lapp_detail: app_detail,
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
            //$(".dialogForm").dialog("close");
            alert('Request has been sent');
            Refresh();
        },
        });
    };
}

function ActionRequestWAComplete() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    var rs = $.xResponse(linkProc + '/WA/ActionRequestWAComplete',
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

function ActionRequestWAApprove() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    var rs = $.xResponse(linkProc + '/WA/ActionRequestWAApprove',
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

function ActionRequestWAEdit() {
    var txt_id = $("#txt_id").text();
    var txt_wa_number = $("#txt_wa_number").text();
    var txt_dept_name = $("#txt_dept_name").text();
    var txt_division_id = $("#txt_division_id").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    var txt_desc = $("#txt_desc").val();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    } else {
        $.ajax({
            url: linkProc + '/WA/ActionRequestWAEdit',
            type: 'Post',
            data: {
                id: txt_id,
                wa_number: txt_wa_number,
                dept_name: txt_dept_name,
                division_id: txt_division_id,
                lapp_detail: app_detail,
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
            //$(".dialogForm").dialog("close");
            alert('Request has been sent');
            Refresh();
        },
        });
    };
}

function ActionRequestWADelete() {
    var txt_id = $("#txt_id").text();
    var txt_wa_number = $("#txt_wa_number").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    var txt_desc = $("#txt_desc").val();

    $.ajax({
        url: linkProc + '/WA/ActionRequestWADelete',
        type: 'Post',
        data: {
            id: txt_id,
            wa_number: txt_wa_number,
            lapp_detail: app_detail,
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

//========================================End Request Script===============================================






