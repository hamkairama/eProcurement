@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_SUPPLIER)
@Imports eProcurementApps.Helpers

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-xs-12">
                <div Class="row">
                    <div Class="col-xs-12">
                        <div Class="clearfix">
                            <div Class="pull-right tableTools-container">
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'Supplier')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                        </div>

                        <div class="table-header">
                            <a class="red" href="#" onclick="ModalCreate('/Create/', '.dialogForm')" data-toggle="modal" title="Create">
                                @Html.Raw(Labels.ButtonForm("Create"))
                            </a>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <label>
                                <input name="form-field-radio" type="radio" class="ace" id="rb_legal_list" />
                                <span class="lbl">MVL</span>
                            </label>
                            <label>
                                <input name="form-field-radio" type="radio" class="ace" id="rb_ilegal_list" />
                                <span class="lbl">Non-MVL</span>
                            </label>
                            &nbsp; &nbsp; &nbsp;
                            @Html.Raw(Labels.ButtonForm("GetDataByRowStat"))
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr class="hidden">
                                        <th colspan="18">VENDOR'S INFORMATION</th>
                                        <th colspan="4">VENDOR'S BANK INFORMATION</th>
                                        <th colspan="7">AVAILABILITY OF DOCUMENTS</th>
                                    </tr>

                                    <tr>
                                        @*<th class="center">
                                            <label class="pos-rel">
                                                <input type="checkbox" class="ace" />
                                                <span class="lbl"></span>
                                            </label>
                                        </th>*@
                                        <th>Vendor Code</th>
                                        <th>Vendor Name</th>
                                        <th class="hidden">Alias</th>
                                        <th>Core Business</th>
                                        <th>B.U Owner</th>
                                        <th class="hidden">Nama Barang</th>
                                        <th>Cp</th>
                                        <th>Address</th>
                                        <th class="hidden">City</th>
                                        <th>Mobile</th>
                                        <th class="hidden">Email</th>
                                        <th class="hidden">Fax Number</th>
                                        <th class="hidden">Office</th>
                                        <th class="hidden">Tax Number</th>
                                        <th class="hidden">Website</th>
                                        <th class="hidden">Other Desc.</th>
                                        <th>Effective Date</th>
                                        <th>Schedule Evalution</th>
                                        <th class="hidden">Bank Name</th>
                                        <th class="hidden">Bank Branch</th>
                                        <th class="hidden">Bank Account</th>
                                        <th class="hidden">NPWP</th>
                                        <th class="hidden">Weight Factor / Qualification</th>
                                        <th class="hidden">Bridger Scan</th>
                                        <th class="hidden">NDA</th>
                                        <th class="hidden">CIDCI</th>
                                        <th class="hidden">Legal Documents</th>
                                        <th class="hidden">Agreement (If Any)</th>
                                        <th class="hidden">Vendors Validity Checking</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr>
                                            @*<td class="center">
                                                <label class="pos-rel">
                                                    <input type="checkbox" class="ace" />
                                                    <span class="lbl"></span>
                                                </label>
                                            </td>*@
                                            <td>
                                                @item.VENDOR_CODE
                                            </td>
                                            <td>
                                                @item.SUPPLIER_NAME
                                            </td>
                                            <td class="hidden">
                                                @item.SUPPLIER_ALIAS_NAME
                                            </td>
                                            <td>
                                                @item.CORE_BUSINESS
                                            </td>
                                            <td>
                                                @item.B_UNIT_OWNER
                                            </td>
                                            <td class="hidden">
                                                @item.NAMA_BARANG
                                            </td>
                                            <td>
                                                @item.CONTACT_PERSON
                                            </td>
                                            <td>
                                                @item.SUPPLIER_ADDRESS
                                            </td>
                                            <td class="hidden">
                                                @item.CITY
                                            </td>
                                            <td>
                                                @item.MOBILE_NUMBER
                                            </td>
                                            <td class="hidden">
                                                @item.EMAIL_ADDRESS
                                            </td>
                                            <td class="hidden">
                                                @item.FAX_NUMBER
                                            </td>
                                            <td class="hidden">
                                                @item.OFFICE_NUMBER
                                            </td>
                                            <td class="hidden">
                                                @item.TAX_NUMBER
                                            </td>
                                             <td class="hidden">
                                                 @item.WEBSITE
                                             </td>
                                             <td class="hidden">
                                                 @item.DESCRIPTION
                                             </td>
                                             <td>
                                                 @item.EFFECTIVE_DATE
                                             </td>
                                             <td>
                                                 @item.SCHEDULE_EVALUATION
                                             </td>
                                             <td class="hidden">
                                                 @item.BANK_NAME
                                             </td>
                                             <td class="hidden">
                                                 @item.BANK_BRANCH
                                             </td>
                                             <td class="hidden">
                                                 @item.BANK_ACCOUNT_NUMBER
                                             </td>
                                             <td class="hidden">
                                                 @item.NPWP
                                             </td>
                                             <td class="hidden" width="15">
                                                 @item.TPROC_SUPP_DOC(0).WEIGHT_FACTOR
                                             </td>
                                             <td class="hidden">
                                                 @item.TPROC_SUPP_DOC(0).BRIDGER_SCAN
                                             </td>
                                             <td class="hidden">
                                                 @item.TPROC_SUPP_DOC(0).NDA
                                             </td>
                                             <td class="hidden">
                                                 @item.TPROC_SUPP_DOC(0).CIDCI
                                             </td>
                                             <td class="hidden">
                                                 @item.TPROC_SUPP_DOC(0).LEGAL_DOC
                                             </td>
                                             <td class="hidden">
                                                 @item.TPROC_SUPP_DOC(0).AGGREEMENT
                                             </td>
                                             <td class="hidden">
                                                 @item.TPROC_SUPP_DOC(0).VALIDITY_CHECKING
                                             </td>
                                            <td>
                                                <div class="hidden-sm hidden-xs action-buttons">
                                                    <a class="blue" href="#" onclick="Modal('/Details/','@item.ID','.dialogForm')" data-toggle="modal" title="View">
                                                        @Html.Raw(Labels.IconAction("Details"))
                                                    </a>

                                                    <a class="green" href="#" onclick="Modal('/Edit/','@item.ID','.dialogForm')" data-toggle="modal" title="Edit">
                                                        @Html.Raw(Labels.IconAction("Edit"))
                                                    </a>

                                                    <a class="red" href="#" onclick="Modal('/Delete/','@item.ID','.dialogForm')" data-toggle="modal" title="Delete">
                                                        @Html.Raw(Labels.IconAction("Delete"))
                                                    </a>
                                                </div>
                                                <div class="hidden-md hidden-lg">
                                                    <div class="inline pos-rel">
                                                        <button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">
                                                            <i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>
                                                        </button>

                                                        <ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
                                                            <li>
                                                                <a class="blue" href="#" onclick="Modal('/Details/','@item.ID','.dialogForm')" data-toggle="modal" title="View">
                                                                    @Html.Raw(Labels.IconAction("Details"))
                                                                </a>
                                                            </li>

                                                            <li>
                                                                <a class="green" href="#" onclick="Modal('/Edit/','@item.ID','.dialogForm')" data-toggle="modal" title="Edit">
                                                                    @Html.Raw(Labels.IconAction("Edit"))
                                                                </a>
                                                            </li>

                                                            <li>
                                                                <a class="red" href="#" onclick="Modal('/Delete/','@item.ID','.dialogForm')" data-toggle="modal" title="Delete">
                                                                    @Html.Raw(Labels.IconAction("Delete"))
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    Next
                                </tbody>
                            </table>
                            <hr />

                            <div class="frow">
                                @Using (Html.BeginForm("Upload", "SUPPLIER", FormMethod.Post, New With {.enctype = "multipart/form-data"}))

                                    @<div class="col-xs-6">
                                        <input type="file" id="id-input-file-2" name="UploadedFile" />
                                    </div>
                                    @<div class="col-xs-3">
                                        @Html.Raw(Labels.ButtonForm("Upload"))
                                    </div>
                                End Using
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Standard/StandardTable.js"></script>

<script type="text/javascript">
    //initiate dataTables plugin
    var oTable1 =
    $('#dynamic-table')
    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
    .dataTable({
        bAutoWidth: false,
        "aoColumns": [
          null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
          { "bSortable": false }
        ],
        "aaSorting": [],

        //,
        //"sScrollY": "200px",
        //"bPaginate": false,

        "sScrollX": "100%",
        //"sScrollXInner": "120%",
        //"bScrollCollapse": true,
        //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

        //"iDisplayLength": 50
    });
</script>
<script>
    jQuery(function ($) {
        $('#id-input-file-2').ace_file_input({
            no_file: 'No File ...',
            btn_choose: 'Choose',
            btn_change: 'Change',
            droppable: false,
            onchange: null,
            thumbnail: false //| true | large
            //whitelist:'gif|png|jpg|jpeg'
            //blacklist:'exe|php'
            //onchange:''
            //
        });
    });
</script>


