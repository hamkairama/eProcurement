@ModelType eProcurementApps.Models.TPROC_WA
@Imports eProcurementApps.Helpers
@imports eProcurementApps.Models

@Code
    ViewBag.Breadcrumbs = "Request"
    ViewBag.Request = "active open"
    ViewBag.ListRequest = "active open"

    If ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.Submitted) Then
        ViewBag.IndexListOutstanding = "active"
        ViewBag.Title = "Request Outstanding"
    ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) Then
        ViewBag.IndexListNeedApprove = "active"
        ViewBag.Title = "Request Need Approve"
    ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedComplete) Then
        ViewBag.IndexListNeedComplete = "active"
        ViewBag.Title = "Request Need Completed"
    ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.Completed) Then
        ViewBag.IndexListAlreadyCompleted = "active"
        ViewBag.Title = "Request Already Completed"
    End If
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

<div class="row">
    <div class="col-sm-5">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Requestor Detail</h4>
            </div>
            <div class="widget-body" id="">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name"> Request No. </div>
                        <div class="profile-info-value">
                            <span id="txt_request_no">@ViewBag.ReqNo</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Request By </div>
                        <div class="profile-info-value ">
                            <span Class="" id="">@ViewBag.RequestBy</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Request Date </div>
                        <div class="profile-info-value">
                            <span Class="" id="" maxlenght="10">@ViewBag.RequestDt</span>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name "> Subject </div>
                        <div Class="profile-info-value ">
                            <span Class="" id="txt_actions">@ViewBag.RequestAction</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Description </div>
                        <div class="profile-info-value ">
                            <span id="" class="">@ViewBag.RequestDesc</span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-7">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Work Area Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
            </div>
            <div class="widget-body" id="">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name "> ID </div>
                        <div class="profile-info-value ">
                            <span Class="" id="txt_id">@Model.ID</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> WA Number </div>
                        <div class="profile-info-value">
                            <span Class="freeText" id="txt_wa_number" maxlenght="10">@Model.WA_NUMBER</span>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name "> Department </div>
                        <div Class="profile-info-value ">
                            <span Class="freeText" id="txt_dept_name">@Model.TPROC_APPROVAL_GR.DEPARTMENT_NAME</span>
                            <label id=""></label>
                        </div>

                        <div Class="profile-info-name "> To be  </div>
                        <div Class="profile-info-value ">
                            <span Class="" id="">@ViewBag.DepartmentTobe</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name "> Division </div>
                        <div class="profile-info-value ">
                            <span id="" class="">@Model.TPROC_APPROVAL_GR.TPROC_DIVISION.DIVISION_NAME</span>
                            <span id="txt_division_id" class="hidden">@Model.TPROC_APPROVAL_GR.DIVISION_ID</span>
                            @*@Html.DropDownListFor(Function(x) x.TPROC_APPROVAL_GR.DIVISION_ID, ViewBag.Division)*@
                            <label id=""></label>
                        </div>

                        <div class="profile-info-name "> To be </div>
                        <div class="profile-info-value ">
                            <span id="" class="">@ViewBag.DivisionTobe</span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Approval</h4>
            </div>
            <div class="widget-body" id="">
                <table id="simple-table" class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th>User Id</th>
                            <th>User Name</th>
                            <th>Email</th>
                            <th>Flow</th>
                            <th class="hidden">Limit Approval Id</th>
                            <th>Limit appoval</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTable">
                        @code
                            Dim no As String = ""
                            Dim flag As Integer = 0
                            @For Each item In Model.TPROC_APPROVAL_GR.TPROC_APPROVAL_DT
                                @<tr id="0">
                                    <td>
                                        <input size=10 type="text" id="txt_user_id2_@no" value="@item.APPROVAL_NAME" readonly="readonly" />
                                    </td>
                                    <td>
                                        <input size=15 type="text" id="txt_user_name2_@no" value="@item.USER_NAME" readonly="readonly" />
                                    </td>
                                    <td>
                                        <input size=30 type="text" id="txt_user_email2_@no" value="@item.EMAIL" readonly="readonly" />
                                    </td>
                                    <td><input size=5 type="text" id="txt_flow_@no" value="@item.FLOW_NUMBER" readonly="readonly" /></td>
                                    <td class="hidden"><input size=10 type="text" id="" value="@item.LEVEL_ID" readonly="readonly" /></td>
                                    <td><input size=15 type="text" id="" value="@item.TPROC_LEVEL.INDONESIAN_LEVEL" readonly="readonly" /></td>
                                </tr>
                                flag += 1
                                no = flag
                            Next
                        End code

                    </tbody>
                </table>
            </div>
        </div>
    </div>

    @code
        Dim no2 As String = ""
        Dim flag2 As Integer = 0
        Dim lappr_dt As New List(Of TPROC_APPROVAL_DT)

        lappr_dt = Facade.WAFacade.GetApprDtToBe(Model.ID, ListEnum.RowStat.Edit)

        If lappr_dt.Count > 0 Then
            @<div Class="col-sm-12">
                <div Class="widget-box">
                    <div Class="widget-header">
                        <h4 Class="widget-title">Approval To Be</h4>
                    </div>
                    <div Class="widget-body" id="">
                        <Table id="simple-table" Class="table table-striped table-bordered table-hover">
                            <tbody>
                                <tr>
                                    <th> User Id</th>
                                    <th> User Name</th>
                                    <th> Email</th>
                                    <th> Flow</th>
                                    <th Class="hidden">Limit Approval Id</th>
                                    <th> Limit appoval</th>
                                </tr>
                            </tbody>
                            <tbody id="dataTable">

                                @For Each item2 As TPROC_APPROVAL_DT In lappr_dt
                                    @<tr id="0">
                                        <td>
                                            <input size=10 type="text" id="txt_user_id2_@no2" value="@item2.APPROVAL_NAME" readonly="readonly" />
                                        </td>
                                        <td>
                                            <input size=15 type="text" id="txt_user_name2_@no2" value="@item2.USER_NAME" readonly="readonly" />
                                        </td>
                                        <td>
                                            <input size=30 type="text" id="txt_user_email2_@no2" value="@item2.EMAIL" readonly="readonly" />
                                        </td>
                                        <td><input size=5 type="text" id="txt_flow_@no" value="@item2.FLOW_NUMBER" readonly="readonly" /></td>
                                        <td class="hidden"><input size=10 type="text" id="" value="@item2.LEVEL_ID" readonly="readonly" /></td>
                                        <td><input size=15 type="text" id="" value="@CommonFunction.GetIndLevelById(item2.LEVEL_ID)" readonly="readonly" /></td>
                                    </tr>
                                    flag2 += 1
                                    no2 = flag2
                                Next
                            </tbody>
                        </Table>
                    </div>
                </div>
            </div>
        End If
    End code


    <div Class="clearfix form-action">
        <div Class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                @If ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedComplete) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                    @Html.Raw(Labels.ButtonForm("ActionRequestWAComplete"))
                ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                    @Html.Raw(Labels.ButtonForm("ActionRequestWAApprove"))
                End If

               
                @If ViewBag.access_from = "0" Then '0 from menu request
                    @<a Class="red" href="@Url.Action("IndexRequestList", "REQUEST", New With {.status = Convert.ToDecimal(ViewBag.data_flag)})" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                Else
                    @<a Class="red" href="@Url.Action("IndexJobLists", "DASHBOARD")" title="Close">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                End If  

            </div>
        </div>
    </div>


</div>


<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Controllers/WAController.js"></script>

