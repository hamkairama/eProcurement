@ModelType eProcurementApps.Models.TPROC_DELIVERY_ADDRESS
@Imports eProcurementApps.Helpers

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Edit Delivery Address
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row  hidden">
                    <div class="profile-info-name" id="dataID"> ID </div>
                    <div class="profile-info-value">
                        <span id="txt_id">@Model.ID.ToString("0")</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name required"> Delivery Nm </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText change" id="txt_delivery_name" maxlenght="50">@Model.DELIVERY_NAME</span>
                        <label id="required_txt_delivery_name"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name required"> Address </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText change" id="txt_delivery_address1" maxlenght="100">@Model.DELIVERY_ADDRESS</span>
                        <label id="required_txt_delivery_address1"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Phone </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText" id="txt_delivery_phone" maxlenght="20">@Model.DELIVERY_PHONE</span>
                        <label id="required_txt_delivery_phone"></label>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Fax </div>
                    <div Class="profile-info-value item-required">
                        <span Class="freeText" id="txt_delivery_fax" maxlenght="20">@Model.DELIVERY_FAX</span>
                        <label id="required_txt_delivery_fax"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Default Indicator </div>
                    <div class="profile-info-value">
                        @If Model.DEFAULT_IND = 1 Then
                            @<input type="checkbox" id="cb_default_indicator" checked="checked" />
                        Else
                            @<input type="checkbox" id="cb_default_indicator" />
                        End If
                        <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be default indicator used">?</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name "> Created Time </div>
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