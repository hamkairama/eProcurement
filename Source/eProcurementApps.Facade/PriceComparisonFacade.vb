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

Public Class PriceComparisonFacade

    Private lMapping_PcDt As New List(Of MAPPING_PCDT)
    Private lMapping_SpGr As New List(Of MAPPING_SPGR)

    '<HttpPost>
    Public Function InsertPriceCom(ByVal ObjectsPCx As OBJECTSPC, ByVal listOfObjectsHeaderx As List(Of OBJECTSHEADER), ByVal listOfObjectsBodyx As List(Of OBJECTSBODY),
                                   ByVal listMappingSuppItemx As List(Of MAPPINGSUPPITEM), ByVal listOfObjectsFooterx As List(Of OBJECTSFOOTER),
                                   ByRef priceComNum As String, ByRef pc_id As Decimal, ByVal for_storage As Integer, file_name As String, ByVal cb_dropdownList_potyp_nm As String) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope()
            Try
                rs = InsertPC(ObjectsPCx, pc_id, priceComNum, for_storage, file_name, cb_dropdownList_potyp_nm)
                If rs.IsSuccess Then
                    rs = InsertPcSpGr(listOfObjectsFooterx, pc_id)
                    If rs.IsSuccess Then
                        rs = InsertPcDt(listOfObjectsBodyx, pc_id)
                        If rs.IsSuccess Then
                            rs = InsertPcSpDt(listMappingSuppItemx, listOfObjectsHeaderx.Count)
                            If rs.IsSuccess Then
                                rs = InsertPCHistorical(pc_id, Date.Now, ListEnum.PriceCom.Submitted.ToString(), CurrentUser.GetCurrentUserName(), Date.Now)
                                If rs.IsSuccess Then
                                    Generate.CommitGenerator("TPROC_PC")
                                    If rs.IsSuccess Then
                                        scope.Complete()
                                        rs.SetSuccessStatus()
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Function InsertPC(ByVal obj As OBJECTSPC, ByRef pc_id As Decimal, ByRef priceComNum As String, ByVal for_storage As Integer, ByVal file_name As String, ByVal cb_dropdownList_potyp_nm As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim pc_new As New List(Of TPROC_PC)

        Try
            Using db As New eProcurementEntities
                Dim pc As New TPROC_PC
                Dim gNumb = Generate.GetPCSeq("TPROC_PC") + cb_dropdownList_potyp_nm
                pc.PC_NUM = gNumb
                pc.PO_TYPE_NM = obj.Po_type_nm
                pc.RECOM_SUPPLIER_NM = obj.Recom_supplier_nm
                pc.RECOM_SUPPLIER_ID = obj.Recom_supplier_id
                pc.RECOM_SUPPLIER_CP = obj.Recom_supplier_cp
                pc.RECOM_SUPPLIER_PHONE = obj.Recom_supplier_phone
                pc.RECOM_SUPPLIER_FAX = obj.Recom_supplier_fax
                pc.RECOM_SUPPLIER_ADDRESS = obj.Recom_supplier_address
                pc.DELIVERY_NM = obj.Delivery_nm
                pc.DELIVERY_ID = obj.Delivery_id
                pc.DELIVERY_DATE = obj.Delivery_date
                pc.DELIVERY_PHONE = obj.Delivery_phone
                pc.DELIVERY_FAX = obj.Delivery_fax
                pc.DELIVERY_ADDRESS = obj.Delivery_address
                pc.GRAND_TOTAL = obj.Grand_total
                pc.NOTE_BY_USER = obj.Note_by_user
                pc.NOTE_BY_EPROC = obj.Note_by_eproc
                pc.IS_DISC_PERC = obj.Is_disc_perc
                pc.IS_VAT_PERC = obj.Is_vat_perc
                pc.IS_PPH_PERC = obj.Is_pph_perc
                pc.STATUS = ListEnum.PriceCom.Submitted.ToString()
                pc.CURRENCY = obj.Currency
                pc.NOTES = obj.Notes
                pc.IS_ACKNOWLEDGE_USER = obj.Is_acknowledge_user
                pc.FOR_STORAGE = for_storage
                pc.FILE_NAME = file_name
                pc.CREATED_BY = CurrentUser.GetCurrentUserId
                pc.CREATED_TIME = Date.Now

                db.TPROC_PC.Add(pc)
                db.SaveChanges()
                rs.SetSuccessStatus()

                'pc_id = pc.ID
                'pc_new = db.TPROC_PC.Where(Function(x) x.PC_NUM = gNumb).ToList()
                'pc_id = (From d In pc_new Select d.ID).Max()

                pc_id = db.TPROC_PC.Where(Function(x) x.PC_NUM = gNumb).FirstOrDefault().ID
                priceComNum = gNumb
            End Using

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Function InsertPcSpGr(ByVal obj As List(Of OBJECTSFOOTER), ByVal pc_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            For Each item In obj
                Dim pc_sp_gr As New TPROC_PC_SP_GR
                pc_sp_gr.PC_ID = pc_id
                pc_sp_gr.SUPP_ID = item.Supp_id
                pc_sp_gr.SUPP_NM = item.Supp_nm
                pc_sp_gr.IS_USED = item.Is_used
                pc_sp_gr.COL_NUM = item.Col_num

                pc_sp_gr.SUB_TOTAL = item.Sub_total
                pc_sp_gr.DISCOUNT_TEMP = item.Disc_temp
                pc_sp_gr.DISCOUNT = item.Disc
                pc_sp_gr.VAT_TEMP = item.Vat_temp
                pc_sp_gr.VAT = item.Vat
                pc_sp_gr.PPH_TEMP = item.Pph_temp
                pc_sp_gr.PPH = item.Pph

                pc_sp_gr.GRAND_TOTAL = item.Grand_total
                pc_sp_gr.DESCRIPTION = item.Desc
                pc_sp_gr.CREATED_BY = CurrentUser.GetCurrentUserId
                pc_sp_gr.CREATED_TIME = Date.Now

                Dim sp_gr_id As Decimal
                Using db As New eProcurementEntities
                    db.TPROC_PC_SP_GR.Add(pc_sp_gr)
                    db.SaveChanges()
                    rs.SetSuccessStatus()
                    sp_gr_id = db.TPROC_PC_SP_GR.Where(Function(x) x.PC_ID = pc_id And x.SUPP_ID = item.Supp_id And x.COL_NUM = item.Col_num).FirstOrDefault().ID
                End Using

                Dim mp As New MAPPING_SPGR
                mp.Col = item.Col_num
                mp.Sp_gr_id = sp_gr_id

                lMapping_SpGr.Add(mp)

            Next

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Function InsertPcDt(ByVal obj As List(Of OBJECTSBODY), ByVal pc_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            For Each item In obj
                Dim pc_dt As New TPROC_PC_DT
                pc_dt.ITEM_NAME = item.Item
                pc_dt.UNIT_MEASUREMENT = item.Um
                pc_dt.QUANTITY = item.Qty
                pc_dt.PRICE = item.Price
                pc_dt.TOTAL = item.Total
                pc_dt.ROW_NUM = item.Row
                pc_dt.PC_ID = pc_id
                pc_dt.CREATED_BY = CurrentUser.GetCurrentUserId
                pc_dt.CREATED_TIME = Date.Now

                Dim dt_id As Decimal
                Using db As New eProcurementEntities
                    db.TPROC_PC_DT.Add(pc_dt)
                    db.SaveChanges()
                    rs.SetSuccessStatus()
                    'dt_id = db.TPROC_PC_DT.ToList().Max().ID
                    dt_id = db.TPROC_PC_DT.Where(Function(x) x.ITEM_NAME = item.Item And x.QUANTITY = item.Qty And x.PRICE = item.Price And x.ROW_NUM = item.Row And x.PC_ID = pc_id).FirstOrDefault().ID
                End Using

                Dim mp As New MAPPING_PCDT
                mp.Row = item.Row
                mp.Pc_dt_id = dt_id

                lMapping_PcDt.Add(mp)

            Next
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Function InsertPcSpDt(ByVal obj As List(Of MAPPINGSUPPITEM), ByVal countHeader As Integer) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Dim start As Integer = 0
            For i As Integer = 0 To lMapping_PcDt.Count - 1
                For j As Integer = 0 To countHeader - 1
                    Dim pc_sp_dt As New TPROC_PC_SP_DT
                    pc_sp_dt.PRICE_PER_PIECE = Convert.ToDecimal(obj(start).Supp_each_price)
                    pc_sp_dt.PRICE_TOTAL = Convert.ToDecimal(obj(start).Supp_total_price)
                    pc_sp_dt.PC_SP_GR_ID = lMapping_SpGr(j).Sp_gr_id
                    pc_sp_dt.PC_DT_ID = lMapping_PcDt(i).Pc_dt_id
                    pc_sp_dt.CREATED_BY = CurrentUser.GetCurrentUserId
                    pc_sp_dt.CREATED_TIME = Date.Now

                    Using db As New eProcurementEntities
                        db.TPROC_PC_SP_DT.Add(pc_sp_dt)
                        db.SaveChanges()
                        rs.SetSuccessStatus()
                    End Using

                    start += 1
                Next j
            Next i

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function SendEmailApprAcknowUser(pc_id As Decimal, ByRef exsist_acknow As Boolean) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim pc = GetPcById(pc_id)
        Dim acknow_appr_gr = db.TPROC_ACKNOW_APPR.Where(Function(x) x.PC_ID = pc.ID).ToList()

        'set success
        result.SetSuccessStatus()

        If acknow_appr_gr IsNot Nothing Then
            Dim pcNum As String = pc.PC_NUM
            Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPC")
            Dim flag As String = Convert.ToString(ListEnum.FlagInbox.ApprAckPC)
            Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pc_id.ToString() + "?flag=" + flag)
            Dim reqDate As String = pc.CREATED_TIME.ToString("dd-MM-yyyy")
            Dim statusPC As String = pc.STATUS
            Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")


            For Each item_gr In acknow_appr_gr
                Dim emailToApproveAppr As New ListFieldNameAndValue

                For Each item_dt In item_gr.TPROC_ACKNOW_APPR_DT
                    emailToApproveAppr.AddItem("Email", item_dt.USER_EMAIL)
                Next

                result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, pcNum, ListEnum.eProcApprAction.approve.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, pcNum, reqDate, statusPC),
                                                sendEmail2)
            Next
            exsist_acknow = True
        Else
            exsist_acknow = False
        End If

        Return result
    End Function

    Public Function SendEmailApprPC(pc_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim pc = GetPcById(pc_id)
        Dim appr = db.TPROC_APPR_PC.Where(Function(x) x.PC_ID = pc.ID).ToList()

        Dim pcNum As String = pc.PC_NUM
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPC")
        Dim flag = Convert.ToString(ListEnum.FlagInbox.ApprPC)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pc_id.ToString() + "?flag=" + flag)
        Dim reqDate As String = pc.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPC As String = pc.STATUS
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
            result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, pcNum, ListEnum.eProcApprAction.review.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, pcNum, reqDate, statusPC),
                                            sendEmail2)
        Else
            result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, pcNum, ListEnum.eProcApprAction.approve.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, pcNum, reqDate, statusPC),
                                            sendEmail2)
        End If

        Return result
    End Function

    Public Function SendEmailVrfPC(pc_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim pc = GetPcById(pc_id)

        Dim pcNum As String = pc.PC_NUM
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPC")
        Dim flag = Convert.ToString(ListEnum.FlagInbox.VrfPC)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pc_id.ToString() + "?flag=" + flag)
        Dim reqDate As String = pc.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPC As String = pc.STATUS
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        Dim emailToVrf As New ListFieldNameAndValue
        Using db2 As New eProcurementEntities
            Dim vrf = db2.TPROC_VRF_PC.Where(Function(x) x.PC_ID = pc.ID).ToList()
            For Each item In vrf
                If item.STATUS = "Waiting for verify" Then
                    emailToVrf.AddItem("Email", item.EMAIL)
                End If
            Next
        End Using

        If emailToVrf.Count > 0 Then
            result = mailFacade.SendEmail(emailSender, emailToVrf, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPO, pcNum, ListEnum.eProcApprAction.verify.ToString()),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification, link, pcNum, reqDate, statusPC),
                                            sendEmail2)
        End If

        Return result
    End Function

    Public Function SendEmailCreatorPCNotif(pc_id As Decimal, new_status As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim pc = GetPcById(pc_id)

        Dim pcNum As String = pc.PC_NUM
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPC")
        Dim flag = Convert.ToString(ListEnum.ViewPageArea.Viewed)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pc.ID.ToString() + "?flag=" + flag)
        Dim reqDate As String = pc.CREATED_TIME.ToString("dd-MM-yyyy")
        Dim statusPo As String = pc.STATUS
        'Dim statusPo As String = po.PO_STATUS
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        Dim emailToCreatorPo As New ListFieldNameAndValue

        'get email the creator po base on po.createdby
        Dim usr_mail = db.TPROC_USER.Where(Function(x) x.USER_ID = pc.CREATED_BY).FirstOrDefault().USER_MAIL
        emailToCreatorPo.AddItem("Email", usr_mail)

        result = mailFacade.SendEmail(emailSender, emailToCreatorPo, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPCPONotif, pcNum, new_status),
                                        String.Format(EmailTemplate.eProcEmailTemplatePCPONotification_2, link, pcNum, reqDate, statusPo),
                                            sendEmail2)

        Return result
    End Function

    Function GetPcById(id As Decimal) As TPROC_PC
        Dim pc As New TPROC_PC
        Using db2 As New eProcurementEntities
            pc = db2.TPROC_PC.Find(id)
        End Using

        Return pc
    End Function

    Public Shared Function GetStatusApprovalAs(pc_id As Decimal, user_id As String) As String
        Dim db As New eProcurementEntities
        Dim result = db.TPROC_APPR_PC.Where(Function(x) x.PC_ID = pc_id And x.USER_ID = user_id).FirstOrDefault().STATUS.ToString()

        Return result
    End Function

    Function InsertAcknowledgePc(lacknowledge As List(Of OBJECTSWA), pc_id As Decimal, user_id As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim facade As New PriceComparisonFacade

        Using scope As New TransactionScope()
            Try
                For Each item In lacknowledge
                    Dim ack_appr As New TPROC_ACKNOW_APPR
                    ack_appr.PC_ID = pc_id
                    ack_appr.WA_ID = item.Wa_id
                    ack_appr.WA_NUMBER = item.Wa_number
                    ack_appr.CREATED_BY = user_id
                    ack_appr.CREATED_TIME = Date.Now

                    Dim ack_appr_id As Decimal
                    Using db As New eProcurementEntities
                        db.TPROC_ACKNOW_APPR.Add(ack_appr)
                        db.SaveChanges()

                        ack_appr_id = db.TPROC_ACKNOW_APPR.Where(Function(x) x.PC_ID = pc_id And x.WA_ID = item.Wa_id).FirstOrDefault().ID
                    End Using

                    Dim dt_appr = item.Wa_approval.Split("|")
                    For i As Integer = 1 To dt_appr.Count - 1
                        Dim persons = dt_appr(i).Split("-")
                        Dim ack_appr_dt As New TPROC_ACKNOW_APPR_DT
                        ack_appr_dt.ACKNOW_APPR_ID = ack_appr_id
                        ack_appr_dt.USER_ID = persons(0)
                        ack_appr_dt.USER_NAME = persons(1)
                        ack_appr_dt.USER_EMAIL = persons(2)
                        ack_appr_dt.STATUS = "Waiting for approve"
                        ack_appr_dt.CREATED_BY = user_id
                        ack_appr_dt.CREATED_TIME = Date.Now
                        Using db2 As New eProcurementEntities
                            db2.TPROC_ACKNOW_APPR_DT.Add(ack_appr_dt)
                            db2.SaveChanges()
                            rs.SetSuccessStatus()
                        End Using
                    Next i

                Next

                If rs.IsSuccess Then
                    scope.Complete()
                    rs.SetSuccessStatus()
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Function InsertPCHistorical(ByVal pc_id As Decimal, ByVal historical_dt As DateTime, ByVal historical_status As String, ByVal historical_by As String, ByVal pc_request_dt As DateTime) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim pc_history As New TPROC_PC_HISTORICAL
                pc_history.PC_ID = pc_id
                pc_history.HISTORICAL_DT = historical_dt
                pc_history.HISTORICAL_STATUS = historical_status
                pc_history.HISTORICAL_BY = historical_by
                pc_history.CREATED_TIME = Date.Now
                pc_history.CREATED_BY = CurrentUser.GetCurrentUserId()

                Dim dt As DateTime
                If historical_status = ListEnum.PRStatus.Submitted.ToString() Then
                    dt = pc_request_dt
                ElseIf historical_status = ListEnum.PriceCom.ApprovedByAcknowledge.ToString() Then
                    dt = GetHistoricalDate(pc_id, ListEnum.PriceCom.Submitted.ToString())
                ElseIf historical_status = ListEnum.PriceCom.Approved.ToString() Then
                    dt = GetHistoricalDate(pc_id, ListEnum.PriceCom.ApprovedByAcknowledge.ToString())
                ElseIf historical_status = ListEnum.PriceCom.Completed.ToString() Then
                    dt = GetHistoricalDate(pc_id, ListEnum.PriceCom.Approved.ToString())
                Else
                    dt = Now.Date()
                End If

                pc_history.QUEUE = (historical_dt.Subtract(dt)).Days

                db.TPROC_PC_HISTORICAL.Add(pc_history)
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Private Function GetHistoricalDate(ByVal pc_id As Decimal, ByVal historical_status As String) As Date
        Dim dt As DateTime

        Using db As New eProcurementEntities
            If historical_status = ListEnum.PriceCom.ApprovedByAcknowledge.ToString() Then
                Dim submitted = ListEnum.PriceCom.Submitted.ToString()
                dt = db.TPROC_PC_HISTORICAL.Where(Function(x) x.PC_ID = pc_id And (x.HISTORICAL_STATUS = historical_status Or x.HISTORICAL_STATUS = submitted)).FirstOrDefault().HISTORICAL_DT
            Else
                dt = db.TPROC_PC_HISTORICAL.Where(Function(x) x.PC_ID = pc_id And x.HISTORICAL_STATUS = historical_status).FirstOrDefault().HISTORICAL_DT
            End If
        End Using

        Return dt
    End Function

    Function InsertVerifyPcByApprovalRole(pc_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim facadePo As New PurchaseOrderFacade
        Dim db As New eProcurementEntities

        Using scope As New TransactionScope()
            Try

                'Dim appr_role = db.TPROC_APPROVAL_ROLE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live And x.AS_IS = "Verifier" And x.ROLE_NAME = ListEnum.ApprovalRole.PC).ToList()
                Dim vrf_role As List(Of APPROVAL_ROLE) = facadePo.GetVerifyRoleFor("PC")

                For Each item In vrf_role
                    Dim vrf_pc As New TPROC_VRF_PC
                    vrf_pc.PC_ID = pc_id
                    vrf_pc.USER_ID = item.USER_ID
                    vrf_pc.NAME = item.NAME
                    vrf_pc.STATUS = "Waiting for verify"
                    vrf_pc.AS_IS = "Verifier"
                    vrf_pc.EMAIL = item.EMAIL
                    vrf_pc.CREATED_TIME = Date.Now
                    vrf_pc.CREATED_BY = CurrentUser.GetCurrentUserId()
                    Using db2 As New eProcurementEntities
                        db2.TPROC_VRF_PC.Add(vrf_pc)
                        db2.SaveChanges()
                        rs.SetSuccessStatus()
                    End Using
                Next

                If rs.IsSuccess Then
                    rs = SendEmailVrfPC(pc_id)
                End If

                If rs.IsSuccess Then
                    scope.Complete()
                    rs.SetSuccessStatus()
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Function InsertApprovalPcByApprovalRole(pc_id As Decimal, total_price As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim facadePo As New PurchaseOrderFacade
        Dim db As New eProcurementEntities

        Using scope As New TransactionScope()
            Try

                'Dim appr_role = db.TPROC_APPROVAL_ROLE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live And x.AS_IS = "Approver" And x.ROLE_NAME = ListEnum.ApprovalRole.PC).ToList()
                Dim appr_role As List(Of APPROVAL_ROLE) = facadePo.GetVerifyRoleFor("PC")

                For Each item In appr_role
                    Dim appr_pc As New TPROC_APPR_PC
                    appr_pc.PC_ID = pc_id
                    appr_pc.USER_ID = item.USER_ID
                    appr_pc.NAME = item.NAME
                    appr_pc.STATUS = CommonFunction.GetLimitForStatus(item.RUPIAH_LIMIT, total_price)
                    appr_pc.AS_IS = CommonFunction.GetLimitForAsIs(item.RUPIAH_LIMIT, total_price)
                    appr_pc.EMAIL = item.EMAIL
                    appr_pc.CREATED_TIME = Date.Now
                    appr_pc.CREATED_BY = CurrentUser.GetCurrentUserId()
                    Using db2 As New eProcurementEntities
                        db2.TPROC_APPR_PC.Add(appr_pc)
                        db2.SaveChanges()
                        rs.SetSuccessStatus()
                    End Using
                Next

                If rs.IsSuccess Then
                    scope.Complete()
                    rs.SetSuccessStatus()
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Function InsertVerifyRoleForPC(pc_id As Decimal, user_id As String) As ResultStatus
        Dim odal As New DalOracle
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim rs As New ResultStatus
        Dim result As Integer

        carr.Add(New cArrayList("pc_id", pc_id))
        carr.Add(New cArrayList("created_by", user_id))
        odal.fn_AddRecordSP("SP_INS_VERIFY_ROLE_TO_PO", carr, result)

        If result = 1 Then
            rs.Status = True
        End If

        odr.Close()
        carr.Clear()

        Return rs
    End Function

    Public Function InsertApprovalRoleForPC(pc_id As Decimal, user_id As String) As ResultStatus
        Dim odal As New DalOracle
        Dim odr As OracleDataReader = Nothing
        Dim carr As New ArrayList
        Dim rs As New ResultStatus
        Dim result As Integer

        carr.Add(New cArrayList("pc_id", pc_id))
        carr.Add(New cArrayList("created_by", user_id))
        odal.fn_AddRecordSP("SP_INS_VERIFY_ROLE_TO_PO", carr, result)

        If result = 1 Then
            rs.Status = True
        End If

        odr.Close()
        carr.Clear()

        Return rs
    End Function

End Class
