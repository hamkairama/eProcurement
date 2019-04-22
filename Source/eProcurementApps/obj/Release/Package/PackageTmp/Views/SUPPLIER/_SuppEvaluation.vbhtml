@ModelType eProcurementApps.Models.TPROC_SUPPLIER
@Imports eProcurementApps.Helpers


<div align="right">
    <button class='btn btn-sm btn-success btn-white btn-round' id="export" onClick="fnExcelReport('export', 'table-evaluation', 'Supplier')">
        <i class="fa fa-fa-file-excel-o bigger-110 green"></i>
        Export
    </button>
</div>

<div id="table-evaluation">
    <div align="center"><b>CHECKLIST EVALUASI SUPPLIER</b></div>
    <div>Nama Vendor : @Model.SUPPLIER_NAME</div>
    <div>Contact Person : @Model.CONTACT_PERSON</div>
    <div>Core Business : @Model.CORE_BUSINESS</div>
    <div>Nama Barang : @Model.NAMA_BARANG</div>

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
            <td colspan="4">Fasilitas</td>
        </tr>
        <tr>
            <td class="center">a</td>
            <td>Fasilitas sangat tidak memadai</td>
            <td class="center">1</td>
            <td class="center">4</td>
            <td class="center">@Model.TPROC_SUPP_EVAL(0).F_STM</td>
        </tr>
        <tr>
            <td class="center">b</td>
            <td>Fasilitas kurang memadai</td>
            <td class="center">2</td>
            <td class="center">4</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).F_KM*@</td>
        </tr>
        <tr>
            <td class="center">c</td>
            <td>Fasilitas memadai</td>
            <td class="center">3</td>
            <td class="center">4</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).F_M*@</td>
        </tr>
        <tr>
            <td class="center">d</td>
            <td>Fasilitas Sangat Memadai</td>
            <td class="center">4</td>
            <td class="center">4</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).F_SM*@</td>
        </tr>
        <tr>
            <td></td>
            <td>Keterangan : @*@Model.TPROC_SUPP_EVAL(0).F_KET*@</td>
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
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).A_TA*@</td>
        </tr>
        <tr>
            <td Class="center">b</td>
            <td> Tidak rapi dan tidak konsisten</td>
            <td Class="center">2</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).A_TRK*@</td>
        </tr>
        <tr>
            <td Class="center">c</td>
            <td> Rapi dan konsisten</td>
            <td Class="center">3</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).A_RK*@</td>
        </tr>
        <tr>
            <td></td>
            <td>Keterangan : @*@Model.TPROC_SUPP_EVAL(0).A_KET*@</td>
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
            <td colspan="4"> Harga</td>
        </tr>
        <tr>
            <td Class="center">a</td>
            <td> Tidak kompetitif</td>
            <td Class="center">1</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).H_TK*@</td>
        </tr>
        <tr>
            <td Class="center">b</td>
            <td> Kompetitif</td>
            <td Class="center">2</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).H_K*@</td>
        </tr>
        <tr>
            <td Class="center">c</td>
            <td> Sangat Kompetitif</td>
            <td Class="center">3</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).H_SK*@</td>
        </tr>
        <tr>
            <td></td>
            <td> Keterangan : @*@Model.TPROC_SUPP_EVAL(0).H_KET*@</td>
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
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).PO_LP*@</td>
        </tr>
        <tr>
            <td Class="center">b</td>
            <td> Lambat tetapi aktif</td>
            <td Class="center">2</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).PO_LTA*@</td>
        </tr>
        <tr>
            <td Class="center">c</td>
            <td> Cepat dan aktif</td>
            <td Class="center">3</td>
            <td Class="center">5</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).PO_CA*@</td>
        </tr>
        <tr>
            <td></td>
            <td> Keterangan : @*@Model.TPROC_SUPP_EVAL(0).PO_KET*@</td>
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
            <td colspan="4"> Pengiriman</td>
        </tr>
        <tr>
            <td Class="center">a</td>
            <td> Sering tidak tepat waktu</td>
            <td Class="center">1</td>
            <td Class="center">6</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).P_STTW*@</td>
        </tr>
        <tr>
            <td Class="center">b</td>
            <td> Kadang-kadang tidak tepat waktu</td>
            <td Class="center">2</td>
            <td Class="center">6</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).P_KTTW*@</td>
        </tr>
        <tr>
            <td Class="center">c</td>
            <td> Tepat Waktu</td>
            <td Class="center">3</td>
            <td Class="center">6</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).P_TW*@</td>
        </tr>
        <tr>
            <td></td>
            <td> Keterangan : @*@Model.TPROC_SUPP_EVAL(0).P_KET*@</td>
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
            <td>6</td>
            <td colspan="4"> Hasil Produksi</td>
        </tr>
        <tr>
            <td Class="center">a</td>
            <td> Sering tidak sesuai dengan permintaan</td>
            <td Class="center">1</td>
            <td Class="center">7</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).HP_STSP*@</td>
        </tr>
        <tr>
            <td Class="center">b</td>
            <td> Kadang-kadang tidak sesuai dengan permintaan</td>
            <td Class="center">2</td>
            <td Class="center">7</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).HP_KTSP*@</td>
        </tr>
        <tr>
            <td Class="center">c</td>
            <td> Sesuai dengan permintaan</td>
            <td Class="center">3</td>
            <td Class="center">7</td>
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).HP_SP*@</td>
        </tr>
        <tr>
            <td></td>
            <td> Keterangan : @*@Model.TPROC_SUPP_EVAL(0).HP_KET*@</td>
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
            <td class="center">@*@Model.TPROC_SUPP_EVAL(0).TOTAL_SCORE*@</td>
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
                    <input name="form-field-radio" type="radio" class="ace" id="rb_ya" />
                    <span class="lbl">Ya</span>
                </label>
            </td>
            <td Class="center">
                <label>
                    <input name="form-field-radio" type="radio" class="ace" id="rb_tidak" />
                    <span class="lbl">Tidak</span>
                </label>
            </td>
        </tr>
    </table>

    <div>
        <Label for="form-field-5">Komentar/Saran</Label>
        <textarea Class="form-control" id="form-field-5" placeholder="Default Text"></textarea>
    </div>

    <div Class="space-2"></div>
    <div Class="space-2"></div>

    <p> Jakarta, @*@Model.TPROC_SUPP_EVAL(0).CREATED_TIME.ToString("dd MMMM yyyy")*@</p>

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
            <td> <span id="txt_dibuat_oleh">@*@Model.TPROC_SUPP_EVAL(0).DISIAPKAN_OLEH*@</span></td>
            <td> <span id="txt_disetujui_oleh" Class="">@*@Model.TPROC_SUPP_QUAL(0).DISETUJUI_OLEH*@</span></td>
        </tr>
    </Table>

</div>