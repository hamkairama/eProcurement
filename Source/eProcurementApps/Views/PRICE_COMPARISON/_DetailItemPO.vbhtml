@ModelType eProcurementApps.Models.TPROC_PO_DETAILS_ITEM
@Imports eProcurementApps.Helpers

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                <b>Details Purchase Order</b>
            </div>
        </div>
        <table>
            <tr>
                <td>
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Item </div>
                            <div class="profile-info-value">
                                <span>@Model.ITEM_NAME</span>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table>
            @For Each item In Model.TPROC_PO_DETAILS
                @<tr>
                     <td width="150px">
                         <div class="profile-user-info">
                             <div class="profile-info-row">
                                 <div class="profile-info-name"> WA </div>
                                 <div class="profile-info-value">
                                     <span>@item.WA_T4</span>
                                 </div>
                             </div>
                             <div class="profile-info-row">
                                 <div class="profile-info-name"></div>
                                 <div class="profile-info-value">
                                     <span></span>
                                 </div>
                             </div>
                         </div>
                     </td>
                     <td width="150px">
                         <div class="profile-user-info">
                             <div class="profile-info-row">
                                 <div class="profile-info-name"> Fund T1 </div>
                                 <div class="profile-info-value">
                                     <span>@item.FUND_T1</span>
                                 </div>
                             </div>
                             <div class="profile-info-row">
                                 <div class="profile-info-name">Lob1 T2</div>
                                 <div class="profile-info-value">
                                     <span>@item.LOB1_T2</span>
                                 </div>
                             </div>
                         </div>
                     </td>
                     <td width="150px">
                         <div class="profile-user-info">
                             <div class="profile-info-row">
                                 <div class="profile-info-name"> Plan T3 </div>
                                 <div class="profile-info-value">
                                     <span>@item.PLAN_T3</span>
                                 </div>
                             </div>
                             <div class="profile-info-row">
                                 <div class="profile-info-name">Lob2 T5</div>
                                 <div class="profile-info-value">
                                     <span>@item.LOB2_T5</span>
                                 </div>
                             </div>
                         </div>
                     </td>
                </tr>
                Exit For
            Next
        </table>
        
        
        <div class="space-4"></div>

        <div Class="clearfix">
            <div Class="pull-right tableTools-container"></div>
        </div>

        <div>
        @code
            Dim valTotal As Decimal = 0
            @<table id = "dynamic-table" Class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>PR Number</th>
                        <th>U/M</th>
                        <th>Quantity</th>
                        <th>Price</th>
                    </tr>
                </thead>
                    <tbody>
                        @For Each item In Model.TPROC_PO_DETAILS
                            valTotal = item.TOTAL.Value
                            @<tr>
                                <td>@item.PR_HEADER_NO</td>
                                <td>@item.USERMEASUREMENT</td>
                                <td>@item.QUANTITY</td>
                                <td>@item.PRICE</td>
                            </tr>
                        Next
                    </tbody>
            </table>
            @<div Class="hr hr-18 dotted hr-double"></div>
                    @<div Class="row">
                        <div Class="col-xs-7">
                        </div>
                        <div Class="col-xs-1" style="text-align:right">
                            Total:
                        </div>
                        <div Class="col-xs-4" style="font-weight:bold">
                            <div>
                                Rp. <span>@valTotal</span>
                            </div>
                        </div>
                    </div>
            @<div Class="hr hr-18 dotted hr-double"></div>
        End Code
        </div>

        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
        </div>
    </div>
</div>


<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
