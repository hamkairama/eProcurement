@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.GMController 

@Code
    ViewBag.Breadcrumbs = "Good Match"
    ViewBag.Title = "Good Match List"
    ViewBag.Matching = "active open"
    ViewBag.IndexGoodMatch = "active"
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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'CRV')">
                                    <span>
                                        <i class="fa fa-file-excel-o bigger-110 green"></i>
                                    </span>
                                </button>
                            </div>

                            <div Class="space-4"></div>
                            <div Class="profile-info-row">
                                <div Class="profile-info-name"> Status </div>
                                <div Class="profile-info-value">
                                    @Html.TextBox("txt_status_gm_val", "", New With {.class = "hidden"})
                                    @Html.DropDownList("dropdownList_status_gm", Dropdown.ListStatusGm)
                                    <Label id="required_txt_submitter"></Label>
                                    <button type='submit' onclick="GetData()" class='btn btn-sm btn-success btn-white btn-round'>
                                        <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                                        Get GM
                                    </button>
                                </div>

                                <a class="blue" href="#" onclick="add_filter()" title="Add filter base on date">
                                    <i class='ace-icon fa fa-arrow-circle-down bigger-130'></i>
                                </a>
                            </div>

                            <div style="display:none" id="add_filter">
                                <div Class="profile-info-row">
                                    <div Class="profile-info-name"> Last Modified </div>
                                    <div Class="profile-info-value">
                                        <span Class="dateText" id="txt_date_from"></span>
                                    </div>

                                    <div Class="profile-info-name"> To</div>
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
                                        <th style="display:none"></th> 
                                        <th class="hidden">ID</th>
                                        <th>GM Number</th>
                                        <th>GM Date</th>
                                        <th>PO Number</th>
                                        <th>Supplier Name</th> 
                                        <th>Delivery Name</th> 
                                        <th>Status</th> 
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody> 
                                    @If dtTable IsNot Nothing Then
                                        @For i As Integer = 0 To dtTable.Rows.Count - 1
                                            @<tr>
                                                <td style="display:none"></td>
                                                <td class="hidden">
                                                    @dtTable.Rows(i).Item("ID")
                                                </td>
                                                <td>
                                                    <a class="blue" href="@Url.Action("FormInput", "GM")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Detail" title="Detail">
                                                        @dtTable.Rows(i).Item("GM_NUMBER")
                                                    </a>
                                                </td>
                                                @*<td>
                                                        @dtTable.Rows(i).Item("GM_NUMBER")
                                                    </td>*@
                                                <td>
                                                    @Format(dtTable.Rows(i).Item("CREATE_DATE"), "dd-MMM-yyyy")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("PO_NUMBER")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("SUPPLIER_NAME")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("DELIVERY_NAME")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("STATUS")
                                                </td>
                                                <td>
                                                    <div class="hidden-sm hidden-xs action-buttons">
                                                        <a class="blue" href="@Url.Action("FormInput", "GM")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Detail" title="Detail">
                                                            @Html.Raw(Labels.IconAction("Details"))
                                                        </a>
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Active Then
                                                            @<a Class="green" href="@Url.Action("FormInput", "GM")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Edit" title="Edit">
                                                                @Html.Raw(Labels.IconAction("Edit"))
                                                            </a>
                                                            @<a Class="red" href="@Url.Action("FormInput", "GM")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Delete" title="Delete">
                                                                @Html.Raw(Labels.IconAction("Delete"))
                                                            </a>
                                                        End If
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Approve Then
                                                            @<a class="red" href="@Url.Action("FormInput", "GM")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Approve" title="Approve">
                                                                @Html.Raw(Labels.IconAction("Approve"))
                                                            </a>
                                                        End If
                                                    </div>
                                                </td>
                                            </tr>
                                        Next
                                    End If
                                    
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

<script type="text/javascript">
    var oTable1 =
    $('#dynamic-table')
    .dataTable({
        bAutoWidth: false,
        "aaSorting": [],
    });
</script>