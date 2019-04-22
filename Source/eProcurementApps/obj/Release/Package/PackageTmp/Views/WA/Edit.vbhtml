@ModelType eProcurementApps.Models.TPROC_WA
@Imports eProcurementApps.Helpers

@Code
    If ViewBag.flag = 0 Then
        ViewBag.Breadcrumbs = "Setup"
        ViewBag.Title = "Edit Work Area"
        ViewBag.Setup = "active open"
        ViewBag.IndexWA = "active"
    Else
        ViewBag.Breadcrumbs = "Request"
        ViewBag.Title = "Request Edit WA"
        ViewBag.Request = "active open"
        ViewBag.IndexRequestWA = "active"
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
    <div class="col-sm-12">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Work Area Application</h4> 
                <span id="txt_action" class="hidden">Edit</span>
            </div>
            <div class="widget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row hidden">
                        <div class="profile-info-name "> ID </div>
                        <div class="profile-info-value ">
                            <span  id="txt_id">@Model.ID</span>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> WA Number </div>
                        <div class="profile-info-value item-required">
                            <span Class="freeText" id="txt_wa_number" maxlenght="10">@Model.WA_NUMBER</span>
                            <label id="required_txt_wa_number"></label>
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> Department </div>
                        <div Class="profile-info-value item-required">
                            <span Class="freeText" id="txt_dept_name">@Model.TPROC_APPROVAL_GR.DEPARTMENT_NAME</span>
                            <label id="required_txt_dept_name"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Division </div>
                        <div class="profile-info-value item-required">
                            <span id="txt_division_id" class="hidden">@Model.TPROC_APPROVAL_GR.DIVISION_ID</span>
                            @Html.DropDownListFor(Function(x) x.TPROC_APPROVAL_GR.DIVISION_ID, ViewBag.Division)
                            <label id="required_txt_division_id"></label>
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
            <div class="widget-body" id="testing">
                <table id="simple-table" class="table table-striped table-bordered table-hover">
                    <tbody>
                        <tr>
                            <th>User Id</th>
                            <th>User Name</th>
                            <th>Email</th>
                            <th>Limit Approval</th>
                            <th>Flow</th>
                            <th>Action</th>
                        </tr>
                    </tbody>
                    <tbody id="dataTable">
                        @code
                            Dim no As String = ""
                            Dim flag As Integer = 0
                            @For Each item In Model.TPROC_APPROVAL_GR.TPROC_APPROVAL_DT
                                @<tr id="0">
                                    <td>
                                        <input size=10 type="text" id="txt_user_id2_@no" value="@item.APPROVAL_NAME"  />
                                    </td>
                                    <td>
                                        <input size=15 type="text" id="txt_user_name2_@no" value="@item.USER_NAME" />
                                    </td>
                                    <td>
                                        <input size=30 type="text" id="txt_user_email2_@no" value="@item.EMAIL"  />
                                        <a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                            @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                        </a>
                                    </td>
                                     <td>
                                         @Html.DropDownList("dropdownList", Dropdown.LevelApproval(item.LEVEL_ID & ""), New With {.style = "width:  200px;"})
                                     </td>
                                    <td><input size=5 type="text" id="txt_flow_@no" value="@item.FLOW_NUMBER" /></td>
                                    <td>
                                        <div>
                                            @Html.Raw(Labels.ButtonForm("Remove"))
                                        </div>
                                    </td>
                                </tr>
                                flag += 1
                                no = flag
                            Next
                        End code                     

                    </tbody>
                </table>
                @Html.Raw(Labels.ButtonForm("AddRowApprovalWa"))

                @If ViewBag.flag = 1 Then
                    @<div class="profile-info-row">
                        <div class="profile-info-name required"> Description </div>
                        <div class="profile-info-value item-required">
                            <input type="text" id="txt_desc" style="width:500px" maxlenght="200" />
                            <label id="required_txt_desc"></label>
                        </div>
                    </div>
                End If
            </div>
        </div>
    </div>

    <div class="clearfix form-action">
        <div class="col-lg-12">
            <div class="modal-footer no-margin-top">
                <a Class="red" href="@Url.Action("Index", "WA", New With {.flag = Convert.ToDecimal(ViewBag.flag)})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
               
                @If ViewBag.flag = 0 Then
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                Else
                    @Html.Raw(Labels.ButtonForm("SendRequestWAEdit"))
                End If


            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Controllers/WAController.js"></script>
