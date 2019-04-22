@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Good Type"
    ViewBag.Setup = "active open"
    ViewBag.IndexGoodType = "active"
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
<script src="~/Scripts/Controllers/GOOD_TYPEController.js"></script>