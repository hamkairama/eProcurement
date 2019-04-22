@Imports eProcurementApps.Helpers

<div class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                <b>List approval of WA in each of input item</b>
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
                        <table id="tableChooseOneApproval" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th class="hidden">WA Id</th>
                                    <th class="hidden">WA numb</th>
                                    <th>Work Area</th>
                                    <th>Approval</th>
                                    <th class="">Action</th>
                                </tr>
                            </thead>

                            <tbody id="dataTableChooseOneApproval">
                                <tr>
                                    <td class="hidden"><input type="text" id="txt_wa_id_" readonly="readonly" /></td>
                                    <td class="hidden"><input type="text" id="txt_wa_numb_" readonly="readonly" /></td>
                                    <td>
                                        @Html.DropDownList("dropdownList_wa", Dropdown.WorkAreaWithDetailByUser(Session("USER_ID_ID")), New With {.style = "width:  200px;", .onchange = "javascript:GetWaId(this);"})
                                    </td>
                                    <td><input size=100 type="text" id="txt_approval_" readonly="readonly" style="text-align:left" /></td>
                                    <td>
                                        <a class="blue" href="#" onclick="DeleteRowChooseOneApproval(this)" data-toggle="modal" title="Delete Row">
                                            @Html.Raw(Labels.IconAction("Delete"))
                                        </a>
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                        @Html.Raw(Labels.ButtonForm("AddRowChooseOneApproval"))
                    </div>
                    <div class="hr hr-18 dotted hr-double"></div>

                    <b>Approval WA</b>
                    @Html.Partial("_ListApprovalWa")

                    <div class="hr hr-18 dotted hr-double"></div>

                </div>
            </div>

        </div>
        <div class='modal-footer no-margin-top'>
            @*@Html.Raw(Labels.ButtonForm("Close"))*@
            @*@Html.Raw(Labels.ButtonForm("SubmitPoDetail"))*@


            <button class="btn btn-sm btn-danger btn-white btn-round" onclick="ClosePopupAck()">
                <i class="ace-icon fa fa-times bigger-110 red2"></i>
                Close
            </button>

            <Button Class='btn btn-sm btn-success btn-white btn-round' onclick="GetWaApproval()" type='submit'>
                Oke Set
            </Button>

        </div>
    </div>
</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>
@*<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
  <script src="~/Scripts/Standard/StandardProfile.js"></script>*@
