@ModelType eProcurementApps.Models.TPROC_REL_DEPT
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
    <div class="col-sm-4">
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

    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Approval</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
            </div>
            <div class="widget-body" id="testing">
                <table>
                    <tr>
                        <td width="500px">
                            <div class="profile-user-info">
                                <div class="profile-info-row">
                                    <div class="profile-info-name "> ID  </div>
                                    <div class="profile-info-value ">
                                        <span id="txt_id">@Model.ID</span>
                                    </div>
                                </div>

                                <div class="profile-info-row">
                                    <div class="profile-info-name required"> RelDept.  </div>
                                    <div class="profile-info-value item-required">
                                        <span Class="" id="txt_related_department_name" maxlenght="50">@Model.RELATED_DEPARTMENT_NAME</span>
                                        <label id="required_txt_related_department_name"></label>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td width="500px">
                            @*<div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Approval  </div>
                                    </div>
                                    @For Each item In Model.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT
                                    @<div Class="profile-info-name "> @item.USER_NAME : </div>
                                    @<div Class="profile-info-value">
                                        <span Class="">@item.TPROC_LEVEL.INDONESIAN_LEVEL</span>
                                    </div>
                                    Next
                                </div>*@
                        </td>
                    </tr>
                </table>

                <table id="simple-table" Class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th>User Id</th>
                            <th>User Name</th>
                            <th>Email</th>
                            <th class="hidden">Limit Approval Id</th>
                            <th>Limit appoval</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTable">
                        @code
                            Dim no As String = ""
                            Dim flag As Integer = 0

                            @For Each item In Model.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT
                                @<tr id="0">
                                    <td>
                                        <input size=8 type="text" id="txt_user_id2_@no" value="@item.APPROVAL_NAME" readonly="readonly" />
                                    </td>
                                    <td>
                                        <input size=12 type="text" id="txt_user_name2_@no" value="@item.USER_NAME" readonly="readonly" />
                                    </td>
                                    <td>
                                        <input size=35 type="text" id="txt_user_email2_@no" value="@item.EMAIL" readonly="readonly" />
                                        @*<a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                                @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                            </a>*@
                                    </td>
                                    <td class="hidden"><input size=10 type="text" id="txt_user_id2_@no" value="@item.LEVEL_ID" readonly="readonly" /></td>
                                    <td><input size=10 type="text" id="txt_user_id2_@no" value="@item.TPROC_LEVEL.INDONESIAN_LEVEL" readonly="readonly" /></td>
                                </tr>
                                flag += 1
                                no = flag
                            Next
                        End code
                    </tbody>
                </table>

                @*@Html.Raw(Labels.ButtonForm("AddRowApprovalRelDept"))*@
            </div>
        </div>
    </div>

    @code
        Dim no2 As String = ""
        Dim flag2 As Integer = 0

        Dim lappr_dt As New List(Of TPROC_APPR_RELDEPT_DT)

        lappr_dt = Facade.RelDeptFacade.GetApprDtToBe(Model.ID, ListEnum.RowStat.Edit)

        If lappr_dt.Count > 0 Then
            @<div Class="col-sm-12">
                <div Class="widget-box">
                    <div Class="widget-header">
                        <h4 Class="widget-title">Approval To be</h4> 
                    </div>
                    <div Class="widget-body" id="testing">
                        <Table>
                            <tr>
                                <td width="500px">
                                    <div class="profile-user-info">
                                        <div class="profile-info-row">
                                            <div class="profile-info-name "> ID  </div>
                                            <div class="profile-info-value ">
                                                <span id="txt_id">@Model.ID</span>
                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name required"> RelDept.  </div>
                                            <div class="profile-info-value item-required">
                                                <span Class="" id="txt_related_department_name" maxlenght="50">@Model.RELATED_DEPARTMENT_NAME</span>
                                                <label id="required_txt_related_department_name"></label>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td width="500px">
                                    @*<div class="profile-user-info">
                                            <div class="profile-info-row">
                                                <div class="profile-info-name "> Approval  </div>
                                            </div>
                                            @For Each item In Model.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT
                                            @<div Class="profile-info-name "> @item.USER_NAME : </div>
                                            @<div Class="profile-info-value">
                                                <span Class="">@item.TPROC_LEVEL.INDONESIAN_LEVEL</span>
                                            </div>
                                            Next
                                        </div>*@
                                </td>
                            </tr>
                        </Table>

                        <Table id="simple-table" Class="table table-striped table-bordered table-hover">
                            <tbody>
                                <tr>
                                    <th> User Id</th>
                                    <th> User Name</th>
                                    <th> Email</th>
                                    <th Class="hidden">Limit Approval Id</th>
                                    <th> Limit appoval</th>
                                </tr>
                            </tbody>
                            <tbody id="dataTable">

                                @For Each item2 As TPROC_APPR_RELDEPT_DT In lappr_dt
                                    @<tr id="0">
                                        <td>
                                            <input size=8 type="text" id="txt_user_id2_@no2" value="@item2.APPROVAL_NAME" readonly="readonly" />
                                        </td>
                                        <td>
                                            <input size=12 type="text" id="txt_user_name2_@no2" value="@item2.USER_NAME" readonly="readonly" />
                                        </td>
                                        <td>
                                            <input size=35 type="text" id="txt_user_email2_@no2" value="@item2.EMAIL" readonly="readonly" />
                                            @*<a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                                </a>*@
                                        </td>
                                        <td class="hidden"><input size=10 type="text" id="txt_user_id2_@no2" value="@item2.LEVEL_ID" readonly="readonly" /></td>
                                        @*<td><input size=10 type="text" id="txt_user_id2_@no2" value="@item2.TPROC_LEVEL.INDONESIAN_LEVEL" readonly="readonly" /></td>*@
                                         <td><input size=15 type="text" id="" value="@CommonFunction.GetIndLevelById(item2.LEVEL_ID)" readonly="readonly" /></td>
                                    </tr>
                                    flag2 += 1
                                    no2 = flag
                                Next
                            </tbody>
                        </Table>

                        @*@Html.Raw(Labels.ButtonForm("AddRowApprovalRelDept"))*@
                    </div>
                </div>
            </div>
        End If
    End Code

    <div Class="clearfix form-action">
        <div Class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                @If ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedComplete) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                    @Html.Raw(Labels.ButtonForm("ActionRequestRDComplete"))
                ElseIf ViewBag.data_flag = Convert.ToDecimal(ListEnum.Request.NeedApprove) And (Model.ROW_STATUS = ListEnum.RowStat.Create Or Model.ROW_STATUS = 2 Or Model.ROW_STATUS = ListEnum.RowStat.Delete) Then
                    @Html.Raw(Labels.ButtonForm("ActionRequestRDApprove"))
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

<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Controllers/RELATED_DEPARTMENTController.js"></script>
