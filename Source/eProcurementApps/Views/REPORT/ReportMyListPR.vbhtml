@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PR_DETAIL)
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Report"
    ViewBag.Title = "Report My List PR"
    ViewBag.Report = "active open"
    ViewBag.IndexReportMyListPR = "active"
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


@Html.Partial("_ListMyReport")
<script src="~/Scripts/Controllers/REPORTController.js"></script>

