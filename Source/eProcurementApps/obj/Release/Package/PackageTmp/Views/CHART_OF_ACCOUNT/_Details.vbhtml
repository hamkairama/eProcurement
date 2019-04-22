@ModelType eProcurementApps.Models.TPROC_CHART_OF_ACCOUNTS
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Chart of Account
            </div>
        </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name"> crcy cd </div>
                    <div class="profile-info-value">
                        <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.CRCY_CD </span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> acct num </div>
                    <div class="profile-info-value">
                        <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> acct desc</div>
                    <div class="profile-info-value">
                        <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Converted acct num </div>
                    <div class="profile-info-value">
                        <span>@Model.TPROC_CHART_OF_ACCOUNT_GR.CONVERTED_ACCT_NUM</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Created Time </div>
                    <div class="profile-info-value">
                        <span>@Model.CREATED_TIME.ToString("dd-MM-yyy HH:mm")</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Created By </div>
                    <div class="profile-info-value">
                        <span> @Model.CREATED_BY </span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Last Modified Time </div>
                    <div class="profile-info-value">
                         @If Model.LAST_MODIFIED_TIME.HasValue Then  @<span>@Model.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")</span> End If
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Last Modified By </div>
                    <div class="profile-info-value">
                        <span>  @Model.LAST_MODIFIED_BY </span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Row Status </div>
                    <div class="profile-info-value">
                        <span> @Model.ROW_STATUS.ToString("0") </span>
                    </div>
                </div>
            </div>
        </div>
        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
        </div>
    </div>
</div>
<script src="~/Scripts/Standard/StandardModal.js"></script>


