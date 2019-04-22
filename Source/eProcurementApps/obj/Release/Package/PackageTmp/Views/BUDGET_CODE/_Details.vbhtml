@ModelType eProcurementApps.Models.TPROC_BUDGET_CODE
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Budget Code
            </div>
        </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name "> T. Budget </div>
                    <div class="profile-info-value">
                        <span>@Model.TABLE_BUDGET</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name "> T. Usage </div>
                    <div Class="profile-info-value ">
                        <span>@Model.TABLE_BUDGET_USAGE</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name "> Printing U </div>
                    <div class="profile-info-value">
                        <span>@Model.CD_PRINTING_USAGE</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name "> Printing S </div>
                    <div Class="profile-info-value ">
                        <span>@Model.CD_PRINTING_START</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name "> Printing E </div>
                    <div class="profile-info-value">
                        <span>@Model.CD_PRINTING_END</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name "> OfSup U </div>
                    <div Class="profile-info-value ">
                        <span>@Model.CD_OFFICE_SUPPLIE_USAGE</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name "> OfSup S </div>
                    <div class="profile-info-value ">
                        <span>@Model.CD_OFFICE_SUPPLIE_START</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name "> OfSup E </div>
                    <div Class="profile-info-value ">
                        <span>@Model.CD_OFFICE_SUPPLIE_END</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name"> A/NA U </div>
                    <div class="profile-info-value ">
                        <span>@Model.CD_ASSET_NONASSET_USAGE</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name"> A/NA S </div>
                    <div Class="profile-info-value ">
                        <span>@Model.CD_ASSET_NONASSET_START</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name "> A/NA E </div>
                    <div class="profile-info-value">
                        <span>@Model.CD_ASSET_NONASSET_END</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name "> PI U </div>
                    <div Class="profile-info-value">
                        <span>@Model.CD_PROMOTIONAL_ITEM_USAGE</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name "> PI S </div>
                    <div class="profile-info-value ">
                        <span>@Model.CD_PROMOTIONAL_ITEM_START</span>
                    </div>
                </div>

                <div Class="profile-info-row">
                    <div Class="profile-info-name"> PI E </div>
                    <div Class="profile-info-value ">
                        <span>@Model.CD_PROMOTIONAL_ITEM_END</span>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Is Active </div>
                    <div class="profile-info-value">
                        @If Model.IS_ACTIVE = 1 Then
                            @<input type="checkbox" id="txt_is_active" checked="checked" />
                        Else
                            @<input type="checkbox" id="txt_is_active" />
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
                        <span>@Model.CREATED_BY</span>
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
                        <span>@Model.LAST_MODIFIED_BY</span>
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


