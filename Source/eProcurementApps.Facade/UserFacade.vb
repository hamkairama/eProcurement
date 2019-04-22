Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports System.Transactions
Imports System.Web
Imports System.Configuration

Public Class UserFacade

#Region "OTHER FUNCTION"
    Public Shared Function CekPassword(password As String, password_true As String, password_salt As String, user_id As String) As Boolean
        Dim status As Boolean = False
        Dim passhass As String = Hashing.CreatePasswordAndSaltHash(Hashing.CreatePasswordHash(password), password_salt)
        If passhass = password_true Then
            status = True
        Else
            Using db As New eProcurementEntities()
                Dim user As TPROC_USER = db.TPROC_USER.Where(Function(x) x.USER_ID = user_id).FirstOrDefault()
                db.SaveChanges()
                status = False
            End Using
        End If
        Return status
    End Function

    Public Shared Function GetUserByEmail(email As String) As TPROC_USER
        Dim user As New TPROC_USER()
        Using db As New eProcurementEntities()
            user = db.TPROC_USER.Where(Function(a) a.USER_MAIL = email).FirstOrDefault()
        End Using
        Return user
    End Function

    Public Shared Function GetUserByEmpID(user_id As String) As TPROC_USER
        Dim user As New TPROC_USER()
        Using db As New eProcurementEntities()
            user = db.TPROC_USER.Where(Function(a) a.USER_ID = user_id).FirstOrDefault()
        End Using
        Return user
    End Function
#End Region

#Region "CREATE USER"
    Public Shared Function ActionSaveUSer(user As TPROC_USER, user_dt As TPROC_USER_DT, lwa_number As Integer(), row_stat As Integer) As ResultStatus
        Dim rs As New ResultStatus
        Dim wa_gr_id As Integer
        Dim user_dt_id As Integer

        Using scope As New TransactionScope
            Try
                rs = SaveWAGroup(wa_gr_id)
                If rs.IsSuccess Then
                    rs = SaveWADetail(wa_gr_id, lwa_number)
                    If rs.IsSuccess Then
                        user_dt.WA_ALLOWED_GR_ID = wa_gr_id
                        rs = SaveUserDetail(user_dt_id, user_dt)
                        If rs.IsSuccess Then
                            rs = SaveUser(user, user_dt_id, row_stat)
                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus(user_dt_id)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        'If rs.IsSuccess Then
        '    rs = SaveUserProfile(user_id_id, usr_profile)
        '    If rs.IsSuccess Then
        '        rs.SetSuccessStatus()
        '    Else
        '        rs.SetErrorStatus("User detail has been created, but user profile has been failed")
        '    End If
        'End If

        Return rs
    End Function

    Public Shared Function SaveWAGroup(ByRef id As Integer) As ResultStatus
        Dim rs As New ResultStatus
        Dim waGroup As New TPROC_WA_ALLOWED_GR
        Try
            Using db As New eProcurementEntities
                waGroup.CREATED_BY = CurrentUser.GetCurrentUserId()
                waGroup.CREATED_TIME = Date.Now
                db.TPROC_WA_ALLOWED_GR.Add(waGroup)
                db.SaveChanges()
                id = db.TPROC_WA_ALLOWED_GR.Max(Function(x) x.ID)
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function SaveWADetail(waGroup_id As Integer, lwa_number As Integer()) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope
            Try
                Dim wa As New List(Of TPROC_WA)
                Using db As New eProcurementEntities
                    wa = db.TPROC_WA.OrderBy(Function(x) x.WA_NUMBER).ToList()
                End Using

                Dim lwa = (From c In wa
                           Where lwa_number.Contains(c.WA_NUMBER)
                           Select c.ID)

                For Each item As Integer In lwa
                    Dim wa_detail As New TPROC_WA_ALLOWED_DT
                    wa_detail.WA_ALLOWED_GROUP_ID = waGroup_id
                    wa_detail.WORK_AREA_ID = item
                    wa_detail.CREATED_BY = CurrentUser.GetCurrentUserId()
                    wa_detail.CREATED_TIME = Date.Now
                    Using db As New eProcurementEntities
                        db.TPROC_WA_ALLOWED_DT.Add(wa_detail)
                        db.SaveChanges()
                    End Using
                Next
                scope.Complete()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function SaveUserDetail(ByRef user_dt_id As Decimal, user_dt As TPROC_USER_DT) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                'Dim user_dt As New TPROC_USER_DT
                'user_dt.WA_ALLOWED_GR_ID = wa_gr_id
                'user_dt.ROLE_ID = role_id
                'user_dt.DIVISION_ID = division_id
                'user_dt.COMP_CD = comp_code
                'user_dt.ROW_STATUS = is_request
                'user_dt.REV_USER_ID = user_id
                'user_dt.IS_SUPER_ADMIN = is_super_admin
                'user_dt.IS_EPROC_ADMIN = is_eproc_admin
                user_dt.CREATED_BY = CurrentUser.GetCurrentUserId()
                user_dt.CREATED_TIME = Date.Now
                db.TPROC_USER_DT.Add(user_dt)
                db.SaveChanges()
                user_dt_id = db.TPROC_USER_DT.Max(Function(x) x.ID)
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function SaveUser(user As TPROC_USER, ByVal usr_dt_id As Integer, ByVal row_stat As Integer) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim usr As New TPROC_USER
                usr.CREATED_BY = CurrentUser.GetCurrentUserId()
                usr.USER_ID = user.USER_ID
                usr.USER_NAME = user.USER_NAME
                usr.USER_MAIL = user.USER_MAIL
                usr.PASSWORD = user.PASSWORD
                usr.CREATED_TIME = Date.Now
                usr.ROW_STATUS = row_stat
                usr.USER_DT_ID = usr_dt_id
                db.TPROC_USER.Add(usr)
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function SaveUserProfile(user_id As Decimal, ByVal usr_profile As TPROC_USER_PROFILE) As ResultStatus
        Dim rs As New ResultStatus
        rs.SetSuccessStatus()

        If usr_profile.PHOTO IsNot Nothing Then
            Try
                Using db As New eProcurementEntities
                    usr_profile.USER_ID_ID = user_id
                    usr_profile.CREATED_BY = CurrentUser.GetCurrentUserId()
                    usr_profile.CREATED_TIME = Date.Now
                    db.TPROC_USER_PROFILE.Add(usr_profile)
                    db.SaveChanges()
                End Using
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End If
        CDataImage.CleanDataImage()
        Return rs
    End Function
#End Region

#Region "DELETE USER"
    Public Shared Function Delete(user_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope()
            Try
                Using db As New eProcurementEntities
                    Dim usr = db.TPROC_USER.Find(user_id)
                    Dim wa_group_Id = usr.TPROC_USER_DT.WA_ALLOWED_GR_ID
                    Dim lWa_group_dt As List(Of TPROC_WA_ALLOWED_DT) = db.TPROC_WA_ALLOWED_DT.Where(Function(x) x.WA_ALLOWED_GROUP_ID = wa_group_Id).ToList()
                    For Each item As TPROC_WA_ALLOWED_DT In lWa_group_dt
                        db.TPROC_WA_ALLOWED_DT.Remove(item)
                    Next
                    db.TPROC_WA_ALLOWED_GR.Remove(db.TPROC_WA_ALLOWED_GR.Find(wa_group_Id))

                    Dim usr_dt As TPROC_USER_DT = db.TPROC_USER_DT.Find(usr.USER_DT_ID)
                    db.TPROC_USER_DT.Remove(usr_dt)

                    Dim usr_profile As TPROC_USER_PROFILE = db.TPROC_USER_PROFILE.Where(Function(x) x.USER_ID_ID = user_id).FirstOrDefault()
                    If usr_profile IsNot Nothing Then
                        db.TPROC_USER_PROFILE.Remove(usr_profile)
                    End If

                    'Dim usr_req As TPROC_REQUEST = db.TPROC_REQUEST.Where(Function(x) x.RELATION_ID = user_id).FirstOrDefault()
                    'If usr_req IsNot Nothing Then
                    '    db.TPROC_REQUEST.Remove(usr_req)
                    'End If

                    db.TPROC_USER.Remove(db.TPROC_USER.Find(user_id))

                    db.SaveChanges()
                End Using
                scope.Complete()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function

    Public Shared Function DeleteSelected(ids As Integer()) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope()
            Try
                For Each id In ids
                    rs = Delete(id)
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
#End Region

#Region "EDIT USER"
    Public Shared Function ActionEditUSer(id As Decimal, user As TPROC_USER, user_dt As TPROC_USER_DT, lwa_number As Integer()) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope
            Try
                Dim wa_gr_id As Decimal

                rs = UpdateUser(id, user)
                If rs.IsSuccess Then
                    rs = UpdateUserDetail(id, user_dt, wa_gr_id)
                    If rs.IsSuccess Then
                        rs = DeleteCreateWaAllowedDt(wa_gr_id, lwa_number)
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

    Public Shared Function UpdateUser(id As Decimal, user As TPROC_USER) As ResultStatus
        Dim rs As New ResultStatus
        Try
            'update user
            Using db As New eProcurementEntities
                Dim new_user As New TPROC_USER
                new_user = db.TPROC_USER.Find(id)


                'from request edit
                If user.USER_DT_ID > 0 Then
                    new_user.USER_DT_ID = user.USER_DT_ID
                Else
                    new_user.USER_ID = user.USER_ID
                    new_user.USER_MAIL = user.USER_MAIL
                    new_user.USER_NAME = user.USER_NAME
                End If

                new_user.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId
                new_user.LAST_MODIFIED_TIME = Date.Now
                new_user.ROW_STATUS = user.ROW_STATUS

                db.Entry(new_user).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Private Shared Function UpdateUserDetail(user_id_id As Decimal, user_dt As TPROC_USER_DT, ByRef wa_gr_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim usr_dt As New TPROC_USER_DT

        Try
            'update user detail
            Using db As New eProcurementEntities
                'Dim user_dt = db.TPROC_USER_DT.Where(Function(x) x.USER_ID_ID = user_id_id).ToList()
                Dim usr = db.TPROC_USER.Find(user_id_id)

                'If user_dt.Count > 0 Then
                'For Each item In user_dt
                usr.TPROC_USER_DT.ROLE_ID = user_dt.ROLE_ID
                usr.TPROC_USER_DT.DIVISION_ID = user_dt.DIVISION_ID
                usr.TPROC_USER_DT.COMP_CD = user_dt.COMP_CD
                usr.TPROC_USER_DT.ROW_STATUS = ListEnum.RowStat.Live
                usr.TPROC_USER_DT.IS_SUPER_ADMIN = user_dt.IS_SUPER_ADMIN
                usr.TPROC_USER_DT.IS_EPROC_ADMIN = user_dt.IS_EPROC_ADMIN
                'Using db2 As New eProcurementEntities
                db.Entry(usr).State = EntityState.Modified
                db.SaveChanges()
                'End Using
                'Next

                'get wa allow group id
                wa_gr_id = usr.TPROC_USER_DT.TPROC_WA_ALLOWED_GR.ID

                rs.SetSuccessStatus()
                'End If

            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Private Shared Function DeleteCreateWaAllowedDt(wa_gr_id As Decimal, lwa_number As Integer()) As ResultStatus
        Dim rs As New ResultStatus

        Try
            'delete wa allowed detail base on wa allow group id
            Using db As New eProcurementEntities
                Dim wa_allowed_dt As List(Of TPROC_WA_ALLOWED_DT) = db.TPROC_WA_ALLOWED_DT.Where(Function(x) x.WA_ALLOWED_GROUP_ID = wa_gr_id).ToList()

                If wa_allowed_dt.Count > 0 Then
                    For Each item In wa_allowed_dt
                        db.TPROC_WA_ALLOWED_DT.Remove(item)
                    Next
                    db.SaveChanges()
                    rs.SetSuccessStatus()
                End If
            End Using

            If rs.IsSuccess Then
                'create wa allowed detail base on wa allow group id
                rs = SaveWADetail(wa_gr_id, lwa_number)
            End If
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function ActionEditRequestUSer(id As Decimal, user As TPROC_USER, user_dt As TPROC_USER_DT, lwa_number As Integer()) As ResultStatus
        Dim rs As New ResultStatus

        Using scope As New TransactionScope
            Try
                Dim wa_gr_id As Decimal

                rs = UpdateUserDetail(id, user_dt, wa_gr_id)
                If rs.IsSuccess Then
                    rs = DeleteCreateWaAllowedDt(wa_gr_id, lwa_number)
                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try
        End Using

        Return rs
    End Function


#End Region

    Public Shared Function UpdateRowStatusUser(id As Decimal, rowStat As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim user As New TPROC_USER

        Try
            Using db As New eProcurementEntities
                user = db.TPROC_USER.Find(id)
                user.ROW_STATUS = rowStat
                user.LAST_MODIFIED_TIME = Date.Now
                user.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(user).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdatesUserByRequest(id As Decimal, status1 As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim usr As New TPROC_USER
        Dim user_dt_new As New TPROC_USER_DT


        Using scope As New TransactionScope()
            Try
                rs = UpdateUserDtOld(id)
                If rs.IsSuccess Then
                    rs = UpdateUserDtNew(id, user_dt_new)
                    If rs.IsSuccess Then
                        usr.USER_DT_ID = user_dt_new.ID
                        usr.ROW_STATUS = status1
                        rs = UpdateUser(id, usr)
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

    Public Shared Function UpdateUserDtNew(ByVal user_id As Decimal, ByRef user_dt_new As TPROC_USER_DT) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim user_dt = db.TPROC_USER_DT.Where(Function(x) x.REV_USER_ID = user_id And x.ROW_STATUS = ListEnum.RowStat.Edit).FirstOrDefault()
                user_dt.ROW_STATUS = ListEnum.RowStat.Live
                user_dt.LAST_MODIFIED_TIME = Date.Now
                user_dt.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                user_dt_new = user_dt

                db.Entry(user_dt).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateUserDtOld(ByVal user_id As Decimal) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim usr = db.TPROC_USER.Find(user_id)

                usr.TPROC_USER_DT.REV_USER_ID = user_id
                usr.TPROC_USER_DT.ROW_STATUS = ListEnum.RowStat.InActive
                usr.TPROC_USER_DT.LAST_MODIFIED_TIME = Date.Now
                usr.TPROC_USER_DT.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

                db.Entry(usr.TPROC_USER_DT).State = EntityState.Modified
                db.SaveChanges()
            End Using

            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function GetUserDtToBe(user_id As Decimal, row_stat As Integer) As TPROC_USER_DT
        Dim lappr_dt As New TPROC_USER_DT
        Dim db As New eProcurementEntities
        lappr_dt = db.TPROC_USER_DT.Where(Function(x) x.REV_USER_ID = user_id And x.ROW_STATUS = row_stat).FirstOrDefault()

        Return lappr_dt
    End Function

End Class