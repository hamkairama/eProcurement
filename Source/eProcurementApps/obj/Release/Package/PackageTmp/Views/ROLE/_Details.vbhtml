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
                <h4 class="widget-title">Details Role Application</h4>

            </div>

            <div class="widget-body">
                <div class="widget-main">
                    <div class="profile-user-info">
                        <div class="profile-info-row">
                            <div class="profile-info-name"> Division </div>
                            <div class="profile-info-value">
                                <span>@Model.ROLE_NAME</span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Description </div>
                            <div class="profile-info-value">
                                <span>@Model.ROLE_DESCRIPTION</span>
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
                                    @<input type="checkbox" checked="checked" />
                                Else
                                    @<input type="checkbox" />
                                End If
                            </div>
                        </div>

                        <div Class="profile-info-row">
                            <div Class="profile-info-name"> Created Time </div>
                            <div Class="profile-info-value">
                                <span>@Model.CREATED_TIME.ToString("dd-MM-yyy HH:mm") </span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Created By </div>
                            <div class="profile-info-value">
                                <span> @Model.CREATED_BY </span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Last Modified Time </div>
                            <div class="profile-info-value">
                                 @If Model.LAST_MODIFIED_TIME.HasValue Then  @<span>@Model.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")</span> End If
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Last Modified By </div>
                            <div class="profile-info-value">
                                <span>  @Model.LAST_MODIFIED_BY </span>
                            </div>
                        </div>

                        <div class="profile-info-row">
                            <div class="profile-info-name"> Row Status </div>
                            <div class="profile-info-value">
                                <span> @Model.ROW_STATUS.ToString("0") </span>
                            </div>
                        </div>
                    </div>
                    <a class="red" href="@Url.Action("Index", "Role")" title="Create">
                        @Html.Raw(Labels.ButtonForm("Close"))
                    </a>
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
