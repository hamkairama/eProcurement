@ModelType eProcurementApps.Models.LOGIN_MODEL
@code
    Layout = ""
End Code
<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Login Page - Ace Admin</title>

    <meta name="description" content="User login page" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    @Styles.Render("~/Layout/css")

    <!--[if lte IE 9]>
      <link rel="stylesheet" href="../assets/css/ace-ie.css" />
    <![endif]-->
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
    <script src="../assets/js/html5shiv.js"></script>
    <script src="../assets/js/respond.js"></script>
    <![endif]-->
</head>

<body class="login-layout">
    <div class="main-container">
        <div class="main-content">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="login-container">
                        <div class="center">
                            <h1>
                                <i class="ace-icon fa fa-leaf green"></i>
                                <span class="red">eProcurement</span>
                                <span class="white" id="id-text2">Apps</span>
                            </h1>
                            <h4 class="blue" id="id-company-text">&copy; Manulife Indonesia</h4>
                        </div>

                        <div class="space-6"></div>

                        <div class="position-relative">
                            <div id="login-box" class="login-box visible widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header blue lighter bigger">
                                            <i class="ace-icon fa fa-coffee green"></i>
                                            Please Enter Your Information
                                        </h4>

                                        <div class="space-6"></div>
                                        @Using (Html.BeginForm())
                                            @Html.AntiForgeryToken()

                                            @<div class="form-horizontal">
                                                 @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
                                                <fieldset>
                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            @Html.EditorFor(Function(model) model.EMAIL, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Username"}})
                                                            <i class="ace-icon fa fa-user"></i>
                                                        </span>
                                                    </label>

                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            @Html.EditorFor(Function(model) model.PASSWORD, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Username"}})
                                                            <i class="ace-icon fa fa-lock"></i>
                                                        </span>
                                                    </label>

                                                    <div class="space"></div>
                                                    <div class="clearfix">
                                                        <label class="inline">
                                                            <input type="checkbox" class="ace" />
                                                            <span class="lbl"> Remember Me</span>
                                                        </label>

                                                        <button type="submit" class="width-35 pull-right btn btn-sm btn-primary">
                                                            <i class="ace-icon fa fa-key"></i>
                                                            <span class="bigger-110">Login</span>
                                                        </button>
                                                    </div>

                                                    <div class="space-4"></div>
                                                </fieldset>
                                            </div>
                                        End Using


                                        <div class="social-or-login center">
                                            <span class="bigger-110">Or Login Using</span>
                                        </div>

                                        <div class="space-6"></div>

                                        <div class="social-login center">
                                            <a class="btn btn-primary">
                                                <i class="ace-icon fa fa-facebook"></i>
                                            </a>

                                            <a class="btn btn-info">
                                                <i class="ace-icon fa fa-twitter"></i>
                                            </a>

                                            <a class="btn btn-danger">
                                                <i class="ace-icon fa fa-google-plus"></i>
                                            </a>
                                        </div>
                                    </div><!-- /.widget-main -->

                                    <div Class="toolbar clearfix">
                                        <div>
                                            <a href="#" data-target="#forgot-box" Class="forgot-password-link">
                                                <i Class="ace-icon fa fa-arrow-left"></i>
                                                I forgot my password
                                            </a>
                                        </div>

                                        <div>
                                            <a href="#" data-target="#signup-box" Class="user-signup-link">
                                                I want To register
                                                <i Class="ace-icon fa fa-arrow-right"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div><!-- /.widget-body -->
                            </div><!-- /.login-box -->

                            <div id="forgot-box" Class="forgot-box widget-box no-border">
                                <div Class="widget-body">
                                    <div Class="widget-main">
                                        <h4 Class="header red lighter bigger">
                                            <i Class="ace-icon fa fa-key"></i>
                                            Retrieve Password
                                        </h4>

                                        <div Class="space-6"></div>
                                        <p>
                                            Enter your email And To receive instructions
                                        </p>

                                        <form>
                                            <fieldset>
                                                <Label Class="block clearfix">
                                                    <span Class="block input-icon input-icon-right">
                                                        <input type="email" Class="form-control" placeholder="Email" />
                                                        <i Class="ace-icon fa fa-envelope"></i>
                                                    </span>
                                                </Label>

                                                <div Class="clearfix">
                                                    <Button type="button" Class="width-35 pull-right btn btn-sm btn-danger">
                                                        <i Class="ace-icon fa fa-lightbulb-o"></i>
                                                        <span Class="bigger-110">Send Me!</span>
                                                    </Button>
                                                </div>
                                            </fieldset>
                                        </form>
                                    </div><!-- /.widget-main -->

                                    <div Class="toolbar center">
                                        <a href="#" data-target="#login-box" Class="back-to-login-link">
                                            Back to login
                                            <i Class="ace-icon fa fa-arrow-right"></i>
                                        </a>
                                    </div>
                                </div><!-- /.widget-body -->
                            </div><!-- /.forgot-box -->

                            <div id="signup-box" Class="signup-box widget-box no-border">
                                <div Class="widget-body">
                                    <div Class="widget-main">
                                        <h4 Class="header green lighter bigger">
                                            <i Class="ace-icon fa fa-users blue"></i>
                                            New User Registration
                                        </h4>

                                        <div Class="space-6"></div>
                                        <p> Enter your details To begin: </p>

                                        <form>
                                            <fieldset>
                                                <Label Class="block clearfix">
                                                    <span Class="block input-icon input-icon-right">
                                                        <input type="email" Class="form-control" placeholder="Email" />
                                                        <i Class="ace-icon fa fa-envelope"></i>
                                                    </span>
                                                </Label>

                                                <Label Class="block clearfix">
                                                    <span Class="block input-icon input-icon-right">
                                                        <input type="text" Class="form-control" placeholder="Username" />
                                                        <i Class="ace-icon fa fa-user"></i>
                                                    </span>
                                                </Label>

                                                <Label Class="block clearfix">
                                                    <span Class="block input-icon input-icon-right">
                                                        <input type="password" Class="form-control" placeholder="Password" />
                                                        <i Class="ace-icon fa fa-lock"></i>
                                                    </span>
                                                </Label>

                                                <Label Class="block clearfix">
                                                    <span Class="block input-icon input-icon-right">
                                                        <input type="password" Class="form-control" placeholder="Repeat password" />
                                                        <i Class="ace-icon fa fa-retweet"></i>
                                                    </span>
                                                </Label>

                                                <Label Class="block">
                                                    <input type="checkbox" Class="ace" />
                                                    <span Class="lbl">
                                                        I accept the
                                                        <a href="#"> User Agreement</a>
                                                    </span>
                                                </Label>

                                                <div Class="space-24"></div>

                                                <div Class="clearfix">
                                                    <Button type="reset" Class="width-30 pull-left btn btn-sm">
                                                        <i Class="ace-icon fa fa-refresh"></i>
                                                        <span Class="bigger-110">Reset</span>
                                                    </Button>

                                                    <Button type="button" Class="width-65 pull-right btn btn-sm btn-success">
                                                        <span Class="bigger-110">Register</span>

                                                        <i Class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                    </Button>
                                                </div>
                                            </fieldset>
                                        </form>
                                    </div>

                                    <div Class="toolbar center">
                                        <a href="#" data-target="#login-box" Class="back-to-login-link">
                                            <i Class="ace-icon fa fa-arrow-left"></i>
                                            Back to login
                                        </a>
                                    </div>
                                </div><!-- /.widget-body -->
                            </div><!-- /.signup-box -->
                        </div><!-- /.position-relative -->

                        <div Class="navbar-fixed-top align-right">
                            <br />
                            &nbsp;
                            <a id="btn-login-dark" href="#">Dark</a>
                            &nbsp;
                            <span Class="blue">/</span>
                            &nbsp;
                            <a id="btn-login-blur" href="#">Blur</a>
                            &nbsp;
                            <span Class="blue">/</span>
                            &nbsp;
                            <a id="btn-login-light" href="#">Light</a>
                            &nbsp; &nbsp; &nbsp;
                        </div>
                    </div>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.main-content -->
    </div><!-- /.main-container -->
    <!-- basic scripts -->
    <!--[if !IE]> -->
    <Script type="text/javascript">
        window.jQuery || document.write("<script src='../../ace/assets/js/jquery.js'>" + "<" + "/script>");
    </Script>

    <!-- <![endif]-->
    <!--[if IE]>
    <Script type = "text/javascript" >
     window.jQuery || document.write("<script src='../assets/js/jquery1x.js'>"+"<"+"/script>");
    </script>
    <![endif]-->
    <Script type="text/javascript">
        If('ontouchstart' in document.documentElement) document.write("<script src='../../ace/assets/js/jquery.mobile.custom.js'>" + "<" + "/script>");
    </Script>

    <!-- inline scripts related to this page -->
    <Script type="text/javascript">
        jQuery(Function($) {
            $(document).on('click', '.toolbar a[data-target]', function (e) {
                e.preventDefault();
                var target = $(this).data('target');
                $('.widget-box.visible').removeClass('visible');//hide others
                $(target).addClass('visible');//show target
            });
        });



        //you don't need this, just used for changing background
        jQuery(function ($) {
            $('#btn-login-dark').on('click', function (e) {
                $('body').attr('class', 'login-layout');
                $('#id-text2').attr('class', 'white');
                $('#id-company-text').attr('class', 'blue');

                e.preventDefault();
            });
            $('#btn-login-light').on('click', function (e) {
                $('body').attr('class', 'login-layout light-login');
                $('#id-text2').attr('class', 'grey');
                $('#id-company-text').attr('class', 'blue');

                e.preventDefault();
            });
            $('#btn-login-blur').on('click', function (e) {
                $('body').attr('class', 'login-layout blur-login');
                $('#id-text2').attr('class', 'white');
                $('#id-company-text').attr('class', 'light-blue');

                e.preventDefault();
            });

        });
    </Script>
</body>
</html>
