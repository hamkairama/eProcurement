﻿@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Role"
    ViewBag.Setup = "active open"
    ViewBag.IndexRole = "active"
End Code

@If ViewBag.Message <> "" Then
    @<div Class="alert alert-block alert-error">
        <Button Class="close" data-dismiss="alert" type="button">
            <i Class="icon-remove"></i>
        </Button>
        <i Class="icon-warning-sign red"></i>
        @ViewBag.Message
    </div>
End If

@Html.Partial("_List")
<script src="~/Scripts/Controllers/ROLEController.js"></script>
