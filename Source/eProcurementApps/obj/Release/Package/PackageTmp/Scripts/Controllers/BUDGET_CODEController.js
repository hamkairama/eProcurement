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

    if (IsValidForm() == 0) {
        if (IsValidDate() == 0) {
            if (IsValidLength() == 0) {
                ReadyToExec();
                if (action == "create") {
                    ActionCreate();
                }
                else {
                    ActionEdit();
                }
            }
        }
    }
}

function ActionCreate() {
    txt_table_budget = $("#txt_table_budget").text();
    txt_table_budget_usage = $("#txt_table_budget_usage").text();
    txt_cd_printing_usage = $("#txt_cd_printing_usage").text();
    txt_cd_printing_start = $("#txt_cd_printing_start").text();
    txt_cd_printing_end = $("#txt_cd_printing_end").text();
    txt_cd_office_supplie_usage = $("#txt_cd_office_supplie_usage").text();
    txt_cd_office_supplie_start = $("#txt_cd_office_supplie_start").text();
    txt_cd_office_supplie_end = $("#txt_cd_office_supplie_end").text();
    txt_cd_asset_nonasset_usage = $("#txt_cd_asset_nonasset_usage").text();
    txt_cd_asset_nonasset_start = $("#txt_cd_asset_nonasset_start").text();
    txt_cd_asset_nonasset_end = $("#txt_cd_asset_nonasset_end").text();
    txt_cd_promotional_item_usage = $("#txt_cd_promotional_item_usage").text();
    txt_cd_promotional_item_start = $("#txt_cd_promotional_item_start").text();
    txt_cd_promotional_item_end = $("#txt_cd_promotional_item_end").text();

    txt_cd_office_supplie_mt = $("#txt_cd_office_supplie_mt").text();
    txt_cd_printing_mt = $("#txt_cd_printing_mt").text();
    txt_table_t1 = $("#txt_table_t1").text();
    txt_table_t2 = $("#txt_table_t2").text();
    txt_table_t3 = $("#txt_table_t3").text();
    txt_table_t5 = $("#txt_table_t5").text();
    txt_cd_issued_usage = $("#txt_cd_issued_usage").text();

    var txt_is_active;
    $("#txt_is_active").is(":checked") ? txt_is_active = 1 : txt_is_active = 0;

    $.ajax({
        url: '../BUDGET_CODE/ActionCreate',
        type: 'Post',
        data: {
            table_budget: txt_table_budget,
            table_budget_usage: txt_table_budget_usage,
            cd_printing_usage: txt_cd_printing_usage,
            cd_printing_start: txt_cd_printing_start,
            cd_printing_end: txt_cd_printing_end,
            cd_office_supplie_usage: txt_cd_office_supplie_usage,
            cd_office_supplie_start: txt_cd_office_supplie_start,
            cd_office_supplie_end: txt_cd_office_supplie_end,
            cd_asset_nonasset_usage: txt_cd_asset_nonasset_usage,
            cd_asset_nonasset_start: txt_cd_asset_nonasset_start,
            cd_asset_nonasset_end: txt_cd_asset_nonasset_end,
            cd_promotional_item_usage: txt_cd_promotional_item_usage,
            cd_promotional_item_start: txt_cd_promotional_item_start,
            cd_promotional_item_end: txt_cd_promotional_item_end,
            is_active: txt_is_active,

            cd_office_supplie_mt: txt_cd_office_supplie_mt,
            cd_printing_mt: txt_cd_printing_mt,
            table_t1: txt_table_t1,
            table_t2: txt_table_t2,
            table_t3: txt_table_t3,
            table_t5: txt_table_t5,
            cd_issued_usage: txt_cd_issued_usage,
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
    txt_table_budget = $("#txt_table_budget").text();
    txt_table_budget_usage = $("#txt_table_budget_usage").text();
    txt_cd_printing_usage = $("#txt_cd_printing_usage").text();
    txt_cd_printing_start = $("#txt_cd_printing_start").text();
    txt_cd_printing_end = $("#txt_cd_printing_end").text();
    txt_cd_office_supplie_usage = $("#txt_cd_office_supplie_usage").text();
    txt_cd_office_supplie_start = $("#txt_cd_office_supplie_start").text();
    txt_cd_office_supplie_end = $("#txt_cd_office_supplie_end").text();
    txt_cd_asset_nonasset_usage = $("#txt_cd_asset_nonasset_usage").text();
    txt_cd_asset_nonasset_start = $("#txt_cd_asset_nonasset_start").text();
    txt_cd_asset_nonasset_end = $("#txt_cd_asset_nonasset_end").text();
    txt_cd_promotional_item_usage = $("#txt_cd_promotional_item_usage").text();
    txt_cd_promotional_item_start = $("#txt_cd_promotional_item_start").text();
    txt_cd_promotional_item_end = $("#txt_cd_promotional_item_end").text();

    txt_cd_office_supplie_mt = $("#txt_cd_office_supplie_mt").text();
    txt_cd_printing_mt = $("#txt_cd_printing_mt").text();
    txt_table_t1 = $("#txt_table_t1").text();
    txt_table_t2 = $("#txt_table_t2").text();
    txt_table_t3 = $("#txt_table_t3").text();
    txt_table_t5 = $("#txt_table_t5").text();
    txt_cd_issued_usage = $("#txt_cd_issued_usage").text();


    var txt_is_active;
    $("#txt_is_active").is(":checked") ? txt_is_active = 1 : txt_is_active = 0;

    $.ajax({
        url: linkProc + '/BUDGET_CODE/ActionEdit',
        type: 'Post',
        data: {
            id: txt_id,
            table_budget: txt_table_budget,
            table_budget_usage: txt_table_budget_usage,
            cd_printing_usage: txt_cd_printing_usage,
            cd_printing_start: txt_cd_printing_start,
            cd_printing_end: txt_cd_printing_end,
            cd_office_supplie_usage: txt_cd_office_supplie_usage,
            cd_office_supplie_start: txt_cd_office_supplie_start,
            cd_office_supplie_end: txt_cd_office_supplie_end,
            cd_asset_nonasset_usage: txt_cd_asset_nonasset_usage,
            cd_asset_nonasset_start: txt_cd_asset_nonasset_start,
            cd_asset_nonasset_end: txt_cd_asset_nonasset_end,
            cd_promotional_item_usage: txt_cd_promotional_item_usage,
            cd_promotional_item_start: txt_cd_promotional_item_start,
            cd_promotional_item_end: txt_cd_promotional_item_end,
            is_active: txt_is_active,

            cd_office_supplie_mt : txt_cd_office_supplie_mt,
            cd_printing_mt : txt_cd_printing_mt,
            table_t1 :txt_table_t1,
            table_t2 :txt_table_t2,
            table_t3 :txt_table_t3,
            table_t5 :txt_table_t5,
            cd_issued_usage: txt_cd_issued_usage,

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

