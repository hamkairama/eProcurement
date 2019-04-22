@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Division"
    ViewBag.Setup = "active open"
    ViewBag.IndexDivision = "active"
End Code

@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-error">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>
End If


@Html.Partial("_List")
<script src="~/Scripts/Controllers/DIVISIONController.js"></script>