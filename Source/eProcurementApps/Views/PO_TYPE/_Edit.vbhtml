@ModelType eProcurementApps.Models.TPROC_PO_TYPE
@Imports eProcurementApps.Helpers

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Edit PO Type
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row" hidden="hidden">
                    <div class="profile-info-name" id="dataID"> ID </div>
                    <div class="profile-info-value">
                        <span id="txt_id">@Model.ID.ToString("0")</span>
                    </div>
                </div>
                <div class="profile-info-row">
                    <div class="profile-info-name required"> PO Type </div>
                    <div class="profile-info-value item-required">
                        <span class="freeText change" id="txt_po_type_name" maxlenght="4">@Model.PO_TYPE_NAME</span>
                        <label id="required_txt_po_type_name"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name required"> Form Type</div>
                    <div class="profile-info-value item-required">
                        <span id="txt_form_type_id" class="hidden">@Model.FORM_TYPE_ID</span>
                        @Html.DropDownListFor(Function(x) x.FORM_TYPE_ID, ViewBag.FormType)
                        <label id="required_txt_form_type_id"></label>
                    </div>
                </div>

                <div class="profile-info-row">
                    <div class="profile-info-name"> Description </div>
                    <div class="profile-info-value">
                        <span class="freeText" id="txt_po_type_description">@Model.PO_TYPE_DESCRIPTION</span>
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

<script>
    //edit
    $(function () {
        $('#FORM_TYPE_ID').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_form_type_id').text(optionSelected);
        });
    });
</script>