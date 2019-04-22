@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.APPROVAL_ROLEController 
 
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'ApprovalRole')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>
                        </div>

                        <div class="table-header">
                            <a class="red" href="@Url.Action("FormInput", "Approval_Role")" title="Create">
                                @Html.Raw(Labels.ButtonForm("Create"))
                            </a>
                        </div>

                        <div>
                            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th style="display:none"></th> 
                                        <th class="hidden">ID</th>
                                        <th>User ID</th>
                                        <th>User Name</th>
                                        <th>Email</th>
                                        <th>Role Name</th> 
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For i As Integer = 0 To dtTable.Rows.Count - 1
                                        @<tr>
                                            <td style="display:none"></td> 
                                            <td class="hidden">
                                                @dtTable.Rows(i).Item("ID")
                                            </td>
                                            <td>
                                                @dtTable.Rows(i).Item("USER_ID")
                                            </td> 
                                            <td>
                                                @dtTable.Rows(i).Item("USER_NAME")
                                            </td> 
                                            <td>
                                                @dtTable.Rows(i).Item("EMAIL")
                                            </td> 
                                            <td>
                                                @dtTable.Rows(i).Item("INDONESIAN_LEVEL")
                                            </td> 
                                            <td>
                                                <div class="hidden-sm hidden-xs action-buttons">
                                                    <a class="green" href="@Url.Action("FormInput", "APPROVAL_ROLE")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Edit" title="Edit">
                                                        @Html.Raw(Labels.IconAction("Edit"))
                                                    </a>
                                                    <a class="red" href="@Url.Action("FormInput", "APPROVAL_ROLE")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Delete" title="Delete">
                                                        @Html.Raw(Labels.IconAction("Delete"))
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

<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Standard/StandardTable.js"></script>

<script type="text/javascript">
    //initiate dataTables plugin
    var oTable1 =
    $('#dynamic-table')
    .dataTable({
        bAutoWidth: false,
        "aaSorting": [],
    });
</script>  