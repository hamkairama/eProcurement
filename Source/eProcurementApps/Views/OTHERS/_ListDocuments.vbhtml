@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_DOCUMENTS)
@Imports eProcurementApps.Helpers

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            <div Class="col-xs-12">
                <div Class="row">
                    <div Class="col-xs-12">
                        <div Class="clearfix">
                            @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
                            <div class="frow">
                                @Using (Html.BeginForm("InsertDocuments", "OTHERS", FormMethod.Post, New With {.enctype = "multipart/form-data"}))

                                    @<div class="col-xs-6">
                                        <input type="file" id="id-input-file-2" name="UploadedFile" />
                                    </div>
                                    @<div class="col-xs-3">
                                        @Html.Raw(Labels.ButtonForm("Upload"))
                                    </div>
                                End Using
                            </div>

                            <div Class="pull-right tableTools-container">
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'Currency')">
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
                                        <th>No</th>
                                        <th>File</th>
                                        <th>Created Time</th>
                                        <th>Created By</th>
                                        <th>Last Modified Time</th>
                                        <th>Last Modified By</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @code
                                        Dim no_item As Integer = 1
                                    @For Each item In Model
                                    @<tr>
                                        <td>
                                            @no_item
                                        </td>
                                        <td>
                                            @item.FILE_NAME
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
                                        <td>
                                            <a class="green" href="@Url.Action("Download", "OTHERS", New With {.id = item.ID})" title="Download">
                                                @Html.Raw(Labels.IconAction("Download"))
                                            </a>

                                            @if Session("IS_EPROC_ADMIN") = 1 Then
                                                 @<a Class="red" href="#" onclick="ModalPop('@CommonFunction.GetLinkEproc/OTHERS/Delete/','@item.ID','.dialogForm')" data-toggle="modal" title="Delete">
                                                    @Html.Raw(Labels.IconAction("Delete"))
                                                </a>
                                            End If
                                           
                                        </td>
                                    </tr>
                                        no_item += 1
                                    Next
                                    End Code
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

