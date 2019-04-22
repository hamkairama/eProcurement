@Imports eProcurementApps.Helpers
@Code
    ViewBag.Breadcrumbs = "Setup"
    ViewBag.Title = "User"
    ViewBag.Setup = "active open"
    ViewBag.IndexUser = "active"
End Code


<div class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Select User Form Active Directory 
            </div>
        </div>
        <div class="modal-body no-padding">
            <div class="radio">
                <label>
                    <input name="form-field-radio" type="radio" class="ace" id="rb_user_id" onclick="Show(0)" />
                    <span class="lbl">User Id</span>
                </label>
                <label>
                    <input name="form-field-radio" type="radio" class="ace" id="rb_user_email" onclick="Show(1)" />
                    <span class="lbl">Email</span>
                </label>
            </div>

            <div style="display:none" id="div_user_id">
                <input type="text" id="txt_user_id" placeholder="input user id" onkeypress="SearchKey(event, 0)" /> 
                <a onclick="Search(0)" title="Search" href="#">@Html.Raw(Labels.ButtonForm("FindAD"))</a>                             
            </div>

            <div style="display:none" id="div_user_email">
                <input type="text" id="txt_user_email" placeholder="input email" onkeypress="SearchKey(event, 1)" />
                <a onclick="Search(1)" title="Search" href="#">@Html.Raw(Labels.ButtonForm("FindAD"))</a>
            </div>

            @*<input type="text" value="@TempData("flag")" />*@
            <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
            <table class="table table-striped table-bordered table-hover" id="table_id">
                <thead>
                    <tr>
                        <th>No</th>
                        <th>User Id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Department</th>
                        <th>action</th>
                    </tr>
                </thead>
                <tbody id="table_ad"></tbody>
            </table>
        </div>
    </div>
</div>


<Script src="~/Scripts/Standard/StandardModal.js"></Script>