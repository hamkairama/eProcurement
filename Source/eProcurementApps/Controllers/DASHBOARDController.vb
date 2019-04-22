Imports eProcurementApps.Helpers
Imports eProcurementApps.Models
Imports System
Imports System.Data.Entity
Imports System.Net

Namespace Controllers
    Public Class DASHBOARDController
        Inherits Controller

        Public Shared isActive As String
        Dim db As New eProcurementEntities

        'Get :   Dashboard
        <AuthorizeLogin()>
        Function Index() As ActionResult 'This is dashboard image eprocurement
            Dim db As New eProcurementEntities

            Return View()
        End Function

        <AuthorizeLogin()>
        Function IndexJobLists(Optional _FlagInbox As String = "100") As ActionResult
            isActive = ""
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)

            ViewBag.UserId = Session("USER_ID")
            ViewBag.UserMail = Session("USER_MAIL")

            query_inboxs = GetMyListApprPRWA(Session("USER_ID").ToString())
            ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprWA)
            If _FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprWA) Then
            ElseIf _FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprGM) Then
                query_inboxs = GetListApprGM(Session("USER_ID").ToString())
                ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprGM)
            ElseIf _FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrCRV) Then
                query_inboxs = GetListVrCRV(Session("USER_ID").ToString())
                ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrCRV)
            ElseIf _FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprCRV) Then
                query_inboxs = GetListApprCRV(Session("USER_ID").ToString())
                ViewBag.FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprCRV)
            End If

            isActive &=
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprWA), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprRD), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprReq), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.CompReq), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrfPC), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprAckPC), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprPC), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrfPO), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprPO), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprGM), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.VrCRV), "active;", ";") &
            IIf(_FlagInbox = Convert.ToInt32(ListEnum.FlagInbox.ApprCRV), "active;", ";")

            Return View(query_inboxs)
        End Function

        Function ListApprInbox(flag As Integer, user_id As String, user_mail As String) As ActionResult
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)

            If flag = Convert.ToInt32(ListEnum.FlagInbox.ApprWA) Then
                query_inboxs = GetMyListApprPRWA(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprRD) Then
                query_inboxs = GetMyListApprPRRD(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprReq) Then
                query_inboxs = GetMyListApprReq(user_mail)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.CompReq) Then
                query_inboxs = GetMyListCompReq(user_mail)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.VrfPC) Then
                query_inboxs = GetListVrfPC(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprAckPC) Then
                query_inboxs = GetListApprAckPC(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprPC) Then
                query_inboxs = GetListApprPC(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.VrfPO) Then
                query_inboxs = GetListVrfPO(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprPO) Then
                query_inboxs = GetListApprReviewPO(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprGM) Then
                query_inboxs = GetListApprGM(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.VrCRV) Then
                query_inboxs = GetListVrCRV(user_id)
            ElseIf flag = Convert.ToInt32(ListEnum.FlagInbox.ApprCRV) Then
                query_inboxs = GetListApprCRV(user_id)
            End If

            ViewBag.FlagInbox = flag

            Return PartialView("_ListApprPR", query_inboxs)
        End Function

        Public Function GetMyListApprPRWA(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)

            Using db As New eProcurementEntities
                Dim query = (From _header In db.TPROC_PR_HEADER
                             Join _detail In db.TPROC_PR_DETAIL On _header.ID Equals (_detail.PR_HEADER_ID)
                             Join _detail_appr_wa In db.TPROC_PR_APPR_WA On _detail.ID Equals (_detail_appr_wa.PR_DETAIL_ID)
                             Join _user In db.TPROC_USER On _user.ID Equals (_header.USER_ID_ID)
                             Where _detail_appr_wa.USER_ID.ToUpper() = user_id.ToUpper() And _detail.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Complete _
                                        And _detail.PR_DETAIL_STATUS <> ListEnum.ItemStatus.Rejected And _header.PR_STATUS <> ListEnum.PRStatus.PrRejected
                             Select _header.ID, _header.PR_NO, _user.USER_NAME, _header.PR_DATE
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PR_NO
                    query_inbox.REQUEST_BY = item.USER_NAME
                    query_inbox.REQUEST_DATE = item.PR_DATE

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetMyListApprPRRD(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)

            Using db As New eProcurementEntities
                Dim query = (From _header In db.TPROC_PR_HEADER
                             Join _detail In db.TPROC_PR_DETAIL On _header.ID Equals (_detail.PR_HEADER_ID)
                             Join _appr_reldept_gr In db.TPROC_PR_APPR_RELDEPT_GR On _appr_reldept_gr.PR_HEADER_ID Equals (_header.ID)
                             Join _appr_reldept_dt In db.TPROC_PR_APPR_RELDEPT_DT On _appr_reldept_dt.PR_APPR_RELDEPT_GR_ID Equals (_appr_reldept_gr.ID)
                             Join _user In db.TPROC_USER On _user.ID Equals (_header.USER_ID_ID)
                             Where _appr_reldept_dt.USER_ID.ToUpper() = user_id.ToUpper() And _detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete _
                                 And _header.PR_STATUS <> ListEnum.PRStatus.PrRejected And _header.PR_STATUS < ListEnum.PRStatus.PrApprovedComplete
                             Select _header.ID, _header.PR_NO, _user.USER_NAME, _header.PR_DATE
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PR_NO
                    query_inbox.REQUEST_BY = item.USER_NAME
                    query_inbox.REQUEST_DATE = item.PR_DATE

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetMyListApprReq(user_email As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)

            Using db As New eProcurementEntities
                Dim query = (From _request In db.TPROC_REQUEST
                             Where _request.APPROVAL_EMAIL.ToUpper() = user_email.ToUpper() And _request.STATUS = ListEnum.Request.NeedApprove
                             Select _request.ID, _request.REQUEST_NO, _request.REQUEST_BY, _request.REQUEST_DT, _request.CONTROL, _request.ACTION, _request.RELATION_FLAG, _request.STATUS
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.REQUEST_NO
                    query_inbox.REQUEST_BY = item.REQUEST_BY
                    query_inbox.REQUEST_DATE = item.REQUEST_DT
                    query_inbox.CONTROL = item.CONTROL
                    query_inbox.ACTION = item.ACTION
                    query_inbox.RELATION_FLAG = item.RELATION_FLAG
                    query_inbox.STATUS = item.STATUS

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetMyListCompReq(user_email As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)

            If Session("IS_EPROC_ADMIN") = 1 Then
                Using db As New eProcurementEntities
                    Dim query = (From _request In db.TPROC_REQUEST
                                 Where _request.STATUS = ListEnum.Request.NeedComplete
                                 Select _request.ID, _request.REQUEST_NO, _request.REQUEST_BY, _request.REQUEST_DT, _request.CONTROL, _request.ACTION, _request.RELATION_FLAG, _request.STATUS
                                 Distinct
                                 Order By ID).ToList()

                    For Each item In query
                        Dim query_inbox As New INBOX_JOB_LIST
                        query_inbox.JOB_ID = item.ID
                        query_inbox.REQUEST_NO = item.REQUEST_NO
                        query_inbox.REQUEST_BY = item.REQUEST_BY
                        query_inbox.REQUEST_DATE = item.REQUEST_DT
                        query_inbox.CONTROL = item.CONTROL
                        query_inbox.ACTION = item.ACTION
                        query_inbox.RELATION_FLAG = item.RELATION_FLAG
                        query_inbox.STATUS = item.STATUS

                        query_inboxs.Add(query_inbox)
                    Next
                End Using
            End If

            Return query_inboxs
        End Function

        Public Function GetListVrfPC(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim submitted As String = ListEnum.PriceCom.Submitted.ToString().ToUpper()

            Using db As New eProcurementEntities
                Dim query = (From _pc In db.TPROC_PC
                             Join _vrf In db.TPROC_VRF_PC On _vrf.PC_ID Equals (_pc.ID)
                             Where (_pc.STATUS.ToUpper() = submitted) And _vrf.USER_ID.ToUpper() = user_id.ToUpper()
                             Select _pc.ID, _pc.PC_NUM, _pc.CREATED_BY, _pc.CREATED_TIME
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PC_NUM
                    query_inbox.REQUEST_BY = item.CREATED_BY
                    query_inbox.REQUEST_DATE = item.CREATED_TIME

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetListApprAckPC(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim Verified As String = ListEnum.PriceCom.Verified.ToString().ToUpper()

            Using db As New eProcurementEntities
                Dim query = (From _pc In db.TPROC_PC
                             Join _acknow In db.TPROC_ACKNOW_APPR On _acknow.PC_ID Equals (_pc.ID)
                             Join _acknow_dt In db.TPROC_ACKNOW_APPR_DT On _acknow_dt.ACKNOW_APPR_ID Equals (_acknow.ID)
                             Where (_pc.STATUS.ToUpper() = Verified) And _acknow.STATUS Is Nothing And _acknow_dt.USER_ID.ToUpper() = user_id.ToUpper()
                             Select _pc.ID, _pc.PC_NUM, _pc.CREATED_BY, _pc.CREATED_TIME
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PC_NUM
                    query_inbox.REQUEST_BY = item.CREATED_BY
                    query_inbox.REQUEST_DATE = item.CREATED_TIME

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetListApprPC(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim verified As String = ListEnum.PriceCom.Verified.ToString().ToUpper()
            Dim approvedByAcknowledge As String = ListEnum.PriceCom.ApprovedByAcknowledge.ToString().ToUpper()
            Dim reviewed As String = ListEnum.PriceCom.Reviewed.ToString().ToUpper()

            Using db As New eProcurementEntities
                Dim query = (From _pc In db.TPROC_PC
                             Join _appr In db.TPROC_APPR_PC On _appr.PC_ID Equals (_pc.ID)
                             Where (_pc.STATUS.ToUpper() = verified Or _pc.STATUS.ToUpper() = approvedByAcknowledge Or _pc.STATUS.ToUpper() = reviewed) And _appr.USER_ID.ToUpper() = user_id.ToUpper()
                             Select _pc.ID, _pc.PC_NUM, _pc.CREATED_BY, _pc.CREATED_TIME
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PC_NUM
                    query_inbox.REQUEST_BY = item.CREATED_BY
                    query_inbox.REQUEST_DATE = item.CREATED_TIME

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetListVrfPO(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim submitted As String = Convert.ToString(ListEnum.PO.Submitted)

            Using db As New eProcurementEntities
                Dim query = (From _po In db.TPROC_PO_HEADERS
                             Join _vrf In db.TPROC_VRF_PO On _vrf.PO_ID Equals (_po.ID)
                             Where (_po.PO_STATUS = submitted) And _vrf.USER_ID.ToUpper() = user_id.ToUpper() And _po.PC_ID Is Nothing
                             Select _po.ID, _po.PO_NUMBER, _po.CREATED_BY, _po.CREATED_TIME
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PO_NUMBER
                    query_inbox.REQUEST_BY = item.CREATED_BY
                    query_inbox.REQUEST_DATE = item.CREATED_TIME

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetListApprReviewPO(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim submitted As String = Convert.ToString(ListEnum.PO.Submitted)
            Dim verified As String = Convert.ToString(ListEnum.PO.Verified)

            Using db As New eProcurementEntities
                Dim query = (From _po In db.TPROC_PO_HEADERS
                             Join _appr In db.TPROC_APPR_PO On _appr.PO_ID Equals (_po.ID)
                             Where (_po.PO_STATUS = verified) And _appr.USER_ID.ToUpper() = user_id.ToUpper() And _po.PC_ID Is Nothing
                             Select _po.ID, _po.PO_NUMBER, _po.CREATED_BY, _po.CREATED_TIME
                             Distinct
                             Order By ID).ToList()

                For Each item In query
                    Dim query_inbox As New INBOX_JOB_LIST
                    query_inbox.JOB_ID = item.ID
                    query_inbox.REQUEST_NO = item.PO_NUMBER
                    query_inbox.REQUEST_BY = item.CREATED_BY
                    query_inbox.REQUEST_DATE = item.CREATED_TIME

                    query_inboxs.Add(query_inbox)
                Next
            End Using

            Return query_inboxs
        End Function

        Public Function GetListApprGM(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim Query As String =
            "SELECT ID " & vbCrLf &
            "  , GM_NUMBER " & vbCrLf &
            "  , COALESCE(LAST_MODIFIED_BY, CREATED_BY) AS REQUEST_BY " & vbCrLf &
            "  , COALESCE(LAST_MODIFIED_TIME, CREATED_TIME) AS REQUEST_TIME " & vbCrLf &
            "FROM TPROC_GM_HEADERS " & vbCrLf &
            "WHERE ROW_STATUS = '" & GMController.RowStatus.Approve & "' " & vbCrLf &
            "AND '" & user_id & "' IN (" & vbCrLf &
            "SELECT USER_ID " & vbCrLf &
            "FROM TPROC_APPROVAL_ROLE a " & vbCrLf &
            "INNER JOIN TPROC_APPROVAL_ROLE_DETAIL b ON b.ID_APPROVAL_ROLE = a.ID " & vbCrLf &
            "WHERE a.ROW_STATUS = 0 " & vbCrLf &
            "AND b.ROLE_NAME = 'Good Match' " & vbCrLf &
            "AND b.AS_IS = 'Approver' " & vbCrLf &
            ")"
            Dim dtTable As DataTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)
            For i As Integer = 0 To dtTable.Rows.Count - 1
                Dim query_inbox As New INBOX_JOB_LIST
                query_inbox.JOB_ID = Trim(dtTable.Rows(i).Item("ID") & "")
                query_inbox.REQUEST_NO = Trim(dtTable.Rows(i).Item("GM_NUMBER") & "")
                query_inbox.REQUEST_BY = Trim(dtTable.Rows(i).Item("REQUEST_BY") & "")
                query_inbox.REQUEST_DATE = Trim(dtTable.Rows(i).Item("REQUEST_TIME") & "")

                query_inboxs.Add(query_inbox)
            Next

            Return query_inboxs
        End Function

        Public Function GetListVrCRV(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim Query As String =
            "SELECT ID " & vbCrLf &
            "  , CRV_NUM " & vbCrLf &
            "  , COALESCE(LAST_MODIFIED_BY, CREATED_BY) AS REQUEST_BY " & vbCrLf &
            "  , COALESCE(LAST_MODIFIED_TIME, CREATED_TIME) AS REQUEST_TIME " & vbCrLf &
            "FROM TPROC_CRV " & vbCrLf &
            "WHERE ROW_STATUS = '" & CRVController.RowStatus.Verify & "' " & vbCrLf &
            "AND '" & user_id & "' IN (" & vbCrLf &
            "SELECT USER_ID " & vbCrLf &
            "FROM TPROC_APPROVAL_ROLE a " & vbCrLf &
            "INNER JOIN TPROC_APPROVAL_ROLE_DETAIL b ON b.ID_APPROVAL_ROLE = a.ID " & vbCrLf &
            "WHERE a.ROW_STATUS = 0 " & vbCrLf &
            "AND b.ROLE_NAME = 'CRV' " & vbCrLf &
            "AND b.AS_IS = 'Verifier' " & vbCrLf &
            ")"
            Dim dtTable As DataTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)
            For i As Integer = 0 To dtTable.Rows.Count - 1
                Dim query_inbox As New INBOX_JOB_LIST
                query_inbox.JOB_ID = Trim(dtTable.Rows(i).Item("ID") & "")
                query_inbox.REQUEST_NO = Trim(dtTable.Rows(i).Item("CRV_NUM") & "")
                query_inbox.REQUEST_BY = Trim(dtTable.Rows(i).Item("REQUEST_BY") & "")
                query_inbox.REQUEST_DATE = Trim(dtTable.Rows(i).Item("REQUEST_TIME") & "")

                query_inboxs.Add(query_inbox)
            Next

            Return query_inboxs
        End Function

        Public Function GetListApprCRV(user_id As String) As List(Of INBOX_JOB_LIST)
            Dim query_inboxs As New List(Of INBOX_JOB_LIST)
            Dim Query As String =
            "SELECT ID " & vbCrLf &
            "  , CRV_NUM " & vbCrLf &
            "  , VERIFY_BY AS REQUEST_BY " & vbCrLf &
            "  , VERIFY_TIME AS REQUEST_TIME " & vbCrLf &
            "FROM TPROC_CRV " & vbCrLf &
            "WHERE ROW_STATUS = '" & CRVController.RowStatus.Approve & "' " & vbCrLf &
            "AND '" & user_id & "' IN (" & vbCrLf &
            "SELECT USER_ID " & vbCrLf &
            "FROM TPROC_APPROVAL_ROLE a " & vbCrLf &
            "INNER JOIN TPROC_APPROVAL_ROLE_DETAIL b ON b.ID_APPROVAL_ROLE = a.ID " & vbCrLf &
            "WHERE a.ROW_STATUS = 0 " & vbCrLf &
            "AND b.ROLE_NAME = 'CRV' " & vbCrLf &
            "AND b.AS_IS = 'Approver' " & vbCrLf &
            ")"
            Dim dtTable As DataTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)
            For i As Integer = 0 To dtTable.Rows.Count - 1
                Dim query_inbox As New INBOX_JOB_LIST
                query_inbox.JOB_ID = Trim(dtTable.Rows(i).Item("ID") & "")
                query_inbox.REQUEST_NO = Trim(dtTable.Rows(i).Item("CRV_NUM") & "")
                query_inbox.REQUEST_BY = Trim(dtTable.Rows(i).Item("REQUEST_BY") & "")
                query_inbox.REQUEST_DATE = Trim(dtTable.Rows(i).Item("REQUEST_TIME") & "")

                query_inboxs.Add(query_inbox)
            Next

            Return query_inboxs
        End Function
    End Class
End Namespace