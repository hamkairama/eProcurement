@ModelType eProcurementApps.Models.TPROC_PO_HEADERS
@Imports eProcurementApps.Helpers

<link href="~/Ace/fileInput/fileinput.css" rel="stylesheet" />
<link href="~/Ace/fileInput/theme.css" rel="stylesheet" />
<script src="~/Ace/fileInput/fileinput.js"></script>
@Code
    ViewBag.Breadcrumbs = "Purchase Order"
    ViewBag.Title = "Print Purchase Order"
    ViewBag.PurchasingRequest = "active open"
    ViewBag.IndexCreatePR = "active"
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


<div class="row">
    <div class="profile-info-title-center"> <b>PURCHASE ORDER</b> </div>

    <div class="col-sm-6">
        <div class="widget-box">
            @*<div class="widget-header">
                </div>*@
            <div class="widget-body" id="">
                @*<table>
                        <tr>
                            <td>To</td>
                            <td>@Model.SUPPLIER_NAME</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Attn</td>
                            <td>@Model.CONTACT_PERSON</td>
                        </tr>
                        <tr>
                            <td>Address</td>
                            <td>@Model.SUPPLIER_ADDTRSS</td>
                        </tr>
                        <tr>
                            <td>Telp</td>
                            <td>@Model.SUPPLIER_PHONE &nbsp;&nbsp; Fax &nbsp;&nbsp; @Model.SUPPLIER_FAX</td>
                        </tr>
                    </table>*@
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">To. </div>
                        <div class="profile-info-value">
                            <span>@Model.SUPPLIER_NAME</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Attn. </div>
                        <div class="profile-info-value">
                            <span>@Model.CONTACT_PERSON</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Address </div>
                        <div class="profile-info-value">
                            <span>@Model.SUPPLIER_ADDTRSS</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Telp </div>
                        <div class="profile-info-value">
                            <span>@Model.SUPPLIER_PHONE</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Fax </div>
                        <div class="profile-info-value">
                            <span>@Model.SUPPLIER_FAX</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-6">
        <div class="widget-box">
            @*<div class="widget-header">
                </div>*@
            <div class="widget-body" id="">
                <div class="profile-user-info">
                    <div class="profile-info-row">
                        <div class="profile-info-name-left"> </div>
                        <div class="profile-info-value">
                            <span>Please Delivery</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">To</div>
                        <div class="profile-info-value">
                            <span>@Model.DELIVERY_NAME</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Address</div>
                        <div class="profile-info-value">
                            <span>@Model.DELIVERY_ADDRESS</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Telp</div>
                        <div class="profile-info-value">
                            <span>@Model.DELIVERY_PHONE</span>
                        </div>
                    </div>
                    <div class="profile-info-row">
                        <div class="profile-info-name-left">Fax </div>
                        <div class="profile-info-value">
                            <span>@Model.DELIVERY_FAX</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    @*<div class="space-4"></div>*@
    <div Class="clearfix">
        <div Class="pull-right tableTools-container"></div>
    </div>
    <div Class="hr hr-10 dotted hr-double"></div>

    <div class="col-sm-12">
        <div class="widget-box center no-border">
            @*<div class="widget-header">
                </div>*@
            <div class="widget-body" id="">
                <table style="width:100%;">
                    <tr>
                        <td><b>PO. DATE</b></td>
                        <td><b>PO. NUMBER</b></td>
                        <td><b>PO. CURRENCY</b></td>
                        <td><b>PO. DELIVERY DATE</b></td>
                    </tr>
                    <tr>
                        <td>@Model.CREATE_DATE.ToString("dd-MM-yyyy")</td>
                        <td>@Model.PO_NUMBER</td>
                        <td>@Model.TPROC_CURRENCY.CURRENCY_NAME</td>
                        <td>@Model.DELIVERY_DATE.Value.ToString("dd-MM-yyyy")</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div Class="clearfix">
        <div Class="pull-right tableTools-container"></div>
    </div>
    <div Class="hr hr-10 dotted hr-double"></div>

    <div class="col-sm-12">
        <div class="widget-box center">
            @*<div class="widget-header">
                </div>*@
            <div class="widget-body" id="">
                <table id="table_input" class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr id="">
                            <th style="text-align:center">No</th>
                            <th style="text-align:center">Quantity </th>
                            <th style="text-align:center">Description</th>
                            <th style="text-align:center">U/M</th>
                            <th style="text-align:center">Unit Price</th>
                            <th style="text-align:center">Total</th>
                        </tr>
                    </thead>

                    <tbody id="dataTable">
                        @code
                            Dim valTotal As Decimal = 0
                            Dim no As Decimal = 1
                            @For Each item In Model.TPROC_PO_DETAILS_ITEM
                                valTotal = (Convert.ToDecimal(item.QUANTITY) * item.PRICE.Value)
                                @<tr>
                                    <td>@no</td>
                                    <td>@item.QUANTITY</td>
                                    <td>@item.ITEM_NAME</td>
                                    <td>@item.UNITMEASUREMENT</td>
                                    <td>@item.PRICE</td>
                                    <td>@valTotal</td>
                                </tr>

                                no += 1
                            Next
                        End Code
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div Class="clearfix">
        <div Class="pull-right tableTools-container"></div>
    </div>
    <div Class="hr hr-18 dotted hr-double"></div>

    <div class="col-sm-12">
        <div class="widget-box center no-border">
            @*<div class="widget-header">
                </div>*@
            <div class="widget-body" id="">
                <div Class="row">
                    <div class="col-xs-8"></div>
                    <div class="col-xs-2" style="text-align:right">
                        Sub Total :
                    </div>

                    <div class="col-xs-1">
                        Rp.
                    </div>

                    <div class="col-xs-1" style="text-align:right">
                        @Model.SUB_TOTAL.Value.ToString("###,###")
                    </div>
                </div>

                <div Class="row">
                    <div class="col-xs-8"></div>
                    <div class="col-xs-2" style="text-align:right">
                        Discount % :
                    </div>

                    <div class="col-xs-1">
                        Rp.
                    </div>

                    <div class="col-xs-1" style="text-align:right">
                        @Model.DSCNT_AMT.Value.ToString("###,###")
                    </div>
                </div>

                <div Class="row">
                    <div class="col-xs-8"></div>
                    <div class="col-xs-2" style="text-align:right">
                        Vat :
                    </div>

                    <div class="col-xs-1">
                        Rp.
                    </div>

                    <div class="col-xs-1" style="text-align:right">
                        @Model.VAT.Value.ToString("###,###")
                    </div>
                </div>

                <div Class="row">
                    <div class="col-xs-8"></div>
                    <div class="col-xs-2" style="text-align:right">
                        Tax :
                    </div>

                    <div class="col-xs-1">
                        Rp.
                    </div>

                    <div class="col-xs-1" style="text-align:right">
                        @Model.WTH_TAX.Value.ToString("###,###")
                    </div>
                </div>

                <div Class="row">
                    <div class="col-xs-8"></div>
                    <div class="col-xs-2" style="text-align:right">
                        Total :
                    </div>

                    <div class="col-xs-1">
                        Rp.
                    </div>

                    <div class="col-xs-1" style="text-align:right">
                        @Model.GRAND_TOTAL.Value.ToString("###,###")
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div Class="clearfix">
        <div Class="pull-right tableTools-container"></div>
    </div>
    <div Class="hr hr-10 dotted hr-double"></div>

    <div class="col-sm-12">
        <div class="widget-box text-justify">
            @*<div class="widget-header">
                </div>*@
            <div class="widget-body" id="">
                <div class="profile-info-title-center"> <h5>SYARAT DAN KETENTUAN</h5> </div>
                <small>
                    1. Perusahaan adalah PT Asuransi Jiwa Manulife Indonesia dan perusahaan-perusahaan yang terafiliasi dengannya. Supplier adalah badan hukum dan/atau individu yang menyediakan barang dan/atau jasa untuk Perusahaan, sebagaimana tercantum dalam Purchase Order.
                    2. Pada saat diterima oleh Supplier, Syarat dan Ketentuan ini merupakan bagian yang tidak terpisah dari Purchase Order dan harus dianggap sebagai satu kesatuan dari Purchase Order yang mengikat Supplier.
                    3.Purchase Order hanya dapat divariasikan, dimodifikasi atau dibatalkan dengan perjanjian tertulis antara Perusahaan dan Supplier.
                    4. Supplier akan mengirimkan barang dan/atau jasa ke tujuan yang ditetapkan dalam Purchase Order dalam kondisi yang baik menurut Perusahaan.
                    5.Perusahaan akan membebankan denda sebesar 1% per hari untuk keterlambatan pengiriman barang dan/atau jasa oleh Supplier kecuali disetujui lain oleh Perusahaan secara tertulis.  Perusahaan berhak membatalkan Purchase Order secara keseluruhan atau atas sebagian barang yang tidak dikirimkan apabila Supplier melanggar jadwal pengiriman sebagaimana tercantum dalam angka 4 diatas atau melanggar syarat dan ketentuan Purchase Order lainnya. Supplier wajib memberitahukan Perusahaan secara tertulis atas alasan keterlambatan atau kemungkinan keterlambatan beserta usulan tindakan untuk mengatasinya.
                    6.Perusahaan berhak menghitung dan memeriksa seluruh barang dan/atau jasa dan memeriksa kualitas bahan yang digunakan dan bagian-bagian barang dan/atau jasa dan menolak barang dan/atau jasa yang cacat atau tidak sesuai tanpa berkewajiban membayar barang dan/atau jasa tersebut. Supplier menjamin bahwa barang dan/atau jasa dalam Purchase Order sesuai dengan spesifikasi gambar, contoh deskripsi lainnya yang diberikan oleh Perusahaan, bahwa barang-barang dibuat dengan bahan yang baik dan bebas dari cacat serta sesuai dan cukup untuk dipergunakan sesuai dengan fungsinya. Apabila setelah pengiriman Perusahaan memberikan pemberitahuan secara tertulis kepada Supplier atas ketidaksesuaian barang-barang yang timbul dari kesalahan desain, bahan atau pengerjaannya, Supplier akan, dalam jangka waktu yang wajar, mengganti atau memperbaiki barang-barang yang tidak sesuai kepada Perusahaan tanpa biaya.
                    7. Pemberitahuan atas pengiriman atau pembayaran barang dan/atau jasa sebelum pemeriksaan tidak dapat dianggap sebagai penerimaan atas barang dan/atau jasa atau pengabaian hak Perusahaan untuk menolaknya.
                    8. Harga barang dan/atau jasa yang telah ditetapkan oleh para pihak dalam Purchase Order mengikat para pihak dan tidak akan berubah kecuali atas kesepakatan para pihak.
                    9. Masing-masing pihak akan bertanggung jawab atas kewajiban pajak yang timbul berdasarkan Purchase Order ini, kecuali disetujui lain oleh Para Pihak secara tertulis.
                    10. Supplier menjamin hal-hal dibawah ini:
                    a. menjamin Perusahaan berkaitan dengan seluruh kerusakan atau kecelakaan yang timbul sebelum berakhirnya masa garansi kepada setiap orang atau properti dan terhadap setiap aksi, gugatan, klaim, permintaan, biaya, denda atau pengeluaran yang timbul akibat dari kelalaian Supplier, karyawannya atau agennya. Supplier juga menjamin bahwa semua hasil produksi yang cacat tidak akan dipergunakan dan akan dihancurkan dalam waktu yang ditentukan oleh Perusahaan;
                    b.  tidak menggunakan corporate image dari Perusahaan termasuk tetapi tidak terbatas pada penggunaan nama, logo, materi cetakan serta ornamen Perusahaan lainnya tanpa ijin tertulis dari Perusahaan;
                    c. menjaga kerahasiaan data dan informasi yang diserahkan oleh Perusahaan setiap waktu baik selama masih berlangsungnya kerjasama maupun setelahnya;
                    d. tidak menyebarluarkan, memberitahukan, ataupun memberikan hak kepada pihak lain untuk mengetahui dan menyalahgunakan setiap data dan informasi yang diperoleh dari Perusahaan.
                    11. Syarat dan Ketentuan ini diatur berdasarkan hukum Negara Republik Indonesia.  Setiap perbedaan, perselisihan, konflik atau kontroversi (secara bersama-sama disebut sebagai “Perselisihan”), yang timbul berkaitan dengan Purchase Order ini, sedapat mungkin diselesesaikan dengan musyawarah.  Kegagalan untuk menyelesaikan secara musyawarah dalam waktu 30 (tiga puluh) hari sejak tanggal salah satu pihak menerima pemberitahuan dari pihak lainnya atas Perselisihan, maka salah satu pihak dapat mendaftarkan Perselisihan ke Pengadilan Negeri Jakarta Selatan.
                    12. Supplier sudah membaca, memahami dan menyetujui isi Syarat dan Ketentuan diatas.
                </small>
            </div>
        </div>
    </div>

</div>


<script src="~/Scripts/Custom/CustomOtherTable.js"></script>
<script src="~/Scripts/Standard/StandardProfile.js"></script>