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

Public Class PurchasingRequestFacade
#Region "DalOracle"
    Dim odal As New DalOracle
    Dim odr As OracleDataReader = Nothing
#End Region

#Region "DalSQL"
    Dim sqldal As New DalSQL
    Dim sqldr As SqlDataReader = Nothing
#End Region

    Dim carr As New ArrayList
    Dim ds As New DataSet

#Region "CREATE NEW PR"
    Public Function InsertPRHeader(ByVal pr_header As TPROC_PR_HEADER, ByRef pr_header_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        pr_header.CREATED_TIME = Date.Now
        pr_header.CREATED_BY = CurrentUser.GetCurrentUserId()

        Try
            Using db As New eProcurementEntities
                db.TPROC_PR_HEADER.Add(pr_header)
                db.SaveChanges()
                pr_header_id = db.TPROC_PR_HEADER.Where(Function(x) x.PR_NO = pr_header.PR_NO).FirstOrDefault().ID
                rs.SetSuccessStatus()
            End Using

            If rs.IsSuccess Then
                rs = InsertApprReldeptGr(pr_header_id, pr_header.SUB_TYPE_ID, pr_header.SUB_TOTAL)
            End If

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function InsertPRHistorical(ByVal pr_header_id As Decimal, ByVal historical_dt As DateTime, ByVal historical_status As String, ByVal historical_by As String, ByVal pr_request_dt As DateTime) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim pr_history As New TPROC_PR_HISTORICAL
                pr_history.PR_HEADER_ID = pr_header_id
                pr_history.HISTORICAL_DT = historical_dt
                pr_history.HISTORICAL_STATUS = historical_status
                pr_history.HISTORICAL_BY = historical_by
                pr_history.CREATED_TIME = Date.Now
                pr_history.CREATED_BY = CurrentUser.GetCurrentUserId()

                Dim dt As DateTime
                If historical_status = ListEnum.PRStatus.Submitted.ToString() Then
                    dt = pr_request_dt
                ElseIf historical_status = ListEnum.PRStatus.PrHandled.ToString() Then
                    dt = GetHistoricalDate(pr_header_id, ListEnum.PRStatus.PrApprovedComplete.ToString())
                ElseIf historical_status = ListEnum.PRStatus.Complete.ToString() Then
                    dt = GetHistoricalDate(pr_header_id, ListEnum.PRStatus.PrHandled.ToString())
                ElseIf historical_status = ListEnum.PRStatus.SignOff.ToString() Then
                    dt = GetHistoricalDate(pr_header_id, ListEnum.PRStatus.Complete.ToString())
                Else
                    dt = Now.Date()
                End If

                pr_history.QUEUE = (historical_dt.Subtract(dt)).Days

                db.TPROC_PR_HISTORICAL.Add(pr_history)
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function GetHistoricalDate(ByVal pr_header_id As Decimal, ByVal historical_status As String) As Date
        Dim dt As DateTime

        Using db As New eProcurementEntities
            dt = db.TPROC_PR_HISTORICAL.Where(Function(x) x.PR_HEADER_ID = pr_header_id And x.HISTORICAL_STATUS = historical_status).FirstOrDefault().HISTORICAL_DT
        End Using

        Return dt
    End Function
    Public Function CheckBudgetEachItem(ByVal l_pr_dt As String(), ByVal form_type As String, ByVal count_code As String, ByVal count_code_start As String, ByVal count_code_end As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim budget As TPROC_BUDGET_CODE
        'Dim form_sub_type As TPROC_FORM_SUB_TYPE
        Dim map_budg_wa As New List(Of MAPPING_BUDGET_WA)

        Try
            Dim db As New eProcurementEntities
            'get the property of budget from table budget code
            budget = db.TPROC_BUDGET_CODE.Where(Function(x) x.IS_ACTIVE = 1).FirstOrDefault()
            'form_sub_type = db.TPROC_FORM_SUB_TYPE.Find(form_sub_type_id)

            For Each item As String In l_pr_dt
                Dim arry = item.Split("|")

                If arry(12) <> 199 Then 'Check budget only wa not 199
                    Dim wa_name = arry(11) 'wa_name
                    Dim total_price_item = arry(7) 'total_price_item
                    Dim convertion_price_item = arry(14) 'convertion_price_item
                    Dim total_price_item_after_convertion = Convert.ToDecimal(total_price_item)
                    Dim query_and_wa = " AND ANAL_T4 = " + wa_name

                    'get budged_alloc_rp
                    Dim query_budget_alloc_statis = "select coalesce(sum(AMOUNT)*-1,0) from " + budget.TABLE_BUDGET + " AS budget_alloc_rp"
                    Dim query_budget_alloc_where As String = ""

                    If form_type.ToUpper().Replace(" ", "") = "PRINTING" Or form_type.ToUpper().Replace(" ", "") = "OFFICESUPPLIES" Then
                        query_budget_alloc_where = " WHERE ACCNT_CODE >= " + count_code_start + " AND ACCNT_CODE<= " + count_code_end
                    ElseIf form_type.ToUpper().Replace(" ", "") = "ASSET" Or form_type.ToUpper().Replace(" ", "") = "NONASSET" Or form_type.ToUpper().Replace(" ", "") = "PROMOTIONALITEM" Then
                        query_budget_alloc_where = " WHERE ACCNT_CODE = " + count_code
                    Else
                        query_budget_alloc_where = " WHERE ACCNT_CODE = " + count_code
                    End If

                    Dim budget_alloc_rp As Decimal

                    budget_alloc_rp = GetBudgetAllocRp(query_budget_alloc_statis + query_budget_alloc_where + query_and_wa)


                    'get budget_usage_rp
                    Dim query_budget_usage_statis = "select coalesce(sum(AMOUNT)*-1,0) from " + budget.TABLE_BUDGET_USAGE + " AS budget_usage_rp"
                    Dim query_budget_usage_where As String = ""

                    If form_type.ToUpper().Replace(" ", "") = "PRINTING" Or form_type.ToUpper().Replace(" ", "") = "OFFICESUPPLIES" Then
                        query_budget_usage_where = " WHERE ACCNT_CODE = " + count_code
                    ElseIf form_type.ToUpper().Replace(" ", "") = "ASSET" Or form_type.ToUpper().Replace(" ", "") = "NONASSET" Or form_type.ToUpper().Replace(" ", "") = "PROMOTIONALITEM" Then
                        query_budget_usage_where = " WHERE ACCNT_CODE >= " + count_code_start + " AND ACCNT_CODE<= " + count_code_end
                    Else
                        query_budget_usage_where = " WHERE ACCNT_CODE >= " + count_code_start + " AND ACCNT_CODE<= " + count_code_end
                    End If

                    Dim budget_usage_rp = GetBudgetAllocRp(query_budget_usage_statis + query_budget_usage_where + query_and_wa)

                    'Mapping budge_wa
                    Dim budg_wa As New MAPPING_BUDGET_WA
                    budg_wa.WA_NAME = wa_name
                    budg_wa.BUDGET_ALLOC_RP = budget_alloc_rp
                    budg_wa.BUDGET_USAGE_RP = budget_usage_rp
                    budg_wa.SUB_TOTAL_WA_IN_PR = total_price_item_after_convertion

                    If map_budg_wa.Count = 0 Then
                        map_budg_wa.Add(budg_wa)
                    Else
                        Dim isNewItemPass As Boolean = True
                        For Each map In map_budg_wa
                            If map.WA_NAME = wa_name Then
                                isNewItemPass = False
                                map.SUB_TOTAL_WA_IN_PR = map.SUB_TOTAL_WA_IN_PR + total_price_item
                                Exit For
                            End If
                        Next

                        If isNewItemPass Then
                            map_budg_wa.Add(budg_wa)
                        End If
                    End If
                End If
            Next

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
            Return rs
        End Try

        Dim sb As New StringBuilder
        For Each map_r In map_budg_wa
            Dim sisa_budget = map_r.BUDGET_ALLOC_RP - map_r.BUDGET_USAGE_RP
            If sisa_budget < map_r.SUB_TOTAL_WA_IN_PR Then
                sb.Append(Labels.IconWidget("Edit") + "Item for WA " + map_r.WA_NAME + " : Budget Alloc => Rp. 0 " + map_r.BUDGET_ALLOC_RP.ToString("###,###") + "     Budget Usage => Rp. 0 " + map_r.BUDGET_USAGE_RP.ToString("###,###") + "      Total Request In This PR => Rp. " + map_r.SUB_TOTAL_WA_IN_PR.ToString("###,###") + ". Please contact accounting budget since your budget is unsufficient." + "<br />")
            End If
        Next

        If sb.Length > 0 Then
            rs.SetErrorStatus(sb.ToString())
        Else
            rs.SetSuccessStatus()
        End If

        Return rs
    End Function
    Public Function GetBudgetAllocRp(query As String) As Decimal
        Dim result As Decimal
        sqldal.fn_GetSQLDataReaderByQuery(query, sqldr, "SunConn")

        If sqldr.HasRows Then
            While sqldr.Read
                result = Convert.ToDecimal(sqldr.GetValue(0))
            End While
        End If

        Return result
    End Function
    Public Function GetBudgeUsagetRp(query As String) As Integer
        Dim result As Integer
        sqldal.fn_GetSQLDataReaderByQuery(query, sqldr, "SunConn")

        If sqldr.HasRows Then
            While sqldr.Read
                result = Convert.ToInt32(sqldr.GetValue(0))
            End While
        End If

        Return result
    End Function
    Public Function InsertPRDetail(ByVal l_pr_dt As String(), ByVal pr_header_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            For Each item As String In l_pr_dt
                Dim arry = item.Split("|")
                Dim pr_dt As New TPROC_PR_DETAIL
                Dim pr_dt_new As New List(Of TPROC_PR_DETAIL)
                Dim pr_detail_id As Integer

                pr_dt.PR_HEADER_ID = pr_header_id
                pr_dt.ITEM_NAME = Trim(arry(0))
                pr_dt.SPECIFICATION = arry(1)
                pr_dt.CURRENCY = arry(2)
                pr_dt.QTY = arry(3)
                pr_dt.USER_MEASUREMENT = arry(4)
                pr_dt.REVISED_QTY = Nothing
                pr_dt.PRICE = arry(6)
                pr_dt.TOTAL_PRICE = arry(7)
                pr_dt.REMARK = arry(8)
                pr_dt.SUPPLIER_NAME = arry(9)
                pr_dt.WA_ID = arry(10)
                pr_dt.ITEM_CD = arry(13)
                pr_dt.CONVERSION_RP = arry(14)
                If arry(15) <> "" Then
                    pr_dt.ITEM_ID = arry(15)
                End If
                pr_dt.PR_DETAIL_STATUS = ListEnum.ItemStatus.Submitted
                pr_dt.WA_NUMBER_ACC = arry(12)
                pr_dt.WA_NUMBER = arry(11)
                pr_dt.CREATED_BY = CurrentUser.GetCurrentUserId()
                pr_dt.CREATED_TIME = Date.Now
                Using db As New eProcurementEntities
                    db.TPROC_PR_DETAIL.Add(pr_dt)
                    db.SaveChanges()
                    pr_dt_new = db.TPROC_PR_DETAIL.Where(Function(x) x.PR_HEADER_ID = pr_header_id).ToList()
                    pr_detail_id = (From d In pr_dt_new Select d.ID).Max()
                    rs.SetSuccessStatus()
                End Using

                If rs.IsSuccess Then
                    'rs = InsertApprWA(pr_detail_id, arry(10), arry(7)) 'not used anymore
                    rs = InsertApprWAChooseOneApproval(pr_detail_id, arry(16))
                End If
            Next
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function InsertApprWAChooseOneApproval(ByRef pr_detail_id As Decimal, ByVal one_approval As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim sb As New StringBuilder

        Try
            Dim appr_wa As New TPROC_PR_APPR_WA
            Dim arry_r = one_approval.Split("-")

            Dim status = "Waiting for approve"
            appr_wa.PR_DETAIL_ID = pr_detail_id
            appr_wa.USER_ID = arry_r(0)
            appr_wa.NAME = arry_r(1)
            appr_wa.APPR_WA_STATUS = status
            appr_wa.EMAIL = arry_r(2)
            appr_wa.CREATED_TIME = Date.Now
            appr_wa.CREATED_BY = CurrentUser.GetCurrentUserId()

            db.TPROC_PR_APPR_WA.Add(appr_wa)
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function InsertApprWA(ByRef pr_detail_id As Decimal, ByVal wa_id As Decimal, ByVal total_price As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim sb As New StringBuilder

        Dim wa = db.TPROC_WA.Find(wa_id)
        Dim appr_gr_id As Integer = wa.TPROC_APPROVAL_GR.ID
        Dim l_appr_gr_dt As New List(Of TPROC_APPROVAL_DT)
        l_appr_gr_dt = db.TPROC_APPROVAL_DT.Where(Function(x) x.APPROVAL_GROUP_ID = appr_gr_id).ToList()

        Try
            For Each item As TPROC_APPROVAL_DT In l_appr_gr_dt
                Dim appr_wa As New TPROC_PR_APPR_WA
                Dim status = CommonFunction.GetLimitForStatus(item.TPROC_LEVEL.RUPIAH_LIMIT, total_price)
                appr_wa.PR_DETAIL_ID = pr_detail_id
                appr_wa.USER_ID = item.APPROVAL_NAME
                appr_wa.NAME = item.USER_NAME
                appr_wa.APPR_WA_STATUS = status
                appr_wa.EMAIL = item.EMAIL
                appr_wa.CREATED_TIME = Date.Now
                appr_wa.CREATED_BY = CurrentUser.GetCurrentUserId()
                Using db2 As New eProcurementEntities
                    db2.TPROC_PR_APPR_WA.Add(appr_wa)
                    db2.SaveChanges()
                End Using

                If status = "Waiting for approve" Then
                    sb.Append(status)
                End If
            Next

            If sb.Length = 0 Then
                rs.SetErrorStatus("Limit approver for " + wa.WA_NUMBER.ToString() + " is unsufficient, please contact your supervisor to add another approver who has bigger limit.")
            Else
                rs.SetSuccessStatus()
            End If

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function InsertApprReldeptGr(ByVal pr_header_id As Decimal, ByVal sub_type_id As Decimal, ByVal sub_total As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Dim sub_type_dt As New List(Of TPROC_FORM_SUBTYPE_DT)
        sub_type_dt = db.TPROC_FORM_SUB_TYPE.Find(sub_type_id).TPROC_FORM_SUBTYPE_GR.TPROC_FORM_SUBTYPE_DT.ToList()

        Try
            If sub_type_dt.Count > 0 Then
                For Each item As TPROC_FORM_SUBTYPE_DT In sub_type_dt
                    Dim pr_appr_rd_gr As New TPROC_PR_APPR_RELDEPT_GR
                    Dim pr_apr_rd_gr_new As New List(Of TPROC_PR_APPR_RELDEPT_GR)
                    Dim pr_appr_rd_gr_id As Integer

                    pr_appr_rd_gr.PR_HEADER_ID = pr_header_id
                    pr_appr_rd_gr.RELDEPT_NAME = item.TPROC_REL_DEPT.RELATED_DEPARTMENT_NAME
                    pr_appr_rd_gr.CREATED_TIME = Date.Now
                    pr_appr_rd_gr.CREATED_BY = CurrentUser.GetCurrentUserId()

                    Using db2 As New eProcurementEntities
                        db2.TPROC_PR_APPR_RELDEPT_GR.Add(pr_appr_rd_gr)
                        db2.SaveChanges()

                        pr_apr_rd_gr_new = db.TPROC_PR_APPR_RELDEPT_GR.Where(Function(x) x.PR_HEADER_ID = pr_header_id).ToList()
                        pr_appr_rd_gr_id = (From d In pr_apr_rd_gr_new Select d.ID).Max()
                        rs.SetSuccessStatus()
                    End Using

                    If rs.IsSuccess Then
                        rs = InsertApprReldeptDt(pr_appr_rd_gr_id, item.TPROC_REL_DEPT.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT.ToList, sub_total)
                    End If
                Next
            Else
                rs.SetSuccessStatus()
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function InsertApprReldeptDt(ByVal pr_appr_rd_gr_id As Decimal, list_appr_rd_dt As List(Of TPROC_APPR_RELDEPT_DT), ByVal sub_total As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            For Each item As TPROC_APPR_RELDEPT_DT In list_appr_rd_dt
                Dim pr_appr_rd_dt As New TPROC_PR_APPR_RELDEPT_DT
                pr_appr_rd_dt.PR_APPR_RELDEPT_GR_ID = pr_appr_rd_gr_id
                pr_appr_rd_dt.USER_ID = item.APPROVAL_NAME
                pr_appr_rd_dt.NAME = item.USER_NAME
                pr_appr_rd_dt.APPR_RELDEPT_STATUS = CommonFunction.GetLimitForStatus(item.TPROC_LEVEL.RUPIAH_LIMIT, sub_total)
                pr_appr_rd_dt.EMAIL = item.EMAIL
                pr_appr_rd_dt.CREATED_TIME = Date.Now
                pr_appr_rd_dt.CREATED_BY = CurrentUser.GetCurrentUserId()

                Using db As New eProcurementEntities
                    db.TPROC_PR_APPR_RELDEPT_DT.Add(pr_appr_rd_dt)
                    db.SaveChanges()
                End Using
            Next
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function GetUserNameByUserId(user_id As Decimal) As String
        Dim name As String = ""

        Using db As New eProcurementEntities
            name = db.TPROC_USER.Where(Function(x) x.USER_ID = user_id).FirstOrDefault().USER_NAME
        End Using

        Return name
    End Function
    Public Function SendEmailToUserCreatePR(header_id As Decimal, pr_no As String, pr_date As String, email_user As String, status_pr As String, form_type_name As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToUser As New ListFieldNameAndValue


        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + header_id.ToString() + "?flag=0")
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")
        emailToUser.AddItem("Email", email_user)

        result = mailFacade.SendEmail(emailSender, emailToUser, Nothing, String.Format(EmailTemplate.eProcSubjectEmailUserCreatePR, pr_no, form_type_name, status_pr),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotificationCreatePR, pr_no, link, pr_no, pr_date),
                                            Nothing)

        Return result
    End Function
    Public Function SendEmailApprRevWA(pr_header_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities
        Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=1")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        '<<zoer20171010 add form type in subject
        Dim form_type = db.TPROC_FORM_TYPE.Find(pr_header.FORM_TYPE_ID)
        Dim formTypeName As String = form_type.FORM_TYPE_NAME
        '>>zoer20171010

        'get all wa Approval email
        'Dim pr_dt = pr_header.TPROC_PR_DETAIL.ToList()
        'For Each item_dt In pr_dt
        '    Dim emailToReviewAppr As New ListFieldNameAndValue
        '    Dim emailToApproveAppr As New ListFieldNameAndValue
        '    Dim item_name As String = item_dt.ITEM_NAME
        '    For Each item_wa In item_dt.TPROC_PR_APPR_WA
        '        If item_wa.APPR_WA_STATUS = "Waiting for review" Then
        '            emailToReviewAppr.AddItem("Email", item_wa.EMAIL)
        '        Else
        '            emailToApproveAppr.AddItem("Email", item_wa.EMAIL)
        '        End If
        '    Next
        '    If emailToReviewAppr.Count > 0 Then
        '        result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, item_name, ListEnum.eProcApprAction.review.ToString()),
        '                                    String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR, item_name),
        '                                        sendEmail2)
        '    Else
        '        result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, item_name, ListEnum.eProcApprAction.approve.ToString()),
        '                                    String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR, item_name),
        '                                        sendEmail2)
        '    End If
        'Next

        Dim emailToReviewAppr As New ListFieldNameAndValue
        Dim emailToApproveAppr As New ListFieldNameAndValue
        Dim pr_dt = pr_header.TPROC_PR_DETAIL.ToList()
        For Each item_dt In pr_dt
            Dim item_name As String = item_dt.ITEM_NAME
            For Each item_wa In item_dt.TPROC_PR_APPR_WA
                If item_wa.APPR_WA_STATUS = "Waiting for review" Then
                    emailToReviewAppr.AddItem("Email", item_wa.EMAIL)
                Else
                    emailToApproveAppr.AddItem("Email", item_wa.EMAIL)
                End If
            Next
        Next

        If emailToReviewAppr.Count > 0 Then
            result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, formTypeName, ListEnum.eProcApprAction.review.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR),
                                                sendEmail2)
        Else
            result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, formTypeName, ListEnum.eProcApprAction.approve.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR),
                                                sendEmail2)
        End If

        If result.IsSuccess Then
            result = SendEmailToUserCreatePR(pr_header_id, reqNumber, reqDate, pr_header.TPROC_USER.USER_MAIL, ListEnum.PRStatus.Submitted.ToString(), formTypeName)
        End If


        Return result
    End Function

    Public Sub GetDetailBc(ByVal fst_gr_id As Decimal, ByVal bc As String, ByRef bcs As String, ByRef bce As String)
        Dim db As New eProcurementEntities

        Dim fst_gr = db.TPROC_FORM_SUBTYPE_GR.Find(fst_gr_id)
        If fst_gr IsNot Nothing Then
            If fst_gr.BUDGET_CODE = bc Then
                bcs = fst_gr.ACCOUNT_CODE_START
                bce = fst_gr.ACCOUNT_CODE_END
            Else
                Dim fst_bc = db.TPROC_FST_BUDGET_CD_ADD.Where(Function(x) x.FORM_SUBTYPE_GR_ID = fst_gr_id And x.BUDGET_CODE = bc).FirstOrDefault
                If fst_bc IsNot Nothing Then
                    bcs = fst_bc.ACCOUNT_CODE_START
                    bce = fst_bc.ACCOUNT_CODE_END
                End If
            End If
        End If

    End Sub

#End Region

#Region "ITEM/WA"
    Public Function UpdateStatusApprPRWa(pr_header As TPROC_PR_HEADER, status As String, old_status As String, pr_detail_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Dim user_id = CurrentUser.GetCurrentUserId()

        Try
            Dim appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = pr_detail_id And x.USER_ID.ToUpper() = user_id.ToUpper() And x.APPR_WA_STATUS = old_status).FirstOrDefault()

            appr_wa.APPR_WA_STATUS = status
            appr_wa.LAST_MODIFIED_TIME = Date.Now
            appr_wa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(appr_wa).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

            If rs.IsSuccess Then
                'send Email to user
                rs = SendEmailToUser_WA(pr_header, status, appr_wa.PR_DETAIL_ID)
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function UpdateStatusApprPRWaApprovalReviewSelected(pr_header As TPROC_PR_HEADER, ByVal ids_dt_pr As Integer()) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            Dim count_ids_dt_pr = ids_dt_pr.Count()
            For Each item_id_dt_pr In ids_dt_pr
                rs = UpdateApprWaApproveOrReview(pr_header, item_id_dt_pr, count_ids_dt_pr)
            Next

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function UpdateApprWaApproveOrReview(pr_header As TPROC_PR_HEADER, dt_pr_id As Decimal, count_dt_pr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim status_history As String = ""

        Dim user_id = CurrentUser.GetCurrentUserId()
        Dim user_name = CurrentUser.GetCurrentUserName()

        Try
            If IsPrApprWaOustandingSelected(dt_pr_id) = False Then
                rs.SetSuccessStatus("Record has been change status by other")
                Return rs
            End If

            Dim appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = dt_pr_id And x.USER_ID.ToUpper() = user_id.ToUpper()).FirstOrDefault()

            rs.SetSuccessStatus()

            If appr_wa IsNot Nothing Then
                If appr_wa.APPR_WA_STATUS = "Waiting for approve" Then
                    appr_wa.APPR_WA_STATUS = "Approved"
                    status_history = ListEnum.ApprItemStatus.ApproveWA.ToString()
                ElseIf appr_wa.APPR_WA_STATUS = "Waiting for review" Then
                    appr_wa.APPR_WA_STATUS = "Reviewed"
                    status_history = ListEnum.ApprItemStatus.ReviewWA.ToString()
                End If

                appr_wa.LAST_MODIFIED_TIME = Date.Now
                appr_wa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(appr_wa).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()

                If rs.IsSuccess And appr_wa.APPR_WA_STATUS = "Approved" Then
                    rs = UpdateStatusDetailItemToCompleteSelected(dt_pr_id)
                ElseIf rs.IsSuccess And appr_wa.APPR_WA_STATUS = "Reviewed" Then
                    rs = UpdateStatusDetailItemToReadyApprove(dt_pr_id)
                End If

                If rs.IsSuccess And count_dt_pr_id = 1 Then
                    rs = InsertPRHistorical(pr_header.ID, Date.Now, status_history, user_name, pr_header.PR_DATE)
                End If

                If rs.IsSuccess Then
                    'send Email to user
                    rs = SendEmailToUser_WA(pr_header, appr_wa.APPR_WA_STATUS, appr_wa.PR_DETAIL_ID)
                End If
            End If

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function IsPrApprWaOustanding(pr_detail_id As Decimal) As Boolean
        Dim db As New eProcurementEntities
        Dim result As Boolean = True
        Dim appr_wa As New List(Of TPROC_PR_APPR_WA)
        appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = pr_detail_id).ToList()

        For Each item In appr_wa
            If item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Approved.ToString() Or item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Rejected.ToString() Then
                result = False
                Exit For
            Else
                result = True
            End If
        Next

        Return result
    End Function

    Public Function IsPrApprWaOustandingSelected(appr_wa_id As Decimal) As Boolean
        Dim db As New eProcurementEntities
        Dim result As Boolean = True
        Dim appr_wa As New List(Of TPROC_PR_APPR_WA)
        Dim user_id = CurrentUser.GetCurrentUserId()

        appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = appr_wa_id).ToList()

        For Each item In appr_wa
            If item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Approved.ToString() Or item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Rejected.ToString() Then
                result = False
                Exit For
            ElseIf item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Reviewed.ToString() And item.USER_ID = user_id Then
                result = False
                Exit For
            Else
                result = True
            End If
        Next

        Return result
    End Function


    Public Function UpdateStatusApprPRWaSelectedReject(pr_header As TPROC_PR_HEADER, ByVal ids_dt_pr As Integer(), reject_reason As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            For Each item_id_dt_pr In ids_dt_pr
                rs = UpdateApprWaReject(pr_header, item_id_dt_pr, reject_reason)
            Next

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
    Public Function UpdateApprWaReject(pr_header As TPROC_PR_HEADER, dt_pr_id As Decimal, reject_reason As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Dim user_id = CurrentUser.GetCurrentUserId()

        Try
            If IsPrApprWaOustandingSelected(dt_pr_id) = False Then
                rs.SetSuccessStatus("Record has been change status by other")
                Return rs
            End If

            Dim appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = dt_pr_id And x.USER_ID.ToUpper() = user_id.ToUpper()).FirstOrDefault()

            appr_wa.APPR_WA_STATUS = "Rejected"

            appr_wa.LAST_MODIFIED_TIME = Date.Now
            appr_wa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(appr_wa).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

            If rs.IsSuccess Then
                Dim sub_total_new As Decimal
                rs = UpdateStatusDetailItemToRejectSelected(dt_pr_id, reject_reason, sub_total_new)
                If rs.IsSuccess Then
                    rs = UpdateSubTotalHeader(sub_total_new, pr_header.ID)
                End If
            End If

            If rs.IsSuccess Then
                'send Email to user
                rs = SendEmailToUser_WA(pr_header, appr_wa.APPR_WA_STATUS, appr_wa.PR_DETAIL_ID)
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function




    Public Function UpdateStatusDetailItemToReadyApprove(pr_detail_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim is_all_reviewed As Boolean = False

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            'get the detail
            Dim pr_detail = db.TPROC_PR_DETAIL.Find(pr_detail_id)

            For Each appr_wa In pr_detail.TPROC_PR_APPR_WA
                If appr_wa.APPR_WA_STATUS = "Waiting for review" Then
                    is_all_reviewed = False
                    Exit For
                Else
                    is_all_reviewed = True
                End If
            Next

            If is_all_reviewed Then
                pr_detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.ReadyToApprove
                pr_detail.LAST_MODIFIED_TIME = Date.Now
                pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(pr_detail).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusDetailItemToComplete(pr_detail_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            'get the detail
            Dim pr_detail = db.TPROC_PR_DETAIL.Find(pr_detail_id)

            pr_detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete
            pr_detail.LAST_MODIFIED_TIME = Date.Now
            pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(pr_detail).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusDetailItemToCompleteSelected(pr_dt_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            'get the detail
            Dim pr_detail = db.TPROC_PR_DETAIL.Find(pr_dt_id)

            pr_detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete
            pr_detail.LAST_MODIFIED_TIME = Date.Now
            pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(pr_detail).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusDetailItemToReject(reject_reason As String, ByRef sub_total_new As Decimal, pr_detail_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            'get the detail
            Dim pr_detail = db.TPROC_PR_DETAIL.Find(pr_detail_id)

            pr_detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Rejected
            pr_detail.LAST_MODIFIED_TIME = Date.Now
            pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            pr_detail.REJECT_REASON = reject_reason

            sub_total_new = pr_detail.TOTAL_PRICE

            db.Entry(pr_detail).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateStatusDetailItemToRejectSelected(dt_pr_id As Decimal, reject_reason As String, ByRef sub_total_new As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            'get the detail
            Dim pr_detail = db.TPROC_PR_DETAIL.Find(dt_pr_id)

            pr_detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Rejected
            pr_detail.LAST_MODIFIED_TIME = Date.Now
            pr_detail.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            pr_detail.REJECT_REASON = reject_reason

            sub_total_new = pr_detail.TOTAL_PRICE

            db.Entry(pr_detail).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function UpdateSubTotalHeader(total_price As Decimal, pr_header_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            'get the detail
            Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

            Dim sub_total_new = pr_header.SUB_TOTAL - total_price

            pr_header.SUB_TOTAL = sub_total_new
            pr_header.LAST_MODIFIED_TIME = Date.Now
            pr_header.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(pr_header).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function SendEmailToUser_WA(pr_header As TPROC_PR_HEADER, status_item As String, pr_dtail_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToUser As New ListFieldNameAndValue
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim pr_detail = db.TPROC_PR_DETAIL.Find(pr_dtail_id)

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=1")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        emailToUser.AddItem("Email", pr_header.TPROC_USER.USER_MAIL)

        result = mailFacade.SendEmail(emailSender, emailToUser, Nothing, String.Format(EmailTemplate.eProcSubjectEmailUser_WA, reqNumber, pr_detail.ITEM_NAME, status_item),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotification_WA, status_item, link, reqNumber, reqDate, pr_detail.ITEM_NAME, status_item, CurrentUser.GetCurrentUserId()),
                                            Nothing)

        Return result
    End Function

    Public Function SendEmailByReviewerWAToApprWA(pr_header As TPROC_PR_HEADER, pr_detail_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToApproveAppr As New ListFieldNameAndValue
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities
        Dim is_all_reviewed As Boolean = False

        'set result is success  
        result.SetSuccessStatus()

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=1")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        'get the detail
        Dim pr_detail = pr_header.TPROC_PR_DETAIL.Where(Function(x) x.ID = pr_detail_id).FirstOrDefault()
        Dim item_name = pr_detail.ITEM_NAME

        For Each item_dt In pr_detail.TPROC_PR_APPR_WA
            If item_dt.APPR_WA_STATUS = "Waiting for review" Then
                is_all_reviewed = False
            ElseIf item_dt.APPR_WA_STATUS = "Waiting for approve" Then
                emailToApproveAppr.AddItem("Email", item_dt.EMAIL)
                is_all_reviewed = True
            End If
        Next

        If is_all_reviewed = True Then
            result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, item_name, ListEnum.eProcApprAction.approve.ToString()),
                                    String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR, item_name),
                                        sendEmail2)
        End If


        Return result
    End Function

    Public Function SendEmailToApprRevRD(pr_header As TPROC_PR_HEADER) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=2")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")


        '<<zoer20171010 add form type in subject
        'Dim form_type = db.TPROC_FORM_TYPE.Find(pr_header.FORM_TYPE_ID)
        'Dim formTypeName As String = form_type.FORM_TYPE_NAME
        Dim formTypeName As String = pr_header.TPROC_FORM_TYPE.FORM_TYPE_NAME
        '>>zoer20171010

        'get all wa Approval email
        For Each rd_gr In pr_header.TPROC_PR_APPR_RELDEPT_GR.ToList()
            Dim emailToReviewAppr As New ListFieldNameAndValue
            Dim emailToApproveAppr As New ListFieldNameAndValue
            For Each rd_dt In rd_gr.TPROC_PR_APPR_RELDEPT_DT
                If rd_dt.APPR_RELDEPT_STATUS = "Waiting for review" Then
                    emailToReviewAppr.AddItem("Email", rd_dt.EMAIL)
                Else
                    emailToApproveAppr.AddItem("Email", rd_dt.EMAIL)
                End If
            Next

            If emailToReviewAppr.Count > 0 Then
                result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_RD, reqNumber, formTypeName, ListEnum.eProcApprAction.review.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_RD, link, reqNumber, reqDate, reqBy, statusPR),
                                                sendEmail2)
            Else
                result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_RD, reqNumber, formTypeName, ListEnum.eProcApprAction.approve.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_RD, link, reqNumber, reqDate, reqBy, statusPR),
                                                sendEmail2)
            End If
        Next


        Return result
    End Function

    Public Function IsAllGRItemNotOutStanding(pr_header As TPROC_PR_HEADER) As Boolean
        Dim result As Boolean = True
        For Each item_gr In pr_header.TPROC_PR_DETAIL.ToList()
            If item_gr.PR_DETAIL_STATUS = ListEnum.ItemStatus.Submitted Or item_gr.PR_DETAIL_STATUS = ListEnum.ItemStatus.ReadyToApprove Then
                result = False
                Exit For
            End If
        Next

        Return result
    End Function

    Public Shared Function CheckIsAllGRItemNotOutStanding(pr_header As TPROC_PR_HEADER) As Boolean
        Dim result As Boolean = True
        For Each item_gr In pr_header.TPROC_PR_DETAIL.ToList()
            If item_gr.PR_DETAIL_STATUS = ListEnum.ItemStatus.Submitted Then
                result = False
                Exit For
            End If
        Next

        Return result
    End Function
    Public Function IsAllItemRejected(pr_header As TPROC_PR_HEADER) As Boolean
        Dim result As Boolean = True
        For Each item_gr In pr_header.TPROC_PR_DETAIL.ToList()
            If item_gr.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Rejected Then
                result = False
                Exit For
            End If
        Next

        Return result
    End Function
#End Region

#Region "RD"
    Public Function UpdateStatusApprPRRD(pr_header As TPROC_PR_HEADER, status As String, old_status As String, to_by As Integer, reject_reason As String) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Dim lgr_rd = pr_header.TPROC_PR_APPR_RELDEPT_GR.ToList()
            Dim dt_rd_id As Integer
            Dim lpr_dt As List(Of TPROC_PR_APPR_RELDEPT_DT)

            For Each item_gr In lgr_rd
                For Each item_dt In item_gr.TPROC_PR_APPR_RELDEPT_DT
                    If item_dt.USER_ID.ToUpper() = CurrentUser.GetCurrentUserId().ToUpper() And item_dt.APPR_RELDEPT_STATUS = old_status Then
                        dt_rd_id = item_dt.ID
                        Exit For
                    End If
                Next
            Next

            Dim db As New eProcurementEntities
            Dim dt_rd = db.TPROC_PR_APPR_RELDEPT_DT.Find(dt_rd_id)

            dt_rd.APPR_RELDEPT_STATUS = status
            dt_rd.LAST_MODIFIED_TIME = Date.Now
            dt_rd.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(dt_rd).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

            lpr_dt = db.TPROC_PR_APPR_RELDEPT_DT.Where(Function(x) x.PR_APPR_RELDEPT_GR_ID = dt_rd.PR_APPR_RELDEPT_GR_ID).ToList()

            Dim prStatus As String = ""
            If status = ListEnum.ApprRDStatus.Approved.ToString() Then
                prStatus = ListEnum.ApprRDStatus.Approved.ToString()
            ElseIf status = ListEnum.ApprRDStatus.Reviewed.ToString() Then
                prStatus = ListEnum.ApprRDStatus.Reviewed.ToString()
            Else
                prStatus = ListEnum.ApprRDStatus.Rejected.ToString()
            End If

            If rs.IsSuccess And to_by = ListEnum.RDStatus.Complete Then
                rs = UpdateStatusGRRDToComplete(pr_header, lpr_dt)
            End If

            If rs.IsSuccess And to_by = ListEnum.RDStatus.ReadyToApprove Then
                rs = UpdateStatusGRRDToReadyApprove(pr_header, lpr_dt)
            End If

            If rs.IsSuccess And to_by = ListEnum.RDStatus.Rejected Then
                rs = UpdateStatusGRRDToReject(pr_header, lpr_dt, reject_reason)
            End If

            If rs.IsSuccess Then
                'send Email to user
                rs = SendEmailToUser_RD(pr_header.ID, pr_header.PR_NO, pr_header.PR_DATE, pr_header.TPROC_USER.USER_MAIL, prStatus)
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function


#Region "RD BY REVIEWER"
    Public Function UpdateStatusRDByReviewer(pr_header As TPROC_PR_HEADER, status As String, old_status As String) As ResultStatus
        Dim rs As New ResultStatus

        Dim lpr_dt As New List(Of TPROC_PR_APPR_RELDEPT_DT)
        rs = UpdateStatusApprPRRD(pr_header, status, old_status, ListEnum.RDStatus.ReadyToApprove, Nothing)

        If rs.IsSuccess Then
            rs = SendEmailByReviewerRDToApprRD(pr_header, lpr_dt)
        End If

        Return rs
    End Function

    Public Function UpdateStatusGRRDToReadyApprove(pr_header As TPROC_PR_HEADER, lpr_dt As List(Of TPROC_PR_APPR_RELDEPT_DT)) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim is_all_reviewed As Boolean = False

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            For Each appr_dt In lpr_dt
                If appr_dt.APPR_RELDEPT_STATUS = "Waiting for review" Then
                    is_all_reviewed = False
                    Exit For
                Else
                    is_all_reviewed = True
                End If
            Next

            If is_all_reviewed Then
                Using db2 As New eProcurementEntities
                    Dim appr_rd_gr_new = db.TPROC_PR_APPR_RELDEPT_GR.Find(lpr_dt(0).PR_APPR_RELDEPT_GR_ID)

                    If appr_rd_gr_new.RELDEPT_GR_STATUS = ListEnum.RDStatus.ReadyToApprove Or pr_header.PR_STATUS = ListEnum.PRStatus.PrRejected Then
                        rs.SetErrorStatus("Data has been change by other")
                        Return rs
                    End If

                    appr_rd_gr_new.RELDEPT_GR_STATUS = ListEnum.RDStatus.ReadyToApprove
                    appr_rd_gr_new.LAST_MODIFIED_TIME = Date.Now
                    appr_rd_gr_new.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                    db.Entry(appr_rd_gr_new).State = EntityState.Modified
                    db.SaveChanges()
                    rs.SetSuccessStatus()
                End Using
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function SendEmailByReviewerRDToApprRD(pr_header As TPROC_PR_HEADER, lpr_dt As List(Of TPROC_PR_APPR_RELDEPT_DT)) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToApproveAppr As New ListFieldNameAndValue
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities
        Dim is_all_reviewed As Boolean = False

        'set result is success  
        result.SetSuccessStatus()

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=2")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")


        '<<zoer20171010 add form type in subject
        Dim form_type = db.TPROC_FORM_TYPE.Find(pr_header.FORM_TYPE_ID)
        Dim formTypeName As String = form_type.FORM_TYPE_NAME
        '>>zoer20171010

        'Dim rd_gr = pr_header.TPROC_PR_APPR_RELDEPT_GR.ToList
        'For Each item In rd_gr.Find(gr_id)
        For Each item_dt In lpr_dt
            If item_dt.APPR_RELDEPT_STATUS = "Waiting for review" Then
                is_all_reviewed = False
            ElseIf item_dt.APPR_RELDEPT_STATUS = "Waiting for approve" Then
                emailToApproveAppr.AddItem("Email", item_dt.EMAIL)
                is_all_reviewed = True
            End If
        Next

        If is_all_reviewed = True Then
            result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_RD, reqNumber, ListEnum.eProcApprAction.approve.ToString(),
                                                                                                  formTypeName 'zoer20171010
                                                                                                  ),
                                        String.Format(EmailTemplate.eProcEmailTemplatePRNotification_RD, link, reqNumber, reqDate, reqBy, statusPR),
                                            sendEmail2)
        End If
        'Next

        Return result
    End Function

#End Region

#Region "RD BY APPROVER"
    Public Function UpdateStatusRDByApprover(pr_header As TPROC_PR_HEADER, status As String, old_status As String) As ResultStatus
        Dim rs As New ResultStatus

        Dim lpr_dt As New List(Of TPROC_PR_APPR_RELDEPT_DT)
        rs = UpdateStatusApprPRRD(pr_header, status, old_status, ListEnum.RDStatus.Complete, Nothing)

        Return rs
    End Function

    Public Function UpdateStatusGRRDToComplete(pr_header As TPROC_PR_HEADER, lpr_dt As List(Of TPROC_PR_APPR_RELDEPT_DT)) As ResultStatus
        Dim rs As New ResultStatus
        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            Using db As New eProcurementEntities
                Dim gr_rd As New TPROC_PR_APPR_RELDEPT_GR
                gr_rd = db.TPROC_PR_APPR_RELDEPT_GR.Find(lpr_dt(0).PR_APPR_RELDEPT_GR_ID)

                If gr_rd.RELDEPT_GR_STATUS = ListEnum.RDStatus.Complete Or pr_header.PR_STATUS = ListEnum.PRStatus.PrRejected Then
                    rs.SetErrorStatus("Data has been change by other")
                    Return rs
                End If

                gr_rd.RELDEPT_GR_STATUS = ListEnum.RDStatus.Complete
                gr_rd.LAST_MODIFIED_TIME = Date.Now
                gr_rd.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(gr_rd).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function IsAllGRRDCompleted(pr_header As TPROC_PR_HEADER) As Boolean
        Dim db As New eProcurementEntities
        Dim is_all_completed As Boolean = True
        'set rs.success is true

        Try
            Dim appr_rd_gr = db.TPROC_PR_HEADER.Find(pr_header.ID).TPROC_PR_APPR_RELDEPT_GR.ToList()

            For Each item In appr_rd_gr
                If item.RELDEPT_GR_STATUS <> ListEnum.RDStatus.Complete Then
                    is_all_completed = False
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try

        Return is_all_completed
    End Function

    Public Function UpdateStatusPR(pr_header As TPROC_PR_HEADER, status_pr_new As Integer, reject_reason As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            Dim pr_header_new = db.TPROC_PR_HEADER.Find(pr_header.ID)

            If pr_header_new.PR_STATUS = status_pr_new Then
                rs.SetErrorStatus("Data has been change by other")
                Return rs
            End If

            pr_header_new.PR_STATUS = status_pr_new
            pr_header_new.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
            pr_header_new.LAST_MODIFIED_TIME = Date.Now
            pr_header_new.REJECT_REASON = reject_reason
            db.Entry(pr_header_new).State = EntityState.Modified
            db.SaveChanges()
            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Function SendEmailToEprocStaff(pr_header As TPROC_PR_HEADER, act_status_proc_staff As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToEprocStaff As New ListFieldNameAndValue
        Dim db As New eProcurementEntities

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=4")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        'get ALL procurement staff to handle
        Dim eproc_staff = db.TPROC_USER.Where(Function(x) x.TPROC_USER_DT.IS_EPROC_ADMIN = 1).ToList()


        '<<zoer20171010 add form type in subject
        'Dim form_type = db.TPROC_FORM_TYPE.Find(pr_header.FORM_TYPE_ID)
        'Dim formTypeName As String = form_type.FORM_TYPE_NAME
        Dim formTypeName As String = pr_header.TPROC_FORM_TYPE.FORM_TYPE_NAME
        '>>zoer20171010

        'get All email from staff procurement
        For Each item In eproc_staff
            emailToEprocStaff.AddItem("Email", item.USER_MAIL)
        Next


        If emailToEprocStaff.Count > 0 Then
            result = mailFacade.SendEmail(emailSender, emailToEprocStaff, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_Handle, reqNumber, formTypeName, act_status_proc_staff),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_Handle, link, reqNumber, reqDate, reqBy, statusPR),
                                                Nothing)
        End If

        Return result
    End Function
#End Region

#Region "RD BY REJECTER"
    Public Function UpdateStatusRDByRejecter(pr_header As TPROC_PR_HEADER, status As String, old_status As String, reject_reason As String) As ResultStatus
        Dim rs As New ResultStatus

        Dim lpr_dt As New List(Of TPROC_PR_APPR_RELDEPT_DT)
        rs = UpdateStatusApprPRRD(pr_header, status, old_status, ListEnum.RDStatus.Rejected, reject_reason)

        Return rs
    End Function

    Public Function UpdateStatusGRRDToReject(pr_header As TPROC_PR_HEADER, lpr_dt As List(Of TPROC_PR_APPR_RELDEPT_DT), reject_reason As String) As ResultStatus
        Dim rs As New ResultStatus

        'set rs.success is true
        rs.SetSuccessStatus()

        Try
            Using db As New eProcurementEntities
                Dim gr_rd As New TPROC_PR_APPR_RELDEPT_GR
                gr_rd = db.TPROC_PR_APPR_RELDEPT_GR.Find(lpr_dt(0).PR_APPR_RELDEPT_GR_ID)

                If gr_rd.RELDEPT_GR_STATUS = ListEnum.RDStatus.Rejected Or gr_rd.RELDEPT_GR_STATUS = ListEnum.RDStatus.Complete Or pr_header.PR_STATUS = ListEnum.PRStatus.PrApprovedComplete Then
                    rs.SetErrorStatus("Data has been change by other")
                    Return rs
                End If

                gr_rd.RELDEPT_GR_STATUS = ListEnum.RDStatus.Rejected
                gr_rd.LAST_MODIFIED_TIME = Date.Now
                gr_rd.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                gr_rd.REJECT_REASON = reject_reason

                db.Entry(gr_rd).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using


        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
#End Region

#Region "RD SEND EMAIL TO USER"
    Public Function SendEmailToUser_RD(header_id As Decimal, pr_no As String, pr_date As String, email_user As String, status_pr As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToUser As New ListFieldNameAndValue

        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + header_id.ToString() + "?flag=0")
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")
        emailToUser.AddItem("Email", email_user)

        result = mailFacade.SendEmail(emailSender, emailToUser, Nothing, String.Format(EmailTemplate.eProcSubjectEmailUser_RD, pr_no, status_pr),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotification_RD, status_pr, link, pr_no, pr_date, status_pr, CurrentUser.GetCurrentUserId()),
                                            Nothing)

        Return result
    End Function
#End Region

#Region "EPROC SEND EMAIL TO USER"
    Public Function SendEmailToUser_EPROC(header_id As Decimal, pr_no As String, pr_date As String, email_user As String, status_pr As String, reason As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToUser As New ListFieldNameAndValue

        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + header_id.ToString() + "?flag=0")
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")
        emailToUser.AddItem("Email", email_user)

        result = mailFacade.SendEmail(emailSender, emailToUser, Nothing, String.Format(EmailTemplate.eProcSubjectEmailUser_EPROC, pr_no, status_pr),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotification_EPROC, status_pr, link, pr_no, pr_date, status_pr, CurrentUser.GetCurrentUserId(), reason),
                                            Nothing)

        Return result
    End Function
#End Region

#End Region

#Region "SEND EMAIL COMPLETE TO USER"
    Public Function SendEmailToUserPRCompleted(header_id As Decimal, pr_no As String, pr_date As String, email_user As String, status_pr As String, form_type_name As String) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToUser As New ListFieldNameAndValue


        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + header_id.ToString() + "?flag=0")
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")
        emailToUser.AddItem("Email", email_user)

        result = mailFacade.SendEmail(emailSender, emailToUser, Nothing, String.Format(EmailTemplate.eProcSubjectEmailUserPRCompleted, pr_no, form_type_name, status_pr),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotificationPRCompleted, pr_no, link, pr_no, pr_date),
                                            Nothing)

        Return result
    End Function

#End Region

#Region "OTHER"
    Public Function POP_SRC_DataList(ByVal prd As String, ByVal comp As String, ByVal unitid As String, ByVal nik As String, ByVal statsM As String, ByVal nameM As String, ByVal tpPage As String, ByVal namaUN As String, ByVal sortF As String) As DataSet
        carr.Add(New cArrayList("@FLAG_PG", tpPage))
        odal.fn_GetOracleDataReader("yks_sp_SearchPOP_Find", carr, odr)
        ds.Load(odr, LoadOption.Upsert, "nik", "empNAME", "Unitname", "companyname", "productname", "EMPSTATS_DESC", "UnitID")
        odr.Close()
        carr.Clear()
        Return ds
    End Function

    Public Function GetUserFromId(userid As String) As TPROC_USER
        Dim uSer As New TPROC_USER
        carr.Add(New cArrayList("userid", userid))
        odal.fn_GetOracleDataReader("SP_GET_USER_USING_USER_ID", carr, odr)

        If odr.HasRows Then
            While odr.Read
                uSer.ID = Convert.ToInt32(odr.GetValue(odr.GetOrdinal("ID")))
                uSer.USER_ID = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_ID")))
                uSer.USER_NAME = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_NAME")))
                uSer.USER_MAIL = Convert.ToString(odr.GetValue(odr.GetOrdinal("USER_MAIL")))
            End While
        End If

        Return uSer
    End Function


    Public Function IsStatusReviewedByOther(pr_dt_id As Decimal, appr_wa_status As String) As Boolean
        Dim result As Boolean
        Dim db As New eProcurementEntities
        Dim appr_Wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = pr_dt_id).ToList()

        'set rs is false
        result = False

        For Each item In appr_Wa
            If item.APPR_WA_STATUS = appr_wa_status Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function
#End Region

#Region "PUSH EMAIL"
    Public Function SendPushEmailApprRevWA(pr_header_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities
        Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=1")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        '<<zoer20171010 add form type in subject
        Dim form_type = db.TPROC_FORM_TYPE.Find(pr_header.FORM_TYPE_ID)
        Dim formTypeName As String = form_type.FORM_TYPE_NAME
        '>>zoer20171010


        'get all wa Approval email
        Dim pr_dt = pr_header.TPROC_PR_DETAIL.ToList()
        For Each item_dt In pr_dt
            Dim emailToReviewAppr As New ListFieldNameAndValue
            Dim emailToApproveAppr As New ListFieldNameAndValue
            Dim item_name As String = item_dt.ITEM_NAME
            For Each item_wa In item_dt.TPROC_PR_APPR_WA
                If item_wa.APPR_WA_STATUS = "Waiting for review" Then
                    emailToReviewAppr.AddItem("Email", item_wa.EMAIL)
                ElseIf item_wa.APPR_WA_STATUS = "Waiting for approve" Then
                    emailToApproveAppr.AddItem("Email", item_wa.EMAIL)
                End If
            Next
            If emailToReviewAppr.Count > 0 Then
                result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, item_name, ListEnum.eProcApprAction.review.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR, item_name),
                                                sendEmail2)
            Else
                result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_WA, reqNumber, item_name, ListEnum.eProcApprAction.approve.ToString()),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_WA, link, reqNumber, reqDate, reqBy, statusPR, item_name),
                                                sendEmail2)
            End If
        Next

        If result.IsSuccess Then
            result = SendPushEmailToUser(pr_header_id, reqNumber, reqDate, pr_header.TPROC_USER.USER_MAIL, formTypeName, "LinkPR")
        End If

        Return result
    End Function

    Public Function SendPushEmailToApprRevRD(pr_header_id As Decimal) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim sendEmail2 As String = Nothing
        Dim db As New eProcurementEntities
        Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

        Dim reqNumber As String = pr_header.PR_NO
        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings("LinkPR")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + pr_header.ID.ToString() + "?flag=2")
        Dim reqDate As String = pr_header.PR_DATE.ToString("dd-MM-yyyy")
        Dim reqBy As String = pr_header.TPROC_USER.USER_NAME
        Dim statusPR As String = [Enum].GetName(GetType(ListEnum.PRStatus), Int32.Parse(pr_header.PR_STATUS)).ToString()
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

        '<<zoer20171010 add form type in subject
        Dim form_type = db.TPROC_FORM_TYPE.Find(pr_header.FORM_TYPE_ID)
        Dim formTypeName As String = form_type.FORM_TYPE_NAME
        '>>zoer20171010


        'get all wa Approval email
        For Each rd_gr In pr_header.TPROC_PR_APPR_RELDEPT_GR.ToList()
            Dim emailToReviewAppr As New ListFieldNameAndValue
            Dim emailToApproveAppr As New ListFieldNameAndValue
            For Each rd_dt In rd_gr.TPROC_PR_APPR_RELDEPT_DT
                If rd_dt.APPR_RELDEPT_STATUS = "Waiting for review" Then
                    emailToReviewAppr.AddItem("Email", rd_dt.EMAIL)
                ElseIf rd_dt.APPR_RELDEPT_STATUS = "Waiting for approve" Then
                    emailToApproveAppr.AddItem("Email", rd_dt.EMAIL)
                End If
            Next

            If emailToReviewAppr.Count > 0 Then
                result = mailFacade.SendEmail(emailSender, emailToReviewAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_RD, reqNumber, ListEnum.eProcApprAction.review.ToString(),
                                                                                                     formTypeName 'zoer20171010
                                                                                                     ),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_RD, link, reqNumber, reqDate, reqBy, statusPR),
                                                sendEmail2)
            Else
                result = mailFacade.SendEmail(emailSender, emailToApproveAppr, Nothing, String.Format(EmailTemplate.eProcSubjectEmailPR_RD, reqNumber, ListEnum.eProcApprAction.approve.ToString(),
                                                                                                      formTypeName 'zoer20171010
                                                                                                      ),
                                            String.Format(EmailTemplate.eProcEmailTemplatePRNotification_RD, link, reqNumber, reqDate, reqBy, statusPR),
                                                sendEmail2)
            End If
        Next

        If result.IsSuccess Then
            result = SendPushEmailToUser(pr_header.ID, reqNumber, reqDate, pr_header.TPROC_USER.USER_MAIL, formTypeName, "LinkPR")
        End If

        Return result
    End Function

    Public Function SendPushEmailToUser(header_id As Decimal, data_no As String, data_date As String, email_user As String, type_name As String, config_link As string) As ResultStatus
        Dim mailFacade As New EmailFacade
        Dim result As New ResultStatus
        Dim emailToUser As New ListFieldNameAndValue


        Dim linkHeaderDetail As String = ConfigurationSettings.AppSettings(config_link)
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkHeaderDetail + header_id.ToString() + "?flag=0")
        Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")
        emailToUser.AddItem("Email", email_user)

        result = mailFacade.SendEmail(emailSender, emailToUser, Nothing, String.Format(EmailTemplate.eProcSubjectEmailUser_Push, data_no, type_name),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotification_Push, link, data_no, data_date, Date.Now.ToString("dd-MM-yyy hh:mm")),
                                            Nothing)

        Return result
    End Function
#End Region

#Region "SIGN OFF"
    Function InsertEvaluation(ByVal pr_header_id As Decimal, ByVal supplier_id As Decimal, ByVal eval_detail As String) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Dim arry = eval_detail.Split("|")

            Dim supp_eval As New TPROC_SUPP_EVAL
            supp_eval.SUPPLIER_ID = supplier_id
            supp_eval.F_STM = arry(0)
            supp_eval.F_KM = arry(1)
            supp_eval.F_M = arry(2)
            supp_eval.F_SM = arry(3)
            supp_eval.F_KET = arry(4)
            supp_eval.A_TA = arry(5)
            supp_eval.A_TRK = arry(6)
            supp_eval.A_RK = arry(7)
            supp_eval.A_KET = arry(8)
            supp_eval.H_TK = arry(9)
            supp_eval.H_K = arry(10)
            supp_eval.H_SK = arry(11)
            supp_eval.H_KET = arry(12)
            supp_eval.PO_LP = arry(13)
            supp_eval.PO_LTA = arry(14)
            supp_eval.PO_CA = arry(15)
            supp_eval.PO_KET = arry(16)
            supp_eval.P_STTW = arry(17)
            supp_eval.P_KTTW = arry(18)
            supp_eval.P_TW = arry(19)
            supp_eval.P_KET = arry(20)
            supp_eval.HP_STSP = arry(21)
            supp_eval.HP_KTSP = arry(22)
            supp_eval.HP_SP = arry(23)
            supp_eval.HP_KET = arry(24)
            supp_eval.TOTAL_SCORE = arry(25)
            supp_eval.IS_RECOMMENDED = arry(26)
            supp_eval.KOMENTAR_SARAN = arry(27)
            supp_eval.CREATED_TIME = Date.Now
            supp_eval.CREATED_BY = CurrentUser.GetCurrentUserId()
            supp_eval.PR_HEADER_ID = pr_header_id

            Using db As New eProcurementEntities
                db.TPROC_SUPP_EVAL.Add(supp_eval)
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Function UpdatePRDetailSupplier(ByVal pr_header_id As Decimal, ByVal supplier_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Using scope As New TransactionScope()
            Try
                Using db As New eProcurementEntities
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
                    For Each item In pr_header.TPROC_PR_DETAIL.ToList()
                        If item.SUPPLIER_ID.Value = supplier_id Then
                            item.EVALUATED_SUPPLIER = 1
                            item.LAST_MODIFIED_TIME = Date.Now
                            item.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                            db.Entry(item).State = EntityState.Modified
                            db.SaveChanges()
                            rs.SetSuccessStatus()
                        End If
                    Next
                End Using

                If rs.IsSuccess Then
                    scope.Complete()
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function
#End Region


End Class
