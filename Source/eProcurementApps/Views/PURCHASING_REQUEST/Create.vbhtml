@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_FORM_SUBTYPE_DT)
@Imports eProcurementApps.Helpers

<link href="~/Ace/fileInput/fileinput.css" rel="stylesheet" />
<link href="~/Ace/fileInput/theme.css" rel="stylesheet" />
<script src="~/Ace/fileInput/fileinput.js"></script>
<style>
    .table-striped > tbody > tr:nth-child(odd) > td,
    .table-striped > tbody > tr:nth-child(odd) > th {
        white-space: nowrap;
    }

    #table_input {
        overflow-x: scroll;
        max-width: 100%;
        display: block;
        white-space: nowrap;
    }
</style>

@Code
    ViewBag.Breadcrumbs = "Purchasing Request"
    ViewBag.Title = "Create PR"
    ViewBag.PurchasingRequest = "active open"
    ViewBag.IndexCreatePR = "active"
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

@*@If ViewBag.FailedMessage IsNot Nothing Then
    @<div Class="alert alert-danger">
        <Button type="button" Class="close" data-dismiss="alert">
            <i Class="ace-icon fa fa-times"></i>
        </Button>
        @ViewBag.FailedMessage
        <br />
    </div>
End If*@


<div Class="main-container" id="main-container">
    <div Class="main-content">
        <div Class="main-content-inner">
            <div Class="page-content">
                <div Class="row">
                    <Table>
                        <tr>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Request No : </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_request_no"></span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name required"> Form Type </div>
                                        <div class="profile-info-value item-required">
                                            <span id="txt_cb_form_type_id" class="hidden"></span>
                                            @Html.DropDownList("dropdownList_ft", Dropdown.FormType, New With {.style = "width:  200px;"})
                                            <label id="required_txt_cb_form_type_id"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Delivery Days : </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_delivery_days"></span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row ">
                                        <span Class="hidden" id="txt_is_popup_coa"></span>
                                        <div class="profile-info-name required"> Account Code</div>
                                        <div class="profile-info-value item-required">
                                            <span Class="hidden" id="txt_count_code"></span>                                            
                                            <select id="dropdownList_fst_bc" name="dropdownList_fst_bc" style="width: 200px">
                                                <option>
                                                    -select-
                                                </option>
                                            </select>
                                            <label id="required_txt_count_code"></label>
                                        </div>
                                    </div>
                                </div>
                            </td>

                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> User Name : </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_user_name">@Session("USER_NAME")</span>
                                            <span Class="hidden" id="txt_user_id">@Session("USER_ID")</span>
                                            <span Class="hidden" id="txt_user_id_id">@Session("USER_ID_ID")</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name required"> Sub Type </div>
                                        <div class="profile-info-value item-required">
                                            <span id="txt_cb_sub_type_id" class="hidden"></span>
                                            <select id="dropdownList_fst" name="dropdownList_fst" style="width: 200px">
                                                <option>
                                                    -select-
                                                </option>
                                            </select>
                                            <label id="required_txt_cb_sub_type_id"></label>

                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Exp. Dev Dt : </div>
                                        <div class="profile-info-value">
                                            <label hidden="hidden" id="txt_dev_dt_orin"></label>
                                            <span Class="dateText" id="txt_dev_dt_new"></span>
                                            <label id="required_txt_dev_dt_new"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Status : </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_status"></span>
                                        </div>
                                    </div>
                                </div>
                            </td>

                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Request Date : </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_request_dt">@Date.Now.ToString("dd-MM-yyyy") </span>
                                            <label id="required_txt_request_dt"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Good type </div>
                                        <div class="profile-info-value">
                                            <span id="txt_cb_gt_id" class="hidden"></span>
                                            @Html.DropDownList("dropdownList_gt", Dropdown.GoodType, New With {.style = "width:200px;"})
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Req. Indicator : </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_req_indicator"></span>
                                        </div>
                                    </div>

                                    @If Session("IS_EPROC_ADMIN") = 1 Then
                                        @<div Class="profile-info-row">
                                            <div Class="profile-info-name"> For Storage </div>
                                            <div Class="profile-info-value">
                                                <input type="checkbox" id="cb_for_storage" />
                                                <span Class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be pr storage and will be created po, good match, etc">?</span>
                                            </div>
                                        </div>
                                    End If

                                </div>
                            </td>
                        </tr>
                    </Table>

                    <div Class="space-4"></div>

                    <div Class="clearfix">
                        <div Class="pull-right tableTools-container"></div>
                    </div>

                    <div Class="table-header" style="color:white">
                        Input Item
                    </div>
                    <div>
                        <Table id="table_input" Class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th Class="center">
                                        <Label Class="pos-rel">
                                            <input type="checkbox" Class="ace" />
                                            <span Class="lbl"></span>
                                        </Label>
                                    </th>
                                    <th Class="required">Item Name (Max. 50 char)</th>
                                    <th> Specification</th>
                                    <th Class="required">Currency</th>
                                    <th Class="required">Qty</th>
                                    <th> Measurement</th>
                                    <th Class="required">WA</th>
                                    <th> WA Origin</th>
                                    <th> Revise Qty</th>
                                    <th Class="required">Price</th>
                                    <th> Total Price</th>
                                    <th> Remark</th>
                                    <th> SuppName</th>
                                    <th style="display:none"> IDwa</th>
                                    <th style="display:none"> Item Cd</th>
                                    <th style=""> Conv(RP)</th>
                                    <th style="display:none"> Item Id</th>
                                    <th style="display:none"> Qty storage</th>
                                    <th Class="required">Approval</th>
                                    <th style="color:red"> Action</th>
                                </tr>
                            </thead>

                            <tbody id="dataTable">
                                <tr>
                                    <td class="center">
                                        <label class="pos-rel">
                                            <input type="checkbox" class="ace" id="cb_item_" />
                                            <span class="lbl"></span>
                                        </label>
                                    </td>
                                    <td>
                                        <input size=20 type="text" id="txt_item_name_" onclick="getItem(this)" />
                                    </td>

                                    <td><input size=18 type="text" class="toolTips" id="txt_specification_" /></td>
                                    <td>@Html.DropDownList("txt_currency_", Dropdown.Currency(), New With {.onchange = "getConvertionCurr(this)"})</td>
                                    <td><input size=5 type="text" id="txt_qty_" onkeyup="CalcItem(this)" /></td>
                                    <td><input size=10 type="text" id="txt_unit_measurement_" readonly="readonly" /></td>
                                    <td>@Html.DropDownList("dropdownList_wa_", Dropdown.WorkArea(Session("USER_ID_ID")), New With {.onchange = "getWaOrigin(this)"})</td>
                                    <td><input size=5 type="text" id="wa_orin_" readonly="readonly" /></td>
                                    <td><input size=5 type="text" id="txt_revise_qty_" readonly="readonly" /></td>
                                    <td><input size=10 type="text" id="txt_price_" readonly="readonly" style="text-align:right" onkeyup="CalcItemByPrice(this)" /></td>
                                    <td><input size=10 type="text" id="txt_total_price_" readonly="readonly" style="text-align:right" /></td>
                                    <td><input size=10 type="text" id="txt_remark_" /></td>
                                    <td><input size=15 type="text" id="txt_supp_name_" readonly="readonly" /></td>
                                    <td style="display:none"><input size=2 type="text" id="txt_id_wa_" readonly="readonly" /></td>
                                    <td style="display:none"><input size=10 type="text" id="txt_item_cd_" readonly="readonly" /></td>
                                    <td style=""><input size=10 type="text" id="txt_convertion_" readonly="readonly" value="@CommonFunction.GetDefaultCurrencyIDRConvertion()" /></td>
                                    <td style="display:none"><input size=10 type="text" id="txt_item_id_" readonly="readonly" /></td>
                                    <td style="display:none"><input size=10 type="text" id="txt_qty_storage_" readonly="readonly" /></td>
                                    <td><input size=10 type="text" id="txt_approval_pr_" readonly="readonly" /></td>
                                    <td>
                                        <a class="red" href="#" onclick='DeleteRowPRItem(this)' title="Remove row">
                                            @Html.Raw(Labels.IconAction("Delete"))
                                        </a>
                                    </td>
                                </tr>
                            </tbody>

                        </Table>

                        @Html.Raw(Labels.ButtonForm("AddRowItem"))
                    </div>

                    <div class="hr hr-18 dotted hr-double"></div>

                    <div class="row">
                        <div class="col-xs-8">
                            <div>
                                Note  &nbsp;&nbsp;&nbsp;&nbsp; <span class="required"></span> : <b>field must be filled</b> &nbsp;&nbsp;&nbsp;&nbsp;<span class="red"> @Html.Raw(Labels.IconAction("Delete"))</span> : for <b>delete row </b>
                            </div>
                        </div>

                        <div class="col-xs-2" style="text-align:right">
                            Total Price :
                        </div>

                        <div class="col-xs-2" style="font-weight:bold">
                            <div>
                                Rp. <span id="txt_sub_total_price"></span>
                            </div>
                        </div>
                    </div>
                    <div class="hr hr-18 dotted hr-double"></div>


                    <div class="space-4"></div>

                    <div class="alert alert-danger" id="msg_error" style="display:none">
                        <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
                            <i class="ace-icon fa fa-times"></i>
                        </button>
                        Error :
                        <br />
                        msg error
                        <br />
                    </div>

                    <b>Related Department</b>
                    @Html.Partial("_ListRelDept")

                    <div class="space-4"></div>

                    @*<b>File Attachment</b>
                    <form enctype="multipart/form-data">
                        <div class="form-group" style="width:500px">
                            <input id="file-1" type="file" multiple class="file" data-overwrite-initial="true" data-min-file-count="1">
                        </div>
                        <hr>
                    </form>*@

                    <b>Information Budget</b>
                    <Table>
                        <tr>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Usage Code </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_usage_code"></span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Budget Cd Start </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_budget_code_start"></span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td width="500px">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Budget Cd End </div>
                                        <div class="profile-info-value">
                                            <span Class="" id="txt_budget_code_end"></span>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </Table>

                    <div class="hr hr-18 dotted hr-double"></div>

                    <div class="clearfix form-action">
                        <div class="col-lg-12">
                            <div class="modal-footer no-margin-top">
                                <div class="" id="loader"></div>
                                <a class="red" href="@Url.Action("Create", "PURCHASING_REQUEST")" title="Close">
                                    @Html.Raw(Labels.ButtonForm("Close"))
                                </a>
                                <a class="red" href="#" onclick="ChooseOneApproval()" title="Choose One Approval">
                                    @Html.Raw(Labels.ButtonForm("ChooseOneApproval"))
                                </a>
                                @Html.Raw(Labels.ButtonForm("SubmitPRWithChooseOneApproval"))
                                @*@Html.Raw(Labels.ButtonForm("Submit"))*@
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src="~/Scripts/Controllers/PURCHASING_REQUESTController.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>

<script>
    $("#file-1").fileinput({
        uploadUrl: '#', // you must set a valid URL here else you will get an error
        //allowedFileExtensions: ['jpg', 'png', 'gif'],
        overwriteInitial: false,
        maxFileSize: 1000,
        maxFilesNum: 10,
        //allowedFileTypes: ['image', 'video', 'flash'],
        slugCallback: function (filename) {
            return filename.replace('(', '_').replace(']', '_');
        }
    });

</script>