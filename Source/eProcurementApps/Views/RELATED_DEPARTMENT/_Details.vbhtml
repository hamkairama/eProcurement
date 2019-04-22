@ModelType eProcurementApps.Models.TPROC_REL_DEPT
@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Related Department
            </div>
        </div>
        <div class="modal-body no-padding">
            <div class="profile-user-info">
                <div class="profile-info-row">
                    <div class="profile-info-name"> RelDept.  </div>
                    <div class="profile-info-value">
                        <span>@Model.RELATED_DEPARTMENT_NAME</span>
                    </div>
                </div>
            </div>
            <table id="simple-table" class="table table-striped table-bordered table-hover">
                <tbody>
                    <tr>
                        <th>Approval Name</th>
                        <th>Limit Approval</th>
                    </tr>
                </tbody>
                <tbody id="dataTable">
                    @For Each item In Model.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT
                        @<tr id="0">
                            <td>@item.APPROVAL_NAME</td>
                            <td>@item.TPROC_LEVEL.INDONESIAN_LEVEL</td>
                        </tr>
                    Next

                </tbody>
            </table>

        </div>
        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
        </div>
    </div>
</div>
<script src="~/Scripts/Standard/StandardModal.js"></script>


