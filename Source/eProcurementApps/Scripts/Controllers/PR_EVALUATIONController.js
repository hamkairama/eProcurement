//var linkProc;
//var fullOrigin = window.location.href;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();;
//};

function ActionSaveEval(supplier_idx) {
    var txt_pr_header_id = $("#txt_pr_header_id").text();
    var str = "_" + supplier_idx
    var eval_detailx = getElementEval(str);

    var result = $.xResponse(linkProc + '/PURCHASING_REQUEST/ActionSaveEvaluation/', {
        pr_header_id: txt_pr_header_id,
        supplier_id: supplier_idx,
        eval_detail: eval_detailx,
    });

    Refresh();
}

function getElementEval(str) {
    var x = document.getElementsByClassName(str)
    var a = "";
    for (var i = 0; i < x.length; i++) {
        var r = x[i].value;

        a = a + r
        a = a + "|"
    };

    var check;
    $("#rb_ya" + str).is(":checked") ? check = "1" : check = "0";

    a = a + check + "|";
    
    var komentar = $("#komentar" + str).text();
    a = a + komentar;

    return a;
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
//    }
//});

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


function RefreshTotScore(id) {
    //refresh totalscore
    var totScore = CalcScoreEval(id);
    $('#TOTAL_SCORE_' + id).val(Comma(totScore.toString()));
}

function CalcScoreEval(id) {
    var x = document.getElementById('dataTable-' + id);
    var result = 0;
    for (var i = 0; i < x.rows.length; i++) {
        if (x.rows[i].cells.length == 5) {
            if (x.rows[i].cells[4].childNodes.length > 0) {
                var r = x.rows[i].cells[4].childNodes[0].value;
                if (r != undefined) {
                    result = parseInt(result) + parseInt(r);
                }
            }            
        }       
    }
    return result;
}