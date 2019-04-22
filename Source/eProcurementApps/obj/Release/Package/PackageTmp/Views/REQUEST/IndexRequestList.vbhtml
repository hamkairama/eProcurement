@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Request"
    ViewBag.Request = "active open"
    ViewBag.ListRequest = "active open"

    If ViewBag.IndexList = Convert.ToDecimal(ListEnum.Request.Submitted) Then
        ViewBag.IndexListOutstanding = "active"
        ViewBag.Title = "List Request Outstanding"
    ElseIf ViewBag.IndexList = Convert.ToDecimal(ListEnum.Request.NeedApprove) Then
        ViewBag.IndexListNeedApprove = "active"
        ViewBag.Title = "List Request Need Approve"
    ElseIf ViewBag.IndexList = Convert.ToDecimal(ListEnum.Request.NeedComplete) Then
        ViewBag.IndexListNeedComplete = "active"
        ViewBag.Title = "List Request Need Completed"
    ElseIf ViewBag.IndexList = Convert.ToDecimal(ListEnum.Request.Completed) Then
        ViewBag.IndexListAlreadyCompleted = "active"
        ViewBag.Title = "List Request Already Completed"
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
<script src="~/Scripts/Controllers/REQUESTController.js"></script>