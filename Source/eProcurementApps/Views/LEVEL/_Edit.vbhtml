@ModelType eProcurementApps.Models.TPROC_LEVEL
@Imports eProcurementApps.Helpers

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Edit Level
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row hidden" >
                    <div class="profile-info-name" id="dataID"> ID </div>
                    <div class="profile-info-value">
                        <span id="txt_id">@Model.ID.ToString("0")</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Indonesian Level </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText change" id="txt_indonesian_level" maxlenght="50">@Model.INDONESIAN_LEVEL</span>
                        <label id="required_txt_indonesian_level"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name required"> Rupiah Limit </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText onlyNumber" id="txt_rupiah_limit">@Model.RUPIAH_LIMIT</span>
                        <label id="required_txt_rupiah_limit"></label>
                        <label id="only_number_txt_rupiah_limit"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name required"> Canadian Dollars </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText onlyNumber" id="txt_canadian_dollars">@Model.CANADIAN_DOLLARS</span>
                        <label id="required_txt_canadian_dollars"></label>
                        <label id="only_number_txt_canadian_dollars"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Created Time </div>
                    <div class="profile-info-value">
                        <span>@Model.CREATED_TIME.ToString("dd-MM-yyy HH:mm") </span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Created By </div>
                    <div class="profile-info-value">
                        <span>@Model.CREATED_BY </span>
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
                        <span>@Model.LAST_MODIFIED_BY </span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Row Status </div>
                    <div class="profile-info-value">
                        <span>@Model.ROW_STATUS.ToString("0") </span>
                    </div>
                </div>
            </div>
        </div>
        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
            @Html.Raw(Labels.ButtonForm("SaveEdit"))
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>