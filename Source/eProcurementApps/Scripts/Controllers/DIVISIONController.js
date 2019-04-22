//var fullOrigin = window.location.href;
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                var id = $.xResponse(fullOrigin + '/IsInActive/', { value: $("#txt_division_name").text() });
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
    txt_division_name = $("#txt_division_name").text();
    txt_id = 0;
    if (action == "edit") {
        txt_id = $("#txt_id").text();
    };
    $.ajax({
        url: fullOrigin + '/CheckData',
        type: "Post",
        data: {
            id: txt_id,
            division_name: txt_division_name,
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_division_name');
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

function ActionCreate() {
    txt_division_name = $("#txt_division_name").text();
    txt_division_description = $("#txt_division_description").text();
    $.ajax({
        url: fullOrigin + '/ActionCreate',
        type: 'Post',
        data: {
            division_name: txt_division_name,
            division_description: txt_division_description
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
    txt_division_name = $("#txt_division_name").text();
    txt_division_description = $("#txt_division_description").text();
    $.ajax({
        url: fullOrigin + '/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            division_name: txt_division_name,
            division_description: txt_division_description
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


function ActionDelete() {
    txt_id = $("#txt_id").text();
    var result = $.xResponse(fullOrigin + '/ActionDelete', { id: txt_id });
    if (result == "0") {
        alert("Division is being use by the another table");
    } else {
        $(".dialogForm").dialog("close");
        alert('Data has been Deleted');
        window.location.reload(true);
    }

    //$.ajax({
    //    url: fullOrigin + '/ActionDelete',
    //    type: 'Post',
    //    data: {
    //        id: txt_id,
    //    },
    //    beforeSend:
    //        function () {
    //            $("#loadingRole").toggle()
    //        },
    //    error: function (response) {
    //        alert(response.responseText);
    //    },
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
        url: fullOrigin + '/List',
        success: function (data) {
            $("#renderBody").html(data);
        }
    });
}

function ActionActivating(txt_id) {
    $.ajax({
        url: fullOrigin + '/ActionActiviting',
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