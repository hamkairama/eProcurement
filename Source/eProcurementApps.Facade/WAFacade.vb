Imports eProcurementApps.Models
Imports System.Transactions
Imports eProcurementApps.Helpers


Public Class WAFacade
    Public Shared Function InsertWApproval(ByVal lapp_detail As String(), ByVal wa As TPROC_WA, ByVal dept_name As String, ByVal division_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr_id As Decimal
        Using scope As New TransactionScope()
            Try
                rs = InsertApprovalGr(app_gr_id, dept_name, division_id, Nothing)
                If rs.IsSuccess Then
                    rs = InsertApprovalDt(lapp_detail, app_gr_id)
                    If rs.IsSuccess Then
                        rs = InsertWA(wa, app_gr_id)
                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus("Data has been created")
                        End If
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function InsertWA(ByVal wa As TPROC_WA, ByVal app_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Try
            wa.APPROVAL_GROUP_ID = app_gr_id
            Using db As New eProcurementEntities
                db.TPROC_WA.Add(wa)
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try
        Return rs
    End Function

    Public Shared Function UpdateWA(ByVal id As Decimal, ByVal work_area As TPROC_WA) As ResultStatus
        Dim rs As New ResultStatus
        Dim wa As New TPROC_WA

        Try
            Using db As New eProcurementEntities
                wa = db.TPROC_WA.Find(id)
                wa.ROW_STATUS = work_area.ROW_STATUS
                wa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
                wa.LAST_MODIFIED_TIME = Date.Now

                'from request edit
                If work_area.APPROVAL_GROUP_ID <> 0 Then
                    wa.APPROVAL_GROUP_ID = work_area.APPROVAL_GROUP_ID
                End If

                db.Entry(wa).State = EntityState.Modified
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function InsertApprovalDt(ByVal lapp_detail As String(), ByVal app_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Try
            For Each item As String In lapp_detail
                Dim arry = item.Split("|")
                Dim app_dt As New TPROC_APPROVAL_DT
                app_dt.APPROVAL_GROUP_ID = app_gr_id
                app_dt.APPROVAL_NAME = arry(0)
                app_dt.USER_NAME = arry(1)
                app_dt.EMAIL = arry(2)
                app_dt.LEVEL_ID = arry(3)
                app_dt.FLOW_NUMBER = arry(4)
                app_dt.CREATED_BY = CurrentUser.GetCurrentUserId()
                app_dt.CREATED_TIME = Date.Now
                Using db As New eProcurementEntities
                    db.TPROC_APPROVAL_DT.Add(app_dt)
                    db.SaveChanges()
                End Using
            Next
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    'Public Shared Function InsertApprovalGr(ByRef app_gr_id As Decimal, ByVal dept_name As String, ByVal division_id As Decimal) As ResultStatus
    '    Dim rs As New ResultStatus
    '    Dim app_gr As New TPROC_APPROVAL_GR
    '    app_gr.DEPARTMENT_NAME = dept_name
    '    app_gr.DIVISION_ID = division_id
    '    app_gr.CREATED_TIME = Date.Now
    '    app_gr.CREATED_BY = CurrentUser.GetCurrentUserId()

    '    Try
    '        Using db As New eProcurementEntities
    '            db.TPROC_APPROVAL_GR.Add(app_gr)
    '            db.SaveChanges()
    '            app_gr_id = db.TPROC_APPROVAL_GR.Max(Function(x) x.ID)
    '        End Using

    '        rs.SetSuccessStatus()
    '    Catch ex As Exception
    '        rs.SetErrorStatus(ex.Message)
    '    End Try

    '    Return rs
    'End Function

    Public Shared Function InsertApprovalGr(ByRef app_gr_id As Decimal, ByVal dept_name As String, ByVal division_id As Decimal, ByVal wa_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr As New TPROC_APPROVAL_GR
        app_gr.DEPARTMENT_NAME = dept_name
        app_gr.DIVISION_ID = division_id
        app_gr.CREATED_TIME = Date.Now
        app_gr.CREATED_BY = CurrentUser.GetCurrentUserId()
        app_gr.REV_WA_ID = wa_id
        app_gr.ROW_STATUS = ListEnum.RowStat.Edit

        Try
            Using db As New eProcurementEntities
                db.TPROC_APPROVAL_GR.Add(app_gr)
                db.SaveChanges()
                app_gr_id = db.TPROC_APPROVAL_GR.Max(Function(x) x.ID)
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateApprovalGr(ByVal id As Decimal, ByVal dept_name As String, ByVal division_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim app_gr = db.TPROC_APPROVAL_GR.Find(id)
                app_gr.DEPARTMENT_NAME = dept_name
                app_gr.DIVISION_ID = division_id
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

    Public Shared Function DeleteWA(id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Try
            Dim wa = db.TPROC_WA.Find(id)
            wa.ROW_STATUS = ListEnum.RowStat.InActive
            wa.LAST_MODIFIED_TIME = Date.Now
            wa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
            db.Entry(wa).State = EntityState.Modified

            db.SaveChanges()
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try
        Return rs
    End Function

    Public Shared Function DeleteWApproval(wa_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Using scope As New TransactionScope()
            Try
                Dim app_gr_id = db.TPROC_WA.Find(wa_id).APPROVAL_GROUP_ID

                Dim lapproval_dt As List(Of TPROC_APPROVAL_DT) = db.TPROC_APPROVAL_DT.Where(Function(x) x.APPROVAL_GROUP_ID = app_gr_id).ToList()
                For Each item As TPROC_APPROVAL_DT In lapproval_dt
                    db.TPROC_APPROVAL_DT.Remove(item)
                Next

                db.TPROC_APPROVAL_GR.Remove(db.TPROC_APPROVAL_GR.Find(app_gr_id))
                db.TPROC_WA.Remove(db.TPROC_WA.Find(wa_id))
                db.SaveChanges()

                scope.Complete()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using
        Return rs
    End Function

    Public Shared Function DeleteWApprovalSelected(ids As Integer()) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope()
            Try
                For Each id In ids
                    rs = DeleteWApproval(id)
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

    Public Shared Function EditWApproval(ByVal id As Decimal, ByVal lapp_detail As String(), ByVal wa As TPROC_WA, ByVal dept_name As String, ByVal division_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim appr_Gr_id As Decimal
        Using scope As New TransactionScope()
            Try
                Using db As New eProcurementEntities
                    Dim tproc_wa = db.TPROC_WA.Find(id)
                    tproc_wa.WA_NUMBER = wa.WA_NUMBER
                    tproc_wa.LAST_MODIFIED_TIME = wa.LAST_MODIFIED_TIME
                    tproc_wa.LAST_MODIFIED_BY = wa.LAST_MODIFIED_BY
                    tproc_wa.ROW_STATUS = ListEnum.RowStat.Live
                    db.Entry(tproc_wa).State = EntityState.Modified

                    appr_Gr_id = tproc_wa.APPROVAL_GROUP_ID
                    Dim lapproval_dt As List(Of TPROC_APPROVAL_DT) = db.TPROC_APPROVAL_DT.Where(Function(x) x.APPROVAL_GROUP_ID = appr_Gr_id).ToList()
                    If lapproval_dt.Count > 0 Then
                        For Each item As TPROC_APPROVAL_DT In lapproval_dt
                            db.TPROC_APPROVAL_DT.Remove(item)
                        Next
                    End If

                    db.SaveChanges()
                    rs.SetSuccessStatus()
                End Using

                If rs.IsSuccess Then
                    rs = UpdateApprovalGr(appr_Gr_id, dept_name, division_id)
                End If

                If rs.IsSuccess Then
                    rs = InsertApprovalDt(lapp_detail, appr_Gr_id)
                End If

                If rs.IsSuccess Then
                    scope.Complete()
                    rs.SetSuccessStatus("Data has been edited")
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function UpdateRowStatusWA(id As Decimal, rowStat As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim wa As New TPROC_WA

        Try
            Using db As New eProcurementEntities
                wa = db.TPROC_WA.Find(id)
                wa.ROW_STATUS = rowStat
                wa.LAST_MODIFIED_TIME = Date.Now
                wa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(wa).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdatesWAByRequest(id As Decimal, status1 As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim wa As New TPROC_WA
        Dim appr_gr_new As New TPROC_APPROVAL_GR


        Using scope As New TransactionScope()
            Try
                rs = UpdateApprovalGrOld(id)
                If rs.IsSuccess Then
                    rs = UpdateApprovalGrNew(id, appr_gr_new)
                    If rs.IsSuccess Then
                        wa.APPROVAL_GROUP_ID = appr_gr_new.ID
                        wa.ROW_STATUS = status1
                        rs = UpdateWA(id, wa)
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

    Public Shared Function UpdateApprovalGrNew(ByVal wa_id As Decimal, ByRef appr_gr_new As TPROC_APPROVAL_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim app_gr = db.TPROC_APPROVAL_GR.Where(Function(x) x.REV_WA_ID = wa_id And x.ROW_STATUS = ListEnum.RowStat.Edit).FirstOrDefault()
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
                Dim appr_gr_id = db.TPROC_WA.Find(id).APPROVAL_GROUP_ID

                Dim appr_gr = db.TPROC_APPROVAL_GR.Find(appr_gr_id)
                appr_gr.REV_WA_ID = id
                appr_gr.ROW_STATUS = ListEnum.RowStat.InActive
                appr_gr.LAST_MODIFIED_TIME = Date.Now
                appr_gr.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                db.Entry(appr_gr).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function GetApprDtToBe(wa_id As Decimal, row_stat As Integer) As List(Of TPROC_APPROVAL_DT)
        Dim lappr_dt As New List(Of TPROC_APPROVAL_DT)
        Using db As New eProcurementEntities
            Dim appr_gr = db.TPROC_APPROVAL_GR.Where(Function(x) x.REV_WA_ID = wa_id And x.ROW_STATUS = row_stat).FirstOrDefault()
            If appr_gr IsNot Nothing Then
                lappr_dt = db.TPROC_APPROVAL_DT.Where(Function(y) y.APPROVAL_GROUP_ID = appr_gr.ID).ToList()
            End If
        End Using

        Return lappr_dt
    End Function

    Public Shared Function GetApprGrToBe(wa_id As Decimal, row_stat As Integer) As TPROC_APPROVAL_GR
        Dim lappr_gr As New TPROC_APPROVAL_GR
        Using db2 As New eProcurementEntities
            lappr_gr = db2.TPROC_APPROVAL_GR.Where(Function(x) x.REV_WA_ID = wa_id And x.ROW_STATUS = row_stat).FirstOrDefault()
        End Using

        Return lappr_gr
    End Function

End Class
