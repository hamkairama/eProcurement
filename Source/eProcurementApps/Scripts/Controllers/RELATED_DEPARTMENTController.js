////========================================Start Common Script============================================
//var linkProc;
//var fullOrigin = window.location.href;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function Refresh() {
    //$.ajax({
    //    cache: false,
    //    url: '../RELATED_DEPARTMENT/List',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    window.location.reload(true);
}
//========================================End Common Script================================================



//========================================Start Setup RD Script===========================================
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                var id = $.xResponse(linkProc + '/RELATED_DEPARTMENT/IsInActive/', { value: $("#txt_related_department_name").text() });
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
    txt_related_department_name = $("#txt_related_department_name").text();
    txt_id = 0;
    if (action == "create") {
        $.ajax({
            url: '../RELATED_DEPARTMENT/CheckData',
            type: "Post",
            data: {
                id: txt_id,
                related_department_name: txt_related_department_name,
            },
            cache: false,
            traditional: true,
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                var field = document.getElementById('required_txt_related_department_name');
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
    txt_related_department_name = $("#txt_related_department_name").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email and Limit Approval must be filled")
    } else {
        $.ajax({
            url: '../RELATED_DEPARTMENT/ActionCreate',
            type: 'Post',
            data: {
                related_department_name: txt_related_department_name,
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
                    $(".dialogForm").dialog("close");
                    alert('Data has been created');
                    Refresh();
                },
        });
    }
}

function ActionEdit() {
    txt_id = $("#txt_id").text();
    txt_related_department_name = $("#txt_related_department_name").text();

    var app_detail = new Array();
    app_detail = GetDataTable();
    if (app_detail.length == 0) {
        alert("User Id, User Name, Email and Limit Approval must be filled")
    } else {
        $.ajax({
            url: linkProc + '/RELATED_DEPARTMENT/ActionEdit',
            type: 'Post',
            data: {
                id: txt_id,
                related_department_name: txt_related_department_name,
                lapp_detail: app_detail,
            },
            cache: false,
            traditional: true,
            beforeSend: function () {
                $("#loadingRole").toggle()
            },
            success: function () {
                $(".dialogForm").dialog("close");
                alert('Data has been edited');
                Refresh();
            },
        });
    }
}

function ActionDelete() {
    txt_id = $("#txt_id").text();
    $.ajax({
        url: linkProc + '/RELATED_DEPARTMENT/ActionDelete',
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

        var cellVal0 = "";
        var cellVal1 = "";
        var cellVal2 = "";
        var cellVal3 = "";
        for (var j = 0; j < cellLength; j++) {
            var value = oCells.item(j).firstElementChild.value;
            if (j == 0) {
                cellVal0 = value;
            }
            else if (j == 1) {
                cellVal1 = value;
            }
            else if (j == 2) {
                cellVal2 = value;
            }
            else {
                cellVal3 = value;
            }
        }

        if (cellVal0 != "" && cellVal1 != "" && cellVal2 != "" && cellVal3 != 0) {
            temp.push(cellVal0 + "|" + cellVal1 + "|" + cellVal2 + "|" + cellVal3);
        }
    }

    if (rowLength != temp.length) {
        temp = new Array();
    }

    return temp;
}

function ActionActivating(txt_id) {
    $.ajax({
        url: linkProc + '/RELATED_DEPARTMENT/ActionActiviting',
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
//========================================End Setup RD Script=============================================




//========================================Start Request Script=============================================
function BtnActionRequestRD(action) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                if (IsValidTextbox() == 0) {
                    CheckDataRequestRD(action);
                }
            }
        }
    }
}

function CheckDataRequestRD(action) {
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
            rel_flag: $("#txt_related_department_name").text(),
            control: 'RD',
            actions: $("#txt_action").text(),
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_related_department_name');
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
                    ActionRequestRDCreate();
                } else if (action == "edit") {
                    ActionRequestRDEdit();
                } else  {
                    ActionRequestRDDelete();
                }
            };
        }
    })

    
}

function ActionRequestRDCreate() {
    txt_related_department_name = $("#txt_related_department_name").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    var txt_desc = $("#txt_desc").val();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email and Limit Approval must be filled")
    } else {
        $.ajax({
            url: linkProc + '/RELATED_DEPARTMENT/ActionRequestRDCreate',
            type: 'Post',
            data: {
                related_department_name: txt_related_department_name,
                lapp_detail: app_detail,
                desc: txt_desc,
            },
            cache: false,
            traditional: true,
            beforeSend: function () {
                $("#loadingRole").toggle()
            },
            success: function () {
                $(".dialogForm").dialog("close");
                alert('Request has been sent');
                Refresh();
            },
        });
    }
}

function ActionRequestRDComplete() {
    txt_id = $("#txt_id").text();
    //txt_related_department_name = $("#txt_related_department_name").text();

    //var app_detail = new Array();
    //app_detail = GetDataTable();

    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    //if (app_detail.length == 0) {
    //    alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    //} else {
    var rs = $.xResponse(linkProc + '/RELATED_DEPARTMENT/ActionRequestRDComplete',
    {
        id: txt_id,
        //related_department_name: txt_related_department_name,
        //lapp_detail: app_detail,
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

function ActionRequestRDApprove() {
    var txt_id = $("#txt_id").text();
    txt_request_no = $("#txt_request_no").text();
    txt_actions = $("#txt_actions").text();

    var rs = $.xResponse(linkProc + '/RELATED_DEPARTMENT/ActionRequestRDApprove',
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

function ActionRequestRDEdit() {
    var txt_id = $("#txt_id").text();
    txt_related_department_name = $("#txt_related_department_name").text();

    var app_detail = new Array();
    app_detail = GetDataTable();

    var txt_desc = $("#txt_desc").val();

    if (app_detail.length == 0) {
        alert("User Id, User Name, Email, flow and Limit Approval must be filled")
    } else {
        $.ajax({
            url: linkProc + '/RELATED_DEPARTMENT/ActionRequestRDEdit',
            type: 'Post',
            data: {
                id: txt_id,
                related_department_name: txt_related_department_name,
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

function ActionRequestRDDelete() {
    txt_id = $("#txt_id").text();
    txt_related_department_name = $("#txt_related_department_name").text();
  
    var app_detail = new Array();
    app_detail = GetDataTable();

    var txt_desc = $("#txt_desc").val();

    $.ajax({
        url: linkProc + '/RELATED_DEPARTMENT/ActionRequestRDDelete',
        type: 'Post',
        data: {
            id: txt_id,
            related_department_name: txt_related_department_name,
            lapp_detail: app_detail,
            desc: txt_desc,
        },
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
}

//========================================End Request Script===============================================






