@Imports eProcurementApps.Helpers

<div Class="modal-dialog">
    <div Class="modal-content">
        <div Class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Create Good Type
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Good Type </div>
                    <div class="profile-info-value item-required">
                        <span Class="freeText" id="txt_good_type_name" maxlenght="50"></span>
                        <label id="required_txt_good_type_name"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name"> Description </div>
                    <div Class="profile-info-value">
                        <span Class="freeText" id="txt_good_type_description"></span>
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