//var fullOrigin = window.location.href;
//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function GetDataDOStock() {
    var url = linkProc + '/DAILY_OPERATION/DailyOperationStockByFTandDt';
    ActionGetDataDO(url);
}

function GetDataDOStockSummary() {
    var url = linkProc + '/DAILY_OPERATION/DailyOperationStockSummaryByFTandDt';
    ActionGetDataDO(url);
}

function GetDataDONonStock() {
    var url = linkProc + '/DAILY_OPERATION/DailyOperationNonStockByFTandDt';
    ActionGetDataDO(url);
}


function ActionGetDataDO(urlLink) {
    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            txt_form_type_id = $("#txt_form_type_id").text();
            txt_date = ConvertDate($("#txt_date").text());
            $.ajax({
                url: urlLink,
                type: 'Post',
                data: {
                    ft: txt_form_type_id,
                    dt: txt_date
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


