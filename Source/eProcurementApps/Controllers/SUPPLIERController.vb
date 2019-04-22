Imports System.Data.Entity
Imports System.Net
Imports System.Transactions
Imports eProcurementApps.Helpers
Imports eProcurementApps.Models
Imports OfficeOpenXml

Namespace Controllers
    Public Class SUPPLIERController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "INDEx LIST"
        <CAuthorize(Role:="MNU15")>
        Function Index() As ActionResult
            Dim sUPPLIER As New List(Of TPROC_SUPPLIER)
            sUPPLIER = db.TPROC_SUPPLIER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.SUPPLIER_NAME).ToList()
            ViewBag.Message = TempData("msg")

            Return View(sUPPLIER)
        End Function

        <CAuthorize(Role:="MNU15")>
        Function List() As ActionResult
            Dim sUPPLIER As New List(Of TPROC_SUPPLIER)
            sUPPLIER = db.TPROC_SUPPLIER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.SUPPLIER_NAME).ToList()

            Return PartialView("_List", sUPPLIER)
        End Function
#End Region

#Region "DETAIL"
        <CAuthorize(Role:="MNU15")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim sUPPLIER As TPROC_SUPPLIER = db.TPROC_SUPPLIER.Find(id)
            If IsNothing(sUPPLIER) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", sUPPLIER)
        End Function
#End Region

#Region "CREATE"
        <CAuthorize(Role:="MNU15")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU15")>
        Function ActionCreate(ByVal supplier_name As String, ByVal supplier_alias_name As String, ByVal supplier_address As String, ByVal bank_name As String,
                              ByVal bank_branch As String, ByVal bank_account_number As String, ByVal contact_person As String, ByVal email_address As String,
                              ByVal mobile_number As String, ByVal office_number As String, ByVal fax_number As String, ByVal tax_number As String, ByVal website As String, ByVal npwp As String,
                              ByVal description As String, ByVal core_business As String, ByVal nama_barang As String, ByVal b_unit_owner As String,
                              ByVal city As String, ByVal effective_dt As String, ByVal schedule_eval As String,
                              ByVal doc_checking As String, ByVal supp_qual As String, ByVal supp_eval As String, ByVal legalisation As Integer) As ActionResult

            Dim rs As New ResultStatus
            Dim vendor_code = Generate.GetNo("TPROC_SUPPLIER")

            Using scope As New TransactionScope
                Using db1 As New eProcurementEntities
                    Dim sUPPLIER As New TPROC_SUPPLIER
                    sUPPLIER.SUPPLIER_NAME = supplier_name
                    sUPPLIER.SUPPLIER_ALIAS_NAME = supplier_alias_name
                    sUPPLIER.SUPPLIER_ADDRESS = supplier_address
                    sUPPLIER.VENDOR_CODE = vendor_code
                    sUPPLIER.BANK_NAME = bank_name
                    sUPPLIER.BANK_BRANCH = bank_branch
                    sUPPLIER.BANK_ACCOUNT_NUMBER = bank_account_number
                    sUPPLIER.CONTACT_PERSON = contact_person
                    sUPPLIER.EMAIL_ADDRESS = email_address
                    sUPPLIER.MOBILE_NUMBER = mobile_number
                    sUPPLIER.OFFICE_NUMBER = office_number
                    sUPPLIER.FAX_NUMBER = fax_number
                    sUPPLIER.TAX_NUMBER = tax_number
                    sUPPLIER.WEBSITE = website
                    sUPPLIER.NPWP = npwp
                    sUPPLIER.DESCRIPTION = description
                    sUPPLIER.CORE_BUSINESS = core_business
                    sUPPLIER.NAMA_BARANG = nama_barang
                    sUPPLIER.B_UNIT_OWNER = b_unit_owner
                    sUPPLIER.CITY = city
                    sUPPLIER.EFFECTIVE_DATE = effective_dt
                    sUPPLIER.SCHEDULE_EVALUATION = schedule_eval

                    sUPPLIER.CREATED_TIME = Date.Now
                    sUPPLIER.CREATED_BY = Session("USER_ID")
                    sUPPLIER.ROW_STATUS = legalisation
                    db1.TPROC_SUPPLIER.Add(sUPPLIER)
                    db1.SaveChanges()
                    rs.SetSuccessStatus()
                End Using

                'get supplier id
                Dim supp_id
                Using db1 As New eProcurementEntities
                    supp_id = db1.TPROC_SUPPLIER.Where(Function(x) x.VENDOR_CODE = vendor_code And x.ROW_STATUS = legalisation).FirstOrDefault().ID
                End Using

                If rs.IsSuccess Then
                    rs = InsertSuppDoc(supp_id, doc_checking)
                    If rs.IsSuccess Then
                        rs = InsertSuppQual(supp_id, supp_qual)
                        If rs.IsSuccess Then
                            rs = InsertSuppEval(supp_id, supp_eval)
                            If rs.IsSuccess Then
                                rs = Generate.CommitGenerator("TPROC_SUPPLIER")
                                If rs.IsSuccess Then
                                    scope.Complete()
                                    rs.SetSuccessStatus()
                                End If
                            End If
                        End If
                    End If
                End If
            End Using

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU15")>
        Function InsertSuppDoc(ByVal supp_id As Decimal, ByVal doc_checking As String) As ResultStatus
            Dim rs As New ResultStatus

            Using db As New eProcurementEntities
                Dim t_supp_doc As New TPROC_SUPP_DOC
                Dim arry_supp_doc = doc_checking.Split("|")

                t_supp_doc.SUPPLIER_ID = supp_id
                t_supp_doc.WEIGHT_FACTOR = arry_supp_doc(0)
                t_supp_doc.BRIDGER_SCAN = arry_supp_doc(1)
                t_supp_doc.NDA = arry_supp_doc(2)
                t_supp_doc.CIDCI = arry_supp_doc(3)
                t_supp_doc.LEGAL_DOC = arry_supp_doc(4)
                t_supp_doc.AGGREEMENT = arry_supp_doc(5)
                t_supp_doc.VALIDITY_CHECKING = arry_supp_doc(6)

                t_supp_doc.CREATED_TIME = Date.Now
                t_supp_doc.CREATED_BY = Session("USER_ID")
                db.TPROC_SUPP_DOC.Add(t_supp_doc)
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU15")>
        Function InsertSuppQual(ByVal supp_id As Decimal, ByVal supp_qual As String) As ResultStatus
            Dim rs As New ResultStatus

            Using db As New eProcurementEntities
                Dim t_supp_qual As New TPROC_SUPP_QUAL

                t_supp_qual.SUPPLIER_ID = supp_id

                Dim arry_supp_qual = supp_qual.Split("|")
                If arry_supp_qual.Length > 1 Then
                    t_supp_qual.H_STK = arry_supp_qual(0)
                    t_supp_qual.H_TK = arry_supp_qual(1)
                    t_supp_qual.H_K = arry_supp_qual(2)
                    t_supp_qual.H_SK = arry_supp_qual(3)
                    t_supp_qual.H_KET = arry_supp_qual(4)
                    t_supp_qual.A_TA = arry_supp_qual(5)
                    t_supp_qual.A_TRK = arry_supp_qual(6)
                    t_supp_qual.A_RK = arry_supp_qual(7)
                    t_supp_qual.A_KET = arry_supp_qual(8)
                    t_supp_qual.F_STM = arry_supp_qual(9)
                    t_supp_qual.F_KM = arry_supp_qual(10)
                    t_supp_qual.F_M = arry_supp_qual(11)
                    t_supp_qual.F_SM = arry_supp_qual(12)
                    t_supp_qual.F_KET = arry_supp_qual(13)
                    t_supp_qual.P_LP = arry_supp_qual(14)
                    t_supp_qual.P_LTA = arry_supp_qual(15)
                    t_supp_qual.P_CA = arry_supp_qual(16)
                    t_supp_qual.P_KET = arry_supp_qual(17)
                    t_supp_qual.Q_TA = arry_supp_qual(18)
                    t_supp_qual.Q_CS = arry_supp_qual(19)
                    t_supp_qual.Q_HHA = arry_supp_qual(20)
                    t_supp_qual.Q_SLP = arry_supp_qual(21)
                    t_supp_qual.Q_KET = arry_supp_qual(22)
                    t_supp_qual.TOTAL_SCORE = arry_supp_qual(23)
                    t_supp_qual.KOMENTAR_SARAN = arry_supp_qual(24)
                    t_supp_qual.DIBUAT_OLEH = arry_supp_qual(25)
                    t_supp_qual.DIPERIKSA_OLEH = arry_supp_qual(26)
                    t_supp_qual.DISETUJUI_OLEH = arry_supp_qual(27)
                Else
                    t_supp_qual.H_STK = 0
                    t_supp_qual.H_TK = 0
                    t_supp_qual.H_K = 0
                    t_supp_qual.H_SK = 0
                    t_supp_qual.H_KET = ""
                    t_supp_qual.A_TA = 0
                    t_supp_qual.A_TRK = 0
                    t_supp_qual.A_RK = 0
                    t_supp_qual.A_KET = ""
                    t_supp_qual.F_STM = 0
                    t_supp_qual.F_KM = 0
                    t_supp_qual.F_M = 0
                    t_supp_qual.F_SM = 0
                    t_supp_qual.F_KET = ""
                    t_supp_qual.P_LP = 0
                    t_supp_qual.P_LTA = 0
                    t_supp_qual.P_CA = 0
                    t_supp_qual.P_KET = ""
                    t_supp_qual.Q_TA = 0
                    t_supp_qual.Q_CS = 0
                    t_supp_qual.Q_HHA = 0
                    t_supp_qual.Q_SLP = 0
                    t_supp_qual.Q_KET = ""
                    t_supp_qual.TOTAL_SCORE = 0
                    t_supp_qual.KOMENTAR_SARAN = ""
                    t_supp_qual.DIBUAT_OLEH = ""
                    t_supp_qual.DIPERIKSA_OLEH = ""
                    t_supp_qual.DISETUJUI_OLEH = ""
                End If

                t_supp_qual.CREATED_TIME = Date.Now
                t_supp_qual.CREATED_BY = Session("USER_ID")
                db.TPROC_SUPP_QUAL.Add(t_supp_qual)
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU15")>
        Function InsertSuppEval(ByVal supp_id As Decimal, ByVal supp_eval As String) As ResultStatus
            Dim rs As New ResultStatus

            Using db As New eProcurementEntities
                Dim t_supp_eval As New TPROC_SUPP_EVAL

                t_supp_eval.SUPPLIER_ID = supp_id

                Dim arry_supp_qual = supp_eval.Split("|")
                If arry_supp_qual.Count > 1 Then
                    t_supp_eval.F_STM = arry_supp_qual(0)
                    t_supp_eval.F_KM = arry_supp_qual(1)
                    t_supp_eval.F_M = arry_supp_qual(2)
                    t_supp_eval.F_SM = arry_supp_qual(3)
                    t_supp_eval.F_KET = arry_supp_qual(4)
                    t_supp_eval.A_TA = arry_supp_qual(5)
                    t_supp_eval.A_TRK = arry_supp_qual(6)
                    t_supp_eval.A_RK = arry_supp_qual(7)
                    t_supp_eval.A_KET = arry_supp_qual(8)
                    t_supp_eval.H_TK = arry_supp_qual(9)
                    t_supp_eval.H_K = arry_supp_qual(10)
                    t_supp_eval.H_SK = arry_supp_qual(11)
                    t_supp_eval.H_KET = arry_supp_qual(12)
                    t_supp_eval.PO_LP = arry_supp_qual(13)
                    t_supp_eval.PO_LTA = arry_supp_qual(14)
                    t_supp_eval.PO_CA = arry_supp_qual(15)
                    t_supp_eval.PO_KET = arry_supp_qual(16)
                    t_supp_eval.P_STTW = arry_supp_qual(17)
                    t_supp_eval.P_KTTW = arry_supp_qual(18)
                    t_supp_eval.P_TW = arry_supp_qual(19)
                    t_supp_eval.P_KET = arry_supp_qual(20)
                    t_supp_eval.HP_STSP = arry_supp_qual(21)
                    t_supp_eval.HP_KTSP = arry_supp_qual(22)
                    t_supp_eval.HP_SP = arry_supp_qual(23)
                    t_supp_eval.HP_KET = arry_supp_qual(24)
                    t_supp_eval.TOTAL_SCORE = arry_supp_qual(25)
                    t_supp_eval.IS_RECOMMENDED = arry_supp_qual(26)
                    t_supp_eval.KOMENTAR_SARAN = arry_supp_qual(27)
                    t_supp_eval.DISIAPKAN_OLEH = arry_supp_qual(28)
                    t_supp_eval.DISETUJUI_OLEH = arry_supp_qual(29)
                Else
                    t_supp_eval.F_STM = 0
                    t_supp_eval.F_KM = 0
                    t_supp_eval.F_M = 0
                    t_supp_eval.F_SM = 0
                    t_supp_eval.F_KET = ""
                    t_supp_eval.A_TA = 0
                    t_supp_eval.A_TRK = 0
                    t_supp_eval.A_RK = 0
                    t_supp_eval.A_KET = ""
                    t_supp_eval.H_TK = 0
                    t_supp_eval.H_K = 0
                    t_supp_eval.H_SK = 0
                    t_supp_eval.H_KET = ""
                    t_supp_eval.PO_LP = 0
                    t_supp_eval.PO_LTA = 0
                    t_supp_eval.PO_CA = 0
                    t_supp_eval.PO_KET = ""
                    t_supp_eval.P_STTW = 0
                    t_supp_eval.P_KTTW = 0
                    t_supp_eval.P_TW = 0
                    t_supp_eval.P_KET = ""
                    t_supp_eval.HP_STSP = 0
                    t_supp_eval.HP_KTSP = 0
                    t_supp_eval.HP_SP = 0
                    t_supp_eval.HP_KET = ""
                    t_supp_eval.TOTAL_SCORE = 0
                    t_supp_eval.IS_RECOMMENDED = 0
                    t_supp_eval.KOMENTAR_SARAN = ""
                    t_supp_eval.DISIAPKAN_OLEH = ""
                    t_supp_eval.DISETUJUI_OLEH = ""
                End If

                t_supp_eval.CREATED_TIME = Date.Now
                t_supp_eval.CREATED_BY = Session("USER_ID")
                db.TPROC_SUPP_EVAL.Add(t_supp_eval)
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

            Return rs
        End Function
#End Region

#Region "EDIT"
        <CAuthorize(Role:="MNU15")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim sUPPLIER As TPROC_SUPPLIER = db.TPROC_SUPPLIER.Find(id)
            If IsNothing(sUPPLIER) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", sUPPLIER)
        End Function

        <CAuthorize(Role:="MNU15")>
        Function ActionEdit(ByVal id As Decimal, ByVal supplier_name As String, ByVal supplier_alias_name As String, ByVal supplier_address As String, ByVal bank_name As String,
                            ByVal bank_branch As String, ByVal bank_account_number As String, ByVal contact_person As String, ByVal email_address As String,
                              ByVal mobile_number As String, ByVal office_number As String, ByVal fax_number As String, ByVal tax_number As String, ByVal website As String, ByVal npwp As String,
                              ByVal description As String, ByVal core_business As String, ByVal nama_barang As String, ByVal b_unit_owner As String,
                              ByVal city As String, ByVal effective_dt As String, ByVal schedule_eval As String,
                              ByVal doc_checking As String, ByVal supp_qual As String, ByVal supp_eval As String, ByVal legalisation As Integer) As ActionResult

            Dim rs As New ResultStatus
            Dim sUPPLIER As TPROC_SUPPLIER

            Using scope As New TransactionScope
                Using db1 As New eProcurementEntities
                    sUPPLIER = db.TPROC_SUPPLIER.Find(id)
                    sUPPLIER.SUPPLIER_NAME = supplier_name
                    sUPPLIER.SUPPLIER_ALIAS_NAME = supplier_alias_name
                    sUPPLIER.SUPPLIER_ADDRESS = supplier_address
                    sUPPLIER.BANK_NAME = bank_name
                    sUPPLIER.BANK_BRANCH = bank_branch
                    sUPPLIER.BANK_ACCOUNT_NUMBER = bank_account_number
                    sUPPLIER.CONTACT_PERSON = contact_person
                    sUPPLIER.EMAIL_ADDRESS = email_address
                    sUPPLIER.MOBILE_NUMBER = mobile_number
                    sUPPLIER.OFFICE_NUMBER = office_number
                    sUPPLIER.FAX_NUMBER = fax_number
                    sUPPLIER.TAX_NUMBER = tax_number
                    sUPPLIER.WEBSITE = website
                    sUPPLIER.NPWP = npwp
                    sUPPLIER.DESCRIPTION = description
                    sUPPLIER.CORE_BUSINESS = core_business
                    sUPPLIER.NAMA_BARANG = nama_barang
                    sUPPLIER.B_UNIT_OWNER = b_unit_owner
                    sUPPLIER.CITY = city
                    sUPPLIER.EFFECTIVE_DATE = effective_dt
                    sUPPLIER.SCHEDULE_EVALUATION = schedule_eval

                    sUPPLIER.LAST_MODIFIED_TIME = Date.Now
                    sUPPLIER.LAST_MODIFIED_BY = Session("USER_ID")
                    sUPPLIER.ROW_STATUS = legalisation
                    db.Entry(sUPPLIER).State = EntityState.Modified
                    db.SaveChanges()
                    rs.SetSuccessStatus()
                End Using

                If rs.IsSuccess Then
                    rs = EditSuppDoc(sUPPLIER.TPROC_SUPP_DOC.FirstOrDefault.ID, doc_checking)
                    If rs.IsSuccess Then
                        rs = EditSuppQual(sUPPLIER.TPROC_SUPP_QUAL.FirstOrDefault.ID, supp_qual)
                        If rs.IsSuccess Then
                            rs = EditSuppEval(sUPPLIER.TPROC_SUPP_EVAL.FirstOrDefault.ID, supp_eval)
                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus()
                            End If
                        End If
                    End If
                End If
            End Using

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU15")>
        Function EditSuppDoc(ByVal id As Decimal, ByVal doc_checking As String) As ResultStatus
            Dim rs As New ResultStatus

            Using db As New eProcurementEntities
                Dim t_supp_doc As TPROC_SUPP_DOC = db.TPROC_SUPP_DOC.Find(id)
                Dim arry_supp_doc = doc_checking.Split("|")

                t_supp_doc.WEIGHT_FACTOR = arry_supp_doc(0)
                t_supp_doc.BRIDGER_SCAN = arry_supp_doc(1)
                t_supp_doc.NDA = arry_supp_doc(2)
                t_supp_doc.CIDCI = arry_supp_doc(3)
                t_supp_doc.LEGAL_DOC = arry_supp_doc(4)
                t_supp_doc.AGGREEMENT = arry_supp_doc(5)
                t_supp_doc.VALIDITY_CHECKING = arry_supp_doc(6)

                t_supp_doc.LAST_MODIFIED_TIME = Date.Now
                t_supp_doc.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(t_supp_doc).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU15")>
        Function EditSuppQual(ByVal id As Decimal, ByVal supp_qual As String) As ResultStatus
            Dim rs As New ResultStatus

            Using db As New eProcurementEntities
                Dim t_supp_qual As TPROC_SUPP_QUAL = db.TPROC_SUPP_QUAL.Find(id)
                Dim arry_supp_qual = supp_qual.Split("|")

                t_supp_qual.H_STK = arry_supp_qual(0)
                t_supp_qual.H_TK = arry_supp_qual(1)
                t_supp_qual.H_K = arry_supp_qual(2)
                t_supp_qual.H_SK = arry_supp_qual(3)
                t_supp_qual.H_KET = arry_supp_qual(4)
                t_supp_qual.A_TA = arry_supp_qual(5)
                t_supp_qual.A_TRK = arry_supp_qual(6)
                t_supp_qual.A_RK = arry_supp_qual(7)
                t_supp_qual.A_KET = arry_supp_qual(8)
                t_supp_qual.F_STM = arry_supp_qual(9)
                t_supp_qual.F_KM = arry_supp_qual(10)
                t_supp_qual.F_M = arry_supp_qual(11)
                t_supp_qual.F_SM = arry_supp_qual(12)
                t_supp_qual.F_KET = arry_supp_qual(13)
                t_supp_qual.P_LP = arry_supp_qual(14)
                t_supp_qual.P_LTA = arry_supp_qual(15)
                t_supp_qual.P_CA = arry_supp_qual(16)
                t_supp_qual.P_KET = arry_supp_qual(17)
                t_supp_qual.Q_TA = arry_supp_qual(18)
                t_supp_qual.Q_CS = arry_supp_qual(19)
                t_supp_qual.Q_HHA = arry_supp_qual(20)
                t_supp_qual.Q_SLP = arry_supp_qual(21)
                t_supp_qual.Q_KET = arry_supp_qual(22)
                t_supp_qual.TOTAL_SCORE = arry_supp_qual(23)
                t_supp_qual.KOMENTAR_SARAN = arry_supp_qual(24)
                t_supp_qual.DIBUAT_OLEH = arry_supp_qual(25)
                t_supp_qual.DIPERIKSA_OLEH = arry_supp_qual(26)
                t_supp_qual.DISETUJUI_OLEH = arry_supp_qual(27)

                t_supp_qual.LAST_MODIFIED_TIME = Date.Now
                t_supp_qual.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(t_supp_qual).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU15")>
        Function EditSuppEval(ByVal id As Decimal, ByVal supp_eval As String) As ResultStatus
            Dim rs As New ResultStatus

            Using db As New eProcurementEntities
                Dim t_supp_eval As TPROC_SUPP_EVAL = db.TPROC_SUPP_EVAL.Find(id)
                Dim arry_supp_qual = supp_eval.Split("|")

                t_supp_eval.F_STM = arry_supp_qual(0)
                t_supp_eval.F_KM = arry_supp_qual(1)
                t_supp_eval.F_M = arry_supp_qual(2)
                t_supp_eval.F_SM = arry_supp_qual(3)
                t_supp_eval.F_KET = arry_supp_qual(4)
                t_supp_eval.A_TA = arry_supp_qual(5)
                t_supp_eval.A_TRK = arry_supp_qual(6)
                t_supp_eval.A_RK = arry_supp_qual(7)
                t_supp_eval.A_KET = arry_supp_qual(8)
                t_supp_eval.H_TK = arry_supp_qual(9)
                t_supp_eval.H_K = arry_supp_qual(10)
                t_supp_eval.H_SK = arry_supp_qual(11)
                t_supp_eval.H_KET = arry_supp_qual(12)
                t_supp_eval.PO_LP = arry_supp_qual(13)
                t_supp_eval.PO_LTA = arry_supp_qual(14)
                t_supp_eval.PO_CA = arry_supp_qual(15)
                t_supp_eval.PO_KET = arry_supp_qual(16)
                t_supp_eval.P_STTW = arry_supp_qual(17)
                t_supp_eval.P_KTTW = arry_supp_qual(18)
                t_supp_eval.P_TW = arry_supp_qual(19)
                t_supp_eval.P_KET = arry_supp_qual(20)
                t_supp_eval.HP_STSP = arry_supp_qual(21)
                t_supp_eval.HP_KTSP = arry_supp_qual(22)
                t_supp_eval.HP_SP = arry_supp_qual(23)
                t_supp_eval.HP_KET = arry_supp_qual(24)
                t_supp_eval.TOTAL_SCORE = arry_supp_qual(25)
                t_supp_eval.IS_RECOMMENDED = arry_supp_qual(26)
                t_supp_eval.KOMENTAR_SARAN = arry_supp_qual(27)
                t_supp_eval.DISIAPKAN_OLEH = arry_supp_qual(28)
                t_supp_eval.DISETUJUI_OLEH = arry_supp_qual(29)

                t_supp_eval.LAST_MODIFIED_TIME = Date.Now
                t_supp_eval.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(t_supp_eval).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

            Return rs
        End Function
#End Region

#Region "DELETE"
        <CAuthorize(Role:="MNU15")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim sUPPLIER As TPROC_SUPPLIER = db.TPROC_SUPPLIER.Find(id)
            If IsNothing(sUPPLIER) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", sUPPLIER)
        End Function

        <CAuthorize(Role:="MNU15")>
        Function ActionDelete(ByVal id As Decimal) As String
            Dim result As String = ""
            Using scope As New TransactionScope
                Try
                    Using db1 As New eProcurementEntities
                        Dim sUPPLIER As TPROC_SUPPLIER = db.TPROC_SUPPLIER.Find(id)

                        If sUPPLIER.TPROC_PO_HEADERS.Count > 0 Then
                            result = "0"
                        Else
                            sUPPLIER.ROW_STATUS = ListEnum.RowStat.InActive
                            sUPPLIER.LAST_MODIFIED_TIME = Date.Now
                            sUPPLIER.LAST_MODIFIED_BY = Session("USER_ID")
                            db.Entry(sUPPLIER).State = EntityState.Modified

                            Dim supp_qual As TPROC_SUPP_QUAL = db.TPROC_SUPP_QUAL.Where(Function(x) x.SUPPLIER_ID = id).FirstOrDefault()
                            supp_qual.ROW_STATUS = ListEnum.RowStat.InActive
                            supp_qual.LAST_MODIFIED_TIME = Date.Now
                            supp_qual.LAST_MODIFIED_BY = Session("USER_ID")
                            db.Entry(supp_qual).State = EntityState.Modified

                            db.SaveChanges()
                            scope.Complete()
                            result = "1"
                        End If


                    End Using
                Catch ex As Exception
                    result = "0"
                End Try

            End Using


            Return result
        End Function
#End Region

#Region "OTHER"
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU15")>
        Function CheckData(ByVal id As Decimal, ByVal supplier_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim sUPPLIER As New TPROC_SUPPLIER
            'check create
            If id = 0 Then
                sUPPLIER = db.TPROC_SUPPLIER.Where(Function(x) x.SUPPLIER_NAME.Replace(" ", "").ToUpper() = supplier_name.Replace(" ", "").ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If sUPPLIER IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                sUPPLIER = db.TPROC_SUPPLIER.Where(Function(x) x.SUPPLIER_NAME.Replace(" ", "").ToUpper() = supplier_name.Replace(" ", "").ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If sUPPLIER IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU15")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_SUPPLIER.Where(Function(x) x.SUPPLIER_NAME.Replace(" ", "").ToUpper() = value.Replace(" ", "").ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU15")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_SUPPLIER = db.TPROC_SUPPLIER.Find(id)
                obj.ROW_STATUS = ListEnum.RowStat.Live
                obj.LAST_MODIFIED_TIME = Date.Now
                obj.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(obj).State = EntityState.Modified
                db.SaveChanges()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


            Return RedirectToAction("Index")
        End Function
        Function PopSupplier() As ActionResult
            Dim supp As New List(Of TPROC_SUPPLIER)

            Return PartialView("_PopupSupplier", supp)
        End Function
        Function PopSearch(ByVal supp_name As String) As ActionResult
            Dim supp As New List(Of TPROC_SUPPLIER)
            If supp_name Is Nothing Or supp_name = "" Then
                supp = db.TPROC_SUPPLIER.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()
            Else
                supp = db.TPROC_SUPPLIER.Where(Function(x) x.SUPPLIER_NAME.ToUpper().Contains(supp_name.ToUpper()) And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()
            End If

            Return PartialView("_PopupSupplierList", supp)
        End Function


        <CAuthorize(Role:="MNU15")>
        Public Function Upload(formCollection As FormCollection) As ActionResult
            Dim sb As New StringBuilder
            Dim rs As New ResultStatus

            Try
                If Request IsNot Nothing Then
                    Dim file As HttpPostedFileBase = Request.Files("UploadedFile")
                    If (file IsNot Nothing) AndAlso (file.ContentLength > 0) AndAlso Not String.IsNullOrEmpty(file.FileName) Then
                        Dim fileName As String = file.FileName
                        Dim fileContentType As String = file.ContentType
                        Dim fileBytes As Byte() = New Byte(file.ContentLength - 1) {}
                        Dim data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength))

                        Using package = New ExcelPackage(file.InputStream)
                            Dim currentSheet = package.Workbook.Worksheets
                            Dim workSheet = currentSheet.First()
                            Dim noOfCol = workSheet.Dimension.[End].Column
                            Dim noOfRow = workSheet.Dimension.[End].Row

                            For rowIterator As Integer = 3 To noOfRow
                                Dim item As String = workSheet.Cells(rowIterator, 2).Value.ToString()
                                Dim isExist = CheckData(0, item)

                                If isExist = 1 Then
                                    sb.Append("Item " + item + " already exist." + "<br />")
                                    sb.AppendLine()
                                Else
                                    'START
                                    Dim vendor_code = Generate.GetNo("TPROC_SUPPLIER")

                                    Using scope As New TransactionScope
                                        Using db1 As New eProcurementEntities
                                            Dim sUPPLIER As New TPROC_SUPPLIER
                                            sUPPLIER.VENDOR_CODE = vendor_code
                                            sUPPLIER.SUPPLIER_NAME = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 2).Value)
                                            sUPPLIER.SUPPLIER_ALIAS_NAME = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 3).Value)
                                            sUPPLIER.CORE_BUSINESS = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 4).Value)
                                            sUPPLIER.B_UNIT_OWNER = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 5).Value)
                                            sUPPLIER.NAMA_BARANG = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 6).Value)
                                            sUPPLIER.CONTACT_PERSON = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 7).Value)
                                            sUPPLIER.SUPPLIER_ADDRESS = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 8).Value)
                                            sUPPLIER.CITY = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 9).Value)
                                            sUPPLIER.MOBILE_NUMBER = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 10).Value)
                                            sUPPLIER.EMAIL_ADDRESS = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 11).Value)
                                            sUPPLIER.FAX_NUMBER = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 12).Value)
                                            sUPPLIER.OFFICE_NUMBER = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 13).Value)
                                            sUPPLIER.TAX_NUMBER = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 14).Value)
                                            sUPPLIER.WEBSITE = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 15).Value)
                                            sUPPLIER.DESCRIPTION = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 16).Value)
                                            sUPPLIER.EFFECTIVE_DATE = Convert.ToDateTime(workSheet.Cells(rowIterator, 17).Value).ToString("MMM-yy")
                                            sUPPLIER.SCHEDULE_EVALUATION = Convert.ToDateTime(workSheet.Cells(rowIterator, 18).Value).ToString("MMM-yy")
                                            sUPPLIER.BANK_NAME = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 19).Value)
                                            sUPPLIER.BANK_BRANCH = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 20).Value)
                                            sUPPLIER.BANK_ACCOUNT_NUMBER = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 21).Value)
                                            sUPPLIER.NPWP = CommonFunction.GetValueNotEmpty(workSheet.Cells(rowIterator, 22).Value)

                                            sUPPLIER.CREATED_TIME = Date.Now
                                            sUPPLIER.CREATED_BY = Session("USER_ID")
                                            db1.TPROC_SUPPLIER.Add(sUPPLIER)
                                            db1.SaveChanges()
                                            rs.SetSuccessStatus()
                                        End Using

                                        'get supplier id
                                        Dim supp_id
                                        Using db1 As New eProcurementEntities
                                            supp_id = db1.TPROC_SUPPLIER.Where(Function(x) x.VENDOR_CODE = vendor_code And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
                                        End Using

                                        Dim weight = workSheet.Cells(rowIterator, 23).Value.ToString()
                                        Dim bridger = workSheet.Cells(rowIterator, 24).Value.ToString()
                                        Dim nda = workSheet.Cells(rowIterator, 25).Value.ToString()
                                        Dim cidci = workSheet.Cells(rowIterator, 26).Value.ToString()
                                        Dim legal = workSheet.Cells(rowIterator, 27).Value.ToString()
                                        Dim agree = workSheet.Cells(rowIterator, 28).Value.ToString()
                                        Dim valid = workSheet.Cells(rowIterator, 29).Value.ToString()

                                        Dim doc_checking As String = weight + "|" + bridger + "|" + nda + "|" + cidci + "|" + legal + "|" + agree + "|" + valid
                                        Dim supp_qual As String = ""
                                        Dim supp_eval As String = ""

                                        If rs.IsSuccess Then
                                            rs = InsertSuppDoc(supp_id, doc_checking)
                                            If rs.IsSuccess Then
                                                rs = InsertSuppQual(supp_id, supp_qual)
                                                If rs.IsSuccess Then
                                                    rs = InsertSuppEval(supp_id, supp_eval)
                                                    If rs.IsSuccess Then
                                                        rs = Generate.CommitGenerator("TPROC_SUPPLIER")
                                                        If rs.IsSuccess Then
                                                            scope.Complete()
                                                            rs.SetSuccessStatus()
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End Using
                                    'END

                                End If
                            Next
                        End Using
                    Else
                        sb.Append("Please select the file before")
                    End If
                End If
            Catch ex As Exception
                sb.Append(ex.Message + " please check format and available of value" + "<br />")
            End Try

            TempData("msg") = sb.ToString()

            Return RedirectToAction("Index")
        End Function

#End Region

        <CAuthorize(Role:="MNU15")>
        Function GetDataByRowStat(row_stat As Integer) As ActionResult
            Dim supp As New List(Of TPROC_SUPPLIER)
            If row_stat = ListEnum.RowStat.Live Then
                supp = db.TPROC_SUPPLIER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.SUPPLIER_NAME).ToList()
            Else
                supp = db.TPROC_SUPPLIER.Where(Function(y) y.ROW_STATUS = row_stat).OrderBy(Function(x) x.SUPPLIER_NAME).ToList()
            End If

            Return PartialView("_List", supp)
        End Function

    End Class
End Namespace
