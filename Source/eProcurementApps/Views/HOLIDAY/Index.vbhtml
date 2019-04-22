﻿@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "Holiday"
    ViewBag.Setup = "active open"
    ViewBag.IndexHoliday = "active"
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


@Html.Partial("_List")
<script src="~/Scripts/Controllers/HOLIDAYController.js"></script>

