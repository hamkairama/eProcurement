Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Facade
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports System.Transactions

Namespace Controllers
    Public Class USERController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU17")>
        <CAuthorize(Role:="MNU51")>
        Function Index(flag As Decimal) As ActionResult
            Dim uSER As New List(Of TPROC_USER)
            uSER = db.TPROC_USER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.USER_ID).ToList()

            ViewBag.flag = flag

            Return View(uSER)
        End Function

        <CAuthorize(Role:="MNU17")>
        Function List() As ActionResult
            Dim uSER As New List(Of TPROC_USER)
            uSER = db.TPROC_USER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = ListEnum.RowStat.Edit Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.USER_ID).ToList()

            ViewBag.Message = TempData("result")

            Return PartialView("_List", uSER)
        End Function

        <CAuthorize(Role:="MNU17")>
        Function GetDataByRowStat(row_stat As Integer) As ActionResult
            Dim uSER As New List(Of TPROC_USER)
            If row_stat = ListEnum.RowStat.Live Then
                uSER = db.TPROC_USER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = ListEnum.RowStat.Edit Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.USER_ID).ToList()
            Else
                uSER = db.TPROC_USER.Where(Function(y) y.ROW_STATUS = row_stat).OrderBy(Function(x) x.USER_ID).ToList()
            End If

            Return PartialView("_List", uSER)
        End Function

        <CAuthorize(Role:="MNU17")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim usr As TPROC_USER = db.TPROC_USER.Find(id)
            Return View(usr)
        End Function

        <CAuthorize(Role:="MNU17")>
        <CAuthorize(Role:="MNU51")>
        Function Create(flag As Decimal) As ActionResult
            Session("Active_Directory") = "User"
            Dim wa As New List(Of TPROC_WA)
            wa = db.TPROC_WA.OrderBy(Function(x) x.WA_NUMBER).ToList()

            ViewBag.flag = flag
            ViewBag.Message = TempData("result")

            Return View(wa)
        End Function

        <CAuthorize(Role:="MNU17")>
        Function ActionCreate(ByVal user_id As String, ByVal user_name As String, ByVal user_mail As String, ByVal role_id As Integer, ByVal is_super_admin As Integer, ByVal is_eproc_admin As Integer,
                              ByVal comp_cd As Decimal, ByVal lwa_number As Integer(), ByVal division_id As Decimal, ByVal password As String) As ActionResult
            Dim rs As New ResultStatus
            Dim db As New eProcurementEntities

            Dim user As New TPROC_USER
            user.USER_ID = user_id
            user.USER_MAIL = user_mail
            user.USER_NAME = user_name
            user.PASSWORD = password

            Dim user_dt As New TPROC_USER_DT
            user_dt.ROLE_ID = role_id
            user_dt.DIVISION_ID = division_id
            user_dt.IS_EPROC_ADMIN = is_eproc_admin
            user_dt.IS_SUPER_ADMIN = is_super_admin
            user_dt.COMP_CD = comp_cd


            'Dim usr_profile As New TPROC_USER_PROFILE
            'usr_profile.PHOTO = CDataImage.GetImageByte

            Dim row_state As Integer = ListEnum.RowStat.Live
            rs = UserFacade.ActionSaveUSer(user, user_dt, lwa_number, row_state)


            TempData("result") = rs.MessageText

            Return RedirectToAction("Create", "User", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU17")>
        <CAuthorize(Role:="MNU51")>
        Function Edit(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If
            ViewBag.Division = Dropdown.Division()
            ViewBag.RoleUser = Dropdown.Role()
            Dim usr As TPROC_USER = db.TPROC_USER.Find(id)

            ViewBag.flag = flag
            ViewBag.Message = TempData("result")

            Return View(usr)
        End Function

        <CAuthorize(Role:="MNU17")>
        Function ActionEdit(ByVal id As Decimal, ByVal user_id As String, ByVal user_name As String, ByVal user_mail As String, ByVal is_super_admin As Integer, ByVal is_eproc_admin As Integer,
                            ByVal role_id As Integer, ByVal comp_cd As Decimal, ByVal lwa_number As Integer(), ByVal division_id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Dim db As New eProcurementEntities

            Dim user As New TPROC_USER
            user.USER_ID = user_id
            user.USER_MAIL = user_mail
            user.USER_NAME = user_name
            user.LAST_MODIFIED_BY = Session("USER_ID")
            user.LAST_MODIFIED_TIME = Date.Now
            user.ROW_STATUS = ListEnum.RowStat.Live

            Dim user_dt As New TPROC_USER_DT
            user_dt.ROLE_ID = role_id
            user_dt.DIVISION_ID = division_id
            user_dt.COMP_CD = comp_cd
            user_dt.IS_EPROC_ADMIN = is_eproc_admin
            user_dt.IS_SUPER_ADMIN = is_super_admin

            'Dim usr_profile As New TPROC_USER_PROFILE
            'usr_profile.PHOTO = CDataImage.GetImageByte

            rs = UserFacade.ActionEditUSer(id, user, user_dt, lwa_number)

            If rs.IsSuccess = False Then
                MsgBox(rs.MessageText)
                Return HttpNotFound()
            End If

            TempData("result") = rs.MessageText

            Return RedirectToAction("Edit", "User", New With {.id = id, .flag = 0})
        End Function

        <CAuthorize(Role:="MNU17")>
        <CAuthorize(Role:="MNU51")>
        Function Delete(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim uSER As TPROC_USER = db.TPROC_USER.Find(id)
            If IsNothing(uSER) Then
                Return HttpNotFound()
            End If

            ViewBag.flag = flag
            ViewBag.Message = TempData("result")

            Return View(uSER)
        End Function

        <CAuthorize(Role:="MNU17")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus

            'rs = UserFacade.Delete(id)
            'If rs.IsSuccess = False Then
            '    MsgBox(rs.MessageText)
            '    Return HttpNotFound()
            'End If

            Try
                rs = UserFacade.UpdateRowStatusUser(id, ListEnum.RowStat.InActive)
                rs.SetSuccessStatus("Data has been deleted")
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("result") = rs.MessageText

            Return RedirectToAction("Delete", "User", New With {.id = id, .flag = 0})
        End Function

        <CAuthorize(Role:="MNU17")>
        Function ActionDeleteSelected(ByVal ids As Integer()) As ActionResult
            Dim rs As New ResultStatus

            'rs = UserFacade.DeleteSelected(ids)
            'If rs.IsSuccess = False Then
            '    MsgBox(rs.MessageText)
            '    Return HttpNotFound()
            'End If

            Using scope As New TransactionScope
                Try
                    For Each id In ids
                        Dim user = db.TPROC_USER.Find(id)
                        user.ROW_STATUS = ListEnum.RowStat.InActive
                        user.LAST_MODIFIED_TIME = Date.Now
                        user.LAST_MODIFIED_BY = Session("USER_ID")
                        db.Entry(user).State = EntityState.Modified
                        db.SaveChanges()
                    Next
                    scope.Complete()
                    rs.SetSuccessStatus()
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU17")>
        Function CheckData(ByVal id As Decimal, ByVal user_id As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim uSER As New TPROC_USER
            'check create
            If id = 0 Then
                uSER = db.TPROC_USER.Where(Function(x) x.USER_ID = user_id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If uSER IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                uSER = db.TPROC_USER.Where(Function(x) x.USER_ID = user_id And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If uSER IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        Public Function GetFormData(file As HttpPostedFileBase) As ResultStatus
            Dim rs As ResultStatus
            rs = CDataImage.DataImage(file)
            Return rs
        End Function

        Function DetailProfile() As ActionResult
            If IsNothing(Session("USER_ID")) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim uSer As New TPROC_USER
            uSer = db.TPROC_USER.Where(Function(x) x.USER_ID = Session("USER_ID").ToString() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()

            Return PartialView("_DetailProfile", uSer.TPROC_USER_DT)
        End Function

        'Function SendEmailCreateUser(wa As String) As ActionResult
        '    Dim rs As New ResultStatus

        '    Dim facade As New UserFacade
        '    Using scope As New TransactionScope()
        '        Try
        '            rs = facade.SendEmailCreateUser(wa)
        '            If rs.IsSuccess Then
        '                rs = Generate.CommitGenerator("TPROC_USER_CREATE")
        '                If rs.IsSuccess Then
        '                    scope.Complete()
        '                    rs.SetSuccessStatus()
        '                End If
        '            End If
        '        Catch ex As Exception
        '            rs.SetErrorStatus("failed to send email")
        '        End Try
        '    End Using

        '    Dim user_dt As New TPROC_USER_DT

        '    Return PartialView("_DetailProfile", user_dt)
        'End Function

        <CAuthorize(Role:="MNU17")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_USER.Where(Function(x) x.USER_ID.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU17")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_USER = db.TPROC_USER.Find(id)
                obj.ROW_STATUS = ListEnum.RowStat.Live
                obj.LAST_MODIFIED_TIME = Date.Now
                obj.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(obj).State = EntityState.Modified
                db.SaveChanges()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            Return RedirectToAction("Index", New With {.flag = 1})
        End Function

        <CAuthorize(Role:="MNU17")>
        Function UpdateHibernate() As ActionResult
            Dim rs As New ResultStatus
            Dim uSER As New List(Of TPROC_USER)

            Try
                Dim duration = Today.AddDays(-180)
                uSER = db.TPROC_USER.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).ToList()

                For Each item In uSER
                    If item.LAST_LOGIN <= duration Then
                        item.ROW_STATUS = ListEnum.RowStat.Hibernate
                        db.Entry(item).State = EntityState.Modified
                        db.SaveChanges()
                    End If
                Next
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return RedirectToAction("List", "User", New With {.flag = 0})
        End Function


#Region "REQUEST USER"
        <CAuthorize(Role:="MNU51")>
        Function IndexRequestUser() As ActionResult

            Return View()
        End Function

        <CAuthorize(Role:="MNU51")>
        Function ActionRequestUserCreate(ByVal user_id As String, ByVal user_name As String, ByVal user_mail As String, ByVal is_super_admin As Integer, ByVal is_eproc_admin As Integer, ByVal role_id As Integer,
                              ByVal comp_cd As Decimal, ByVal lwa_number As Integer(), wa As String, ByVal division_id As Decimal, ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim db As New eProcurementEntities
            Dim new_req As New TPROC_REQUEST

            Dim user As New TPROC_USER
            user.USER_ID = user_id
            uSER.USER_MAIL = user_mail
            uSER.USER_NAME = user_name

            Dim user_dt As New TPROC_USER_DT
            user_dt.ROLE_ID = role_id
            user_dt.DIVISION_ID = division_id
            user_dt.COMP_CD = comp_cd
            user_dt.IS_EPROC_ADMIN = is_eproc_admin
            user_dt.IS_SUPER_ADMIN = is_super_admin

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = user_id
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedComplete
            req.DESCRIPTION = desc
            req.CONTROL = "User"
            req.ACTION = "Create"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = Nothing
            req.APPROVAL_EMAIL = Nothing

            Dim emailTo As New ListFieldNameAndValue
            emailTo = RequestFacade.GetEmailEprocStaff()

            Using scope As New TransactionScope()
                Try
                    Dim reqNumber As String = ""
                    Dim row_stat As Integer = ListEnum.RowStat.Create
                    rs = UserFacade.ActionSaveUSer(user, user_dt, lwa_number, row_stat)
                    If rs.IsSuccess Then
                        rs = RequestFacade.SaveRequest(req, new_req)
                        If rs.IsSuccess Then
                            rs = Generate.CommitGenerator("TPROC_REQUEST")
                            If rs.IsSuccess Then
                                rs = RequestFacade.SendEmailRequest(new_req, ListEnum.Request.NeedComplete, emailTo)
                                If rs.IsSuccess Then
                                    scope.Complete()
                                    rs.SetSuccessStatus("Request has been send")
                                End If
                            End If

                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus("failed to send email")
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return RedirectToAction("Create", "User", New With {.flag = 1})
        End Function

        <CAuthorize(Role:="MNU51")>
        Function ActionRequestUserDelete(ByVal id As Decimal, ByVal user_id As String, ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim user As New TPROC_USER
            user.ROW_STATUS = ListEnum.RowStat.Delete
            user.LAST_MODIFIED_TIME = Date.Now
            user.LAST_MODIFIED_BY = Session("USER_ID")

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = user_id
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedComplete
            req.DESCRIPTION = desc
            req.CONTROL = "User"
            req.ACTION = "Delete"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = Nothing
            req.APPROVAL_EMAIL = Nothing

            Dim emailTo As New ListFieldNameAndValue
            emailTo = RequestFacade.GetEmailEprocStaff()


            Using scope As New TransactionScope()
                Try
                    Dim reqNumber As String = ""

                    rs = UserFacade.UpdateRowStatusUser(id, ListEnum.RowStat.Delete)
                    If rs.IsSuccess Then
                        rs = RequestFacade.SaveRequest(req, new_req)
                        If rs.IsSuccess Then
                            rs = Generate.CommitGenerator("TPROC_REQUEST")
                            If rs.IsSuccess Then
                                rs = RequestFacade.SendEmailRequest(new_req, ListEnum.Request.NeedComplete, emailTo)
                                If rs.IsSuccess Then
                                    scope.Complete()
                                    rs.SetSuccessStatus("Request has been send")
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus("failed to send email")
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return RedirectToAction("Delete", "USER", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU51")>
        Function ActionRequestUSerEdit(ByVal id As Decimal, ByVal user_id As String, ByVal user_name As String, ByVal user_mail As String, ByVal is_super_admin As Integer, ByVal is_eproc_admin As Integer,
                            ByVal role_id As Integer, ByVal comp_cd As Decimal, ByVal lwa_number As Integer(), ByVal division_id As Decimal, ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim wa_gr_id As Decimal
            Dim user_dt_id As Decimal
            Dim new_req As New TPROC_REQUEST

            Dim user As New TPROC_USER
            user.USER_ID = user_id
            user.USER_MAIL = user_mail
            user.USER_NAME = user_name
            user.ROW_STATUS = ListEnum.RowStat.Edit

            Dim user_dt As New TPROC_USER_DT
            user_dt.ROLE_ID = role_id
            user_dt.DIVISION_ID = division_id
            user_dt.COMP_CD = comp_cd
            user_dt.IS_EPROC_ADMIN = is_eproc_admin
            user_dt.IS_SUPER_ADMIN = is_super_admin
            user_dt.REV_USER_ID = id
            user_dt.ROW_STATUS = ListEnum.RowStat.Edit

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            Req.RELATION_FLAG = user_id
            Req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            Req.REQUEST_DT = Date.Now
            Req.CREATED_BY = CurrentUser.GetCurrentUserId()
            Req.CREATED_TIME = Date.Now
            Req.STATUS = ListEnum.Request.NeedComplete
            Req.DESCRIPTION = desc
            Req.CONTROL = "User"
            Req.ACTION = "Edit"
            Req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            Req.APPROVAL_BY = Nothing
            Req.APPROVAL_EMAIL = Nothing

            Dim emailTo As New ListFieldNameAndValue
            emailTo = RequestFacade.GetEmailEprocStaff()

            Using scope As New TransactionScope()
                Try
                    rs = UserFacade.UpdateUser(id, user)
                    If rs.IsSuccess Then
                        rs = UserFacade.SaveWAGroup(wa_gr_id)
                        If rs.IsSuccess Then
                            rs = UserFacade.SaveWADetail(wa_gr_id, lwa_number)
                            If rs.IsSuccess Then
                                user_dt.WA_ALLOWED_GR_ID = wa_gr_id
                                rs = UserFacade.SaveUserDetail(user_dt_id, user_dt)
                                If rs.IsSuccess Then
                                    rs = RequestFacade.SaveRequest(req, new_req)
                                    If rs.IsSuccess Then
                                        rs = Generate.CommitGenerator("TPROC_REQUEST")
                                        If rs.IsSuccess Then
                                            rs = RequestFacade.SendEmailRequest(new_req, ListEnum.Request.NeedComplete, emailTo)
                                            If rs.IsSuccess Then
                                                scope.Complete()
                                                rs.SetSuccessStatus("Request has been send")
                                            End If
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


            TempData("result") = rs.MessageText

            Return RedirectToAction("Edit", "USER", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU51")>
        Function RequestApproveComplete(ByVal reqno As String, ByVal rel_flag As String, ByVal control As String, ByVal actions As String, ByVal data_flag As Decimal, ByVal access_from As String) As ActionResult
            Dim request As TPROC_REQUEST = db.TPROC_REQUEST.Where(Function(x) x.REQUEST_NO = reqno).FirstOrDefault()
            ViewBag.ReqNo = reqno
            ViewBag.RequestBy = request.REQUEST_BY
            ViewBag.RequestDt = request.REQUEST_DT
            ViewBag.RequestAction = request.ACTION
            ViewBag.RequestDesc = request.DESCRIPTION

            ViewBag.Division = Dropdown.Division()
            ViewBag.RoleUser = Dropdown.Role()
            ViewBag.ReqNo = reqno
            ViewBag.data_flag = data_flag
            ViewBag.access_from = access_from

            ViewBag.Message = TempData("result")

            Dim usr As TPROC_USER = db.TPROC_USER.Where(Function(x) x.USER_ID = rel_flag).FirstOrDefault()
            Return View(usr)
        End Function

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestUserComplete(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            If actions = ListEnum.RowStat.Create.ToString() Then
                rs = ActionRequestUserCompleteCreate(id, request_no)
            ElseIf actions = ListEnum.RowStat.Edit.ToString() Then
                rs = ActionRequestUserCompleteEdit(id, request_no)
            ElseIf actions = ListEnum.RowStat.Delete.ToString() Then
                rs = ActionRequestUserCompleteDelete(id, request_no)
            End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU51")>
        Function ActionRequestUserCompleteCreate(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UserFacade.UpdateRowStatusUser(id, ListEnum.RowStat.Live)
                    If rs.IsSuccess Then
                        rs = RequestFacade.UpdateRequest(request_no, Session("USER_ID"), ListEnum.Request.NeedComplete, ListEnum.Request.Completed, request)
                        If rs.IsSuccess Then
                            rs = RequestFacade.SendEmailToUserApprovedCompleted(request_no, request, ListEnum.Request.Completed)
                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus("failed to send email")
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return rs
        End Function

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestUserCompleteDelete(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UserFacade.UpdateRowStatusUser(id, ListEnum.RowStat.InActive)
                    If rs.IsSuccess Then
                        rs = RequestFacade.UpdateRequest(request_no, Session("USER_ID"), ListEnum.Request.NeedComplete, ListEnum.Request.Completed, request)
                        If rs.IsSuccess Then
                            rs = RequestFacade.SendEmailToUserApprovedCompleted(request_no, request, ListEnum.Request.Completed)
                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus("failed to send email")
                End Try
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU51")>
        Function ActionRequestUserCompleteEdit(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UserFacade.UpdatesUserByRequest(id, ListEnum.RowStat.Live)
                    If rs.IsSuccess Then
                        rs = RequestFacade.UpdateRequest(request_no, Session("USER_ID"), ListEnum.Request.NeedComplete, ListEnum.Request.Completed, request)
                        If rs.IsSuccess Then
                            rs = RequestFacade.SendEmailToUserApprovedCompleted(request_no, request, ListEnum.Request.Completed)
                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus("failed to send email")
                End Try
            End Using

            Return rs
        End Function

#End Region


    End Class
End Namespace
