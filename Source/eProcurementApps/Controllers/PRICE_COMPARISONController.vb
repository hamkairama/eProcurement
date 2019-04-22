Imports eProcurementApps.Models
Imports eProcurementApps.Facade
Imports eProcurementApps.Helpers
Imports System.Net
Imports System.Transactions
Imports System.Data.Entity

Namespace Controllers
    Public Class PRICE_COMPARISONController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU60")>
        Function Create() As ActionResult
            ViewBag.Message = TempData("message")

            Return View()
        End Function

        <CAuthorize(Role:="MNU60")>
        Function RecommSupp(id As Decimal, sub_total As String, disc As String, vat As String, pph As String, grand_total As String) As ActionResult
            Dim db As New eProcurementEntities
            Dim supp = db.TPROC_SUPPLIER.Find(id)

            ViewBag.SubTotal = sub_total
            ViewBag.Disc = disc
            ViewBag.Vat = vat
            ViewBag.Pph = pph
            ViewBag.GrandTotal = grand_total

            Return PartialView("_RecommSupp", supp)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Dim db As New eProcurementEntities
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <HttpPost>
        <CAuthorize(Role:="MNU60")>
        Function ListPcByStatus(txt_status_pc_val As String) As ActionResult
            Dim r As String = ""
            TempData("message") = txt_status_pc_val

            If txt_status_pc_val = "" Then
                TempData("message") = "null"
            End If

            Return RedirectToAction("_ListPC")
        End Function

        <CAuthorize(Role:="MNU60")>
        Function _ListPC() As ActionResult
            Dim pc As New List(Of TPROC_PC)
            ViewBag.Message = TempData("message")

            If TempData("message") = "null" Then
                ViewBag.Message = "Please select status"
                Return View(pc)
            Else
                pc = GetDataPCByStatus(TempData("message"))
                ViewBag.Message = Nothing
            End If

            Return View(pc)
        End Function

        <CAuthorize(Role:="MNU60")>
        Function GetDataPCByStatus(txt_status_pc_val As String) As List(Of TPROC_PC)
            Dim pcx As New List(Of TPROC_PC)

            pcx = db.TPROC_PC.Where(Function(x) x.STATUS = txt_status_pc_val).ToList()

            Return pcx
        End Function

        <CAuthorize(Role:="MNU60")>
        Function DetailPc(id As Decimal, flag As Integer) As ActionResult
            Dim pcx As New TPROC_PC
            Dim pox As New TPROC_PO_HEADERS

            Try
                pcx = db.TPROC_PC.Find(id)
                pox = db.TPROC_PO_HEADERS.Where(Function(x) x.PC_ID = id).FirstOrDefault()

                If pox IsNot Nothing Then
                    ViewBag.POID = pox.ID
                    ViewBag.PONumber = pox.PO_NUMBER
                End If
                ViewBag.ViewOnly = ""
                If flag = ListEnum.FlagInbox.OnlyView Then
                    ViewBag.ViewOnly = "1"
                End If
                ViewBag.FlagInbox = flag
                ViewBag.Message = TempData("message")
            Catch ex As Exception
                ViewBag.Message = TempData(ex.Message)
            End Try


            Return View(pcx)
        End Function


        <CAuthorize(Role:="MNU60")>
        Function PopupAcknowledgeUser() As ActionResult

            Return PartialView("_PopupAcknowledgeUser")
        End Function

        <CAuthorize(Role:="MNU60")>
        Function ListApprovalWa(wa_id As Decimal) As ActionResult
            Dim list_appr_wa_dt As New List(Of TPROC_APPROVAL_DT)

            Dim gr_id = db.TPROC_WA.Find(wa_id).APPROVAL_GROUP_ID
            list_appr_wa_dt = db.TPROC_APPROVAL_DT.Where(Function(x) x.APPROVAL_GROUP_ID = gr_id).ToList()

            Return PartialView("_ListApprovalWa", list_appr_wa_dt)
        End Function

        <CAuthorize(Role:="MNU60")>
        Function ApproveAcknowledge(pc_id As Decimal, pc_dt As DateTime, user_id As String, grand_total As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Dim acknow As New List(Of TPROC_ACKNOW_APPR)
            Dim facade As New PriceComparisonFacade

            Try
                acknow = (From _acknow In db.TPROC_ACKNOW_APPR
                          Join _acknowDt In db.TPROC_ACKNOW_APPR_DT On _acknowDt.ACKNOW_APPR_ID Equals (_acknow.ID)
                          Where _acknow.PC_ID = pc_id And _acknowDt.USER_ID = user_id
                          Select _acknow).ToList() 'use list but get one record


                If acknow.Count > 0 Then
                    Using scope As New TransactionScope()
                        Using db2 As New eProcurementEntities
                            Dim acknow_update = db2.TPROC_ACKNOW_APPR.Find(acknow(0).ID)

                            For Each item In acknow_update.TPROC_ACKNOW_APPR_DT.ToList()
                                If item.USER_ID = user_id Then
                                    item.STATUS = "Approved"
                                Else
                                    item.STATUS = ""
                                End If

                                item.LAST_MODIFIED_BY = user_id
                                item.LAST_MODIFIED_TIME = Date.Now

                                db2.Entry(item).State = EntityState.Modified
                                db2.SaveChanges()
                                rs.SetSuccessStatus()
                            Next

                            If rs.IsSuccess Then
                                acknow_update.STATUS = "Approved"
                                acknow_update.LAST_MODIFIED_BY = user_id
                                acknow_update.LAST_MODIFIED_TIME = Date.Now
                                db2.Entry(acknow_update).State = EntityState.Modified
                                db2.SaveChanges()
                                rs.SetSuccessStatus()
                            End If
                        End Using

                        If rs.IsSuccess Then
                            If IsAllAcknowStatusApprove(pc_id) Then
                                rs = UpdateStatusPC(pc_id, ListEnum.PriceCom.ApprovedByAcknowledge.ToString())
                                If rs.IsSuccess Then
                                    rs = facade.InsertApprovalPcByApprovalRole(pc_id, grand_total)
                                    If rs.IsSuccess Then
                                        rs = facade.SendEmailApprPC(pc_id)
                                    End If
                                End If
                            End If
                        End If

                        If rs.IsSuccess Then
                            rs = facade.InsertPCHistorical(pc_id, Date.Now, ListEnum.PriceCom.ApprovedByAcknowledge.ToString(), Session("USER_NAME").ToString(), pc_dt)
                        End If

                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End Using

                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return RedirectToAction("DetailPc", New With {.id = pc_id, .flag = "0"})
        End Function

        <CAuthorize(Role:="MNU60")>
        Function UpdateStatusPC(pc_id As Decimal, status As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim pc As New TPROC_PC
            Dim db As New eProcurementEntities

            Try
                pc = db.TPROC_PC.Find(pc_id)

                If pc.STATUS = ListEnum.PriceCom.Rejected.ToString() Or (pc.STATUS = ListEnum.PriceCom.Verified.ToString() And status <> ListEnum.PriceCom.Approved.ToString()) Or pc.STATUS = status Then
                    rs.SetErrorStatus("Request is already updated status by other. Please refresh you browser")
                    Return rs
                End If

                pc.STATUS = status
                pc.LAST_MODIFIED_BY = Session("USER_ID")
                pc.LAST_MODIFIED_TIME = Date.Now
                db.Entry(pc).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU60")>
        Function UpdateStatusPCReject(pc_id As Decimal, status As String, reason As String, ByVal by_on As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim pc As New TPROC_PC
            Dim db As New eProcurementEntities

            Try
                pc = db.TPROC_PC.Find(pc_id)

                If status = ListEnum.PriceCom.Rejected.ToString() Then
                    If pc.STATUS = ListEnum.PriceCom.Verified.ToString() And by_on = "RejectedByVerifier" Then
                        rs.SetErrorStatus("Request is already verified by other. Please refresh your browser")
                        Return rs
                    End If

                    If pc.STATUS = ListEnum.PriceCom.Approved.ToString() And by_on = "RejectedByApprover" Then
                        rs.SetErrorStatus("Request is already approved by other. Please refresh your browser")
                        Return rs
                    End If

                    If pc.STATUS = ListEnum.PriceCom.Reviewed.ToString() And by_on = "RejectedByReviewer" Then
                        rs.SetErrorStatus("Request is already reviewed by other. Please refresh your browser")
                        Return rs
                    End If
                End If

                pc.STATUS = status
                pc.REJECT_REASON = reason
                pc.REJECT_BY = Session("USER_ID")
                pc.REJECT_DATE = Date.Now
                pc.LAST_MODIFIED_BY = Session("USER_ID")
                pc.LAST_MODIFIED_TIME = Date.Now
                db.Entry(pc).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU60")>
        Function UpdateStatusVrfPC(pc_id As Decimal, status As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim vrf_pc As New TPROC_VRF_PC
            Dim db As New eProcurementEntities

            Try
                Dim user_id As String = Session("USER_ID").ToString()
                vrf_pc = db.TPROC_VRF_PC.Where(Function(x) x.PC_ID = pc_id And x.USER_ID = user_id).FirstOrDefault()
                vrf_pc.STATUS = status
                vrf_pc.LAST_MODIFIED_BY = Session("USER_ID")
                vrf_pc.LAST_MODIFIED_TIME = Date.Now
                db.Entry(vrf_pc).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU60")>
        Function IsAllAcknowStatusApprove(pc_id As Decimal) As Boolean
            Dim result As Boolean = False
            Dim acknows As New List(Of TPROC_ACKNOW_APPR)

            Try
                Using db_ack_appr As New eProcurementEntities
                    acknows = db_ack_appr.TPROC_ACKNOW_APPR.Where(Function(x) x.PC_ID = pc_id).ToList()
                    If acknows.Count > 0 Then
                        For Each item In acknows
                            If item.STATUS.ToUpper() <> "APPROVED" Then
                                result = False
                                Exit For
                            Else
                                result = True
                            End If
                        Next
                    End If
                End Using

            Catch ex As Exception
            End Try

            Return result
        End Function

        <CAuthorize(Role:="MNU60")>
        Function Review(pc_id As Decimal, pc_dt As DateTime, user_id As String, status As String) As ActionResult
            Dim rs As New ResultStatus
            Dim appr As New TPROC_APPR_PC
            Dim FacadePC As New PriceComparisonFacade
            Dim FacadePO As New PurchaseOrderFacade

            Try
                Using scope As New TransactionScope()
                    Using dbEnt As New eProcurementEntities
                        appr = dbEnt.TPROC_APPR_PC.Where(Function(x) x.PC_ID = pc_id And x.USER_ID = user_id And x.STATUS = "Waiting for review").FirstOrDefault()

                        If appr.ID > 0 Then
                            appr.STATUS = status
                            appr.LAST_MODIFIED_BY = user_id
                            appr.LAST_MODIFIED_TIME = Date.Now

                            dbEnt.Entry(appr).State = EntityState.Modified
                            dbEnt.SaveChanges()
                            rs.SetSuccessStatus()
                        End If

                    End Using

                    If rs.IsSuccess Then
                        rs = FacadePC.InsertPCHistorical(pc_id, Date.Now, status, user_id, pc_dt)
                    End If

                    If rs.IsSuccess Then
                        rs = UpdateStatusPC(pc_id, status)
                    End If

                    If rs.IsSuccess Then
                        rs = UpdatePoByPc(pc_id, user_id, status)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                End Using
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try



            TempData("message") = rs.MessageText

            Return RedirectToAction("DetailPc", New With {.id = pc_id, .flag = "0"})
        End Function

        Function UpdatePoByPc(pc_id As Decimal, user_id As String, status_new As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim poFacade As New PurchaseOrderFacade
            Dim appr_po_id As Decimal
            Dim vrf_po_id As Decimal

            Dim po_header = db.TPROC_PO_HEADERS.Where(Function(x) x.PC_ID = pc_id).FirstOrDefault()

            If po_header.ID > 0 Then
                rs = poFacade.UpdateStatusPO(po_header, status_new, user_id, appr_po_id, vrf_po_id)

                If rs.IsSuccess And status_new <> ListEnum.PO.Verified.ToString() Then
                    rs = poFacade.UpdateStatusApprPO(appr_po_id, user_id, status_new)
                End If

                If rs.IsSuccess And status_new = ListEnum.PO.Verified.ToString() Then
                    rs = poFacade.UpdateStatusVrfPO(appr_po_id, user_id, status_new)
                End If
            End If

            Return rs
        End Function

        <CAuthorize(Role:="MNU60")>
        Function Approve(pc_id As Decimal, pc_dt As DateTime, user_id As String, status As String) As ActionResult
            Dim rs As New ResultStatus
            Dim appr_list As New List(Of TPROC_APPR_PC)
            Dim appr As New TPROC_APPR_PC
            Dim FacadePC As New PriceComparisonFacade
            Dim FacadePO As New PurchaseOrderFacade
            Dim is_reviewed As Boolean = True

            Try
                Using scope As New TransactionScope()
                    Using dbEnt As New eProcurementEntities
                        appr_list = dbEnt.TPROC_APPR_PC.Where(Function(x) x.PC_ID = pc_id).ToList()

                        'check if there is reviewer and status is still Waiting for review
                        is_reviewed = CheckReviewer(appr_list, status)

                        If is_reviewed = True Then
                            For Each item In appr_list
                                If item.PC_ID = pc_id And item.USER_ID = user_id Then
                                    item.STATUS = status
                                    item.LAST_MODIFIED_BY = user_id
                                    item.LAST_MODIFIED_TIME = Date.Now

                                    dbEnt.Entry(item).State = EntityState.Modified
                                    dbEnt.SaveChanges()
                                    rs.SetSuccessStatus()
                                End If
                            Next
                        Else
                            rs.SetErrorStatus("Someone doesn't review yet. Please wait until reviewed")
                        End If

                    End Using

                    If rs.IsSuccess Then
                        rs = FacadePC.InsertPCHistorical(pc_id, Date.Now, status, user_id, pc_dt)
                    End If

                    If rs.IsSuccess Then
                        rs = UpdateStatusPC(pc_id, status)
                    End If

                    If rs.IsSuccess Then
                        rs = UpdatePoByPc(pc_id, user_id, status)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                End Using
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("message") = rs.MessageText

            Return RedirectToAction("DetailPc", New With {.id = pc_id, .flag = "0"})
        End Function

        <CAuthorize(Role:="MNU60")>
        Function CheckReviewer(appr_list As List(Of TPROC_APPR_PC), status As String) As Boolean
            Dim is_reviewed As Boolean = True
            If status = "Approved" Then
                For Each item In appr_list
                    If item.AS_IS = "Reviewer" And item.STATUS = "Waiting for review" Then
                        is_reviewed = False
                    End If
                Next
            End If

            Return is_reviewed
        End Function

        <CAuthorize(Role:="MNU60")>
        Function Complete(pc_id As Decimal, pc_dt As DateTime, user_id As String, status As String) As ActionResult
            Dim rs As New ResultStatus
            Dim appr As New TPROC_APPR_PC
            Dim FacadePC As New PriceComparisonFacade
            Dim FacadePO As New PurchaseOrderFacade

            Try
                Using scope As New TransactionScope()
                    rs = FacadePC.InsertPCHistorical(pc_id, Date.Now, status, user_id, pc_dt)

                    If rs.IsSuccess Then
                        rs = UpdateStatusPC(pc_id, status)
                    End If

                    If rs.IsSuccess Then
                        rs = FacadePO.UpdateStatusPOFromPC(pc_id, ListEnum.PO.Completed)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                End Using
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("message") = rs.MessageText

            Return RedirectToAction("DetailPc", New With {.id = pc_id, .flag = "0"})
        End Function

        <CAuthorize(Role:="MNU60")>
        Function Verify(pc_id As Decimal, pc_dt As DateTime, user_id As String, status As String) As ActionResult
            Dim rs As New ResultStatus
            Dim appr As New TPROC_APPR_PC
            Dim FacadePC As New PriceComparisonFacade
            Dim FacadePO As New PurchaseOrderFacade

            Try
                Using scope As New TransactionScope()
                    rs = FacadePC.InsertPCHistorical(pc_id, Date.Now, status, user_id, pc_dt)

                    If rs.IsSuccess Then
                        rs = UpdateStatusPC(pc_id, status)
                    End If

                    If rs.IsSuccess Then
                        rs = FacadePO.UpdateStatusPOFromPC(pc_id, ListEnum.PO.Verified)
                    End If

                    If rs.IsSuccess Then
                        rs = UpdateStatusVrfPC(pc_id, status)
                    End If

                    Dim exist_acknow As Boolean
                    If rs.IsSuccess Then
                        rs = FacadePC.SendEmailApprAcknowUser(pc_id, exist_acknow)
                    End If

                    If rs.IsSuccess And exist_acknow = False Then
                        rs = FacadePC.SendEmailApprPC(pc_id)
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                End Using
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("message") = rs.MessageText

            Return RedirectToAction("DetailPc", New With {.id = pc_id, .flag = "0"})
        End Function

        <CAuthorize(Role:="MNU60")>
        Function Reject(pc_id As Decimal, pc_dt As DateTime, reason As String, by_on As String) As ActionResult
            Dim rs As New ResultStatus
            Dim appr As New TPROC_APPR_PC
            Dim FacadePC As New PriceComparisonFacade
            Dim FacadePO As New PurchaseOrderFacade
            Dim user_id = Session("USER_ID")
            Dim status = ListEnum.PriceCom.Rejected.ToString()

            Try
                Using scope As New TransactionScope()
                    rs = FacadePC.InsertPCHistorical(pc_id, Date.Now, status, user_id, pc_dt)

                    If rs.IsSuccess Then
                        rs = UpdateStatusPCReject(pc_id, status, reason, by_on)
                    End If

                    If rs.IsSuccess Then
                        rs = FacadePO.UpdateStatusPOFromPC(pc_id, ListEnum.PO.Rejected)
                    End If

                    If rs.IsSuccess Then
                        rs = FacadePC.SendEmailCreatorPCNotif(pc_id, ListEnum.PriceCom.Rejected.ToString())
                    End If

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If
                End Using
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("message") = rs.MessageText

            Return RedirectToAction("DetailPc", New With {.id = pc_id, .flag = "0"})
        End Function

        Function Download(ByVal id As Decimal) As FileResult
            Dim file_name As String = db.TPROC_PC.Find(id).FILE_NAME
            Dim url_attach As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), file_name)

            Dim arry_content_type As String() = file_name.Split(".")
            Dim content_type As String = arry_content_type(arry_content_type.Length - 1)

            Return File(url_attach, "application/" + content_type, file_name)
        End Function


        Public Function SentPushEmailByPC(pc_id As Decimal, pc_number As String, pc_date As String, pc_type As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim FacadePC As New PriceComparisonFacade
            Dim prFacade As New PurchasingRequestFacade

            Dim email_user = Session("USER_MAIL")
            rs = FacadePC.SendEmailApprPC(pc_id)
            If rs.IsSuccess Then
                rs = prFacade.SendPushEmailToUser(pc_id, pc_number, pc_date, email_user, pc_type, "LinkPC")
            End If

            Return rs
        End Function

    End Class
End Namespace

