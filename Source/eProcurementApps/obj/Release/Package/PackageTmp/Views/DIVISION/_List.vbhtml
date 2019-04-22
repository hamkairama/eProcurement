@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_DIVISION)
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'Division')">
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
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Division</th>
                                        <th>Description</th>
                                        <th>Created Time</th>
                                        <th>Created By</th>
                                        <th>Last Modified Time</th>
                                        <th>Last Modified By</th>
                                        <th class="hidden"></th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr>
                                            <td>
                                                @item.DIVISION_NAME
                                            </td>
                                            <td>
                                                @item.DIVISION_DESCRIPTION
                                            </td>
                                            <td align="right">
                                                @item.CREATED_TIME.ToString("d-MMM-yy HH:mm")
                                            </td>
                                            <td>
                                                @item.CREATED_BY
                                            </td>
                                            <td align="right">
                                                @If item.LAST_MODIFIED_TIME.HasValue Then @item.LAST_MODIFIED_TIME.Value.ToString("d-MMM-yy HH:mm") End If
                                            </td>
                                            <td>
                                                 @item.LAST_MODIFIED_BY
                                            </td>
                                            <td class="hidden"></td>
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
          null, null, null, null, null, null, null,
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



