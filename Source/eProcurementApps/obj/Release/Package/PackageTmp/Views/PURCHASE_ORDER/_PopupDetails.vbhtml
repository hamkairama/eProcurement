@Imports eProcurementApps.Helpers


<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                <b>Details Purchase Order</b>
                @*<span id="txt_id_gt">@Session("ID_GT")</span>*@
                <span id="txt_base_from">@ViewBag.BaseFrom</span>
            </div>
        </div>
        <table>
            <tr>
                <td>
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <span id="txt_cb_itempo_id" class="hidden"></span>
                            <div class="profile-info-name required"> Item </div>
                            <div class="profile-info-value item-required">
                                <span id="txt_cb_itempo_nm" class="hidden"></span>
                                @Html.DropDownList("dropdownList_itempo_nm", Dropdown.ListItemPOId(Convert.ToInt32(ViewBag.ForStorage), Convert.ToInt32(ViewBag.FormTypeId)), New With {.style = "width:  300px;"})
                                <label id="required_txt_cb_itempo_nm"></label>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                @*<td width="150px">
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> WA </div>
                            <div class="profile-info-value">
                                <span Class="" id="txt_wa"></span>
                            </div>
                        </div>
                        <div class="profile-info-row">
                            <div class="profile-info-name"></div>
                            <div class="profile-info-value">
                                <span Class="Text" id=""></span>
                            </div>
                        </div>
                    </div>
                </td>*@
                <td width="150px">
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Fund T1 </div>
                            <div class="profile-info-value">
                                <span id="txt_fund_t1_id" hidden="hidden"></span>
                                <label id="txt_fund_t1_nm"></label>
                            </div>
                        </div>
                        <div class="profile-info-row">
                            <div class="profile-info-name">Lob1 T2</div>
                            <div class="profile-info-value">
                                <span id="txt_lob1_t2_id" hidden="hidden"></span>
                                <label id="txt_lob1_t2_nm"></label>
                            </div>
                        </div>
                    </div>
                </td>
                <td width="150px">
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Plan T3 </div>
                            <div class="profile-info-value">
                                <span id="txt_plan_t3_id" hidden="hidden"></span>
                                <label id="txt_plan_t3_nm"></label>
                            </div>
                        </div>
                        <div class="profile-info-row">
                            <div class="profile-info-name">Lob2 T5</div>
                            <div class="profile-info-value">
                                <span id="txt_lob2_t5_id" hidden="hidden"></span>
                                <label id="txt_lob2_t5_nm"></label>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
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
                                    <th>PR Number</th>
                                    <th>U/M</th>
                                    <th>Quantity</th>
                                    <th>Price</th>
                                    <th>WA Number</th>
                                    <th>Action</th>
                                </tr>
                            </thead>

                            <tbody id="dataTablePODetail">
                                <tr>
                                    <td>
                                        <select id="dropdownList_prnumber" name="dropdownList_prnumber" style="width: 200px" onchange="PassingDataItemDetail(this)">
                                            <option>
                                                -select-
                                            </option>
                                        </select>
                                    </td>
                                    <td><input size=10 type="text" id="txt_um_detailpo_" readonly="readonly" style="text-align:left" /></td>
                                    <td><input size=10 type="text" id="txt_quantity_detailpo_" readonly="readonly" style="text-align:left" /></td>
                                    <td><input size=10 type="text" id="txt_price_detailpo_" readonly="readonly" style="text-align:left" /></td>
                                    <td><input size=10 type="text" id="txt_wa_number_" readonly="readonly" style="text-align:left" /></td>
                                    <td>
                                        <a class="blue" href="#" onclick="DeleteRowPODetail(this)" data-toggle="modal" title="Delete Row">
                                            @Html.Raw(Labels.IconAction("Delete"))
                                        </a>
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                        @Html.Raw(Labels.ButtonForm("AddRowItemPODetail"))
                    </div>
                    <div class="hr hr-18 dotted hr-double"></div>
                    <div class="row">
                        <div class="col-xs-7">
                        </div>

                        <div class="col-xs-1" style="text-align:right">
                            Total :
                        </div>

                        <div class="col-xs-4" style="font-weight:bold">
                            <div>
                                Rp. <span id="txt_sub_total_price_po"></span>
                            </div>
                        </div>
                    </div>
                    <div class="hr hr-18 dotted hr-double"></div>
                </div>
            </div>

        </div>
        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
            @Html.Raw(Labels.ButtonForm("SubmitPoDetail"))
        </div>
    </div>
</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/PURCHASING_ORDER_DETAILController.js"></script>

