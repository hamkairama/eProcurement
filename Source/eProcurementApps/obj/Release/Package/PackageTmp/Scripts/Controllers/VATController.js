//var fullOrigin = window.location.href;
function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }
   
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                var id = $.xResponse(fullOrigin + '/IsInActive/', { value: $("#txt_vat_name").text() });
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
    txt_vat_name = $("#txt_vat_name").text();
    txt_id = 0;
    if (action == "edit") {
        txt_id = $("#txt_id").text();
    };
    $.ajax({
        url: fullOrigin + '/CheckData',
        type: "Post",
        data: {
            id: txt_id,
            vat_name: txt_vat_name,
        },
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_vat_name');
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
    txt_vat_name = $("#txt_vat_name").text();
    txt_start_date = ConvertDate($("#txt_start_date").text());
    $.ajax({
        url: fullOrigin + '/ActionCreate',
        type: 'Post',
        data: {
            vat_name: txt_vat_name,
            start_date: txt_start_date
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
    txt_vat_name = $("#txt_vat_name").text();
    txt_start_date = ConvertDate($("#txt_start_date").text());
    $.ajax({
        url: fullOrigin + '/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            vat_name: txt_vat_name,
            start_date: txt_start_date
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


function ActionDelete() {
    txt_id = $("#txt_id").text();
    $.ajax({
        url: fullOrigin + '/ActionDelete',
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