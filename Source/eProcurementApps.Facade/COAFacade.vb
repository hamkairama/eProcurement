Imports eProcurementApps.Models
Imports System.Transactions
Imports eProcurementApps.Helpers


Public Class COAFacade
    Public Shared Function InsertCOApproval(ByVal coa_gr As TPROC_CHART_OF_ACCOUNT_GR, ByVal coa As TPROC_CHART_OF_ACCOUNTS) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr_id As Decimal

        Using scope As New TransactionScope()
            Try
                rs = InsertCOAGr(app_gr_id, coa_gr)
                If rs.IsSuccess Then
                    rs = InsertCOA(app_gr_id, coa)
                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus("Data has been created")
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function InsertCOAGr(ByRef id As Decimal, ByVal coa_gr As TPROC_CHART_OF_ACCOUNT_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                db.TPROC_CHART_OF_ACCOUNT_GR.Add(coa_gr)
                db.SaveChanges()
                id = db.TPROC_CHART_OF_ACCOUNT_GR.Max(Function(x) x.ID)
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function InsertCOA(ByVal sub_coa_gr_id As Decimal, ByVal coa As TPROC_CHART_OF_ACCOUNTS) As ResultStatus
        Dim rs As New ResultStatus

        Try
            coa.CREATED_BY = CurrentUser.GetCurrentUserId
            coa.CREATED_TIME = Date.Now
            coa.SUB_COA_GR_ID = sub_coa_gr_id

            Using db As New eProcurementEntities
                db.TPROC_CHART_OF_ACCOUNTS.Add(coa)
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateRowStatusCOA(id As Decimal, rowStat As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim coa As New TPROC_CHART_OF_ACCOUNTS

        Try
            Using db As New eProcurementEntities
                coa = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
                coa.ROW_STATUS = rowStat
                coa.LAST_MODIFIED_TIME = Date.Now
                coa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(coa).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateCOA(ByVal id As Decimal, ByVal coa_gr As TPROC_CHART_OF_ACCOUNT_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim coa = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
                coa.LAST_MODIFIED_TIME = Date.Now
                coa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
                coa.ROW_STATUS = ListEnum.RowStat.Live

                coa.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM = coa_gr.ACCT_NUM
                coa.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC = coa_gr.ACCT_DESC
                coa.TPROC_CHART_OF_ACCOUNT_GR.CONVERTED_ACCT_NUM = coa_gr.CONVERTED_ACCT_NUM
                coa.TPROC_CHART_OF_ACCOUNT_GR.LAST_MODIFIED_TIME = Date.Now
                coa.TPROC_CHART_OF_ACCOUNT_GR.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
                coa.TPROC_CHART_OF_ACCOUNT_GR.ROW_STATUS = ListEnum.RowStat.Live

                db.Entry(coa).State = EntityState.Modified
                db.SaveChanges()

                rs.SetSuccessStatus("Data has been edited")
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateCOA(ByVal id As Decimal, ByVal chart_of_account As TPROC_CHART_OF_ACCOUNTS) As ResultStatus
        Dim rs As New ResultStatus
        Dim coa As New TPROC_CHART_OF_ACCOUNTS

        Try
            Using db As New eProcurementEntities
                coa = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
                coa.ROW_STATUS = chart_of_account.ROW_STATUS
                coa.LAST_MODIFIED_TIME = chart_of_account.LAST_MODIFIED_TIME
                coa.LAST_MODIFIED_BY = chart_of_account.LAST_MODIFIED_BY

                'from request edit
                If chart_of_account.SUB_COA_GR_ID <> 0 Then
                    coa.SUB_COA_GR_ID = chart_of_account.SUB_COA_GR_ID
                End If

                db.Entry(coa).State = EntityState.Modified
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdatesCOAByRequest(id As Decimal, status1 As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim coa As New TPROC_CHART_OF_ACCOUNTS
        Dim coa_gr_new As New TPROC_CHART_OF_ACCOUNT_GR

        Using scope As New TransactionScope()
            Try
                rs = UpdateCOAGrOld(id)
                If rs.IsSuccess Then
                    rs = UpdateCOAGrNew(id, coa_gr_new)
                    If rs.IsSuccess Then
                        coa.SUB_COA_GR_ID = coa_gr_new.ID
                        coa.ROW_STATUS = status1
                        coa.LAST_MODIFIED_TIME = Date.Now
                        coa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                        rs = UpdateCOA(id, coa)
                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus("Data has been success")
                        End If
                    End If
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function UpdateCOAGrNew(ByVal coa_id As Decimal, ByRef appr_gr_new As TPROC_CHART_OF_ACCOUNT_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim coa_gr = db.TPROC_CHART_OF_ACCOUNT_GR.Where(Function(x) x.REV_COA_ID = coa_id And x.ROW_STATUS = ListEnum.RowStat.Edit).FirstOrDefault()
                coa_gr.ROW_STATUS = ListEnum.RowStat.Live
                coa_gr.LAST_MODIFIED_TIME = Date.Now
                coa_gr.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                appr_gr_new = coa_gr

                db.Entry(coa_gr).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateCOAGrOld(ByVal id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim coa_gr = db.TPROC_CHART_OF_ACCOUNTS.Find(id).TPROC_CHART_OF_ACCOUNT_GR

                coa_gr.REV_COA_ID = id
                coa_gr.ROW_STATUS = ListEnum.RowStat.InActive
                coa_gr.LAST_MODIFIED_TIME = Date.Now
                coa_gr.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                db.Entry(coa_gr).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function GetCoaGrToBe(rd_id As Decimal, row_stat As Integer) As TPROC_CHART_OF_ACCOUNT_GR
        Dim coa_gr_tobe As New TPROC_CHART_OF_ACCOUNT_GR
        Using db As New eProcurementEntities
            coa_gr_tobe = db.TPROC_CHART_OF_ACCOUNT_GR.Where(Function(x) x.REV_COA_ID = rd_id And x.ROW_STATUS = row_stat).FirstOrDefault()
        End Using

        Return coa_gr_tobe
    End Function


End Class
