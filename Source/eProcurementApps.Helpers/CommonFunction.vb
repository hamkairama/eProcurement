Imports System.Configuration
Imports System.Web
Imports eProcurementApps.Models
Imports System.IO

Public Class CommonFunction

    Public Shared Function GetCurrency() As String
        Dim currency As String = ""

        Using db As New eProcurementEntities()
            Dim max_curr As Date = db.TPROC_CURRENCY.Max(Function(x) x.START_DATE)
            currency = db.TPROC_CURRENCY.Where(Function(x) x.START_DATE = max_curr And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().CURRENCY_NAME
        End Using

        Return currency
    End Function

    Public Shared Function GetLinkEproc() As String
        Dim linkEproc As String = ""
        linkEproc = ConfigurationSettings.AppSettings("LinkeProc")

        Return linkEproc
    End Function

    Public Shared Function GetItemCd(id As Decimal) As String
        Dim result As String = ""
        Using db As New eProcurementEntities
            result = db.TPROC_STOCK.Find(id).ITEM_CODE
        End Using

        Return result
    End Function

    Public Shared Function ConvertStatusItem(detail_status As Decimal) As String
        Dim result As String = ""

        If detail_status = ListEnum.ItemStatus.Complete Then
            result = "darkseagreen"
        ElseIf detail_status = ListEnum.ItemStatus.ReadyToApprove Then
            result = "darkcyan"
        ElseIf detail_status = ListEnum.ItemStatus.Rejected Then
            result = "hotpink"
        End If

        Return result
    End Function

    Public Shared Function ConvertStatusPr(status As Decimal) As String
        Dim result As String = ""

        If status = ListEnum.PRStatus.Submitted Then
            result = "green"
        ElseIf status = ListEnum.PRStatus.PrApprovedComplete Then
            result = "grey"
        ElseIf status = ListEnum.PRStatus.PrRejected Then
            result = "red"
        ElseIf status = ListEnum.PRStatus.PrHandled Then
            result = "pink"
        ElseIf status = ListEnum.PRStatus.CreatePo Then
            result = "purple"
        Else
            result = "blue"
        End If

        Return result
    End Function

    Public Shared Function IsReadyToApprove(lappr_wa As List(Of TPROC_PR_APPR_WA)) As Boolean
        Dim result As Boolean = True
        For Each item In lappr_wa
            If item.APPR_WA_STATUS = "Waiting for review" Or item.APPR_WA_STATUS = "Approved" Or item.APPR_WA_STATUS = "Rejected" Then
                result = False
                Exit For
            End If
        Next

        Return result
    End Function

    Public Shared Function GetListPRInSignOffAndInReject(user_id_id As Decimal) As List(Of TPROC_PR_HEADER)
        Dim pr_header As New List(Of TPROC_PR_HEADER)

        If user_id_id > 0 Then
            Using db As New eProcurementEntities
                pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.USER_ID_ID = user_id_id And y.PR_STATUS <> ListEnum.PRStatus.PrRejected And y.PR_STATUS <> ListEnum.PRStatus.SignOff And y.ROW_STATUS = ListEnum.RowStat.Live).OrderByDescending(Function(x) x.PR_NO).ToList()
            End Using
        End If

        If pr_header.Count > 0 Then
            For Each pr In pr_header
                If pr.PR_STATUS = ListEnum.PRStatus.Complete Then
                    Dim header_param As New PR_HEADER_PARAM
                    header_param.PRNoCompleted_temp = pr.PR_NO
                    header_param.PRHeaderId_temp = pr.ID
                    header_param.PRFlag_temp = ListEnum.FlagDetail.MyPRReadyToSignOff
                    Exit For
                End If
            Next
        End If

        Return pr_header
    End Function

    Public Shared Function GetSunBudgetCode() As TPROC_BUDGET_CODE
        Dim sun As New TPROC_BUDGET_CODE

        Using db As New eProcurementEntities
            sun = db.TPROC_BUDGET_CODE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
        End Using

        Return sun
    End Function

    Public Shared Function GetActPrd(dt As DateTime) As String
        Dim result As String = ""

        Dim month = dt.Month
        If month < 10 Then
            result = dt.Year.ToString() + "00" + month.ToString()
        Else
            result = dt.Year.ToString() + "0" + month.ToString()
        End If

        Return result
    End Function


    Public Shared Function GetFormTypeName(id As Decimal) As String
        Dim name As String = ""

        If id > 0 Then
            Using db As New eProcurementEntities
                name = db.TPROC_FORM_TYPE.Find(id).FORM_TYPE_NAME.ToUpper()
            End Using
        End If

        Return name
    End Function

    Public Shared Function GetDefaultSelectedRole() As Decimal
        Dim role_id As Decimal
        Dim role As New TPROC_ROLE

        Using db As New eProcurementEntities
            role = db.TPROC_ROLE.Where(Function(x) x.DEFAULT_SELECTED = 1 And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
        End Using

        If role Is Nothing Then
            role_id = 0
        Else
            role_id = role.ID
        End If

        Return role_id
    End Function

    Public Shared Function GetDefaultCurrencyIDRConvertion() As Decimal
        Dim convert As Decimal
        Dim curr As New TPROC_CURRENCY

        Try
            Using db As New eProcurementEntities
                curr = db.TPROC_CURRENCY.Where(Function(x) x.CURRENCY_NAME.Contains("IDR") And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            End Using

            If curr IsNot Nothing Then
                convert = curr.CONVERSION_RP
            End If

        Catch ex As Exception
        End Try

        Return convert
    End Function

    Public Shared Function GetGoodTypeNonStockId() As Decimal
        Dim id As Decimal

        Using db As New eProcurementEntities
            id = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper().Replace(" ", "") = "NONSTOCK" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
        End Using

        Return id
    End Function

    Public Shared Function GetValueNotEmpty(value As Object) As String
        Dim result As String = ""
        If value IsNot Nothing Then
            result = value.ToString()
        End If

        Return result
    End Function

    Public Shared Function GetIndLevelById(id As Decimal) As String
        Dim result As String = ""
        Using db As New eProcurementEntities
            result = db.TPROC_LEVEL.Find(id).INDONESIAN_LEVEL
        End Using

        Return result
    End Function


    Public Shared Function GetDivisionNameById(id As Decimal) As String
        Dim result As String = ""
        Using db As New eProcurementEntities
            result = db.TPROC_DIVISION.Find(id).DIVISION_NAME
        End Using

        Return result
    End Function

    Public Shared Function GetRelDeptNameById(id As Decimal) As String
        Dim result As String = ""
        Using db As New eProcurementEntities
            result = db.TPROC_REL_DEPT.Find(id).RELATED_DEPARTMENT_NAME
        End Using

        Return result
    End Function

    Public Shared Function GetPcDtByPcId(pc_id As Decimal) As List(Of TPROC_PC_DT)
        Dim lPcDt As New List(Of TPROC_PC_DT)
        Dim dbPublic As New eProcurementEntities
        'Using db As New eProcurementEntities
        lPcDt = dbPublic.TPROC_PC_DT.Where(Function(x) x.PC_ID = pc_id).ToList()
        'End Using

        Return lPcDt
    End Function

    Public Shared Function GetLimitForStatus(limit As Decimal, total_price As Decimal) As String
        Dim status As String = ""

        If limit < total_price Then
            status = "Waiting for review"
        Else
            status = "Waiting for approve"
        End If

        Return status
    End Function

    Public Shared Function GetLimitForAsIs(limit As Decimal, total_price As Decimal) As String
        Dim Asis As String = ""

        If limit < total_price Then
            Asis = "Reviewer"
        Else
            Asis = "Approver"
        End If

        Return Asis
    End Function


    'Public Shared Function GetAknowApprDtByPcId(pc_id As Decimal) As List(Of TPROC_ACKNOW_APPR_DT)
    '    Dim lacknow_appr_dt As New List(Of TPROC_ACKNOW_APPR_DT)
    '    Using db As New eProcurementEntities
    '        Dim acknow_appr = db.TPROC_ACKNOW_APPR.Where(Function(x) x.PC_ID = pc_id).FirstOrDefault()
    '        If acknow_appr IsNot Nothing Then
    '            For Each item In acknow_appr.TPROC_ACKNOW_APPR_DT
    '                lacknow_appr_dt.Add(item)
    '            Next
    '        End If
    '    End Using

    '    Return lacknow_appr_dt
    'End Function

    Public Shared Function GetAknowApprByPcId(pc_id As Decimal) As List(Of TPROC_ACKNOW_APPR)
        Dim lacknow_appr As New List(Of TPROC_ACKNOW_APPR)
        Dim db As New eProcurementEntities

        lacknow_appr = db.TPROC_ACKNOW_APPR.Where(Function(x) x.PC_ID = pc_id).ToList()

        Return lacknow_appr
    End Function

    Public Shared Function GetBalanceQtyMinItem(qty As Decimal, qty_min As Decimal) As Decimal
        Return qty - qty_min
    End Function

    Public Shared Function GetAdditionalBc(fst_id As Decimal, row_status As Decimal) As List(Of TPROC_FST_BUDGET_CD_ADD)
        Dim result As New List(Of TPROC_FST_BUDGET_CD_ADD)
        Dim db As New eProcurementEntities

        Try
            Dim gr_fst = db.TPROC_FORM_SUBTYPE_GR.Where(Function(x) x.REV_FST_ID = fst_id And x.ROW_STATUS = row_status).FirstOrDefault()

            If gr_fst IsNot Nothing Then
                Dim gr_fst_id = gr_fst.ID

                result = db.TPROC_FST_BUDGET_CD_ADD.Where(Function(x) x.FORM_SUBTYPE_GR_ID = gr_fst_id).ToList()
            End If
        Catch ex As Exception
        End Try

        Return result
    End Function

    Public Shared Function GetPathParam(ByVal param_code As String) As String
        Dim result As String = ""

        Try
            Using db As New eProcurementEntities
                'result = db.TPROC_PARAMETERS.Where(Function(x) x.PARAMETER_CODE = param_code).FirstOrDefault().PARAMETER_VALUE
            End Using
        Catch ex As Exception
        End Try

        Return result
    End Function

    Public Shared Function CheckFolderExisting(ByVal path As String) As ResultStatus
        Dim rs As New ResultStatus
        Try
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function


End Class
