@ModelType eProcurementApps.Models.TPROC_BUDGET_CODE
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
       
        <div class='modal-footer no-margin-top'>
            Are you sure want to delete ? <span class="hidden" id="txt_id">@Model.ID</span>
            @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
            @Html.Raw(Labels.ButtonForm("No"))

        </div>
    </div>
</div>
<script src="~/Scripts/Standard/StandardModal.js"></script>


