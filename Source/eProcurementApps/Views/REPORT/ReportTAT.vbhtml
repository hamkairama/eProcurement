@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PR_HEADER)
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Report"
    ViewBag.Report = "active open"
    ViewBag.IndexReportTAT = "active open"

    If ViewBag.IndexList = ListEnum.FlagReport.TatComplete Then
        ViewBag.Title = "Report TAT Complete"
        ViewBag.IndexReportTATComplete = "active"
    ElseIf ViewBag.IndexList = ListEnum.FlagReport.TatSignOff Then
        ViewBag.Title = "Report TAT Sign Off"
        ViewBag.IndexReportTATSignOff = "active"
    ElseIf ViewBag.IndexList = ListEnum.FlagReport.TatUnComplete Then
        ViewBag.Title = "Report TAT UnComplete"
        ViewBag.IndexReportTATUnComplete = "active"
    End If
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


@Html.Partial("_ListReportTAT")
<script src="~/Scripts/Controllers/REPORTController.js"></script>


