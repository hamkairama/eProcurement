@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_CRV)
@Imports eProcurementApps.Helpers

@*<link href="~/Ace/fileInput/fileinput.css" rel="stylesheet" />
<link href="~/Ace/fileInput/theme.css" rel="stylesheet" />
<script src="~/Ace/fileInput/fileinput.js"></script>*@

<div class="modal-dialog"  style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                <b>Details CRV</b>
            </div>
        </div>

        <div class="space-4"></div>
        <div Class="clearfix">
            <div Class="pull-right tableTools-container"></div>
        </div>

        <div>
            <table id="table_input" class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Crv Number </th>
                        <th>Created By</th>
                        <th>Created Dt</th>
                        <th>Crv Amount</th>
                        <th>Bank Name</th>
                        <th>Bank Acc</th>
                        <th>Complete By</th>
                        <th>Complete Dt</th>
                    </tr>
                </thead>

                <tbody id="dataTable">
                    @for Each item In Model
                        @<tr>
                            <td>@item.CRV_NUM</td>
                            <td>@item.CREATED_BY</td>
                            <td>@item.CREATED_TIME.Value.ToString("d-MMM-yy")</td>
                            <td align="right">@item.GRAND_TOTAL.Value.ToString("###,###")</td>
                            <td>@item.BANK_NAME</td>
                            <td>@item.BANK_ACCOUNT_NUMBER</td>
                            <td>@item.COMPLETED_BY</td>
                            <td>@item.COMPLETED_DATE</td>                             
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
@*<script src="~/Scripts/Custom/CustomOtherTable.js"></script>*@
@*<script src="~/Scripts/Standard/StandardProfile.js"></script>*@
