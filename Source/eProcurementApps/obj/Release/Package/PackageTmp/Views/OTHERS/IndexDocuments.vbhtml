@Code
    ViewBag.Breadcrumbs = "Others"
    ViewBag.Title = "Documents"
    ViewBag.Others = "active open"
    ViewBag.IndexDocuments = "active"
End Code

@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>
End If

@Html.Partial("_ListDocuments")
<script src="~/Scripts/Controllers/OTHERSController.js"></script>
