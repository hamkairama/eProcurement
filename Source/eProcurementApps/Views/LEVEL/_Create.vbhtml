@Imports eProcurementApps.Helpers

<div Class="modal-dialog">
    <div Class="modal-content">
        <div Class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Create Level
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Indonesian Level </div>
                    <div class="profile-info-value item-required">
                        <span Class="freeText" id="txt_indonesian_level" maxlenght="50"></span>
                        <label id="required_txt_indonesian_level"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Rupiah Limit </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText onlyNumber" id="txt_rupiah_limit"></span>
                        <label id="required_txt_rupiah_limit"></label>
                        <label id="only_number_txt_rupiah_limit"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Canadian Dollars </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText onlyNumber" id="txt_canadian_dollars"></span>
                        <label id="required_txt_canadian_dollars"></label>
                        <label id="only_number_txt_canadian_dollars"></label>
                    </div>
                </div>

            </div>
        </div>
        <div Class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
            @Html.Raw(Labels.ButtonForm("SaveCreate"))
        </div>
    </div>
</div>

<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>