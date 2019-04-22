@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Form SubType"
        ViewBag.Setup = "active open"
        ViewBag.IndexFormSubType = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request FST"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestFST = "active"
    End If
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

@Html.Partial("_List")
<script src="~/Scripts/Controllers/FORM_SUB_TYPEController.js"></script>