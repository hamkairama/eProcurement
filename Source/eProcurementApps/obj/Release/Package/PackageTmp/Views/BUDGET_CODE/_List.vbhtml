@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_BUDGET_CODE)
@Imports eProcurementApps.Helpers

@If ViewBag.Message <> "" Then
    @<div Class="alert alert-block alert-error">
        <Button Class="close" data-dismiss="alert" type="button">
            <i Class="icon-remove"></i>
        </Button>
        <i Class="icon-warning-sign red"></i>
        @ViewBag.Message
    </div> End If

<div Class="main-content">
    <div Class="page-content">
        <div Class="row">
            @If Model.Count = 0 Then

                @<div Class="clearfix form-action">
                    <div Class="col-lg-12">
                        <div Class="modal-footer no-margin-top">
                            <a class="red" href="@Url.Action("Create", "BUDGET_CODE")" title="Create">
                                @Html.Raw(Labels.ButtonForm("Create"))
                            </a>
                        </div>
                    </div>
                </div>

            Else
                @For Each item In Model
                    @<div Class="col-sm-5">
                        <div Class="widget-box">
                            <div Class="widget-header">
                                <h4 Class="widget-title">SUN Budget Table</h4>
                            </div>
                            <div Class="widget-body" id="testing">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Table Budget </div>
                                        <div class="profile-info-value">
                                            <span> @item.TABLE_BUDGET</span>
                                        </div>
                                    </div>

                                    <div Class="profile-info-row">
                                        <div Class="profile-info-name"> Table for Usage </div>
                                        <div Class="profile-info-value">
                                            <span>@item.TABLE_BUDGET_USAGE</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Is Active </div>
                                        <div class="profile-info-value">
                                            @if item.IS_ACTIVE = 1 Then
                                                @<input type="checkbox" id="txt_is_active" checked="checked" />
                                            Else
                                                @<input type="checkbox" id="txt_is_active" />
                                            End If
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    @<div Class="col-sm-7">
                        <div Class="widget-box">
                            <div Class="widget-header">
                                <h4 Class="widget-title">T. Table</h4>
                            </div>
                            <div Class="widget-body" id="testing">
                                <div class="profile-user-info">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Table T1 </div>
                                        <div class="profile-info-value">
                                            <span>@item.TABLE_T1</span>
                                        </div>
                                    </div>

                                    <div Class="profile-info-row">
                                        <div Class="profile-info-name"> Table T2 </div>
                                        <div Class="profile-info-value">
                                            <span> @item.TABLE_T2</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Table T3 </div>
                                        <div class="profile-info-value">
                                            <span> @item.TABLE_T3</span>
                                        </div>
                                    </div>

                                    <div Class="profile-info-row">
                                        <div Class="profile-info-name"> Table T5 </div>
                                        <div Class="profile-info-value">
                                            <span>@item.TABLE_T5</span>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    @<div Class="col-sm-12">
                        <div Class="widget-box">
                            <div Class="widget-header">
                                <h4 Class="widget-title">Export Purpose</h4>
                            </div>
                            <div Class="widget-body" id="testing">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Item</th>
                                            <th>Acc Code</th>
                                            <th>Move Type</th>
                                            <th>Start</th>
                                            <th>End</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Office Supplies</td>
                                            <td>@item.CD_OFFICE_SUPPLIE_USAGE</td>
                                            <td>@item.CD_OFFICE_SUPPLIE_MT</td>
                                            <td>@item.CD_OFFICE_SUPPLIE_START</td>
                                            <td>@item.CD_OFFICE_SUPPLIE_END</td>
                                        </tr>
                                        <tr>
                                            <td>Printing</td>
                                            <td>@item.CD_PRINTING_USAGE</td>
                                            <td>@item.CD_PRINTING_MT</td>
                                            <td>@item.CD_PRINTING_START</td>
                                            <td>@item.CD_PRINTING_END</td>
                                        </tr>
                                        <tr>
                                            <td>Issued</td>
                                            <td>@item.CD_ISSUED_USAGE</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </tbody>

                                </table>
                            </div>
                        </div>
                    </div>

                    @<div Class="clearfix form-action">
                        <div Class="col-lg-12">
                            <div Class="modal-footer no-margin-top">
                                <a class="green" href="@Url.Action("Edit", "BUDGET_CODE", New With {.id = item.ID})" title="Edit">
                                    @Html.Raw(Labels.IconAction("Edit"))
                                </a>

                                <a class="red" href="#" onclick="Modal('/Delete/','@item.ID','.dialogForm')" data-toggle="modal" title="Delete">
                                    @Html.Raw(Labels.IconAction("Delete"))
                                </a>
                            </div>
                        </div>
                    </div>

                Next

            End If


        </div>
    </div>
</div>
