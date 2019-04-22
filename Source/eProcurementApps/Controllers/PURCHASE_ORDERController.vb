Imports eProcurementApps.Models
Imports eProcurementApps.Facade
Imports eProcurementApps.Helpers
Imports eProcurementApps.DataAccess
Imports System.Net
Imports System.Transactions
Imports Microsoft.Reporting.WebForms
Imports System.Data.Entity
Imports System.IO
Imports System.Text
Imports System.Diagnostics

Namespace Controllers
    Public Class PURCHASE_ORDERController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "MNU28"
        <CAuthorize(Role:="MNU28")>
        Function Create() As ActionResult
            ViewBag.Message = TempData("message")

            Return View()
        End Function

        <CAuthorize(Role:="MNU28")>
        Function ActionRejectPO(ByVal po_id As Decimal, ByVal po_number As String, ByVal pc_id As Decimal, ByVal reason As String, ByVal by_on As String) As String

            Dim rs As New ResultStatus
            Dim r As String = ""
            Dim poFacade As New PurchaseOrderFacade

            Using scope As New TransactionScope()
                rs = poFacade.UpdatePRByPoReject(po_number)
                If rs.IsSuccess Then
                    Dim status_po = ListEnum.PO.Rejected
                    rs = poFacade.UpdateStatusPORejected(po_id, status_po, by_on)
                End If

                If rs.IsSuccess And pc_id > 0 Then
                    Dim status_pc = ListEnum.PriceCom.Rejected.ToString()
                    rs = poFacade.UpdateStatusPC(pc_id, status_pc)
                End If

                If rs.IsSuccess Then
                    Dim po = db.TPROC_PO_HEADERS.Find(po_id)
                    po.REJECT_REASON = reason
                    po.REJECT_BY = Session("USER_ID")
                    po.REJECT_DATE = Date.Now
                    db.Entry(po).State = EntityState.Modified
                    db.SaveChanges()
                    rs.SetSuccessStatus()
                End If

                If rs.IsSuccess Then
                    rs = poFacade.SendEmailCreatorPONotif(po_id, ListEnum.PO.Rejected.ToString())
                End If

                If rs.IsSuccess Then
                    scope.Complete()
                    rs.SetSuccessStatus()
                End If
            End Using

            If rs.IsSuccess Then
                r = "True|" + TempData("message")
            Else
                TempData("message") = rs.MessageText
                r = "False|" + TempData("message")
            End If

            TempData("message") = TempData("message")

            Return r
        End Function

        <CAuthorize(Role:="MNU28")>
        Function ActionCreatePO(ByVal cb_dropdownList_potyp_id As Decimal, ByVal cb_dropdownList_potyp_nm As String,
                              ByVal prepared_by As String,
                              ByVal notes As String, ByVal created_dt As Date,
                              ByVal cb_dropdownList_currency_id As Decimal, ByVal approved_by As String,
                              ByVal cb_dropdownList_supplier_nm_id As Decimal,
                              ByVal supplier_nm As String,
                              ByVal phone_supplier As String, ByVal address_supplier As String,
                              ByVal contact_person_supplier As String, ByVal fax_supplier As String,
                              ByVal cb_dropdownList_delivery_nm_id As Decimal, ByVal delivery_nm As String,
                              ByVal phone_delivery As String, ByVal address_delivery As String,
                              ByVal delivery_dt_new As Date, ByVal fax_delivery As String,
                              ByVal sub_total As Decimal, ByVal discount As Decimal, ByVal vat As Decimal,
                              ByVal wth_tax_pph As Decimal, ByVal grand_total As Decimal,
                              ByVal litem_detail As String(), ByVal base_from As String, ByVal for_storage As Integer,
                              ByVal is_disc_perc As Integer, ByVal is_vat_perc As Integer, ByVal is_pph_perc As Integer,
                              ByVal discount_temp As Decimal, ByVal vat_temp As Decimal, ByVal pph_temp As Decimal) As String

            Dim rs As New ResultStatus
            Dim r As String = ""
            Dim po_header As New TPROC_PO_HEADERS

            po_header.PO_NUMBER = Generate.GetPOSeq("TPROC_PO_HEADERS") + cb_dropdownList_potyp_nm
            po_header.CREATE_DATE = created_dt
            po_header.PO_TYPE_ID = cb_dropdownList_potyp_id
            po_header.CURRENCY = cb_dropdownList_currency_id
            po_header.PO_STATUS = ListEnum.PO.Submitted
            po_header.PREPARED_BY = prepared_by
            po_header.APPOROVED_BY = approved_by
            po_header.NOTES = notes
            po_header.SUPPLIER_NAME = supplier_nm
            po_header.SUPPLIER_ID = cb_dropdownList_supplier_nm_id
            po_header.CONTACT_PERSON = contact_person_supplier
            po_header.SUPPLIER_PHONE = phone_supplier
            po_header.SUPPLIER_FAX = fax_supplier
            po_header.SUPPLIER_ADDTRSS = address_supplier
            po_header.DELIVERY_NAME = delivery_nm
            po_header.DELIVERY_ID = cb_dropdownList_delivery_nm_id
            po_header.DELIVERY_DATE = delivery_dt_new
            po_header.DELIVERY_PHONE = phone_delivery
            po_header.DELIVERY_FAX = fax_delivery
            po_header.DELIVERY_ADDRESS = address_delivery
            po_header.SUB_TOTAL = sub_total
            po_header.DSCNT_AMT = discount
            po_header.VAT = vat
            po_header.WTH_TAX = wth_tax_pph
            po_header.GRAND_TOTAL = grand_total
            po_header.CREATED_TIME = Date.Now
            po_header.CREATED_BY = Session("USER_ID")
            po_header.LAST_MODIFIED_TIME = Nothing
            po_header.LAST_MODIFIED_BY = ""
            po_header.ROW_STATUS = ListEnum.RowStat.Live
            po_header.FOR_STORAGE = for_storage
            po_header.IS_DISC_PERC = is_disc_perc
            po_header.IS_VAT_PERC = is_vat_perc
            po_header.IS_PPH_PERC = is_pph_perc
            po_header.DISCOUNT_TEMP = discount_temp
            po_header.VAT_TEMP = vat_temp
            po_header.PPH_TEMP = pph_temp

            Dim priceComNum As String = ""

            rs = InsertPO(po_header, litem_detail, base_from)

            If rs.IsSuccess Then
                If priceComNum = "" Then
                    TempData("message") = "PO number " + po_header.PO_NUMBER + " has been created"
                Else
                    TempData("message") = "PO number " + po_header.PO_NUMBER + " and PriceCom number " + priceComNum + " has been created"
                End If
                r = "True|" + TempData("message")
            Else
                TempData("message") = rs.MessageText
                r = "False|" + TempData("message")
            End If


            TempData("message") = TempData("message")
            'Return Json(TempData("message"))

            Return r
        End Function

        <CAuthorize(Role:="MNU28")>
        Function ActionCreatePC(ByVal cb_dropdownList_potyp_id As Decimal, ByVal cb_dropdownList_potyp_nm As String,
                              ByVal prepared_by As String,
                              ByVal notes As String, ByVal created_dt As DateTime,
                              ByVal cb_dropdownList_currency_id As Decimal, ByVal approved_by As String,
                              ByVal cb_dropdownList_supplier_nm_id As Decimal,
                              ByVal supplier_nm As String,
                              ByVal phone_supplier As String, ByVal address_supplier As String,
                              ByVal contact_person_supplier As String, ByVal fax_supplier As String,
                              ByVal cb_dropdownList_delivery_nm_id As Decimal, ByVal delivery_nm As String,
                              ByVal phone_delivery As String, ByVal address_delivery As String,
                              ByVal delivery_dt_new As DateTime, ByVal fax_delivery As String,
                              ByVal sub_total As Decimal, ByVal discount As Decimal, ByVal vat As Decimal,
                              ByVal wth_tax_pph As Decimal, ByVal grand_total As Decimal,
                              ByVal litem_detail As String(), ByVal base_from As String, ByVal for_storage As Integer,
                              ByVal ObjectsPCx As List(Of OBJECTSPC), ByVal listOfObjectsHeaderx As List(Of OBJECTSHEADER),
                              ByVal listOfObjectsBodyx As List(Of OBJECTSBODY),
                              ByVal listMappingSuppItemx As List(Of MAPPINGSUPPITEM), ByVal listOfObjectsFooterx As List(Of OBJECTSFOOTER),
                              ByVal listOfObjectsWax As List(Of OBJECTSWA),
                              ByVal is_disc_perc As Integer, ByVal is_vat_perc As Integer, ByVal is_pph_perc As Integer,
                              ByVal discount_temp As Decimal, ByVal vat_temp As Decimal, ByVal pph_temp As Decimal) As String

            Dim rs As New ResultStatus
            Dim r As String = ""
            Dim po_header As New TPROC_PO_HEADERS

            po_header.PO_NUMBER = Generate.GetPOSeq("TPROC_PO_HEADERS") + cb_dropdownList_potyp_nm
            po_header.CREATE_DATE = created_dt
            po_header.PO_TYPE_ID = cb_dropdownList_potyp_id
            po_header.CURRENCY = cb_dropdownList_currency_id
            po_header.PO_STATUS = ListEnum.PO.Submitted
            po_header.PREPARED_BY = prepared_by
            po_header.APPOROVED_BY = approved_by
            po_header.NOTES = notes
            po_header.SUPPLIER_NAME = supplier_nm
            po_header.SUPPLIER_ID = cb_dropdownList_supplier_nm_id
            po_header.CONTACT_PERSON = contact_person_supplier
            po_header.SUPPLIER_PHONE = phone_supplier
            po_header.SUPPLIER_FAX = fax_supplier
            po_header.SUPPLIER_ADDTRSS = address_supplier
            po_header.DELIVERY_NAME = delivery_nm
            po_header.DELIVERY_ID = cb_dropdownList_delivery_nm_id
            po_header.DELIVERY_DATE = delivery_dt_new
            po_header.DELIVERY_PHONE = phone_delivery
            po_header.DELIVERY_FAX = fax_delivery
            po_header.DELIVERY_ADDRESS = address_delivery
            po_header.SUB_TOTAL = sub_total
            po_header.DSCNT_AMT = discount
            po_header.VAT = vat
            po_header.WTH_TAX = wth_tax_pph
            po_header.GRAND_TOTAL = grand_total
            po_header.CREATED_TIME = Date.Now
            po_header.CREATED_BY = Session("USER_ID")
            po_header.LAST_MODIFIED_TIME = Nothing
            po_header.LAST_MODIFIED_BY = ""
            po_header.ROW_STATUS = ListEnum.RowStat.Live
            po_header.FOR_STORAGE = for_storage
            po_header.IS_DISC_PERC = is_disc_perc
            po_header.IS_VAT_PERC = is_vat_perc
            po_header.IS_PPH_PERC = is_pph_perc
            po_header.DISCOUNT_TEMP = discount_temp
            po_header.VAT_TEMP = vat_temp
            po_header.PPH_TEMP = pph_temp

            Dim priceComNum As String = ""

            rs = InsertPC(po_header, litem_detail, base_from, ObjectsPCx, listOfObjectsHeaderx, listOfObjectsBodyx, listMappingSuppItemx, listOfObjectsFooterx, priceComNum, listOfObjectsWax, cb_dropdownList_potyp_nm)

            If rs.IsSuccess Then
                If priceComNum = "" Then
                    TempData("message") = "PO number " + po_header.PO_NUMBER + " has been created"
                Else
                    TempData("message") = "PO number " + po_header.PO_NUMBER + " and PriceCom number " + priceComNum + " has been created"
                End If
                r = "True|" + TempData("message")
            Else
                TempData("message") = rs.MessageText
                r = "False|" + TempData("message")
            End If


            TempData("message") = TempData("message")
            'Return Json(TempData("message"))

            Return r
        End Function

        Public Sub GetFormData(file As IEnumerable(Of HttpPostedFileBase))
            CDataImage.DataFiles(file)
        End Sub

        Public Function InsertPcAttachment(files As IEnumerable(Of HttpPostedFileBase), ByRef file_name As String) As ResultStatus
            Dim rs As New ResultStatus

            Try
                If files IsNot Nothing Then
                    For Each item As HttpPostedFileBase In files
                        Dim file As HttpPostedFileBase = item
                        Dim attach As String = System.IO.Path.GetFileName(file.FileName)
                        Dim path As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), attach)
                        file_name = attach
                        Dim pr_attach As New TPROC_PR_ATTACHMENT
                        pr_attach.FILE_NAME = attach
                        file.SaveAs(path)
                    Next
                End If
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        Public Function InsertDocuments(formCollection As FormCollection) As ActionResult
            Dim rs As New ResultStatus
            Dim po_id = PO_HEADER_PARAM.GetPOHeaderId

            Try
                If Request IsNot Nothing Then
                    Dim file As HttpPostedFileBase = Request.Files("UploadedFile")
                    If (file IsNot Nothing) AndAlso (file.ContentLength > 0) AndAlso Not String.IsNullOrEmpty(file.FileName) Then
                        Dim attach As String = System.IO.Path.GetFileName(file.FileName)
                        Dim path As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), attach)

                        Using db2 As New eProcurementEntities

                            Dim po_header = db2.TPROC_PO_HEADERS.Find(po_id)
                            po_header.CREATED_BY = Session("USER_ID")
                            po_header.CREATED_TIME = Date.Now
                            po_header.FILE_NAME = attach

                            db2.Entry(po_header).State = EntityState.Modified
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

            TempData("msg") = rs.MessageText

            Return RedirectToAction("DetailHeader", New With {.id = po_id})
        End Function

        Function Download(ByVal id As Decimal) As FileResult
            Dim file_name As String = db.TPROC_PO_HEADERS.Find(id).FILE_NAME
            Dim url_attach As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), file_name)

            Dim arry_content_type As String() = file_name.Split(".")
            Dim content_type As String = arry_content_type(arry_content_type.Length - 1)

            Return File(url_attach, "application/" + content_type, file_name)
        End Function

        <CAuthorize(Role:="MNU28")>
        Public Function InsertPO(ByVal po_header As TPROC_PO_HEADERS, ByVal litem_detail As String(),
                                 ByVal base_from As String) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim rs As New ResultStatus
            Dim poFacade As New PurchaseOrderFacade
            Dim pcFacade As New PriceComparisonFacade
            Dim po_header_id As Decimal

            Using scope As New TransactionScope()
                Try

                    '-------------------------------------------start code for po--------------------------------------------------------
                    If (base_from = "1" And rs.IsSuccess) Or base_from = "0" Then
                        rs = poFacade.InsertPOHeader(current_user_id, po_header, po_header_id)
                    End If

                    If rs.IsSuccess Then
                        'cek List Item PO
                        rs = ListItemPO(litem_detail, po_header_id, po_header)
                    End If

                    If rs.IsSuccess Then
                        Generate.CommitGenerator("TPROC_PO_HEADERS")
                    End If

                    'ro verify
                    If rs.IsSuccess Then
                        rs = InsertVerifyPoByApprovalRole(po_header_id)
                    End If

                    If rs.IsSuccess Then
                        rs = InsertApprovalPoByApprovalRole(po_header_id, po_header.GRAND_TOTAL)
                    End If

                    If base_from = "0" And rs.IsSuccess Then
                        rs = poFacade.SendEmailVrf(po_header_id)
                    End If
                    '-------------------------------------------End code for pc--------------------------------------------------------


                    '-------------------------------------------start insert attachment--------------------------------------------------------
                    '-------------------------------------------end insert attachment--------------------------------------------------------

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message & "|| /n" & ex.StackTrace.ToString() & "|| /n" & ex.InnerException.ToString() & "|| /n" & ex.Source)
                End Try
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU28")>
        Public Function InsertPC(ByVal po_header As TPROC_PO_HEADERS, ByVal litem_detail As String(),
                                 ByVal base_from As String, ByVal ObjectsPCx As List(Of OBJECTSPC),
                                 ByVal listOfObjectsHeaderx As List(Of OBJECTSHEADER), ByVal listOfObjectsBodyx As List(Of OBJECTSBODY),
                                 ByVal listMappingSuppItemx As List(Of MAPPINGSUPPITEM), ByVal listOfObjectsFooterx As List(Of OBJECTSFOOTER),
                                 ByRef priceComNum As String, ByVal listOfObjectsWax As List(Of OBJECTSWA),
                                 ByVal cb_dropdownList_potyp_nm As String) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim rs As New ResultStatus
            Dim poFacade As New PurchaseOrderFacade
            Dim pcFacade As New PriceComparisonFacade
            Dim po_header_id As Decimal
            Dim pc_id As Decimal

            Using scope As New TransactionScope()
                Try
                    '-------------------------------------------start code for pc--------------------------------------------------------
                    If base_from = "1" Then
                        Dim for_storage = po_header.FOR_STORAGE
                        Dim file_name As String = ""
                        Dim files As IEnumerable(Of HttpPostedFileBase)
                        files = CDataImage.GetImageFiles
                        rs = InsertPcAttachment(files, file_name)

                        If rs.IsSuccess Then
                            rs = pcFacade.InsertPriceCom(ObjectsPCx(0), listOfObjectsHeaderx, listOfObjectsBodyx, listMappingSuppItemx, listOfObjectsFooterx, priceComNum, pc_id, for_storage, file_name, cb_dropdownList_potyp_nm)
                            po_header.PC_ID = pc_id
                        End If

                        'ro verify
                        If rs.IsSuccess Then
                            rs = pcFacade.InsertVerifyPcByApprovalRole(pc_id)
                        End If

                        If rs.IsSuccess And ObjectsPCx(0).Is_acknowledge_user = "1" Then
                            rs = pcFacade.InsertAcknowledgePc(listOfObjectsWax, pc_id, current_user_id)
                        ElseIf rs.IsSuccess And ObjectsPCx(0).Is_acknowledge_user = "0" Then
                            rs = pcFacade.InsertApprovalPcByApprovalRole(pc_id, Convert.ToDecimal(ObjectsPCx(0).Grand_total))
                        End If

                    End If
                    '-------------------------------------------End code for pc--------------------------------------------------------


                    '-------------------------------------------start code for po--------------------------------------------------------
                    If (base_from = "1" And rs.IsSuccess) Or base_from = "0" Then
                        rs = poFacade.InsertPOHeader(current_user_id, po_header, po_header_id)
                    End If

                    If rs.IsSuccess Then
                        'cek List Item PO
                        rs = ListItemPO(litem_detail, po_header_id, po_header)
                    End If

                    If rs.IsSuccess Then
                        Generate.CommitGenerator("TPROC_PO_HEADERS")
                    End If

                    'ro verify
                    If rs.IsSuccess Then
                        rs = InsertVerifyPoByApprovalRole(po_header_id)
                    End If

                    If rs.IsSuccess Then
                        rs = InsertApprovalPoByApprovalRole(po_header_id, po_header.GRAND_TOTAL)
                    End If

                    If base_from = "0" And rs.IsSuccess Then
                        rs = poFacade.SendEmailVrf(po_header_id)
                    End If
                    '-------------------------------------------End code for pc--------------------------------------------------------

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message & "|| /n" & ex.StackTrace.ToString() & "|| /n" & ex.InnerException.ToString() & "|| /n" & ex.Source)
                End Try
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU28")>
        Public Function ListItemPO(ByVal litem_detail As String(), ByVal po_header_id As Decimal, ByVal po_header As TPROC_PO_HEADERS) As ResultStatus
            Dim current_user_id As String = Session("USER_ID")
            Dim rs As New ResultStatus
            Dim poFacade As New PurchaseOrderFacade
            Dim po_details = New TPROC_PO_DETAILS
            Dim po_details_itm = New TPROC_PO_DETAILS_ITEM
            Dim po_detail_itm_id As Decimal

            Try
                For Each item As String In litem_detail
                    Dim pr_number As Decimal
                    Dim arry = item.Split(";")

                    'set item id for GM req by rizky
                    Dim item_id = arry(6)

                    po_details_itm.PR_NO = Generate.GetSeqPODetail("TPROC_PO_DETAILS_ITEM")
                    po_details_itm.PO_HEADER_ID = po_header_id
                    po_details_itm.ITEM_NAME = arry(0)
                    po_details_itm.UNITMEASUREMENT = arry(1)
                    po_details_itm.QUANTITY = arry(2)
                    po_details_itm.PRICE = arry(4)

                    If item_id <> "" Then
                        po_details_itm.ITEM_ID = item_id 'for GM req by rizky
                    End If

                    po_details_itm.CREATED_BY = Session("USER_ID")
                    po_details_itm.CREATED_TIME = Date.Now

                    'insert table po detail_item disini
                    Using scope As New TransactionScope()
                        rs = poFacade.InsertPODetailItem(current_user_id, po_details_itm, po_detail_itm_id)
                        If rs.IsSuccess Then
                            Generate.CommitGenerator("TPROC_PO_DETAILS_ITEM")
                            If rs.IsSuccess Then
                                scope.Complete()
                            End If
                        End If
                    End Using

                    If rs.IsSuccess Then
                        Dim arry_details = arry(5).Split("^")
                        For Each item2 As String In arry_details
                            Dim arry_details3 = item2.Split("|")
                            pr_number = Convert.ToDecimal(arry_details3(0))
                            po_details.ITEM_NAME = arry_details3(1)
                            po_details.USERMEASUREMENT = arry_details3(2)
                            po_details.QUANTITY = arry_details3(3)
                            po_details.PRICE = arry_details3(4)
                            po_details.TOTAL = arry_details3(5)
                            po_details.FUND_T1 = arry_details3(6)
                            po_details.LOB1_T2 = arry_details3(7)
                            po_details.PLAN_T3 = arry_details3(8)
                            po_details.WA_T4 = arry_details3(9) 'value null, because get data using join when create crv. this is wrong..
                            po_details.LOB2_T5 = arry_details3(10)
                            If item_id <> "" Then
                                po_details.ITEM_ID = item_id 'for GM req by rizky
                            End If
                            po_details.CREATED_TIME = Date.Now
                            po_details.CREATED_BY = Session("USER_ID")
                            po_details.ROW_STATUS = ListEnum.RowStat.Live

                            'insert table po detail disini
                            Using scope As New TransactionScope()
                                rs = poFacade.InsertPODetail(current_user_id, po_details, pr_number, po_detail_itm_id)
                                If rs.IsSuccess Then
                                    'update po number dan supplier tproc_pr_detail
                                    rs = poFacade.UpdatePoNumberSupp_PrDetail(pr_number, arry_details3(1), arry_details3(4), po_header)
                                    If rs.IsSuccess Then
                                        Generate.CommitGenerator("TPROC_PO_DETAILS")
                                        If rs.IsSuccess Then
                                            scope.Complete()
                                            rs.SetSuccessStatus()
                                        End If

                                    End If

                                End If
                            End Using
                        Next
                    End If
                Next

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message & "|| /n" & ex.StackTrace.ToString() & "|| /n" & ex.InnerException.ToString() & "|| /n" & ex.Source & "|| PO list item")
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU28")>
        Function PopDetailPO(flag As Decimal, for_storage As Integer, form_type_id As Decimal) As ActionResult

            ViewBag.BaseFrom = flag
            ViewBag.ForStorage = for_storage
            ViewBag.FormTypeId = form_type_id

            Return PartialView("_PopupDetails")
        End Function

        <CAuthorize(Role:="MNU28")>
        Function getValT(ByVal list_param1 As String,
                         ByVal list_param2 As String,
                         ByVal list_param3 As String,
                         ByVal list_param5 As String) As String

            Dim r As String
            'Dim po_tcontrol As New List(Of TPROC_CONTROL_PARAMETERS)
            Dim i As Integer = 0
            Dim t1 As String = ""
            Dim t2 As String = ""
            Dim t3 As String = ""
            Dim t5 As String = ""

            'Using db As New eProcurementEntities
            '    po_tcontrol = db.TPROC_CONTROL_PARAMETERS.Where(Function(x) x.PARM_TYP = list_param1 Or x.PARM_TYP = list_param2 _
            '        Or x.PARM_TYP = list_param3 Or x.PARM_TYP = list_param5).ToList()
            'End Using

            'Do While (i < po_tcontrol.Count)
            '    If po_tcontrol(i).PARM_TYP = list_param1 Then
            '        t1 = po_tcontrol(i).PARM_VALU + "|" + po_tcontrol(i).PARM_DESC
            '    ElseIf po_tcontrol(i).PARM_TYP = list_param2 Then
            '        t2 = po_tcontrol(i).PARM_VALU + "|" + po_tcontrol(i).PARM_DESC
            '    ElseIf po_tcontrol(i).PARM_TYP = list_param3 Then
            '        t3 = po_tcontrol(i).PARM_VALU + "|" + po_tcontrol(i).PARM_DESC
            '    ElseIf po_tcontrol(i).PARM_TYP = list_param5 Then
            '        t5 = po_tcontrol(i).PARM_VALU + "|" + po_tcontrol(i).PARM_DESC
            '    End If

            '    i = (i + 1)
            'Loop

            r = t1 + "|" + t2 + "|" + t3 + "|" + t5
            Return r
        End Function

        <CAuthorize(Role:="MNU28")>
        Function GetListPrNumberId(pr_id As String) As ActionResult
            Dim result As New List(Of SelectListItem)
            result = Helpers.Dropdown.GetPrNumberId(pr_id)

            Return Json(result)
        End Function

        <CAuthorize(Role:="MNU28")>
        Function GetValPrDetailId(txt_prnumber_id As String, txt_item_nm As String) As String
            Dim r As String
            Dim po_prdetail As New List(Of TPROC_PR_DETAIL)
            Dim i As Integer = 0
            Dim Id As String = ""
            Dim UM As String = ""
            Dim Qty As String = ""
            Dim Price As String = ""
            Dim Wa_number As String = ""

            Using db As New eProcurementEntities
                po_prdetail = db.TPROC_PR_DETAIL.Where(Function(x) x.PR_HEADER_ID = txt_prnumber_id And Trim(x.ITEM_NAME) = txt_item_nm).ToList()
            End Using

            Do While (i < po_prdetail.Count)
                If (po_prdetail(i).ID <> Nothing) Then
                    Id = System.Convert.ToString(Trim(po_prdetail(i).ID))
                    If po_prdetail(i).USER_MEASUREMENT IsNot Nothing Then
                        UM = Trim(po_prdetail(i).USER_MEASUREMENT.ToString())
                    End If
                    Qty = System.Convert.ToString(Trim(po_prdetail(i).QTY))
                    If po_prdetail(i).REVISED_QTY > 0 Then
                        Qty = System.Convert.ToString(Trim(po_prdetail(i).REVISED_QTY))
                    End If
                    Price = System.Convert.ToString(Trim(po_prdetail(i).PRICE))
                    Wa_number = System.Convert.ToString(Trim(po_prdetail(i).WA_NUMBER))
                End If

                i = (i + 1)
            Loop

            r = Id + "|" + UM + "|" + Qty + "|" + Price + "|" + Wa_number
            Return r
        End Function

        <CAuthorize(Role:="MNU28")>
        Function PopCRVDetails(ByVal id As Decimal) As ActionResult
            Dim crv_list As New List(Of TPROC_CRV)
            Using db As New eProcurementEntities
                crv_list = db.TPROC_CRV.Where(Function(x) x.PO_ID = id).ToList()
            End Using
            Return PartialView("_DetailsCRV", crv_list)
        End Function

        <CAuthorize(Role:="MNU28")>
        Function PopPcDetails(ByVal id As Decimal) As ActionResult
            Dim pc As New TPROC_PC
            Using db As New eProcurementEntities
                pc = db.TPROC_PC.Find(id)
            End Using
            Return PartialView("_DetailsPC", pc)
        End Function
#End Region

#Region "MNU48"

        '<HttpPost>
        <CAuthorize(Role:="MNU48")>
        Function ListPOByStatus(txt_status_po_val As String, txt_date_from As Date, txt_date_to As Date) As ActionResult
            TempData("message") = txt_status_po_val
            TempData("date_from") = txt_date_from
            TempData("date_to") = txt_date_to

            If txt_status_po_val = "" Then
                TempData("message") = "null"
            End If


            Return RedirectToAction("_ListPO")
        End Function

        <CAuthorize(Role:="MNU48")>
        Function _ListPO() As ActionResult
            Dim po_header As New List(Of TPROC_PO_HEADERS)
            ViewBag.Message = TempData("message")

            If TempData("message") = "null" Then
                ViewBag.Message = "Please select status"
                Return View(po_header)
            End If

            If TempData("message") IsNot Nothing And TempData("message") <> "null" Then
                po_header = GetDataPOByStatus(TempData("message"), TempData("date_from"), TempData("date_to"))
                ViewBag.Message = Nothing
            End If

            Return View(po_header)
        End Function

        <CAuthorize(Role:="MNU48")>
        Function GetDataPOByStatus(txt_status_po_val As String, from As Date, tto As Date) As List(Of TPROC_PO_HEADERS)
            Dim po_headerx As New List(Of TPROC_PO_HEADERS)

            If Convert.ToInt32(txt_status_po_val) = ListEnum.PO.Submitted Then
                If from.Year.Equals(1) And tto.Year.Equals(1) Then
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val).OrderBy(Function(y) y.PO_NUMBER).ToList()
                ElseIf from.Year <> 1 And tto.Year.Equals(1) Then
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val And DbFunctions.TruncateTime(x.CREATED_TIME) >= from).OrderBy(Function(y) y.PO_NUMBER).ToList()
                ElseIf from.Year.Equals(1) And tto.Year <> 1 Then
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val And DbFunctions.TruncateTime(x.CREATED_TIME) <= tto).OrderBy(Function(y) y.PO_NUMBER).ToList()
                Else
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val And DbFunctions.TruncateTime(x.CREATED_TIME) >= from And DbFunctions.TruncateTime(x.CREATED_TIME) <= tto).OrderBy(Function(y) y.PO_NUMBER).ToList()
                End If
            Else
                If from.Year.Equals(1) And tto.Year.Equals(1) Then
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val).OrderBy(Function(y) y.PO_NUMBER).ToList()
                ElseIf from.Year <> 1 And tto.Year.Equals(1) Then
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val And DbFunctions.TruncateTime(x.LAST_MODIFIED_TIME) >= from).OrderBy(Function(y) y.PO_NUMBER).ToList()
                ElseIf from.Year.Equals(1) And tto.Year <> 1 Then
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val And DbFunctions.TruncateTime(x.LAST_MODIFIED_TIME) <= tto).OrderBy(Function(y) y.PO_NUMBER).ToList()
                Else
                    po_headerx = db.TPROC_PO_HEADERS.Where(Function(x) x.PO_STATUS = txt_status_po_val And DbFunctions.TruncateTime(x.LAST_MODIFIED_TIME) >= from And DbFunctions.TruncateTime(x.LAST_MODIFIED_TIME) <= tto).OrderBy(Function(y) y.PO_NUMBER).ToList()
                End If
            End If

            Return po_headerx
        End Function

        <CAuthorize(Role:="MNU48")>
        Function DetailHeader(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim user_id As String = Session("USER_ID")

            Dim po_header As TPROC_PO_HEADERS = db.TPROC_PO_HEADERS.Find(id)
            'get set detail param
            Dim detail_param As New PO_HEADER_PARAM
            detail_param.POHeaderId = po_header.ID
            detail_param.PONumber = po_header.PO_NUMBER

            ViewBag.OnlyView = ""
            ViewBag.AsIsVrf = ""
            ViewBag.StatusAsVrf = ""
            ViewBag.AsIsAppr = ""
            ViewBag.StatusAsAppr = ""

            'set verify as is
            For Each itemvrf In po_header.TPROC_VRF_PO.ToList()
                If itemvrf.USER_ID.ToUpper() = user_id.ToUpper() Then
                    ViewBag.AsIsVrf = itemvrf.AS_IS
                    ViewBag.StatusAsVrf = itemvrf.STATUS
                    Exit For
                End If
            Next

            'set approv/review as is
            For Each item In po_header.TPROC_APPR_PO.ToList()
                If item.USER_ID.ToUpper() = user_id.ToUpper() Then
                    ViewBag.AsIsAppr = item.AS_IS
                    ViewBag.StatusAsAppr = item.STATUS
                    Exit For
                End If
            Next

            ViewBag.Message = TempData("message")

            Return View("DetailHeader", po_header)
        End Function

        <CAuthorize(Role:="MNU48")>
        Function DetailHeaderOnlyView(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim user_id = Session("USER_ID")

            Dim po_header As TPROC_PO_HEADERS = db.TPROC_PO_HEADERS.Find(id)
            'get set detail param
            Dim detail_param As New PO_HEADER_PARAM
            detail_param.POHeaderId = po_header.ID
            detail_param.PONumber = po_header.PO_NUMBER

            ViewBag.OnlyView = 1
            ViewBag.AsIsVrf = ""
            ViewBag.StatusAsVrf = ""
            ViewBag.AsIsAppr = ""
            ViewBag.StatusAsAppr = ""

            ViewBag.Message = TempData("message")

            Return View("DetailHeader", po_header)
        End Function

        <CAuthorize(Role:="MNU48")>
        Function ActionPrintPO(ByVal number_id As Decimal) As ActionResult
            If IsNothing(number_id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim po_header As TPROC_PO_HEADERS = db.TPROC_PO_HEADERS.Find(number_id)

            ViewBag.Message = TempData("message")

            Return View("_PrintDetailPO", po_header)
        End Function

        <CAuthorize(Role:="MNU48")>
        Function ActionUpdateStatus(ByVal status_new As String) As ActionResult
            Dim current_user_id As String = Session("USER_ID")
            Dim rs As New ResultStatus
            Dim poFacade As New PurchaseOrderFacade
            Dim r As String
            Dim appr_po_id As Decimal
            Dim vrf_po_id As Decimal

            Dim po_header = db.TPROC_PO_HEADERS.Find(PO_HEADER_PARAM.GetPOHeaderId)

            Using scope As New TransactionScope()
                Try
                    rs = poFacade.UpdateStatusPO(po_header, status_new, current_user_id, appr_po_id, vrf_po_id)

                    If rs.IsSuccess And (status_new <> ListEnum.PO.Verified.ToString() And status_new <> ListEnum.PO.Completed.ToString()) Then
                        rs = poFacade.UpdateStatusApprPO(appr_po_id, current_user_id, status_new)
                    End If

                    'if status_new = verify
                    If rs.IsSuccess And status_new = ListEnum.PO.Verified.ToString() Then
                        rs = poFacade.UpdateStatusVrfPO(vrf_po_id, current_user_id, status_new)
                        If rs.IsSuccess Then
                            rs = poFacade.SendEmailApprPO(po_header.ID)
                        End If
                    End If

                    'if status_new = complete send email to the created po 
                    If rs.IsSuccess And status_new = ListEnum.PO.Completed.ToString() Then
                        rs = poFacade.SendEmailCreatorPO(po_header.ID)
                    ElseIf rs.IsSuccess And status_new <> ListEnum.PO.Completed.ToString() Then
                        rs = poFacade.SendEmailCreatorPONotif(po_header.ID, status_new)
                    End If

                    If rs.IsSuccess Then
                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End If

                    If rs.IsSuccess Then
                        TempData("message") = "PO number " + po_header.PO_NUMBER + " has been change status"
                        Dim message = String.Format("| Success Insert PO")
                        rs.SetSuccessStatus(message)
                    Else
                        TempData("message") = rs.MessageText
                    End If

                    r = rs.IsSuccess.ToString() + "|" + rs.MessageText.ToString()
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using

            Return RedirectToAction("DetailHeader", New With {.id = po_header.ID})
        End Function

        Function DetailItemPO(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Dim po_detail As TPROC_PO_DETAILS_ITEM = db.TPROC_PO_DETAILS_ITEM.Find(id)

            Return PartialView("_DetailItemPO", po_detail)
        End Function
#End Region
        Function InsertVerifyPoByApprovalRole(po_id As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim facadePo As New PurchaseOrderFacade
            Dim db As New eProcurementEntities

            Using scope As New TransactionScope()
                Try

                    'Dim appr_role = db.TPROC_APPROVAL_ROLE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live And x.AS_IS = "Verifier" And x.ROLE_NAME = ListEnum.ApprovalRole.PC).ToList()
                    Dim vrf_role As List(Of APPROVAL_ROLE) = facadePo.GetVerifyRoleFor("PO")

                    For Each item In vrf_role
                        Dim vrf_po As New TPROC_VRF_PO
                        vrf_po.PO_ID = po_id
                        vrf_po.USER_ID = item.USER_ID
                        vrf_po.NAME = item.NAME
                        vrf_po.STATUS = "Waiting for verify"
                        vrf_po.AS_IS = "Verifier"
                        vrf_po.EMAIL = item.EMAIL
                        vrf_po.CREATED_TIME = Date.Now
                        vrf_po.CREATED_BY = CurrentUser.GetCurrentUserId()
                        Using db2 As New eProcurementEntities
                            db2.TPROC_VRF_PO.Add(vrf_po)
                            db2.SaveChanges()
                            rs.SetSuccessStatus()
                        End Using
                    Next

                    If rs.IsSuccess Then
                        scope.Complete()
                        rs.SetSuccessStatus()
                    End If

                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message & "|| /n" & ex.StackTrace.ToString() & "|| /n" & ex.InnerException.ToString() & "|| /n" & ex.Source & "|| verify approve")
                End Try
            End Using

            Return rs
        End Function

        <CAuthorize(Role:="MNU48")>
        Function InsertApprovalPoByApprovalRole(po_id As Decimal, total_price As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Dim facadePo As New PurchaseOrderFacade

            Try
                'Dim appr_role = db.TPROC_APPROVAL_ROLE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live And x.AS_IS = "Approver" And x.ROLE_NAME = ListEnum.ApprovalRole.PO).ToList()
                Dim appr_role As List(Of APPROVAL_ROLE) = facadePo.GetApprovalRoleFor("PO")

                For Each item In appr_role
                    Dim appr_po As New TPROC_APPR_PO
                    appr_po.PO_ID = po_id
                    appr_po.USER_ID = item.USER_ID
                    appr_po.NAME = item.NAME
                    appr_po.STATUS = CommonFunction.GetLimitForStatus(item.RUPIAH_LIMIT, total_price)
                    appr_po.AS_IS = CommonFunction.GetLimitForAsIs(item.RUPIAH_LIMIT, total_price)
                    appr_po.EMAIL = item.EMAIL
                    appr_po.CREATED_TIME = Date.Now
                    appr_po.CREATED_BY = CurrentUser.GetCurrentUserId()
                    Using db2 As New eProcurementEntities
                        db2.TPROC_APPR_PO.Add(appr_po)
                        db2.SaveChanges()
                    End Using
                Next

                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message & "|| /n" & ex.StackTrace.ToString() & "|| /n" & ex.InnerException.ToString() & "|| /n" & ex.Source & "|| approver role")
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU48")>
        Function ActionExportToPdf(ByVal _ID As String, ByVal _PO_NUMB As String) As FileResult
            Dim MsgJSON As String = "Export file success"
            Dim resultView As FileResult = Nothing
            Dim rs As New ResultStatus

            Try
                Dim renderedBytes As Byte() = Nothing
                Dim lclReport As New LocalReport
                Dim source As ReportDataSource
                lclReport.ReportPath = "RDLC\TRANS_PO_RPT.rdlc"
                Dim Query As String =
                "SELECT a.ID " & vbCrLf &
                "  , a.PO_NUMBER " & vbCrLf &
                "  , a.CREATE_DATE " & vbCrLf &
                "  , b.CURRENCY_NAME " & vbCrLf &
                "  , a.SUPPLIER_NAME " & vbCrLf &
                "  , a.CONTACT_PERSON " & vbCrLf &
                "  , a.SUPPLIER_ADDTRSS " & vbCrLf &
                "  , a.SUPPLIER_PHONE " & vbCrLf &
                "  , a.SUPPLIER_FAX " & vbCrLf &
                "  , a.DELIVERY_NAME " & vbCrLf &
                "  , a.DELIVERY_ADDRESS " & vbCrLf &
                "  , a.DELIVERY_PHONE " & vbCrLf &
                "  , a.DELIVERY_FAX " & vbCrLf &
                "  , a.DELIVERY_DATE " & vbCrLf &
                "  , a.NOTES " & vbCrLf &
                "  , c.NO " & vbCrLf &
                "  , c.QUANTITY " & vbCrLf &
                "  , c.DESCRIPTION " & vbCrLf &
                "  , c.UNITMEASUREMENT " & vbCrLf &
                "  , c.PRICE " & vbCrLf &
                "  , c.SUBTOTAL AS SUBTOTALDETAIL " & vbCrLf &
                "  , a.SUB_TOTAL AS SUBTOTAL " & vbCrLf &
                "  , a.DSCNT_AMT " & vbCrLf &
                "  , COALESCE(a.SUB_TOTAL, 0) - COALESCE(a.DSCNT_AMT, 0) AS DIFFAFTRDSCNT " & vbCrLf &
                "  , COALESCE(a.VAT, 0) AS VAT " & vbCrLf &
                "  , COALESCE(a.WTH_TAX, 0) AS WTH_TAX " & vbCrLf &
                "  , COALESCE(a.GRAND_TOTAL, 0) AS GRAND_TOTAL " & vbCrLf &
                "  , d.USER_NAME AS PREPARED_BY " & vbCrLf &
                "  , COALESCE(a.LAST_MODIFIED_TIME, a.CREATE_DATE) AS PREPARED_DATE " & vbCrLf &
                "  , e.USER_NAME AS APPROVE_BY " & vbCrLf &
                "  , a.APPOROVED_DATE AS APPROVE_DATE " & vbCrLf &
                "  , d.SIGNATURE_IMAGE AS SIGPREPAREDBY " & vbCrLf &
                "  , e.SIGNATURE_IMAGE AS SIGAPPROVEBY " & vbCrLf &
                "  , g.USER_NAME AS VERIFY_BY " & vbCrLf &
                "  , a.VERIFIED_DATE AS VERIFY_DATE " & vbCrLf &
                "  , f.USER_NAME AS REVIEW_BY " & vbCrLf &
                "  , a.REVIEWED_DATE AS REVIEW_DATE " & vbCrLf &
                "  , f.SIGNATURE_IMAGE AS SIGREVIEWBY " & vbCrLf &
                "FROM TPROC_PO_HEADERS a " & vbCrLf &
                "LEFT JOIN TPROC_CURRENCY b ON b.ID = a.CURRENCY " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT SUBSTR('000', 1, 3 - LENGTH(ROW_NUMBER() OVER (ORDER BY PO_HEADER_ID ASC))) || ROW_NUMBER() OVER (ORDER BY PO_HEADER_ID ASC) AS NO " & vbCrLf &
                "      , PO_HEADER_ID " & vbCrLf &
                "      , ITEM_NAME AS DESCRIPTION " & vbCrLf &
                "      , UNITMEASUREMENT " & vbCrLf &
                "      , COALESCE(CAST(QUANTITY AS NUMBER), 0) AS QUANTITY " & vbCrLf &
                "      , COALESCE(CAST(PRICE AS NUMBER), 0) / COALESCE(CAST(QUANTITY AS NUMBER), 0) AS PRICE " & vbCrLf &
                "      , COALESCE(CAST(PRICE AS NUMBER), 0) AS SUBTOTAL " & vbCrLf &
                "    FROM TPROC_PO_DETAILS_ITEM " & vbCrLf &
                "    WHERE PO_HEADER_ID = '" & _ID & "' " & vbCrLf &
                "  ) c ON c.PO_HEADER_ID = a.ID " & vbCrLf &
                "LEFT JOIN TPROC_APPROVAL_ROLE d ON d.USER_ID = COALESCE(a.LAST_MODIFIED_BY, a.PREPARED_BY) AND d.ROW_STATUS = '0' " & vbCrLf &
                "LEFT JOIN TPROC_APPROVAL_ROLE e ON e.USER_ID = a.APPOROVED_BY AND e.ROW_STATUS = '0' " & vbCrLf &
                "LEFT JOIN TPROC_APPROVAL_ROLE f ON f.USER_ID = a.REVIEWED_BY AND e.ROW_STATUS = '0' " & vbCrLf &
                "LEFT JOIN TPROC_APPROVAL_ROLE g ON g.USER_ID = a.VERIFIED_BY AND e.ROW_STATUS = '0' " & vbCrLf &
                "WHERE a.ID = '" & _ID & "' " & vbCrLf &
                "ORDER BY a.ID ASC "
                Dim dtTablePrint As DataTable = ConnectionDB.GetDataTable(Query)
                source = New ReportDataSource("DS_PO_RPT", dtTablePrint)

                lclReport.DataSources.Clear()
                lclReport.DataSources.Add(source)
                lclReport.Refresh()

                'Query = "SELECT PARAMETER_VALUE " & vbCrLf &
                '        "FROM TPROC_PARAMETERS " & vbCrLf &
                '        "WHERE PARAMETER_CODE = 'DIR_TRANS_CRV'"
                'Dim dtParameter As DataTable = ConnectionDB.GetDataTable(Query)


                Dim streamFile As System.IO.FileStream
                Dim warnings As Warning() = Nothing
                Dim streamids As String() = Nothing
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim extension As String = Nothing
                Dim DeviceInfo As String = "<DeviceInfo>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>2.5cm</MarginTop>" +
                "  <MarginLeft>1.5cm</MarginLeft>" +
                "  <MarginRight>1.5cm</MarginRight>" +
                "  <MarginBottom>2.5cm</MarginBottom>" +
                "</DeviceInfo>"
                renderedBytes = lclReport.Render("pdf",
                                                 DeviceInfo,
                                                 mimeType,
                                                 encoding,
                                                 extension,
                                                 streamids,
                                                 warnings)

                'streamFile = New System.IO.FileStream(dtParameter.Rows(0).Item(0) & _PO_NUMB & ".pdf", System.IO.FileMode.Create)

                Dim path = CommonFunction.GetPathParam("DIR_TRANS_PO")
                rs = CommonFunction.CheckFolderExisting(path + "\PO\Pdf\")

                If rs.IsSuccess Then
                    Dim path_file As String = path & "\PO\Pdf\" & _PO_NUMB & ".pdf"
                    streamFile = New System.IO.FileStream(path_file, System.IO.FileMode.Create)
                    streamFile.Write(renderedBytes, 0, renderedBytes.Length)
                    streamFile.Flush()
                    streamFile.Dispose()
                    streamFile.Close()

                    resultView = ActionViewFileObject(path_file, "application/pdf")
                End If

            Catch ex As Exception
                MsgJSON = "Failed with error : " & ex.Message
            End Try

            Return resultView
        End Function

        ' this action will create text file 'your_file_name.txt' with data from
        ' string variable 'string_with_your_data', which will be downloaded by
        ' your browser
        Public Function ActionExportToTxt(ByVal _ID As Decimal, ByVal _PO_NUMB As String) As FileResult
            Dim resultView As FileResult = Nothing
            Dim data As String = ""
            Dim po As New TPROC_PO_HEADERS
            Dim rs As New ResultStatus
            Dim path_file As String = ""

            Using db As New eProcurementEntities
                po = db.TPROC_PO_HEADERS.Find(_ID)
                If po IsNot Nothing Then
                    data = po.PO_NUMBER & po.SUPPLIER_NAME
                End If
            End Using

            Dim byteArray = Encoding.ASCII.GetBytes(data)
            'Dim stream = New MemoryStream(byteArray)

            Dim path = CommonFunction.GetPathParam("DIR_TRANS_PO")
            rs = CommonFunction.CheckFolderExisting(path + "\PO\Txt\")

            If rs.IsSuccess Then
                path_file = path & "\PO\Txt\" & _PO_NUMB & ".txt"
                Dim streamFile As System.IO.FileStream
                streamFile = New System.IO.FileStream(path_file, System.IO.FileMode.Create)
                streamFile.Write(byteArray, 0, byteArray.Length)
                streamFile.Flush()
                streamFile.Dispose()
                streamFile.Close()
            End If

            Return ActionViewFileObject(path_file, "text/plain")
        End Function

        <CAuthorize(Role:="MNU48")>
        Function SentEmailToSupplier(ByVal _ID As String, ByVal _PO_NUMB As String) As ActionResult
            Dim MsgJSON As String = "Export file success"
            Dim rs As New ResultStatus

            Try
                Dim renderedBytes As Byte() = Nothing
                Dim lclReport As New LocalReport
                Dim source As ReportDataSource
                lclReport.ReportPath = "RDLC\TRANS_PO_RPT.rdlc"
                Dim Query As String =
                "SELECT a.ID " & vbCrLf &
                "  , a.PO_NUMBER " & vbCrLf &
                "  , a.CREATE_DATE " & vbCrLf &
                "  , b.CURRENCY_NAME " & vbCrLf &
                "  , a.SUPPLIER_NAME " & vbCrLf &
                "  , a.CONTACT_PERSON " & vbCrLf &
                "  , a.SUPPLIER_ADDTRSS " & vbCrLf &
                "  , a.SUPPLIER_PHONE " & vbCrLf &
                "  , a.SUPPLIER_FAX " & vbCrLf &
                "  , a.DELIVERY_NAME " & vbCrLf &
                "  , a.DELIVERY_ADDRESS " & vbCrLf &
                "  , a.DELIVERY_PHONE " & vbCrLf &
                "  , a.DELIVERY_FAX " & vbCrLf &
                "  , a.DELIVERY_DATE " & vbCrLf &
                "  , a.NOTES " & vbCrLf &
                "  , c.NO " & vbCrLf &
                "  , c.QUANTITY " & vbCrLf &
                "  , c.DESCRIPTION " & vbCrLf &
                "  , c.UNITMEASUREMENT " & vbCrLf &
                "  , c.PRICE " & vbCrLf &
                "  , c.SUBTOTAL AS SUBTOTALDETAIL " & vbCrLf &
                "  , a.SUB_TOTAL AS SUBTOTAL " & vbCrLf &
                "  , a.DSCNT_AMT " & vbCrLf &
                "  , COALESCE(a.SUB_TOTAL, 0) - COALESCE(a.DSCNT_AMT, 0) AS DIFFAFTRDSCNT " & vbCrLf &
                "  , COALESCE(a.VAT, 0) AS VAT " & vbCrLf &
                "  , COALESCE(a.WTH_TAX, 0) AS WTH_TAX " & vbCrLf &
                "  , COALESCE(a.GRAND_TOTAL, 0) AS GRAND_TOTAL " & vbCrLf &
                "  , s.email_address AS SUPPLIER_EMAIL " & vbCrLf &
                "FROM TPROC_PO_HEADERS a " & vbCrLf &
                "LEFT JOIN TPROC_CURRENCY b ON b.ID = a.CURRENCY " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT SUBSTR('000', 1, 3 - LENGTH(ROW_NUMBER() OVER (ORDER BY PO_HEADER_ID ASC))) || ROW_NUMBER() OVER (ORDER BY PO_HEADER_ID ASC) AS NO " & vbCrLf &
                "      , PO_HEADER_ID " & vbCrLf &
                "      , ITEM_NAME AS DESCRIPTION " & vbCrLf &
                "      , UNITMEASUREMENT " & vbCrLf &
                "      , COALESCE(CAST(QUANTITY AS NUMBER), 0) AS QUANTITY " & vbCrLf &
                "      , COALESCE(CAST(PRICE AS NUMBER), 0) / COALESCE(CAST(QUANTITY AS NUMBER), 0) AS PRICE " & vbCrLf &
                "      , COALESCE(CAST(PRICE AS NUMBER), 0) AS SUBTOTAL " & vbCrLf &
                "    FROM TPROC_PO_DETAILS_ITEM " & vbCrLf &
                "    WHERE PO_HEADER_ID = '" & _ID & "' " & vbCrLf &
                "  ) c ON c.PO_HEADER_ID = a.ID " & vbCrLf &
                "LEFT JOIN TPROC_SUPPLIER s ON s.ID = a.SUPPLIER_ID " & vbCrLf &
                "WHERE a.ID = '" & _ID & "' " & vbCrLf &
                "ORDER BY a.ID ASC "
                Dim dtTablePrint As DataTable = ConnectionDB.GetDataTable(Query)
                source = New ReportDataSource("DS_PO_RPT", dtTablePrint)

                lclReport.DataSources.Clear()
                lclReport.DataSources.Add(source)
                lclReport.Refresh()

                Query = "SELECT PARAMETER_VALUE " & vbCrLf &
                        "FROM TPROC_PARAMETERS " & vbCrLf &
                        "WHERE PARAMETER_CODE = 'DIR_TRANS_PO'"
                Dim dtParameter As DataTable = ConnectionDB.GetDataTable(Query)

                Dim streamFile As System.IO.FileStream
                Dim warnings As Warning() = Nothing
                Dim streamids As String() = Nothing
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim extension As String = Nothing
                Dim DeviceInfo As String = "<DeviceInfo>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>2.5cm</MarginTop>" +
                "  <MarginLeft>1.5cm</MarginLeft>" +
                "  <MarginRight>1.5cm</MarginRight>" +
                "  <MarginBottom>2.5cm</MarginBottom>" +
                "</DeviceInfo>"
                renderedBytes = lclReport.Render("pdf",
                                                 DeviceInfo,
                                                 mimeType,
                                                 encoding,
                                                 extension,
                                                 streamids,
                                                 warnings)

                'streamFile = New System.IO.FileStream(dtParameter.Rows(0).Item(0) & _PO_NUMB & "_" & Format(Now, "yyyyMMdd") & ".pdf", System.IO.FileMode.Create)
                streamFile = New System.IO.FileStream(dtParameter.Rows(0).Item(0) & _PO_NUMB & ".pdf", System.IO.FileMode.Create)
                streamFile.Write(renderedBytes, 0, renderedBytes.Length)
                streamFile.Flush()
                streamFile.Dispose()
                streamFile.Close()

                Dim supp_email As String = dtTablePrint.Rows(0).Item("SUPPLIER_EMAIL")
                Dim delivery_to As String = dtTablePrint.Rows(0).Item("DELIVERY_ADDRESS")
                Dim delivery_on As String = dtTablePrint.Rows(0).Item("DELIVERY_DATE")
                'System.Text.Encoding.UTF8.GetString(renderedBytes).TrimEnd('\0')
                Dim filex As String = System.Text.Encoding.UTF8.GetString(renderedBytes)
                Dim attach_files() As String = {filex}
                rs = SendEmailToSupplier(_PO_NUMB, supp_email, delivery_to, delivery_on, filex)

            Catch ex As Exception
                MsgJSON = "Failed with error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function

        Public Function SendEmailToSupplier(po_number As String, supp_email As String, delivery_to As String, delivery_on As Date, attch_file As String) As ResultStatus
            Dim mailFacade As New EmailFacade
            Dim result As New ResultStatus
            Dim sendEmail2 As String = Nothing

            Dim poNumber As String = po_number
            Dim reqDate As String = Date.Now
            Dim reqBy As String = Session("USER_NAME").ToString()
            Dim reEmail As String = Session("USER_MAIL")
            Dim emailSender As String = ConfigurationSettings.AppSettings("EmailSender")

            Dim emailToSupplier As New ListFieldNameAndValue
            Dim emailToAdminEproc As New ListFieldNameAndValue

            emailToSupplier.AddItem("Email", supp_email)
            emailToAdminEproc.AddItem("Email", reEmail)

            result = mailFacade.SendEmailAttach(emailSender, emailToSupplier, emailToAdminEproc, String.Format(EmailTemplate.eProcSubjectToSupplier, poNumber),
                                        String.Format(EmailTemplate.eProcEmailTemplateUserNotificationForSupplier, Nothing, poNumber, reqBy, reqDate, delivery_to, delivery_on),
                                            sendEmail2, attch_file)


            Return result
        End Function

        Function ActionViewFileObject(ByVal path_file As String, ByVal type_file As String) As FileResult
            Dim path_exec As String = path_file
            Return File(path_exec, type_file)
        End Function

        '<HttpPost>
        Function ViewPdf(ByVal po_number As String) As FileResult
            Dim path_file = CommonFunction.GetPathParam("DIR_TRANS_PO") + "/PO/Pdf/" + po_number + ".pdf"
            Dim result = ActionViewFileObject(path_file, "application/pdf")

            Return result
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Function ReportPurchaseOrder() As ActionResult
            Return View("FormReport")
        End Function

        Function ActionReport(ByVal _PeriodeFrom As String,
                              ByVal _PeriodeTo As String) As ActionResult
            Dim rs As New ResultStatus
            Dim resultView As FileResult = Nothing
            Try
                Dim Query As String = "SELECT PARAMETER_VALUE " & vbCrLf &
                                     "FROM TPROC_PARAMETERS " & vbCrLf &
                                     "WHERE PARAMETER_CODE = 'DIR_REPORT_PO' "
                Dim dtTablePrint As DataTable = ConnectionDB.GetDataTable(Query)

                Query =
                "SELECT d.CONVERTED_ACCT_NUM AS ACCOUNTCODE " & vbCrLf &
                "  , TO_CHAR(a.CREATED_TIME, 'YYYY') || '0' || TO_CHAR(a.CREATED_TIME, 'MM') || TO_CHAR(a.CREATED_TIME, 'YYYYMMDD') AS PERIODEDATE " & vbCrLf &
                "  , 'L' AS DUMMY1 " & vbCrLf &
                "  , SUBSTR('000000000000000', 1, 15 - LENGTH(CAST(a.GRAND_TOTAL AS VARCHAR(18)))) || CAST(a.GRAND_TOTAL AS VARCHAR(18)) || '000D' AS GRANDTOTAL " & vbCrLf &
                "  , 'POS' AS JOURNALTYPE " & vbCrLf &
                "  , 'PROC' AS SOURCE " & vbCrLf &
                "  , a.PO_NUMBER AS PONO " & vbCrLf &
                "  , b.ITEM_NAME AS PODESCRIPTION " & vbCrLf &
                "  , TO_CHAR(a.CREATED_TIME, 'YYYYMMDD') AS TRANSACTIONDATE " & vbCrLf &
                "  , c.CURRENCY_NAME " & vbCrLf &
                "  , '000000001' || SUBSTR('000000000000000000000000000', 1, 27 - LENGTH(CAST(a.GRAND_TOTAL AS VARCHAR(27)))) || CAST(a.GRAND_TOTAL AS VARCHAR(27)) || '2' AS MONEYIDR " & vbCrLf &
                "  , 'Y' AS DUMMY2 " & vbCrLf &
                "  , b.FUND_T1 AS T1 " & vbCrLf &
                "  , b.LOB1_T2 AS T2 " & vbCrLf &
                "  , b.PLAN_T3 AS T3 " & vbCrLf &
                "  , b.WA_T4 AS T4 " & vbCrLf &
                "FROM TPROC_PO_HEADERS a " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT b.PO_HEADER_ID " & vbCrLf &
                "      , a.PO_DTLS_ITEM_ID " & vbCrLf &
                "      , a.ITEM_NAME " & vbCrLf &
                "      , a.FUND_T1 " & vbCrLf &
                "      , a.LOB1_T2 " & vbCrLf &
                "      , a.PLAN_T3 " & vbCrLf &
                "      , a.WA_T4 " & vbCrLf &
                "      , a.LOB2_T5 " & vbCrLf &
                "      , c.ACCOUNT_CODE " & vbCrLf &
                "    FROM TPROC_PO_DETAILS a " & vbCrLf &
                "    INNER JOIN TPROC_PO_DETAILS_ITEM b ON b.ID = a.PO_DTLS_ITEM_ID " & vbCrLf &
                "    LEFT JOIN TPROC_PR_HEADER c ON c.PR_NO = a.PR_HEADER_NO " & vbCrLf &
                "  ) b ON b.PO_HEADER_ID = a.ID " & vbCrLf &
                "LEFT JOIN TPROC_CURRENCY c ON c.ID = a.CURRENCY " & vbCrLf &
                "LEFT JOIN TPROC_CHART_OF_ACCOUNT_GR d on d.ACCT_NUM = b.ACCOUNT_CODE " & vbCrLf &
                "WHERE TRUNC(a.COMPLETED_DATE, 'DD') >= TO_DATE('" & _PeriodeFrom & "', 'DD-MM-YYYY') AND TRUNC(a.COMPLETED_DATE, 'DD') <= TO_DATE('" & _PeriodeTo & "', 'DD-MM-YYYY')"
                Dim dtParameter As DataTable = ConnectionDB.GetDataTable(Query)
                Dim fs_GenerateContain As String = ""
                'PONO Maksimalnya harusnya 15 char namun dari system valuenya 16 harus diubah generate po numbernya
                For i As Integer = 0 To dtParameter.Rows.Count - 1
                    fs_GenerateContain &= dtParameter.Rows(i).Item("ACCOUNTCODE").ToString & Microsoft.VisualBasic.Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("ACCOUNTCODE").ToString), " ") &
                                          dtParameter.Rows(i).Item("PERIODEDATE").ToString & Microsoft.VisualBasic.Strings.StrDup(17 - Len(dtParameter.Rows(i).Item("PERIODEDATE").ToString), " ") &
                                          dtParameter.Rows(i).Item("DUMMY1").ToString & Microsoft.VisualBasic.Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("DUMMY1").ToString), " ") &
                                          dtParameter.Rows(i).Item("GRANDTOTAL").ToString & Microsoft.VisualBasic.Strings.StrDup(20 - Len(dtParameter.Rows(i).Item("GRANDTOTAL").ToString), " ") &
                                          dtParameter.Rows(i).Item("JOURNALTYPE").ToString & Microsoft.VisualBasic.Strings.StrDup(5 - Len(dtParameter.Rows(i).Item("JOURNALTYPE").ToString), " ") &
                                          dtParameter.Rows(i).Item("SOURCE").ToString & Microsoft.VisualBasic.Strings.StrDup(5 - Len(dtParameter.Rows(i).Item("SOURCE").ToString), " ") &
                                          dtParameter.Rows(i).Item("PONO").ToString & Microsoft.VisualBasic.Strings.StrDup(16 - Len(dtParameter.Rows(i).Item("PONO").ToString), " ") &
                                          dtParameter.Rows(i).Item("PODESCRIPTION").ToString & Microsoft.VisualBasic.Strings.StrDup(40 - Len(dtParameter.Rows(i).Item("PODESCRIPTION").ToString), " ") &
                                          dtParameter.Rows(i).Item("TRANSACTIONDATE").ToString & Strings.StrDup(54 - Len(dtParameter.Rows(i).Item("TRANSACTIONDATE").ToString), " ") &
                                          dtParameter.Rows(i).Item("CURRENCY_NAME").ToString & Strings.StrDup(5 - Len(dtParameter.Rows(i).Item("CURRENCY_NAME").ToString), " ") &
                                          dtParameter.Rows(i).Item("MONEYIDR").ToString & Strings.StrDup(48 - Len(dtParameter.Rows(i).Item("MONEYIDR").ToString), " ") &
                                          dtParameter.Rows(i).Item("DUMMY2").ToString & Strings.StrDup(17 - Len(dtParameter.Rows(i).Item("DUMMY2").ToString), " ") &
                                          dtParameter.Rows(i).Item("T1").ToString & Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("T1").ToString), " ") &
                                          dtParameter.Rows(i).Item("T2").ToString & Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("T2").ToString), " ") &
                                          dtParameter.Rows(i).Item("T3").ToString & Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("T3").ToString), " ") &
                                          dtParameter.Rows(i).Item("T4").ToString & Strings.StrDup(143 - Len(dtParameter.Rows(i).Item("T4").ToString), " ") & vbCrLf
                Next

                'Dim WriteTextFile As New System.IO.StreamWriter(dtTablePrint.Rows(0).Item(0) & "REPORTPO_" & Format(Now, "yyyyMMdd") & ".txt", True)
                'WriteTextFile.WriteLine(fs_GenerateContain)
                'WriteTextFile.Close()
                'rs.SetSuccessStatus("Export file success")

                Dim path = CommonFunction.GetPathParam("DIR_TRANS_PO")
                rs = CommonFunction.CheckFolderExisting(path + "\PO\Txt\")

                If rs.IsSuccess Then
                    Dim path_file As String = path & "\PO\Txt\" & "REPORTPO_" & Replace(_PeriodeFrom, "-", "") & "_" & Replace(_PeriodeTo, "-", "") & ".txt"

                    Dim WriteTextFile As New System.IO.StreamWriter(path_file, True)
                    WriteTextFile.WriteLine(fs_GenerateContain)
                    WriteTextFile.Close()
                    rs.SetSuccessStatus("Export file success")

                    Process.Start(path_file)
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return Json(rs.MessageText)
        End Function


#Region "PUSH EMAIL"
        Public Function SentPushEmailByPO(po_header_id As Decimal, po_number As String, po_date As String, po_type As String) As ResultStatus
            Dim rs As New ResultStatus
            Dim poFacade As New PurchaseOrderFacade
            Dim prFacade As New PurchasingRequestFacade

            Dim email_user = Session("USER_MAIL")
            rs = poFacade.SendEmailApprPO(po_header_id)
            If rs.IsSuccess Then
                rs = prFacade.SendPushEmailToUser(po_header_id, po_number, po_date, email_user, po_type, "LinkPO")
            End If

            Return rs
        End Function
#End Region

    End Class
End Namespace

