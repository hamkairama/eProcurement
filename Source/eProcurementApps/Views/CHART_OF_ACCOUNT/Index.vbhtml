@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Chart of Account"
        ViewBag.Setup = "active open"
        ViewBag.IndexCOA = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request COA"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestCOA = "active"
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
<script src="~/Scripts/Controllers/CHART_OF_ACCOUNTController.js"></script>
