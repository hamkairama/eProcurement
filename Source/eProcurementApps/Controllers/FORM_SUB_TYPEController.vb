Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports System.Transactions
Imports OfficeOpenXml
Imports eProcurementApps.Facade

Namespace Controllers
    Public Class FORM_SUB_TYPEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "SETUP FST"
        <CAuthorize(Role:="MNU10")>
        <CAuthorize(Role:="MNU55")>
        Function Index(flag As Decimal) As ActionResult
            Dim fORM_SUB_TYPE As New List(Of TPROC_FORM_SUB_TYPE)
            fORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.TPROC_FORM_SUBTYPE_GR.FORM_TYPE_ID).ToList()
            ViewBag.Message = TempData("msg")

            ViewBag.flag = flag
            Return View(fORM_SUB_TYPE)
        End Function

        <CAuthorize(Role:="MNU10")>
        <CAuthorize(Role:="MNU55")>
        Function List() As ActionResult
            Dim fORM_SUB_TYPE As New List(Of TPROC_FORM_SUB_TYPE)
            fORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live Or y.ROW_STATUS = 2 Or y.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.TPROC_FORM_SUBTYPE_GR.FORM_TYPE_ID).ToList()

            Return PartialView("_List", fORM_SUB_TYPE)
        End Function

        <CAuthorize(Role:="MNU10")>
        <CAuthorize(Role:="MNU55")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim fORM_SUB_TYPE As TPROC_FORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Find(id)
            If IsNothing(fORM_SUB_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", fORM_SUB_TYPE)
        End Function

        <CAuthorize(Role:="MNU10")>
        <CAuthorize(Role:="MNU55")>
        Function Create(flag As Decimal) As ActionResult
            ViewBag.flag = flag
            Session("Active_Directory") = "Request"

            ViewBag.Message = TempData("result")
            Return View("Create")
        End Function

        <CAuthorize(Role:="MNU10")>
        Function ActionCreate(ByVal form_type_id As Decimal, ByVal form_sub_type_name As String, ByVal form_sub_type_description As String, ByVal sla As Decimal, ByVal fom_sub_type_detail As String(), ByVal popup_account As Decimal,
                              ByVal budget_code As String, ByVal account_code_start As String, ByVal account_code_end As String, ByVal form_sub_type_bc As String()) As ActionResult
            Dim rs As New ResultStatus
            Dim fst_gr As New TPROC_FORM_SUBTYPE_GR

            fst_gr.FORM_TYPE_ID = form_type_id
            fst_gr.SUB_FORM_TYPE_NAME = form_sub_type_name
            fst_gr.SUB_FORM_TYPE_DESCRIPTION = form_sub_type_description
            fst_gr.SLA = sla
            fst_gr.POPUP_ACCOUNT = popup_account
            fst_gr.BUDGET_CODE = budget_code
            fst_gr.ACCOUNT_CODE_START = account_code_start
            fst_gr.ACCOUNT_CODE_END = account_code_end
            fst_gr.CREATED_TIME = Date.Now
            fst_gr.CREATED_BY = Session("USER_ID")

            Dim fst As New TPROC_FORM_SUB_TYPE
            fst.ROW_STATUS = ListEnum.RowStat.Live

            rs = FormSubTypeFacade.InsertFormSubTypeAll(fom_sub_type_detail, fst_gr, fst, form_sub_type_bc)

            TempData("result") = rs.MessageText

            Return RedirectToAction("Index", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU10")>
        <CAuthorize(Role:="MNU55")>
        Function Edit(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            ViewBag.FormType = Dropdown.FormType()
            ViewBag.AccountCode = Dropdown.AccountCode()
            Dim fORM_SUB_TYPE As TPROC_FORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Find(id)
            If IsNothing(fORM_SUB_TYPE) Then
                Return HttpNotFound()
            End If

            ViewBag.flag = flag

            'set active directory for request param
            Session("Active_Directory") = "Request"

            ViewBag.Message = TempData("result")

            Return View("_Edit", fORM_SUB_TYPE)
        End Function

        <CAuthorize(Role:="MNU10")>
        Function ActionEdit(ByVal id As Decimal, ByVal form_type_id As Decimal, ByVal form_sub_type_name As String, ByVal form_sub_type_description As String, ByVal sla As Decimal, ByVal fom_sub_type_detail As String(), ByVal popup_account As Decimal,
                              ByVal budget_code As String, ByVal account_code_start As String, ByVal account_code_end As String, ByVal form_sub_type_bc As String()) As ActionResult
            Dim rs As New ResultStatus
            Dim fst_gr_id As Decimal
            Using scope As New TransactionScope()
                Try
                    Using db As New eProcurementEntities
                        Dim fORM_SUB_TYPE As TPROC_FORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Find(id)
                        fORM_SUB_TYPE.LAST_MODIFIED_TIME = Date.Now
                        fORM_SUB_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
                        db.Entry(fORM_SUB_TYPE).State = EntityState.Modified

                        fst_gr_id = fORM_SUB_TYPE.TPROC_FORM_SUBTYPE_GR.ID
                        Dim fst_dt = db.TPROC_FORM_SUBTYPE_DT.Where(Function(x) x.FORM_SUBTYPE_GR_ID = fst_gr_id).ToList()
                        If fst_dt.Count > 0 Then
                            For Each item As TPROC_FORM_SUBTYPE_DT In fst_dt
                                db.TPROC_FORM_SUBTYPE_DT.Remove(item)
                            Next
                        End If

                        db.SaveChanges()
                        rs.SetSuccessStatus()
                    End Using

                    If rs.IsSuccess Then
                        Dim fst_gr As New TPROC_FORM_SUBTYPE_GR
                        fst_gr.FORM_TYPE_ID = form_type_id
                        fst_gr.SUB_FORM_TYPE_NAME = form_sub_type_name
                        fst_gr.SUB_FORM_TYPE_DESCRIPTION = form_sub_type_description
                        fst_gr.SLA = sla
                        fst_gr.POPUP_ACCOUNT = popup_account
                        fst_gr.BUDGET_CODE = budget_code
                        fst_gr.ACCOUNT_CODE_START = account_code_start
                        fst_gr.ACCOUNT_CODE_END = account_code_end

                        rs = FormSubTypeFacade.UpdateFormSubTypeGr(fst_gr_id, fst_gr)
                    End If

                    If rs.IsSuccess Then
                        rs = FormSubTypeFacade.InsertFormSubTypeDt(fom_sub_type_detail, fst_gr_id)
                    End If

                    If rs.IsSuccess Then
                        rs = FormSubTypeFacade.UpdateAdditionalBudgetFst(fst_gr_id, form_sub_type_bc)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return RedirectToAction("Index", New With {.flag = 0})
        End Function

        <CAuthorize(Role:="MNU10")>
        <CAuthorize(Role:="MNU55")>
        Function Delete(ByVal id As Decimal, flag As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim fORM_SUB_TYPE As TPROC_FORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Find(id)
            If IsNothing(fORM_SUB_TYPE) Then
                Return HttpNotFound()
            End If

            Session("Active_Directory") = "Request"

            ViewBag.flag = flag

            ViewBag.Message = TempData("result")

            Return View("_Delete", fORM_SUB_TYPE)
        End Function

        <CAuthorize(Role:="MNU10")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            'Facade.FormSubTypeFacade.DeleteFormSubType(id)
            Dim rs As New ResultStatus

            Try
                Dim fst = db.TPROC_FORM_SUB_TYPE.Find(id)
                fst.ROW_STATUS = ListEnum.RowStat.InActive
                fst.LAST_MODIFIED_TIME = Date.Now
                fst.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(fst).State = EntityState.Modified
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

        <CAuthorize(Role:="MNU10")>
        Function CheckData(ByVal id As Decimal, ByVal form_sub_type_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim fORM_SUB_TYPE As New TPROC_FORM_SUB_TYPE
            'check create
            If id = 0 Then
                fORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(x) x.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME.ToUpper() = form_sub_type_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If fORM_SUB_TYPE IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                fORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(x) x.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME.ToUpper() = form_sub_type_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If fORM_SUB_TYPE IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU10")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_FORM_SUB_TYPE.Where(Function(x) x.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU10")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_FORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Find(id)
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

        <CAuthorize(Role:="MNU10")>
        Public Function Upload(formCollection As FormCollection) As ActionResult
            Dim sb As New StringBuilder
            Dim rs As New ResultStatus

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
                                Dim item As String = workSheet.Cells(rowIterator, 2).Value.ToString()
                                Dim isExist = CheckData(0, item)

                                If isExist = 1 Then
                                    sb.Append("Item " + item + " already exist." + "<br />")
                                    sb.AppendLine()
                                Else
                                    Dim fst_gr As New TPROC_FORM_SUBTYPE_GR
                                    fst_gr.FORM_TYPE_ID = GetFormTypeID(workSheet.Cells(rowIterator, 1).Value.ToString())
                                    fst_gr.SUB_FORM_TYPE_NAME = workSheet.Cells(rowIterator, 2).Value.ToString()
                                    fst_gr.SUB_FORM_TYPE_DESCRIPTION = workSheet.Cells(rowIterator, 3).Value.ToString()
                                    fst_gr.SLA = workSheet.Cells(rowIterator, 4).Value.ToString()
                                    fst_gr.POPUP_ACCOUNT = workSheet.Cells(rowIterator, 5).Value.ToString()
                                    fst_gr.BUDGET_CODE = workSheet.Cells(rowIterator, 6).Value.ToString()
                                    fst_gr.ACCOUNT_CODE_START = workSheet.Cells(rowIterator, 7).Value.ToString()
                                    fst_gr.ACCOUNT_CODE_END = workSheet.Cells(rowIterator, 8).Value.ToString()
                                    fst_gr.CREATED_TIME = Date.Now
                                    fst_gr.CREATED_BY = Session("USER_ID")

                                    Dim detail_list As New List(Of String)

                                    Dim colRd = 9
                                    For i As Integer = 1 To 10
                                        Dim detail As String = ""
                                        Dim val = workSheet.Cells(rowIterator, colRd).Value
                                        If val IsNot Nothing Then
                                            detail = GetRelDeptID(workSheet.Cells(rowIterator, colRd).Value.ToString()).ToString() + "|" + i.ToString()
                                            detail_list.Add(detail)
                                        End If

                                        colRd += 1
                                    Next

                                    Dim fst As New TPROC_FORM_SUB_TYPE
                                    fst.ROW_STATUS = ListEnum.RowStat.Live

                                    Dim fom_sub_type_detail As String() = detail_list.ToArray()
                                    rs = FormSubTypeFacade.InsertFormSubTypeAll(fom_sub_type_detail, fst_gr, fst, Nothing)
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

        Public Function GetFormTypeID(obj As String) As Integer
            Dim result As Integer

            Using db As New eProcurementEntities
                result = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME.Replace(" ", "").ToUpper() = obj.Replace(" ", "").ToUpper()).FirstOrDefault().ID
            End Using

            Return result
        End Function

        Public Function GetRelDeptID(obj As String) As Integer
            Dim result As Integer

            Using db As New eProcurementEntities
                result = db.TPROC_REL_DEPT.Where(Function(x) x.RELATED_DEPARTMENT_NAME.Replace(" ", "").ToUpper() = obj.Replace(" ", "").ToUpper()).FirstOrDefault().ID
            End Using

            Return result
        End Function
#End Region

#Region "REQUEST FST"

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTCreate(ByVal form_type_id As Decimal, ByVal form_sub_type_name As String, ByVal form_sub_type_description As String, ByVal sla As Decimal, ByVal fom_sub_type_detail As String(), ByVal popup_account As Decimal,
                              ByVal budget_code As String, ByVal account_code_start As String, ByVal account_code_end As String, ByVal desc As String, ByVal appr_nm As String, ByVal appr_email As String, ByVal form_sub_type_bc As String()) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim fst_gr As New TPROC_FORM_SUBTYPE_GR
            fst_gr.FORM_TYPE_ID = form_type_id
            fst_gr.SUB_FORM_TYPE_NAME = form_sub_type_name
            fst_gr.SUB_FORM_TYPE_DESCRIPTION = form_sub_type_description
            fst_gr.SLA = sla
            fst_gr.POPUP_ACCOUNT = popup_account
            fst_gr.BUDGET_CODE = budget_code
            fst_gr.ACCOUNT_CODE_START = account_code_start
            fst_gr.ACCOUNT_CODE_END = account_code_end
            fst_gr.CREATED_TIME = Date.Now
            fst_gr.CREATED_BY = Session("USER_ID")
            fst_gr.ROW_STATUS = ListEnum.RowStat.Create

            Dim fst As New TPROC_FORM_SUB_TYPE
            fst.ROW_STATUS = ListEnum.RowStat.Create

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = form_sub_type_name
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "FORM_SUB_TYPE"
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
                    rs = FormSubTypeFacade.InsertFormSubTypeAll(fom_sub_type_detail, fst_gr, fst, form_sub_type_bc)

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

            Return RedirectToAction("Create", "FORM_SUB_TYPE", New With {.flag = 1})
        End Function

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTDelete(ByVal id As Decimal, ByVal desc As String, ByVal appr_nm As String, ByVal appr_email As String) As ActionResult
            Dim rs As New ResultStatus
            Dim new_req As New TPROC_REQUEST

            Dim fst = db.TPROC_FORM_SUB_TYPE.Find(id)

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = fst.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "FORM_SUB_TYPE"
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
                    fst.ROW_STATUS = ListEnum.RowStat.Delete
                    fst.LAST_MODIFIED_TIME = Date.Now
                    fst.LAST_MODIFIED_BY = Session("USER_ID")
                    db.Entry(fst).State = EntityState.Modified
                    db.SaveChanges()

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
                Catch ex As Exception
                    rs.SetErrorStatus("failed to send email")
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return RedirectToAction("Delete", "FORM_SUB_TYPE", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTEdit(ByVal id As Decimal, ByVal form_type_id As Decimal, ByVal form_sub_type_name As String, ByVal form_sub_type_description As String, ByVal sla As Decimal, ByVal fom_sub_type_detail As String(), ByVal popup_account As Decimal,
                              ByVal budget_code As String, ByVal account_code_start As String, ByVal account_code_end As String, ByVal desc As String, ByVal appr_nm As String, ByVal appr_email As String, ByVal form_sub_type_bc As String()) As ActionResult
            Dim rs As New ResultStatus
            Dim fst_gr_id As Decimal
            Dim new_req As New TPROC_REQUEST

            Dim form_sub_type As New TPROC_FORM_SUB_TYPE
            form_sub_type.ROW_STATUS = ListEnum.RowStat.Edit
            form_sub_type.LAST_MODIFIED_TIME = Date.Now
            form_sub_type.LAST_MODIFIED_BY = Session("USER_ID")

            Dim fst_gr As New TPROC_FORM_SUBTYPE_GR
            fst_gr.FORM_TYPE_ID = form_type_id
            fst_gr.SUB_FORM_TYPE_NAME = form_sub_type_name
            fst_gr.SUB_FORM_TYPE_DESCRIPTION = form_sub_type_description
            fst_gr.SLA = sla
            fst_gr.POPUP_ACCOUNT = popup_account
            fst_gr.BUDGET_CODE = budget_code
            fst_gr.ACCOUNT_CODE_START = account_code_start
            fst_gr.ACCOUNT_CODE_END = account_code_end
            fst_gr.CREATED_BY = Session("USER_ID")
            fst_gr.CREATED_TIME = Date.Now
            fst_gr.REV_FST_ID = id
            fst_gr.ROW_STATUS = ListEnum.RowStat.Edit

            Dim req As New TPROC_REQUEST
            req.REQUEST_NO = Generate.GetNo("TPROC_REQUEST")
            req.RELATION_FLAG = form_sub_type_name
            req.REQUEST_BY = CurrentUser.GetCurrentUserId()
            req.REQUEST_DT = Date.Now
            req.CREATED_BY = CurrentUser.GetCurrentUserId()
            req.CREATED_TIME = Date.Now
            req.STATUS = ListEnum.Request.NeedApprove
            req.DESCRIPTION = desc
            req.CONTROL = "FORM_SUB_TYPE"
            req.ACTION = "Edit"
            req.REQUESTOR_EMAIL = CurrentUser.GetCurrentUserEmail()
            req.APPROVAL_BY = appr_nm
            req.APPROVAL_EMAIL = appr_email

            Dim emailTo As New ListFieldNameAndValue
            emailTo.AddItem("Email", appr_email)

            Using scope As New TransactionScope()
                Try
                    rs = FormSubTypeFacade.UpdateFormSubType(id, form_sub_type)
                    If rs.IsSuccess Then
                        rs = FormSubTypeFacade.InsertFormSubTypeGr(fst_gr_id, fst_gr)
                        If rs.IsSuccess Then
                            rs = FormSubTypeFacade.InsertFormSubTypeDt(fom_sub_type_detail, fst_gr_id)
                            If rs.IsSuccess Then
                                rs = FormSubTypeFacade.UpdateAdditionalBudgetFst(fst_gr_id, form_sub_type_bc)
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
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            TempData("result") = rs.MessageText

            Return RedirectToAction("Edit", "FORM_SUB_TYPE", New With {.id = id, .flag = 1})
        End Function

        <CAuthorize(Role:="MNU55")>
        Function RequestApproveComplete(ByVal reqno As String, ByVal rel_flag As String, ByVal control As String, ByVal actions As String, ByVal data_flag As Decimal, ByVal access_from As String) As ActionResult
            Dim request As TPROC_REQUEST = db.TPROC_REQUEST.Where(Function(x) x.REQUEST_NO = reqno).FirstOrDefault()
            ViewBag.ReqNo = reqno
            ViewBag.RequestBy = request.REQUEST_BY
            ViewBag.RequestDt = request.REQUEST_DT
            ViewBag.RequestAction = request.ACTION
            ViewBag.RequestDesc = request.DESCRIPTION

            ViewBag.access_from = access_from

            ViewBag.ReqNo = reqno
            ViewBag.data_flag = data_flag
            ViewBag.FormType = Dropdown.FormType()
            ViewBag.AccountCode = Dropdown.AccountCode()
            ViewBag.Message = TempData("result")

            Dim fst As TPROC_FORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(x) x.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME = rel_flag).FirstOrDefault()

            Dim fst_gr = FormSubTypeFacade.GetFstGrToBe(fst.ID, CInt(DirectCast([Enum].Parse(GetType(ListEnum.RowStat), actions), ListEnum.RowStat)))
            If fst_gr IsNot Nothing Then
                ViewBag.FormTypeName = fst_gr.TPROC_FORM_TYPE.FORM_TYPE_NAME
                ViewBag.FormSubTypeName = fst_gr.SUB_FORM_TYPE_NAME
                ViewBag.Desc = fst_gr.SUB_FORM_TYPE_DESCRIPTION
                ViewBag.Sla = fst_gr.SLA
                ViewBag.PopUp = fst_gr.POPUP_ACCOUNT
                ViewBag.BudgetCode = fst_gr.BUDGET_CODE
                ViewBag.BudgetCodeStart = fst_gr.ACCOUNT_CODE_START
                ViewBag.BudgetCodeEnd = fst_gr.ACCOUNT_CODE_END
            End If

            Return View("RequestApproveComplete", fst)
        End Function

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTComplete(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            If actions = ListEnum.RowStat.Create.ToString() Then
                rs = ActionRequestFSTCompleteCreate(id, request_no)
            ElseIf actions = ListEnum.RowStat.Edit.ToString() Then
                rs = ActionRequestFSTCompleteEdit(id, request_no)
            ElseIf actions = ListEnum.RowStat.Delete.ToString() Then
                rs = ActionRequestFSTCompleteDelete(id, request_no)
            End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTApprove(ByVal id As Decimal, ByVal request_no As String, ByVal actions As String) As Integer
            Dim rs As New ResultStatus

            'If actions = ListEnum.RowStat.Edit.ToString() Then
            rs = UpdateRequestFSTApprove(id, request_no)
            'End If

            TempData("result") = rs.MessageText

            Return rs.Status
        End Function

        <CAuthorize(Role:="MNU55")>
        Function UpdateRequestFSTApprove(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
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

        <CAuthorize(Role:="MNU55")>
        Function UpdateRowStatusFST(id As Decimal, rowStat As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim fst As New TPROC_FORM_SUB_TYPE

            Try
                fst = db.TPROC_FORM_SUB_TYPE.Find(id)
                fst.ROW_STATUS = rowStat
                fst.LAST_MODIFIED_TIME = Date.Now
                fst.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(fst).State = EntityState.Modified
                db.SaveChanges()

                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try


            Return rs
        End Function

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTCompleteCreate(id As Decimal, request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UpdateRowStatusFST(id, ListEnum.RowStat.Live)
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

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTCompleteDelete(id As Decimal, request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim uSER As New TPROC_USER
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = UpdateRowStatusFST(id, ListEnum.RowStat.InActive)
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

        <CAuthorize(Role:="MNU55")>
        Function ActionRequestFSTCompleteEdit(ByVal id As Decimal, ByVal request_no As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim request As New TPROC_REQUEST

            Using scope As New TransactionScope()
                Try
                    rs = FormSubTypeFacade.UpdatesFSTByRequest(id, ListEnum.RowStat.Live)
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
