﻿//var linkProc = "http://localhost:16543"
//var fullOrigin = window.location.href;
//var linkProc = location.origin;

$(document).ready(function () {
    $(".dialogForm").dialog({
        autoOpen: false,
        width: 'auto',
        modal: true,
        //show: "blind",
        //hide: "blind",
    });
    $(".ui-dialog-titlebar").hide();
    $(document).ready(function () {
        $(".btn-danger").click(function () {
            $(".dialogForm").dialog("close");
        });
        $(".close").click(function () {
            $(".dialogForm").dialog("close");
        });
    });
});


function Modal(url, id, form) {    
    $.get(UrlWithoutParam(fullOrigin) + url + id, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}

function UrlWithoutParam(route) {
    var fields = route.split('?');
    return fields[0];
}

function ModalRouterWithParam(url, id, form) {
    $.get(UrlWithoutParam(fullOrigin) + url + id, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}

function ModalCreate(url, form) {
    $.get(fullOrigin + url, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}

function ModalPop(url, id, form) {
    $.get(url + id, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}

function ModalPop2(url, form) {
    $.get(url, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}


function ModalDetailItem(url, id, form) {
    $.get(linkProc + url + id, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}


function ModalCommon(url, form) {
    $.get(linkProc + url, { "_": $.now() }, function (data) {
        //cache: false;
        $(form).html(data).dialog("open");
        $(form).html(data).dialog({ draggable: false }).parent().draggable();
    });
}

function ModalSearch(url, jsondata, headerdata, innerhtmldata, form) {
    $.ajax({
        url: linkProc + url,
        type: "Post",
        data: {
            _JsonDataTable: jsondata,
            _HeaderForm: headerdata,
            _InnerHtmlData: innerhtmldata,
        },
        cache: false,
        traditional: true,  
        success: function (data) {
            $(form).html(data).dialog("open");
            $(form).html(data).dialog({ draggable: false }).parent().draggable();
        }
    }) 
}

function ActiveDir() {
    $.ajax({
        url: '../ACTIVE_DIRECTORY/_Index',
        type: "Post",
        cache: false,
        traditional: true,
        error: function (response) {
            alert(response.responseText);
        },
        success: function (data) {
            var field = document.getElementById('required_txt_emp_id');
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

