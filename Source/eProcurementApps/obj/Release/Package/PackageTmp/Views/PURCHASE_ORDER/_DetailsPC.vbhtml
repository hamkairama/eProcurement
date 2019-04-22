@ModelType eProcurementApps.Models.TPROC_PC
@Imports eProcurementApps.Helpers

<div Class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                <b>Details PC</b>
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
                        <th>PC Number </th>
                        <th>Rec Supplier</th>
                        <th>Grand Total</th>
                        <th>Status</th>
                        <th>Created By</th>
                        <th>Created Dt</th>
                    </tr>
                </thead>

                <tbody id="dataTable">
                    @if Model IsNot Nothing Then
                        @<tr>
                            <td><a href="@Url.Action("DetailPc", "PRICE_COMPARISON", New With {.id = Model.ID, .flag = Convert.ToInt32(ListEnum.FlagInbox.OnlyView)})" target="_blank">@Model.PC_NUM</a></td>
                            <td>@Model.RECOM_SUPPLIER_NM</td>
                            <td align="right">@Model.GRAND_TOTAL.ToString("###,###")</td>
                            <td>@Model.STATUS</td>
                            <td>@Model.CREATED_BY</td>
                            <td>@Model.CREATED_TIME.ToString("d-MMM-yy")</td>
                        </tr>
                    End If
                </tbody>
            </table>
        </div>

        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
        </div>

    </div>
</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>
@*<script src="~/Scripts/Standard/StandardProfile.js"></script>
    <script src="~/Scripts/Custom/CustomOtherTable.js"></script>*@
