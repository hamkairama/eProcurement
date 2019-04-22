@Imports eProcurementApps.Helpers

<div class="modal-dialog"  style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                <div class="nav-search" id="nav-search">
                    <span class="input-icon">
                        <input type="text" placeholder="Type or enter here..." class="nav-search-input" id="txt_search_stock" onkeypress="txtSearchStock(event)"
                               autocomplete="off" />
                        <i class="ace-icon fa fa-search nav-search-icon"></i>
                    </span>
                </div>
                List Item
                <span id="txt_id_gt" class="hidden">@Session("ID_GT")</span>
                <span id="txt_id_ft" class="hidden">@Session("ID_FT")</span>
            </div>
        </div>
        <div class="modal-body no-padding">
            <div Class="row">
                <div Class="col-xs-12">
                    <div Class="clearfix">
                        <div Class="pull-right tableTools-container"></div>
                    </div>
                    <div>
                        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
                        <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>No</th>
                                    @*<th class="center">
                                            Select
                                        </th>*@
                                    <th>ID Stock</th>
                                    <th>Item Code</th>
                                    <th>Description</th>
                                    <th>Lookup Cd</th>
                                    <th>Measurement</th>
                                    <th>Lastest Cost</th>
                                    <th>Good Type</th>
                                    <th>Qty T</th>
                                    <th>Select</th>
                                </tr>
                            </thead>

                            <tbody id="table_ad"></tbody>
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