@imports eProcurementApps.Helpers

<div class="page-header">
    <div class="row">
        <div class="col-xs-4">
            <div>
                <h1>@ViewBag.Title</h1>
            </div>
        </div>

        <div class="col-xs-8" style="text-align:right">
            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.MyListPR) And ViewBag.FlagAction IsNot Nothing Then
                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListPR), .SubTitle = ""})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                 If ViewBag.StatusPR = ListEnum.PRStatus.Complete Then
                    @<a Class="green" href="@Url.Action("SignOff", "PURCHASING_REQUEST", New With {.id = Convert.ToDecimal(ViewBag.PRID), .flag = Convert.ToInt32(ViewBag.FlagAction)})" title="Sign Off">
                        @Html.Raw(Labels.ButtonForm("SignOff"))
                    </a>
                End If
                @<a Class="red" target="_blank" href="@Url.Action("PrintDetailHeader", "PURCHASING_REQUEST", New With {.id = Convert.ToDecimal(ViewBag.PRID)})" title="Print">
                    @Html.Raw(Labels.ButtonForm("PrintDetailHeader"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRWA) Then
                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRWA), .SubTitle = "Approval/Review WA of each item"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRRD) Then
                @If ViewBag.StatusRDApprRev = "Waiting for review" Then
                    @<a Class="green" href="#" title="ReviewByReviewer">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRReview"))
                    </a>
                    @<a Class="green" href="#" title="RejectByReviewer">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRRejectByReviewer"))
                    </a>
                ElseIf ViewBag.StatusRDApprRev = "Waiting for approve" And (ViewBag.StatusRDGR = Convert.ToString(ListEnum.RDStatus.ReadyToApprove) Or ViewBag.StillWaitingBy = "") Then
                    @<a Class="green" href="#" title="ApproveByApprover">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRApprove"))
                    </a>

                    @<a Class="green" href="#" title="RejectByApprover">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRRejectByApprover"))
                    </a>
                ElseIf ViewBag.StatusRDApprRev = "Waiting for approve" And ViewBag.StatusRDGR Is Nothing Then
                    @<a Class="red col-xs-8" style="text-align:right" href="#">
                        <span> Still waiting to review by @ViewBag.StillWaitingBy </span>
                    </a>
                End If

                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRRD), .SubTitle = "Approval Rel. Dept of PR"})">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>

            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToHandle) Then
                If ViewBag.StatusPR = ListEnum.PRStatus.PrApprovedComplete And ViewBag.StatusPR <> ListEnum.PRStatus.PrHandled Then
                    @<a class="yellow" href="@Url.Action("Revise", "PURCHASING_REQUEST", New With {.pr_header_id = Convert.ToDecimal(ViewBag.PRID)})" title="Revise">
                        @Html.Raw(Labels.ButtonForm("Revise"))
                    </a>
                    @<a Class="green" href="#" title="Handle">
                        @Html.Raw(Labels.ButtonForm("SaveHandlePR"))
                    </a>
                End If

                @<a class="yellow" href="@Url.Action("EditPRRemarkOrSupplier", "PURCHASING_REQUEST", New With {.pr_header_id = Convert.ToDecimal(ViewBag.PRID)})" title="Edit">
                    @Html.Raw(Labels.ButtonForm("EditPRRemarkOrSupplier"))
                </a>

                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToHandle), .SubTitle = "PR Ready To Handle"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToCreatePO) Then
                If ViewBag.StatusPR = ListEnum.PRStatus.PrHandled Then
                    @<a Class="green" href="#" title="Create PO">
                        @Html.Raw(Labels.ButtonForm("SaveReadyToCreatePO"))
                    </a>
                End If

                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToCreatePO), .SubTitle = "PR Ready To Create PO"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToComplete) Then
                If ViewBag.StatusPR = ListEnum.PRStatus.GoodReceive Or ViewBag.StatusPR = ListEnum.PRStatus.PrHandled Or ViewBag.StatusPR = ListEnum.PRStatus.CreatePo Then
                    @<a Class="green" href="#" title="Complete">
                        @Html.Raw(Labels.ButtonForm("SaveReadyToComplete"))
                    </a>
                End If

                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToComplete), .SubTitle = "PR Ready To Complete"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToSignOff) Then
                If ViewBag.StatusPR = ListEnum.PRStatus.Complete Then
                    @<a Class="green" href="@Url.Action("SignOff", "PURCHASING_REQUEST", New With {.id = Convert.ToDecimal(ViewBag.PRID), .flag = Convert.ToInt32(ViewBag.FlagAction)})" title="Sign Off">
                        @Html.Raw(Labels.ButtonForm("SignOff"))
                    </a>
                End If

                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToSignOff), .SubTitle = "PR Ready To Sign Off"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.PRRevise) Then
                @<a Class="progress-yellow" href="#" title="Save Revise">
                    @Html.Raw(Labels.ButtonForm("SaveRevise"))
                </a>

                @<a Class="red" href="@Url.Action("BackToHeaderFromRevise", "PURCHASING_REQUEST", New With {.pr_header_id = Convert.ToDecimal(ViewBag.PRIDFromRevise)})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.PREdit) Then
                @<a Class="progress-yellow" href="#" title="Save Edit">
                    @Html.Raw(Labels.ButtonForm("SaveEditPRRemarkOrSupplier"))
                </a>

                @<a Class="red" href="@Url.Action("BackToHeaderFromEditRemarkOrSupplier", "PURCHASING_REQUEST", New With {.pr_header_id = Convert.ToDecimal(ViewBag.PRIDFromEdit)})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagDetail.MyPRReadyToSignOff) Then
                If ViewBag.StatusPR = ListEnum.PRStatus.Complete Then
                    @<a Class="green" href="@Url.Action("SignOff", "PURCHASING_REQUEST", New With {.id = Convert.ToDecimal(ViewBag.PRID), .flag = Convert.ToInt32(ViewBag.FlagAction)})" title="Sign Off">
                        @Html.Raw(Labels.ButtonForm("SignOff"))
                    </a>
                End If

                @<a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyPRReadyToSignOff), .SubTitle = "My PR Ready To Sign Off"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagInbox.ApprWA) Then
                @<a Class="red" href="@Url.Action("IndexJobLists", "DASHBOARD")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

            @If ViewBag.FlagAction = Convert.ToInt32(ListEnum.FlagInbox.ApprRD) Then
                @If ViewBag.StatusRDApprRev = "Waiting for review" And ViewBag.StatusRDGR <> Convert.ToString(ListEnum.RDStatus.ReadyToApprove) Then
                    @<a Class="green" href="#" title="ReviewByReviewer">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRReview"))
                    </a>
                    @<a Class="green" href="#" title="RejectByReviewer">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRRejectByReviewer"))
                    </a>
                ElseIf ViewBag.StatusRDApprRev = "Waiting for approve" And (ViewBag.StatusRDGR = Convert.ToString(ListEnum.RDStatus.ReadyToApprove) Or ViewBag.StillWaitingBy = "") And ViewBag.StatusRDGR <> Convert.ToString(ListEnum.RDStatus.Complete) Then
                    @<a Class="green" href="#" title="ApproveByApprover">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRApprove"))
                    </a>

                    @<a Class="green" href="#" title="RejectByApprover">
                        @Html.Raw(Labels.ButtonForm("SaveRDPRRejectByApprover"))
                    </a>
                ElseIf ViewBag.StatusRDApprRev = "Waiting for approve" And ViewBag.StatusRDGR Is Nothing Then
                    @<a Class="red col-xs-8" style="text-align:right" href="#">
                        <span> Still waiting to review by @ViewBag.StillWaitingBy </span>
                    </a>
                End If

                @<a Class="red" href="@Url.Action("IndexJobLists", "DASHBOARD")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            End If

        </div>
    </div>
</div>
