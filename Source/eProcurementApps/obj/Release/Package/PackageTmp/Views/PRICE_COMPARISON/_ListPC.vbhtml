@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PC)
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Price Comparison"
    ViewBag.Title = "List Price Comparison"
    ViewBag.PriceComparison = "active open"
    ViewBag.IndexListPriceCom = "active"
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

                            @code
                                @<div Class="space-4"></div>

                                Using Html.BeginForm("ListPcByStatus", "PRICE_COMPARISON", FormMethod.Post)
                                        @<div Class="profile-info-row">
                                            <div Class="profile-info-name"> Status </div>
                                            <div Class="profile-info-value">
                                                @Html.TextBox("txt_status_pc_val", "", New With {.class = "hidden"})
                                                @Html.DropDownList("dropdownList_status_pc", Dropdown.ListStatusPC)
                                                <Label id="required_txt_submitter"></Label>
                                                <button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                                                    <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                                                    Get Data
                                                </button>
                                            </div>
                                        </div>
                                End Using
                            End Code
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>PC Number</th>
                                        <th>Recommed Supp</th>
                                        <th>Grand Total</th>
                                        <th>Created Date</th>
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
                                                @item.PC_NUM 
                                            </td>
                                            <td>
                                                @item.RECOM_SUPPLIER_NM
                                            </td>
                                             <td align="right">
                                                @item.GRAND_TOTAL.ToString("###,###")
                                             </td>
                                            <td>
                                                @item.CREATED_TIME.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @item.STATUS
                                            </td>
                                            <td>
                                                @if item.LAST_MODIFIED_TIME IsNot Nothing Then
                                                    @item.LAST_MODIFIED_TIME.Value.ToString("d-MMM-yy")
                                                End If
                                            </td>
                                             <td>
                                                 <div class="hidden-sm hidden-xs action-buttons">
                                                     <a class="blue" href="@Url.Action("DetailPc", "PRICE_COMPARISON", New With {.id = item.ID, .flag = Convert.ToDecimal(ListEnum.ViewPageArea.Viewed)})" title="Detail">
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

<script src="~/Scripts/Controllers/PRICE_COMPARISONController.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Standard/StandardTable.js"></script>

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
    //$(function () {
    //    $('#dropdownList_status_pc').change(function () {
    //        var optionSelected = $(this).find('option:selected').attr('Value');
    //        $('#txt_status_pc_val').val(optionSelected);
    //    });
    //});
</script>