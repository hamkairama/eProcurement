@ModelType eProcurementApps.Models.TPROC_STOCK
@Imports eProcurementApps.Helpers

<div class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Edit Item
            </div>
        </div>
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
       
        <div class="modal-body no-padding">
            <div class="profile-user-info">                  
         
                <div class="col-sm-12">
                    <div class="widget-box">
                        <div class="widget-header">
                            <h4 class="widget-title">Details <span id="txt_id">@Model.ID.ToString("0")</span></h4>
                            <div class="widget-toolbar">
                                <a href="#" data-action="collapse">
                                    <i class="ace-icon fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="widget-body" id="">
                           
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> item code </div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText change" id="txt_item_code" maxlenght="6">@Model.ITEM_CODE</span>
                                    <label id="required_txt_item_code"></label>
                                </div>

                                <div class="profile-info-name"> lookup code </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_lookup_code">@Model.LOOKUP_CODE</span>
                                    <label id="required_txt_lookup_code"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> description </div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText" id="txt_item_description"  maxlenght="50">@Model.ITEM_DESCRIPTION</span>
                                    <label id="required_txt_item_description"></label>
                                </div>

                                <div class="profile-info-name required"> measurement </div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText change" id="txt_unit_of_stock" maxlenght="5">@Model.UNIT_OF_STOCK</span>
                                    <label id="required_txt_unit_of_stock"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name"> Qty. stock </div>
                                <div class="profile-info-value">
                                    <span class="freeText" id="txt_qty">@Model.QUANTITY</span>
                                </div>

                                <div class="profile-info-name "> min. Stock </div>
                                <div class="profile-info-value">
                                    <span class="freeText change" id="txt_qty_min" maxlenght="10">@Model.QUANTITY_MIN</span>
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
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> latest cost </div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText" id="txt_latest_cost">@Model.LATEST_COST</span>
                                    <label id="required_txt_latest_cost"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> average cost</div>
                                <div class="profile-info-value item-required">
                                    <span class="freeText" id="txt_average_cost">@Model.AVERAGE_COST</span>
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
                                <div class="profile-info-name required"> purchase account </div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_purchase_account" class="hidden">@Model.PURCHASE_ACCOUNT</span>
                                    @Html.DropDownListFor(Function(x) x.PURCHASE_ACCOUNT, ViewBag.COA)
                                    <label id="required_txt_purchase_account"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name required"> stock expenses </div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_stock_expenses" class="hidden">@Model.STOCK_EXPENSES</span>
                                    @Html.DropDownListFor(Function(x) x.STOCK_EXPENSES, ViewBag.COA)
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
                            <div class="profile-info-row">
                                <div class="profile-info-name required"> form type</div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_form_type_id" class="hidden">@Model.FORM_TYPE_ID</span>
                                    @Html.DropDownListFor(Function(x) x.FORM_TYPE_ID, ViewBag.FormType)
                                    <label id="required_txt_form_type_id"></label>
                                </div>

                                <div class="profile-info-name required"> good type</div>
                                <div class="profile-info-value item-required">
                                    <span id="txt_good_type_id" class="hidden">@Model.GOOD_TYPE_ID</span>
                                    @Html.DropDownListFor(Function(x) x.GOOD_TYPE_ID, ViewBag.GoodType)
                                    <label id="required_txt_good_type_id"></label>
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name "> Supplier </div>
                                <div Class="profile-info-value ">
                                    <span id="txt_supplier_id" class="hidden">@Model.SUPPLIER_ID</span>
                                    @Html.DropDownListFor(Function(x) x.SUPPLIER_ID, ViewBag.Supplier)
                                    <label id="required_txt_supplier_id"></label>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name"> Created Time </div>
                                <div class="profile-info-value">
                                    <span>@Model.CREATED_TIME.ToString("dd-MM-yyy HH:mm") </span>
                                </div>

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

                                <div class="profile-info-name"> Last Modified By </div>
                                <div class="profile-info-value">
                                    <span>@Model.LAST_MODIFIED_BY </span>
                                </div>
                            </div>

                        </div>
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
    //create
    $(function () {
        $('#PURCHASE_ACCOUNT').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_purchase_account').text(optionSelected);
        });
    });
    $(function () {
        $('#STOCK_EXPENSES').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_stock_expenses').text(optionSelected);
        });
    });
    $(function () {
        $('#GOOD_TYPE_ID').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_good_type_id').text(optionSelected);
        });
    });
    $(function () {
        $('#SUPPLIER_ID').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_supplier_id').text(optionSelected);
        });
    });
    $(function () {
        $('#FORM_TYPE_ID').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_form_type_id').text(optionSelected);
        });
    });
</script>