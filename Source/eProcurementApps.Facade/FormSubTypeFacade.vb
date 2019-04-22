Imports eProcurementApps.Models
Imports System.Transactions
Imports eProcurementApps.Helpers

Public Class FormSubTypeFacade

#Region "CREATE FORM SUB TYPE"
    Public Shared Function InsertFormSubTypeAll(ByVal lapp_detail As String(), ByVal fst_gr As TPROC_FORM_SUBTYPE_GR, ByVal form_sub_type As TPROC_FORM_SUB_TYPE, ByVal form_sub_type_bc As String()) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr_id As Decimal

        Using scope As New TransactionScope()
            Try
                rs = InsertFormSubTypeGr(app_gr_id, fst_gr)
                If rs.IsSuccess Then
                    rs = InsertFormSubTypeDt(lapp_detail, app_gr_id)
                    If rs.IsSuccess Then
                        rs = InsertFormSubType(app_gr_id, form_sub_type)
                        If rs.IsSuccess Then
                            rs = InsertAdditionalBudgetFst(app_gr_id, form_sub_type_bc)
                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus()
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

    Private Shared Function InsertAdditionalBudgetFst(fst_gr_id As Decimal, fom_sub_type_bc As String()) As ResultStatus
        Dim rs As New ResultStatus

        Try
            If fom_sub_type_bc Is Nothing Then
                rs.SetSuccessStatus() 'related department can be null
            Else
                For Each item As String In fom_sub_type_bc
                    Dim arry = item.Split("|")
                    Dim sub_form_bc As New TPROC_FST_BUDGET_CD_ADD
                    sub_form_bc.FORM_SUBTYPE_GR_ID = fst_gr_id
                    sub_form_bc.BUDGET_CODE = arry(0)
                    sub_form_bc.ACCOUNT_CODE_START = arry(1)
                    sub_form_bc.ACCOUNT_CODE_END = arry(2)
                    sub_form_bc.CREATED_BY = CurrentUser.GetCurrentUserId()
                    sub_form_bc.CREATED_TIME = Date.Now
                    Using db As New eProcurementEntities
                        db.TPROC_FST_BUDGET_CD_ADD.Add(sub_form_bc)
                        db.SaveChanges()
                    End Using
                Next
                rs.SetSuccessStatus()
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function InsertFormSubType(ByVal sub_form_gr_id As Decimal, ByVal form_sub_type As TPROC_FORM_SUB_TYPE) As ResultStatus
        Dim rs As New ResultStatus

        Try
            form_sub_type.CREATED_BY = CurrentUser.GetCurrentUserId
            form_sub_type.CREATED_TIME = Date.Now
            form_sub_type.SUB_FORMTYPE_GR_ID = sub_form_gr_id

            Using db As New eProcurementEntities
                db.TPROC_FORM_SUB_TYPE.Add(form_sub_type)
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try
        Return rs
    End Function

    Public Shared Function InsertFormSubTypeDt(ByVal lapp_detail As String(), ByVal sub_form_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Try
            If lapp_detail Is Nothing Then
                rs.SetSuccessStatus() 'related department can be null
            Else
                For Each item As String In lapp_detail
                    Dim arry = item.Split("|")
                    If arry(0) > 0 Then
                        Dim sub_form_dt As New TPROC_FORM_SUBTYPE_DT
                        sub_form_dt.FORM_SUBTYPE_GR_ID = sub_form_gr_id
                        sub_form_dt.REL_DEPT_ID = arry(0)
                        sub_form_dt.FLOW_NUMBER = arry(1)
                        sub_form_dt.CREATED_BY = CurrentUser.GetCurrentUserId()
                        sub_form_dt.CREATED_BY = Date.Now
                        Using db As New eProcurementEntities
                            db.TPROC_FORM_SUBTYPE_DT.Add(sub_form_dt)
                            db.SaveChanges()
                        End Using
                    Else
                        Exit For
                    End If
                Next
                rs.SetSuccessStatus()
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function InsertFormSubTypeGr(ByRef id As Decimal, ByVal sub_form_gr As TPROC_FORM_SUBTYPE_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                db.TPROC_FORM_SUBTYPE_GR.Add(sub_form_gr)
                db.SaveChanges()
                id = db.TPROC_FORM_SUBTYPE_GR.Max(Function(x) x.ID)
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
#End Region


#Region "EDIT FORM SUB TYPE"
    Public Shared Function UpdateFormSubTypeAll(ByVal lapp_detail As String(), ByVal wa As TPROC_FORM_SUB_TYPE) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr_id As Decimal
        Using scope As New TransactionScope()
            Try
                rs = UpdateFormSubTypeGr(app_gr_id)
                If rs.IsSuccess Then
                    rs = UpdateFormSubTypeDt(lapp_detail, app_gr_id)
                    If rs.IsSuccess Then
                        rs = UpdateFormSubType(wa, app_gr_id)
                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function UpdateFormSubType(ByVal form_sub_type As TPROC_FORM_SUB_TYPE, ByVal sub_form_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Try
            form_sub_type.SUB_FORMTYPE_GR_ID = sub_form_gr_id
            Using db As New eProcurementEntities
                db.TPROC_FORM_SUB_TYPE.Add(form_sub_type)
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try
        Return rs
    End Function

    Public Shared Function UpdateFormSubType(ByVal id As Decimal, ByVal form_sub_type As TPROC_FORM_SUB_TYPE) As ResultStatus
        Dim rs As New ResultStatus
        Dim fst As New TPROC_FORM_SUB_TYPE

        Try
            Using db As New eProcurementEntities
                fst = db.TPROC_FORM_SUB_TYPE.Find(id)
                fst.ROW_STATUS = form_sub_type.ROW_STATUS
                fst.LAST_MODIFIED_TIME = Date.Now
                fst.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId

                'from request edit
                If form_sub_type.SUB_FORMTYPE_GR_ID <> 0 Then
                    fst.SUB_FORMTYPE_GR_ID = form_sub_type.SUB_FORMTYPE_GR_ID
                End If

                db.Entry(fst).State = EntityState.Modified
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateFormSubTypeDt(ByVal lapp_detail As String(), ByVal sub_form_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Try
            For Each item As String In lapp_detail
                Dim arry = item.Split("|")
                If arry(0) > 0 Then
                    Dim sub_form_dt As New TPROC_FORM_SUBTYPE_DT
                    sub_form_dt.FORM_SUBTYPE_GR_ID = sub_form_gr_id
                    sub_form_dt.REL_DEPT_ID = arry(0)
                    sub_form_dt.FLOW_NUMBER = arry(1)
                    sub_form_dt.CREATED_BY = CurrentUser.GetCurrentUserId()
                    sub_form_dt.CREATED_BY = Date.Now
                    Using db As New eProcurementEntities
                        db.TPROC_FORM_SUBTYPE_DT.Add(sub_form_dt)
                        db.SaveChanges()
                    End Using
                Else
                    Exit For
                End If
            Next

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateFormSubTypeGr(ByRef id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim sub_form_gr As New TPROC_FORM_SUBTYPE_GR
        sub_form_gr.CREATED_TIME = Date.Now
        sub_form_gr.CREATED_BY = CurrentUser.GetCurrentUserId

        Try
            Using db As New eProcurementEntities
                db.TPROC_FORM_SUBTYPE_GR.Add(sub_form_gr)
                db.SaveChanges()
                id = db.TPROC_FORM_SUBTYPE_GR.Max(Function(x) x.ID)
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateFormSubTypeGr(ByVal id As Decimal, ByVal sub_form_gr As TPROC_FORM_SUBTYPE_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim app_gr = db.TPROC_FORM_SUBTYPE_GR.Find(id)
                app_gr.FORM_TYPE_ID = sub_form_gr.FORM_TYPE_ID
                app_gr.SUB_FORM_TYPE_NAME = sub_form_gr.SUB_FORM_TYPE_NAME
                app_gr.SUB_FORM_TYPE_DESCRIPTION = sub_form_gr.SUB_FORM_TYPE_DESCRIPTION
                app_gr.SLA = sub_form_gr.SLA
                app_gr.POPUP_ACCOUNT = sub_form_gr.POPUP_ACCOUNT
                app_gr.BUDGET_CODE = sub_form_gr.BUDGET_CODE
                app_gr.ACCOUNT_CODE_START = sub_form_gr.ACCOUNT_CODE_START
                app_gr.ACCOUNT_CODE_END = sub_form_gr.ACCOUNT_CODE_END
                app_gr.LAST_MODIFIED_TIME = Date.Now
                app_gr.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                db.Entry(app_gr).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateAdditionalBudgetFst(fst_gr_id As Decimal, fom_sub_type_bc As String()) As ResultStatus
        Dim rs As New ResultStatus

        Try
            rs = DeleteAdditionalBudgetFstByGstGrId(fst_gr_id)
            If rs.IsSuccess Then
                rs = InsertAdditionalBudgetFst(fst_gr_id, fom_sub_type_bc)
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Private Shared Function DeleteAdditionalBudgetFstByGstGrId(fst_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try

            Dim db As New eProcurementEntities
            Dim add_fst_list = db.TPROC_FST_BUDGET_CD_ADD.Where(Function(x) x.FORM_SUBTYPE_GR_ID = fst_gr_id).ToList()
            For Each item In add_fst_list
                db.TPROC_FST_BUDGET_CD_ADD.Remove(item)
                db.SaveChanges()
            Next

            rs.SetSuccessStatus()

        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

#End Region

    Public Shared Function DeleteFormSubType(form_sub_type_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Using scope As New TransactionScope()
            Try
                Dim form_sub_gr_id = db.TPROC_FORM_SUB_TYPE.Find(form_sub_type_id).SUB_FORMTYPE_GR_ID

                Dim fom_sub_type_dt As List(Of TPROC_FORM_SUBTYPE_DT) = db.TPROC_FORM_SUBTYPE_DT.Where(Function(x) x.FORM_SUBTYPE_GR_ID = form_sub_gr_id).ToList()
                For Each item As TPROC_FORM_SUBTYPE_DT In fom_sub_type_dt
                    db.TPROC_FORM_SUBTYPE_DT.Remove(item)
                Next

                db.TPROC_FORM_SUBTYPE_GR.Remove(db.TPROC_FORM_SUBTYPE_GR.Find(form_sub_gr_id))
                db.TPROC_FORM_SUB_TYPE.Remove(db.TPROC_FORM_SUB_TYPE.Find(form_sub_type_id))
                db.SaveChanges()

                scope.Complete()
            Catch ex As Exception
            End Try
        End Using
        Return rs
    End Function

    Public Shared Function GetRelDeptFromSubTypeId(form_sub_type_id As Decimal) As List(Of TPROC_FORM_SUBTYPE_DT)
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities
        Dim result As New List(Of TPROC_FORM_SUBTYPE_DT)

        If form_sub_type_id > 0 Then
            Dim sub_formtype_gr_id = db.TPROC_FORM_SUB_TYPE.Find(form_sub_type_id).SUB_FORMTYPE_GR_ID
            result = db.TPROC_FORM_SUBTYPE_DT.Where(Function(x) x.FORM_SUBTYPE_GR_ID = sub_formtype_gr_id).ToList()
        End If

        Return result
    End Function

    Public Shared Function UpdatesFSTByRequest(id As Decimal, status1 As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim fst As New TPROC_FORM_SUB_TYPE
        Dim appr_gr_new As New TPROC_FORM_SUBTYPE_GR

        Using scope As New TransactionScope()
            Try
                rs = UpdateApprovalGrOld(id)
                If rs.IsSuccess Then
                    rs = UpdateApprovalGrNew(id, appr_gr_new)
                    If rs.IsSuccess Then
                        fst.SUB_FORMTYPE_GR_ID = appr_gr_new.ID
                        fst.ROW_STATUS = status1
                        rs = UpdateFormSubType(id, fst)
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

    Public Shared Function UpdateApprovalGrNew(ByVal fst_id As Decimal, ByRef appr_gr_new As TPROC_FORM_SUBTYPE_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim app_gr = db.TPROC_FORM_SUBTYPE_GR.Where(Function(x) x.REV_FST_ID = fst_id And x.ROW_STATUS = ListEnum.RowStat.Edit).FirstOrDefault()
                app_gr.ROW_STATUS = ListEnum.RowStat.Live
                app_gr.LAST_MODIFIED_TIME = Date.Now
                app_gr.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                appr_gr_new = app_gr

                db.Entry(app_gr).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateApprovalGrOld(ByVal id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim fst_gr_id = db.TPROC_FORM_SUB_TYPE.Find(id).SUB_FORMTYPE_GR_ID

                Dim fst_gr = db.TPROC_FORM_SUBTYPE_GR.Find(fst_gr_id)
                fst_gr.REV_FST_ID = id
                fst_gr.ROW_STATUS = ListEnum.RowStat.InActive
                fst_gr.LAST_MODIFIED_TIME = Date.Now
                fst_gr.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                db.Entry(fst_gr).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function GetFstDtToBe(rd_id As Decimal, row_stat As Integer) As List(Of TPROC_FORM_SUBTYPE_DT)
        Dim lfst_dt As New List(Of TPROC_FORM_SUBTYPE_DT)
        Using db As New eProcurementEntities
            Dim fst_gr = db.TPROC_FORM_SUBTYPE_GR.Where(Function(x) x.REV_FST_ID = rd_id And x.ROW_STATUS = row_stat).FirstOrDefault()

            If fst_gr IsNot Nothing Then
                lfst_dt = db.TPROC_FORM_SUBTYPE_DT.Where(Function(y) y.FORM_SUBTYPE_GR_ID = fst_gr.ID).ToList()
            End If
        End Using

        Return lfst_dt
    End Function

    Public Shared Function GetFstGrToBe(rd_id As Decimal, row_stat As Integer) As TPROC_FORM_SUBTYPE_GR
        Dim lfst_gr As New TPROC_FORM_SUBTYPE_GR
        Dim db20 As New eProcurementEntities
        lfst_gr = db20.TPROC_FORM_SUBTYPE_GR.Where(Function(x) x.REV_FST_ID = rd_id And x.ROW_STATUS = row_stat).FirstOrDefault()


        Return lfst_gr
    End Function


End Class
