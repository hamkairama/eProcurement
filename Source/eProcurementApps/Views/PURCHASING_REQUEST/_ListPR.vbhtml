@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_PR_HEADER)
@Imports eProcurementApps.Helpers

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
                                @If ViewBag.IndexListPRBySubmitter = "active" Then
                                    @<div Class="space-4"></div>

                                    Using Html.BeginForm("ListPRBySubmitter", "PURCHASING_REQUEST", FormMethod.Post)
                                        @<div Class="profile-info-row">
                                            <div Class="profile-info-name required"> Submitter </div>
                                            <div Class="profile-info-value item-required">
                                                @Html.TextBox("user_id_id", "", New With {.class = "hidden"})
                                                @Html.DropDownList("dropdownList", Dropdown.GetAllUser)
                                                <Label id="required_txt_submitter"></Label>
                                                <button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                                                    <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                                                    Get Data
                                                </button>
                                            </div>
                                        </div>
                                    End Using
                                End If

                                @If ViewBag.IndexAllListPR = "active" Then
                                    @<div Class="space-4"></div>
                                    Dim fd As Integer = ViewBag.FlagDetail
                                    Dim st As String = ViewBag.SubTitle
                                    Using Html.BeginForm("ListPrByStatus", "PURCHASING_REQUEST", FormMethod.Post)
                                            @<div Class="profile-info-row">
                                                <div Class="profile-info-name"> Status </div>
                                                <div Class="profile-info-value">
                                                    @Html.TextBox("txt_status_pr_val", "", New With {.class = "hidden"})
                                                    @Html.TextBox("flag_detail", fd, New With {.class = "hidden"})
                                                    @Html.TextBox("sub_title", st, New With {.class = "hidden"})
                                                    @*ViewBag.FlagDetail = FlagDetail
                                                    ViewBag.SubTitle = SubTitle*@
                                                    @Html.DropDownList("dropdownList_status_pr", Dropdown.ListStatusPR)
                                                    <Label id="required_txt_submitter"></Label>
                                                    <button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                                                        <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                                                        Get Data
                                                    </button>
                                                </div>
                                            </div>
                                    End Using
                                End If
                        End Code
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
                                        <th>PR No</th>
                                        <th>User Name</th>
                                        <th>PR Date</th>
                                        <th>Form Type</th>
                                        <th>Sub Type</th>
                                        <th>Good Type</th>
                                        <th>Dev Days</th>
                                        <th>Exp Dev Date</th>
                                        <th>PR Indicator</th>
                                        <th>Account Code</th>
                                        <th>PR Status</th>
                                        <th>Sub Total (Rp)</th>
                                        <th>Reject Reason</th>
                                        <th style="color:red">
                                            View
                                        </th>
                                    </tr>
                                </thead>

                                <tbody>
                                    @For Each item In Model
                                        @<tr @*class='clickable-row'*@ data-href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = ViewBag.FlagDetail.ToString()})">
                                             <td style="display:none"></td>
                                             <td class="center">
                                                 <label class="pos-rel">
                                                     <input type="checkbox" class="ace" id=" @item.ID" />
                                                     <span class="lbl"></span>
                                                 </label>
                                             </td>
                                            <td>
                                                <a href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = ViewBag.FlagDetail.ToString()})">@item.PR_NO</a>
                                            </td>
                                            <td>
                                                @item.TPROC_USER.USER_NAME
                                            </td>
                                            <td>
                                                @item.PR_DATE.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @item.TPROC_FORM_TYPE.FORM_TYPE_NAME
                                            </td>
                                            <td>
                                                @item.TPROC_FORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME
                                            </td>
                                            <td>
                                                @item.TPROC_GOOD_TYPE.GOOD_TYPE_NAME
                                            </td>
                                            <td>
                                                @item.DELIVERY_DAYS
                                            </td>
                                            <td>
                                                @item.EXP_DEV_DT.ToString("d-MMM-yy")
                                            </td>
                                            <td>
                                                @item.PR_INDICATOR
                                            </td>
                                            <td>
                                                @item.ACCOUNT_CODE
                                            </td>
                                            @code
                                                'CType([Enum].Parse(GetType(Colors), colorString), Colors)
                                                Dim status As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(item.PR_STATUS)).ToString()
                                                @*@<td style="color: @CommonFunction.ConvertStatusPr(item.PR_STATUS)">@status</td>*@
                                                @<td>@status</td>
                                            End Code
                                            <td style="text-align:right">
                                                @item.SUB_TOTAL.ToString("###,###")
                                            </td>
                                            <td>
                                                @item.REJECT_REASON
                                            </td>
                                             <td>
                                                 <div class="hidden-sm hidden-xs action-buttons">
                                                     <a class="blue" href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = ViewBag.FlagDetail.ToString()})" title="Detail">
                                                         @Html.Raw(Labels.IconAction("Details"))
                                                     </a>
                                                 </div>
                                             </td>
                                        </tr>
                                                Next
                                </tbody>
                            </table>

                            @if ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRComplete) Then
                                @<Button Class="btn btn-sm btn-success btn-white btn-round" onclick="ReminderCompleteUser()" title="Delete selected">
                                    <i Class="ace-icon fa fa-floppy-o bigger-200"></i>
                                    Reminder Push Email
                                </Button>
                            End If

                            <div class="alert alert-danger" id="msg_error" style="display:none">
                                <button type="button" class="close" data-dismiss="alert" id="close_msg_error">
                                    <i class="ace-icon fa fa-times"></i>
                                </button>
                                msg error
                                <br />
                            </div>
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
    // initiate dataTables plugin
    var oTable1 =
    $('#dynamic-table')
    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
    .dataTable({
        bAutoWidth: false,
        "aoColumns": [
          { "bSortable": false },
          null, null, null, null, null, null, null, null, null, null, null, null, null, null,
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
    $(function () {
        $('#dropdownList').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#user_id_id').val(optionSelected);
        });
    });

    jQuery(document).ready(function ($) {
        $(".clickable-row").click(function () {
            window.location = $(this).data("href");
        });
    });


    $(function () {
        $('#dropdownList_status_pr').change(function () {
            var optionSelected = $(this).find('option:selected').attr('value');
            $('#txt_status_pr_val').val(optionSelected);
        });
    });
</script>


