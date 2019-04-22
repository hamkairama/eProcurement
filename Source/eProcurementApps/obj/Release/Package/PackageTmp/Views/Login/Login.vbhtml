@ModelType eProcurementApps.Models.LOGIN_MODEL
@code
    Layout = ""
End Code
<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Login Page - eProcurement</title>

    <meta name="description" content="User login page" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <link href="~/Ace/assets/css/bootstrap.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/font-awesome.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/ace-fonts.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/ace.css" rel="stylesheet" />

    <script src="~/Ace/assets/js/jquery.js"></script>
    <script src="~/Ace/assets/js/jquery1x.js"></script>
    <script src="~/Ace/assets/js/jquery.mobile.custom.js"></script>


</head>

<body class="login-layout">
    <div class="main-container">
        <div class="main-content">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="login-container">
                        <div class="center">
                            <h1>
                                <img src="~/Images/eproc2.ico" sizes="50x50" />
                                <span class="red">eProcurement</span>
                            </h1>
                        </div>

                        <div class="space-6"></div>

                        <div class="position-relative">
                            <div id="login-box" class="login-box visible widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header blue lighter bigger">
                                            <i class="ace-icon fa fa-coffee green"></i>
                                            Please Enter Window Account
                                        </h4>

                                        <div class="space-6"></div>

                                        @Using (Html.BeginForm())
                                            @Html.AntiForgeryToken()

                                            @<div class="form-horizontal">
                                                @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
                                                <fieldset>
                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            @Html.EditorFor(Function(model) model.USER_ID, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "User Id", .value = "hamkair", .text = "hamkair"}})
                                                            <i class="ace-icon fa fa-user"></i>
                                                        </span>
                                                    </label>

                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">
                                                            @Html.EditorFor(Function(model) model.PASSWORD, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Password", .value = "Danu123$%", .text = "Danu123$%"}})
                                                            <i class="ace-icon fa fa-lock"></i>
                                                        </span>
                                                    </label>

                                                    <div class="space"></div>
                                                    <div class="clearfix">
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
                                    </div>
                                </div>
                            </div>
                            <h4 class="green" id="id-company-text">&copy; Indonesia</h4>
                        </div>

                        <div class="navbar-fixed-top align-right">
                            <br />
                            &nbsp;
                            <a id="btn-login-dark" href="#">Dark</a>
                            &nbsp;
                            <span class="blue">/</span>
                            &nbsp;
                            <a id="btn-login-blur" href="#">Blur</a>
                            &nbsp;
                            <span class="blue">/</span>
                            &nbsp;
                            <a id="btn-login-light" href="#">Light</a>
                            &nbsp; &nbsp; &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script src="~/Scripts/Standard/StandardLogin.js"></script>