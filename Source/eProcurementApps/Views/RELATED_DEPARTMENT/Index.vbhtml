@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Related Department"
        ViewBag.Setup = "active open"
        ViewBag.IndexRelatedDepartment = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request RD"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestRD = "active"
        ViewBag.Title = "Request RD"
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

@Html.Partial("_List")
<script src="~/Scripts/Controllers/RELATED_DEPARTMENTController.js"></script>