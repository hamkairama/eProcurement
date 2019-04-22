Imports eProcurementApps.DataAccess
Imports System
Imports System.Data
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports System.Collections
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports Oracle.ManagedDataAccess.Client
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports System.Transactions
Imports System.Text
Imports System.Web
Imports System.Configuration

Public Class PurchaseOrderFacade

#Region "AREA PO"
    Public Function InsertPOHeader(ByVal user_id As String, ByVal po_header As TPROC_PO_HEADERS, ByRef po_header_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        'current_user_id = user_id
        'po_header.CREATED_TIME = Date.Now
        'po_header.CREATED_BY = current_user_id

        Try
            Using db As New eProcurementEntities
                db.TPROC_PO_HEADERS.Add(po_header)
                db.SaveChanges()
                po_header_id = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_NUMBER = po_header.PO_NUMBER And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
                rs.SetSuccessStatus()
            End Using

            'If rs.IsSuccess Then
            '    rs = InsertApprReldept(pr_header_id, pr_header.SUB_TYPE_ID, pr_header.SUB_TOTAL)
            'End If

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message & "|| /n" & ex.StackTrace.ToString() & "|| /n" & ex.InnerException.ToString() & "|| /n" & ex.Source & "|| PO HEADER")
        End Try

        Return rs
    End Function

    Public Function InsertPODetail(ByVal user_id As String, ByVal po_details As TPROC_PO_DETAILS, ByVal pr_number_id As Decimal, ByRef po_detail_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim pr_number As String

        Try
            Using db As New eProcurementEntities
                pr_number = db.TPROC_PR_HEADER.Where(Function(x) x.ID = pr_number_id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().PR_NO
                po_details.PR_HEADER_NO = pr_number
                po_details.PO_DTLS_ITEM_ID = po_detail_id
                db.TPROC_PO_DETAILS.Add(po_details)
                db.SaveChanges()
                po_detail_id = db.TPROC_PO_DETAILS.Where(Function(x) x.PO_DTLS_ITEM_ID = po_details.PO_DTLS_ITEM_ID And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().PO_DTLS_ITEM_ID
                rs.SetSuccessStatus()
            End Using

            'If rs.IsSuccess Then
            '    rs = InsertApprReldept(pr_header_id, pr_header.SUB_TYPE_ID, pr_header.SUB_TOTAL)
            'End If

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function InsertPODetailItem(ByVal user_id As String, ByVal po_details_itm As TPROC_PO_DETAILS_ITEM, ByRef po_detail_itm_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        'Dim pr_number As String

        Try
            Using db As New eProcurementEntities
                'pr_number = db.TPROC_PR_HEADER.Where(Function(x) x.ID = pr_number_id).FirstOrDefault().PR_NO
                'po_details_itm.PR_NO = pr_number
                db.TPROC_PO_DETAILS_ITEM.Add(po_details_itm)
                db.SaveChanges()
                po_detail_itm_id = db.TPROC_PO_DETAILS_ITEM.Where(Function(x) x.PR_NO = po_details_itm.PR_NO And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
                rs.SetSuccessStatus()
            End Using

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function


    Public Function UpdatePoNumberSupp_PrDetail(pr_id As Decimal, item_nm As String, price As Decimal, po_header As TPROC_PO_HEADERS) As ResultStatus
        Dim rs As New ResultStatus
        Try
            Using db As New eProcurementEntities
                Dim pr_detail = db.TPROC_PR_DETAIL.Where(Function(x) x.PR_HEADER_ID = pr_id And x.ITEM_NAME = item_nm And x.PRICE = price).FirstOrDefault()
                pr_detail.PO_NUMBER = po_header.PO_NUMBER
                pr_detail.SUPPLIER_ID = po_header.SUPPLIER_ID
                pr_detail.SUPPLIER_NAME = po_header.SUPPLIER_NAME
                pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                pr_detail.LAST_MODIFIED_TIME = Date.Now
                db.Entry(pr_detail).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdatePoNumberSupp_PrDetail_PoRejected(pr_id As Decimal, item_nm As String, qty As Decimal, price As Decimal, po_header As TPROC_PO_HEADERS) As ResultStatus
        Dim rs As New ResultStatus
        Try
            Using db As New eProcurementEntities
                Dim pr_detail = db.TPROC_PR_DETAIL.Where(Function(x) x.PR_HEADER_ID = pr_id And x.ITEM_NAME = item_nm And x.QTY = qty And x.PRICE = price).FirstOrDefault()
                pr_detail.PO_NUMBER = Nothing
                pr_detail.SUPPLIER_ID = Nothing
                pr_detail.SUPPLIER_NAME = Nothing
                pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                pr_detail.LAST_MODIFIED_TIME = Date.Now
                db.Entry(pr_detail).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusPO(po_header As TPROC_PO_HEADERS, new_status As String, user_id As String, ByRef appr_po_id As Decimal, ByRef vrf_po_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            'Dim status_old = db.TPROC_FIELD_VALUES.Where(Function(x) x.FLD_VALU_DESC = old_status And x.STAT_CD = "A" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            'Dim status_new = db.TPROC_FIELD_VALUES.Where(Function(x) x.FLD_VALU_DESC = status And x.STAT_CD = "A" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()

            'Dim status_po = db.TPROC_PO_HEADERS.Where(Function(x) x.ID = PO_HEADER_PARAM.GetPOHeaderId And x.PO_STATUS = status_old.ID And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            Dim po_header_new = db.TPROC_PO_HEADERS.Find(po_header.ID)

            If po_header_new.PO_STATUS = ListEnum.PO.Rejected Or (po_header_new.PO_STATUS = ListEnum.PO.Verified And new_status <> ListEnum.PO.Approved.ToString()) Then
                rs.SetErrorStatus("Request is already updated status by other. Please refresh you browser")
                Return rs
            End If

            If new_status.Equals("Reviewed") Then
                po_header_new.PO_STATUS = ListEnum.PO.Reviewed
                po_header_new.REVIEWED_BY = CurrentUser.GetCurrentUserId()
                po_header_new.REVIEWED_DATE = Date.Now
            ElseIf new_status.Equals("Approved") Then
                po_header_new.PO_STATUS = ListEnum.PO.Approved
                po_header_new.APPOROVED_BY = CurrentUser.GetCurrentUserId()
                po_header_new.APPOROVED_DATE = Date.Now
            ElseIf new_status.Equals("Completed") Then
                po_header_new.PO_STATUS = ListEnum.PO.Completed
                po_header_new.COMPLETED_BY = CurrentUser.GetCurrentUserId()
                po_header_new.COMPLETED_DATE = Date.Now
            ElseIf new_status.Equals("Verified") Then
                po_header_new.PO_STATUS = ListEnum.PO.Verified
                po_header_new.VERIFIED_BY = CurrentUser.GetCurrentUserId()
                po_header_new.VERIFIED_DATE = Date.Now
            End If

            po_header_new.LAST_MODIFIED_TIME = Date.Now
            po_header_new.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(po_header_new).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

            'get ref appr id base on 
            For Each item In po_header_new.TPROC_APPR_PO.ToList()
                If item.USER_ID.ToUpper() = user_id.ToUpper() Then
                    appr_po_id = item.ID
                    Exit For
                End If
            Next

            'get ref VRF id base on 
            For Each item_vrf In po_header_new.TPROC_VRF_PO.ToList()
                If item_vrf.USER_ID.ToUpper() = user_id.ToUpper() Then
                    vrf_po_id = item_vrf.ID
                    Exit For
                End If
            Next

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusApprPO(appr_po_id As Decimal, user_id As String, new_status As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim appr_po As New TPROC_APPR_PO

        Try
            appr_po = db.TPROC_APPR_PO.Find(appr_po_id)

            appr_po.STATUS = new_status
            appr_po.LAST_MODIFIED_TIME = Date.Now
            appr_po.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(appr_po).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusVrfPO(vrf_po_id As Decimal, user_id As String, new_status As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim vrf_po As New TPROC_VRF_PO

        Try
            vrf_po = db.TPROC_VRF_PO.Find(vrf_po_id)

            vrf_po.STATUS = new_status
            vrf_po.LAST_MODIFIED_TIME = Date.Now
            vrf_po.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(vrf_po).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusPOFromPC(pc_id As Decimal, status As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            Dim status_po = db.TPROC_PO_HEADERS.Where(Function(x) x.PC_ID = pc_id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()

            status_po.PO_STATUS = status
            status_po.LAST_MODIFIED_TIME = Date.Now
            status_po.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            If status = ListEnum.PO.Completed Then
                status_po.COMPLETED_BY = CurrentUser.GetCurrentUserId()
                status_po.COMPLETED_DATE = Date.Now
            ElseIf status = ListEnum.PO.Rejected Then
                status_po.REJECT_BY = CurrentUser.GetCurrentUserId()
                status_po.REJECT_DATE = Date.Now
            End If

            db.Entry(status_po).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function SendEmailApprPO(po_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim po = GetPoById(po_id)
        Dim appr = db.TPROC_APPR_PO.Where(Function(x) x.PO_ID = po.ID).ToList()

        Dim poNum As String = po.PO_NUMBER
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPO")
        Dim flag = Convert.ToString(ListEnum.ViewPageArea.Approval)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + po.ID.ToString())  '+ "?flag=" + flag
        Dim reqDate As String = po.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPo As String = po.PO_STATUS
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        Dim emailToReviewAppr As New ListFieldNameAndValue
        Dim emailToApproveAppr As New ListFieldNameAndValue
        For Each item In appr
            If item.STATUS = "Waiting for review" Then
                emailToReviewAppr.AddItem("Email", item.EMAIL)
            Else
                emailToApproveAppr.AddItem("Email", item.EMAIL)
            End If
        Next

        If emailToReviewAppr.Count > 0 Then
            result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, poNum, ListEnum.eProcApprAction.review.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, poNum, reqDate, statusPo),
                                            sendEmail2)
        Else
            result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, poNum, ListEnum.eProcApprAction.approve.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, poNum, reqDate, statusPo),
                                            sendEmail2)
        End If

        Return result
    End Function

    Public Function SendEmailCreatorPO(po_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim po = GetPoById(po_id)

        Dim poNum As String = po.PO_NUMBER
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPO")
        Dim flag = Convert.ToString(ListEnum.ViewPageArea.Approval)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + po.ID.ToString())  '+ "?flag=" + flag
        Dim reqDate As String = po.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPo As String = [Enum].GetName(GetType(ListEnum.PO), Int32.Parse(po.PO_STATUS)).ToString()
        'Dim statusPo As String = po.PO_STATUS
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        Dim emailToCreatorPo As New ListFieldNameAndValue

        'get email the creator po base on po.createdby
        Dim usr_mail = db.TPROC_USER.Where(Function(x) x.USER_ID.ToUpper() = po.CREATED_BY.ToUpper()).FirstOrDefault().USER_MAIL
        emailToCreatorPo.AddItem("Email", usr_mail)

        result = mailFacade.SendEmail(emailSender, emailToCreatorPo, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, poNum, ListEnum.eProcApprAction.complete.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, poNum, reqDate, statusPo),
                                            sendEmail2)

        Return result
    End Function

    Public Function SendEmailCreatorPONotif(po_id As Decimal, new_status As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim po = GetPoById(po_id)

        Dim poNum As String = po.PO_NUMBER
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPO")
        Dim flag = Convert.ToString(ListEnum.ViewPageArea.Approval)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + po.ID.ToString())  '+ "?flag=" + flag
        Dim reqDate As String = po.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPo As String = [Enum].GetName(GetType(ListEnum.PO), Int32.Parse(po.PO_STATUS)).ToString()
        'Dim statusPo As String = po.PO_STATUS
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        Dim emailToCreatorPo As New ListFieldNameAndValue

        'get email the creator po base on po.createdby
        Dim usr_mail = db.TPROC_USER.Where(Function(x) x.USER_ID.ToUpper() = po.CREATED_BY.ToUpper()).FirstOrDefault().USER_MAIL
        emailToCreatorPo.AddItem("Email", usr_mail)

        result = mailFacade.SendEmail(emailSender, emailToCreatorPo, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPONotif, poNum, new_status),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification_2, link, poNum, reqDate, statusPo),
                                            sendEmail2)

        Return result
    End Function

    Public Function SendEmailVrf(po_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim po = GetPoById(po_id)
        Dim appr = db.TPROC_VRF_PO.Where(Function(x) x.PO_ID = po.ID).ToList()

        Dim poNum As String = po.PO_NUMBER
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPO")
        Dim flag = Convert.ToString(ListEnum.ViewPageArea.Verify)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + po.ID.ToString())  '+ "?flag=" + flag
        Dim reqDate As String = po.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPo As String = po.PO_STATUS
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        Dim emailToVrf As New ListFieldNameAndValue
        For Each item In appr
            If item.STATUS = "Waiting for verify" Then
                emailToVrf.AddItem("Email", item.EMAIL)
            End If
        Next

        If emailToVrf.Count > 0 Then
            result = mailFacade.SendEmail(emailSender, emailToVrf, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, poNum, ListEnum.eProcApprAction.verify.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, poNum, reqDate, statusPo),
                                            sendEmail2)
        End If

        Return result
    End Function

    Function GetPoById(id As Decimal) As TPROC_PO_HEADERS
        Dim db As New eProcurementEntities
        Return db.TPROC_PO_HEADERS.Find(id)
    End Function


    Public Function GetVerifyRoleFor(role_name As String) As List(Of APPROVAL_ROLE)
        Dim lvrf_po As New List(Of APPROVAL_ROLE)

        Try
            Using db As New eProcurementEntities
                Dim query = (From _ap In db.TPROC_APPROVAL_ROLE
                             Join _dt In db.TPROC_APPROVAL_ROLE_DETAIL On _dt.ID_APPROVAL_ROLE Equals (_ap.ID)
                             Join _level In db.TPROC_LEVEL On _level.ID Equals (_ap.LEVEL_ID)
                             Where _ap.ROW_STATUS = 0 And _dt.AS_IS = "Verifier" And _dt.ROLE_NAME = role_name
                             Select _ap.USER_ID, _ap.USER_NAME, _ap.EMAIL, _level.RUPIAH_LIMIT
                             Distinct
                             Order By USER_ID).ToList()

                For Each item In query
                    Dim re As New APPROVAL_ROLE
                    re.USER_ID = item.USER_ID
                    re.NAME = item.USER_NAME
                    re.EMAIL = item.EMAIL
                    re.RUPIAH_LIMIT = item.RUPIAH_LIMIT

                    lvrf_po.Add(re)
                Next
            End Using
        Catch ex As Exception
        End Try

        Return lvrf_po
    End Function

    Public Function GetApprovalRoleFor(role_name As String) As List(Of APPROVAL_ROLE)
        Dim lappr_po As New List(Of APPROVAL_ROLE)

        Try
            Using db As New eProcurementEntities
                Dim query = (From _ap In db.TPROC_APPROVAL_ROLE
                             Join _dt In db.TPROC_APPROVAL_ROLE_DETAIL On _dt.ID_APPROVAL_ROLE Equals (_ap.ID)
                             Join _level In db.TPROC_LEVEL On _level.ID Equals (_ap.LEVEL_ID)
                             Where _ap.ROW_STATUS = "0" And _dt.AS_IS = "Approver" And _dt.ROLE_NAME = role_name
                             Select _ap.USER_ID, _ap.USER_NAME, _ap.EMAIL, _level.RUPIAH_LIMIT
                             Distinct
                             Order By USER_ID).ToList()

                For Each item In query
                    Dim re As New APPROVAL_ROLE
                    re.USER_ID = item.USER_ID
                    re.NAME = item.USER_NAME
                    re.EMAIL = item.EMAIL
                    re.RUPIAH_LIMIT = item.RUPIAH_LIMIT

                    lappr_po.Add(re)
                Next
            End Using
        Catch ex As Exception
        End Try

        Return lappr_po
    End Function

    Public Function UpdatePRByPoReject(po_number As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim pr_dt As New List(Of TPROC_PR_DETAIL)

        Try

            Using db As New eProcurementEntities
                pr_dt = db.TPROC_PR_DETAIL.Where(Function(x) x.PO_NUMBER = po_number).ToList()
            End Using

            For Each item As TPROC_PR_DETAIL In pr_dt
                Using db2 As New eProcurementEntities
                    Dim new_pdr_dt As New TPROC_PR_DETAIL
                    new_pdr_dt = item
                    new_pdr_dt.SUPPLIER_ID = Nothing
                    new_pdr_dt.SUPPLIER_NAME = Nothing
                    new_pdr_dt.PO_NUMBER = Nothing
                    db2.Entry(new_pdr_dt).State = EntityState.Modified
                    db2.SaveChanges()
                    rs.SetSuccessStatus()
                End Using
            Next
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function


    Public Function UpdateStatusPC(pc_id As Decimal, status As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            Dim po = db.TPROC_PC.Find(pc_id)
            po.STATUS = status
            db.Entry(po).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusPORejected(po_id As Decimal, status As Integer, by_on As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            Dim po = db.TPROC_PO_HEADERS.Find(po_id)
            If status = ListEnum.PO.Rejected Then
                If po.PO_STATUS = ListEnum.PO.Verified And by_on = "RejectedByVerifier" Then
                    rs.SetErrorStatus("Request is already verified by other. Please refresh your browser")
                    Return rs
                End If

                If po.PO_STATUS = ListEnum.PO.Approved And by_on = "RejectedByApprover" Then
                    rs.SetErrorStatus("Request is already approved by other. Please refresh your browser")
                    Return rs
                End If
            End If
            po.PO_STATUS = status
            po.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
            po.LAST_MODIFIED_TIME = Date.Now
            db.Entry(po).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    '===========================================================PROCEDURE=============================================================================
    Public Function POP_SRC_DataList(ByVal prd As String, ByVal comp As String, ByVal unitid As String, ByVal nik As String, ByVal statsM As String, ByVal nameM As String, ByVal tpPage As String, ByVal namaUN As String, ByVal sortF As String) As DataSet
        Dim odal As New DalOracle
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim ds As New DataSet

        carr.Add(New cArrayList("FLAG_PG", tpPage))
        odal.fn_GetOracleDataReader("yks_sp_SearchPOP_Find", carr, odr)
        ds.Load(odr, LoadOption.Upsert, "nik", "empNAME", "Unitname", "companyname", "productname", "EMPSTATS_DESC", "UnitID")
        odr.Close()
        carr.Clear()
        Return ds
    End Function

    Public Function GetVerifyRoleFor2(role_name As String) As List(Of APPROVAL_ROLE)
        Dim odal As New DalOracle
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim lvrf_po As New List(Of APPROVAL_ROLE)

        carr.Add(New cArrayList("roleName", role_name))
        odal.fn_GetOracleDataReader("SP_GET_VERIFY_ROLE", carr, odr)

        If odr.HasRows Then
            While odr.Read
                Dim vrf_po As New APPROVAL_ROLE
                vrf_po.USER_ID = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_ID")))
                vrf_po.NAME = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_NAME")))
                vrf_po.EMAIL = Convert.ToString(odr.GetValue(odr.GetOrdinal("EMAIL")))
                lvrf_po.Add(vrf_po)
            End While
        End If

        'odr.Close()
        'carr.Clear()

        Return lvrf_po
    End Function

    Public Function GetApprovalRoleFor2(role_name As String) As List(Of APPROVAL_ROLE)
        Dim odal As New DalOracle
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim lappr_po As New List(Of APPROVAL_ROLE)

        carr.Add(New cArrayList("roleName", role_name))
        odal.fn_GetOracleDataReader("SP_GET_APPROVAL_ROLE", carr, odr)

        If odr.HasRows Then
            While odr.Read
                Dim appr_po As New APPROVAL_ROLE
                appr_po.USER_ID = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_ID")))
                appr_po.NAME = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_NAME")))
                appr_po.EMAIL = Convert.ToString(odr.GetValue(odr.GetOrdinal("EMAIL")))
                appr_po.RUPIAH_LIMIT = Convert.ToDecimal(odr.GetValue(odr.GetOrdinal("RUPIAH_LIMIT")))
                lappr_po.Add(appr_po)
            End While
        End If

        'odr.Close()
        'carr.Clear()

        Return lappr_po
    End Function

    'Public Function InsertVerifyRoleForPO(po_id As Decimal, user_id As String) As ResultStatus
    '    Dim odal As New DalOracle
    '    Dim odr As OracleDataReader = Nothing
    '    Dim carr As New ArrayList
    '    Dim rs As New ResultStatus
    '    Dim result As Integer

    '    carr.Add(New cArrayList("po_id", po_id))
    '    carr.Add(New cArrayList("created_by", user_id))
    '    odal.fn_AddRecordSP("SP_INS_VERIFY_ROLE_TO_PO", carr, result)

    '    If result = 1 Then
    '        rs.Status = True
    '    End If

    '    odr.Close()
    '    carr.Clear()

    '    Return rs
    'End Function

    'Public Function InsertApprovalRoleForPO(po_id As Decimal, user_id As String) As ResultStatus
    '    Dim odal As New DalOracle
    '    Dim odr As OracleDataReader = Nothing
    '    Dim carr As New ArrayList
    '    Dim rs As New ResultStatus
    '    Dim result As Integer

    '    carr.Add(New cArrayList("po_id", po_id))
    '    carr.Add(New cArrayList("created_by", user_id))
    '    odal.fn_AddRecordSP("SP_INS_VERIFY_ROLE_TO_PO", carr, result)

    '    If result = 1 Then
    '        rs.Status = True
    '    End If

    '    odr.Close()
    '    carr.Clear()

    '    Return rs
    'End Function

    '===========================================================PROCEDURE=============================================================================

#End Region


End Class
