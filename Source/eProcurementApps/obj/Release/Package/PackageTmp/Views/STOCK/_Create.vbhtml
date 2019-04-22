@Imports eProcurementApps.Helpers

<div Class="modal-dialog" style="width:1200px">
    <div Class="modal-content">
        <div Class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Create Item
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>

        <div class="modal-body no-padding">
            <div class="profile-user-info">

                <div class="col-sm-12">
                    <div class="widget-box">
                        <div class="widget-header">
                            <h4 class="widget-title">Details</h4>
                            <div class="widget-toolbar">
                                <a href="#" data-action="collapse">
                                    <i class="ace-icon fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="widget-body" id="">
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> Item Code  </div>
                                <div class="profile-info-value item-required">
                                    <span Class="freeText" id="txt_item_code" maxlenght="6"></span>
                                    <label id="required_txt_item_code"></label>
                                </div>

                                <div Class="profile-info-name required"> Description  </div>
                                <div Class="profile-info-value item-required">
                                    <span Class="freeText" id="txt_item_description" maxlenght="50"></span>
                                    <label id="required_txt_item_description"></label>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Lookup Code  </div>
                                <div Class="profile-info-value">
                                    <span Class="freeText" id="txt_lookup_code"></span>
                                    <label id="required_txt_lookup_code"></label>
                                </div>

                                <div class="profile-info-name required"> Measurement </div>
                                <div class="profile-info-value item-required">
                                    <span Class="freeText" id="txt_unit_of_stock" maxlenght="5"></span>
                                    <label id="required_txt_unit_of_stock"></label>
                                </div>
                            </div>
                            
                            <div class="profile-info-row">
                                <div class="profile-info-name "> Qty. stock </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_qty"></span>
                                </div>

                                <div class="profile-info-name "> min. Stock </div>
                                <div class="profile-info-value">
                                    <span class="freeText change" id="txt_qty_min"></span>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="widget-box">
                        <div class="widget-header">
                            <h4 class="widget-title">Cost</h4>
                            <div class="widget-toolbar">
                                <a href="#" data-action="collapse">
                                    <i class="ace-icon fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="widget-body" id="">
                            <div Class="profile-info-row">
                                <div Class="profile-info-name required"> Latest Cost  </div>
                                <div Class="profile-info-value item-required">
                                    <span Class="freeText" id="txt_latest_cost"></span>
                                    <label id="required_txt_latest_cost"></label>
                                </div>

                                <div Class="profile-info-name required"> Average Cost  </div>
                                <div Class="profile-info-value item-required">
                                    <span Class="freeText" id="txt_average_cost"></span>
                                    <label id="required_txt_average_cost"></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="widget-box">
                        <div class="widget-header">
                            <h4 class="widget-title">Account Code</h4>
                            <div class="widget-toolbar">
                                <a href="#" data-action="collapse">
                                    <i class="ace-icon fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="widget-body" id="">
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> Purc Acct.  </div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_purchase_account" class="hidden"></span>
                                    @Html.DropDownList("dropdownList_pa", Dropdown.COA)
                                    <label id="required_txt_purchase_account"></label>
                                </div>

                                <div Class="profile-info-name required"> Stock Exp.  </div>
                                <div Class="profile-info-value item-required">
                                    <span id="txt_stock_expenses" class="hidden"></span>
                                    @Html.DropDownList("dropdownList_se", Dropdown.COA)
                                    <label id="required_txt_stock_expenses"></label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="widget-box">
                        <div class="widget-header">
                            <h4 class="widget-title">Good Type and Supplier</h4>
                            <div class="widget-toolbar">
                                <a href="#" data-action="collapse">
                                    <i class="ace-icon fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="widget-body" id="">
                            <div Class="profile-info-row">
                                <div Class="profile-info-name required"> Form Type  </div>
                                <div Class="profile-info-value item-required">
                                    <span id="txt_form_type_id" class="hidden"></span>
                                    @Html.DropDownList("dropdownList_ft", Dropdown.FormType)
                                    <label id="required_txt_form_type_id"></label>
                                </div>

                                <div Class="profile-info-name required"> Good Type  </div>
                                <div Class="profile-info-value item-required">
                                    <span id="txt_good_type_id" class="hidden"></span>
                                    @Html.DropDownList("dropdownList_gt", Dropdown.GoodType)
                                    <label id="required_txt_good_type_id"></label>
                                </div>
                            </div>


                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Supplier </div>
                                <div Class="profile-info-value">
                                    <span id="txt_supplier_id" class="hidden"></span>
                                    @Html.DropDownList("dropdownList_sp", Dropdown.Supplier)
                                    <label id="required_txt_supplier_id"></label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                
            </div>
        </div>
        <div Class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
            @Html.Raw(Labels.ButtonForm("SaveCreate"))
        </div>
    </div>
</div>

<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>

<script>
    //create
    $(function () {
        $('#dropdownList_pa').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_purchase_account').text(optionSelected);
        });
    });
    $(function () {
        $('#dropdownList_se').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_stock_expenses').text(optionSelected);
        });
    });
    $(function () {
        $('#dropdownList_gt').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_good_type_id').text(optionSelected);
        });
    });
    $(function () {
        $('#dropdownList_sp').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_supplier_id').text(optionSelected);
        });
    });

    $(function () {
        $('#dropdownList_ft').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_form_type_id').text(optionSelected);
        });
    });
</script>