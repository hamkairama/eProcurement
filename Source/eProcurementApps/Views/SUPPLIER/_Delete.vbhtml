@ModelType eProcurementApps.Models.TPROC_SUPPLIER
@Imports eProcurementApps.Helpers

<style>
    .right {
        text-align: right;
    }

    .center {
        text-align: center;
    }

    .tab {
        display: inline-block;
        margin-left: 250px;
    }
</style>
<div class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
        <div class="modal-body no-padding">
            <div class="tabbable">
                <ul class="nav nav-tabs" id="myTab">
                    <li class="active">
                        <a data-toggle="tab" href="#newSupplier">
                            <i class="green ace-icon fa fa-puzzle-piece bigger-120"></i>
                            Detail Vendor
                        </a>
                    </li>

                    <li>
                        <a data-toggle="tab" href="#qualification">
                            <i class="blue ace-icon fa fa-file-text bigger-120"></i>
                            Qualification
                        </a>
                    </li>

                </ul>

                <div class="tab-content">
                    <div id="newSupplier" class="tab-pane fade in active">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Vendor Code </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.VENDOR_CODE</span> <span class="hidden" id="txt_id">@Model.ID</span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><div class="hr hr-18 dotted hr-double"></div></td>
                            </tr>
                            <tr>
                                <td width="500px">
                                    Vendor Information
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Name </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.SUPPLIER_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Alias </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.SUPPLIER_ALIAS_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Address </div>
                                        <div class="profile-info-value ">
                                            <span class="">@Model.SUPPLIER_ADDRESS</span>
                                        </div>
                                    </div>

                                    Contact
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Email </div>
                                        <div class="profile-info-value ">
                                            <span class="">@Model.EMAIL_ADDRESS</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Office </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.OFFICE_NUMBER</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Tax </div>
                                        <div class="profile-info-value ">
                                            <span class="">@Model.TAX_NUMBER</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> CP </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.CONTACT_PERSON</span>
                                        </div>
                                    </div>
                                </td>
                                <td width="500px">
                                    Vendor's Bank Information
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Bank Name </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.BANK_NAME</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Branch </div>
                                        <div class="profile-info-value">
                                            <span class=" ">@Model.BANK_BRANCH </span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Account No </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.BANK_ACCOUNT_NUMBER</span>
                                        </div>
                                    </div>
                                    .
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Mobile </div>
                                        <div class="profile-info-value ">
                                            <span class="">@Model.MOBILE_NUMBER</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Fax </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.FAX_NUMBER</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Website </div>
                                        <div class="profile-info-value">
                                            <span class="">@Model.WEBSITE</span>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2"><div class="hr hr-18 dotted hr-double"></div></td>
                            </tr>

                            <tr>
                                <td width="500px">
                                    Others
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Description </div>
                                        <div class="profile-info-value">
                                            <span class="" id="txt_description" maxlenght="200">@Model.DESCRIPTION</span>
                                        </div>
                                    </div>
                                </td>

                                <td width="500px">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> ~ </div>
                                        <div class="profile-info-value">
                                            <span class="" id="" maxlenght="50"></span>
                                        </div>
                                    </div>


                                </td>
                            </tr>

                        </table>
                    </div>

                    <div id="qualification" class="tab-pane fade">
                        <div align="center"><b>CHECKLIST KUALIFIKASI SUPPLIER</b></div>
                        <div>Nama Supplier : @Model.SUPPLIER_NAME</div>
                        <div>Contact Person : @Model.CONTACT_PERSON</div>
                        <div>Core Business : <span id="txt_core_business" class="freeText">@Model.CORE_BUSINESS</span></div>
                        <div>Nama Barang : <span id="txt_nama_barang" class="freeText">@Model.NAMA_BARANG</span></div>

                        <table border="1" style="align-content:center" align="center">
                            <tr>
                                <th width="50">No</th>
                                <th width="400">Uraian</th>
                                <th width="100">Rating</th>
                                <th width="100">Bobot</th>
                                <th width="100">Score</th>
                            </tr>
                            <tr style="font-weight:bold">
                                <td>1</td>
                                <td colspan="4">Harga</td>
                            </tr>
                            <tr>
                                <td class="center">a</td>
                                <td>Sangat tidak kompetitif</td>
                                <td class="center">1</td>
                                <td class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).H_STK</td>
                            </tr>
                            <tr>
                                <td class="center">b</td>
                                <td>Tidak kompetitif</td>
                                <td class="center">2</td>
                                <td class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).H_TK</td>
                            </tr>
                            <tr>
                                <td class="center">c</td>
                                <td>Kompetitif</td>
                                <td class="center">3</td>
                                <td class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).H_K</td>
                            </tr>
                            <tr>
                                <td class="center">d</td>
                                <td>Sangat kompetitif</td>
                                <td class="center">4</td>
                                <td class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).H_SK</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : @Model.TPROC_SUPP_QUAL(0).H_KET</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr style="font-weight:bold">
                                <td>2</td>
                                <td colspan="4">Administrasi</td>
                            </tr>
                            <tr>
                                <td class="center">a</td>
                                <td>Sistem administrasi tidak ada</td>
                                <td class="center">1</td>
                                <td class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).A_TA</td>
                            </tr>
                            <tr>
                                <td Class="center">b</td>
                                <td> Tidak rapi dan tidak konsisten</td>
                                <td Class="center">1</td>
                                <td Class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).A_TRK</td>
                            </tr>
                            <tr>
                                <td Class="center">c</td>
                                <td> Rapi dan konsisten</td>
                                <td Class="center">1</td>
                                <td Class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).A_RK</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : @Model.TPROC_SUPP_QUAL(0).A_KET</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr style="font-weight:bold">
                                <td>3</td>
                                <td colspan="4"> Fasilitas</td>
                            </tr>
                            <tr>
                                <td Class="center">a</td>
                                <td> Fasilitas sangat tidak memadai</td>
                                <td Class="center">1</td>
                                <td Class="center">4</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).F_STM</td>
                            </tr>
                            <tr>
                                <td Class="center">b</td>
                                <td> Fasilitas kurang memadai</td>
                                <td Class="center">2</td>
                                <td Class="center">4</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).F_KM</td>
                            </tr>
                            <tr>
                                <td Class="center">c</td>
                                <td> Fasilitas memadai</td>
                                <td Class="center">3</td>
                                <td Class="center">4</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).F_M</td>
                            </tr>
                            <tr>
                                <td Class="center">d</td>
                                <td> Fasilitas sangat memadai</td>
                                <td Class="center">4</td>
                                <td Class="center">4</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).F_SM</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td> Keterangan : @Model.TPROC_SUPP_QUAL(0).F_KET</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>



                            <tr style="font-weight:bold">
                                <td>4</td>
                                <td colspan="4"> Penangan Order &Aacute; Keluhan</td>
                            </tr>
                            <tr>
                                <td Class="center">a</td>
                                <td> Lambat dan pasif</td>
                                <td Class="center">1</td>
                                <td Class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).P_LP</td>
                            </tr>
                            <tr>
                                <td Class="center">b</td>
                                <td> Lambat tetapi aktif</td>
                                <td Class="center">2</td>
                                <td Class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).P_LTA</td>
                            </tr>
                            <tr>
                                <td Class="center">c</td>
                                <td> Cepat dan aktif</td>
                                <td Class="center">3</td>
                                <td Class="center">7</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).P_CA</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td> Keterangan : @Model.TPROC_SUPP_QUAL(0).P_KET</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr style="font-weight:bold">
                                <td>5</td>
                                <td colspan="4"> Quality Control</td>
                            </tr>
                            <tr>
                                <td Class="center">a</td>
                                <td> Tidak ada control</td>
                                <td Class="center">1</td>
                                <td Class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).Q_ta</td>
                            </tr>
                            <tr>
                                <td Class="center">b</td>
                                <td> Control sederhana</td>
                                <td Class="center">2</td>
                                <td Class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).Q_CS</td>
                            </tr>
                            <tr>
                                <td Class="center">c</td>
                                <td> Hanya hasil akhir yang dikontrol</td>
                                <td Class="center">3</td>
                                <td Class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).Q_HHA</td>
                            </tr>
                            <tr>
                                <td Class="center">d</td>
                                <td> Setiap langkah pekerjaan dikontrol</td>
                                <td Class="center">4</td>
                                <td Class="center">5</td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).Q_SLP</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td> Keterangan : @Model.TPROC_SUPP_QUAL(0).Q_KET</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td colspan="2"> Total Score</td>
                                <td></td>
                                <td></td>
                                <td class="center">@Model.TPROC_SUPP_QUAL(0).TOTAL_SCORE</td>
                            </tr>
                        </table>

                        <div>
                            <Label for="form-field-5">Komentar/Saran</Label>
                            <textarea Class="form-control" id="form-field-5" placeholder="Default Text"></textarea>
                        </div>

                        <div Class="space-2"></div>
                        <div Class="space-2"></div>

                        <p> Jakarta, @Model.TPROC_SUPP_QUAL(0).CREATED_TIME.ToString("dd MMMM yyyy")</p>

                        <Table>
                            <tr>
                                <td width="300"> Dibuat oleh :</td>
                                <td width="300"> Diperiksa oleh :</td>
                                <td width="300"> Disetujui oleh :</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td> <span id="txt_dibuat_oleh">@Model.TPROC_SUPP_QUAL(0).DIBUAT_OLEH</span></td>
                                <td> <span id="txt_diperiksa_oleh" Class="">@Model.TPROC_SUPP_QUAL(0).DIPERIKSA_OLEH</span></td>
                                <td> <span id="txt_disetujui_oleh" Class="">@Model.TPROC_SUPP_QUAL(0).DISETUJUI_OLEH</span></td>
                            </tr>
                        </Table>


                    </div>
                </div>
            </div>




            <div class="vspace-6-sm"></div>
        </div>
        <div class='modal-footer no-margin-top'>
            Are you sure want to delete ?
            @Html.Raw(Labels.ButtonForm("ConfirmDelete"))
            @Html.Raw(Labels.ButtonForm("No"))

        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>





