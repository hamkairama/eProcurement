@ModelType eProcurementApps.Models.TPROC_WA
@Imports eProcurementApps.Helpers

<div class="row">
    <div class="col-sm-5">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Work Area Application</h4>
            </div>
            <div class="widget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> WA Number </div>
                        <div class="profile-info-value item-required">
                            <span>@Model.WA_NUMBER</span>
                            <label id="required_txt_wa_number"></label>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> Department </div>
                        <div Class="profile-info-value item-required">
                            <span>@Model.TPROC_APPROVAL_GR.DEPARTMENT_NAME</span>
                            <label id="required_txt_dept_name"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Division </div>
                        <div class="profile-info-value item-required">
                            <span>@Model.TPROC_APPROVAL_GR.TPROC_DIVISION.DIVISION_NAME</span>
                            <label id="required_txt_division_id"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-9">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Approval</h4>
            </div>
            <div class="widget-body" id="testing">
                <table id="simple-table" class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th>Approval Name</th>
                            <th>Email</th>
                            <th>Limit Approval</th>
                            <th>Flow Number</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTable">
                        @For Each item In Model.TPROC_APPROVAL_GR.TPROC_APPROVAL_DT
                        @<tr id="0">
                            <td>@item.APPROVAL_NAME</td>
                             <td>@item.EMAIL</td>
                             <td>@item.TPROC_LEVEL.INDONESIAN_LEVEL</td>
                            <td>@item.FLOW_NUMBER</td>
                        </tr>
                        Next

                    </tbody>
                </table>
            </div>
        </div>
    </div>


    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                @Html.Raw(Labels.ButtonForm("Close"))
            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>

