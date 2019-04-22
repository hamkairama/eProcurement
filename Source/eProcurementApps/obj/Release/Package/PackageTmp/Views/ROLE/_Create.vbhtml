@ModelType eProcurementApps.Models.ROLE_HELPER
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Role"
    ViewBag.Setup = "active open"
    ViewBag.IndexRole = "active"
End Code


<div class="row">
    <div class="col-sm-6">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Create Role Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@

            </div>

            <div class="widget-body">
                <div class="widget-main">
                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Role Name </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText change" id="txt_role_name" maxlenght="50"></span>
                            <label id="required_txt_role_name"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name required"> Description </div>
                        <div class="profile-info-value item-required">
                            <span class="freeText" id="txt_role_description" maxlenght="50"></span>
                            <label id="required_txt_role_description"></label>
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Is Active </div>
                        <div class="profile-info-value">
                            <input type="checkbox" id="cb_is_inactive" />
                        </div>
                    </div>

                    <div class="profile-info-row">
                        <div class="profile-info-name"> Default Selected </div>
                        <div class="profile-info-value">
                            <input type="checkbox" id="cb_default_selected" />
                            <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be selected default when create the new user.">?</span>
                        </div>
                    </div>

                    <a class="red" href="@Url.Action("Index", "Role")" title="Create">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                    @Html.Raw(Labels.ButtonForm("SaveCreate"))
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-4">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Menu List Application</h4>

                <div class="widget-toolbar">
                    <a href="#" data-action="collapse">
                        <i class="ace-icon fa fa-chevron-up"></i>
                    </a>
                </div>
            </div>

            <div class="widget-body">
                <div class="widget-main">

                    <div class="sidebar">
                        <ul Class="nav nav-list">
                            @code
                                @for Each item As eProcurementApps.Models.ROLE_MENU_HELPER In Model.ROLE_MENU_HELPER.Where(Function(x) x.MENU_PARENT_ID = 0).OrderBy(Function(x) x.MENU_NAME)
                                    @<li id="@item.MENU_ID">
                                        <a Class="dropdown-toggle">
                                            <span Class="menu-text">@item.MENU_DESCRIPTION</span>
                                        </a>

                                        <ul Class="submenu">
                                            @For Each child As eProcurementApps.Models.ROLE_MENU_HELPER In Model.ROLE_MENU_HELPER.Where(Function(x) x.MENU_PARENT_ID = item.MENU_ID).OrderBy(Function(x) x.MENU_NAME)
                                                @<li id="@child.MENU_PARENT_ID">

                                                    @If (child.MENU_DESCRIPTION = "Request User" Or child.MENU_DESCRIPTION = "Request WA" Or child.MENU_DESCRIPTION = "Request RD" Or child.MENU_DESCRIPTION = "Request COA" Or child.MENU_DESCRIPTION = "Request Form SubType") Then
                                                        @<input type="checkbox" class="@child.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" checked="checked" disabled="disabled" />
                                                    Else
                                                        @If child.IS_ACCESS = 1 Then
                                                            @<input type="checkbox" Class="@child.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" checked="checked" />
                                                        Else
                                                            @<input type="checkbox" class="@child.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" />
                                                        End If
                                                    End If                                                                                                

                                                    @*@If child.IS_ACCESS = 1 Then
                                                        @<input type="checkbox" Class="@child.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" checked="checked" />
                                                    Else
                                                        @<input type="checkbox" class="@child.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" />
                                                    End If*@

                                                    <span Class="menu-text">@child.MENU_DESCRIPTION </span>

                                                </li>
                                            Next
                                        </ul>
                                    </li>
                                Next

                            End Code
                        </ul>
                    </div>
                </div>
            </div>
            <!-- #section:custom/file-input.filter -->
            @*<label>
                    select one or more
                </label>*@
        </div>
    </div>
</div>



<script src="~/Scripts/Controllers/ROLEController.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
