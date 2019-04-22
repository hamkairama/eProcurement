//var fullOrigin = window.location.href;
//var linkProc;

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();
//};

function IndexJobListsjs(flagx) {
    $.ajax({
        cache: false,
        url: linkProc + '/DASHBOARD/ListApprInbox',
        data: {
            flag: flagx,
            user_id: $("#txt_user_id").text(),
            user_mail: $("#txt_user_mail").text()
        },
        success: function (data) {
            $("#renderInbox").html(data);
        }
    });
}

//function ListApprPRWA(flagx) {
//    $.ajax({
//        cache: false,
//        url: linkProc + '/DASHBOARD/ListApprInbox',
//        data: {
//            flag: flagx,
//            user_id: $("#txt_user_id").text(),
//            user_mail: $("#txt_user_mail").text()
//        },
//        success: function (data) {
//            $("#renderInbox").html(data);
//        }
//    });
//}

//function ListApprPRRD(flagx) {
//    $.ajax({
//        cache: false,
//        data: {
//            flag: flagx,
//            user_id: $("#txt_user_id").text(),
//            user_mail: $("#txt_user_mail").text()
//        },
//        url: linkProc + '/DASHBOARD/ListApprInbox',
//        success: function (data) {
//            $("#renderInbox").html(data);
//        }
//    });
//}

//function ListApprReq(flagx) {
//    $.ajax({
//        cache: false,
//        data: {
//            flag: flagx,
//            user_id: $("#txt_user_id").text(),
//            user_mail: $("#txt_user_mail").text()
//        },
//        url: linkProc + '/DASHBOARD/ListApprInbox',
//        success: function (data) {
//            $("#renderInbox").html(data);
//        }
//    });
//}

//function ListCompReq(flagx) {
//    $.ajax({
//        cache: false,
//        data: {
//            flag: flagx,
//            user_id: $("#txt_user_id").text(),
//            user_mail: $("#txt_user_mail").text()
//        },
//        url: linkProc + '/DASHBOARD/ListApprInbox',
//        success: function (data) {
//            $("#renderInbox").html(data);
//        }
//    });
//}

//function ListApprAckPC(flagx) {
//    $.ajax({
//        cache: false,
//        data: {
//            flag: flagx,
//            user_id: $("#txt_user_id").text(),
//            user_mail: $("#txt_user_mail").text()
//        },
//        url: linkProc + '/DASHBOARD/ListApprInbox',
//        success: function (data) {
//            $("#renderInbox").html(data);
//        }
//    });
//}

//function ListApprPC(flagx) {
//    $.ajax({
//        cache: false,
//        data: {
//            flag: flagx,
//            user_id: $("#txt_user_id").text(),
//            user_mail: $("#txt_user_mail").text()
//        },
//        url: linkProc + '/DASHBOARD/ListApprInbox',
//        success: function (data) {
//            $("#renderInbox").html(data);
//        }
//    });
//}

//window.onload = function () {
//    linkProc = $("#txt_linkProc").text();
//    ListApprPRWA(100);
//};

function ActionDblClick(idx, flagx) {
    $.ajax({
        cache: false,
        data: {
            id: idx,
            flag: flagx,
        },
        url: linkProc + '/PURCHASING_REQUEST/DetailHeader',
        success: function () {
            window.location.reload(true);
        }
    });
}