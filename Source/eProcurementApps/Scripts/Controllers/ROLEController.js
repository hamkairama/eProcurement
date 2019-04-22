//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

//var fullOrigin = window.location.href;

function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                var id = $.xResponse(linkProc + '/ROLE/IsInActive/', { value: $("#txt_role_name").text() });
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
    txt_role_name = $("#txt_role_name").text();
    txt_id = 0;

    if (action == "create") {
        $.ajax({
            url: '../ROLE/CheckData',
            type: "Post",
            data: {
                id: txt_id,
                role_name: txt_role_name,
            },
            cache: false,
            traditional: true,
            error: function (response) {
                alert(response.responseText);
            },
            success: function (data) {
                var field = document.getElementById('required_txt_role_name');
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
    txt_role_name = $("#txt_role_name").text();
    txt_role_description = $("#txt_role_description").text();
    if ($("#cb_is_inactive").is(':checked')) {
        //if (document.getElementById('cb_is_inactive').is(':checked')) {
        txt_is_inactive = 1;
    }
    else {
        txt_is_inactive = 0;
    };

    var menus = GetMenusChecked();

    if ($("#cb_default_selected").is(':checked')) {
        txt_default_selected = 1;
    }
    else {
        txt_default_selected = 0;
    };

    $.ajax({
        url: '../ROLE/ActionCreate',
        type: 'Post',
        data: {
            role_name: txt_role_name,
            role_description: txt_role_description,
            is_inactive: txt_is_inactive,
            menus: menus,
            default_selected: txt_default_selected,
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
                location.reload();
            },
    });
}

function ActionEdit() {
    txt_id = $("#txt_id").text();
    txt_role_name = $("#txt_role_name").text();
    txt_role_description = $("#txt_role_description").text();
    if ($("#cb_is_inactive").is(':checked')) {
        //if (document.getElementById('cb_is_inactive').is(':checked')) {
        txt_is_inactive = 1;
    }
    else {
        txt_is_inactive = 0;
    };

    var menus = GetMenusChecked();

    if ($("#cb_default_selected").is(':checked')) {
        txt_default_selected = 1;
    }
    else {
        txt_default_selected = 0;
    };

    $.ajax({
        url: linkProc + '/ROLE/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            role_name: txt_role_name,
            role_description: txt_role_description,
            is_inactive: txt_is_inactive,
            menus: menus,
            default_selected: txt_default_selected,
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
                alert('Data has been edited');
                location.reload();
            },
    });
};


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


//adfdaf
function ActionDelete() {
    txt_id = $("#txt_id").text();
    var result = $.xResponse(fullOrigin + '/ActionDelete', { id: txt_id });
    if (result == "0") {
        alert("Role is being use by the another table");
    } else {
        $(".dialogForm").dialog("close");
        alert('Data has been Deleted');
        Refresh();
    }


    //txt_id = $("#txt_id").text();
    //$.ajax({
    //    url: '../ROLE/ActionDelete',
    //    type: 'Post',
    //    data: {
    //        id: txt_id,
    //    },
    //    beforeSend:
    //        function () {
    //            $("#loadingRole").toggle()
    //        },
    //    success:
    //        function () {
    //            $(".dialogForm").dialog("close");
    //            alert('Data has been Deleted');
    //            Refresh();
    //        },
    //});
};

function Refresh() {
    $.ajax({
        cache: false,
        url: '../ROLE/List',
        success: function (data) {
            $("#renderBody").html(data);
        }
    });
}

function BackToIndex() {
    $.ajax({
        url: '../ROLE/List',
        type: 'Post',
        cache: false,
        traditional: true,
    });
}

function GetMenuConverDict() {
    var fields = $(".submenu").find("input");

    var menus = new Array;

    for (i = 0; i < fields.length; ++i) {
        var item = fields[i]
        if (item.checked) {
            //menus.push(item.id);
            menus.push({ key: item.id, value: item.id })
        }

    };

    //var vars = new Array;
    //vars.push({ key: "newkey", value: "newvalue" })

    return menus;
}

function GetMenusChecked() {
    var fields = $(".submenu").find("input");

    var menus = new Array;

    for (i = 0; i < fields.length; ++i) {
        var item = fields[i]
        if (item.checked) {
            menus.push(item.id);
            menus.push(item.className)
        }

    };

    return menus;
}


function ActionActivating(txt_id) {
    $.ajax({
        url: linkProc + '/ROLE/ActionActiviting',
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
                window.location.reload(true);
            },
    });
};