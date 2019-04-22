@ModelType eProcurementApps.Models.TPROC_SUPPLIER

<div class="form-group" id="recomm_supp">
    @If Model IsNot Nothing Then

        @<div Class="col-sm-6">
           <h3><b>Recommendation Supplier</b></h3> 
            <div Class="profile-info-row">
                <div Class="profile-info-name">Name</div>
                <div Class="profile-info-value">                   
                    <span Class="" id="txt_cb_supplier_nm">@Model.SUPPLIER_NAME</span>
                    <span Class="hidden" id="txt_supp_id_recomm">@Model.ID</span>
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Address</div>
                <div Class="profile-info-value">
                    <span Class="" id="txt_address_supplier">@Model.SUPPLIER_ADDRESS</span>
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Phone</div>
                <div Class="profile-info-value">
                    <span Class="" id="txt_phone_supplier">@Model.MOBILE_NUMBER</span>
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Fax</div>
                <div Class="profile-info-value">
                    <span Class="" id="txt_fax_supplier">@Model.FAX_NUMBER</span>
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Contact Person</div>
                <div Class="profile-info-value">
                    <span Class="" id="txt_contact_person_supplier">@Model.CONTACT_PERSON</span>
                </div>
            </div>   
             
            <div Class="profile-info-row">
                <div Class="profile-info-name">Sub Total</div>
                <div Class="profile-info-value">
                    @*Rp. <span class="" id="txt_sub_total" style="text-align:right;font-weight:bold">@ViewBag.SubTotal</span>*@
                    Rp. <input class="" id="txt_sub_total" text="text" style="text-align:right" disabled value="@ViewBag.SubTotal" />
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Discount</div>
                <div Class="profile-info-value">
                    Rp. <input class="" id="txt_discount" text="text" style="text-align:right" disabled value=" @ViewBag.Disc"/>
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Vat</div>
                <div Class="profile-info-value">
                    Rp. <input class="" id="txt_vat" text="text" style="text-align:right" disabled  value=" @ViewBag.Vat"/>
                </div>
            </div>
            <div Class="profile-info-row">
                <div Class="profile-info-name">Pph</div>
                <div Class="profile-info-value">
                    Rp. <input class="" id="txt_wth_tax_pph" text="text"  style="text-align:right;" disabled value=" @ViewBag.Pph" />
                </div>
            </div>

            <div Class="profile-info-row">
                <div Class="profile-info-name">Grand Total</div>
                <div Class="profile-info-value">
                    @*Rp. <span Class="" id="txt_grand_total" style="text-align:right;font-weight:bold">@ViewBag.GrandTotal </span>*@
                    Rp. <input class="" id="txt_grand_total" text="text" style="text-align:right" disabled value="@ViewBag.GrandTotal" />
                </div>
            </div>
                 
        </div>

        @<div Class="col-xs-12" style="text-align:right">
            @*<a href = "#"><i Class='ace-icon fa fa-plus icon-on-right bigger-110' onclick="ShowAddCost()" title="click for additional cost"></i></a>*@ 
        </div>

        @<div id = "input_add_cost" style="display:none">
            <div Class="row">
                <div Class="col-xs-6" style="text-align:right">           
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                
                    Additional Cost For
                    <input size = 30 id="txt_add_cost_desc" text="text" placeholder="input description additional" />
                </div>

                <div Class="col-xs-6" style="font-weight:bold;text-align:left">
                    <div>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input size =6 id="txt_add_cost_per_" text="text" onkeyup="CalcAddCostPo(this)" style="text-align:right" />
                        <input id = "txt_add_cost" text="text" style="text-align:right" ReadOnly='readonly' />
                    </div>
                </div>
            </div>

            <div Class="row">
                <div Class="col-xs-6" style="text-align:right">
                    <Button Class='btn btn-sm btn-success btn-white btn-round' onclick="CalcFinalTotalPO()" type='submit'>
                        <i Class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                        Final Total :  
                    </button>
                </div>

                <div Class="col-xs-6" style="font-weight:bold;">
                    <div>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Rp. <input id="txt_final_total" text="text" style="text-align:right" readonly='readonly' />
                    </div>
                </div>
            </div>
        </div>
    End If
</div>
