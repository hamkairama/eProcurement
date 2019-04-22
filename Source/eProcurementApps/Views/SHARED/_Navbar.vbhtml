@imports eProcurementApps.Models
@imports eProcurementApps.Helpers

<script type="text/javascript">
    try { ace.settings.check('navbar', 'fixed') } catch (e) { }
</script>
<div class="navbar-container" id="navbar-container" style="background-color:green">
    <!-- #section:basics/sidebar.mobile.toggle -->
    <button type="button" class="navbar-toggle menu-toggler pull-left" id="menu-toggler"
            data-target="#sidebar">
        <span class="sr-only">Toggle sidebar</span> <span class="icon-bar"></span><span class="icon-bar">
        </span><span class="icon-bar"></span>
    </button>
    <!-- /section:basics/sidebar.mobile.toggle -->
    <div class="navbar-header pull-left">
        <a href="#" class="navbar-brand">eProcurement - @WebConfigKey.Environment</a>
    </div>
    <!-- #section:basics/navbar.dropdown -->
    <div class="navbar-buttons navbar-header pull-right" role="navigation">
        <ul class="nav ace-nav">
            @code
                Dim lpr_header As New List(Of TPROC_PR_HEADER)
                Dim user_id_id = Session("USER_ID_ID")
                lpr_header = CommonFunction.GetListPRInSignOffAndInReject(user_id_id)

                @<li class="grey">
                    <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                        <i class="ace-icon fa fa-tasks">
                        </i><span class="badge badge-grey">@lpr_header.Count()</span>
                    </a>
                    <ul class="dropdown-menu-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                        <li class="dropdown-header">
                            <i class="ace-icon fa fa-check"></i>@lpr_header.Count() My List PR Un-Sign Off
                        </li>
                        <li class="dropdown-content">
                            <ul class="dropdown-menu dropdown-navbar">
                                @for Each item As TPROC_PR_HEADER In lpr_header
                                    If item.PR_STATUS = ListEnum.PRStatus.Submitted Then
                                        @<li>
                                            <a href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = 0})">
                                                <div class="clearfix">
                                                    <span class="pull-left">@item.PR_NO</span><span class="pull-right">25%</span>
                                                </div>
                                                <div Class="progress progress-mini progress-striped active">
                                                    <div style="width: 25%" Class="progress-bar progress-bar-success">
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                    ElseIf item.PR_STATUS = ListEnum.PRStatus.PrApprovedComplete Then
                                        @<li>
                                             <a href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = 0})">
                                                 <div class="clearfix">
                                                     <span class="pull-left">@item.PR_NO</span><span class="pull-right">50%</span>
                                                 </div>
                                                 <div Class="progress progress-mini progress-striped active">
                                                     <div style="width: 50%" Class="progress-bar progress-bar-yellow">
                                                     </div>
                                                 </div>
                                             </a>
                                        </li>
                                    ElseIf item.PR_STATUS = ListEnum.PRStatus.PrHandled Then
                                        @<li>
                                             <a href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = 0})">
                                                 <div class="clearfix">
                                                     <span class="pull-left">@item.PR_NO</span><span class="pull-right">75%</span>
                                                 </div>
                                                 <div Class="progress progress-mini progress-striped active">
                                                     <div style="width: 75%" Class="progress-bar progress-bar-pink">
                                                     </div>
                                                 </div>
                                             </a>
                                        </li>

                                    ElseIf item.PR_STATUS = ListEnum.PRStatus.CreatePo Then
                                        @<li>
                                            <a href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = 0})">
                                                <div class="clearfix">
                                                    <span class="pull-left">@item.PR_NO</span><span class="pull-right">85%</span>
                                                </div>
                                                <div Class="progress progress-mini progress-striped active">
                                                    <div style="width: 85%" Class="progress-bar progress-bar-purple">
                                                    </div>
                                                </div>
                                            </a>
                                        </li>

                                    ElseIf item.PR_STATUS = ListEnum.PRStatus.Complete Then
                                        @<li>
                                             <a href="@Url.Action("DetailHeader", "PURCHASING_REQUEST", New With {.id = item.ID, .flag = 0})">
                                                 <div class="clearfix">
                                                     <span class="pull-left">@item.PR_NO</span><span class="pull-right">100%</span>
                                                 </div>
                                                 <div Class="progress progress-mini progress-striped active">
                                                     <div style="width: 100%" Class="progress-bar progress-bar-info">
                                                     </div>
                                                 </div>
                                             </a>
                                        </li>
                                    End If
                                Next
                            </ul>
                        </li>
                        <li Class="dropdown-footer">
                            <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = 0, .SubTitle = ""})">
                                All my list PR with details <i Class="ace-icon fa fa-arrow-right">
                                </i>
                            </a>
                        </li>
                    </ul>
                </li>
            End Code

            @*<li Class="purple">
                    <a data-toggle="dropdown" Class="dropdown-toggle" href="#">
                        <i Class="ace-icon fa fa-bell icon-animated-bell"></i><span class="badge badge-important">
                            8
                        </span>
                    </a>
                    <ul Class="dropdown-menu-right dropdown-navbar navbar-pink dropdown-menu dropdown-caret dropdown-close">
                        <li Class="dropdown-header">
                            <i Class="ace-icon fa fa-exclamation-triangle"></i>8 Notifications
                        </li>
                        <li Class="dropdown-content">
                            <ul Class="dropdown-menu dropdown-navbar navbar-pink">
                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-pink fa fa-comment"></i>New
                                                Comments
                                            </span><span class="pull-right badge badge-info">+12</span>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <i class="btn btn-xs btn-primary fa fa-user"></i>Bob just signed up
                                        as an editor ...
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-success fa fa-shopping-cart">
                                                </i>New Orders
                                            </span><span class="pull-right badge badge-success">+8</span>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <div class="clearfix">
                                            <span class="pull-left">
                                                <i class="btn btn-xs no-hover btn-info fa fa-twitter"></i>Followers
                                            </span><span class="pull-right badge badge-info">+11</span>
                                        </div>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li class="dropdown-footer">
                            <a href="#">
                                See all notifications <i class="ace-icon fa fa-arrow-right">
                                </i>
                            </a>
                        </li>
                    </ul>
                </li>
                <li class="green">
                    <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                        <i class="ace-icon fa fa-envelope icon-animated-vertical">
                        </i><span class="badge badge-success">5</span>
                    </a>
                    <ul class="dropdown-menu-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                        <li class="dropdown-header">
                            <i class="ace-icon fa fa-envelope-o"></i>5 Messages
                        </li>
                        <li class="dropdown-content">
                            <ul class="dropdown-menu dropdown-navbar">
                                <li>
                                    <a href="#" class="clearfix">
                                        <img src="../assets/avatars/avatar.png" class="msg-photo" alt="Alex's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Alex:</span> Ciao
                                                sociis natoque penatibus et auctor ...
                                            </span><span class="msg-time">
                                                <i class="ace-icon fa fa-clock-o">
                                                </i><span>a moment ago</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="clearfix">
                                        <img src="../assets/avatars/avatar3.png" class="msg-photo" alt="Susan's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Susan:</span> Vestibulum
                                                id ligula porta felis euismod ...
                                            </span><span class="msg-time">
                                                <i class="ace-icon fa fa-clock-o">
                                                </i><span>20 minutes ago</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="clearfix">
                                        <img src="../assets/avatars/avatar4.png" class="msg-photo" alt="Bob's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Bob:</span> Nullam
                                                quis risus eget urna mollis ornare ...
                                            </span><span class="msg-time">
                                                <i class="ace-icon fa fa-clock-o">
                                                </i><span>3:15 pm</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="clearfix">
                                        <img src="../assets/avatars/avatar2.png" class="msg-photo" alt="Kate's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Kate:</span> Ciao
                                                sociis natoque eget urna mollis ornare ...
                                            </span><span class="msg-time">
                                                <i class="ace-icon fa fa-clock-o">
                                                </i><span>1:33 pm</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="clearfix">
                                        <img src="../assets/avatars/avatar5.png" class="msg-photo" alt="Fred's Avatar" />
                                        <span class="msg-body">
                                            <span class="msg-title">
                                                <span class="blue">Fred:</span> Vestibulum
                                                id penatibus et auctor ...
                                            </span><span class="msg-time">
                                                <i class="ace-icon fa fa-clock-o">
                                                </i><span>10:09 am</span>
                                            </span>
                                        </span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li class="dropdown-footer">
                            <a href="inbox.html">
                                See all messages <i class="ace-icon fa fa-arrow-right">
                                </i>
                            </a>
                        </li>
                    </ul>
                </li>*@
            <!-- #section:basics/navbar.user_menu -->
            <li class="green">
                <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                    <img class="nav-user-photo" src="~/Ace/assets/avatars/avatar2.png" alt="Jason's Photo" />
                    <span class="user-info"><small>Welcome,</small> @Session("USER_NAME")</span><i class="ace-icon fa fa-caret-down">
                    </i>
                </a>
                <ul class="user-menu dropdown-menu-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
                    @*<li><a href="#"><i class="ace-icon fa fa-cog"></i>Settings </a></li>*@

                    <li>
                        <a class="black" href="#" onclick="ModalCommon('/USER/DetailProfile/', '.dialogForm')" data-toggle="modal" title="Profile">
                            <i class="ace-icon fa fa-user"></i>Profile
                        </a>
                    </li>


                    @*<li><a href="@Url.Action("DetaiProfile", "USER", New With {.id = Session("USER_ID")})"><i class="ace-icon fa fa-user"></i>Profile </a></li>*@
                    <li class="divider"></li>
                    <li><a href="@Url.Action("LogOff", "Login")"><i class="ace-icon fa fa-power-off"></i>Logout </a></li>
                </ul>
            </li>
        </ul>
    </div>
</div>
