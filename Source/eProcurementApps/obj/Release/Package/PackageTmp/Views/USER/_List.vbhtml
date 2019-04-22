@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_USER)
@Imports eProcurementApps.Helpers

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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'User')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                        </div>

                        <div class="table-header">
                            <a class="red" href="@Url.Action("Create", "USER", New With {.flag = 0})" title="Create">
                                @Html.Raw(Labels.ButtonForm("Create"))
                            </a>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <label>
                                <input name="form-field-radio" type="radio" class="ace" id="rb_active" />
                                <span class="lbl">Active</span>
                            </label>
                            <label>
                                <input name="form-field-radio" type="radio" class="ace" id="rb_hibernate" />
                                <span class="lbl">Hibernate</span>
                            </label>
                            <label>
                                <input name="form-field-radio" type="radio" class="ace" id="rb_inactive" />
                                <span class="lbl">In-Active</span>
                            </label>
                            &nbsp; &nbsp; &nbsp;
                            @Html.Raw(Labels.ButtonForm("GetDataByRowStat"))
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
                                        <th>UserId</th>
                                        <th>Email</th>
                                        <th>Name</th>
                                        <th>Super Adm</th>
                                        <th>Eproc Adm</th>
                                        <th>Comp</th>
                                        <th>Role</th>
                                        <th>Division</th>
                                        <th>WA</th>
                                        <th class="hidden">Created By</th>
                                        <th class="hidden">Created Time</th>
                                        <th class="hidden">Last Modified Time</th>
                                        <th class="hidden">Last Modified By</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody id="dataTable">
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
                                            @item.USER_ID
                                        </td>
                                        <td>
                                            @item.USER_MAIL
                                        </td>
                                        <td>
                                            @item.USER_NAME
                                        </td>

                                        @code
                                            If item.TPROC_USER_DT.IS_SUPER_ADMIN = 1 Then
                                                @<td>Yes</td>
                                            Else
                                                @<td>No</td>
                                            End If
                                        End Code

                                        @code
                                            If item.TPROC_USER_DT.IS_EPROC_ADMIN = 1 Then
                                                @<td>Yes</td>
                                            Else
                                                @<td>No</td>
                                            End If
                                        End Code

                                        @code
                                            If item.TPROC_USER_DT.COMP_CD = 0 Then
                                                @<td>eProc</td>
                                            Else
                                                @<td>MAMI</td>
                                            End If
                                        End Code

                                        <td>
                                            @item.TPROC_USER_DT.TPROC_ROLE.ROLE_NAME
                                        </td>

                                        <td>
                                            @item.TPROC_USER_DT.TPROC_DIVISION.DIVISION_NAME
                                        </td>

                                        <td>
                                            @code
                                                Dim wa_result As String = ""
                                                For Each x In item.TPROC_USER_DT.TPROC_WA_ALLOWED_GR.TPROC_WA_ALLOWED_DT.ToList()
                                                    wa_result = wa_result + x.TPROC_WA.WA_NUMBER.ToString() + "; "
                                                Next

                                                @wa_result
                                            End Code
                                        </td>

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
                                                <a class="blue" href="@Url.Action("Details", "USER", New With {.id = item.ID})" title="Details">
                                                    @Html.Raw(Labels.IconAction("Details"))
                                                </a>
                                                <a class="green" href="@Url.Action("Edit", "USER", New With {.id = item.ID, .flag = 0})" title="Edit">
                                                    @Html.Raw(Labels.IconAction("Edit"))
                                                </a>
                                                <a class="red" href="@Url.Action("Delete", "USER", New With {.id = item.ID, .flag = 0})" title="Delete">
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
                                                            <a class="blue" href="@Url.Action("Details", "USER")" title="Create">
                                                                @Html.Raw(Labels.IconAction("Details"))
                                                            </a>
                                                        </li>

                                                        <li>
                                                            <a class="green" href="@Url.Action("Edit", "USER", New With {.id = item.ID, .flag = 0})" title="Edit">
                                                                @Html.Raw(Labels.IconAction("Edit"))
                                                            </a>
                                                        </li>

                                                        <li>
                                                            <a class="red" href="@Url.Action("Delete", "USER", New With {.id = item.ID, .flag = 0})" title="Delete">
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
                            <button class="btn btn-mini btn-danger btn-sm" onclick="DeleteSelected()" title="Delete selected">
                                <i class="ace-icon fa fa-trash-o bigger-200"></i>
                            </button>

                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            @Html.Raw(Labels.ButtonForm("UpdateHibernate"))

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
          null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
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



