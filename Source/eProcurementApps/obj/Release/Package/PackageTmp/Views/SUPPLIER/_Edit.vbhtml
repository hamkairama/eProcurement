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
                            New Vendor
                        </a>
                    </li>

                    <li>
                        <a data-toggle="tab" href="#documentChecking">
                            <i class="blue ace-icon fa fa-file-text bigger-120"></i>
                            Document checking
                        </a>
                    </li>

                    <li>
                        <a data-toggle="tab" href="#qualification" onclick="GetSupplierQualEval()">
                            <i class="blue ace-icon fa fa-file-text bigger-120"></i>
                            Qualification
                        </a>
                    </li>

                    <li>
                        <a data-toggle="tab" href="#evaluation" onclick="GetSupplierQualEval()">
                            <i class="blue ace-icon fa fa-file-text bigger-120"></i>
                            Evaluation
                        </a>
                    </li>

                </ul>

                <div class="tab-content">
                    <div id="newSupplier" class="tab-pane fade in active">
                        <table>
                            <tr>
                                <td width="500px">
                                    <div class="profile-info-row  hidden">
                                        <div class="profile-info-name"> ID </div>
                                        <div class="profile-info-value">
                                            <span class="" id="txt_id" maxlenght="">@Model.ID</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Vendor Code </div>
                                        <div class="profile-info-value">
                                            <span class="" id="txt_vendor_code" maxlenght="50">@Model.VENDOR_CODE</span>
                                            <label id="required_txt_vendor_code"></label>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Legalisation </div>
                                        <div class="profile-info-value">
                                                @if Model.ROW_STATUS = 0 Then
                                                    @<label>
                                                        <input name="form-field-q" type="radio" id="rb_legal"  checked="checked" />
                                                        <span Class="lbl">VML</span>
                                                    </label>
                                                    @<Label>
                                                        <input name="form-field-q" type="radio" id="rb_ilegal" />
                                                        <span Class="lbl">Non-VML</span>
                                                    </Label>
                                               Else
                                                    @<label>
                                                        <input name = "form-field-q" type="radio" id="rb_legal" />
                                                        <span Class="lbl">VML</span>
                                                    </label>
                                                    @<Label>
                                                        <input name = "form-field-q" type="radio" id="rb_ilegal" checked="checked" />
                                                        <span Class="lbl">Non-MVL</span>
                                                    </label>
                                                End If                                                
                                        </div>
                                    </div>
                                </td>
                                <td width = "500px" >
                                    <div class="profile-info-row">
                                                            <div class="profile-info-name"> B.U. Owner </div>
                                                            <div class="profile-info-value">
                                                                <span class="freeText" id="txt_b_unit_owner" maxlenght="50">@Model.B_UNIT_OWNER</span>
                                            <label id="required_txt_b_unit_owner"></label>
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
                                        <div class="profile-info-name required"> Name </div>
                                        <div class="profile-info-value item-required">
                                            <span class="freeText" id="txt_supplier_name" maxlenght="100">@Model.SUPPLIER_NAME</span>
                                            <label id="required_txt_supplier_name"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Alias </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_supplier_alias_name" maxlenght="50">@Model.SUPPLIER_ALIAS_NAME</span>
                                            <label id="required_txt_supplier_alias_name"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Address </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_supplier_address" maxlenght="200">@Model.SUPPLIER_ADDRESS</span>
                                            <label id="required_txt_supplier_address"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> City </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_city" maxlenght="100">@Model.CITY</span>
                                            <label id="required_txt_city"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Core Business </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_core_business" maxlenght="200">@Model.CORE_BUSINESS</span>
                                            <label id="required_txt_core_business"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Nama Barang </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_nama_barang" maxlenght="100">@Model.NAMA_BARANG</span>
                                            <label id="required_txt_nama_barang"></label>
                                        </div>
                                    </div>
                                </td>
                                <td width="500px">
                                    Vendor's Bank Information
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Bank Name </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_bank_name" maxlenght="50">@Model.BANK_NAME</span>
                                            <label id="required_txt_bank_name"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Branch </div>
                                        <div class="profile-info-value">
                                            <span class="freeText " id="txt_bank_branch" maxlenght="50">@Model.BANK_BRANCH</span>
                                            <label id="required_txt_bank_branch"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Account No </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_bank_account_number" maxlenght="50">@Model.BANK_ACCOUNT_NUMBER</span>
                                            <label id="required_txt_bank_account_number"></label>
                                        </div>
                                    </div>   
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> NPWP </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_npwp" maxlenght="50">@Model.NPWP</span>
                                        </div>
                                    </div>                                 
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2"><div class="hr hr-18 dotted hr-double"></div></td>
                            </tr>

                            <tr>
                                <td width="500px">                                    
                                    Contact
                                    <div class="profile-info-row">
                                        <div class="profile-info-name required"> CP </div>
                                        <div class="profile-info-value item-required">
                                            <span class="freeText" id="txt_contact_person" maxlenght="50">@Model.CONTACT_PERSON</span>
                                            <label id="required_txt_contact_person"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Email </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_email_address" maxlenght="50">@Model.EMAIL_ADDRESS</span>
                                            <label id="required_txt_email_address"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Office </div>
                                        <div class="profile-info-value ">
                                            <span class="freeText" id="txt_office_number" maxlenght="50">@Model.OFFICE_NUMBER</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Tax </div>
                                        <div class="profile-info-value ">
                                            <span class="freeText" id="txt_tax_number" maxlenght="50">@Model.TAX_NUMBER</span>
                                        </div>
                                    </div>

                                </td>
                                <td width="500px">                                   
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Mobile </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_mobile_number" maxlenght="50">@Model.MOBILE_NUMBER</span>
                                            <label id="required_txt_mobile_number"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Fax </div>
                                        <div class="profile-info-value ">
                                            <span class="freeText" id="txt_fax_number" maxlenght="50">@Model.FAX_NUMBER</span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Website </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_website" maxlenght="50">@Model.WEBSITE</span>
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
                                            <span class="freeText" id="txt_description" maxlenght="200">@Model.DESCRIPTION</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Efective Dt. </div>
                                        <div class="profile-info-value">
                                            <span class="monthYearText" id="txt_efective_dt" maxlenght="20">@Model.EFFECTIVE_DATE</span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Schedule Evaluation </div>
                                        <div class="profile-info-value">
                                            <span class="monthYearText" id="txt_schedule_eval" maxlenght="20">@Model.SCHEDULE_EVALUATION</span>
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

                    <div id="documentChecking" class="tab-pane fade">
                        <div class="profile-info-row">
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().WEIGHT_FACTOR = 1 Then
                                    @<input type = "checkbox" Class="check-doc" id="cb_weigth_factor" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_weigth_factor" />
                                End If
                                <span class="lbl">Weighted Factor / Qualification</span>
                            </div>
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().BRIDGER_SCAN = 1 Then
                                    @<input type="checkbox" class="check-doc" id="cb_bridger_scan" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_bridger_scan" />
                                End If
                                <span class="lbl">Bridger Scan</span>
                            </div>
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().NDA = 1 Then
                                    @<input type="checkbox" class="check-doc" id="cb_nda" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_nda" />
                                End If
                                <span class="lbl">NDA</span>
                            </div>
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().CIDCI = 1 Then
                                    @<input type="checkbox" class="check-doc" id="cb_cidci" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_cidci" />
                                End If
                                <span class="lbl">CIDCI</span>
                            </div>
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().LEGAL_DOC = 1 Then
                                    @<input type="checkbox" class="check-doc" id="cb_legal_doc" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_legal_doc" />
                                End If
                                <span class="lbl">Legal Documents</span>
                            </div>
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().AGGREEMENT = 1 Then
                                    @<input type="checkbox" class="check-doc" id="cb_aggreement" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_aggreement" />
                                End If
                                <span class="lbl">Agreement</span>
                            </div>
                            <div class="profile-info-value">
                                @if Model.TPROC_SUPP_DOC.FirstOrDefault().VALIDITY_CHECKING = 1 Then
                                    @<input type="checkbox" class="check-doc" id="cb_validity_checking" checked="checked" />
                                Else
                                    @<input type="checkbox" class="check-doc" id="cb_validity_checking" />
                                End If
                                <span class="lbl">Vendor Validity Checking</span>
                            </div>
                        </div>
                    </div>

                    <div id="qualification" class="tab-pane fade">
                        <div align="center"><b>CHECKLIST KUALIFIKASI SUPPLIER</b></div>
                        <div>Nama Supplier : <span id="qual_nama_supplier"></span></div>
                        <div>Contact Person : <span id="qual_cp"></span> </div>
                        <div> Core Bussines : <span id="qual_core_business"></span></div>
                        <div> Nama Barang : <span id="qual_nama_barang"></span></div>

                        <table border="1" style="align-content:center" align="center" id="dataTableQual">
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
                                <td><input style="text-align:center" id="txt_h_stk" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).H_STK" /></td>
                            </tr>
                            <tr>
                                <td class="center">b</td>
                                <td>Tidak kompetitif</td>
                                <td class="center">2</td>
                                <td class="center">7</td>
                                <td><input style="text-align:center" id="txt_h_tk" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).H_TK" /></td>
                            </tr>
                            <tr>
                                <td class="center">c</td>
                                <td>Kompetitif</td>
                                <td class="center">3</td>
                                <td class="center">7</td>
                                <td><input style="text-align:center" id="txt_h_k" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).H_K" /></td>
                            </tr>
                            <tr>
                                <td class="center">d</td>
                                <td>Sangat kompetitif</td>
                                <td class="center">4</td>
                                <td class="center">7</td>
                                <td><input style="text-align:center" id="txt_h_sk" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).H_SK" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : <input style="text-align:center" size=100 id="txt_h_ket" class="qual" value="@Model.TPROC_SUPP_QUAL(0).H_KET"/></td>
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
                                <td><input style="text-align:center" id="txt_a_ta" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).A_TA" /></td>
                            </tr>
                            <tr>
                                <td class="center">b</td>
                                <td>Tidak rapi dan tidak konsisten</td>
                                <td class="center">1</td>
                                <td class="center">5</td>
                                <td><input style="text-align:center" id="txt_a_trk" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).A_TRK" /></td>
                            </tr>
                            <tr>
                                <td class="center">c</td>
                                <td>Rapi dan konsisten</td>
                                <td class="center">1</td>
                                <td class="center">5</td>
                                <td><input style="text-align:center" id="txt_a_rk" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).A_RK" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : <input style="text-align:center" size=100 id="txt_a_ket" class="qual" value="@Model.TPROC_SUPP_QUAL(0).A_KET"/></td>
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
                                <td colspan="4">Fasilitas</td>
                            </tr>
                            <tr>
                                <td class="center">a</td>
                                <td>Fasilitas sangat tidak memadai</td>
                                <td class="center">1</td>
                                <td class="center">4</td>
                                <td><input style="text-align:center" id="txt_f_stm" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).F_STM" /></td>
                            </tr>
                            <tr>
                                <td class="center">b</td>
                                <td>Fasilitas kurang memadai</td>
                                <td class="center">2</td>
                                <td class="center">4</td>
                                <td><input style="text-align:center" id="txt_f_km" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).F_KM" /></td>
                            </tr>
                            <tr>
                                <td class="center">c</td>
                                <td>Fasilitas memadai</td>
                                <td class="center">3</td>
                                <td class="center">4</td>
                                <td><input style="text-align:center" id="txt_f_m" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).F_M" /></td>
                            </tr>
                            <tr>
                                <td class="center">d</td>
                                <td>Fasilitas sangat memadai</td>
                                <td class="center">4</td>
                                <td class="center">4</td>
                                <td><input style="text-align:center" id="txt_f_sm" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).F_SM" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : <input style="text-align:center" size=100 id="txt_f_ket" class="qual" value="@Model.TPROC_SUPP_QUAL(0).F_KET" /></td>
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
                                <td colspan="4">Penangan Order &Aacute; Keluhan</td>
                            </tr>
                            <tr>
                                <td class="center">a</td>
                                <td>Lambat dan pasif</td>
                                <td class="center">1</td>
                                <td class="center">7</td>
                                <td><input style="text-align:center" id="txt_p_lp" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).P_LP" /></td>
                            </tr>
                            <tr>
                                <td class="center">b</td>
                                <td>Lambat tetapi aktif</td>
                                <td class="center">2</td>
                                <td class="center">7</td>
                                <td><input style="text-align:center" id="txt_p_lta" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).P_LTA" /></td>
                            </tr>
                            <tr>
                                <td class="center">c</td>
                                <td>Cepat dan aktif</td>
                                <td class="center">3</td>
                                <td class="center">7</td>
                                <td><input style="text-align:center" id="txt_p_ca" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).P_CA" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : <input style="text-align:center" size=100 id="txt_p_ket" class="qual" value="@Model.TPROC_SUPP_QUAL(0).P_KET" /></td>
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
                                <td colspan="4">Quality Control</td>
                            </tr>
                            <tr>
                                <td class="center">a</td>
                                <td>Tidak ada control</td>
                                <td class="center">1</td>
                                <td class="center">5</td>
                                <td><input style="text-align:center" id="txt_q_ta" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).Q_TA" /></td>
                            </tr>
                            <tr>
                                <td class="center">b</td>
                                <td>Control sederhana</td>
                                <td class="center">2</td>
                                <td class="center">5</td>
                                <td><input style="text-align:center" id="txt_q_cs" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).Q_CS" /></td>
                            </tr>
                            <tr>
                                <td class="center">c</td>
                                <td>Hanya hasil akhir yang dikontrol</td>
                                <td class="center">3</td>
                                <td class="center">5</td>
                                <td><input style="text-align:center" id="txt_q_hha" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).Q_HHA" /></td>
                            </tr>
                            <tr>
                                <td class="center">d</td>
                                <td>Setiap langkah pekerjaan dikontrol</td>
                                <td class="center">4</td>
                                <td class="center">5</td>
                                <td><input style="text-align:center" id="txt_q_slp" class="qual" onkeyup="RefreshTotScoreQual()" value="@Model.TPROC_SUPP_QUAL(0).Q_SLP" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Keterangan : <input style="text-align:center" size=100 id="txt_q_ket" class="qual" value="@Model.TPROC_SUPP_QUAL(0).Q_KET"/></td>
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
                                <td colspan="2">Total Score</td>
                                <td></td>
                                <td></td>
                                <td><input style="text-align:center" id="txt_qual_total_score" class="qual" value="@Model.TPROC_SUPP_QUAL(0).TOTAL_SCORE" readonly="readonly" /></td>
                            </tr>
                        </table>

                        <div>
                            <label for="form-field-5">Komentar/Saran</label>
                            <textarea class="form-control" id="komentar_qual" >@Model.TPROC_SUPP_QUAL(0).KOMENTAR_SARAN</textarea>
                        </div>

                        <div class="space-2"></div>
                        <div class="space-2"></div>

                        <p> Jakarta, @Model.TPROC_SUPP_QUAL(0).CREATED_TIME.ToString("dd MMMM yyyy")</p>

                        <table>
                            <tr>
                                <td width="300">Dibuat oleh :</td>
                                <td width="300">Diperiksa oleh :</td>
                                <td width="300">Disetujui oleh :</td>
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
                                <td><span id="txt_dibuat_oleh_qual" class="freeText">@Model.TPROC_SUPP_QUAL(0).DIBUAT_OLEH</span></td>
                                <td><span id="txt_diperiksa_oleh_qual" class="freeText">@Model.TPROC_SUPP_QUAL(0).DIPERIKSA_OLEH</span></td>
                                <td><span id="txt_disetujui_oleh_qual" class="freeText">@Model.TPROC_SUPP_QUAL(0).DISETUJUI_OLEH</span></td>
                            </tr>
                        </table>
                    </div>

                    <div id="evaluation" Class="tab-pane fade">
                        <div align="right">
                            <button class='btn btn-sm btn-success btn-white btn-round' id="export" onClick="fnExcelReport('export', 'table-eval', 'Supplier')">
                                <i class="fa fa-fa-file-excel-o bigger-110 green"></i>
                                Export
                            </button>
                        </div>

                        <div id="table-eval">
                            <div align="center"><b>CHECKLIST EVALUASI SUPPLIER</b></div>
                            <div> Nama Supplier : <span id="eval_nama_supplier"></span></div>
                            <div> Contact Person : <span id="eval_cp"></span> </div>
                            <div> Core Bussines : <span id="eval_core_business"></span></div>
                            <div> Nama Barang : <span id="eval_nama_barang"></span></div>

                            <table border="1" style="align-content:center" align="center" id="dataTableEval">
                                <tr>
                                    <th width="50">No</th>
                                    <th width="400">Uraian</th>
                                    <th width="100">Rating</th>
                                    <th width="100">Bobot</th>
                                    <th width="100">Score</th>
                                </tr>
                                <tr style="font-weight:bold">
                                    <td>1</td>
                                    <td colspan="4">Fasilitas</td>
                                </tr>
                                <tr>
                                    <td class="center">a</td>
                                    <td>Fasilitas sangat tidak memadai</td>
                                    <td class="center">1</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_STM" value="@Model.TPROC_SUPP_EVAL(0).F_STM" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Fasilitas kurang memadai</td>
                                    <td class="center">2</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_KM" value="@Model.TPROC_SUPP_EVAL(0).F_KM" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Fasilitas memadai</td>
                                    <td class="center">3</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_M" value="@Model.TPROC_SUPP_EVAL(0).F_M" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">d</td>
                                    <td>Fasilitas Sangat Memadai</td>
                                    <td class="center">4</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_SM" value="@Model.TPROC_SUPP_EVAL(0).F_SM" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 class="eval" id="F_KET" value="@Model.TPROC_SUPP_EVAL(0).F_KET" /></td>
                                    <td colspan="3"></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="A_TA" value="@Model.TPROC_SUPP_EVAL(0).A_TA" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Tidak rapi dan tidak konsisten</td>
                                    <td Class="center">2</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="A_TRK" value="@Model.TPROC_SUPP_EVAL(0).A_TRK" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Rapi dan konsisten</td>
                                    <td Class="center">3</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="A_RK" value="@Model.TPROC_SUPP_EVAL(0).A_RK" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size="100" class="eval" id="A_KET" value="@Model.TPROC_SUPP_EVAL(0).A_KET" /></td>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td colspan="5"></td>
                                </tr>
                                <tr style="font-weight:bold">
                                    <td>3</td>
                                    <td colspan="4"> Harga</td>
                                </tr>
                                <tr>
                                    <td Class="center">a</td>
                                    <td> Tidak kompetitif</td>
                                    <td Class="center">1</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="H_TK" value="@Model.TPROC_SUPP_EVAL(0).H_TK" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Kompetitif</td>
                                    <td Class="center">2</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="H_K" value="@Model.TPROC_SUPP_EVAL(0).H_K" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Sangat Kompetitif</td>
                                    <td Class="center">3</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="H_SK" value="@Model.TPROC_SUPP_EVAL(0).H_SK" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> Keterangan : <input style="text-align:center" size="100" class="eval" id="H_KET" value="@Model.TPROC_SUPP_EVAL(0).H_KET" /></td>
                                    <td colspan="3"></td>
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
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="PO_LP" value="@Model.TPROC_SUPP_EVAL(0).PO_LP" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Lambat tetapi aktif</td>
                                    <td Class="center">2</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="PO_LTA" value="@Model.TPROC_SUPP_EVAL(0).PO_LTA" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Cepat dan aktif</td>
                                    <td Class="center">3</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="PO_CA" value="@Model.TPROC_SUPP_EVAL(0).PO_CA" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> Keterangan : <input style="text-align:center" size="100" class="eval" id="PO_KET" value="@Model.TPROC_SUPP_EVAL(0).PO_KET" /></td>
                                    <td colspan="3"></td>
                                </tr>

                                <tr style="font-weight:bold">
                                    <td>5</td>
                                    <td colspan="4"> Pengiriman</td>
                                </tr>
                                <tr>
                                    <td Class="center">a</td>
                                    <td> Sering tidak tepat waktu</td>
                                    <td Class="center">1</td>
                                    <td Class="center">6</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="P_STTW" value="@Model.TPROC_SUPP_EVAL(0).P_STTW" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Kadang-kadang tidak tepat waktu</td>
                                    <td Class="center">2</td>
                                    <td Class="center">6</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="P_KTTW" value="@Model.TPROC_SUPP_EVAL(0).P_KTTW" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Tepat Waktu</td>
                                    <td Class="center">3</td>
                                    <td Class="center">6</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="P_TW" value="@Model.TPROC_SUPP_EVAL(0).P_TW" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> Keterangan : <input style="text-align:center" size="100" class="eval" id="P_KET" value="@Model.TPROC_SUPP_EVAL(0).P_KET" /></td>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>

                                <tr style="font-weight:bold">
                                    <td>6</td>
                                    <td colspan="4"> Hasil Produksi</td>
                                </tr>
                                <tr>
                                    <td Class="center">a</td>
                                    <td> Sering tidak sesuai dengan permintaan</td>
                                    <td Class="center">1</td>
                                    <td Class="center">7</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="HP_STSP" value="@Model.TPROC_SUPP_EVAL(0).HP_STSP" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Kadang-kadang tidak sesuai dengan permintaan</td>
                                    <td Class="center">2</td>
                                    <td Class="center">7</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="HP_KTSP" value="@Model.TPROC_SUPP_EVAL(0).HP_KTSP" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Sesuai dengan permintaan</td>
                                    <td Class="center">3</td>
                                    <td Class="center">7</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="HP_SP" value="@Model.TPROC_SUPP_EVAL(0).HP_SP" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> Keterangan : <input style="text-align:center" size="100" class="eval" id="HP_KET" value="@Model.TPROC_SUPP_EVAL(0).HP_KET" /></td>
                                    <td colspan="3"></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="txt_eval_total_score" value="@Model.TPROC_SUPP_EVAL(0).TOTAL_SCORE" readonly="readonly" /></td>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>

                                <tr style="font-weight:bold">
                                    <td>6</td>
                                    <td colspan="2"> Apakah supplier ini direkomendasikan untuk tetap digunakan ?</td>
                                    <td Class="center">
                                        <label>
                                                @if Model.TPROC_SUPP_EVAL(0).IS_RECOMMENDED = 1 Then
                                                    @<input name="form-field-radio" type="radio" class="ace" id="rb_ya_eval" checked="checked" />
                                                Else
                                                    @<input name="form-field-radio" type="radio" class="ace" id="rb_ya_eval" />
                                                End If
                                                <span Class="lbl">Ya</span>
                                            </label>
                                    </td>
                                    <td Class="center">
                                        <Label>
                                            @if Model.TPROC_SUPP_EVAL(0).IS_RECOMMENDED = 1 Then
                                                @<input name="form-field-radio" type="radio" Class="ace" id="rb_tidak"  />
                                            Else
                                                @<input name="form-field-radio" type="radio" Class="ace" id="rb_tidak" checked="checked" />
                                            End If
                                            <span Class="lbl">Tidak</span>
                                        </Label>
                                    </td>
                                </tr>
                            </table>

                            <div>
                                <Label for="form-field-5">Komentar/Saran</Label>
                                <textarea Class="form-control" id="komentar_eval">@Model.TPROC_SUPP_EVAL(0).KOMENTAR_SARAN</textarea>
                            </div>

                            <div Class="space-2"></div>
                            <div Class="space-2"></div>

                            <p> Jakarta, @Date.Now.ToString("dd MMMM yyy")</p>

                            <Table>
                                <tr>
                                    <td width="300"> Prepared by :</td>
                                    <td width="300"> Approved by :</td>
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
                                    <td> <span id="txt_disiapkan_oleh_eval" class="freeText">@Model.TPROC_SUPP_EVAL(0).DISIAPKAN_OLEH</span></td>
                                    <td> <span id="txt_disetujui_oleh_eval" class="freeText">@Model.TPROC_SUPP_EVAL(0).DISETUJUI_OLEH</span></td>
                                </tr>
                            </Table>

                        </div>
                    </div>

                </div>
            </div>

            <div class="vspace-6-sm"></div>
        </div>
        <div class='modal-footer no-margin-top'>
            @Html.Raw(Labels.ButtonForm("Close"))
            @Html.Raw(Labels.ButtonForm("SaveEdit"))
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>