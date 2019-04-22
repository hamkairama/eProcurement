@Imports eProcurementApps.Helpers

<div Class="modal-dialog">
    <div Class="modal-content">
        <div Class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Create Delivery Address
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Delivery Nm </div>
                    <div class="profile-info-value item-required">
                        <span Class="freeText" id="txt_delivery_name" maxlenght="50"></span>
                        <label id="required_txt_delivery_name"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Address </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText" id="txt_delivery_address1" maxlenght="100"></span>
                        <label id="required_txt_delivery_address1"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Phone </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText" id="txt_delivery_phone" maxlenght="20"></span>
                        <label id="required_txt_delivery_phone"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Fax </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText" id="txt_delivery_fax" maxlenght="20"></span>
                        <label id="required_txt_delivery_fax"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Default Indicator </div>
                    <div class="profile-info-value">
                        <input type="checkbox" id="cb_default_indicator" />
                        <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be default indicator used">?</span>
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