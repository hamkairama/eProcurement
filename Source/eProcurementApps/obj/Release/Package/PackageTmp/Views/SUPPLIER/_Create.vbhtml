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
                        <table id="">
                            <tr>
                                <td width="500px">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Vendor Code </div>
                                        <div class="profile-info-value">
                                            <span class="" id="txt_vendor_code" maxlenght="50"></span>
                                            <label id="required_txt_vendor_code"></label>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Legalisation </div>
                                        <div class="profile-info-value">
                                            <div class="radio">
                                                <label>
                                                    <input name="form-field-radio" type="radio" class="ace" id="rb_legal" />
                                                    <span class="lbl">VML</span>
                                                </label>
                                                <label>
                                                    <input name="form-field-radio" type="radio" class="ace" id="rb_ilegal" />
                                                    <span class="lbl">Non-VML</span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td width="500px">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> B.U. Owner </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_b_unit_owner" maxlenght="50"></span>
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
                                            <span class="freeText" id="txt_supplier_name" maxlenght="100"></span>
                                            <label id="required_txt_supplier_name"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Alias </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_supplier_alias_name" maxlenght="50"></span>
                                            <label id="required_txt_supplier_alias_name"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name required"> Address </div>
                                        <div class="profile-info-value item-required">
                                            <span class="freeText" id="txt_supplier_address" maxlenght="200"></span>
                                            <label id="required_txt_supplier_address"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> City </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_city" maxlenght="100"></span>
                                            <label id="required_txt_city"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Core Business </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_core_business" maxlenght="200"></span>
                                            <label id="required_txt_core_business"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Nama Barang </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_nama_barang" maxlenght="100"></span>
                                            <label id="required_txt_nama_barang"></label>
                                        </div>
                                    </div>
                                </td>
                                <td width="500px">
                                    Vendor's Bank Information
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Bank Name </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_bank_name" maxlenght="50"></span>
                                            <label id="required_txt_bank_name"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Branch </div>
                                        <div class="profile-info-value">
                                            <span class="freeText " id="txt_bank_branch" maxlenght="50"></span>
                                            <label id="required_txt_bank_branch"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Account No </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_bank_account_number" maxlenght="50"></span>
                                            <label id="required_txt_bank_account_number"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> NPWP </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_npwp" maxlenght="50"></span>
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
                                            <span class="freeText" id="txt_contact_person" maxlenght="50"></span>
                                            <label id="required_txt_contact_person"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Email </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_email_address" maxlenght="50"></span>
                                            <label id="required_txt_email_address"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Office </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_office_number" maxlenght="50"></span>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name"> Tax </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_tax_number" maxlenght="50"></span>
                                        </div>
                                    </div>
                                </td>

                                <td width="500px">
                                    <div class="profile-info-row">
                                        <div class="profile-info-name required"> Mobile </div>
                                        <div class="profile-info-value item-required">
                                            <span class="freeText" id="txt_mobile_number" maxlenght="50"></span>
                                            <label id="required_txt_mobile_number"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name required"> Fax </div>
                                        <div class="profile-info-value item-required">
                                            <span class="freeText" id="txt_fax_number" maxlenght="50"></span>
                                            <label id="required_txt_fax_number"></label>
                                        </div>
                                    </div>

                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Website </div>
                                        <div class="profile-info-value">
                                            <span class="freeText" id="txt_website" maxlenght="50"></span>
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
                                            <span class="freeText" id="txt_description" maxlenght="200"></span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Efective Dt. </div>
                                        <div class="profile-info-value">
                                            <span class="monthYearText" id="txt_efective_dt" maxlenght="20"></span>
                                        </div>
                                    </div>
                                    <div class="profile-info-row">
                                        <div class="profile-info-name "> Schedule Evaluation </div>
                                        <div class="profile-info-value">
                                            <span class="monthYearText" id="txt_schedule_eval" maxlenght="20"></span>
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
                                <input type="checkbox" class="check-doc" id="cb_weigth_factor" />
                                <span class="lbl">Weighted Factor / Qualification</span>
                            </div>
                            <div class="profile-info-value">
                                <input type="checkbox" class="check-doc" id="cb_bridger_scan" />
                                <span class="lbl">Bridger Scan</span>
                            </div>
                            <div class="profile-info-value">
                                <input type="checkbox" class="check-doc" id="cb_nda" />
                                <span class="lbl">NDA</span>
                            </div>
                            <div class="profile-info-value">
                                <input type="checkbox" class="check-doc" id="cb_cidci" />
                                <span class="lbl">CIDCI</span>
                            </div>
                            <div class="profile-info-value">
                                <input type="checkbox" class="check-doc" id="cb_legal_doc" />
                                <span class="lbl">Legal Documents</span>
                            </div>
                            <div class="profile-info-value">
                                <input type="checkbox" class="check-doc" id="cb_aggreement" />
                                <span class="lbl">Agreement</span>
                            </div>
                            <div class="profile-info-value">
                                <input type="checkbox" class="check-doc" id="cb_validity_checking" />
                                <span class="lbl">Vendor Validity Checking</span>
                            </div>
                        </div>
                    </div>

                    <div id="qualification" class="tab-pane fade">
                        <div align="right">
                            <button class='btn btn-sm btn-success btn-white btn-round' id="export" onClick="fnExcelReport('export', 'table-qual', 'Supplier')">
                                <i class="fa fa-fa-file-excel-o bigger-110 green"></i>
                                Export
                            </button>
                        </div>

                        <div id="table-qual">
                            <div align="center"><b>CHECKLIST KUALIFIKASI SUPPLIER</b></div>
                            <div> Nama Supplier : <span id="qual_nama_supplier"></span></div>
                            <div> Contact Person : <span id="qual_cp"></span> </div>
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
                                    <td><input style="text-align:center" id="txt_h_stk" value="0" class="qual" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Tidak kompetitif</td>
                                    <td class="center">2</td>
                                    <td class="center">7</td>
                                    <td><input style="text-align:center" id="txt_h_tk" value="0" class="qual" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Kompetitif</td>
                                    <td class="center">3</td>
                                    <td class="center">7</td>
                                    <td><input style="text-align:center" id="txt_h_k" value="0" class="qual" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">d</td>
                                    <td>Sangat kompetitif</td>
                                    <td class="center">4</td>
                                    <td class="center">7</td>
                                    <td><input style="text-align:center" id="txt_h_sk" value="0"  class="qual" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 id="txt_h_ket" class="qual" /></td>
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
                                    <td><input style="text-align:center" id="txt_a_ta" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Tidak rapi dan tidak konsisten</td>
                                    <td class="center">1</td>
                                    <td class="center">5</td>
                                    <td><input style="text-align:center" id="txt_a_trk" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Rapi dan konsisten</td>
                                    <td class="center">1</td>
                                    <td class="center">5</td>
                                    <td><input style="text-align:center" id="txt_a_rk" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 id="txt_a_ket" class="qual" /></td>
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
                                    <td>3</td>
                                    <td colspan="4">Fasilitas</td>
                                </tr>
                                <tr>
                                    <td class="center">a</td>
                                    <td>Fasilitas sangat tidak memadai</td>
                                    <td class="center">1</td>
                                    <td class="center">4</td>
                                    <td><input style="text-align:center" id="txt_f_stm" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Fasilitas kurang memadai</td>
                                    <td class="center">2</td>
                                    <td class="center">4</td>
                                    <td><input style="text-align:center" id="txt_f_km" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Fasilitas memadai</td>
                                    <td class="center">3</td>
                                    <td class="center">4</td>
                                    <td><input style="text-align:center" id="txt_f_m" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">d</td>
                                    <td>Fasilitas sangat memadai</td>
                                    <td class="center">4</td>
                                    <td class="center">4</td>
                                    <td><input style="text-align:center" id="txt_f_sm"  class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 id="txt_f_ket" class="qual" /></td>
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
                                    <td colspan="4">Penangan Order &Aacute; Keluhan</td>
                                </tr>
                                <tr>
                                    <td class="center">a</td>
                                    <td>Lambat dan pasif</td>
                                    <td class="center">1</td>
                                    <td class="center">7</td>
                                    <td><input style="text-align:center" id="txt_p_lp" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Lambat tetapi aktif</td>
                                    <td class="center">2</td>
                                    <td class="center">7</td>
                                    <td><input style="text-align:center" id="txt_p_lta" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Cepat dan aktif</td>
                                    <td class="center">3</td>
                                    <td class="center">7</td>
                                    <td><input style="text-align:center" id="txt_p_ca" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 id="txt_p_ket" class="qual" /></td>
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
                                    <td>5</td>
                                    <td colspan="4">Quality Control</td>
                                </tr>
                                <tr>
                                    <td class="center">a</td>
                                    <td>Tidak ada control</td>
                                    <td class="center">1</td>
                                    <td class="center">5</td>
                                    <td><input style="text-align:center" id="txt_q_ta" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Control sederhana</td>
                                    <td class="center">2</td>
                                    <td class="center">5</td>
                                    <td><input style="text-align:center" id="txt_q_cs" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Hanya hasil akhir yang dikontrol</td>
                                    <td class="center">3</td>
                                    <td class="center">5</td>
                                    <td><input style="text-align:center" id="txt_q_hha" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">d</td>
                                    <td>Setiap langkah pekerjaan dikontrol</td>
                                    <td class="center">4</td>
                                    <td class="center">5</td>
                                    <td><input style="text-align:center" id="txt_q_slp" class="qual" value="0" onkeyup="RefreshTotScoreQual()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 id="txt_q_ket" class="qual" /></td>
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
                                    <td colspan="2">Total Score</td>
                                    <td></td>
                                    <td></td>
                                    <td><input style="text-align:center" id="txt_qual_total_score" class="qual" value="0" readonly="readonly" /></td>
                                </tr>
                            </table>

                            <div>
                                <label for="form-field-5">Komentar/Saran</label>
                                <textarea class="form-control" id="komentar_qual"></textarea>
                            </div>

                            <div class="space-2"></div>
                            <div class="space-2"></div>

                            <p>Jakarta, @Date.Now.ToString("dd MMMM yyy")</p>

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
                                    <td><span id="txt_dibuat_oleh_qual" class="freeText">@Session("USER_NAME")</span></td>
                                    <td><span id="txt_diperiksa_oleh_qual" class="freeText"></span></td>
                                    <td><span id="txt_disetujui_oleh_qual" class="freeText"></span></td>
                                </tr>
                            </table>
                        </div>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="F_STM" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">b</td>
                                    <td>Fasilitas kurang memadai</td>
                                    <td class="center">2</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_KM" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">c</td>
                                    <td>Fasilitas memadai</td>
                                    <td class="center">3</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_M" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td class="center">d</td>
                                    <td>Fasilitas Sangat Memadai</td>
                                    <td class="center">4</td>
                                    <td class="center">4</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="F_SM" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size=100 class="eval" id="F_KET" value="" /></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="A_TA" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Tidak rapi dan tidak konsisten</td>
                                    <td Class="center">2</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="A_TRK" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Rapi dan konsisten</td>
                                    <td Class="center">3</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="A_RK" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Keterangan : <input style="text-align:center" size="100" class="eval" id="A_KET" value="" /></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="H_TK" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Kompetitif</td>
                                    <td Class="center">2</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="H_K" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Sangat Kompetitif</td>
                                    <td Class="center">3</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="H_SK" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td > Keterangan : <input style="text-align:center" size="100" class="eval" id="H_KET" value="" /></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="PO_LP" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Lambat tetapi aktif</td>
                                    <td Class="center">2</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="PO_LTA" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Cepat dan aktif</td>
                                    <td Class="center">3</td>
                                    <td Class="center">5</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="PO_CA" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> Keterangan : <input style="text-align:center" size="100" class="eval" id="PO_KET" value="" /></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="P_STTW" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Kadang-kadang tidak tepat waktu</td>
                                    <td Class="center">2</td>
                                    <td Class="center">6</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="P_KTTW" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Tepat Waktu</td>
                                    <td Class="center">3</td>
                                    <td Class="center">6</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="P_TW" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td > Keterangan : <input style="text-align:center" size ="100" class="eval" id="P_KET" value="" /></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="HP_STSP" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">b</td>
                                    <td> Kadang-kadang tidak sesuai dengan permintaan</td>
                                    <td Class="center">2</td>
                                    <td Class="center">7</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="HP_KTSP" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td Class="center">c</td>
                                    <td> Sesuai dengan permintaan</td>
                                    <td Class="center">3</td>
                                    <td Class="center">7</td>
                                    <td class="center"><input style="text-align:center" class="eval" id="HP_SP" value="0" onkeyup="RefreshTotScoreEval()" /></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td> Keterangan : <input style="text-align:center" size="100" class="eval" id="HP_KET" value="" /></td>
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
                                    <td class="center"><input style="text-align:center" class="eval" id="txt_eval_total_score" value="0" readonly="readonly" /></td>
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
                                            <input name="form-field-radio" type="radio" class="ace" id="rb_ya_eval" />
                                            <span class="lbl">Ya</span>
                                        </label>
                                    </td>
                                    <td Class="center">
                                        <label>
                                            <input name="form-field-radio" type="radio" class="ace" id="rb_tidak_eval" />
                                            <span class="lbl">Tidak</span>
                                        </label>
                                    </td>
                                </tr>
                            </table>

                            <div>
                                <Label for="form-field-5">Komentar/Saran</Label>
                                <textarea Class="form-control" id="komentar_eval"></textarea>
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
                                    <td> <span id="txt_disiapkan_oleh_eval" class="freeText"></span></td>
                                    <td> <span id="txt_disetujui_oleh_eval" class="freeText"></span></td>
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
            @Html.Raw(Labels.ButtonForm("SaveCreate"))
        </div>
    </div>
</div>

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>