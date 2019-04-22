@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_CRV)
@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.CRVController 

@Code
    ViewBag.Breadcrumbs = "CRV"
    ViewBag.Title = "CRV List"
    ViewBag.CRV = "active open"
    ViewBag.IndexCreateCRV = "active"
End Code

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
                                <button title="Export to xls" class="btn btn-white btn-info btn-bold" id="export" onClick="fnExcelReport('export', 'dynamic-table', 'CRV')">
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
                                        <th style="display:none"></th> 
                                        <th class="hidden">ID</th>
                                        <th>CRV Number</th>
                                        <th>PO Number</th>
                                        <th>Supplier Name</th>
                                        <th>Orig Amount</th>
                                        <th>Bank A/C Name</th>
                                        <th>Bank A/C Number</th>
                                        <th>Currency Code</th>
                                        <th>Amount</th>
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
                                            <td class="hidden">
                                                @item.ID
                                            </td>
                                            <td>
                                                @item.CRV_NUM
                                            </td> 
                                            <td>
                                                @item.PO_ID 
                                            </td> 
                                            <td>
                                                @item.TPROC_PO_HEADERS.SUPPLIER_NAME
                                            </td> 
                                            <td>
                                                @item.ORIGIN_AMOUNT.ToString("#,##0")
                                            </td> 
                                            <td>
                                                @item.BANK_NAME
                                            </td>  
                                            <td>
                                                @item.BANK_ACCOUNT_NUMBER
                                            </td>  
                                            <td>
                                                @item.TPROC_PO_HEADERS.TPROC_CURRENCY.CURRENCY_NAME
                                            </td>     
                                            <td>
                                                @item.AMOUNT.ToString("#,##0")
                                            </td>  
                                            <td class="hidden">
                                                @item.CREATED_TIME.ToString("dd-MM-yyy HH:mm")
                                            </td>
                                            <td class="hidden">
                                                @item.CREATED_BY
                                            </td>
                                            <td class="hidden">
                                                @If item.LAST_MODIFIED_TIME.HasValue Then @item.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm") End If
                                            </td>
                                            <td class="hidden">
                                                @item.LAST_MODIFIED_BY
                                            </td>
                                            <td>
                                                <div class="hidden-sm hidden-xs action-buttons">
                                                    <a class="green" href="@Url.Action("FormInput", "CRV")?_Id=@item.ID&_Action=Edit" title="Edit">
                                                        @Html.Raw(Labels.IconAction("Edit"))
                                                    </a>
                                                    <a class="red" href="@Url.Action("FormInput", "CRV")?_Id=@item.ID&_Action=Delete" title="Delete">
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
    var oTable1 =
    $('#dynamic-table')
    .dataTable({
        bAutoWidth: false,
        "aaSorting": [],
    });
</script>