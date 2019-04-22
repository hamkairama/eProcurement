Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports eProcurementApps.Facade
Imports System.Transactions

Namespace Controllers
    Public Class RELATED_DEPARTMENTController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "SETUP RD"
        <CAuthorize(Role:="MNU05")>
        <CAuthorize(Role:="MNU53")>
        Function Index(flag As Decimal) As ActionResult
            Dim rELATED_DEPARTMENT As New List(Of TPROC_REL_DEPT)
            rELATED_DEPARTMENT = db.TPROC_REL_DEPT.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.RELATED_DEPARTMENT_NAME).ToList()

            ViewBag.flag = flag
            Return View(rELATED_DEPARTMENT)
        End Function

        <CAuthorize(Role:="MNU05")>
        <CAuthorize(Role:="MNU53")>
        Function List() As ActionResult
            Dim rELATED_DEPARTMENT As New List(Of TPROC_REL_DEPT)
            rELATED_DEPARTMENT = db.TPROC_REL_DEPT.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.RELATED_DEPARTMENT_NAME).ToList()

            Return PartialView("_List", rELATED_DEPARTMENT)
        End Function

        <CAuthorize(Role:="MNU05")>
        <CAuthorize(Role:="MNU53")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim rELATED_DEPARTMENT As TPROC_REL_DEPT = db.TPROC_REL_DEPT.Find(id)
            If IsNothing(rELATED_DEPARTMENT) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", rELATED_DEPARTMENT)
        End Function

        <CAuthorize(Role:="MNU05")>
        <CAuthorize(Role:="MNU53")>
        Function Create(flag As Decimal) As ActionResult
            Session("Active_Directory") = "RelDept"

            ViewBag.flag = flag

            ViewBag.Message = TempData("result")
            Return View()
        End Function

        <CAuthorize(Role:="MNU05")>
        Function ActionCreate(ByVal related_department_name As String, ByVal lapp_detail As String()) As ActionResult
            Dim rs As New ResultStatus
            Dim rELATED_DEPARTMENT As New TPROC_REL_DEPT

            rELATED_DEPARTMENT.RELATED_DEPARTMENT_NAME = related_department_name
            rELATED_DEPARTMENT.CREATED_TIME = Date.Now
            rELATED_DEPARTMENT.CREATED_BY = Session("USER_ID")
            rs = RelDeptFacade.InsertRDpproval(lapp_detail, rELATED_DEPARTMENT)

            TempData("result") = rs.MessageText

            Return RedirectToAction("Index", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU05")>
        <CAuthorize(Role:="MNU53")>
        Function Edit(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim rELATED_DEPARTMENT As TPROC_REL_DEPT = db.TPROC_REL_DEPT.Find(id)
            Session("Active_Directory") = "RelDept"
            If IsNothing(rELATED_DEPARTMENT) Then
                Return HttpNotFound()
            End If

            ViewBag.flag = flag
            ViewBag.Message = TempData("result")

            Return View("_Edit", rELATED_DEPARTMENT)
        End Function

        <CAuthorize(Role:="MNU05")>
        Function ActionEdit(ByVal id As Decimal, ByVal related_department_name As String, ByVal lapp_detail As String()) As ActionResult
            Dim rELATED_DEPARTMENT As New TPROC_REL_DEPT
            rELATED_DEPARTMENT.RELATED_DEPARTMENT_NAME = related_department_name
            rELATED_DEPARTMENT.LAST_MODIFIED_TIME = Date.Now
            rELATED_DEPARTMENT.LAST_MODIFIED_BY = Session("USER_ID")
            RelDeptFacade.EditRDpproval(id, lapp_detail, rELATED_DEPARTMENT)

            Return RedirectToAction("Index", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU05")>
        <CAuthorize(Role:="MNU53")>
        Function Delete(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim rELATED_DEPARTMENT As TPROC_REL_DEPT = db.TPROC_REL_DEPT.Find(id)
            If IsNothing(rELATED_DEPARTMENT) Then
                Return HttpNotFound()
            End If

            ViewBag.flag = flag
            Return View("_Delete", rELATED_DEPARTMENT)
        End Function

        <CAuthorize(Role:="MNU05")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            'rs = RelDeptFacade.DeleteRelDept(id)
            Dim rd = db.TPROC_REL_DEPT.Find(id)
            rd.ROW_STATUS = ListEnum.RowStat.InActive
            rd.LAST_MODIFIED_TIME = Date.Now
            rd.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(rd).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index", New With {.flag = 0})
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU05")>
        Function CheckData(ByVal id As Decimal, ByVal related_department_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim rELATED_DEPARTMENT As New TPROC_REL_DEPT
            'check create
            If id = 0 Then
                rELATED_DEPARTMENT = db.TPROC_REL_DEPT.Where(Function(x) x.RELATED_DEPARTMENT_NAME.ToUpper() = related_department_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If rELATED_DEPARTMENT IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                rELATED_DEPARTMENT = db.TPROC_REL_DEPT.Where(Function(x) x.RELATED_DEPARTMENT_NAME.ToUpper() = related_department_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If rELATED_DEPARTMENT IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU05")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_REL_DEPT.Where(Function(x) x.RELATED_DEPARTMENT_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU05")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_REL_DEPT = db.TPROC_REL_DEPT.Find(id)
                obj.ROW_STATUS = ListEnum.RowStat.Live
                obj.LAST_MODIFIED_TIME = Date.Now
                obj.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(obj).State = EntityState.Modified
                db.SaveChanges()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


            Return RedirectToAction("Index")
        End Function
#End Region

#Region "REQUEST RD"

        <CAuthorize(Role:="MNU53")>
        Function ActionRequestRDCreate(ByVal related_department_name As String, ByVal lapp_detail As String(), ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim rd As New TPROC_REL_DEPT
            rd.RELATED_DEPARTMENT_NAME = related_department_name
            rd.CREATED_TIME = Date.Now
            rd.CREATED_BY = Session("USER_ID")
            rd.ROW_STATUS = ListEnum.RowStat.Create

            Dim approverHighest = RequestFacade.GetApproverByHighestLevel(lapp_detail)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = related_department_name
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "RELATED_DEPARTMENT"
            req.ACTION = "Create"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = approverHighest.USER_NAME
            req.APPROVAL_EMAIL = approverHighest.EMAIL

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", approverHighest.EMAIL)

            Using scope As New TransactionScope()
                Try
                    Dim reqNumber As String = ""
                    rs = RelDeptFacade.InsertRDpproval(lapp_detail, rd)
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

            Return RedirectToAction("Create", "RELATED_DEPARTMENT", New With {.flag = 1})
        End Function

        <CAuthorize(Role:="MNU53")>
        Function ActionRequestRDDelete(ByVal id As Decimal, related_department_name As String, ByVal lapp_detail As String(), ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim rel_dept As New TPROC_REL_DEPT
            rel_dept.ROW_STATUS = ListEnum.RowStat.Delete
            rel_dept.LAST_MODIFIED_TIME = Date.Now
            rel_dept.LAST_MODIFIED_BY = Session("USER_ID")

            Dim approverHighest = RequestFacade.GetApproverByHighestLevel(lapp_detail)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = related_department_name
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "RELATED_DEPARTMENT"
            req.ACTION = "Delete"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = approverHighest.USER_NAME
            req.APPROVAL_EMAIL = approverHighest.EMAIL

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", approverHighest.EMAIL)

            Using scope As New TransactionScope()
                Try
                    rs = RelDeptFacade.UpdateRD(id, rel_dept)
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

            Return RedirectToAction("Delete", "RELATED_DEPARTMENT", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU53")>
        Function ActionRequestRDEdit(ByVal id As Decimal, ByVal related_department_name As String, ByVal lapp_detail As String(), ByVal desc As String) As ActionResult
            Dim rs As New ResultStatus
            Dim app_gr_id As Decimal
            Dim new_req As New TPROC_REQUEST

            Dim rd As New TPROC_REL_DEPT
            rd.ROW_STATUS = ListEnum.RowStat.Edit
            rd.LAST_MODIFIED_TIME = Date.Now
            rd.LAST_MODIFIED_BY = Session("USER_ID")

            Dim approverHighest = RequestFacade.GetApproverByHighestLevel(lapp_detail)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = related_department_name
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "RELATED_DEPARTMENT"
            req.ACTION = "Edit"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = approverHighest.USER_NAME
            req.APPROVAL_EMAIL = approverHighest.EMAIL

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", approverHighest.EMAIL)

            Using scope As New TransactionScope()
                Try
                    rs = RelDeptFacade.UpdateRD(id, rd)
                    If rs.IsSuccess Then
                        rs = RelDeptFacade.InsertApprovalGr(app_gr_id, id)
                        If rs.IsSuccess Then
                            rs = RelDeptFacade.InsertApprovalDt(lapp_detail, app_gr_id)
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

            Return RedirectToAction("Edit", "RELATED_DEPARTMENT", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU53")>
        Function RequestApproveComplete(ByVal reqno As String, ByVal rel_flag As String, ByVal control As String, ByVal actions As String, ByVal data_flag As Decimal, ByVal access_from As String) As ActionResult
            Dim request As TPROC_REQUEST = db.TPROC_REQUEST.Where(Function(x) x.REQUEST_NO = reqno).FirstOrDefault()
            ViewBag.ReqNo = reqno
            ViewBag.RequestBy = request.REQUEST_BY
            ViewBag.RequestDt = request.REQUEST_DT
            ViewBag.RequestAction = request.ACTION
            ViewBag.RequestDesc = request.DESCRIPTION

            ViewBag.access_from = access_from

            Dim rd As TPROC_REL_DEPT = db.TPROC_REL_DEPT.Where(Function(x) x.RELATED_DEPARTMENT_NAME = rel_flag).FirstOrDefault()
            Session("Active_Directory") = "RelDept"
            ViewBag.ReqNo = reqno
            ViewBag.data_flag = data_flag

            ViewBag.Message = TempData("result")

            Return View("RequestApproveComplete", rd)
        End Function

        <CAuthorize(Role:="MNU53")>
        Function ActionRequestRDComplete(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            If actions = ListEnum.RowStat.Create.ToString() Then
                rs = ActionRequestRDCompleteCreate(id, request_no)
            ElseIf actions = ListEnum.RowStat.Edit.ToString() Then
                rs = ActionRequestRDCompleteEdit(id, request_no)
            ElseIf actions = ListEnum.RowStat.Delete.ToString() Then
                rs = ActionRequestRDCompleteDelete(id, request_no)
            End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU53")>
        Function ActionRequestRDApprove(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            'If actions = ListEnum.RowStat.Edit.ToString() Then
            rs = UpdateRequestRDApprove(id, request_no)
            'End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        Function UpdateRequestRDApprove(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim uSER As New TPROC_USER
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

        Function UpdateRowStatusRD(id As Decimal, rowStat As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim rd As New TPROC_REL_DEPT

            Try
                rd = db.TPROC_REL_DEPT.Find(id)
                rd.ROW_STATUS = rowStat
                rd.LAST_MODIFIED_TIME = Date.Now
                rd.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(rd).State = EntityState.Modified
                db.SaveChanges()

                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try


            Return rs
        End Function

        Function ActionRequestRDCompleteCreate(id As Decimal, request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UpdateRowStatusRD(id, ListEnum.RowStat.Live)
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

        Function ActionRequestRDCompleteDelete(id As Decimal, request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UpdateRowStatusRD(id, ListEnum.RowStat.InActive)
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

        <CAuthorize(Role:="MNU53")>
        Function ActionRequestRDCompleteEdit(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = RelDeptFacade.UpdatesRDByRequest(id, ListEnum.RowStat.Live)
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
