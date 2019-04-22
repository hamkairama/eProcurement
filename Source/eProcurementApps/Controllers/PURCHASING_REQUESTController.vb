Imports System.Web.Mvc
Imports eProcurementApps.Models
Imports eProcurementApps.Facade
Imports eProcurementApps.Helpers
Imports System.Text
Imports System.Web
Imports System.Net
Imports System.Transactions
Imports System.Data.Entity
Imports System.Data.SqlClient

Namespace Controllers
    Public Class PURCHASING_REQUESTController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "LIST PR"
        <HttpPost>
        <CAuthorize(Role:="MNU24")>
        Function ListPrByStatus(txt_status_pr_val As String, flag_detail As Integer, sub_title As String) As ActionResult
            Dim r As String = ""
            TempData("status") = txt_status_pr_val

            If txt_status_pr_val = "" Then
                TempData("status") = "null"
            End If

            Return RedirectToAction("IndexListPR", New With {.FlagDetail = flag_detail, .SubTitle = sub_title})
        End Function

        <CAuthorize(Role:="MNU60")>
        Function GetDataPrByStatus(txt_status_pr_val As String) As List(Of TPROC_PR_HEADER)
            Dim prx As New List(Of TPROC_PR_HEADER)

            prx = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = txt_status_pr_val).OrderBy(Function(y) y.ID).ToList()

            Return prx
        End Function

        <CAuthorize(Role:="MNU24")>
        Function IndexListPR(FlagDetail As Integer, SubTitle As String) As ActionResult
            Dim pr_header As New List(Of TPROC_PR_HEADER)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")
            Dim is_user_admin As String = Session("IS_SUPER_ADMIN")

            ViewBag.FlagDetail = FlagDetail
            ViewBag.SubTitle = SubTitle

            If FlagDetail = ListEnum.FlagDetail.MyListPR Then
                pr_header = MyListPR(user_id_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.MyListApprovalPRWA Then
                pr_header = MyListApprPRWA(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.MyListApprovalPRRD Then
                pr_header = MyListApprPRRelDept(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.MyPRReadyToSignOff Then
                pr_header = MyPRReadyToSignOff(user_id)

            ElseIf FlagDetail = ListEnum.FlagDetail.AllListPR Then
                If TempData("Status") IsNot Nothing Then
                    If TempData("Status") = "null" Then
                        ViewBag.Message = "Please select status"
                        Return View(pr_header)
                    Else
                        pr_header = GetDataPrByStatus(TempData("Status"))
                        ViewBag.Message = Nothing
                    End If
                End If
                'pr_header = AllListPR(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.PRsReadyToHandle Then
                pr_header = AllPRReadyToHandle(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.PRsReadyToCreatePO Then
                pr_header = AllPRReadyToCreatePO(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.ListPRBySubmitter Then
                If TempData("user_id_id") Is Nothing Or TempData("user_id_id") = "0" Or TempData("user_id_id") = "" Then
                    Return View(pr_header)
                Else
                    pr_header = GetDataPRBySubmitter(Convert.ToInt32(TempData("user_id_id")))
                End If
            ElseIf FlagDetail = ListEnum.FlagDetail.ListPRComplete Then
                pr_header = ListPRComplete(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.ListPRSignOff Then
                pr_header = ListPRSignOff(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.ListPRReject Then
                pr_header = ListPRRejected(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.PRsReadyToComplete Then
                pr_header = AllPRReadyToComplete(user_id)
            ElseIf FlagDetail = ListEnum.FlagDetail.PRsReadyToSignOff Then
                pr_header = AllPRReadyToSignOff(user_id)
            End If

            ViewBag.Message = TempData("message")

            Return View(pr_header)
        End Function

        <CAuthorize(Role:="MNU24")>
        Function MyListPR(user_id_id As Decimal) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.USER_ID_ID = user_id_id).OrderByDescending(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU25")>
        Public Function MyListApprPRWA(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)
            pr_header = (From _header In db.TPROC_PR_HEADER
                         Join _detail In db.TPROC_PR_DETAIL On _header.ID Equals (_detail.PR_HEADER_ID)
                         Join _detail_appr_wa In db.TPROC_PR_APPR_WA On _detail.ID Equals (_detail_appr_wa.PR_DETAIL_ID)
                         Where _detail_appr_wa.USER_ID.ToUpper() = user_id.ToUpper() And _detail.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Complete _
                             And _detail.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Rejected And _header.PR_STATUS <> ListEnum.PRStatus.PrRejected
                         Select _header
                         Distinct
                         Order By _header.PR_NO Descending).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU26")>
        Function MyListApprPRRelDept(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = (From _header In db.TPROC_PR_HEADER
                         Join _detail In db.TPROC_PR_DETAIL On _header.ID Equals (_detail.PR_HEADER_ID)
                         Join _appr_reldept_gr In db.TPROC_PR_APPR_RELDEPT_GR On _appr_reldept_gr.PR_HEADER_ID Equals (_header.ID)
                         Join _appr_reldept_dt In db.TPROC_PR_APPR_RELDEPT_DT On _appr_reldept_dt.PR_APPR_RELDEPT_GR_ID Equals (_appr_reldept_gr.ID)
                         Where _appr_reldept_dt.USER_ID.ToUpper() = user_id.ToUpper() And _detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete _
                             And _header.PR_STATUS <> ListEnum.PRStatus.PrRejected And _header.PR_STATUS < ListEnum.PRStatus.PrApprovedComplete
                         Select _header
                         Distinct
                         Order By _header.PR_NO Descending).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU47")>
        Function MyPRReadyToSignOff(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.TPROC_USER.USER_ID.ToUpper() = user_id.ToUpper() And x.PR_STATUS = ListEnum.PRStatus.Complete).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU22")>
        Function ListRelDept(form_sub_type_id As Decimal) As ActionResult
            Dim list_sub_formtype As New List(Of TPROC_FORM_SUBTYPE_DT)
            list_sub_formtype = Facade.FormSubTypeFacade.GetRelDeptFromSubTypeId(form_sub_type_id)

            Return PartialView("_ListRelDept", list_sub_formtype)
        End Function

        <CAuthorize(Role:="MNU29")>
        Function AllListPR(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU30")>
        Function AllPRReadyToHandle(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.PrApprovedComplete).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU31")>
        Function AllPRReadyToCreatePO(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.PrHandled).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU45")>
        Function AllPRReadyToComplete(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.GoodReceive Or x.PR_STATUS = ListEnum.PRStatus.PrHandled Or x.PR_STATUS = ListEnum.PRStatus.CreatePo).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU46")>
        Function AllPRReadyToSignOff(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.Complete).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <HttpPost>
        <CAuthorize(Role:="32")>
        Function ListPRBySubmitter(user_id_id As String) As ActionResult
            Dim r As String = ""
            TempData("user_id_id") = user_id_id

            Return RedirectToAction("IndexListPR", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRBySubmitter), .SubTitle = "List PR By Submitter"})
        End Function

        <CAuthorize(Role:="MNU32")>
        Function GetDataPRBySubmitter(user_id_id As Decimal) As List(Of TPROC_PR_HEADER)
            Dim pr_headerx As New List(Of TPROC_PR_HEADER)

            pr_headerx = db.TPROC_PR_HEADER.Where(Function(x) x.USER_ID_ID = user_id_id).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_headerx
        End Function

        <CAuthorize(Role:="MNU33")>
        Function ListPRComplete(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.Complete).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU34")>
        Function ListPRSignOff(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.SignOff).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU35")>
        Function ListPRRejected(user_id As String) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(x) x.PR_STATUS = ListEnum.PRStatus.PrRejected).OrderBy(Function(x) x.PR_NO).ToList()

            Return pr_header
        End Function


#End Region

#Region "CREATE NEW PR"
        <CAuthorize(Role:="MNU22")>
        Function Create() As ActionResult
            Try
                Dim pr_header_completed = GetPRIsCompleted()
                If pr_header_completed IsNot Nothing Then
                    TempData("message") = pr_header_completed.PR_NO + " has been completed. Please sign off your request before you create new PR"
                    Dim idx = pr_header_completed.ID
                    Dim flagx = Convert.ToDecimal(ListEnum.FlagDetail.MyPRReadyToSignOff)
                    Return RedirectToAction("DetailHeader", New With {.id = idx, .flag = flagx})
                End If
            Catch ex As Exception
            End Try

            CDataImage.CleanDataImageFiles()

            ViewBag.Message = TempData("message")
            ViewBag.FailedMessage = TempData("failedMessage")

            Return View()
        End Function

        Function GetQtyItem(id As Decimal) As Decimal
            Dim item = db.TPROC_STOCK.Find(id)
            Dim qty_r As Decimal = 0

            If item.QUANTITY_MIN IsNot Nothing Then
                qty_r = item.QUANTITY - item.QUANTITY_MIN.Value
            End If

            Return qty_r
        End Function

        Function GetPRIsCompleted() As TPROC_PR_HEADER
            Dim pr_head As TPROC_PR_HEADER
            Dim user_id_ID = Convert.ToDecimal(Session("USER_ID_ID"))
            pr_head = db.TPROC_PR_HEADER.Where(Function(x) x.USER_ID_ID = user_id_ID And x.PR_STATUS = ListEnum.PRStatus.Complete).FirstOrDefault()

            Return pr_head
        End Function

        <CAuthorize(Role:="MNU22")>
        Function ActionCreate(ByVal user_id_id As Decimal, ByVal request_dt As DateTime, ByVal cb_form_type_id As Decimal, ByVal cb_sub_type_id As Decimal,
                             ByVal cb_gt_id As Decimal, ByVal delivery_days As Decimal, dev_dt_new As DateTime, ByVal req_indicator As String,
                              ByVal count_code As String, ByVal sub_total_price As Decimal, ByVal litem_detail As String(), ByVal for_storage As Integer,
                              ByVal count_code_start As String, ByVal count_code_end As String) As String
            Dim rs As New ResultStatus
            Dim r As String
            Dim sb As New StringBuilder
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_header As New TPROC_PR_HEADER

            pr_header.PR_NO = Generate.GetPRSeq("TPROC_PR_HEADER")
            pr_header.USER_ID_ID = user_id_id
            pr_header.PR_DATE = request_dt
            pr_header.FORM_TYPE_ID = cb_form_type_id
            pr_header.SUB_TYPE_ID = cb_sub_type_id

            Dim gt_id As Decimal
            If cb_gt_id = 0 Then
                gt_id = CommonFunction.GetGoodTypeNonStockId()
            Else
                gt_id = cb_gt_id
            End If
            pr_header.GOOD_TYPE_ID = gt_id

            pr_header.DELIVERY_DAYS = delivery_days
            pr_header.EXP_DEV_DT = dev_dt_new
            pr_header.PR_INDICATOR = req_indicator
            pr_header.ACCOUNT_CODE = count_code
            pr_header.SUB_TOTAL = sub_total_price
            pr_header.PR_STATUS = ListEnum.PRStatus.Submitted
            pr_header.CREATED_TIME = Date.Now
            pr_header.CREATED_BY = Session("USER_ID")
            pr_header.FOR_STORAGE = for_storage

            Dim files As IEnumerable(Of HttpPostedFileBase)
            files = CDataImage.GetImageFiles


            'get form type name by formtype_id
            Dim form_type_name As String = ""
            Using db As New eProcurementEntities
                form_type_name = db.TPROC_FORM_TYPE.Find(cb_form_type_id).FORM_TYPE_NAME
            End Using

            'cek budget each item wa
            rs = prFacade.CheckBudgetEachItem(litem_detail, form_type_name, count_code, count_code_start, count_code_end)
            If rs.IsSuccess Then
                rs = InsertPR(pr_header, litem_detail, files)
            End If


            r = rs.IsSuccess.ToString() + "|" + rs.MessageText.ToString()

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " has been created"
                TempData("failedMessage") = Nothing
            Else
                TempData("failedMessage") = rs.MessageText
            End If

            Return r
        End Function

        <CAuthorize(Role:="MNU22")>
        Public Function InsertPR(ByVal pr_header As TPROC_PR_HEADER, ByVal l_pr_dt As String(), files As IEnumerable(Of HttpPostedFileBase)) As ResultStatus
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_header_id As Decimal

            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")

            Using scope As New TransactionScope()
                Try
                    rs = prFacade.InsertPRHeader(pr_header, pr_header_id)
                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRDetail(l_pr_dt, pr_header_id)
                        If rs.IsSuccess Then
                            rs = InsertPRAttachment(pr_header_id, files)
                            If rs.IsSuccess Then
                                Generate.CommitGenerator("TPROC_PR_HEADER")
                                If rs.IsSuccess Then
                                    rs = prFacade.InsertPRHistorical(pr_header_id, Date.Now, ListEnum.PRStatus.Submitted.ToString(), current_user_name, Date.Now)
                                    If rs.IsSuccess Then
                                        rs = prFacade.SendEmailApprRevWA(pr_header_id)
                                        If rs.IsSuccess Then
                                            scope.Complete()
                                            rs.SetSuccessStatus()
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

            Return rs
        End Function

        <CAuthorize(Role:="MNU22")>
        Public Function InsertPRAttachment(header_id As Decimal, files As IEnumerable(Of HttpPostedFileBase)) As ResultStatus
            Dim rs As New ResultStatus

            Try
                If files IsNot Nothing Then
                    For Each item As HttpPostedFileBase In files
                        Dim file As HttpPostedFileBase = item
                        Dim attach As String = System.IO.Path.GetFileName(file.FileName)
                        Dim path As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), attach)
                        'Dim path As String = Server.MapPath("Attachments") + "\\" + attach
                        Dim pr_attach As New TPROC_PR_ATTACHMENT
                        pr_attach.CREATED_BY = Session("USER_ID")
                        pr_attach.CREATED_TIME = Date.Now
                        pr_attach.PR_HEADER_ID = header_id
                        pr_attach.FILE_NAME = attach
                        Using db As New eProcurementEntities
                            db.TPROC_PR_ATTACHMENT.Add(pr_attach)
                            db.SaveChanges()
                        End Using
                        'copy file to folder in application
                        file.SaveAs(path)
                    Next
                End If
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        'Function SendEmailCreateUser(wa As String) As ResultStatus
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

        '    Return rs
        'End Function

        <CAuthorize(Role:="MNU22")>
        Public Function GetFormData(file As IEnumerable(Of HttpPostedFileBase)) As String
            Dim rs As New ResultStatus
            Dim r As String = ""

            rs.SetSuccessStatus()
            Try
                If file IsNot Nothing And file.Count > 0 Then
                    CDataImage.DataFiles(file)
                End If

                If file Is Nothing Then
                    CDataImage.CleanDataImage()
                    CDataImage.CleanDataImageFiles()
                End If

                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            If rs.IsSuccess Then
                r = "True|" + "uploaded"
            Else
                r = "False|" + rs.MessageText
            End If

            Return r
        End Function

        <CAuthorize(Role:="MNU22")>
        Function GetFormSubTypeFromId(form_type_id As Decimal) As ActionResult
            Dim result As New List(Of SelectListItem)
            result = Helpers.Dropdown.GetFormSubTypeFromFormTypeId(form_type_id)

            Return Json(result)
        End Function

        <CAuthorize(Role:="MNU22")>
        Function GetFstBcFromid(form_sub_type_id As Decimal) As ActionResult
            Dim result As New List(Of SelectListItem)
            result = Helpers.Dropdown.GetBcFromFormSubTypeId(form_sub_type_id)

            Return Json(result)
        End Function

        <CAuthorize(Role:="MNU22")>
        Function GetExpectedDt(count_days As Decimal) As String
            Dim holis As New List(Of TPROC_HOLIDAY)
            Dim list_holi As New List(Of String)
            Dim result As String = ""

            Dim toDay As Date = Date.Now
            Dim toDayEnd As Date = toDay.AddDays(count_days)
            Dim targetCount As Decimal = 0

            holis = db.TPROC_HOLIDAY.Where(Function(x) (x.CURR_MONTH >= toDay.Month And x.CURR_MONTH <= toDayEnd.Month) And (x.CURR_YEAR >= toDay.Year And x.CURR_YEAR <= toDayEnd.Year)).ToList()
            For Each ho As TPROC_HOLIDAY In holis
                list_holi.Add(ho.HOLIDAY_DATE.ToString("dd-MM-yyyy"))
            Next


            Dim index As Integer = 0
            Dim param As Integer = 1
            While index < count_days
                Dim newDay As Date = toDay.AddDays(param)
                If list_holi.Contains(newDay.ToString("dd-MM-yyyy")) = False Then
                    result = newDay.ToString("dd-MM-yyyy")
                    index += 1
                End If
                param += 1
            End While

            Return result
        End Function

        <CAuthorize(Role:="MNU22")>
        Function GetIsGoodType(id_ft As Decimal) As Integer
            Dim ft As New TPROC_FORM_TYPE

            Return db.TPROC_FORM_TYPE.Find(id_ft).IS_GOOD_TYPE
        End Function

        <CAuthorize(Role:="MNU24")>
        Function DetailHeader(ByVal id As Decimal, ByVal flag As Decimal) As ActionResult
            Dim prFacade As New PurchasingRequestFacade

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim user_id = Session("USER_ID")
            Dim header_param As New PR_HEADER_PARAM
            header_param.PRHeaderId = id
            header_param.PRFlag = flag

            Dim pr_header As TPROC_PR_HEADER = db.TPROC_PR_HEADER.Find(id)
            Dim status_appr_rd_gr As String = ""
            Dim status_appr_rd_dt As String = ""
            Dim gr_id As Integer
            'Get Status pr approver/reviewer for approve/review in related department
            For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT
                    If dt.USER_ID.ToUpper() = user_id.ToUpper() Then
                        status_appr_rd_dt = dt.APPR_RELDEPT_STATUS 'status of approver/reviewer reldept
                        status_appr_rd_gr = gr.RELDEPT_GR_STATUS 'status of reldept group
                        gr_id = gr.ID
                        Exit For
                    End If
                Next
            Next

            ViewBag.StatusRDGR = status_appr_rd_gr
            ViewBag.StatusRDApprRev = status_appr_rd_dt

            'if still waiting reviewer, get the reviewer using gr_id
            Dim reviewer_still_waiting As New StringBuilder
            If status_appr_rd_gr Is Nothing Then
                For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                    For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT.Where(Function(x) x.PR_APPR_RELDEPT_GR_ID = gr_id And x.APPR_RELDEPT_STATUS = "Waiting for review")
                        reviewer_still_waiting.Append(dt.USER_ID)
                        reviewer_still_waiting.Append("/")
                    Next
                Next
            End If

            ViewBag.StillWaitingBy = reviewer_still_waiting.ToString()
            ViewBag.PRStatus = pr_header.PR_STATUS
            ViewBag.FlagAction = flag '0 : list pr, 1 : list wa appr, 2 : list reldept appr
            ViewBag.PRID = pr_header.ID

            'set subtitle breadcrums
            If flag = ListEnum.FlagDetail.MyListPR Then
                ViewBag.SubTitle = ""
            ElseIf flag = ListEnum.FlagDetail.MyListApprovalPRWA Or flag = ListEnum.FlagInbox.ApprWA Then
                ViewBag.SubTitle = "Approval/Review WA of each item"
            ElseIf flag = ListEnum.FlagDetail.MyListApprovalPRRD Or flag = ListEnum.FlagInbox.ApprRD Then
                ViewBag.SubTitle = "Approval Rel. Dept of PR"
            ElseIf flag = ListEnum.FlagDetail.AllListPR Then
                ViewBag.SubTitle = "PR"
            ElseIf flag = ListEnum.FlagDetail.PRsReadyToHandle Then
                ViewBag.SubTitle = "PR Ready To Handle"
            ElseIf flag = ListEnum.FlagDetail.PRsReadyToCreatePO Then
                ViewBag.SubTitle = "PR Ready To Create PO"
            ElseIf flag = ListEnum.FlagDetail.PRsReadyToComplete Then
                ViewBag.SubTitle = "PR Ready To Complete"
            ElseIf flag = ListEnum.FlagDetail.PRsReadyToSignOff Then
                ViewBag.SubTitle = "PR Ready To Sign Off"
            End If

            'get status pr for to know button handle is on or off
            ViewBag.StatusPR = pr_header.PR_STATUS


            ViewBag.Bc = pr_header.ACCOUNT_CODE
            prFacade.GetDetailBc(pr_header.TPROC_FORM_SUB_TYPE.SUB_FORMTYPE_GR_ID, ViewBag.Bc, ViewBag.Bcs, ViewBag.Bce)

            ViewBag.Message = TempData("message")

            Return View("DetailHeader", pr_header)
        End Function

        <CAuthorize(Role:="MNU24")>
        Function DetailHeaderByPrNo(ByVal pr_no As String) As ActionResult
            Dim pr_header As TPROC_PR_HEADER = db.TPROC_PR_HEADER.Where(Function(x) x.PR_NO = pr_no).FirstOrDefault()

            Return View("DetailHeader", pr_header)
        End Function

        <CAuthorize(Role:="MNU24")>
        Function PrintDetailHeader(id As Decimal) As ActionResult
            Dim flag = Convert.ToInt32(ListEnum.FlagDetail.PRPrinting)

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim user_id = Session("USER_ID")

            Dim pr_header As TPROC_PR_HEADER = db.TPROC_PR_HEADER.Find(id)
            Dim status_appr_rd_gr As String = ""
            Dim status_appr_rd_dt As String = ""
            Dim gr_id As Integer
            'Get Status pr approver/reviewer for approve/review in related department
            For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT
                    If dt.USER_ID.ToUpper() = user_id.ToUpper() Then
                        status_appr_rd_dt = dt.APPR_RELDEPT_STATUS 'status of approver/reviewer reldept
                        status_appr_rd_gr = gr.RELDEPT_GR_STATUS 'status of reldept group
                        gr_id = gr.ID
                        Exit For
                    End If
                Next
            Next

            ViewBag.StatusRDGR = status_appr_rd_gr
            ViewBag.StatusRDApprRev = status_appr_rd_dt

            'if still waiting reviewer, get the reviewer using gr_id
            Dim reviewer_still_waiting As New StringBuilder
            If status_appr_rd_gr Is Nothing Then
                For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                    For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT.Where(Function(x) x.PR_APPR_RELDEPT_GR_ID = gr_id And x.APPR_RELDEPT_STATUS = "Waiting for review")
                        reviewer_still_waiting.Append(dt.USER_ID)
                        reviewer_still_waiting.Append("/")
                    Next
                Next
            End If

            ViewBag.StillWaitingBy = reviewer_still_waiting.ToString()

            ViewBag.PRStatus = pr_header.PR_STATUS

            ViewBag.FlagAction = flag '0 : list pr, 1 : list wa appr, 2 : list reldept appr

            'set subtitle breadcrums
            ViewBag.SubTitle = "PR Printing"

            'get status pr for to know button handle is on or off
            ViewBag.StatusPR = pr_header.PR_STATUS

            ViewBag.Message = TempData("message")

            Return View("PrintDetailHeader", pr_header)
        End Function

        <CAuthorize(Role:="MNU22")>
        Function PopupChooseOneApproval() As ActionResult
            Return PartialView("_PopupChooseOneApproval")
        End Function

        <CAuthorize(Role:="MNU22")>
        Function ListApprovalWa(wa_id As Decimal, total_price_wa As Decimal) As ActionResult
            Dim list_appr_wa_dt As New List(Of TPROC_APPROVAL_DT)

            Dim gr_id = db.TPROC_WA.Find(wa_id).APPROVAL_GROUP_ID
            list_appr_wa_dt = db.TPROC_APPROVAL_DT.Where(Function(x) x.APPROVAL_GROUP_ID = gr_id And x.TPROC_LEVEL.RUPIAH_LIMIT > total_price_wa).ToList()

            Return PartialView("_ListApprovalWa", list_appr_wa_dt)
        End Function

#End Region

#Region "ITEM / WORK AREA"
        Function DetailItem(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim pr_detail As TPROC_PR_DETAIL = db.TPROC_PR_DETAIL.Find(id)

            Dim apprBy As String = ""
            Dim apprDt As String = ""

            For Each item_gr In pr_detail.TPROC_PR_APPR_WA
                If pr_detail.PR_DETAIL_STATUS = Convert.ToInt32(ListEnum.ItemStatus.Complete) Then
                    If item_gr.APPR_WA_STATUS = ListEnum.ApprItemStatus.Approved.ToString() Then
                        apprBy = item_gr.NAME
                        If item_gr.LAST_MODIFIED_TIME.HasValue Then
                            apprDt = item_gr.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")
                        End If
                    End If

                ElseIf pr_detail.PR_DETAIL_STATUS = Convert.ToInt32(ListEnum.ItemStatus.Rejected) Then
                    If item_gr.APPR_WA_STATUS = ListEnum.ApprItemStatus.Rejected.ToString() Then
                        apprBy = item_gr.NAME
                        If item_gr.LAST_MODIFIED_TIME.HasValue Then
                            apprDt = item_gr.LAST_MODIFIED_TIME.Value.ToString("dd-MM-yyyy HH:mm")
                        End If
                    End If

                Else
                    apprBy = ""
                    apprDt = ""
                End If
            Next

            ViewBag.PRDetailStatus = pr_detail.PR_DETAIL_STATUS
            ViewBag.ApproveOrReviewBy = apprBy
            ViewBag.ApproveOrReviewDt = apprDt

            Return PartialView("_DetailItem", pr_detail)
        End Function
        Function DetailItemReview(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Dim pr_detail As New TPROC_PR_DETAIL
            Dim detail_param As New PR_DETAIL_PARAM
            Dim prFacade As New PurchasingRequestFacade

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            'check the item has been reviewed or not
            'ViewBag.IsReviewedByOther = prFacade.IsStatusReviewedByOther(id, ListEnum.ApprItemStatus.Reviewed.ToString())

            'get status wa as flag to button approve in the detail action approve wa
            rs = GetStatusApprWa(id)


            If rs.IsSuccess Then
                ViewBag.StatusApprWA = rs.MessageText
                pr_detail = db.TPROC_PR_DETAIL.Find(id)

                'get set detail param
                detail_param.PRDetialId = pr_detail.ID
                detail_param.PRDetailName = pr_detail.ITEM_NAME
            Else
                ViewBag.MessageError = rs.MessageText
                MsgBox(rs.MessageText)
                Return RedirectToAction("")
            End If

            Return PartialView("_DetailItemReview", pr_detail)
        End Function
        Function ActionItemReview(ByVal id As Decimal, pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_header As New TPROC_PR_HEADER

            Using scope As New TransactionScope()
                Try
                    pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

                    rs = prFacade.UpdateStatusApprPRWa(pr_header, ListEnum.ApprItemStatus.Reviewed.ToString(), "Waiting for review", id)
                    If rs.IsSuccess Then
                        rs = prFacade.UpdateStatusDetailItemToReadyApprove(id)
                        If rs.IsSuccess Then
                            rs = prFacade.SendEmailByReviewerWAToApprWA(pr_header, id)
                        End If
                    End If

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprItemStatus.ReviewWA.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus("Data has been reviewed")
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            TempData("message") = rs.MessageText

            Return rs
        End Function
        Function DetailItemApprove(ByVal id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Dim pr_detail As TPROC_PR_DETAIL
            Dim user_id As String = Session("USER_ID")
            Dim detail_param As New PR_DETAIL_PARAM

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            'get status wa as flag to button approve in the detail action approve wa
            rs = GetStatusApprWa(id)

            If rs.IsSuccess Then
                ViewBag.StatusApprWA = rs.MessageText
                pr_detail = db.TPROC_PR_DETAIL.Find(id)

                'get set detail param
                detail_param.PRDetialId = pr_detail.ID
                detail_param.PRDetailName = pr_detail.ITEM_NAME
            Else
                ViewBag.MessageError = rs.MessageText
                MsgBox(rs.MessageText)
                Return RedirectToAction("")
            End If

            Return PartialView("_DetailItemApprove", pr_detail)
        End Function
        Function ActionItemApprove(ByVal id As Decimal, pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

                    If prFacade.IsPrApprWaOustanding(id) = False Then
                        rs.SetSuccessStatus("Record has been change status by other")
                        Return rs
                    End If

                    rs = prFacade.UpdateStatusApprPRWa(pr_header, ListEnum.ApprItemStatus.Approved.ToString(), "Waiting for approve", id)
                    If rs.IsSuccess Then
                        rs = prFacade.UpdateStatusDetailItemToComplete(id)
                        If rs.IsSuccess Then
                            If prFacade.IsAllGRItemNotOutStanding(pr_header) Then
                                If prFacade.IsAllGRRDCompleted(pr_header) Then
                                    rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrApprovedComplete, Nothing)
                                    If rs.IsSuccess Then
                                        rs = prFacade.SendEmailToEprocStaff(pr_header, ListEnum.eProcApprAction.handle.ToString())
                                        If rs.IsSuccess Then
                                            rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrApprovedComplete.ToString(), current_user_name, pr_header.PR_DATE)
                                        End If
                                    End If
                                Else
                                    rs = prFacade.SendEmailToApprRevRD(pr_header)
                                End If
                            End If

                            If rs.IsSuccess Then
                                rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprItemStatus.ApproveWA.ToString(), current_user_name, pr_header.PR_DATE)
                            End If

                            If rs.IsSuccess Then
                                scope.Complete()
                                rs.SetSuccessStatus("Data has been approved")
                            End If
                        End If
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            TempData("message") = rs.MessageText

            Return rs
        End Function
        Function ActionApproveReviewItemSelected(ByVal ids As Integer(), pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

                    rs = prFacade.UpdateStatusApprPRWaApprovalReviewSelected(pr_header, ids)

                    If rs.MessageText = "Record has been change status by other" Then
                        Return rs
                    End If

                    If rs.IsSuccess And ids.Length > 1 Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprItemStatus.ApproveWA.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        If prFacade.IsAllGRItemNotOutStanding(pr_header) Then
                            If prFacade.IsAllGRRDCompleted(pr_header) Then
                                rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrApprovedComplete, Nothing)
                                If rs.IsSuccess Then
                                    rs = prFacade.SendEmailToEprocStaff(pr_header, ListEnum.eProcApprAction.handle.ToString())
                                    If rs.IsSuccess Then
                                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrApprovedComplete.ToString(), current_user_name, pr_header.PR_DATE)
                                    End If
                                End If
                            Else
                                rs = prFacade.SendEmailToApprRevRD(pr_header)
                            End If
                        End If
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus("Data has been approved/review")
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            TempData("message") = rs.MessageText

            Return rs
        End Function
        Function DetailItemReject(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim pr_detail As TPROC_PR_DETAIL = db.TPROC_PR_DETAIL.Find(id)
            Return PartialView("_DetailItemReject", pr_detail)
        End Function
        Function ActionItemReject(ByVal id As Decimal, ByVal reason As String, ByVal status_appr_wa As String, pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

                    If prFacade.IsPrApprWaOustanding(id) = False Then
                        rs.SetSuccessStatus("Record has been change status by other")
                        Return rs
                    End If

                    rs = prFacade.UpdateStatusApprPRWa(pr_header, ListEnum.ApprItemStatus.Rejected.ToString(), status_appr_wa, id)

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprItemStatus.RejectWA.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        Dim sub_total_new As Decimal
                        rs = prFacade.UpdateStatusDetailItemToReject(reason, sub_total_new, id)
                        If prFacade.IsAllItemRejected(pr_header) Then
                            rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrRejected, reason)
                            If rs.IsSuccess Then
                                rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrRejected.ToString(), current_user_name, pr_header.PR_DATE)
                            End If
                        Else
                            rs = prFacade.UpdateSubTotalHeader(sub_total_new, pr_header.ID)
                            'Check is the all item dont have outstanding and there is item rejected and there is item is not rejected,, then check is there related department of not
                            If prFacade.IsAllGRItemNotOutStanding(pr_header) Then
                                If prFacade.IsAllGRRDCompleted(pr_header) Then
                                    rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrApprovedComplete, Nothing)
                                    If rs.IsSuccess Then
                                        rs = prFacade.SendEmailToEprocStaff(pr_header, ListEnum.eProcApprAction.handle.ToString())
                                        If rs.IsSuccess Then
                                            rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrApprovedComplete.ToString(), current_user_name, pr_header.PR_DATE)
                                        End If
                                    End If
                                Else
                                    rs = prFacade.SendEmailToApprRevRD(pr_header)
                                End If
                            End If
                        End If



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
        Function ActionItemRejectSelected(ByVal ids As Integer(), ByVal reject_reason As String, pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
                    rs = prFacade.UpdateStatusApprPRWaSelectedReject(pr_header, ids, reject_reason)

                    If rs.MessageText = "Record has been change status by other" Then
                        Return rs
                    End If

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprItemStatus.Rejected.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        If prFacade.IsAllItemRejected(pr_header) Then
                            rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrRejected, reject_reason)
                            If rs.IsSuccess Then
                                rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrRejected.ToString(), current_user_name, pr_header.PR_DATE)
                            End If
                        Else
                            'Check is the all item dont have outstanding and there is item rejected and there is item is not rejected,, then check is there related department of not
                            If prFacade.IsAllGRItemNotOutStanding(pr_header) Then
                                If prFacade.IsAllGRRDCompleted(pr_header) Then
                                    rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrApprovedComplete, Nothing)
                                    If rs.IsSuccess Then
                                        rs = prFacade.SendEmailToEprocStaff(pr_header, ListEnum.eProcApprAction.handle.ToString())
                                        If rs.IsSuccess Then
                                            rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrApprovedComplete.ToString(), current_user_name, pr_header.PR_DATE)
                                        End If
                                    End If
                                Else
                                    rs = prFacade.SendEmailToApprRevRD(pr_header)
                                End If
                            End If
                        End If

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
        Function ActionRejectPRByEproc(ByVal reject_reason As String, pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)

                    rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrRejected, reject_reason)
                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrRejected.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        rs = prFacade.SendEmailToUser_EPROC(pr_header.ID, pr_header.PR_NO, pr_header.PR_DATE, pr_header.TPROC_USER.USER_MAIL, ListEnum.PRStatus.PrRejected.ToString(), reject_reason)
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
        Private Function GetStatusApprWa(pr_dt_id As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim user_id As String = Session("USER_ID")
            Dim status_appr_wa As String = ""

            Try
                status_appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = pr_dt_id And x.USER_ID.ToUpper() = user_id.ToUpper()).FirstOrDefault().APPR_WA_STATUS
                rs.SetSuccessStatus(status_appr_wa)
            Catch ex As Exception
                rs.SetErrorStatus("You don't have permission access")
            End Try

            Return rs
        End Function
        Function IsPrApprWaOustanding(pr_detail_id As Decimal) As Boolean
            Dim result As Boolean = True
            Dim appr_wa As New List(Of TPROC_PR_APPR_WA)
            appr_wa = db.TPROC_PR_APPR_WA.Where(Function(x) x.PR_DETAIL_ID = pr_detail_id).ToList()

            For Each item In appr_wa
                If item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Approved.ToString() Or item.APPR_WA_STATUS = ListEnum.ApprItemStatus.Rejected.ToString() Then
                    result = False
                    Exit For
                Else
                    result = True
                End If
            Next

            Return result
        End Function
#End Region

#Region "RELATED DEPARTMENT"
        Function ActionRDReview(pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_header As New TPROC_PR_HEADER

            Using scope As New TransactionScope()
                Try
                    pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
                    rs = prFacade.UpdateStatusRDByReviewer(pr_header, ListEnum.ApprRDStatus.Reviewed.ToString(), "Waiting for review")

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprRDStatus.ReviewRD.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus("Data has been approved")
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " has been reviewed"
            Else
                TempData("message") = rs.MessageText
            End If

            Return rs
        End Function
        Function ActionRDApprove(pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_header As New TPROC_PR_HEADER

            Using scope As New TransactionScope()
                Try
                    pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
                    rs = prFacade.UpdateStatusRDByApprover(pr_header, ListEnum.ApprRDStatus.Approved.ToString(), "Waiting for approve")

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprRDStatus.ApproveRD.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                If prFacade.IsAllGRRDCompleted(pr_header) Then
                    rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrApprovedComplete, Nothing)
                    If rs.IsSuccess Then
                        rs = prFacade.SendEmailToEprocStaff(pr_header, ListEnum.eProcApprAction.handle.ToString())
                        If rs.IsSuccess Then
                            rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrApprovedComplete.ToString(), current_user_name, pr_header.PR_DATE)
                        End If
                    End If
                End If
            End If

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " has been approved"
            Else
                TempData("message") = rs.MessageText
            End If

            Return rs
        End Function
        Function ActionRDReject(ByVal reason As String, ByVal status_appr_rd As String, pr_header_id As Decimal) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim current_user_name As String = Session("USER_NAME")
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_header As New TPROC_PR_HEADER

            Using scope As New TransactionScope()
                Try
                    pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
                    rs = prFacade.UpdateStatusRDByRejecter(pr_header, ListEnum.ApprRDStatus.Rejected.ToString(), status_appr_rd, reason)
                    If rs.IsSuccess Then
                        rs = prFacade.UpdateStatusPR(pr_header, ListEnum.PRStatus.PrRejected, reason)
                        If rs.IsSuccess Then
                            rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrRejected.ToString(), current_user_name, pr_header.PR_DATE)
                            If rs.IsSuccess Then
                                rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.ApprRDStatus.RejectRD.ToString(), current_user_name, pr_header.PR_DATE)
                            End If

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

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " has been rejected"
            Else
                TempData("message") = rs.MessageText
            End If

            Return rs
        End Function
#End Region

#Region "Handle"
        <CAuthorize(Role:="MNU30")>
        Function ActionHandlePR(pr_header_id As Decimal) As ResultStatus
            Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim current_user_name As String = Session("USER_NAME")
            Dim pr_status As String = ""

            Using scope As New TransactionScope()
                Try

                    If pr_header.TPROC_GOOD_TYPE.GOOD_TYPE_NAME.ToUpper = "STOCK" Then
                        pr_status = ListEnum.PRStatus.Complete
                    Else
                        pr_status = ListEnum.PRStatus.PrHandled
                    End If

                    'change pr_status whatever goodtype. if for_storage is checked, so pr_status will be handled
                    If pr_header.FOR_STORAGE = 1 Then
                        pr_status = ListEnum.PRStatus.PrHandled
                    End If

                    rs = prFacade.UpdateStatusPR(pr_header, pr_status, Nothing)

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.PrHandled.ToString(), current_user_name, pr_header.PR_DATE)
                        If rs.IsSuccess And pr_status = ListEnum.PRStatus.Complete Then
                            rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.Complete.ToString(), current_user_name, pr_header.PR_DATE)
                            If rs.IsSuccess Then
                                rs = CuttingStockByPRCompleted(pr_header)
                            End If
                        End If
                    End If

                    'send email to user that the pr has been completed
                    If rs.IsSuccess And pr_header.TPROC_GOOD_TYPE.GOOD_TYPE_NAME.ToUpper = "STOCK" Then
                        rs = prFacade.SendEmailToUserPRCompleted(pr_header.ID, pr_header.PR_NO, pr_header.PR_DATE, pr_header.TPROC_USER.USER_MAIL, ListEnum.PRStatus.Complete.ToString(), pr_header.TPROC_FORM_TYPE.FORM_TYPE_NAME)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " " + ListEnum.PRStatus.PrHandled.ToString()
                If pr_header.TPROC_GOOD_TYPE.GOOD_TYPE_NAME.ToUpper = "STOCK" Then
                    TempData("message") = TempData("message") + " and " + ListEnum.PRStatus.Complete.ToString()
                End If
            Else
                TempData("message") = rs.MessageText
            End If

            Return rs
        End Function

        <CAuthorize(Role:="MNU30")>
        Function CuttingStockByPRCompleted(pr_header As TPROC_PR_HEADER) As ResultStatus
            Dim rs As New ResultStatus
            Using scope As New TransactionScope()
                Try
                    'Dim stock_mv = db.TPROC_STOCKMOVEMENT.ToList().Max()
                    Dim stock_mv_id_temp As Decimal = (From d In db.TPROC_STOCKMOVEMENT Select d.ID_TEMP).Max()

                    If stock_mv_id_temp > 0 Then
                        stock_mv_id_temp = stock_mv_id_temp + 1
                    Else
                        stock_mv_id_temp = 1
                    End If

                    For Each pr_detail In pr_header.TPROC_PR_DETAIL.ToList()
                        Dim cutting_stock = pr_detail.QTY
                        If pr_detail.REVISED_QTY IsNot Nothing Then
                            cutting_stock = pr_detail.REVISED_QTY.Value
                        End If
                        rs = UpdateQuantityStorageOfStock(pr_detail.ITEM_ID, cutting_stock, pr_header.PR_NO, stock_mv_id_temp)
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

        <CAuthorize(Role:="MNU30")>
        Function UpdateQuantityStorageOfStock(item_id As Decimal, cut_qty As Decimal, pr_number As String, id_temp As Decimal) As ResultStatus
            Dim rs As New ResultStatus

            Try
                Using db As New eProcurementEntities
                    Dim item_dt = db.TPROC_STOCK.Find(item_id)
                    Dim current_qty = item_dt.QUANTITY.Value
                    Dim cut_result = current_qty - cut_qty
                    item_dt.QUANTITY = cut_result
                    item_dt.LAST_MODIFIED_BY = Session("USER_ID")
                    item_dt.LAST_MODIFIED_TIME = Date.Now

                    db.Entry(item_dt).State = EntityState.Modified


                    Dim movement As New TPROC_STOCKMOVEMENT
                    Dim stock_mv_id_temp = (From d In db.TPROC_STOCKMOVEMENT Select d.ID).Max()
                    If stock_mv_id_temp > 0 Then
                        movement.ID = stock_mv_id_temp + 1
                    Else
                        movement.ID = 1
                    End If

                    movement.REFNO = pr_number
                    movement.ITEM_ID = item_id
                    movement.STOCK_OUT = cut_qty
                    movement.STOCK_CURRENT = current_qty
                    movement.STOCK_LAST = cut_result
                    movement.CREATED_BY = Session("USER_ID")
                    movement.CREATED_TIME = Date.Now
                    movement.ID_TEMP = id_temp
                    db.TPROC_STOCKMOVEMENT.Add(movement)

                    db.SaveChanges()
                    rs.SetSuccessStatus()
                End Using
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU24")>
        Function Revise(pr_header_id As Decimal) As ActionResult
            Dim id = pr_header_id
            Dim flag = Convert.ToInt32(ListEnum.FlagDetail.PRRevise)

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim user_id = Session("USER_ID")

            Dim pr_header As TPROC_PR_HEADER = db.TPROC_PR_HEADER.Find(id)
            Dim status_appr_rd_gr As String = ""
            Dim status_appr_rd_dt As String = ""
            Dim gr_id As Integer
            'Get Status pr approver/reviewer for approve/review in related department
            For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT
                    If dt.USER_ID.ToUpper() = user_id.ToUpper() Then
                        status_appr_rd_dt = dt.APPR_RELDEPT_STATUS 'status of approver/reviewer reldept
                        status_appr_rd_gr = gr.RELDEPT_GR_STATUS 'status of reldept group
                        gr_id = gr.ID
                        Exit For
                    End If
                Next
            Next

            ViewBag.StatusRDGR = status_appr_rd_gr
            ViewBag.StatusRDApprRev = status_appr_rd_dt

            'if still waiting reviewer, get the reviewer using gr_id
            Dim reviewer_still_waiting As New StringBuilder
            If status_appr_rd_gr Is Nothing Then
                For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                    For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT.Where(Function(x) x.PR_APPR_RELDEPT_GR_ID = gr_id And x.APPR_RELDEPT_STATUS = "Waiting for review")
                        reviewer_still_waiting.Append(dt.USER_ID)
                        reviewer_still_waiting.Append("/")
                    Next
                Next
            End If

            ViewBag.StillWaitingBy = reviewer_still_waiting.ToString()

            ViewBag.PRStatus = pr_header.PR_STATUS

            ViewBag.FlagAction = flag '0 : list pr, 1 : list wa appr, 2 : list reldept appr

            'set subtitle breadcrums
            ViewBag.SubTitle = "PR Revise"

            'get status pr for to know button handle is on or off
            ViewBag.StatusPR = pr_header.PR_STATUS

            ViewBag.Message = TempData("message")
            ViewBag.MessageError = TempData("messageError")

            ViewBag.PRIDFromRevise = pr_header_id

            Return View("DetailHeaderRevise", pr_header)
        End Function

        <CAuthorize(Role:="MNU24")>
        Function BackToHeaderFromRevise(pr_header_id As Decimal) As ActionResult
            Dim id = pr_header_id
            Dim flag = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToHandle)

            Return RedirectToAction("DetailHeader", New With {.id = id, .flag = flag})
        End Function

        <CAuthorize(Role:="MNU24")>
        Function ActionSaveRevise(ByVal pr_header_id As Decimal, ByVal sub_total_price As Decimal, ByVal litem_detail As String()) As ActionResult
            Dim header As New TPROC_PR_HEADER
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Using dbx As New eProcurementEntities
                        header = dbx.TPROC_PR_HEADER.Find(pr_header_id)
                        header.SUB_TOTAL = sub_total_price
                        header.LAST_MODIFIED_BY = Session("USER_ID")
                        header.LAST_MODIFIED_TIME = Date.Now
                        dbx.Entry(header).State = EntityState.Modified
                        dbx.SaveChanges()
                        rs.SetSuccessStatus()
                    End Using

                    For Each item As String In litem_detail
                        Dim arry = item.Split("|")
                        Dim pr_dt As New TPROC_PR_DETAIL

                        If arry(0) IsNot Nothing Then
                            Using dby As New eProcurementEntities
                                pr_dt = dby.TPROC_PR_DETAIL.Find(Convert.ToDecimal(arry(3)))
                                pr_dt.REVISED_QTY = Convert.ToDecimal(arry(0))
                                pr_dt.TOTAL_PRICE = Convert.ToDecimal(arry(1))
                                pr_dt.REMARK = arry(2).ToString()
                                pr_dt.LAST_MODIFIED_BY = Session("USER_ID")
                                pr_dt.LAST_MODIFIED_TIME = Date.Now
                                dby.Entry(pr_dt).State = EntityState.Modified
                                dby.SaveChanges()
                            End Using
                        End If
                        rs.SetSuccessStatus()
                    Next

                    rs = prFacade.InsertPRHistorical(pr_header_id, Date.Now, ListEnum.PRStatus.Revise.ToString(), Session("USER_NAME"), Date.Now)

                    If rs.IsSuccess Then
                        scope.Complete()
                        TempData("message") = "PR has been revised"
                        ViewBag.PRIDFromRevise = pr_header_id
                    End If

                Catch ex As Exception
                    TempData("messageError") = ex.Message
                End Try
            End Using

            Return RedirectToAction("Revise")
        End Function

        <CAuthorize(Role:="MNU24")>
        Function EditPRRemarkOrSupplier(pr_header_id As Decimal) As ActionResult
            Dim id = pr_header_id
            Dim flag = Convert.ToInt32(ListEnum.FlagDetail.PREdit)

            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim user_id = Session("USER_ID")

            Dim pr_header As TPROC_PR_HEADER = db.TPROC_PR_HEADER.Find(id)
            Dim status_appr_rd_gr As String = ""
            Dim status_appr_rd_dt As String = ""
            Dim gr_id As Integer
            'Get Status pr approver/reviewer for approve/review in related department
            For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT
                    If dt.USER_ID.ToUpper() = user_id.ToUpper() Then
                        status_appr_rd_dt = dt.APPR_RELDEPT_STATUS 'status of approver/reviewer reldept
                        status_appr_rd_gr = gr.RELDEPT_GR_STATUS 'status of reldept group
                        gr_id = gr.ID
                        Exit For
                    End If
                Next
            Next

            ViewBag.StatusRDGR = status_appr_rd_gr
            ViewBag.StatusRDApprRev = status_appr_rd_dt

            'if still waiting reviewer, get the reviewer using gr_id
            Dim reviewer_still_waiting As New StringBuilder
            If status_appr_rd_gr Is Nothing Then
                For Each gr In pr_header.TPROC_PR_APPR_RELDEPT_GR
                    For Each dt In gr.TPROC_PR_APPR_RELDEPT_DT.Where(Function(x) x.PR_APPR_RELDEPT_GR_ID = gr_id And x.APPR_RELDEPT_STATUS = "Waiting for review")
                        reviewer_still_waiting.Append(dt.USER_ID)
                        reviewer_still_waiting.Append("/")
                    Next
                Next
            End If

            ViewBag.StillWaitingBy = reviewer_still_waiting.ToString()

            ViewBag.PRStatus = pr_header.PR_STATUS

            ViewBag.FlagAction = flag '0 : list pr, 1 : list wa appr, 2 : list reldept appr

            'set subtitle breadcrums
            ViewBag.SubTitle = "PR Edit"

            'get status pr for to know button handle is on or off
            ViewBag.StatusPR = pr_header.PR_STATUS

            ViewBag.Message = TempData("message")
            ViewBag.MessageError = TempData("messageError")

            ViewBag.PRIDFromEdit = pr_header_id

            Return View("DetailHeaderEdit", pr_header)
        End Function

        <CAuthorize(Role:="MNU24")>
        Function BackToHeaderFromEditRemarkOrSupplier(pr_header_id As Decimal) As ActionResult
            Dim id = pr_header_id
            Dim flag = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToHandle)

            Return RedirectToAction("DetailHeader", New With {.id = id, .flag = flag})
        End Function

        <CAuthorize(Role:="MNU24")>
        Function ActionSaveEditPRRemarkOrSupplier(ByVal pr_header_id As Decimal, ByVal litem_detail As String()) As ActionResult
            Dim header As New TPROC_PR_HEADER
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            Using scope As New TransactionScope()
                Try
                    Using dbx As New eProcurementEntities
                        header = dbx.TPROC_PR_HEADER.Find(pr_header_id)
                        header.LAST_MODIFIED_BY = Session("USER_ID")
                        header.LAST_MODIFIED_TIME = Date.Now
                        dbx.Entry(header).State = EntityState.Modified
                        dbx.SaveChanges()
                        rs.SetSuccessStatus()
                    End Using

                    For Each item As String In litem_detail
                        Dim arry = item.Split("|")
                        Dim pr_dt As New TPROC_PR_DETAIL

                        If arry(0) IsNot Nothing Then
                            Using dby As New eProcurementEntities
                                pr_dt = dby.TPROC_PR_DETAIL.Find(Convert.ToDecimal(arry(2)))
                                pr_dt.REMARK = arry(0).ToString()
                                pr_dt.SUPPLIER_NAME = arry(1).ToString()
                                pr_dt.LAST_MODIFIED_BY = Session("USER_ID")
                                pr_dt.LAST_MODIFIED_TIME = Date.Now
                                dby.Entry(pr_dt).State = EntityState.Modified
                                dby.SaveChanges()
                            End Using
                        End If
                        rs.SetSuccessStatus()
                    Next

                    rs = prFacade.InsertPRHistorical(pr_header_id, Date.Now, ListEnum.PRStatus.Edit.ToString(), Session("USER_NAME"), Date.Now)

                    If rs.IsSuccess Then
                        scope.Complete()
                        TempData("message") = "PR has been edited"
                        ViewBag.PRIDFromEdit = pr_header_id
                    End If

                Catch ex As Exception
                    TempData("messageError") = ex.Message
                End Try
            End Using

            Return RedirectToAction("EditPRRemarkOrSupplier")
        End Function

#End Region

#Region "Ready To Create PO"
        <CAuthorize(Role:="MNU31")>
        Function ActionReadyToCreatePO(pr_header_id As Decimal) As ResultStatus
            Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim current_user_name As String = Session("USER_NAME")
            Dim pr_status As String = ""

            Using scope As New TransactionScope()
                Try
                    pr_status = ListEnum.PRStatus.CreatePo

                    rs = prFacade.UpdateStatusPR(pr_header, pr_status, Nothing)

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.CreatePo.ToString(), current_user_name, pr_header.PR_DATE)

                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " ready to create po"
            End If

            Return rs
        End Function
#End Region

#Region "Ready To Complete"
        <CAuthorize(Role:="MNU45")>
        Function ActionReadyToComplete(pr_header_id As Decimal) As ResultStatus
            Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim current_user_name As String = Session("USER_NAME")
            Dim pr_status As String = ""

            Using scope As New TransactionScope()
                Try
                    pr_status = ListEnum.PRStatus.Complete

                    rs = prFacade.UpdateStatusPR(pr_header, pr_status, Nothing)

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.Complete.ToString(), current_user_name, pr_header.PR_DATE)
                    End If

                    If rs.IsSuccess Then
                        rs = prFacade.SendEmailToUserPRCompleted(pr_header.ID, pr_header.PR_NO, pr_header.PR_DATE, pr_header.TPROC_USER.USER_MAIL, ListEnum.PRStatus.Complete.ToString(), pr_header.TPROC_FORM_TYPE.FORM_TYPE_NAME)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " has been completed"
            End If

            Return rs
        End Function
#End Region

#Region "Ready To Sign Off"
        <CAuthorize(Role:="MNU47")>
        Function SignOff(ByVal id As Decimal, ByVal flag As Integer) As ActionResult
            Dim pr_header = db.TPROC_PR_HEADER.Find(id)
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim current_user_name As String = Session("USER_NAME")
            Dim pr_status As String = ""

            ViewBag.PR_Header_id = pr_header.ID
            ViewBag.FlagDetail = flag

            'If CheckEvaluationOnPRDetail(pr_header.ID) Then
            '    ViewBag.AlreadyEvaluated = "1"
            'End If

            Dim listSupp As New ArrayList

            If pr_header.TPROC_GOOD_TYPE.GOOD_TYPE_NAME.Replace(" ", "").ToUpper() = "NONSTOCK" Then
                For Each item In pr_header.TPROC_PR_DETAIL.ToList()
                    If item.SUPPLIER_ID IsNot Nothing And item.EVALUATED_SUPPLIER = 0 Then
                        If listSupp.Contains(item.SUPPLIER_ID) = False Then
                            listSupp.Add(item.SUPPLIER_ID)
                        End If
                    End If
                Next
            End If

            If listSupp.Count = 0 Then
                Return View("_EvaluationSupplier")
            Else
                Dim supps As New List(Of TPROC_SUPPLIER)

                For Each supIdx In listSupp
                    Dim suppsxx = db.TPROC_SUPPLIER.Find(supIdx)
                    supps.Add(suppsxx)
                Next

                Return View("_EvaluationSupplier", supps)
            End If
        End Function

        Function ActionReadyToSignOff(ByVal pr_header_id As Decimal, ByVal FlagDetail As Integer, ByVal SubTitle As String) As ActionResult
            Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim current_user_name As String = Session("USER_NAME")
            Dim pr_status As String = ""

            Using scope As New TransactionScope()
                Try
                    pr_status = ListEnum.PRStatus.SignOff
                    rs = prFacade.UpdateStatusPR(pr_header, pr_status, Nothing)

                    If rs.IsSuccess Then
                        rs = prFacade.InsertPRHistorical(pr_header.ID, Date.Now, ListEnum.PRStatus.SignOff.ToString(), current_user_name, pr_header.PR_DATE)

                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                TempData("message") = "PR number " + pr_header.PR_NO + " has been sign off. Thank you and have a nice day"
                Dim header_param As New PR_HEADER_PARAM
                header_param.PRNoCompleted_temp = Nothing
                header_param.PRHeaderId_temp = Nothing
                header_param.PRFlag_temp = Nothing
            End If

            Return RedirectToAction("IndexListPR", New With {.FlagDetail = FlagDetail, .SubTitle = SubTitle})
        End Function

        Function ActionSaveEvaluation(ByVal pr_header_id As Decimal, ByVal supplier_id As Decimal, ByVal eval_detail As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade
            Dim current_user_name As String = Session("USER_NAME")
            Dim current_id As String = Session("USER_ID")

            Using scope As New TransactionScope()
                Try
                    rs = prFacade.InsertEvaluation(pr_header_id, supplier_id, eval_detail)
                    If rs.IsSuccess Then
                        rs = prFacade.UpdatePRDetailSupplier(pr_header_id, supplier_id)
                    End If
                    scope.Complete()
                Catch ex As Exception

                End Try
            End Using

            Return rs
        End Function

        Function CheckEvaluationOnPRDetail(pr_header_id) As Boolean
            Dim allEvaluation As Boolean = True

            Dim rs As New ResultStatus

            Try
                Using db As New eProcurementEntities
                    Dim pr_header = db.TPROC_PR_HEADER.Find(pr_header_id)
                    For Each item In pr_header.TPROC_PR_DETAIL.ToList()
                        If item.EVALUATED_SUPPLIER = 0 Then
                            allEvaluation = False
                            Exit For
                        End If
                    Next
                End Using
            Catch ex As Exception
            End Try

            Return allEvaluation
        End Function
#End Region

#Region "PUSH EMAIL"
        Public Function SentPushEmailByWaItem(pr_header_id As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            rs = prFacade.SendPushEmailApprRevWA(pr_header_id)
            Return rs
        End Function

        Public Function SentPushEmailByRD(pr_header_id As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim prFacade As New PurchasingRequestFacade

            rs = prFacade.SendPushEmailToApprRevRD(pr_header_id)
            Return rs
        End Function

        Function ActionReminderCompleteUser(ByVal ids As Integer()) As String
            Dim rs As New ResultStatus
            Dim r As String = ""
            Dim prFacade As New PurchasingRequestFacade
            Dim pr_status_complete = ListEnum.PRStatus.Complete.ToString()

            Using scope As New TransactionScope
                Try
                    For Each id In ids
                        Dim pr_header = db.TPROC_PR_HEADER.Find(id)
                        rs = prFacade.SendEmailToUserPRCompleted(pr_header.ID, pr_header.PR_NO, pr_header.PR_DATE, pr_header.TPROC_USER.USER_MAIL, pr_status_complete, pr_header.TPROC_FORM_TYPE.FORM_TYPE_NAME)
                    Next
                    scope.Complete()
                    rs.SetSuccessStatus()
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            If rs.IsSuccess Then
                TempData("message") = "Email has been sent"
                r = "True|" + TempData("message")
            Else
                TempData("message") = rs.MessageText
                r = "False|" + TempData("message")
            End If

            TempData("message") = TempData("message")

            Return r
        End Function
#End Region

#Region "OTHER"
        <CAuthorize(Role:="MNU24")>
        Function Download(ByVal id As Decimal) As FileResult
            Dim file_name As String = db.TPROC_PR_ATTACHMENT.Find(id).FILE_NAME
            Dim url_attach As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), file_name)

            Dim arry_content_type As String() = file_name.Split(".")
            Dim content_type As String = arry_content_type(arry_content_type.Length - 1)

            Return File(url_attach, "application/" + content_type, file_name)
        End Function

        Public Function InsertDocuments(formCollection As FormCollection, pr_header_id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Dim pr_id = pr_header_id

            Try
                If Request IsNot Nothing Then
                    Dim file As HttpPostedFileBase = Request.Files("UploadedFile")
                    If (file IsNot Nothing) AndAlso (file.ContentLength > 0) AndAlso Not String.IsNullOrEmpty(file.FileName) Then
                        Dim attach As String = System.IO.Path.GetFileName(file.FileName)
                        Dim path As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), attach)

                        Using db2 As New eProcurementEntities
                            Dim pr_attach As New TPROC_PR_ATTACHMENT
                            pr_attach.CREATED_BY = Session("USER_ID")
                            pr_attach.CREATED_TIME = Date.Now
                            pr_attach.FILE_NAME = attach
                            pr_attach.PR_HEADER_ID = pr_id

                            db2.TPROC_PR_ATTACHMENT.Add(pr_attach)
                            db2.SaveChanges()
                            rs.SetSuccessStatus()
                        End Using

                        'copy file to folder in application
                        file.SaveAs(path)
                        rs.SetSuccessStatus("Data has been uploaded")
                    Else
                        rs.SetErrorStatus("Please select the file before")
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("message") = rs.MessageText

            Return RedirectToAction("DetailHeader", New With {.id = pr_id, .flag = Convert.ToDecimal(ListEnum.FlagDetail.MyListPR)})
        End Function

        Function UserNotRegistered() As ActionResult
            Return PartialView("_UserNotRegistered")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
#End Region


    End Class
End Namespace