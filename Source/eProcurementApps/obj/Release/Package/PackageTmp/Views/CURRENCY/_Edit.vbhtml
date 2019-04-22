@ModelType eProcurementApps.Models.TPROC_CURRENCY
@Imports eProcurementApps.Helpers

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Edit Currency
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row hidden">
                    <div class="profile-info-name" id="dataID"> ID </div>
                    <div class="profile-info-value">
                        <span id="txt_id">@Model.ID.ToString("0")</span>
                    </div>
                </div>
                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Currency </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText change" id="txt_currency_name" maxlenght="10">@Model.CURRENCY_NAME</span>
                        <label id="required_txt_currency_name"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name required"> Rate </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText" id="txt_rate">@Model.RATE.ToString("0")</span>
                        <label id="required_txt_rate"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name required"> Start Date </div>
                    <div class="profile-info-value item-required">
                        <span class="dateText" id="txt_start_date">@Model.START_DATE.ToString("dd-MM-yyyy")</span>
                        <label id="required_txt_start_date"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Convertion(RP) </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText onlyNumber" id="txt_convertion">@Model.CONVERSION_RP</span>
                        <label id="required_txt_convertion"></label>
                        <label id="only_number_txt_convertion"></label>
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
                        <span>@Model.CREATED_BY</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Last Modified Time </div>
                    <div class="profile-info-value">
                        @If Model.LAST_MODIFIED_TIME.HasValue Then  @<span>@Model.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")</span> End If
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name"> Last Modified By </div>
                    <div Class="profile-info-value">
                        <span>@Model.LAST_MODIFIED_BY </span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name"> Row Status </div>
                    <div Class="profile-info-value">
                        <span>@Model.ROW_STATUS.ToString("0")</span>
                    </div>
                </div>
            </div>
        </div>
        <div Class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
            @Html.Raw(Labels.ButtonForm("SaveEdit"))
        </div>
    </div>
</div>

<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<Script src="~/Scripts/Standard/StandardProfile.js"></Script>
