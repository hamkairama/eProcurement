@ModelType eProcurementApps.Models.TPROC_STOCK
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
                    <div class="profile-info-name"> item code </div>
                    <div class="profile-info-value">
                        <span>@Model.ITEM_CODE</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> item description </div>
                    <div class="profile-info-value">
                        <span>@Model.ITEM_DESCRIPTION</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> unit of stock </div>
                    <div class="profile-info-value">
                        <span>@Model.UNIT_OF_STOCK</span>
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


