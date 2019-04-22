//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function Refresh() {
    //$.ajax({
    //    cache: false,
    //    url: '../USER/Index',
    //    success: function (data) {
    //        $("#renderBody").html(data);
    //    }
    //});
    window.location.reload(true);
}

function GetDataPRBySubmitter(urlLink) {
    if (IsValidForm() == 0) {
        txt_submitter = $("#txt_submitter").text();
        $.ajax({
            url: linkProc + '/PURCHASING_REQUEST/ListPRBySubmitter',
            type: 'Post',
            data: {
                user_id_id: txt_submitter,
            },
            cache: false,
            traditional: true,
            beforeSend:
                function () {
                    $("#loadingRole").toggle();
                },
            success: function (data) {
                $("#renderBody").html(data);
                $("#loadingRole").toggle();
            },
        });
    };
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

function ReminderCompleteUser() {
    var item_detail = new Array();
    item_detail = GetDataTableCheck();

    if (item_detail.length > 0) {
        result = $.xResponse(linkProc + '/PURCHASING_REQUEST/ActionReminderCompleteUser', {
            ids: item_detail,
        });

        if (result != undefined || result != null) {
            var arry_result = result.split('|');
            if (arry_result[0] == 'False') {
                msg_error.style.display = '';
                msg_error.textContent = arry_result[1];
                alert("fail");
            } else {
                msg_error.style.display = 'none';
                alert("Emails has been sent");
                Refresh();
            }
        } else {
            alert("error system");
        }
    } else {
        alert("Please select data that want to send email")
    }
}
