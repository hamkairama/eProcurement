@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.CRVController 

@Code
    ViewBag.Breadcrumbs = "CRV"
    ViewBag.Title = "CRV List"
    ViewBag.CRV = "active open"
    ViewBag.IndexCreateCRV = "active"
End Code
<style>
    .table-striped > tbody > tr:nth-child(odd) > td,
    .table-striped > tbody > tr:nth-child(odd) > th {
        white-space: nowrap;
    }

    #dynamic-table {
        overflow-x: scroll;
        max-width: 100%;
        display: block;
        white-space: nowrap;
    }
</style>

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
                                    @Html.TextBox("txt_status_crv_val", "", New With {.class = "hidden"})
                                    @Html.DropDownList("dropdownList_status_crv", Dropdown.ListStatusCrv)
                                    <Label id="required_txt_submitter"></Label>
                                    <button type='submit' onclick="GetData()" class='btn btn-sm btn-success btn-white btn-round'>
                                        <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                                        Get Crv
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
                                        <th>CRV Number</th>
                                        <th>CRV Date</th>
                                        <th>GM Number</th>
                                        <th>Supplier Name</th>
                                        <th>Payment Method</th>
                                        <th>Bank Account Number</th>
                                        <th>Bank Name</th>
                                        <th>Bank Branch</th>
                                        <th>Ref Tax No</th>
                                        <th>Last Modified</th>
                                        <th>Status</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @if dtTable IsNot Nothing Then
                                        @For i As Integer = 0 To dtTable.Rows.Count - 1
                                            @<tr>
                                                <td style="display:none"></td>
                                                <td class="hidden">
                                                    @dtTable.Rows(i).Item("ID")
                                                </td>
                                                <td>
                                                    <a class="blue" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Detail" title="Detail">
                                                        @dtTable.Rows(i).Item("CRV_NUM")
                                                    </a>                                                   
                                                </td>
                                                <td>
                                                    @Format(dtTable.Rows(i).Item("CREATED_TIME"), "dd-MMM-yyyy")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("GM_NUMBER")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("SUPPLIER_NAME")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("PAYMENTMETHOD")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("BANK_ACCOUNT_NUMBER")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("BANK_NAME")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("BANK_BRANCH")
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("REFTAXNO")
                                                </td>
                                                <td>
                                                    @if IsDBNull(dtTable.Rows(i).Item("LAST_MODIFIED_TIME")) = False Then
                                                        @Format(dtTable.Rows(i).Item("LAST_MODIFIED_TIME"), "dd-MMM-yyyy")
                                                    End If                                                    
                                                </td>
                                                <td>
                                                    @dtTable.Rows(i).Item("STATUS")
                                                </td>
                                                <td>
                                                    <div class="hidden-sm hidden-xs action-buttons">
                                                        <a class="blue" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Detail" title="Detail">
                                                            @Html.Raw(Labels.IconAction("Details"))
                                                        </a>
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Active Then
                                                            @<a Class="green" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Edit" title="Edit">
                                                                @Html.Raw(Labels.IconAction("Edit"))
                                                            </a>
                                                            @<a Class="red" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Delete" title="Delete">
                                                                @Html.Raw(Labels.IconAction("Delete"))
                                                            </a>
                                                        End If
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Verify Then
                                                            @<a Class="red" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Verified" title="Verified">
                                                                @Html.Raw(Labels.IconAction("Review"))
                                                            </a>
                                                        End If
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Approve Then
                                                            @<a class="red" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Approve" title="Approve">
                                                                @Html.Raw(Labels.IconAction("Approve"))
                                                            </a>
                                                        End If
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Received Then
                                                            @<a class="green" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Received" title="Received">
                                                                @Html.Raw(Labels.IconAction("Received"))                                                               
                                                            </a>
                                                        End If
                                                        @If dtTable.Rows(i).Item("ROW_STATUS") = RowStatus.Paid Then
                                                            @<a class="green" href="@Url.Action("FormInput", "CRV")?_Id=@dtTable.Rows(i).Item("ID")&_Action=Paid" title="Paid">
                                                                @Html.Raw(Labels.IconAction("Paid"))
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

    function ActionPrint(ID) {
        $.ajax({
            url: linkProc + '/CRV/ActionPrint',
            type: 'Post',
            data: {
                _ID: ID, 
            },
            cache: false,
            traditional: true,
            beforeSend:
                function () {
                    $("#loadingRole").toggle()
                },
            success:
                function (result) {
                    $(".dialogForm").dialog("close");
                    alert(result); 
                },
        });
    }
</script>