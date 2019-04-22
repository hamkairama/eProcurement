@ModelType eProcurementApps.Models.TPROC_ROLE
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Role"
    ViewBag.Setup = "active open"
    ViewBag.IndexRole = "active"
End Code

<div class="row">
    <div class="col-sm-4">
        <div class="widget-box">
            <div class="widget-header">
                <h4 class="widget-title">Edit Role Application</h4> @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@

            </div>

            <div class="widget-body">
                <div class="widget-main">
                    <div class="profile-user-info">
                        <div class="profile-info-row  hidden">
                            <div class="profile-info-name "> ID </div>
                            <div class="profile-info-value ">
                                <span id="txt_id">@Model.ID</span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name required"> Role Name </div>
                            <div class="profile-info-value item-required">
                                <span class="freeText change" id="txt_role_name" maxlenght="50">@Model.ROLE_NAME</span>
                                <label id="required_txt_role_name"></label>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Description </div>
                            <div class="profile-info-value">
                                <span class="freeText" id="txt_role_description" maxlenght="50">@Model.ROLE_DESCRIPTION</span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Is Active </div>
                            <div class="profile-info-value">
                                @If Model.IS_ACTIVE = 1 Then
                                    @<input type="checkbox" id="cb_is_inactive" checked="checked" />
                                Else
                                    @<input type="checkbox" id="cb_is_inactive" />
                                End If
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Default Selected </div>
                            <div class="profile-info-value">
                                @If Model.DEFAULT_SELECTED = 1 Then
                                    @<input type="checkbox" id="cb_default_selected" checked="checked" />
                                Else
                                    @<input type="checkbox" id="cb_default_selected" />
                                End If

                                <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="More details." title="Check will be selected default when create the new user.">?</span>
                            </div>
                        </div>
                                                                        
                    </div>
                    <a class="red" href="@Url.Action("Index", "Role")" title="Create">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
                    @Html.Raw(Labels.ButtonForm("SaveEdit"))
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-3">
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
                                @for Each item As eProcurementApps.Models.TPROC_ROLE_MENU In Model.TPROC_ROLE_MENU.Where(Function(x) x.TPROC_MENU.MENU_PARENT_ID Is Nothing).OrderBy(Function(x) x.TPROC_MENU.MENU_NAME)
                                    @<li id="@item.MENU_ID">
                                        <a Class="dropdown-toggle">
                                            <span Class="menu-text">@item.TPROC_MENU.MENU_DESCRIPTION</span>
                                        </a>

                                        <ul Class="submenu">
                                            @For Each child As eProcurementApps.Models.TPROC_ROLE_MENU In Model.TPROC_ROLE_MENU.Where(Function(x) x.TPROC_MENU.MENU_PARENT_ID IsNot Nothing And x.TPROC_MENU.MENU_PARENT_ID = item.MENU_ID).OrderBy(Function(x) x.TPROC_MENU.MENU_NAME)
                                                @<li id="@child.TPROC_MENU.MENU_PARENT_ID">

                                                    @If child.IS_ACCESS = 1 Then
                                                        @<input type="checkbox" Class="@child.TPROC_MENU.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" checked="checked" />
                                                    Else
                                                        @<input type="checkbox" class="@child.TPROC_MENU.MENU_PARENT_ID" id="@child.MENU_ID" name="@child.MENU_ID" value="1" />
                                                    End If
                                                    <span Class="menu-text">@child.TPROC_MENU.MENU_DESCRIPTION </span>

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
        </div>
    </div>
</div>



<script src="~/Scripts/Controllers/ROLEController.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>