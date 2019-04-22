//var fullOrigin = window.location.href;
//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function BtnAction(action) {
    if (action == "delete") {
        ActionDelete();
        return false;
    }
}

function ActionDelete() {
    txt_id = $("#txt_id").text();
    $.ajax({
        url: linkProc + '/OTHERS/ActionDelete',
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
    //$.ajax({
    //    cache: false,
    //    url: fullOrigin + '/ListDocuments',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    window.location.reload(true);
}
