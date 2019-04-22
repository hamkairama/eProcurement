Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports eProcurementApps.Facade
Imports System.Transactions

Namespace Controllers
    Public Class WAController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "SETUP WA"
        <CAuthorize(Role:="MNU19")>
        <CAuthorize(Role:="MNU52")>
        Function Index(flag As Decimal) As ActionResult
            Dim wA As New List(Of TPROC_WA)
            wA = db.TPROC_WA.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.WA_NUMBER).ToList()

            ViewBag.flag = flag

            Return View(wA)
        End Function

        <CAuthorize(Role:="MNU19")>
        <CAuthorize(Role:="MNU52")>
        Function List() As ActionResult
            Dim wA As New List(Of TPROC_WA)
            wA = db.TPROC_WA.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.WA_NUMBER).ToList()

            Return PartialView("_List", wA)
        End Function

        <CAuthorize(Role:="MNU19")>
        <CAuthorize(Role:="MNU52")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim wA As TPROC_WA = db.TPROC_WA.Find(id)
            If IsNothing(wA) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", wA)
        End Function

        <CAuthorize(Role:="MNU19")>
        <CAuthorize(Role:="MNU52")>
        Function Create(flag As Decimal) As ActionResult
            Session("Active_Directory") = "WA"
            ViewBag.Flag = flag

            ViewBag.Message = TempData("result")
            Return View("Create")
        End Function

        <CAuthorize(Role:="MNU19")>
        Function ActionCreate(ByVal wa_number As Decimal, ByVal dept_name As String, ByVal division_id As Decimal, ByVal lapp_detail As String()) As ActionResult
            Dim rs As New ResultStatus
            Dim wA As New TPROC_WA
            wA.WA_NUMBER = wa_number
            wA.CREATED_TIME = Date.Now
            wA.CREATED_BY = Session("USER_ID")
            rs = WAFacade.InsertWApproval(lapp_detail, wA, dept_name, division_id)

            TempData("result") = rs.MessageText

            Return RedirectToAction("Create", "WA", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU19")>
        <CAuthorize(Role:="MNU52")>
        Function Edit(ByVal id As Decimal, ByVal flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim wA As TPROC_WA = db.TPROC_WA.Find(id)
            ViewBag.Division = Dropdown.Division()
            ViewBag.Level = Dropdown.Level()
            Session("Active_Directory") = "WA"
            If IsNothing(wA) Then
                Return HttpNotFound()
            End If

            ViewBag.Flag = flag

            ViewBag.Message = TempData("result")

            Return View("Edit", wA)
        End Function

        <CAuthorize(Role:="MNU19")>
        Function ActionEdit(ByVal id As Decimal, ByVal wa_number As Decimal, ByVal dept_name As String, ByVal division_id As Decimal, ByVal lapp_detail As String()) As ActionResult
            Dim rs As ResultStatus
            Dim wA As New TPROC_WA
            wA.WA_NUMBER = wa_number
            wA.LAST_MODIFIED_TIME = Date.Now
            wA.LAST_MODIFIED_BY = Session("USER_ID")
            rs = WAFacade.EditWApproval(id, lapp_detail, wA, dept_name, division_id)

            TempData("result") = rs.MessageText

            Return RedirectToAction("Edit", "WA", New With {.id = id, .flag = 0})
        End Function

        <CAuthorize(Role:="MNU19")>
        <CAuthorize(Role:="MNU52")>
        Function Delete(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim wA As TPROC_WA = db.TPROC_WA.Find(id)
            If IsNothing(wA) Then
                Return HttpNotFound()
            End If

            ViewBag.flag = flag
            ViewBag.Message = TempData("result")

            Return View("_Delete", wA)
        End Function

        <CAuthorize(Role:="MNU19")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            rs = WAFacade.DeleteWA(id)

            TempData("result") = rs.MessageText

            Return RedirectToAction("Delete", "WA", New With {.id = id, .flag = 0})
        End Function

        <CAuthorize(Role:="MNU19")>
        Function ActionDeleteSelected(ByVal ids As Integer()) As ActionResult
            Dim rs As New ResultStatus

            'rs = WAFacade.DeleteWApprovalSelected(ids)
            'If rs.IsSuccess = False Then
            '    MsgBox(rs.MessageText)
            '    Return HttpNotFound()
            'End If

            Using scope As New TransactionScope
                Try
                    For Each id In ids
                        Dim wa = db.TPROC_WA.Find(id)
                        wa.ROW_STATUS = ListEnum.RowStat.InActive
                        wa.LAST_MODIFIED_TIME = Date.Now
                        wa.LAST_MODIFIED_BY = Session("USER_ID")
                        db.Entry(wa).State = EntityState.Modified
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

        <CAuthorize(Role:="MNU19")>
        Function CheckData(ByVal id As Decimal, ByVal wa_number As Decimal) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim wA As New TPROC_WA
            'check create
            If id = 0 Then
                wA = db.TPROC_WA.Where(Function(x) x.WA_NUMBER = wa_number And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If wA IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                wA = db.TPROC_WA.Where(Function(x) x.WA_NUMBER = wa_number And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If wA IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        Function PopWA(ByVal id As Decimal) As ActionResult
            Dim usr = db.TPROC_USER.Find(id)

            Return PartialView("_PopupWA", usr.TPROC_USER_DT.TPROC_WA_ALLOWED_GR)
        End Function

        <CAuthorize(Role:="MNU19")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_WA.Where(Function(x) x.WA_NUMBER = value And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU19")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Try
                Dim obj As TPROC_WA = db.TPROC_WA.Find(id)
                obj.ROW_STATUS = ListEnum.RowStat.Live
                obj.LAST_MODIFIED_TIME = Date.Now
                obj.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(obj).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus("Data has been re-activated")
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("result") = rs.MessageText

            Return RedirectToAction("Create", "WA", New With {.flag = 0})
        End Function

#End Region


#Region "REQUEST WA"
        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWACreate(ByVal wa_number As Decimal, ByVal dept_name As String, ByVal division_id As Decimal, ByVal lapp_detail As String(), ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim wA As New TPROC_WA
            wA.WA_NUMBER = wa_number
            wA.CREATED_TIME = Date.Now
            wA.CREATED_BY = Session("USER_ID")
            wA.ROW_STATUS = ListEnum.RowStat.Create

            Dim approverHighest = RequestFacade.GetApproverByHighestLevel(lapp_detail)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = wa_number
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "WA"
            req.ACTION = "Create"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = approverHighest.USER_NAME
            req.APPROVAL_EMAIL = approverHighest.EMAIL

            'Dim emailTo As New ListFieldNameAndValue
            'emailTo = RequestFacade.GetEmailEprocStaff()

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", approverHighest.EMAIL)

            Using scope As New TransactionScope()
                Try
                    rs = WAFacade.InsertWApproval(lapp_detail, wA, dept_name, division_id)
                    If rs.IsSuccess Then
                        rs = RequestFacade.SaveRequest(req, new_req)
                        If rs.IsSuccess Then
                            rs = Generate.CommitGenerator("TPROC_REQUEST")
                            If rs.IsSuccess Then
                                rs = RequestFacade.SendEmailRequest(new_req, ListEnum.Request.NeedApprove, emailTo)
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

            Return RedirectToAction("Create", "WA", New With {.flag = 1})
        End Function

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWADelete(ByVal id As Decimal, ByVal wa_number As Decimal, ByVal lapp_detail As String(), ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim wa As New TPROC_WA
            wa.ROW_STATUS = ListEnum.RowStat.Delete
            wa.LAST_MODIFIED_TIME = Date.Now
            wa.LAST_MODIFIED_BY = Session("USER_ID")

            Dim approverHighest = RequestFacade.GetApproverByHighestLevel(lapp_detail)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = wa_number
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "WA"
            req.ACTION = "Delete"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = approverHighest.USER_NAME
            req.APPROVAL_EMAIL = approverHighest.EMAIL

            'Dim emailTo As New ListFieldNameAndValue
            'emailTo = RequestFacade.GetEmailEprocStaff()

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", approverHighest.EMAIL)

            Using scope As New TransactionScope()
                Try
                    rs = WAFacade.UpdateRowStatusWA(id, ListEnum.RowStat.Delete)
                    If rs.IsSuccess Then
                        rs = RequestFacade.SaveRequest(req, new_req)
                        If rs.IsSuccess Then
                            rs = Generate.CommitGenerator("TPROC_REQUEST")
                            If rs.IsSuccess Then
                                rs = RequestFacade.SendEmailRequest(new_req, ListEnum.Request.NeedApprove, emailTo)
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

            Return RedirectToAction("Delete", "WA", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWAEdit(ByVal id As Decimal, ByVal wa_number As Decimal, ByVal dept_name As String, ByVal division_id As Decimal, ByVal lapp_detail As String(), ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST
            Dim app_gr_id As Decimal

            Dim wa As New TPROC_WA
            wa.ROW_STATUS = ListEnum.RowStat.Edit
            wa.LAST_MODIFIED_TIME = Date.Now
            wa.LAST_MODIFIED_BY = Session("USER_ID")

            Dim approverHighest = RequestFacade.GetApproverByHighestLevel(lapp_detail)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = wa_number
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "WA"
            req.ACTION = "Edit"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = approverHighest.USER_NAME
            req.APPROVAL_EMAIL = approverHighest.EMAIL

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", approverHighest.EMAIL)

            Using scope As New TransactionScope()
                Try
                    rs = WAFacade.UpdateWA(id, wa)
                    If rs.IsSuccess Then
                        rs = WAFacade.InsertApprovalGr(app_gr_id, dept_name, division_id, id)
                        If rs.IsSuccess Then
                            rs = WAFacade.InsertApprovalDt(lapp_detail, app_gr_id)
                            If rs.IsSuccess Then
                                rs = RequestFacade.SaveRequest(req, new_req)
                                If rs.IsSuccess Then
                                    rs = Generate.CommitGenerator("TPROC_REQUEST")
                                    If rs.IsSuccess Then
                                        rs = RequestFacade.SendEmailRequest(new_req, ListEnum.Request.NeedApprove, emailTo)
                                        If rs.IsSuccess Then
                                            scope.Complete()
                                            rs.SetSuccessStatus("Request has been send")
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

            Return RedirectToAction("Edit", "WA", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU52")>
        Function RequestApproveComplete(ByVal reqno As String, ByVal rel_flag As String, ByVal control As String, ByVal actions As String, ByVal data_flag As Decimal, ByVal access_from As String) As ActionResult
            Dim request As TPROC_REQUEST = db.TPROC_REQUEST.Where(Function(x) x.REQUEST_NO = reqno).FirstOrDefault()
            ViewBag.ReqNo = reqno
            ViewBag.RequestBy = request.REQUEST_BY
            ViewBag.RequestDt = request.REQUEST_DT
            ViewBag.RequestAction = request.ACTION
            ViewBag.RequestDesc = request.DESCRIPTION

            ViewBag.access_from = access_from

            Dim wA As TPROC_WA = db.TPROC_WA.Where(Function(x) x.WA_NUMBER = rel_flag).FirstOrDefault()
            ViewBag.Division = Dropdown.Division()
            ViewBag.Level = Dropdown.Level()
            Session("Active_Directory") = "WA"

            ViewBag.data_flag = data_flag
            ViewBag.Message = TempData("result")

            Dim app_gr = WAFacade.GetApprGrToBe(wA.ID, CInt(DirectCast([Enum].Parse(GetType(ListEnum.RowStat), actions), ListEnum.RowStat)))
            If app_gr IsNot Nothing Then
                ViewBag.DepartmentTobe = app_gr.DEPARTMENT_NAME
                ViewBag.DivisionTobe = CommonFunction.GetDivisionNameById(app_gr.DIVISION_ID)
            End If

            Return View("RequestApproveComplete", wA)
        End Function

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWAApprove(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            'If actions = ListEnum.RowStat.Edit.ToString() Then
            rs = UpdateRequestWAApprove(id, request_no)
            'End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        Function UpdateRequestWAApprove(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Dim emailTo As New ListFieldNameAndValue
            emailTo = RequestFacade.GetEmailEprocStaff()

            Using scope As New TransactionScope()
                Try
                    rs = RequestFacade.UpdateRequest(request_no, Session("USER_ID"), ListEnum.Request.NeedApprove, ListEnum.Request.NeedComplete, request)
                    If rs.IsSuccess Then
                        rs = RequestFacade.SendEmailToUserApprovedCompleted(request_no, request, ListEnum.Request.Approved)
                        If rs.IsSuccess Then
                            rs = RequestFacade.SendEmailToAdminOrApprover(request, ListEnum.Request.NeedComplete, emailTo)
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

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWAComplete(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            If actions = ListEnum.RowStat.Create.ToString() Then
                rs = ActionRequestWACompleteCreate(id, request_no)
            ElseIf actions = ListEnum.RowStat.Edit.ToString() Then
                rs = ActionRequestWACompleteEdit(id, request_no)
            ElseIf actions = ListEnum.RowStat.Delete.ToString() Then
                rs = ActionRequestWACompleteDelete(id, request_no)
            End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWACompleteCreate(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    'rs = Facade.WAFacade.EditWApproval(id, Session("USER_ID"), lapp_detail, wA)
                    rs = WAFacade.UpdateRowStatusWA(id, ListEnum.RowStat.Live)
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

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWACompleteDelete(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = WAFacade.UpdateRowStatusWA(id, ListEnum.RowStat.InActive)
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

        <CAuthorize(Role:="MNU52")>
        Function ActionRequestWACompleteEdit(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = WAFacade.UpdatesWAByRequest(id, ListEnum.RowStat.Live)
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
