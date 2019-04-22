@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Work Area"
        ViewBag.Setup = "active open"
        ViewBag.IndexWA = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request WA"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestWA = "active"
        ViewBag.Title = "Request WA"
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
<script src="~/Scripts/Controllers/WAController.js"></script>
