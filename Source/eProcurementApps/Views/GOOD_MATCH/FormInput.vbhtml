@Imports eProcurementApps.Helpers 
@Imports eProcurementApps.Controllers.CRVController


@Code
    ViewBag.Breadcrumbs = "CRV"
    ViewBag.CRV = "active open"
    ViewBag.IndexCreateCRV = "active"
End Code

@If ViewBag.Message IsNot Nothing Then
    @<div Class="alert alert-success">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.Message 
        <br />
    </div>
End If

<div class="row">
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">CRV Application</h4><span id="txt_link_eproc" class="hidden">@CommonFunction.GetLinkEproc</span>
                <span id="txt_action" class="hidden">Create</span>
            </div>
            <div id="CRV_Id" class="hidden">@CRV.ID</div>
            <div id="txt_poid" class="hidden">@CRV.PO_ID</div>
            <div class=" Thenwidget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> CRV Number </div>
                        <div class="profile-info-value item-required">
                            @If CRV_Action = "Delete" Then
                                @<span Class="" id="txt_crvnum" maxlenght="20">
                                    @CRV.CRV_NUM
                                </span>
                                @<Label id = "required_txt_crvnum" ></label>
                            Else
                                @<span Class="freeText" id="txt_crvnum" maxlenght="20">
                                        @CRV.CRV_NUM
                                </span>
                                @<Label id = "required_txt_crvnum" ></label>
                            End If
                        </div> 

                        <div class="profile-info-name required"> Bank A/C Name </div>
                        <div class="profile-info-value item-required">
                            @If CRV_Action = "Delete" Then
                                @<span Class="" id="txt_bankaccountname" maxlenght="50">
                                    @CRV.BANK_NAME
                                </span>
                                @<label id="required_txt_bankaccountname"></label>
                            Else
                                @<span Class="freeText" id="txt_bankaccountname" maxlenght="50">
                                    @CRV.BANK_NAME
                                </span>
                                @<label id="required_txt_bankaccountname"></label>
                            End If
                            
                        </div> 
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> PO Number </div>
                        <div Class="profile-info-value item-required">
                            <span Class="" id="txt_ponum">
                                @If Not IsNothing(CRV.TPROC_PO_HEADERS) Then
                                    @CRV.TPROC_PO_HEADERS.PO_NUMBER
                                End If
                            </span> 
                            @If CRV_Action <> "Delete" Then
                                @<a Class="red" href="#" onclick="ModalSearch('/Search/Index', @Json.Encode(JsonPOHeader), 'Purchase Order', 'txt_poid:Y|txt_ponum:N|txt_suppliername:N|txt_currencycode:N|txt_origamount:N', '.dialogForm')" data-toggle="modal" title="Create">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>
                            End If
                        </div>

                        <div class="profile-info-name required"> Bank A/C Number </div>
                        <div class="profile-info-value item-required">
                            @If CRV_Action = "Delete" Then
                                @<span Class="" id="txt_bankaccountnum" maxlenght="50">
                                    @CRV.BANK_ACCOUNT_NUMBER
                                </span>
                                @<label id="required_txt_bankaccountnum"></label>
                            Else
                                @<span Class="freeText" id="txt_bankaccountnum" maxlenght="50">
                                    @CRV.BANK_ACCOUNT_NUMBER
                                </span>
                                @<label id="required_txt_bankaccountnum"></label>
                            End If
                            
                        </div> 
                    </div> 

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> Supplier Name </div>
                        <div Class="profile-info-value item-required">
                            <div id="txt_suppliername">
                                @If Not IsNothing(CRV.TPROC_PO_HEADERS) Then
                                    @CRV.TPROC_PO_HEADERS.SUPPLIER_NAME
                                End If
                            </div>
                        </div>

                        <div class="profile-info-name required"> Currency Code </div>
                        <div class="profile-info-value item-required">
                            <div id="txt_currencycode">
                                @If Not IsNothing(CRV.TPROC_PO_HEADERS) Then
                                    @CRV.TPROC_PO_HEADERS.TPROC_CURRENCY.CURRENCY_NAME
                                End If
                            </div>
                        </div> 
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Orig Amount </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_origamount" >
                                @CRV.AMOUNT
                            </span>
                            <label id="required_txt_crvnum"></label>
                        </div>                      
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                <a Class="red" href="@Url.Action("Index", "CRV")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                @If CRV_Action = "Create" Then
                    ViewBag.Title = "Create CRV"
                    @Html.Raw(Labels.ButtonForm("SaveCreate"))
                ElseIf CRV_Action = "Edit" Then
                    ViewBag.Title = "Edit CRV"
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                ElseIf CRV_Action = "Delete" Then
                    ViewBag.Title = "Delete CRV"
                    @Html.Raw(Labels.ButtonForm("SaveDelete"))
                End If
            </div>
        </div>
    </div>

</div>

<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>  
<script src="~/Scripts/Controllers/CRVController.js"></script> 