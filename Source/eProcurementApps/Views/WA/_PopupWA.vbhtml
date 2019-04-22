@ModelType eProcurementApps.Models.TPROC_WA_ALLOWED_GR
@Imports eProcurementApps.Helpers

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                List Work Area
            </div>
        </div>
        <div class="modal-body no-padding">
            <div Class="row">
                <div Class="col-xs-12">
                    <div Class="clearfix">
                        <div Class="pull-right tableTools-container"></div>
                    </div>
                    <div>
                        <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>No</th>
                                    @*<th class="center">
                                            Select
                                        </th>*@
                                    <th style="display:none">ID</th>
                                    <th>Wa No</th>
                                    <th>Department</th>
                                    <th>Division</th>
                                    <th>Select</th>
                                </tr>
                            </thead>

                            <tbody>
                                @code
                                    Dim no As Integer = 1
                                    @For Each item In Model.TPROC_WA_ALLOWED_DT
                                        @if item.TPROC_WA.WA_NUMBER <> "199" Then
                                             @<tr>
                                            <td>@no</td>
                                             <td id="txt_wa_id_@no" style="display:none">
                                                 @item.TPROC_WA.ID.ToString("0")
                                             </td>
                                            <td id="txt_wa_@no">
                                                @item.TPROC_WA.WA_NUMBER.ToString("0")
                                            </td>
                                            <td id="txt_dept_@no">
                                                @item.TPROC_WA.TPROC_APPROVAL_GR.DEPARTMENT_NAME
                                            </td>
                                            <td id="txt_division_@no">
                                                @item.TPROC_WA.TPROC_APPROVAL_GR.TPROC_DIVISION.DIVISION_NAME
                                            </td>
                                            <td><a onclick="PassingDataWAOrigin('@no')" title="Select" href="#">@Html.Raw(Labels.ButtonForm("Select"))</a></td>
                                        </tr>
                                            no = no + 1
                                        End If
                                    Next
                                End Code
                            </tbody>
                        </table>
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
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>