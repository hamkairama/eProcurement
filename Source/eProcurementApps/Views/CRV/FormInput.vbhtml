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
                <div style="float: right;">
                    <div class="profile-info-name"> Created Date </div>
                    <div class="profile-info-value">
                        @if dtTable.Rows.Count > 0 Then
                            @<span Class="" id="txt_created_dt">@dtTable.Rows(0).Item("CREATED_TIME") </span>
                        End If
                    </div>
                </div>
            </div>
            <div id="CRV_Id" class="hidden">
                @If dtTable.Rows.Count > 0 Then
                    @dtTable.Rows(0).Item("ID")
                End If
            </div>
            <div id="txt_poid" Class="hidden">
                @If dtTable.Rows.Count > 0 Then
                    @dtTable.Rows(0).Item("PO_ID")
                End If
            </div>
            <div id="txt_supplierid" Class="hidden">
                @If dtTable.Rows.Count > 0 Then
                    @dtTable.Rows(0).Item("SUPPLIER_ID")
                End If
            </div>
            <div id="FLAGFORM" Class="hidden">
                @FLAGFORM
            </div>
            <div id="File_Doc" class="hidden">
            </div>
            <div class=" Thenwidget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> CRV Number </div>
                        <div class="profile-info-value item-required">
                            @If dtTable.Rows.Count > 0 Then
                                @<span Class="" id="txt_crvnum" maxlenght="20">
                                    @dtTable.Rows(0).Item("CRV_NUM")
                                </span> 
                            Else
                                @<span Class="" id="txt_crvnum" maxlenght="20">
                                    AUTO GENERATE BY SYSTEM
                                </span> 
                            End If
                        </div>

                        <div class="profile-info-name required"> Supplier Name </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_suppliername">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("SUPPLIER_NAME")
                                End If
                            </span>
                            @If (CRV_Action = "Create" OrElse CRV_Action = "Edit") = True Then
                                @<a Class="red" href="#" onclick="ModalSearch('/Search/Index', @Json.Encode(JSONSupplierDataTable), 'Search Supplier', 'txt_supplierid:Y|txt_suppliername:N|txt_bankname:N|txt_bankbranch:N|txt_bankaccountnumber:N|txt_reftaxno:N', '.dialogForm')" data-toggle="modal" title="Create">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>
                            End If
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Ref Number </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_gmnumber">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("GM_NUMBER")
                                End If
                            </span>
                            @If (CRV_Action = "Create" OrElse CRV_Action = "Edit") = True Then
                                @<a id="posearch" Class="red" href="#" data-toggle="modal" title="Create">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>
                            End If
                        </div>

                        <div class="profile-info-name required"> Ref Tax No. </div>
                        <div class="profile-info-value item-required">
                            <span Class="freeText" id="txt_reftaxno" maxlenght="20">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("REFTAXNO")
                                End If
                            </span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> PO Number </div>
                        <div class="profile-info-value item-required">
                            @*<span Class="" id="txt_ponumber" maxlenght="50">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("PO_NUMBER")
                                End If
                            </span>*@
                            @If dtTable.Rows.Count > 0 Then
                                @<span id="txt_ponum" maxlenght="50"><a href="@Url.Action("DetailHeaderOnlyView", "PURCHASE_ORDER", New With {.id = dtTable.Rows(0).Item("PO_ID")})" target="_blank">@dtTable.Rows(0).Item("PO_NUMBER")</a></span>
                            Else
                                @<span id="txt_ponumber" maxlenght="50"></span>
                            End If
                        </div>

                        <div class="profile-info-name required"> Payment Mtd </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_paymentmethod" maxlenght="50">
                                TRANSFER
                            </span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Bank Name </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_bankname" maxlenght="50">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("BANK_NAME")
                                End If
                            </span>
                        </div>

                        <div class="profile-info-name required"> Bank Branch </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_bankbranch" maxlenght="50">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("BANK_BRANCH")
                                End If
                            </span>  
                        </div> 
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Bank Acc No. </div>
                        <div class="profile-info-value item-required">
                            <span Class="" id="txt_bankaccountnumber" maxlenght="50">
                                @If dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("BANK_ACCOUNT_NUMBER")
                                End If
                            </span>
                        </div>

                        <div class="profile-info-name required"> Kliring No </div>
                        <div class="profile-info-value item-required">
                            @If (CRV_Action = "Create" OrElse CRV_Action = "Edit") = True Then
                                @<span Class="freeText" id="txt_kliringno" maxlenght="50">
                                    @If dtTable.Rows.Count > 0 Then
                                        @dtTable.Rows(0).Item("KLIRINGNO")
                                    End If
                                </span>
                            Else
                                @<span Class="" id="txt_kliringno" maxlenght="50">
                                    @If dtTable.Rows.Count > 0 Then
                                        @dtTable.Rows(0).Item("KLIRINGNO")
                                    End If
                                </span>
                            End If
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Detail Item</h4>
                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table id="myTable" class="table table-striped table-bordered table-hover">
                    <thead id="table_th">
                        <tr>
                            <th> No </th> 
                            <th class="hidden"> Item Id </th>
                            <th class="hidden"> UNITMEASUREMENT </th>
                            <th class=""> QUANTITY </th>
                            <th class=""> PRICE </th>
                            <th> DESCRIPTION </th>
                            <th> CURR </th>
                            <th> ACCOUNT DEBIT </th>
                            <th> OTHER AMOUNT </th>
                            <th> RUPIAH AMOUNT </th>
                            <th> T1 FUND </th>
                            <th> T2 LOB1 </th>
                            <th> T3 PLAN </th>
                            <th> T4 W/A </th>
                            <th> T5 LOB2 </th> 
                            <th class="hidden"> Currency Id </th>
                        </tr>
                    </thead>
                    <tbody id="table_ap">
                        @For i = 0 To dtTableDetail.Rows.Count - 1
                            @<tr id="@i">
                                <td id=@(i.ToString() + "_1")>
                                    <input size=2 type="text" id=@(i.ToString() + "1") readonly="readonly" value="@dtTableDetail.Rows(i).Item("NO")" />
                                </td>
                                <td class="hidden" id=@(i.ToString() + "_2")>
                                    <input size=5 type="text" id=@(i.ToString() + "2") readonly="readonly" value="@dtTableDetail.Rows(i).Item("ITEM_ID")" />
                                </td>
                                <td class="hidden" id=@(i.ToString() + "_3")>
                                    <input size=5 type="text" id=@(i.ToString() + "3") readonly="readonly" value="@dtTableDetail.Rows(i).Item("UNITMEASUREMENT")" />
                                </td>
                                <td class="" id=@(i.ToString() + "_4")>
                                    <input size=5 type="text" id=@(i.ToString() + "4") readonly="readonly" value="@dtTableDetail.Rows(i).Item("QUANTITY")" />
                                </td>
                                <td class="" id=@(i.ToString() + "_5")>
                                    <input size=5 type="text" id=@(i.ToString() + "5") readonly="readonly" value="@dtTableDetail.Rows(i).Item("PRICE")" />
                                </td>
                                <td id=@(i.ToString() + "_6")>
                                    <input size=20 type="text" id=@(i.ToString() + "6") readonly="readonly" value="@dtTableDetail.Rows(i).Item("DESCRIPTION")" />
                                </td>
                                <td id=@(i.ToString() + "_7")>
                                    <input size=3 type="text" id=@(i.ToString() + "7") readonly="readonly" value="@dtTableDetail.Rows(i).Item("CURR")" />
                                </td>
                                <td id=@(i.ToString() + "_8")>
                                    <input size=5  type="text" id=@(i.ToString() + "8") readonly="readonly" value="@dtTableDetail.Rows(i).Item("ACCOUNT_DEBIT")" />
                                </td>
                                <td id=@(i.ToString() + "_9")>
                                    <input size=10 style="text-align:right" type="text" id=@(i.ToString() + "9") readonly="readonly" value="@Format(dtTableDetail.Rows(i).Item("OTH_AMOUNT"), "###,##0")" />
                                </td>
                                <td id=@(i.ToString() + "_10")>
                                    <input size=10 style="text-align:right" type="text" id=@(i.ToString() + "10") readonly="readonly" value="@Format(dtTableDetail.Rows(i).Item("RUPIAH_AMOUNT"), "###,##0")" />
                                </td>
                                <td id=@(i.ToString() + "_11")>
                                    <input size=5 type="text" id=@(i.ToString() + "11") readonly="readonly" value="@dtTableDetail.Rows(i).Item("FUND_T1")" />
                                </td>
                                <td id=@(i.ToString() + "_12")>
                                    <input size=5 type="text" id=@(i.ToString() + "12") readonly="readonly" value="@dtTableDetail.Rows(i).Item("LOB1_T2")" />
                                </td>
                                <td id=@(i.ToString() + "_13")>
                                    <input size=5 type="text" id=@(i.ToString() + "13") readonly="readonly" value="@dtTableDetail.Rows(i).Item("PLAN_T3")" />
                                </td>
                                <td id=@(i.ToString() + "_14")>
                                    <input size=5 type="text" id=@(i.ToString() + "14") readonly="readonly" value="@dtTableDetail.Rows(i).Item("WA_T4")" />
                                </td>
                                <td id=@(i.ToString() + "_15")>
                                    <input size=5 type="text" id=@(i.ToString() + "15") readonly="readonly" value="@dtTableDetail.Rows(i).Item("LOB2_T5")" />
                                </td> 
                                 <td class="hidden" id=@(i.ToString() + "_16")>
                                     <input size=5 type="text" id=@(i.ToString() + "16") readonly="readonly" value="@dtTableDetail.Rows(i).Item("CURRENCY")" />
                                 </td>
                            </tr>
                        Next
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Historical</h4>
                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>
            <div class="widget-body" id="">
                <table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Created By </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_created_by">
                                            @If dtTable.Rows.Count > 0 Then
                                            @dtTable.Rows(0).Item("CREATED_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Created Dt </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_created_time">
                                            @If dtTable.Rows.Count > 0 Then
                                            @dtTable.Rows(0).Item("CREATED_TIME")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>

                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Verified By </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_created_by">
                                            @If dtTable.Rows.Count > 0 Then
                                            @dtTable.Rows(0).Item("VERIFY_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Verified Dt </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_created_time">
                                            @If dtTable.Rows.Count > 0 Then
                                            @dtTable.Rows(0).Item("VERIFY_TIME")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Approved By  </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_approved_by">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("APPROVE_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Approved Dt </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_approved_date">
                                            @If dtTable.Rows.Count > 0 Then
                                             @dtTable.Rows(0).Item("APPROVE_DATE")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>                       
                    </tr>
                    <tr>
                        <td colspan="3"><hr /></td>
                    </tr>
                    <tr>
                         <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Received By  </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_received_by">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("RECEIVED_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Received Dt </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_received_date">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("RECEIVED_DATE")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Paid/Completed By  </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_completed_by">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("COMPLETED_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Paid/Completed Dt </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_completed_date">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("COMPLETED_DATE")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                          <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Rejected By  </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_rejected_by">
                                            @If dtTable.Rows.Count > 0 Then
                                                 @dtTable.Rows(0).Item("REJECTED_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Rejected Dt </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_rejected_date">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("REJECTED_TIME")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Reject Note </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_completed_date">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("REJECTNOTE")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="row">
            <div class="col-xs-10" style="text-align:right">
                Sub Total :
            </div>

            <div class="col-xs-1" style="font-weight:bold;text-align:right">
                <div>
                    <span id="txt_sub_total" style="text-align:right">
                        @If dtTable.Rows.Count > 0 Then
                            @Format(dtTable.Rows(0).Item("SUB_TOTAL"), "###,##0")
                        End If
                    </span>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-10" style="text-align:right">
                Discount % :
            </div>

            <div class="col-xs-1" style="font-weight:bold;text-align:right">
                <div>
                    <span id="txt_discount" style="text-align:right">
                        @If dtTable.Rows.Count > 0 Then
                            @Format(dtTable.Rows(0).Item("DSCNT_AMT"), "###,##0")
                        End If
                    </span>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-10" style="text-align:right">
                VAT % :
            </div>

            <div class="col-xs-1" style="font-weight:bold;text-align:right">
                <div>
                    <span id="txt_vat" style="text-align:right">
                        @If dtTable.Rows.Count > 0 Then
                            @Format(dtTable.Rows(0).Item("VAT"), "###,##0")
                        End If
                    </span>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-10" style="text-align:right">
                Wth tax / PPH % :
            </div>

            <div class="col-xs-1" style="font-weight:bold;text-align:right">
                <div>
                    <span id="txt_wth_tax_pph" style="text-align:right">
                        @If dtTable.Rows.Count > 0 Then
                            @Format(dtTable.Rows(0).Item("WTH_TAX"), "###,##0")
                        End If
                    </span>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-10" style="text-align:right">
                Grand Total :
            </div>

            <div class="col-xs-1" style="font-weight:bold;text-align:right">
                <div>
                    <span id="txt_grand_total" style="text-align:right">
                        @If dtTable.Rows.Count > 0 Then
                            @Format(dtTable.Rows(0).Item("GRAND_TOTAL"), "###,##0")
                        End If
                    </span>
                </div>
            </div>
        </div>
    </div>

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                <a id="BtnClosed" Class="red" href="#" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                @If CRV_Action = "Create" Then
                    ViewBag.Title = "Create CRV"
                    @Html.Raw(Labels.ButtonForm("SaveSubmit"))
                ElseIf CRV_Action = "Detail" Then
                    ViewBag.Title = "Detail CRV"
                ElseIf CRV_Action = "Edit" Then
                    ViewBag.Title = "Edit CRV"
                    @Html.Raw(Labels.ButtonForm("SaveSubmit"))
                ElseIf CRV_Action = "Verified" Then
                    ViewBag.Title = "Verify CRV"
                    @Html.Raw(Labels.ButtonForm("Verified")) 
                ElseIf CRV_Action = "Approve" Then
                    ViewBag.Title = "Approve CRV"
                    @Html.Raw(Labels.ButtonForm("SaveApprove"))
                ElseIf CRV_Action = "Received" Then
                    ViewBag.Title = "Received CRV"
                    @Html.Raw(Labels.ButtonForm("SaveReceived"))
                ElseIf CRV_Action = "Paid" Then
                    ViewBag.Title = "Paid CRV"
                    @Html.Raw(Labels.ButtonForm("SavePaid"))
                End If

                @If CRV_Action = "Create" Then
                    ViewBag.Title = "Create CRV"
                    @Html.Raw(Labels.ButtonForm("SaveCreate"))
                ElseIf CRV_Action = "Edit" Then
                    ViewBag.Title = "Edit CRV"
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                    @<a Class="blue" href="@Url.Action("ActionExportPdf", "CRV", New With {._ID = dtTable.Rows(0).Item("ID")})" target="_blank" title="Export pdf file">
                        @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                    </a>
                ElseIf CRV_Action = "Verified" Then
                    ViewBag.Title = "Verify CRV"
                    @Html.Raw(Labels.ButtonForm("Rejected")) 
                    @<a Class="blue" href="@Url.Action("ActionExportPdf", "CRV", New With {._ID = dtTable.Rows(0).Item("ID")})" target="_blank" title="Export pdf file">
                        @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                    </a>
                ElseIf CRV_Action = "Delete" Then
                    ViewBag.Title = "Delete CRV"
                    @Html.Raw(Labels.ButtonForm("SaveDelete"))
                ElseIf CRV_Action = "Approve" Then
                    ViewBag.Title = "Approve CRV"
                    @Html.Raw(Labels.ButtonForm("Rejected"))
                    @<a Class="blue" href="@Url.Action("ActionExportPdf", "CRV", New With {._ID = dtTable.Rows(0).Item("ID")})" target="_blank" title="Export pdf file">
                        @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                    </a>
                ElseIf CRV_Action = "Received" Or CRV_Action = "Paid" Or CRV_Action = "Detail" Then
                    @<a Class="blue" href="@Url.Action("ActionExportPdf", "CRV", New With {._ID = dtTable.Rows(0).Item("ID")})" target="_blank" title="Export pdf file">
                        @Html.Raw(Labels.ButtonForm("ActionExportToPdf"))
                    </a> 
                End If       
            </div>

            @If CRV_Action = "Approve" Or CRV_Action = "Verified" Then
                @<br />
                @<div Class="row top-left" id="">
                    <div Class="col-lg-12">
                        Reason of reject
                        <textarea type="text" style="height:100px" Class="form-control input-group" id="txt_reason_reject"></textarea>
                    </div>
                </div>
            End If
            @If CRV_Action = "Paid" Then
                @<br />
                @<div Class="row top-left" id="">
                    <div Class="col-lg-12">
                        Attach document from bank <br />
                        <input type="text" id="txt_attach_doc" style="width:60%" readonly="readonly" />
                        <button id="btnBrowse" onclick="GetFile()">Browse</button>
                        <input type="file" id="postedFile" style="display:none"  />  
                    </div>
                </div>
            End If
        </div>
    </div>

</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>  
<script src="~/Scripts/Controllers/CRVController.js"></script> 

<script type="text/javascript">
    document.getElementById("BtnClosed").onclick = function () {
        FLAGFORM = $("#FLAGFORM").text().trim();
        if (FLAGFORM == "1") {
            document.getElementById("BtnClosed").href = linkProc + '/CRV/Index';
        } else {
            document.getElementById("BtnClosed").href = linkProc + '/DASHBOARD/IndexJobLists?_FlagInbox=' + FLAGFORM;
        }
    };

</script>