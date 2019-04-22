Imports eProcurementApps.Models
Imports System.Transactions
Imports eProcurementApps.Helpers


Public Class RelDeptFacade
    Public Shared Function InsertRDpproval(ByVal lapp_detail As String(), ByVal rel_dept As TPROC_REL_DEPT) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr_id As Decimal
        Using scope As New TransactionScope()
            Try
                rs = InsertApprovalGr(app_gr_id)
                If rs.IsSuccess Then
                    rs = InsertApprovalDt(lapp_detail, app_gr_id)
                    If rs.IsSuccess Then
                        rs = InsertRD(rel_dept, app_gr_id)
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

    Public Shared Function InsertRD(ByVal rel_dept As TPROC_REL_DEPT, ByVal app_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Try
            rel_dept.APPROVAL_RELDEPT_GR_ID = app_gr_id
            Using db As New eProcurementEntities
                db.TPROC_REL_DEPT.Add(rel_dept)
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
                Dim app_dt As New TPROC_APPR_RELDEPT_DT
                app_dt.APPROVAL_RELDEPT_GR_ID = app_gr_id
                app_dt.APPROVAL_NAME = arry(0)
                app_dt.USER_NAME = arry(1)
                app_dt.EMAIL = arry(2)
                app_dt.LEVEL_ID = arry(3)
                app_dt.CREATED_BY = CurrentUser.GetCurrentUserId
                app_dt.CREATED_TIME = Date.Now
                Using db As New eProcurementEntities
                    db.TPROC_APPR_RELDEPT_DT.Add(app_dt)
                    db.SaveChanges()
                End Using
            Next
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function InsertApprovalGr(ByRef app_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr As New TPROC_APPR_RELDEPT_GR
        app_gr.CREATED_TIME = Date.Now
        app_gr.CREATED_BY = CurrentUser.GetCurrentUserId()

        Try
            Using db As New eProcurementEntities
                db.TPROC_APPR_RELDEPT_GR.Add(app_gr)
                db.SaveChanges()
                app_gr_id = db.TPROC_APPR_RELDEPT_GR.Max(Function(x) x.ID)
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function InsertApprovalGr(ByRef app_gr_id As Decimal, ByVal rd_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim app_gr As New TPROC_APPR_RELDEPT_GR
        app_gr.CREATED_TIME = Date.Now
        app_gr.CREATED_BY = CurrentUser.GetCurrentUserId()
        app_gr.REV_RD_ID = rd_id
        app_gr.ROW_STATUS = ListEnum.RowStat.Edit

        Try
            Using db As New eProcurementEntities
                db.TPROC_APPR_RELDEPT_GR.Add(app_gr)
                db.SaveChanges()
                app_gr_id = db.TPROC_APPR_RELDEPT_GR.Max(Function(x) x.ID)
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function DeleteRelDept(rel_dept_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim db As New eProcurementEntities

        Using scope As New TransactionScope()
            Try
                Dim app_gr_id = db.TPROC_REL_DEPT.Find(rel_dept_id).APPROVAL_RELDEPT_GR_ID

                Dim lapproval_dt As List(Of TPROC_APPR_RELDEPT_DT) = db.TPROC_APPR_RELDEPT_DT.Where(Function(x) x.APPROVAL_RELDEPT_GR_ID = app_gr_id).ToList()
                For Each item As TPROC_APPR_RELDEPT_DT In lapproval_dt
                    db.TPROC_APPR_RELDEPT_DT.Remove(item)
                Next

                db.TPROC_APPR_RELDEPT_GR.Remove(db.TPROC_APPR_RELDEPT_GR.Find(app_gr_id))
                db.TPROC_REL_DEPT.Remove(db.TPROC_REL_DEPT.Find(rel_dept_id))
                db.SaveChanges()

                scope.Complete()
            Catch ex As Exception
            End Try
        End Using
        Return rs
    End Function

    Public Shared Function EditRDpproval(ByVal id As Decimal, ByVal lapp_detail As String(), ByVal rel_dept As TPROC_REL_DEPT) As ResultStatus
        Dim rs As New ResultStatus
        Dim appr_Gr_id As Decimal
        Using scope As New TransactionScope()
            Try
                Using db As New eProcurementEntities
                    Dim rd = db.TPROC_REL_DEPT.Find(id)
                    rd.RELATED_DEPARTMENT_NAME = rel_dept.RELATED_DEPARTMENT_NAME
                    rd.LAST_MODIFIED_BY = rel_dept.LAST_MODIFIED_BY
                    rd.LAST_MODIFIED_TIME = rel_dept.LAST_MODIFIED_TIME
                    rd.ROW_STATUS = ListEnum.RowStat.Live
                    db.Entry(rd).State = EntityState.Modified

                    appr_Gr_id = rd.APPROVAL_RELDEPT_GR_ID
                    Dim appr_rd_dt = db.TPROC_APPR_RELDEPT_DT.Where(Function(x) x.APPROVAL_RELDEPT_GR_ID = appr_Gr_id).ToList()
                    If appr_rd_dt.Count > 0 Then
                        For Each item As TPROC_APPR_RELDEPT_DT In appr_rd_dt
                            db.TPROC_APPR_RELDEPT_DT.Remove(item)
                        Next
                    End If

                    db.SaveChanges()

                    rs.SetSuccessStatus()
                End Using

                If rs.IsSuccess Then
                    Using db2 As New eProcurementEntities
                        rs = InsertApprovalDt(lapp_detail, appr_Gr_id)
                    End Using
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

    Public Shared Function UpdateRD(ByVal id As Decimal, ByVal rel_dept As TPROC_REL_DEPT) As ResultStatus
        Dim rs As New ResultStatus
        Dim rd As New TPROC_REL_DEPT

        Try
            Using db As New eProcurementEntities
                rd = db.TPROC_REL_DEPT.Find(id)
                rd.ROW_STATUS = rel_dept.ROW_STATUS
                rd.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
                rd.LAST_MODIFIED_TIME = Date.Now

                'from request edit
                If rel_dept.APPROVAL_RELDEPT_GR_ID <> 0 Then
                    rd.APPROVAL_RELDEPT_GR_ID = rel_dept.APPROVAL_RELDEPT_GR_ID
                End If

                db.Entry(rd).State = EntityState.Modified
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdatesRDByRequest(id As Decimal, status1 As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim rd As New TPROC_REL_DEPT
        Dim appr_gr_new As New TPROC_APPR_RELDEPT_GR


        Using scope As New TransactionScope()
            Try
                rs = UpdateApprovalGrOld(id)
                If rs.IsSuccess Then
                    rs = UpdateApprovalGrNew(id, appr_gr_new)
                    If rs.IsSuccess Then
                        rd.APPROVAL_RELDEPT_GR_ID = appr_gr_new.ID
                        rd.ROW_STATUS = status1
                        rs = UpdateRD(id, rd)
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

    Public Shared Function UpdateApprovalGrNew(ByVal wa_id As Decimal, ByRef appr_gr_new As TPROC_APPR_RELDEPT_GR) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim app_gr = db.TPROC_APPR_RELDEPT_GR.Where(Function(x) x.REV_RD_ID = wa_id And x.ROW_STATUS = ListEnum.RowStat.Edit).FirstOrDefault()
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
                Dim appr_gr_id = db.TPROC_REL_DEPT.Find(id).APPROVAL_RELDEPT_GR_ID

                Dim appr_gr = db.TPROC_APPR_RELDEPT_GR.Find(appr_gr_id)
                appr_gr.REV_RD_ID = id
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

    Public Shared Function GetApprDtToBe(rd_id As Decimal, row_stat As Integer) As List(Of TPROC_APPR_RELDEPT_DT)
        Dim lappr_dt As New List(Of TPROC_APPR_RELDEPT_DT)
        Using db As New eProcurementEntities
            Dim appr_gr = db.TPROC_APPR_RELDEPT_GR.Where(Function(x) x.REV_RD_ID = rd_id And x.ROW_STATUS = row_stat).FirstOrDefault()

            If appr_gr IsNot Nothing Then
                lappr_dt = db.TPROC_APPR_RELDEPT_DT.Where(Function(y) y.APPROVAL_RELDEPT_GR_ID = appr_gr.ID).ToList()
            End If
        End Using

        Return lappr_dt
    End Function

    Public Shared Function GetApprGrToBe(rd_id As Decimal, row_stat As Integer) As TPROC_APPR_RELDEPT_GR
        Dim lappr_gr As New TPROC_APPR_RELDEPT_GR
        Using db2 As New eProcurementEntities
            lappr_gr = db2.TPROC_APPR_RELDEPT_GR.Where(Function(x) x.REV_RD_ID = rd_id And x.ROW_STATUS = row_stat).FirstOrDefault()
        End Using

        Return lappr_gr
    End Function


End Class
