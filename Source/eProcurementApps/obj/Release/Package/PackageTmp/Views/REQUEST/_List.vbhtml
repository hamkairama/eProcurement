@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_REQUEST)
@Imports eProcurementApps.Helpers

@If ViewBag.Message <> "" Then
    @<div Class="alert alert-block alert-error">
        <Button Class="close" data-dismiss="alert" type="button">
            <i Class="icon-remove"></i>
        </Button>
        <i Class="icon-warning-sign red"></i>
        @ViewBag.Message
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'Request')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Request No.</th>
                                        <th>Request By</th>
                                        <th>Control Menu</th>
                                        <th>User Activity</th>
                                        <th>Created Time</th>
                                        <th>Created By</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr class='clickable-row' data-href="@Url.Action("RequestApproveComplete", item.CONTROL, New With {.reqno = item.REQUEST_NO, .rel_flag = item.RELATION_FLAG, .control = item.CONTROL, .actions = item.ACTION, .data_flag = Convert.ToDecimal(item.STATUS), .access_from = "0"})">
                                            <td>
                                                @item.REQUEST_NO
                                            </td>
                                            <td>
                                                @item.REQUEST_BY
                                            </td>
                                             <td>
                                                 @item.CONTROL
                                             </td>
                                             <td>
                                                 @item.ACTION
                                             </td>
                                            <td align="right">
                                                @item.CREATED_TIME.ToString("d-MMM-yy HH:mm")
                                            </td>
                                            <td>
                                                @item.CREATED_BY
                                            </td>
                                            <td>
                                                <div class="hidden-sm hidden-xs action-buttons">
                                                    <a Class="blue" href="@Url.Action("RequestApproveComplete", item.CONTROL, New With {.reqno = item.REQUEST_NO, .rel_flag = item.RELATION_FLAG, .control = item.CONTROL, .actions = item.ACTION, .data_flag = Convert.ToDecimal(item.STATUS), .access_from = "0"})" title="Complete">
                                                       @if ViewBag.IndexList = ListEnum.Request.NeedApprove Then
                                                        @Html.Raw(Labels.IconAction("Approve"))
                                                       ElseIf ViewBag.IndexList = ListEnum.Request.NeedComplete Then
                                                        @Html.Raw(Labels.IconAction("Complete"))
                                                       End If                                                         
                                                    </a>
                                                </div>
                                                <div class="hidden-md hidden-lg">
                                                    <div class="inline pos-rel">
                                                        <button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">
                                                            <i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>
                                                        </button>

                                                        <ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
                                                            <li>
                                                                <a Class="blue" href="@Url.Action("RequestApproveComplete" + item.CONTROL, item.CONTROL, New With {.reqno = item.REQUEST_NO, .rel_flag = item.RELATION_FLAG, .control = item.CONTROL, .actions = item.ACTION, .data_flag = Convert.ToDecimal(item.STATUS), .access_from = "0"})" title="Complete">
                                                                    @if ViewBag.IndexList = ListEnum.Request.NeedApprove Then
                                                                        @Html.Raw(Labels.IconAction("Approve"))
                                                                    ElseIf ViewBag.IndexList = ListEnum.Request.NeedComplete Then
                                                                        @Html.Raw(Labels.IconAction("Complete"))
                                                                    End If                                                                    
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
          { "bSortable": false },
          null, null, null, null, null,
          { "bSortable": false }
        ],
        "aaSorting": [],

        //,
        //"sScrollY": "200px",
        //"bPaginate": false,

        //"sScrollX": "100%",
        //"sScrollXInner": "120%",
        //"bScrollCollapse": true,
        //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

        //"iDisplayLength": 50
    });
</script>

<script>
    jQuery(document).ready(function ($) {
        $(".clickable-row").click(function () {
            window.location = $(this).data("href");
        });
    });
</script>

