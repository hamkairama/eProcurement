Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports OfficeOpenXml
Imports System.Transactions
Imports eProcurementApps.Facade

Namespace Controllers
    Public Class CHART_OF_ACCOUNTController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "SETUP COA"
        <CAuthorize(Role:="MNU23")>
        <CAuthorize(Role:="MNU54")>
        Function Index(flag As Decimal) As ActionResult
            Dim cOA As New List(Of TPROC_CHART_OF_ACCOUNTS)
            cOA = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = ListEnum.RowStat.Edit Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM).ToList()

            ViewBag.Message = TempData("msg")
            ViewBag.flag = flag

            Return View(cOA)
        End Function

        <CAuthorize(Role:="MNU23")>
        <CAuthorize(Role:="MNU54")>
        Function List() As ActionResult
            Dim cOA As New List(Of TPROC_CHART_OF_ACCOUNTS)
            cOA = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = ListEnum.RowStat.Edit Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM).ToList()

            Return PartialView("_List", cOA)
        End Function

        <CAuthorize(Role:="MNU23")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cOA As TPROC_CHART_OF_ACCOUNTS = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
            If IsNothing(cOA) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", cOA)
        End Function

        <CAuthorize(Role:="MNU23")>
        Function Create(flag As Decimal) As ActionResult
            'ViewBag.Curr = CommonFunction.GetCurrency()
            Session("Active_Directory") = "Request"
            ViewBag.Flag = flag
            ViewBag.Message = TempData("result")

            Return View("_Create")
        End Function

        <CAuthorize(Role:="MNU23")>
        Function ActionCreate(ByVal crcy_cd As String, ByVal acct_num As String, ByVal acct_desc As String, ByVal converted_acct_num As String) As ActionResult
            Dim rs As New ResultStatus

            Dim coa_gr As New TPROC_CHART_OF_ACCOUNT_GR
            coa_gr.CRCY_CD = crcy_cd
            coa_gr.ACCT_NUM = acct_num
            coa_gr.ACCT_DESC = acct_desc
            coa_gr.CREATED_TIME = Date.Now
            coa_gr.CREATED_BY = CurrentUser.GetCurrentUserId()
            coa_gr.CONVERTED_ACCT_NUM = converted_acct_num

            Dim coa As New TPROC_CHART_OF_ACCOUNTS
            coa.ROW_STATUS = ListEnum.RowStat.Live

            rs = Facade.COAFacade.InsertCOApproval(coa_gr, coa)

            TempData("result") = rs.MessageText

            Return RedirectToAction("Index", New With {.flag = 0})

        End Function

        <CAuthorize(Role:="MNU23")>
        Function Edit(ByVal id As Decimal, ByVal flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cOA As TPROC_CHART_OF_ACCOUNTS = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
            ViewBag.Division = Dropdown.Division()
            ViewBag.Level = Dropdown.Level()
            If IsNothing(cOA) Then
                Return HttpNotFound()
            End If

            ViewBag.Flag = flag

            'set active directory for request param
            Session("Active_Directory") = "Request"
            ViewBag.Message = TempData("result")

            Return View("_Edit", cOA)
        End Function

        <CAuthorize(Role:="MNU23")>
        Function ActionEdit(ByVal id As Decimal, ByVal crcy_cd As String, ByVal acct_num As String, ByVal acct_desc As String, ByVal converted_acct_num As String) As ActionResult
            Dim rs As New ResultStatus

            Dim coa_gr As New TPROC_CHART_OF_ACCOUNT_GR
            coa_gr.CRCY_CD = crcy_cd
            coa_gr.ACCT_NUM = acct_num
            coa_gr.ACCT_DESC = acct_desc
            coa_gr.CONVERTED_ACCT_NUM = converted_acct_num

            rs = COAFacade.UpdateCOA(id, coa_gr)

            Return RedirectToAction("Index", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU23")>
        Function Delete(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cOA As TPROC_CHART_OF_ACCOUNTS = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
            If IsNothing(cOA) Then
                Return HttpNotFound()
            End If

            Session("Active_Directory") = "Request"

            ViewBag.flag = flag
            ViewBag.Message = TempData("result")

            Return View("_Delete", cOA)
        End Function

        <CAuthorize(Role:="MNU23")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus

            Try
                Dim coa = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
                coa.ROW_STATUS = ListEnum.RowStat.InActive
                coa.LAST_MODIFIED_TIME = Date.Now
                coa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(coa).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus("Data has been deleted")
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("result") = rs.MessageText

            Return RedirectToAction("Index", New With {.flag = 0})

        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU23")>
        Function CheckData(ByVal id As Decimal, ByVal acct_num As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim cOA As New TPROC_CHART_OF_ACCOUNTS
            'check create
            If id = 0 Then
                cOA = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC.ToUpper() = acct_num.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If cOA IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                cOA = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC.ToUpper() = acct_num.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If cOA IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU23")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM.ToUpper() = value.ToUpper() And x.ROW_STATUS = -1).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU23")>
        Function ActionActiviting(ByVal id As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Try
                Dim obj As TPROC_CHART_OF_ACCOUNTS = db.TPROC_CHART_OF_ACCOUNTS.Find(id)
                obj.ROW_STATUS = ListEnum.RowStat.Live
                obj.LAST_MODIFIED_TIME = Date.Now
                obj.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()
                db.Entry(obj).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                MsgBox(ex.Message)
                rs.SetSuccessStatus(ex.Message)
            End Try


            Return rs
        End Function

        <CAuthorize(Role:="MNU23")>
        Public Function Upload(formCollection As FormCollection) As ActionResult
            Dim sb As New StringBuilder

            Try
                If Request IsNot Nothing Then
                    Dim file As HttpPostedFileBase = Request.Files("UploadedFile")
                    If (file IsNot Nothing) AndAlso (file.ContentLength > 0) AndAlso Not String.IsNullOrEmpty(file.FileName) Then
                        Dim fileName As String = file.FileName
                        Dim fileContentType As String = file.ContentType
                        Dim fileBytes As Byte() = New Byte(file.ContentLength - 1) {}
                        Dim data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength))

                        Using package = New ExcelPackage(file.InputStream)
                            Dim currentSheet = package.Workbook.Worksheets
                            Dim workSheet = currentSheet.First()
                            Dim noOfCol = workSheet.Dimension.[End].Column
                            Dim noOfRow = workSheet.Dimension.[End].Row

                            For rowIterator As Integer = 2 To noOfRow
                                Dim acct_num As String = workSheet.Cells(rowIterator, 2).Value.ToString()
                                Dim isExist = CheckData(0, acct_num)

                                If isExist = 1 Then
                                    sb.Append("Account " + acct_num + " already exist." + "<br />")
                                    sb.AppendLine()
                                Else
                                    Dim coa As New TPROC_CHART_OF_ACCOUNTS
                                    coa.TPROC_CHART_OF_ACCOUNT_GR.CRCY_CD = workSheet.Cells(rowIterator, 1).Value.ToString()
                                    coa.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM = workSheet.Cells(rowIterator, 2).Value.ToString()
                                    coa.TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC = workSheet.Cells(rowIterator, 3).Value.ToString()
                                    coa.CREATED_TIME = Date.Now
                                    coa.CREATED_BY = CurrentUser.GetCurrentUserId()
                                    Using db2 As New eProcurementEntities
                                        db2.TPROC_CHART_OF_ACCOUNTS.Add(coa)
                                        db2.SaveChanges()
                                    End Using
                                End If
                            Next
                        End Using
                    Else
                        sb.Append("Please select the file before")
                    End If
                End If
            Catch ex As Exception
                sb.Append(ex.Message + " please check format and available of value" + "<br />")
            End Try

            TempData("msg") = sb.ToString()

            Return RedirectToAction("Index", New With {.flag = 0})
        End Function
#End Region

#Region "REQUEST COA"
        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOACreate(ByVal crcy_cd As String, ByVal acct_num As String, ByVal acct_desc As String, ByVal desc As String, ByVal appr_nm As String, ByVal appr_email As String, ByVal converted_acct_num As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim COA_GR As New TPROC_CHART_OF_ACCOUNT_GR
            COA_GR.CRCY_CD = crcy_cd
            COA_GR.ACCT_NUM = acct_num
            COA_GR.ACCT_DESC = acct_desc
            COA_GR.CREATED_TIME = Date.Now
            COA_GR.CREATED_BY = CurrentUser.GetCurrentUserId()
            COA_GR.ROW_STATUS = ListEnum.RowStat.Create
            COA_GR.CONVERTED_ACCT_NUM = converted_acct_num

            Dim coa As New TPROC_CHART_OF_ACCOUNTS
            coa.ROW_STATUS = ListEnum.RowStat.Create

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = acct_num
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "CHART_OF_ACCOUNT"
            req.ACTION = "Create"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = appr_nm
            req.APPROVAL_EMAIL = appr_email

            'Dim emailTo As New ListFieldNameAndValue
            'emailTo = RequestFacade.GetEmailEprocStaff()

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", appr_email)

            Using scope As New TransactionScope()
                Try
                    Dim reqNumber As String = ""
                    rs = COAFacade.InsertCOApproval(COA_GR, coa)

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

            Return RedirectToAction("Create", "CHART_OF_ACCOUNT", New With {.flag = 1})
        End Function

        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOADelete(ByVal id As Decimal, ByVal acct_num As Decimal, ByVal desc As String, ByVal appr_nm As String, ByVal appr_email As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim coa As New TPROC_CHART_OF_ACCOUNTS
            coa.ROW_STATUS = ListEnum.RowStat.Delete
            coa.LAST_MODIFIED_TIME = Date.Now
            coa.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId()

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = acct_num
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "CHART_OF_ACCOUNT"
            req.ACTION = "Delete"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = appr_nm
            req.APPROVAL_EMAIL = appr_email

            'Dim emailTo As New ListFieldNameAndValue
            'emailTo = RequestFacade.GetEmailEprocStaff()

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", appr_email)

            Using scope As New TransactionScope()
                Try
                    rs = COAFacade.UpdateCOA(id, coa)
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

            Return RedirectToAction("Delete", "CHART_OF_ACCOUNT", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOAEdit(ByVal id As Decimal, ByVal crcy_cd As String, ByVal acct_num As String, ByVal acct_desc As String, ByVal desc As String, ByVal appr_nm As String, ByVal appr_email As String, ByVal converted_acct_num As String) As ActionResult
            Dim rs As New ResultStatus
            Dim coa_gr_id As Decimal
            Dim new_req As New TPROC_REQUEST

            Dim coa As New TPROC_CHART_OF_ACCOUNTS
            coa.ROW_STATUS = ListEnum.RowStat.Edit
            coa.LAST_MODIFIED_TIME = Date.Now
            coa.LAST_MODIFIED_BY = Session("USER_ID")

            Dim coa_gr As New TPROC_CHART_OF_ACCOUNT_GR
            coa_gr.CRCY_CD = crcy_cd
            coa_gr.ACCT_NUM = acct_num
            coa_gr.ACCT_DESC = acct_desc
            coa_gr.CREATED_BY = Session("USER_ID")
            coa_gr.CREATED_TIME = Date.Now
            coa_gr.REV_COA_ID = id
            coa_gr.ROW_STATUS = ListEnum.RowStat.Edit
            coa_gr.CONVERTED_ACCT_NUM = converted_acct_num

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = db.TPROC_CHART_OF_ACCOUNTS.Find(id).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "CHART_OF_ACCOUNT"
            req.ACTION = "Edit"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = appr_nm
            req.APPROVAL_EMAIL = appr_email

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", appr_email)

            Using scope As New TransactionScope()
                Try
                    rs = COAFacade.UpdateCOA(id, coa)
                    If rs.IsSuccess Then
                        rs = COAFacade.InsertCOAGr(coa_gr_id, coa_gr)
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
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return RedirectToAction("Edit", "CHART_OF_ACCOUNT", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU54")>
        Function RequestApproveComplete(ByVal reqno As String, ByVal rel_flag As String, ByVal control As String, ByVal actions As String, ByVal data_flag As Decimal, ByVal access_from As String) As ActionResult
            Dim request As TPROC_REQUEST = db.TPROC_REQUEST.Where(Function(x) x.REQUEST_NO = reqno).FirstOrDefault()
            ViewBag.ReqNo = reqno
            ViewBag.RequestBy = request.REQUEST_BY
            ViewBag.RequestDt = request.REQUEST_DT
            ViewBag.RequestAction = request.ACTION
            ViewBag.RequestDesc = request.DESCRIPTION

            ViewBag.access_from = access_from

            Dim coa As New TPROC_CHART_OF_ACCOUNTS
            coa = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM = rel_flag).FirstOrDefault()

            ViewBag.data_flag = data_flag
            ViewBag.Message = TempData("result")

            Return View("RequestApproveComplete", coa)
        End Function

        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOAComplete(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            If actions = ListEnum.RowStat.Create.ToString() Then
                rs = ActionRequestCOACompleteCreate(id, request_no)
            ElseIf actions = ListEnum.RowStat.Edit.ToString() Then
                rs = ActionRequestCOACompleteEdit(id, request_no)
            ElseIf actions = ListEnum.RowStat.Delete.ToString() Then
                rs = ActionRequestCOACompleteDelete(id, request_no)
            End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOACompleteCreate(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim uSER As New TPROC_USER
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = COAFacade.UpdateRowStatusCOA(id, ListEnum.RowStat.Live)

                    If rs.IsSuccess Then
                        rs = RequestFacade.UpdateRequest(request_no, CurrentUser.GetCurrentUserId(), ListEnum.Request.NeedComplete, ListEnum.Request.Completed, request)
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

        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOACompleteDelete(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = COAFacade.UpdateRowStatusCOA(id, ListEnum.RowStat.InActive)
                    If rs.IsSuccess Then
                        rs = RequestFacade.UpdateRequest(request_no, CurrentUser.GetCurrentUserId(), ListEnum.Request.NeedComplete, ListEnum.Request.Completed, request)
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

        <CAuthorize(Role:="MNU54")>
        Function ActionRequestCOACompleteEdit(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = COAFacade.UpdatesCOAByRequest(id, ListEnum.RowStat.Live)
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

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestCOAApprove(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            rs = UpdateRequestCOAApprove(id, request_no)
            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU55")>
        Function UpdateRequestCOAApprove(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
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

#End Region
    End Class
End Namespace
