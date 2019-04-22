@Code
    ViewBag.Breadcrumbs = "DailyOperation"
    ViewBag.Title = "Daily Operation Data Stock"
    ViewBag.DailyOperation = "active open"
    ViewBag.IndexDailyOperationStockSummary = "active"
End Code


@If ViewBag.Message <> "" Then
    @<div Class="alert alert-block alert-error">
        <Button Class="close" data-dismiss="alert" type="button">
            <i Class="icon-remove"></i>
        </Button>
        <i Class="icon-warning-sign red"></i>
        @ViewBag.Message
    </div>
End If


@Html.Partial("_ListDOStockSummary")
<script src="~/Scripts/Controllers/DAILY_OPERATIONController.js"></script>


