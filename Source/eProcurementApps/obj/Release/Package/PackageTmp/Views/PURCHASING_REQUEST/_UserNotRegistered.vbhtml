@Imports eProcurementApps.Helpers


<div Class="modal-dialog">
    <div class="error-container">
        <div class="well">
            <h1 class="grey lighter smaller">
                <span class="blue bigger-125">
                    <i class="ace-icon fa fa-random"></i>
                    007
                </span>
                You don't have the profile
            </h1>

            <hr />
            <h3 class="lighter smaller">
                But you can request with following this
            </h3>

            <div class="space"></div>

            <div id="input_wa">
                <div class="required"> Work Area <label id="required_form-field-select-4"></label></div>
                <div class="col-lg-12 item-required-select">
                    <div class="space-2"></div>
                    <select multiple="" class="chosen-select form-control" id="form-field-select-4" data-placeholder="Choose work area...">
                        @For Each item In Dropdown.WorkAreaForCreateUser()
                            @<option>@item</option>
                        Next
                    </select>
                </div>
            </div>

            <hr />


            <div id="action_send_email">
                <ul class="list-unstyled spaced inline bigger-110 margin-15">
                    <li>
                        <i class="ace-icon fa fa-hand-o-right blue"></i>
                        <a class="red" href="#" onclick="ActionEmailCreateUser()" data-toggle="modal" title="Sent the email to Hamka Irama">
                            Sent the email to administrator
                        </a>
                    </li>
                </ul>
            </div>


            <div class="" id="loader"></div>
            <div style="visibility:hidden" id="email_sent">
                Email has been sent
                <br /> For the detail information, please contact eprocurement administrator.
            </div>

            <hr />
            <div class="space"></div>

            <div class="center">
                @Html.Raw(Labels.ButtonForm("Close"))
            </div>
        </div>
    </div>
</div>

<Script src="~/Scripts/Standard/StandardProfile.js"></Script>
<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Scripts/Controllers/PURCHASING_REQUESTController.js"></script>

