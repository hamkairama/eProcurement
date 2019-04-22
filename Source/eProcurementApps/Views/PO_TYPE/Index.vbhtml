@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "PO Type"
    ViewBag.Setup = "active open"
    ViewBag.IndexPoType = "active"
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
<script src="~/Scripts/Controllers/PO_TYPEController.js"></script>