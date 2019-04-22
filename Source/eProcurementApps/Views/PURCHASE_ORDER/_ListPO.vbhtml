@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PO_HEADERS)
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Purchase Order"
    ViewBag.Title = "List Purchase Order"
    ViewBag.PurchaseOrder = "active open"
    ViewBag.IndexListPO = "active"
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

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-xs-12">
                <div Class="row">
                    <div Class="col-xs-12">
                        <div Class="clearfix">
                            <div Class="pull-right tableTools-container">
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'ListPR')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>

                            <div Class="space-4"></div>
                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Status </div>
                                <div Class="profile-info-value">
                                    @Html.TextBox("txt_status_po_val", "", New With {.class = "hidden"})
                                    @Html.DropDownList("dropdownList_status_po", Dropdown.ListStatusPO)
                                    <Label id="required_txt_submitter"></Label>
                                    <button type='submit' onclick="GetData()" class='btn btn-sm btn-success btn-white btn-round'>
                                        <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                                        Get PO
                                    </button>
                                </div>
                                            
                                    <a class="blue" href="#" onclick="add_filter()" title="Add filter base on date">
                                        <i class='ace-icon fa fa-arrow-circle-down bigger-130'></i>
                                    </a>
                            </div>

                            <div style = "display:none" id="add_filter">
                                <div Class="profile-info-row">                                            
                                        <div Class="profile-info-name"> Last Modified </div>
                                        <div Class="profile-info-value">
                                            <span Class="dateText" id="txt_date_from" ></span>
                                        </div>

                                        <div Class="profile-info-name"> To </div>
                                        <div Class="profile-info-value">
                                            <span Class="dateText" id="txt_date_to"></span>
                                        </div>
                                    </div>
                            </div>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>PO Number</th>
                                        <th>Tipe</th>
                                        <th>Created Date</th>
                                        <th>Created By</th>
                                        <th>Status</th>
                                        <th>Last Modified</th>
                                        <th style="color:red">
                                            View
                                        </th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr>
                                            <td>
                                                @item.PO_NUMBER 
                                            </td>
                                            <td>
                                                @item.TPROC_PO_TYPE.PO_TYPE_NAME
                                            </td>
                                            <td align="right">
                                                @item.CREATE_DATE.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @item.CREATED_BY
                                            </td>
                                            <td>
                                                @code
                                                    Dim status As String = [Enum].GetName(GetType(ListEnum.PO), Int32.Parse(item.PO_STATUS)).ToString()
                                                    @<span id="txt_po_status_changed">@status</span>
                                                End Code
                                            </td>
                                            <td>
                                                @if item.LAST_MODIFIED_TIME IsNot Nothing Then
                                                    @item.LAST_MODIFIED_TIME.Value.ToString("d-MMM-yy")
                                                End If                                                
                                            </td>
                                             <td>
                                                 <div class="hidden-sm hidden-xs action-buttons">
                                                     <a class="blue" href="@Url.Action("DetailHeader", "PURCHASE_ORDER", New With {.id = item.ID})" title="Detail">
                                                         @Html.Raw(Labels.IconAction("Details"))
                                                     </a>
                                                 </div>
                                             </td>
                                        </tr>
                                                    Next
                                </tbody>
                            </table>



                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Standard/StandardTable.js"></script>
<script src="~/Scripts/Controllers/PURCHASING_ORDER_DETAILController.js"></script>

<script type="text/javascript">
    // initiate dataTables plugin
    var oTable1 =
    $('#dynamic-table')
    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
    .dataTable({
        bAutoWidth: false,
        "aoColumns": [
          { "bSortable": false },
          null, null, null, null, null,
          { "bSortable": false }
        ],
        "aaSorting": [],

        //,
        //"sScrollY": "200px",
        //"bPaginate": false,

        //"sScrollX" "100%",
        //"sScrollXInner": "120%",
        //"bScrollCollapse": true,
        //Note: If you Then are applying horizontal scrolling (sScrollX) On a ".table-bordered"
        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

        //"iDisplayLength" 50
    });
</script>


<script>
    $(function () {
        $('#dropdownList_status_po').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_status_po_val').val(optionSelected);
        });
    });

    function add_filter() {
            $("#add_filter").toggle(500);
    }

   

</script>