<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>@ViewBag.Title - eProcurement</title>
    <link href="~/Content/jquery-ui.css" rel="stylesheet" />


    <link href="~/Ace/assets/css/bootstrap.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/font-awesome.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/ace.css" rel="stylesheet" />



    <script src="~/Ace/assets/js/jquery.js"></script>
    <script src="~/Ace/assets/js/jquery1x.js"></script>
    <script src="~/Ace/assets/js/jquery.mobile.custom.js"></script>


    <link href="~/Content/Custom/CustomRequired.css" rel="stylesheet" />

    <link href="~/Ace/assets/css/jquery-ui.custom.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/chosen.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/datepicker.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/bootstrap-timepicker.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/daterangepicker.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/bootstrap-datetimepicker.css" rel="stylesheet" />



    <link href="~/Ace/assets/css/jquery-ui.custom.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/datepicker.css" rel="stylesheet" />
    <link href="~/Ace/assets/css/bootstrap-editable.css" rel="stylesheet" />

    <link href="~/Content/Custom/CustomLoading.css" rel="stylesheet" />

    <style>
        body {
            font-family: sans-serif, Arial;
        }
    </style>

</head>
<body class="no-skin">
    <div id="navbar" class="navbar navbar-default">
        @Html.Partial("_Navbar")
    </div>
    <div class="main-container" id="main-container">
        <script type="text/javascript">
            try { ace.settings.check('main-container', 'fixed') } catch (e) { }
        </script>
        <div id="sidebar" class="sidebar                  responsive">
            @Html.Partial("_Sidebar")
        </div>
        <div class="main-content">
            <div class="main-content-inner">
                @Html.Partial("_Breadcrumbs")
                <div class="page-content">
                    @Html.Partial("_PageHeader")
                    <div class="row" id="renderBody">
                        <div class="col-xs-12">
                            @RenderSection("scripts", False)
                            @RenderBody()
                            <div class="dialogForm"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer">
            @Html.Partial("_Footer")
        </div>
    </div>

    <script src="~/Ace/assets/js/bootstrap.js"></script>
    <script src="~/Ace/assets/js/ace/elements.scroller.js"></script>
    <script src="~/Ace/assets/js/ace/elements.fileinput.js"></script>
    <script src="~/Ace/assets/js/ace/elements.wysiwyg.js"></script>

    <script src="~/Ace/assets/js/ace/ace.js"></script>
    <script src="~/Ace/assets/js/ace/ace.ajax-content.js"></script>
    <script src="~/Ace/assets/js/ace/ace.sidebar.js"></script>
    <script src="~/Ace/assets/js/ace/ace.sidebar-scroll-1.js"></script>
    <script src="~/Ace/assets/js/ace/ace.submenu-hover.js"></script>
    <script src="~/Ace/assets/js/ace/ace.widget-box.js"></script>
    <script src="~/Ace/assets/js/ace-extra.js"></script>
    <script src="~/Ace/assets/js/chosen.jquery.js"></script>
    <script src="~/Ace/assets/js/jquery-ui.custom.js"></script>
    <script src="~/Ace/assets/js/jquery.ui.touch-punch.js"></script>
    <script src="~/Ace/assets/js/bootbox.js"></script>
    <script src="~/Ace/assets/js/date-time/bootstrap-datepicker.js"></script>
    <script src="~/Ace/assets/js/bootstrap-wysiwyg.js"></script>
    <script src="~/Ace/assets/js/x-editable/bootstrap-editable.js"></script>
    <script src="~/Ace/assets/js/x-editable/ace-editable.js"></script>
    <script src="~/Ace/assets/js/jquery.maskedinput.js"></script>

    <script src="~/Scripts/jquery-ui.js"></script>
    <script src="~/Scripts/Standard/StandardModal.js"></script>
    <script src="~/Scripts/Helpers/Common.js"></script>
    <script src="~/Scripts/Custom/CustomRequired.js"></script>

</body>
</html>
