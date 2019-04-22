@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Request"
    ViewBag.Title = "Request User"
    ViewBag.Request = "active open"
    ViewBag.IndexRequestUser = "active"
    ViewBag.Title = "Request User"
End Code

@*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
<div class="col-lg-12">
    <div class="pricing-span">
        <a class="red" href="@Url.Action("Create", "User", New With {.flag = 1})" title="Request User Create">
            <div class="widget-box pricing-box-small widget-color-blue">
                <div class="widget-header center">
                    <h5 class="widget-title bigger lighter">Create</h5>
                    <div class="space-4"></div>
                    <i class="ace-icon fa fa-pencil-square-o bigger-300 white"></i>
                </div>
                <div class="widget-body">
                    <div class="widget-main no-padding">
                    </div>
                </div>
            </div>
        </a>
    </div>
    <div class="pricing-span">
        <a class="red" href="@Url.Action("Edit", "User", New With {.id = Convert.ToDecimal(Session("USER_ID_ID")), .flag = 1})" title="Request User Edit">
            <div class="widget-box pricing-box-small widget-color-orange">
                <div class="widget-header center">
                    <h5 class="widget-title bigger lighter">Update</h5>
                    <div class="space-4"></div>
                    <i class="ace-icon fa fa-pencil bigger-300 white"></i>
                </div>
                <div class="widget-body">
                    <div class="widget-main no-padding">
                    </div>
                </div>
            </div>

        </a>
    </div>
    <div class="pricing-span">
        <a class="red" href="#" onclick="InputDelete()" title="Request User Delete">
            <div class="widget-box pricing-box-small widget-color-red3">
                <div class="widget-header center">
                    <h5 class="widget-title bigger lighter">Delete</h5>
                    <div class="space-4"></div>
                    <i class="ace-icon fa fa-trash-o bigger-300 white"></i>
                </div>
                <div class="widget-body">
                    <div class="widget-main no-padding">
                    </div>
                </div>
            </div>
        </a>
    </div>
</div>


<div class="col-lg-12">
    <div style="display:none" id="delete_area">
        <div class="space-4"></div>
        <div class="hr hr-18 dotted hr-double"></div>
        Please select the user want to delete
        <div class="profile-info-row">
            <div class="profile-info-name"> User </div>
            <div class="profile-info-value">
                <span Class="" id="user_id"></span>
                @Html.DropDownList("dropdownList_user_del", Dropdown.GetAllUser, New With {.style = "width:  200px; "})
            </div>

            <button type='submit' onclick="GoDelete()" class='btn btn-sm btn-success btn-white btn-round'>
                <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                Go Delete
            </button>
        </div>
    </div>
</div>

<script src="~/Scripts/Controllers/REQUESTController.js"></script>