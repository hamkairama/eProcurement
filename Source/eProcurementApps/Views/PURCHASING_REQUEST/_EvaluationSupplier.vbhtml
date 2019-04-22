@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_SUPPLIER)
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Purchasing Request"
    ViewBag.Title = "Evaluation Supplier"
    ViewBag.PurchasingRequest = "active open"

    If ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyPRReadyToSignOff) Then
        ViewBag.IndexMyPRReadyToSignOff = "active"
        ViewBag.MyListPR = "active open"
    ElseIf ViewBag.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToSignOff) Then
        ViewBag.IndexAllPRReadyToSignOff = "active"
        ViewBag.EprocListPR = "active open"
    End If

End Code

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
<h4>Please help us to fill this evaluation for better future</h4>
<h6>
    Button of finish will be showed after you filled it or if you don't have it in your order and then click
    @If Model Is Nothing Then
        @<a Class="blue" href="@Url.Action("ActionReadyToSignOff", "PURCHASING_REQUEST", New With {.pr_header_id = ViewBag.PR_Header_id, .FlagDetail = Convert.ToInt32(ViewBag.FlagDetail), .SubTitle = "PRs Ready To Sign Off"})" title="Finish">
            @Html.Raw(Labels.ButtonForm("Finish"))
        </a>
    End If
</h6> 

@If Model IsNot Nothing Then
    @<div class="modal-dialog" style="width:1000px">
        <div class="modal-content">
            <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
            @*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
            <div class="hidden"> ID PR : <span id="txt_pr_header_id">@ViewBag.PR_HEader_id</span></div>
            <div class="modal-body no-padding">
                <div class="tabbable">
                    <ul class="nav nav-tabs" id="myTab">
                        @for i As Integer = 0 To Model.Count() - 1
                            If i = 0 Then
                                @<li Class="active">
                                    <a data-toggle="tab" href="#@Model(i).ID">
                                        <i Class="blue ace-icon fa fa-file-text bigger-120"></i>
                                        @Model(i).SUPPLIER_NAME
                                    </a>
                                </li>
                            Else
                                @<li Class="">
                                    <a data-toggle="tab" href="#@Model(i).ID">
                                        <i Class="blue ace-icon fa fa-file-text bigger-120"></i>
                                        @Model(i).SUPPLIER_NAME
                                    </a>
                                </li>
                            End If
                        Next
                    </ul>
                    <div Class="tab-content">
                        @For h As Integer = 0 To Model.Count() - 1
                            If h = 0 Then
                                @<div id="@Model(h).ID" Class="tab-pane fade in active">
                                    <div align="right">
                                        <button class='btn btn-sm btn-success btn-white btn-round' id="export" onClick="fnExcelReport('export', 'table-@Model(h).ID', 'Supplier')">
                                            <i class="fa fa-fa-file-excel-o bigger-110 green"></i>
                                            Export
                                        </button>
                                        <button type='submit' onclick="ActionSaveEval('@Model(h).ID')" class='btn btn-sm btn-success btn-white btn-round'>
                                            <i class='ace-icon fa fa-floppy-o bigger-110 blue'></i>
                                            Save
                                        </button>
                                    </div>

                                    <div id="table-@Model(h).ID">
                                        <div align="center"><b>CHECKLIST EVALUASI SUPPLIER</b></div>
                                        <div class=""> ID Supplier : @Model(h).ID</div>
                                        <div>Nama Supplier : @Model(h).SUPPLIER_NAME</div>
                                        <div>Contact Person : @Model(h).CONTACT_PERSON</div>
                                        <div>Core Busine    ss : @Model(h).CORE_BUSINESS</div>
                                        <div>Nama Barang : @Model(h).NAMA_BARANG</div>

                                        <table border="1" style="align-content:center" align="center" id="dataTable-@Model(h).ID">
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_STM_@Model(h).ID" onkeyup="RefreshTotScore(@Model(h).ID)" value="0" /></td>
                                            </tr>
                                            <tr>
                                                <td class="center">b</td>
                                                <td>Fasilitas kurang memadai</td>
                                                <td class="center">2</td>
                                                <td class="center">4</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_KM_@Model(h).ID" onkeyup="RefreshTotScore(@Model(h).ID)" value="0" /></td>
                                            </tr>
                                            <tr>
                                                <td class="center">c</td>
                                                <td>Fasilitas memadai</td>
                                                <td class="center">3</td>
                                                <td class="center">4</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_M_@Model(h).ID" onkeyup="RefreshTotScore(@Model(h).ID)" value="0" /></td>
                                            </tr>
                                            <tr>
                                                <td class="center">d</td>
                                                <td>Fasilitas Sangat Memadai</td>
                                                <td class="center">4</td>
                                                <td class="center">4</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_SM_@Model(h).ID" onkeyup="RefreshTotScore(@Model(h).ID)" value="0" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4">Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="F_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="A_TA_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Tidak rapi dan tidak konsisten</td>
                                                <td Class="center">2</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="A_TRK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Rapi dan konsisten</td>
                                                <td Class="center">3</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="A_RK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4">Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="A_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="H_TK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Kompetitif</td>
                                                <td Class="center">2</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="H_K_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Sangat Kompetitif</td>
                                                <td Class="center">3</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="H_SK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="H_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="PO_LP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Lambat tetapi aktif</td>
                                                <td Class="center">2</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="PO_LTA_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Cepat dan aktif</td>
                                                <td Class="center">3</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="PO_CA_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="PO_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="P_STTW_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Kadang-kadang tidak tepat waktu</td>
                                                <td Class="center">2</td>
                                                <td Class="center">6</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="P_KTTW_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Tepat Waktu</td>
                                                <td Class="center">3</td>
                                                <td Class="center">6</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="P_TW_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="P_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="HP_STSP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Kadang-kadang tidak sesuai dengan permintaan</td>
                                                <td Class="center">2</td>
                                                <td Class="center">7</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="HP_KTSP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Sesuai dengan permintaan</td>
                                                <td Class="center">3</td>
                                                <td Class="center">7</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="HP_SP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="HP_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="TOTAL_SCORE_@Model(h).ID" value="0" readonly="readonly" /></td>
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
                                                        <input name="form-field-radio" type="radio" class="ace" id="rb_ya_@Model(h).ID" />
                                                        <span class="lbl">Ya</span>
                                                    </label>
                                                </td>
                                                <td Class="center">
                                                    <label>
                                                        <input name="form-field-radio" type="radio" class="ace" id="rb_tidak_@Model(h).ID" />
                                                        <span class="lbl">Tidak</span>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>

                                        <div>
                                            <Label for="form-field-5">Komentar/Saran</Label>
                                            <textarea Class="form-control" id="komentar_@Model(h).ID" placeholder="Default Text"></textarea>
                                        </div>

                                        <div Class="space-2"></div>
                                        <div Class="space-2"></div>

                                        <p> Jakarta, @Date.Now</p>

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
                                                <td> <span id="txt_dibuat_oleh_@Model(h)" class=""></span></td>
                                                <td> <span id="txt_disetujui_oleh_@Model(h)" class=""></span></td>
                                            </tr>
                                        </Table>

                                    </div>
                                </div>
                            Else
                                @<div id="@Model(h).ID" Class="tab-pane fade">
                                    <div align="right">
                                        <button class='btn btn-sm btn-success btn-white btn-round' id="export" onClick="fnExcelReport('export', 'table-@Model(h).ID', 'Supplier')">
                                            <i class="fa fa-fa-file-excel-o bigger-110 green"></i>
                                            Export
                                        </button>
                                        <button type='submit' onclick="ActionSaveEval('@Model(h).ID')" class='btn btn-sm btn-success btn-white btn-round'>
                                            <i class='ace-icon fa fa-floppy-o bigger-110 blue'></i>
                                            Save
                                        </button>
                                    </div>

                                    <div id="table-@Model(h).ID">
                                        <div align="center"><b>CHECKLIST EVALUASI SUPPLIER</b></div>
                                        <div class=""> ID Supplier : @Model(h).ID</div>
                                        <div>Nama Supplier : @Model(h).SUPPLIER_NAME</div>
                                        <div>Contact Person : @Model(h).CONTACT_PERSON</div>
                                        <div>Core Busine    ss : @Model(h).CORE_BUSINESS</div>
                                        <div>Nama Barang : @Model(h).NAMA_BARANG</div>

                                        <table border="1" style="align-content:center" align="center" id="dataTable-@Model(h).ID">
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_STM_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td class="center">b</td>
                                                <td>Fasilitas kurang memadai</td>
                                                <td class="center">2</td>
                                                <td class="center">4</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_KM_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td class="center">c</td>
                                                <td>Fasilitas memadai</td>
                                                <td class="center">3</td>
                                                <td class="center">4</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_M_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td class="center">d</td>
                                                <td>Fasilitas Sangat Memadai</td>
                                                <td class="center">4</td>
                                                <td class="center">4</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="F_SM_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4">Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="F_KET_@Model(h).ID" value=""  /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="A_TA_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Tidak rapi dan tidak konsisten</td>
                                                <td Class="center">2</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="A_TRK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Rapi dan konsisten</td>
                                                <td Class="center">3</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="A_RK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4">Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="A_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="H_TK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Kompetitif</td>
                                                <td Class="center">2</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="H_K_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Sangat Kompetitif</td>
                                                <td Class="center">3</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="H_SK_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="H_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="PO_LP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Lambat tetapi aktif</td>
                                                <td Class="center">2</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="PO_LTA_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Cepat dan aktif</td>
                                                <td Class="center">3</td>
                                                <td Class="center">5</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="PO_CA_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="PO_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="P_STTW_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Kadang-kadang tidak tepat waktu</td>
                                                <td Class="center">2</td>
                                                <td Class="center">6</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="P_KTTW_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Tepat Waktu</td>
                                                <td Class="center">3</td>
                                                <td Class="center">6</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="P_TW_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="P_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="HP_STSP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">b</td>
                                                <td> Kadang-kadang tidak sesuai dengan permintaan</td>
                                                <td Class="center">2</td>
                                                <td Class="center">7</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="HP_KTSP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td Class="center">c</td>
                                                <td> Sesuai dengan permintaan</td>
                                                <td Class="center">3</td>
                                                <td Class="center">7</td>
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="HP_SP_@Model(h).ID" value="0" onkeyup="RefreshTotScore(@Model(h).ID)" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4"> Keterangan : <input style="text-align:center" class="_@Model(h).ID" id="HP_KET_@Model(h).ID" value="" /></td>
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
                                                <td class="center"><input style="text-align:center" class="_@Model(h).ID" id="TOTAL_SCORE_@Model(h).ID" value="0" readonly="readonly" /></td>
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
                                                        <input name="form-field-radio" type="radio" class="ace" id="rb_ya_@Model(h).ID" />
                                                        <span class="lbl">Ya</span>
                                                    </label>
                                                </td>
                                                <td Class="center">
                                                    <label>
                                                        <input name="form-field-radio" type="radio" class="ace" id="rb_tidak_@Model(h).ID" />
                                                        <span class="lbl">Tidak</span>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>

                                        <div>
                                            <Label for="form-field-5">Komentar/Saran</Label>
                                            <textarea Class="form-control" id="komentar_@Model(h).ID" placeholder="Default Text"></textarea>
                                        </div>

                                        <div Class="space-2"></div>
                                        <div Class="space-2"></div>

                                        <p> Jakarta, @Date.Now</p>

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
                                                <td> <span id="txt_dibuat_oleh_@Model(h)" class=""></span></td>
                                                <td> <span id="txt_disetujui_oleh_@Model(h)" class=""></span></td>
                                            </tr>
                                        </Table>

                                    </div>
                                </div>
                            End If
                        Next
                    </div>
                </div>

                <div class="vspace-6-sm"></div>
            </div>
            <div class='modal-footer no-margin-top'>
                <a Class="red" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ViewBag.FlagDetail), .SubTitle = "PR Ready To Sign Off"})" title="Close">
                    @Html.Raw(Labels.ButtonForm("Close"))
                </a>
            </div>
        </div>
    </div>  End If

<script src="~/Scripts/Standard/StandardModal.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>
<script src="~/Scripts/Controllers/PR_EVALUATIONController.js"></script>


