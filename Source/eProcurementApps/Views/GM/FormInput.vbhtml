@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.GMController


@Code
    ViewBag.Breadcrumbs = "Good Match"
    ViewBag.Matching = "active open"
    ViewBag.CreateGoodMatch = "active"
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
                <h4 class="widget-title">Good Match Application</h4><span id="txt_link_eproc" class="hidden">@CommonFunction.GetLinkEproc</span>
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
            <div id="GM_Id" class="hidden">
                @If dtTable.Rows.Count > 0 Then
                    @dtTable.Rows(0).Item("ID")
                End If
            </div>
            <div id="txt_poid" Class="hidden">
                @If dtTable.Rows.Count > 0 Then
                    @dtTable.Rows(0).Item("PO_ID")
                End If
            </div>
            <div id="FLAGFORM" Class="hidden">
                @FLAGFORM
            </div>
            <div id="PO_TYPE" Class="hidden">
                @If dtTable.Rows.Count > 0 Then
                    @dtTable.Rows(0).Item("PO_TYPE")
                End If 
            </div>
            <div class=" Thenwidget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> GM Number </div>
                        <div class="profile-info-value item-required">
                            @If dtTable.Rows.Count > 0 Then
                                @<span Class="" id="txt_gmnum" maxlenght="20">
                                    @dtTable.Rows(0).Item("GM_NUMBER")
                                </span>
                            Else
                                @<span Class="" id="txt_gmnum" maxlenght="20">
                                    AUTO GENERATE BY SYSTEM
                                </span>
                            End If
                        </div>

                        <div class="profile-info-name required"> PO Number </div>
                        <div class="profile-info-value item-required">
                            @If dtTable.Rows.Count > 0 Then
                                @<span id = "txt_ponum"><a href="@Url.Action("DetailHeaderOnlyView", "PURCHASE_ORDER", New With {.id = dtTable.Rows(0).Item("PO_ID")})" target="_blank">@dtTable.Rows(0).Item("PO_NUMBER")</a></span> 
                            Else
                                @<span id = "txt_ponum"></span>
                            End If
                            @If (GM_Action = "Delete" Or GM_Action = "Approve") = False Then
                                @<a Class="red" href="#" onclick="ModalSearch('/Search/Index', @Json.Encode(JSONPODataTable), 'Search Purchase Order', 'txt_poid:Y|txt_ponum:N|PO_TYPE:N|txt_suppliername:N|txt_phone_supplier:Y|txt_address_supplier:Y|txt_contact_person_supplier:Y|txt_fax_supplier:Y|txt_deliveryname:N|txt_phone_delivery:Y|txt_address_delivery:N|txt_fax_delivery:N', '.dialogForm')" data-toggle="modal" title="Create">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>
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
                <h4 class="widget-title">Supplier</h4>
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
                                <span id="txt_cb_supplier_nm" hidden="hidden"></span>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Name </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_suppliername">
                                            @if dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("SUPPLIER_NAME")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_phone_supplier">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("SUPPLIER_NAME")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_address_supplier">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("SUPPLIER_PHONE")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Contact Person </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_contact_person_supplier">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("SUPPLIER_ADDTRSS")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_fax_supplier">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("CONTACT_PERSON")
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

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Delivery to</h4>
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
                                <span id="txt_cb_delivery_nm" hidden="hidden"></span>
                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> Name </div>
                                    <div class="profile-info-value item-required">
                                        <span Class="" id="txt_deliveryname">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("SUPPLIER_FAX")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Phone </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_phone_delivery">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("DELIVERY_NAME")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Address </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_address_delivery">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("DELIVERY_PHONE")
                                            End If
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Fax </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_fax_delivery">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("DELIVERY_FAX")
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
                            <th class="hidden"> Item Id </th>
                            <th> Item Description </th>
                            <th> Unit Measurement </th>
                            <th> Outstanding </th>
                            <th> Quantity </th>
                            <th class="hidden"> Price </th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody id="table_ap">
                        @For i = 0 To dtTableDetail.Rows.Count - 1
                        @<tr id="@i">
                            <td class="hidden" id=@(i.ToString() + "_0")>
                                <input size=5 type="text" id=@(i.ToString() + "0") readonly="readonly" value="@dtTableDetail.Rows(i).Item(0)" />
                            </td>
                            <td id=@(i.ToString() + "_1")>
                                <input style="width:auto" type="text" id=@(i.ToString() + "1") readonly="readonly" value="@dtTableDetail.Rows(i).Item(1)" />
                            </td>
                            <td id=@(i.ToString() + "_2")>
                                <input size=40 type="text" id=@(i.ToString() + "2") readonly="readonly" value="@dtTableDetail.Rows(i).Item(2)" />
                            </td>
                            <td id=@(i.ToString() + "_3")>
                                <input size=20 type="text" id=@(i.ToString() + "3") readonly="readonly" value="@dtTableDetail.Rows(i).Item(3)" />
                            </td>
                            <td id=@(i.ToString() + "_4")>
                                @If GM_Action = "Delete" Or GM_Action = "Approve" Then
                                    @<input size=20 type="text" readonly="readonly" id=@(i.ToString() + "4") value="@dtTableDetail.Rows(i).Item(4)"  />
                                Else
                                    @<input size=20 type="text" id=@(i.ToString() + "4") value="@dtTableDetail.Rows(i).Item(4)"  />
                                End If
                            </td>
                            <td class="hidden" id=@(i.ToString() + "_5")>
                                <input size=20 type="text" id=@(i.ToString() + "5") readonly="readonly" value="@dtTableDetail.Rows(i).Item(5)" />
                            </td>
                            <td>
                                <a class="red" href="#" onclick="DeleteRow(this)" data-toggle="modal" title="Delete Row">
                                    @Html.Raw(Labels.IconAction("Delete"))
                                </a>
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
                                    <div class="profile-info-name"> Appr/Completed By  </div>
                                    <div class="profile-info-value">
                                        <span Class="" id="txt_completed_by">
                                            @If dtTable.Rows.Count > 0 Then
                                                @dtTable.Rows(0).Item("COMPLETED_BY")
                                            End If
                                        </span>
                                    </div>
                                </div>
                                <div class="profile-info-row">
                                    <div class="profile-info-name"> Appr/Completed Dt </div>
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

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                <a id="BtnClosed" Class="red" href="#" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                @If GM_Action = "Create" Then
                    ViewBag.Title = "Create Good Match"
                @Html.Raw(Labels.ButtonForm("SaveSubmit"))
                ElseIf GM_Action = "Edit" Then
                    ViewBag.Title = "Edit Good Match"
                @Html.Raw(Labels.ButtonForm("SaveSubmit"))
                ElseIf GM_Action = "Approve" Then
                    ViewBag.Title = "Approve Good Match"
                @Html.Raw(Labels.ButtonForm("SaveApprove"))
                End If
                @If GM_Action = "Create" Then
                    ViewBag.Title = "Create Good Match"
                @Html.Raw(Labels.ButtonForm("SaveCreate"))
                ElseIf GM_Action = "Edit" Then
                    ViewBag.Title = "Edit Good Match"
                @Html.Raw(Labels.ButtonForm("SaveEdit"))
                ElseIf GM_Action = "Delete" Then
                    ViewBag.Title = "Delete Good Match"
                @Html.Raw(Labels.ButtonForm("SaveDelete"))
                ElseIf GM_Action = "Approve" Then
                    ViewBag.Title = "Approve Good Match"
                @Html.Raw(Labels.ButtonForm("Rejected"))
                ElseIf GM_Action = "Detail" Or GM_Action = "Rejected" Then
                    ViewBag.Title = "Detail Good Match"
                End If
            </div>
            @If GM_Action = "Approve" Then
                @<br />
                @<div Class="row top-left" id="">
                    <div Class="col-lg-12">
                        Reason of reject 
                        <textarea type = "text" style="height:100px"  Class="form-control input-group" id="txt_reason_reject" ></textarea>
                    </div>
                </div>
            End If
        </div>
    </div>
</div>

<script src="~/Scripts/jquery.tabletojson.js"></script>
<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/GMController.js"></script>

 <Script type = "text/javascript" >
     document.getElementById("BtnClosed").onclick = function () {
         FLAGFORM = $("#FLAGFORM").text().trim();
         if (FLAGFORM == "1") {
             document.getElementById("BtnClosed").href = linkProc + '/GM/Index'; 
         } else {
             document.getElementById("BtnClosed").href = linkProc + '/DASHBOARD/IndexJobLists?_FlagInbox=' + FLAGFORM;
         }
     };
</script>
