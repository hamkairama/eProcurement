@Code
    ViewBag.Breadcrumbs = "Home"
    ViewBag.Title = "User Profile"
    ViewBag.Home = "active open"
    ViewBag.UserProfile = "active"
End Code

@Styles.Render("~/Bundle/Ace/Profile/cs")
<!-- /section:basics/navbar.layout -->
<div class="main-container" id="main-container">

    <!-- /section:basics/sidebar -->
    <div class="main-content">
        <div class="main-content-inner">

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">

                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->

                        <div>
                            <div id="user-profile-1" class="user-profile row">
                                <div class="col-xs-12 col-sm-3 center">
                                    <div>
                                        <!-- #section:pages/profile.picture -->
                                        <span class="profile-picture">
                                            <img id="avatar" class="editable img-responsive" alt="Alex's Avatar" src="../../Ace/assets/avatars/user.jpg" />
                                        </span>

                                        <!-- /section:pages/profile.picture -->
                                        <div class="space-4"></div>

                                        <div class="width-80 label label-info label-xlg arrowed-in arrowed-in-right">
                                            <div class="inline position-relative">
                                                <a href="#" class="user-title-label dropdown-toggle" data-toggle="dropdown">
                                                    <i class="ace-icon fa fa-circle light-green"></i>
                                                    &nbsp;
                                                    <span class="white">Alex M. Doe</span>
                                                </a>

                                                <ul class="align-left dropdown-menu dropdown-caret dropdown-lighter">
                                                    <li class="dropdown-header"> Change Status </li>

                                                    <li>
                                                        <a href="#">
                                                            <i class="ace-icon fa fa-circle green"></i>
                                                            &nbsp;
                                                            <span class="green">Available</span>
                                                        </a>
                                                    </li>

                                                    <li>
                                                        <a href="#">
                                                            <i class="ace-icon fa fa-circle red"></i>
                                                            &nbsp;
                                                            <span class="red">Busy</span>
                                                        </a>
                                                    </li>

                                                    <li>
                                                        <a href="#">
                                                            <i class="ace-icon fa fa-circle grey"></i>
                                                            &nbsp;
                                                            <span class="grey">Invisible</span>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- /section:custom/extra.grid -->
                                    <div class="hr hr16 dotted"></div>
                                </div>

                                <div class="col-xs-12 col-sm-9">
                                    <!-- #section:pages/profile.info -->
                                    <div class="profile-user-info profile-user-info-striped">
                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Username </div>

                                            <div class="profile-info-value">
                                                <span class="editable" id="username">alexdoe</span>
                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Age </div>

                                            <div class="profile-info-value">
                                                <span class="editable" id="age">38</span>
                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Joined </div>

                                            <div class="profile-info-value">
                                                <span class="editable" id="signup">2010/06/20</span>
                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> Last Online </div>

                                            <div class="profile-info-value">
                                                <span class="editable" id="login">3 hours ago</span>
                                            </div>
                                        </div>

                                        <div class="profile-info-row">
                                            <div class="profile-info-name"> About Me </div>

                                            <div class="profile-info-value">
                                                <span class="editable" id="about">Editable as WYSIWYG</span>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>

                        <!-- PAGE CONTENT ENDS -->
                    </div><!-- /.col -->
                </div><!-- /.row -->
            </div><!-- /.page-content -->
        </div>
    </div><!-- /.main-content -->


</div><!-- /.main-container -->

@Scripts.Render("~/Bundle/Ace/Profile/js")
<script src="~/Scripts/Standard/StandardProfile.js"></script>


