@ModelType eProcurementApps.Models.TPROC_DELIVERY_ADDRESS
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Delivery Address
            </div>
        </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name"> Delivery Name </div>
                    <div class="profile-info-value">
                        <span>@Model.DELIVERY_NAME</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Address </div>
                    <div class="profile-info-value">
                        <span>@Model.DELIVERY_ADDRESS</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Phone </div>
                    <div Class="profile-info-value item-required">
                        <span>@Model.DELIVERY_PHONE</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name required"> Fax </div>
                    <div Class="profile-info-value item-required">
                        <span >@Model.DELIVERY_FAX</span>
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


