@Imports eProcurementApps.Helpers 
@Imports eProcurementApps.Controllers.APPROVAL_ROLEController

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Setup = "active open"
    ViewBag.IndexApprovalRole = "active"
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
                <h4 class="widget-title">Approval Price Comparition Application</h4><span id="txt_link_eproc" class="hidden">@CommonFunction.GetLinkEproc</span>
                <span id="txt_action" class="hidden">Create</span>
            </div>
            <div id="Approval_Role_Id" class="hidden"> 
            @If dtTable.Rows.Count > 0 Then
                @dtTable.Rows(0).Item("ID")
            End If
            </div>
            <div class=" Thenwidget-body" id="testing">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> User Id </div>
                        <div class="profile-info-value item-required"> 
                            <span Class="" id="txt_userid">
                                @if dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("USER_ID")
                                End If
                            </span> 
                            @If APPROVAL_ROLE_Action <> "Delete" Then
                                @*@<a class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>*@

                                @<a Class="red" href="#" onclick="ModalCommon('/ACTIVE_DIRECTORY/Index/flag', '.dialogForm')" data-toggle="modal" title="Active Directory">
                                    @Html.Raw(Labels.ButtonForm("ActiveDirectory"))
                                </a>
                            End If
                            
                        </div>
                    </div>

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> Username </div>
                        <div Class="profile-info-value item-required">
                            <div id="txt_username" >
                                @if dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("USER_NAME")
                                End If   
                            </div>
                        </div>
                    </div> 

                    <div Class="profile-info-row">
                        <div Class="profile-info-name required"> Email </div>
                        <div Class="profile-info-value item-required">
                            <div id="txt_email">
                                @if dtTable.Rows.Count > 0 Then
                                    @dtTable.Rows(0).Item("EMAIL")
                                End If   
                            </div>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Level Id </div>
                        <div class="profile-info-value item-required">
                            @If APPROVAL_ROLE_Action = "Delete" Then
                                @<div id="txt_levelid_delete" >
                                     @if dtTable.Rows.Count > 0 Then
                                         @dtTable.Rows(0).Item("INDONESIAN_LEVEL")
                                     End If 
                                </div>
                            Else
                                @<span id="txt_levelid" class="hidden">
                                     @if dtTable.Rows.Count > 0 Then
                                         @dtTable.Rows(0).Item("LEVEL_ID")
                                     End If 
                                </span>
                                @if dtTable.Rows.Count > 0 Then
                                    @Html.DropDownList("dropdownList_levelid_div", Dropdown.LevelApproval(dtTable.Rows(0).Item("LEVEL_ID") & ""), New With {.style = "width:  200px;"})
                                Else
                                    @Html.DropDownList("dropdownList_levelid_div", Dropdown.LevelApproval(""), New With {.style = "width:  200px;"})
                                End If
                            End If            
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Signature </div>
                        <div class="profile-info-value item-required">
                            @code
                                If dtTable.Rows.Count > 0 Then
                                    If IsDBNull(dtTable.Rows(0).Item("SIGNATURE_IMAGE")) = False Then
                                        @<img id="txt_SIGNATURE_IMAGE" src="data:image/png;base64,@System.Convert.ToBase64String(dtTable.Rows(0).Item("SIGNATURE_IMAGE"))" />
                                    Else
                                        @<img id="txt_SIGNATURE_IMAGE" src="data:image/png;base64," />
                                    End If
                                Else
                                    @<img id="txt_SIGNATURE_IMAGE" src="data:image/png;base64," />
                                End If
                                @<br />
                                If APPROVAL_ROLE_Action <> "Delete" Then
                                    @<button id="btnBrowse" onclick="GetFile()">Browse</button>
                                    @<input type = "file" id="postedFile" style="display:none" />
                                End If
                            End Code
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div Class="col-sm-12">
        <div Class="widget-box">
            <div Class="widget-header">
                <h4 Class="widget-title">Detail Approve</h4>
            </div>
            <div Class="widget-body" id="">
                <Table id = "myTable" Class="table table-striped table-bordered table-hover">
                    <thead id = "table_th" >
                        <tr>
                            <th>MENUNAME</th>
                            <th>VERIFY</th>
                            <th>APPROVAL</th>
                            <th>RECEIVED</th>
                            <th>PAID</th>
                        </tr>
                                                                                                                                                                                                                                                                                                                                                                                            </thead>
                    <tbody id = "table_ap" >
                        @For i = 0 To dtTableDetail.Rows.Count - 1
                            @<tr id="@i">
                                <td id=@(i.ToString() + "_1")>
                                    @dtTableDetail.Rows(i).Item("FORMNAME")
                                </td>
                                <td id=@(i.ToString() + "_2")>
                                    @If (dtTableDetail.Rows(i).Item("VERIFY") & "") = "1" Then
                                        @<input type="checkbox" checked>
                                    ElseIf (dtTableDetail.Rows(i).Item("VERIFY") & "") = "0" Then
                                        @<input type="checkbox" >
                                    End If   
                                </td> 
                                <td id=@(i.ToString() + "_3")>
                                    @If (dtTableDetail.Rows(i).Item("APPROVER") & "") = "1" Then
                                        @<input type="checkbox" checked>
                                    Else
                                        @<input type="checkbox" >
                                    End If    
                                </td> 
                                <td id=@(i.ToString() + "_4")>
                                    @If (dtTableDetail.Rows(i).Item("RECEIVED") & "") = "1" Then
                                        @<input type="checkbox" checked>
                                    ElseIf (dtTableDetail.Rows(i).Item("RECEIVED") & "") = "0" Then
                                        @<input type="checkbox" >
                                    End If    
                                </td> 
                                <td id=@(i.ToString() + "_5")>
                                    @If (dtTableDetail.Rows(i).Item("PAID") & "") = "1" Then
                                        @<input type="checkbox" checked>
                                    ElseIf (dtTableDetail.Rows(i).Item("PAID") & "") = "0" Then
                                        @<input type="checkbox" >
                                    End If    
                                </td> 
                            </tr>
                        Next
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div Class="clearfix form-action">
        <div Class="col-lg-12">
            <div Class="modal-footer no-margin-top">
                <a Class="red" href="@Url.Action("Index", "APPROVAL_ROLE")" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
                @If APPROVAL_ROLE_Action = "Create" Then
                    ViewBag.Title = "Create Approval Role"
                    @Html.Raw(Labels.ButtonForm("SaveCreate"))
                ElseIf APPROVAL_ROLE_Action = "Edit" Then
                    ViewBag.Title = "Edit Approval Role"
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                ElseIf APPROVAL_ROLE_Action = "Delete" Then
                    ViewBag.Title = "Delete Approval Role"
                    @Html.Raw(Labels.ButtonForm("SaveDelete"))
                End If
            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Standard/StandardProfile.js" ></script>
<script src="~/Scripts/Controllers/ACTIVE_DIRECTORYController.js"></script>
<script src="~/Scripts/Controllers/APPROVAL_ROLEController.js" ></script>

<script>
    function encode(input) {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        while (i < input.length) {
            chr1 = input[i++];
            chr2 = i < input.length ? input[i++] : Number.NaN; // Not sure if the index 
            chr3 = i < input.length ? input[i++] : Number.NaN; // checks are needed here

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output += keyStr.charAt(enc1) + keyStr.charAt(enc2) +
                      keyStr.charAt(enc3) + keyStr.charAt(enc4);
        }
        return output;
    }

    function GetFile() {
        $('#postedFile').trigger('click'); 
        var fileList = document.getElementById("postedFile").files;
        var file = fileList[0];
        var fileReader = new FileReader(); 
        if (fileReader && fileList && fileList.length) {
            fileReader.readAsArrayBuffer(fileList[0]); 
            fileReader.onload = function () { 
                var arrayBuffer = fileReader.result;
                var bytes = new Uint8Array(arrayBuffer); 
                document.getElementById("txt_SIGNATURE_IMAGE").src = "data:image/png;base64," + encode(bytes);
            };
        } 
    } 
</script>