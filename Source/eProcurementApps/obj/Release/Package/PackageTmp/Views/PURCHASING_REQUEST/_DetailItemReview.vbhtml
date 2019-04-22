@ModelType eProcurementApps.Models.TPROC_PR_DETAIL
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Item PR No : @Model.TPROC_PR_HEADER.PR_NO
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="modal-body no-padding">
                <table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Item Name </div>
                                    <div class="profile-info-value">
                                        <span id="txt_item_id" class="hidden">@Model.ID</span>
                                        <span>@Model.ITEM_NAME</span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> WA </div>
                                    <div class="profile-info-value">
                                        <span> @Model.TPROC_WA.WA_NUMBER </span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Specification </div>
                                    <div class="profile-info-value">
                                        <span>@Model.SPECIFICATION</span>
                                    </div>
                                </div>

                                <div class="space-4"></div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> qty </div>
                                    <div class="profile-info-value">
                                        <span>@Model.QTY.ToString("###,###") </span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Revise Qty </div>
                                    <div class="profile-info-value">
                                        <span>  @Model.REVISED_QTY </span>
                                    </div>
                                </div>

                                <div class="space-4"></div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Remark </div>
                                    <div class="profile-info-value">
                                        <span>  @Model.REMARK </span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> PO No </div>
                                    <div class="profile-info-value">
                                        <span>  @Model.PO_NUMBER </span>
                                    </div>
                                </div>

                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name">Status</div>
                                    <div class="profile-info-value">
                                        @code
                                            Dim status As String = [Enum].GetName(GetType(ListEnum.ItemStatus), Int32.Parse(Model.PR_DETAIL_STATUS)).ToString()
                                            @<span>@status</span>
                                        End Code
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Measurement </div>
                                    <div class="profile-info-value">
                                        <span> @Model.USER_MEASUREMENT </span>
                                    </div>
                                </div>


                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Currency </div>
                                    <div class="profile-info-value">
                                        <span>@Model.CURRENCY</span>
                                    </div>
                                </div>



                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Price </div>
                                    <div class="profile-info-value">
                                        <span> @Model.PRICE.ToString("###,###") </span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Total Price </div>
                                    <div class="profile-info-value">
                                        <span> @Model.TOTAL_PRICE.ToString("###,###") </span>
                                    </div>
                                </div>

                                <div class="space-4"></div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Supp Name </div>
                                    <div class="profile-info-value">
                                        <span> @Model.SUPPLIER_NAME </span>
                                    </div>
                                </div>

                            </div>
                        </td>
                    </tr>
                </table>

                <div class="space-4"></div>

                <div Class="clearfix">
                    <div Class="pull-right tableTools-container"></div>
                </div>

                &nbsp;&nbsp;<b>Approval Work Area</b>
                <div class="form-group">
                    <table class="table table-striped table-bordered table-hover">
                        @For Each item_gr In Model.TPROC_PR_APPR_WA
                            @<tr>
                                <td>
                                    <span>@item_gr.NAME &nbsp; &nbsp;</span>
                                    @If item_gr.APPR_WA_STATUS.Equals("Waiting for review") Or item_gr.APPR_WA_STATUS.Equals("Waiting for approve") Then
                                        @<span style="color:green">@item_gr.APPR_WA_STATUS</span>
                                    ElseIf item_gr.APPR_WA_STATUS.Equals("Reviewed") Or item_gr.APPR_WA_STATUS.Equals("Approved") Then
                                        @<span style="color:blue">@item_gr.APPR_WA_STATUS</span>
                                    Else
                                        @<span style="color:red">@item_gr.APPR_WA_STATUS</span>
                                    End If
                                </td>
                            </tr>
                        Next
                    </table>
                </div>



            </div>
            <div class='modal-footer no-margin-top'>
                @Html.Raw(Labels.ButtonForm("Close"))

                @If ViewBag.StatusApprWA <> "Reviewed" Then 'And ViewBag.IsReviewedByOther = False Then
                    @Html.Raw(Labels.ButtonForm("SaveItemPRReview"))
                    @Html.Raw(Labels.ButtonForm("SaveItemPRRejectByReviewer"))
                End If
                <hr />
                <div class="row top-left" id="input_reason_1" style="display:none">
                    <div class="col-lg-12">
                        Reason of reject :
                        <textarea type="text" style="height:100px" placeholder="input the reason of reject" class="form-control input-group" id="txt_reason_reject_1">
                        </textarea>
                    </div>
                </div>
            </div>
        </div>

    </div>
</div>
<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>


