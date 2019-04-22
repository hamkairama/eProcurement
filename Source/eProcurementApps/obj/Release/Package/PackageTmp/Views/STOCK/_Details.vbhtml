@ModelType eProcurementApps.Models.TPROC_STOCK
@Imports eProcurementApps.Helpers


<div class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Detail Item
            </div>
        </div>
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
                                <div class="profile-info-name "> item code </div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_item_code" maxlenght="10">@Model.ITEM_CODE</span>
                                </div>

                                <div class="profile-info-name"> lookup code </div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_lookup_code">@Model.LOOKUP_CODE</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> description </div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_item_description">@Model.ITEM_DESCRIPTION</span>
                                </div>

                                <div class="profile-info-name "> measurement </div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_unit_of_stock" maxlenght="10">@Model.UNIT_OF_STOCK</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> Qty. stock </div>
                                <div class="profile-info-value ">
                                    <span class="" id="txt_qty">@Model.QUANTITY</span>
                                </div>

                                <div class="profile-info-name "> min. Stock </div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_min_qty" maxlenght="10">@Model.QUANTITY_MIN</span>
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
                                <div class="profile-info-name "> latest cost </div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_latest_cost">@Model.LATEST_COST.ToString("###,###")</span>
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> average cost</div>
                                <div class="profile-info-value">
                                    <span class="" id="txt_average_cost">@Model.AVERAGE_COST.ToString("###,###")</span>
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
                                <div class="profile-info-name "> purchase account </div>
                                <div class="profile-info-value">
                                    <span id="txt_purchase_account" class="hidden">@Model.PURCHASE_ACCOUNT</span>
                                    @Html.DropDownListFor(Function(x) x.PURCHASE_ACCOUNT, ViewBag.COA)
                                </div>
                            </div>

                            <div class="profile-info-row">
                                <div class="profile-info-name "> stock expenses </div>
                                <div class="profile-info-value">
                                    <span id="txt_stock_expenses" class="hidden">@Model.STOCK_EXPENSES</span>
                                    @Html.DropDownListFor(Function(x) x.STOCK_EXPENSES, ViewBag.COA)
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
                                <div class="profile-info-name"> form type</div>
                                <div class="profile-info-value ">
                                    @Html.DropDownListFor(Function(x) x.FORM_TYPE_ID, ViewBag.FormType)
                                </div>

                                <div class="profile-info-name "> good type</div>
                                <div class="profile-info-value ">
                                    @Html.DropDownListFor(Function(x) x.GOOD_TYPE_ID, ViewBag.GoodType)
                                </div>
                            </div>

                            <div Class="profile-info-row">
                                <div Class="profile-info-name "> Supplier </div>
                                <div Class="profile-info-value ">
                                    @Html.DropDownListFor(Function(x) x.SUPPLIER_ID, ViewBag.Supplier)
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
        </div>
    </div>
</div>
<script src="~/Scripts/Standard/StandardModal.js"></script>


