@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_WA)
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'WorkArea')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                        </div>

                        <div class="table-header">
                            <a class="red" href="@Url.Action("Create", "WA", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Create">
                                @Html.Raw(Labels.ButtonForm("Create"))
                            </a>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th style="display:none"></th>
                                        <th class="center">
                                            <label class="pos-rel">
                                                <input type="checkbox" class="ace" />
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th>Wa No</th>
                                        <th>Department</th>
                                        <th>Division</th>
                                        <th class="">Approval</th>
                                        <th class="hidden">Created Time</th>
                                        <th class="hidden">Created By</th>
                                        <th class="hidden">Last Modified Time</th>
                                        <th class="hidden">Last Modified By</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr>
                                            <td style="display:none"></td>
                                            <td class="center">
                                                <label class="pos-rel">
                                                    <input type="checkbox" class="ace" id=" @item.ID" />
                                                    <span class="lbl"></span>
                                                </label>
                                            </td>
                                            <td>
                                                @item.WA_NUMBER
                                            </td>
                                            <td>
                                                @item.TPROC_APPROVAL_GR.DEPARTMENT_NAME
                                            </td>
                                            <td>
                                                @item.TPROC_APPROVAL_GR.TPROC_DIVISION.DIVISION_NAME
                                            </td>
                                            @code
                                                Dim appr As String = ""
                                                @For Each itemx In item.TPROC_APPROVAL_GR.TPROC_APPROVAL_DT
                                                    appr = appr + itemx.USER_NAME + "(" + itemx.TPROC_LEVEL.INDONESIAN_LEVEL + ")" + ";  "
                                                Next
                                                @<td class="">
                                                    @appr
                                                </td>
                                            End Code
                                            <td class="hidden" align="right">
                                                @item.CREATED_TIME.ToString("d-MMM-yy HH:mm")
                                            </td>
                                            <td class="hidden">
                                                @item.CREATED_BY
                                            </td>
                                            <td class="hidden" align="right">
                                                @If item.LAST_MODIFIED_TIME.HasValue Then @item.LAST_MODIFIED_TIME.Value.ToString("d-MMM-yy HH:mm") End If
                                            </td>
                                            <td class="hidden">
                                                @item.LAST_MODIFIED_BY
                                            </td>
                                            <td>
                                                <div class="hidden-sm hidden-xs action-buttons">
                                                    <a class="blue" href="#" onclick="Modal('/Details/','@item.ID','.dialogForm')" data-toggle="modal" title="View">
                                                        @Html.Raw(Labels.IconAction("Details"))
                                                    </a>
                                                    <a class="green" href="@Url.Action("Edit", "WA", New With {.id = item.ID, .flag = Convert.ToDecimal(ViewBag.flag)})" title="Edit">
                                                        @Html.Raw(Labels.IconAction("Edit"))
                                                    </a>
                                                    <a class="red" href="@Url.Action("Delete", "WA", New With {.id = item.ID, .flag = Convert.ToDecimal(ViewBag.flag)})" title="Delete">
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
                                                                <a class="green" href="@Url.Action("Edit", "WA", New With {.id = item.ID, .flag = Convert.ToDecimal(ViewBag.flag)})" title="Edit">
                                                                    @Html.Raw(Labels.IconAction("Edit"))
                                                                </a>
                                                            </li>

                                                            <li>
                                                                <a class="red" href="@Url.Action("Delete", "WA", New With {.id = item.ID, .flag = Convert.ToDecimal(ViewBag.flag)})" title="Delete">
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
                            @if ViewBag.flag = 0 Then
                                @<Button Class="btn btn-mini btn-danger btn-sm" onclick="DeleteSelected()" title="Delete selected">
                                    <i Class="ace-icon fa fa-trash-o bigger-200"></i>
                                </Button>
                            End If
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
          null, null, null, null, null, null, null, null, null,
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


