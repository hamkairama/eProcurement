@Imports eProcurementApps.Helpers

<div class="modal-dialog" style="width:1200px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                <div class="nav-search" id="nav-search">
                    <span class="input-icon">
                        <input type="text" placeholder="Search desc..." class="nav-search-input" id="txt_search_supplier" onkeypress="txtSearchSsupplier(event)"
                               autocomplete="off" />
                        <i class="ace-icon fa fa-search nav-search-icon"></i>
                    </span>
                </div>
                List Vendor
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
                                    <th style="display:none">ID Vendor</th>
                                    <th>Vendor Code</th>
                                    <th>Vendor Name</th>
                                    <th style="display:none">Vendor Alias Name</th>
                                    <th style="display:none">Address</th>
                                    <th style="display:none">Contact Person</th>
                                    <th style="display:none">Email</th>
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