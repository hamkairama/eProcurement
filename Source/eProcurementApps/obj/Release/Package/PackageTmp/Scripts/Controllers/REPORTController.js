//var fullOrigin = window.location.href;
//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function GetTat(flag) {
    var url = linkProc + '/REPORT/ReportTATByDate';
    ActionGetDataDO(flag, url);
}

function ActionGetDataDO(flags, urlLink) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            txt_date_from = ConvertDate($("#txt_date_from").text());
            txt_date_to = ConvertDate($("#txt_date_to").text());
            $.ajax({
                url: urlLink,
                type: 'Post',
                data: {
                    flag: flags,
                    date_from: txt_date_from,
                    date_to: txt_date_to
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
        }
    }
}


function GetReportPrList(base_on) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            var txt_date_from = ConvertDate($("#txt_date_from").text());
            var txt_date_to = ConvertDate($("#txt_date_to").text());

            var link;
            if (base_on == 0) {
                link = '/REPORT/MyReportListPRByDate';
            } else {
                link = '/REPORT/AllReportListPRByDateByAdmin';
            }

            $.ajax({
                url: linkProc + link,
                type: 'Post',
                data: {
                    date_from: txt_date_from,
                    date_to: txt_date_to
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
        }
    }
}


