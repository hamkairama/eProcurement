﻿@ModelType eProcurementApps.Models.TPROC_LEVEL
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Confirmasi Delete
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row  hidden">
                    <div class="profile-info-name"> ID </div>
                    <div class="profile-info-value">
                        <span id="txt_id">@Model.ID.ToString("0")</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Indonesian Level </div>
                    <div class="profile-info-value">
                        <span>@Model.INDONESIAN_LEVEL</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Rupiah Limit </div>
                    <div class="profile-info-value">
                        <span>@Model.RUPIAH_LIMIT</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Canadian Dollars </div>
                    <div class="profile-info-value">
                        <span>@Model.CANADIAN_DOLLARS</span>
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
            Are you sure want to delete ?
            @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
            @Html.Raw(Labels.ButtonForm("No"))

        </div>
    </div>
</div>
<script src="~/Scripts/Standard/StandardModal.js"></script>


