@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Purchasing Request"
    ViewBag.Title = "List PR"
    ViewBag.PurchasingRequest = "active open"
End Code

@If ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListPR) Then
    ViewBag.IndexListPR = "active"
    ViewBag.MyListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRWA) Then
    ViewBag.IndexListApprPRWA = "active"
    ViewBag.MyListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRRD) Then
    ViewBag.IndexListApprPRRelDept = "active"
    ViewBag.MyListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyPRReadyToSignOff) Then
    ViewBag.IndexMyPRReadyToSignOff = "active"
    ViewBag.MyListPR = "active open"

ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.AllListPR) Then
    ViewBag.IndexAllListPR = "active"
    ViewBag.EprocListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToHandle) Then
    ViewBag.IndexAllPRReadyToHandle = "active"
    ViewBag.EprocListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToCreatePO) Then
    ViewBag.IndexAllPRReadyToCreatePO = "active"
    ViewBag.EprocListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToComplete) Then
    ViewBag.IndexAllPRReadyToComplete = "active"
    ViewBag.EprocListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToSignOff) Then
    ViewBag.IndexAllPRReadyToSignOff = "active"
    ViewBag.EprocListPR = "active open"

ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRBySubmitter) Then
    ViewBag.IndexListPRBySubmitter = "active"
    ViewBag.GroupListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRComplete) Then
    ViewBag.IndexListPRComplete = "active"
    ViewBag.GroupListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRSignOff) Then
    ViewBag.IndexListPRSignOff = "active"
    ViewBag.GroupListPR = "active open"
ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRReject) Then
    ViewBag.IndexListPRRejected = "active"
    ViewBag.GroupListPR = "active open"

End If


@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message
        <br />
    </div>
End If


@Html.Partial("_ListPR")
<script src="~/Scripts/Controllers/LIST_PRController.js"></script>
