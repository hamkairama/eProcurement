@Imports eProcurementApps.Helpers

<div Class="modal-dialog">
    <div Class="modal-content">
        <div Class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Create Pph
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Pph </div>
                    <div class="profile-info-value item-required">
                        <span Class="freeText" id="txt_pph_name" maxlenght="10"></span>
                        <label id="required_txt_pph_name"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Start Date </div>
                    <div Class="profile-info-value item-required">
                        <span Class="dateText" id="txt_start_date"></span>
                        <label id="required_txt_start_date"></label>
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