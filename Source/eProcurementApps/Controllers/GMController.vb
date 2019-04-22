Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports eProcurementApps.DataAccess

Namespace Controllers
    Public Class GMController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities
        Public Shared GM_Action As String
        Public Shared dtTable As DataTable
        Public Shared dtTableDetail As DataTable
        Public Shared JSONPODataTable As String = ""
        Public Shared GM_NUMBER As String
        Public Shared FLAGFORM As String
        Dim Query As String

        Public Enum RowStatus As Integer
            Rejected = -2
            InActive = -1
            Active = 0
            Approve = 1
            Complete = 2
        End Enum

        Public Sub New()
            Query =
            "SELECT a.ID " & vbCrLf &
            "  , a.GM_NUMBER " & vbCrLf &
            "  , a.CREATE_DATE " & vbCrLf &
            "  , a.PO_ID " & vbCrLf &
            "  , b.PO_NUMBER " & vbCrLf &
            "  , c.PO_TYPE_NAME AS PO_TYPE " & vbCrLf &
            "  , b.SUPPLIER_ID " & vbCrLf &
            "  , b.SUPPLIER_NAME " & vbCrLf &
            "  , b.CONTACT_PERSON " & vbCrLf &
            "  , b.SUPPLIER_PHONE " & vbCrLf &
            "  , b.SUPPLIER_FAX " & vbCrLf &
            "  , b.SUPPLIER_ADDTRSS " & vbCrLf &
            "  , b.DELIVERY_ID " & vbCrLf &
            "  , b.DELIVERY_NAME " & vbCrLf &
            "  , b.DELIVERY_PHONE " & vbCrLf &
            "  , b.DELIVERY_FAX " & vbCrLf &
            "  , b.DELIVERY_ADDRESS " & vbCrLf &
            "  , a.NOTES " & vbCrLf &
            "  , a.ROW_STATUS " & vbCrLf &
            "  , a.CREATED_BY " & vbCrLf &
            "  , a.CREATED_TIME " & vbCrLf &
            "  , a.COMPLETED_BY " & vbCrLf &
            "  , a.COMPLETED_DATE " & vbCrLf &
            "  , a.REJECTED_BY " & vbCrLf &
            "  , a.REJECTED_TIME " & vbCrLf &
            "  , a.REJECTNOTE " & vbCrLf &
            "  , CASE WHEN a.ROW_STATUS = '" & RowStatus.Active & "' THEN 'Still preparation' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.InActive & "' THEN 'Not active' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Rejected & "' THEN 'Rejected' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Approve & "' THEN 'Waiting to approve' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Complete & "' THEN 'Completed' END AS STATUS " & vbCrLf &
            "FROM TPROC_GM_HEADERS a " & vbCrLf &
            "LEFT JOIN TPROC_PO_HEADERS b On b.ID = a.PO_ID " & vbCrLf &
            "LEFT JOIN TPROC_PO_TYPE c ON c.ID = b.PO_TYPE_ID "
        End Sub

        <CAuthorize(Role:="MNU63")>
        Function ListGmByStatus(txt_status_gm_val As String, txt_date_from As String, txt_date_to As String) As ActionResult
            TempData("message") = txt_status_gm_val
            TempData("date_from") = txt_date_from
            TempData("date_to") = txt_date_to

            If txt_status_gm_val = "" Then
                TempData("message") = "null"
            End If


            Return RedirectToAction("Index")
        End Function


        <CAuthorize(Role:="MNU63")>
        Function Index() As ActionResult
            dtTable = Nothing
            Dim query_where = ""

            ViewBag.Message = TempData("message")

            If TempData("message") = "null" Then
                ViewBag.Message = "Please select status"
                Return View()
            End If

            If TempData("message") IsNot Nothing And TempData("message") <> "null" Then
                query_where = GetDataGmByStatus(TempData("message"), TempData("date_from"), TempData("date_to"))

                Query = Query & vbCrLf & query_where & vbCrLf &
                        "ORDER BY a.ID ASC "
                '"WHERE a.ROW_STATUS Not In ('" & RowStatus.InActive & "', '" & RowStatus.Rejected & "', '" & RowStatus.Complete & "')" & vbCrLf &

                dtTable = ConnectionDB.GetDataTable(Query)

                ViewBag.Message = Nothing
            End If

            Return View()
        End Function


        <CAuthorize(Role:="MNU63")>
        Function GetDataGmByStatus(txt_status_crv_val As String, from As String, tto As String) As String
            Dim query_where = ""

            If Convert.ToInt32(txt_status_crv_val) = ListEnum.Crv.Active Then
                If from = "01-01-0001" And tto = "01-01-0001" Then
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val
                ElseIf from <> "01-01-0001" And tto = "01-01-0001" Then
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val & " AND " & "TO_CHAR(a.CREATED_TIME, 'DDMMYYYY') >= '" & Replace(from, "-", "") & "' "
                ElseIf from = "01-01-0001" And tto <> "01-01-0001" Then
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val & " AND " & "TO_CHAR(a.CREATED_TIME, 'DDMMYYYY') <= '" & Replace(tto, "-", "") & "' "
                Else
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val & " AND " & "TO_CHAR(a.CREATED_TIME, 'DDMMYYYY') BETWEEN '" & Replace(from, "-", "") & "' AND '" & Replace(Replace(tto, "-", ""), "-", "") & "' "
                End If
            Else
                If from = "01-01-0001" And tto = "01-01-0001" Then
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val
                ElseIf from <> "01-01-0001" And tto = "01-01-0001" Then
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val & " AND " & "TO_CHAR(a.LAST_MODIFIED_TIME, 'DDMMYYYY') >= '" & Replace(from, "-", "") & "' "
                ElseIf from = "01-01-0001" And tto <> "01-01-0001" Then
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val & " AND " & "TO_CHAR(a.LAST_MODIFIED_TIME, 'DDMMYYYY') <= '" & Replace(tto, "-", "") & "' "
                Else
                    query_where = "WHERE a.ROW_STATUS = " & txt_status_crv_val & " AND " & "TO_CHAR(a.LAST_MODIFIED_TIME, 'DDMMYYYY') BETWEEN '" & Replace(from, "-", "") & "' AND '" & Replace(tto, "-", "") & "' "
                End If
            End If

            Return query_where
        End Function

        '<CAuthorize(Role:="MNU67")>
        'Function Index() As ActionResult
        '    Query = Query & vbCrLf &
        '    "ORDER BY a.ID ASC "
        '    '"WHERE a.ROW_STATUS NOT IN ('" & RowStatus.InActive & "', '" & RowStatus.Rejected & "', '" & RowStatus.Complete & "')" & vbCrLf &
        '    dtTable = ConnectionDB.GetDataTable(Query)

        '    Return View()
        'End Function

        <CAuthorize(Role:="MNU63")>
        Function FormInput(Optional ByVal _Id As String = "-99",
                           Optional ByVal _Action As String = "Create",
                           Optional ByVal _FlagForm As String = "1") As ActionResult
            Query = Query & vbCrLf &
            IIf(_Action = "Create", "WHERE 1=2", "WHERE a.ID = " & _Id)
            dtTable = ConnectionDB.GetDataTable(Query)

            Query =
            "SELECT 'GM/' || TO_CHAR(SYSDATE, 'YYYYMMDD') || '/' || SUBSTR('00000', 1, 5 - LENGTH((COALESCE(a.MAXSEQUENCE, 0) + 1))) || (COALESCE(a.MAXSEQUENCE, 0) + 1) AS GENERATENO " & vbCrLf &
            "FROM " & vbCrLf &
            "  ( " & vbCrLf &
            "    SELECT CAST(MAX(SUBSTR(GM_NUMBER, 13, 5)) As NUMBER) As MAXSEQUENCE " & vbCrLf &
            "    FROM TPROC_GM_HEADERS " & vbCrLf &
            "    WHERE SUBSTR(GM_NUMBER, 4, 8) = TO_CHAR(SYSDATE, 'YYYYMMDD') " & vbCrLf &
            ") a"
            Dim dtGENNO As DataTable = ConnectionDB.GetDataTable(Query)
            GM_NUMBER = dtGENNO.Rows(0).Item(0)

            Query =
            "Select A.ITEM_ID " & vbCrLf &
            "  , B.ITEM_DESCRIPTION " & vbCrLf &
            "  , A.UNITMEASUREMENT " & vbCrLf &
            "  , A.QUANTITY - COALESCE(C.QUANTITY, 0) As OUTSTANDING " & vbCrLf &
            "  , COALESCE(D.QUANTITY, A.QUANTITY - COALESCE(C.QUANTITY, 0)) As QUANTITY " & vbCrLf &
            "  , A.PRICE As PRICE " & vbCrLf &
            "FROM TPROC_PO_DETAILS_ITEM A " & vbCrLf &
            "LEFT JOIN TPROC_STOCK B On B.ID = A.ITEM_ID " & vbCrLf &
            "LEFT JOIN " & vbCrLf &
            "  ( " & vbCrLf &
            "    Select B.ITEM_ID " & vbCrLf &
            "      , SUM(B.QUANTITY) As QUANTITY " & vbCrLf &
            "    FROM TPROC_GM_HEADERS A, TPROC_GM_DETAILS B " & vbCrLf &
            "    WHERE A.ID = B.GM_ID " & vbCrLf &
            "    And A.ID <> '" & _Id & "' " & vbCrLf
            If dtTable.Rows.Count > 0 Then
                Query = Query & "    AND A.PO_ID = '" & dtTable.Rows(0).Item("PO_ID") & "' "
            Else
                Query = Query & "    AND 1=2 "
            End If
            Query = Query & "    GROUP BY B.ITEM_ID " & vbCrLf &
            "  ) C ON C.ITEM_ID = A.ITEM_ID " & vbCrLf &
            "LEFT JOIN " & vbCrLf &
            "  ( " & vbCrLf &
            "    SELECT B.ITEM_ID " & vbCrLf &
            "    , CAST(B.QUANTITY AS NUMBER) AS QUANTITY " & vbCrLf &
            "    FROM TPROC_GM_HEADERS A, TPROC_GM_DETAILS B " & vbCrLf &
            "    WHERE A.ID = B.GM_ID " & vbCrLf &
            "    AND A.ID = '" & _Id & "'  " & vbCrLf &
            "  ) D ON D.ITEM_ID = A.ITEM_ID " & vbCrLf &
            "WHERE A.QUANTITY - COALESCE(C.QUANTITY, 0) > 0 " & vbCrLf
            If dtTable.Rows.Count > 0 Then
                Query = Query & "AND A.PO_HEADER_ID = '" & dtTable.Rows(0).Item("PO_ID") & "' "
            Else
                Query = Query & "AND 1=2 "
            End If
            dtTableDetail = ConnectionDB.GetDataTable(Query)

            Query =
            "Select a.ID " & vbCrLf &
            "  , a.PO_NUMBER As ""PO NUMBER"" " & vbCrLf &
            "  , b.PO_TYPE_NAME As ""PO TYPE"" " & vbCrLf &
            "  , a.SUPPLIER_NAME As ""SUPPLIER NAME"" " & vbCrLf &
            "  , a.SUPPLIER_PHONE As ""SUPPLIER PHONE"" " & vbCrLf &
            "  , a.SUPPLIER_ADDTRSS As ""SUPPLIER ADDRESS"" " & vbCrLf &
            "  , a.CONTACT_PERSON As ""CONTACT PERSON"" " & vbCrLf &
            "  , a.SUPPLIER_FAX As ""SUPPLIER FAX"" " & vbCrLf &
            "  , a.DELIVERY_NAME As ""DELIVERY NAME"" " & vbCrLf &
            "  , a.DELIVERY_PHONE As ""DELIVERY PHONE"" " & vbCrLf &
            "  , a.DELIVERY_ADDRESS As ""DELIVERY ADDRESS"" " & vbCrLf &
            "  , a.DELIVERY_FAX As ""DELIVERY FAX"" " & vbCrLf &
            "FROM TPROC_PO_HEADERS a" & vbCrLf &
            "LEFT JOIN TPROC_PO_TYPE b ON b.ID = a.PO_TYPE_ID " & vbCrLf &
            "WHERE a.PO_STATUS = " & ListEnum.PO.Completed & vbCrLf &
            "AND COALESCE(a.COMPLETED_BY, ' ') <> ' ' "
            Dim dtTablePO As DataTable = ConnectionDB.GetDataTable(Query)
            dtTablePO.Rows.Add()
            JSONPODataTable = Newtonsoft.Json.JsonConvert.SerializeObject(dtTablePO)

            GM_Action = _Action
            FLAGFORM = _FlagForm
            Return View("FormInput")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU63")>
        Function GetDetail(ByVal GM_ID As String,
                           ByVal PO_ID As String) As ActionResult
            Dim dtPODetail As DataTable
            Try
                If GM_ID = "" Then
                    GM_ID = "-99"
                End If
                If PO_ID = "" Then
                    PO_ID = "-99"
                End If
                dtTableDetail = Nothing
                Query =
                "SELECT A.ITEM_ID " & vbCrLf &
                "  , B.ITEM_DESCRIPTION " & vbCrLf &
                "  , A.UNITMEASUREMENT " & vbCrLf &
                "  , A.QUANTITY - COALESCE(C.QUANTITY, 0) As OUTSTANDING " & vbCrLf &
                "  , COALESCE(D.QUANTITY, A.QUANTITY - COALESCE(C.QUANTITY, 0)) AS QUANTITY " & vbCrLf &
                "  , COALESCE(A.PRICE, 0) / COALESCE(A.QUANTITY, 0) AS PRICE " & vbCrLf &
                "FROM TPROC_PO_DETAILS_ITEM A " & vbCrLf &
                "LEFT JOIN TPROC_STOCK B On B.ID = A.ITEM_ID " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT B.ITEM_ID " & vbCrLf &
                "      , SUM(B.QUANTITY) As QUANTITY " & vbCrLf &
                "    FROM TPROC_GM_HEADERS A, TPROC_GM_DETAILS B " & vbCrLf &
                "    WHERE A.ID = B.GM_ID " & vbCrLf &
                "    AND A.ID <> '" & GM_ID & "' " & vbCrLf &
                "    AND A.PO_ID = '" & PO_ID & "' " & vbCrLf &
                "    AND A.ROW_STATUS <> '" & RowStatus.Rejected & "' " & vbCrLf &
                "    GROUP BY B.ITEM_ID " & vbCrLf &
                "  ) C ON C.ITEM_ID = A.ITEM_ID " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT B.ITEM_ID " & vbCrLf &
                "    , CAST(B.QUANTITY AS NUMBER) AS QUANTITY " & vbCrLf &
                "    FROM TPROC_GM_HEADERS A, TPROC_GM_DETAILS B " & vbCrLf &
                "    WHERE A.ID = B.GM_ID " & vbCrLf &
                "    AND A.ID = '" & GM_ID & "'  " & vbCrLf &
                "  ) D ON D.ITEM_ID = A.ITEM_ID " & vbCrLf &
                "WHERE A.PO_HEADER_ID = '" & PO_ID & "' " & vbCrLf &
                "AND (A.QUANTITY - COALESCE(C.QUANTITY, 0)) > 0"
                dtPODetail = ConnectionDB.GetDataTable(Query)
            Catch ex As Exception
            End Try
            Return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dtPODetail))
        End Function

        <CAuthorize(Role:="MNU63")>
        Function ActionCreate(ByVal _GM_NUMBER As String,
                              ByVal _PO_TYPE As String,
                              ByVal _PO_ID As String,
                              ByVal _JSONDetailDataTable As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                Query =
                "SELECT 'GM' || TO_CHAR(SYSDATE, 'YYYY') || SUBSTR('000000000', 1, 9 - LENGTH((COALESCE(a.MAXSEQUENCE, 0) + 1))) || (COALESCE(a.MAXSEQUENCE, 0) + 1) AS GENERATENO  " & vbCrLf &
                "FROM " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT CAST(MAX(SUBSTR(GM_NUMBER, 7, 9)) As NUMBER) As MAXSEQUENCE " & vbCrLf &
                "    FROM TPROC_GM_HEADERS " & vbCrLf &
                "    WHERE SUBSTR(GM_NUMBER, 3, 4) = TO_CHAR(SYSDATE, 'YYYY') " & vbCrLf &
                ") a "
                Dim dtGENNO As DataTable = ConnectionDB.GetDataTable(Query)
                GM_NUMBER = dtGENNO.Rows(0).Item(0) & _PO_TYPE

                Query = "Select COALESCE((Select MAX(COALESCE(ID, 0)) + 1 As ID FROM TPROC_GM_HEADERS), 1) As ID FROM DUAL"
                Dim dtTable As DataTable = ConnectionDB.GetDataTable(Query)
                Dim dtTableDetail As DataTable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(_JSONDetailDataTable)

                Dim fo_ORAConnection As New Oracle.ManagedDataAccess.Client.OracleConnection
                Dim fo_ORACommand As Oracle.ManagedDataAccess.Client.OracleCommand
                Dim fo_ORATransaction As Oracle.ManagedDataAccess.Client.OracleTransaction
                Try
                    fo_ORAConnection = ConnectionDB.CreateConnection
                    fo_ORATransaction = fo_ORAConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    fo_ORACommand = fo_ORAConnection.CreateCommand
                    fo_ORACommand.Transaction = fo_ORATransaction

                    Query = "INSERT INTO TPROC_GM_HEADERS " & vbCrLf &
                    "( " & vbCrLf &
                    "   ID, GM_NUMBER, PO_ID, CREATED_BY, CREATED_TIME " & vbCrLf &
                    ") " & vbCrLf &
                    "VALUES " & vbCrLf &
                    "( " & vbCrLf &
                    "   " & dtTable.Rows(0).Item("ID") & " " & vbCrLf &
                    "   , '" & GM_NUMBER & "' " & vbCrLf &
                    "   , '" & _PO_ID & "' " & vbCrLf &
                    "   , '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , SYSDATE " & vbCrLf &
                    ") "
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_GM_DETAILS WHERE GM_ID = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        If Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) > 0 Then
                            Query = "INSERT INTO TPROC_GM_DETAILS " & vbCrLf &
                                    "( " & vbCrLf &
                                    "   ID, GM_ID, ITEM_ID, ITEM_NAME, UNITMEASUREMENT, QUANTITY " & vbCrLf &
                                    "   , PRICE, CREATED_TIME, CREATED_BY " & vbCrLf &
                                    ") " & vbCrLf &
                                    "VALUES " & vbCrLf &
                                    "( " & vbCrLf &
                                    "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_GM_DETAILS), 1) AS ID FROM DUAL) " & vbCrLf &
                                    "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(0) & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(1) & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(2) & "' " & vbCrLf &
                                    "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) & "' " & vbCrLf &
                                    "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(5)) & "' " & vbCrLf &
                                    "   , SYSDATE " & vbCrLf &
                                    "   , '" & Session("USER_ID") & "' " & vbCrLf &
                                    ") "
                            ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
                        End If
                    Next

                    fo_ORATransaction.Commit()
                    rs.SetSuccessStatus("Data has been created")
                Catch ex As Exception
                    fo_ORATransaction.Rollback()
                    Throw
                End Try
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return Json(rs.MessageText)
        End Function


        <CAuthorize(Role:="MNU63")>
        Function ActionEdit(ByVal _ID As Decimal,
                            ByVal _GM_NUMBER As String,
                            ByVal _PO_ID As String,
                            ByVal _JSONDetailDataTable As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                Dim dtTableDetail As DataTable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(_JSONDetailDataTable)

                Dim fo_ORAConnection As New Oracle.ManagedDataAccess.Client.OracleConnection
                Dim fo_ORACommand As Oracle.ManagedDataAccess.Client.OracleCommand
                Dim fo_ORATransaction As Oracle.ManagedDataAccess.Client.OracleTransaction
                Try
                    fo_ORAConnection = ConnectionDB.CreateConnection
                    fo_ORATransaction = fo_ORAConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    fo_ORACommand = fo_ORAConnection.CreateCommand
                    fo_ORACommand.Transaction = fo_ORATransaction

                    Query = "UPDATE TPROC_GM_HEADERS " & vbCrLf &
                    "SET GM_NUMBER = '" & _GM_NUMBER & "' " & vbCrLf &
                    "   , PO_ID = '" & _PO_ID & "' " & vbCrLf &
                    "   , LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                    "WHERE ID = '" & _ID & "' "
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_GM_DETAILS WHERE GM_ID = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        If Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) > 0 Then
                            Query = "INSERT INTO TPROC_GM_DETAILS " & vbCrLf &
                                    "( " & vbCrLf &
                                    "   ID, GM_ID, ITEM_ID, ITEM_NAME, UNITMEASUREMENT, QUANTITY " & vbCrLf &
                                    "   , PRICE, CREATED_TIME, CREATED_BY " & vbCrLf &
                                    ") " & vbCrLf &
                                    "VALUES " & vbCrLf &
                                    "( " & vbCrLf &
                                    "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_GM_DETAILS), 1) AS ID FROM DUAL) " & vbCrLf &
                                    "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(0) & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(1) & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(2) & "' " & vbCrLf &
                                    "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) & "' " & vbCrLf &
                                    "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(5)) & "' " & vbCrLf &
                                    "   , SYSDATE " & vbCrLf &
                                    "   , '" & Session("USER_ID") & "' " & vbCrLf &
                                    ") "
                            ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
                        End If
                    Next

                    fo_ORATransaction.Commit()
                    rs.SetSuccessStatus("Data has been edited")
                Catch ex As Exception
                    fo_ORATransaction.Rollback()
                    Throw
                End Try
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return Json(rs.MessageText)
        End Function

        <CAuthorize(Role:="MNU63")>
        Function ActionDelete(ByVal _ID As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Try
                Dim fo_ORAConnection As New Oracle.ManagedDataAccess.Client.OracleConnection
                Dim fo_ORACommand As Oracle.ManagedDataAccess.Client.OracleCommand
                Dim fo_ORATransaction As Oracle.ManagedDataAccess.Client.OracleTransaction
                Try
                    fo_ORAConnection = ConnectionDB.CreateConnection
                    fo_ORATransaction = fo_ORAConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    fo_ORACommand = fo_ORAConnection.CreateCommand
                    fo_ORACommand.Transaction = fo_ORATransaction

                    Query = "UPDATE TPROC_GM_HEADERS " & vbCrLf &
                    "SET LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                    "   , ROW_STATUS = " & ListEnum.RowStat.InActive & vbCrLf &
                    "WHERE ID = " & _ID
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    fo_ORATransaction.Commit()
                    rs.SetSuccessStatus("Data has been deleted")
                Catch ex As Exception
                    fo_ORATransaction.Rollback()
                    Throw
                End Try
            Catch ex As Exception
                rs.SetErrorStatus("Failed insert To db")
            End Try

            Return Json(rs.MessageText)
        End Function

        <CAuthorize(Role:="MNU63")>
        Function ActionApprove(ByVal _ID As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                Dim fo_ORAConnection As New Oracle.ManagedDataAccess.Client.OracleConnection
                Dim fo_ORACommand As Oracle.ManagedDataAccess.Client.OracleCommand
                Dim fo_ORATransaction As Oracle.ManagedDataAccess.Client.OracleTransaction


                Try
                    fo_ORAConnection = ConnectionDB.CreateConnection
                    fo_ORATransaction = fo_ORAConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    fo_ORACommand = fo_ORAConnection.CreateCommand
                    fo_ORACommand.Transaction = fo_ORATransaction

                    Query = "UPDATE TPROC_GM_HEADERS " & vbCrLf &
                    "SET COMPLETED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , COMPLETED_DATE = SYSDATE " & vbCrLf &
                    "   , ROW_STATUS = " & RowStatus.Complete & vbCrLf &
                    "WHERE ID = " & _ID
                    ConnectionDB.ExecuteQuery(Query)

                    Query = "SELECT a.FOR_STORAGE " & vbCrLf &
                    "FROM TPROC_PO_HEADERS a " & vbCrLf &
                    "WHERE a.ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ") "
                    Dim dtTableTemp As DataTable = ConnectionDB.GetDataTable(Query)
                    If dtTableTemp.Rows.Count > 0 Then
                        If dtTableTemp.Rows(0).Item("FOR_STORAGE") = "1" Then
                            Query = "INSERT INTO TPROC_STOCKMOVEMENT " & vbCrLf &
                            "SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 As ID FROM TPROC_STOCKMOVEMENT), 1) As ID FROM DUAL " & vbCrLf &
                            "  , b.ITEM_ID AS ITEM_ID " & vbCrLf &
                            "  , a.GM_NUMBER AS REFNO " & vbCrLf &
                            "  , SYSDATE AS CREATED_TIME " & vbCrLf &
                            "  , a.COMPLETED_BY AS CREATED_BY " & vbCrLf &
                            "  , COALESCE(c.STOCK_LAST, 0) AS STOCK_CURRENT " & vbCrLf &
                            "  , b.QUANTITY AS STOCK_IN " & vbCrLf &
                            "  , 0 AS STOCK_OUT " & vbCrLf &
                            "  , COALESCE(c.STOCK_LAST, 0) + b.QUANTITY AS STOCK_LAST " & vbCrLf &
                            "  , 0 AS LATEST_COST " & vbCrLf &
                            "  , 0 AS AVERAGE_COST " & vbCrLf &
                            "SELECT (SELECT COALESCE((SELECT MAX(COALESCE(ID_TEMP, 0)) + 1 As ID FROM TPROC_STOCKMOVEMENT), 1) As ID_TEMP FROM DUAL) AS ID_TEMP " & vbCrLf &
                            "FROM TPROC_GM_HEADERS a " & vbCrLf &
                            "INNER JOIN TPROC_GM_DETAILS b ON b.GM_ID = a.ID " & vbCrLf &
                            "LEFT JOIN " & vbCrLf &
                            "( " & vbCrLf &
                            "  SELECT ROW_NUMBER() OVER (PARTITION BY ITEM_ID ORDER BY ID DESC) AS NOMER " & vbCrLf &
                            "    , ITEM_ID " & vbCrLf &
                            "    , STOCK_LAST " & vbCrLf &
                            "  FROM TPROC_STOCKMOVEMENT " & vbCrLf &
                            "  WHERE ITEM_ID IN (SELECT ITEM_ID FROM TPROC_GM_DETAILS WHERE GM_ID = " & _ID & ") " & vbCrLf &
                            ") c ON c.ITEM_ID = b.ITEM_ID AND c.NOMER = 1 " & vbCrLf &
                            "WHERE a.ID = " & _ID & "  "
                            ConnectionDB.ExecuteQuery(Query)

                            Query = "UPDATE TPROC_STOCK a " & vbCrLf &
                            "SET a.QUANTITY = a.QUANTITY + " & vbCrLf &
                            "                 ( " & vbCrLf &
                            "                   SELECT b.QUANTITY " & vbCrLf &
                            "                   FROM TPROC_GM_DETAILS b " & vbCrLf &
                            "                   WHERE b.GM_ID = " & _ID & " " & vbCrLf &
                            "                   AND a.ID = b.ITEM_ID " & vbCrLf &
                            "                 ) " & vbCrLf &
                            "WHERE EXISTS " & vbCrLf &
                            "( " & vbCrLf &
                            "  SELECT b.QUANTITY " & vbCrLf &
                            "  FROM TPROC_GM_DETAILS b " & vbCrLf &
                            "  WHERE b.GM_ID = " & _ID & " " & vbCrLf &
                            "  AND a.ID = b.ITEM_ID " & vbCrLf &
                            ") "
                            ConnectionDB.ExecuteQuery(Query)
                        End If
                    End If

                    Query =
                    "SELECT SUM(OUTSTANDING) AS OUTSTANDING " & vbCrLf &
                    "FROM " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    SELECT A.QUANTITY - COALESCE(C.QUANTITY, 0) As OUTSTANDING " & vbCrLf &
                    "    FROM TPROC_PO_DETAILS_ITEM A " & vbCrLf &
                    "    LEFT JOIN " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT B.ITEM_ID " & vbCrLf &
                    "          , SUM(B.QUANTITY) As QUANTITY " & vbCrLf &
                    "        FROM TPROC_GM_HEADERS A, TPROC_GM_DETAILS B " & vbCrLf &
                    "        WHERE A.ID = B.GM_ID " & vbCrLf &
                    "        AND A.PO_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ") " & vbCrLf &
                    "        GROUP BY B.ITEM_ID " & vbCrLf &
                    "      ) C ON C.ITEM_ID = A.ITEM_ID " & vbCrLf &
                    "    WHERE A.PO_HEADER_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ") " & vbCrLf &
                    "  ) A "
                    dtTable = ConnectionDB.GetDataTable(Query)
                    If dtTable.Rows.Count > 0 Then
                        If CInt(dtTable.Rows(0).Item(0)) <= 0 Then
                            Query = "UPDATE TPROC_PO_HEADERS " & vbCrLf &
                        "SET PO_STATUS = " & ListEnum.PO.Closed & vbCrLf &
                        "WHERE ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ")"
                            ConnectionDB.ExecuteQuery(Query)
                        End If
                    End If

                    Query = "UPDATE TPROC_PR_HEADER " & vbCrLf &
                    "SET PR_STATUS = " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    SELECT " & ListEnum.PRStatus.Complete & " " & vbCrLf &
                    "    FROM " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT b.ITEM_ID " & vbCrLf &
                    "          , b.QUANTITY " & vbCrLf &
                    "        FROM TPROC_GM_HEADERS a, TPROC_GM_DETAILS b " & vbCrLf &
                    "        WHERE a.ID = " & _ID & vbCrLf &
                    "        AND a.ID = b.GM_ID " & vbCrLf &
                    "      ) a " & vbCrLf &
                    "    LEFT JOIN " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT b.ITEM_ID " & vbCrLf &
                    "          , SUM(b.QUANTITY) AS QUANTITY " & vbCrLf &
                    "        FROM TPROC_GM_HEADERS a, TPROC_GM_DETAILS b " & vbCrLf &
                    "        WHERE a.ID <> " & _ID & vbCrLf &
                    "        AND a.PO_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ") " & vbCrLf &
                    "        AND a.ROW_STATUS = " & RowStatus.Complete & vbCrLf &
                    "        AND a.ID = b.GM_ID " & vbCrLf &
                    "        GROUP BY b.ITEM_ID " & vbCrLf &
                    "      ) b ON b.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "    INNER JOIN " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT a.ITEM_ID " & vbCrLf &
                    "          , a.QUANTITY " & vbCrLf &
                    "          , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) - CAST(a.QUANTITY AS FLOAT) AS MINQTY " & vbCrLf &
                    "          , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) AS MAXQTY " & vbCrLf &
                    "          , b.ID AS PR_ID " & vbCrLf &
                    "          , b.ROW_STATUS " & vbCrLf &
                    "        FROM TPROC_PO_DETAILS a " & vbCrLf &
                    "        LEFT JOIN TPROC_PR_HEADER b ON b.PR_NO = a.PR_HEADER_NO " & vbCrLf &
                    "        LEFT JOIN TPROC_PR_DETAIL c ON c.PR_HEADER_ID = b.ID AND c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "        WHERE a.PO_DTLS_ITEM_ID IN (SELECT ID FROM TPROC_PO_DETAILS_ITEM WHERE PO_HEADER_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ")) " & vbCrLf &
                    "        ORDER BY a.PO_DTLS_ITEM_ID ASC, a.ID ASC " & vbCrLf &
                    "      ) c ON c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "      AND COALESCE(b.QUANTITY, 0) + COALESCE(a.QUANTITY, 0) >= COALESCE(c.MAXQTY, 0) " & vbCrLf &
                    "      AND c.ROW_STATUS <= " & ListEnum.PRStatus.Complete & " " & vbCrLf &
                    "    WHERE c.PR_ID = TPROC_PR_HEADER.ID " & vbCrLf &
                    "    GROUP BY PR_STATUS " & vbCrLf &
                    "  ) " & vbCrLf &
                    "WHERE EXISTS " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    SELECT c.PR_ID " & vbCrLf &
                    "    FROM " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT b.ITEM_ID " & vbCrLf &
                    "          , b.QUANTITY " & vbCrLf &
                    "        FROM TPROC_GM_HEADERS a, TPROC_GM_DETAILS b " & vbCrLf &
                    "        WHERE a.ID = " & _ID & vbCrLf &
                    "        AND a.ID = b.GM_ID " & vbCrLf &
                    "      ) a " & vbCrLf &
                    "    LEFT JOIN " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT b.ITEM_ID " & vbCrLf &
                    "          , SUM(b.QUANTITY) AS QUANTITY " & vbCrLf &
                    "        FROM TPROC_GM_HEADERS a, TPROC_GM_DETAILS b " & vbCrLf &
                    "        WHERE a.ID <> " & _ID & vbCrLf &
                    "        AND a.PO_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ") " & vbCrLf &
                    "        AND a.ROW_STATUS = " & RowStatus.Complete & vbCrLf &
                    "        AND a.ID = b.GM_ID " & vbCrLf &
                    "        GROUP BY b.ITEM_ID " & vbCrLf &
                    "      ) b ON b.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "    INNER JOIN " & vbCrLf &
                    "      ( " & vbCrLf &
                    "        SELECT a.ITEM_ID " & vbCrLf &
                    "          , a.QUANTITY " & vbCrLf &
                    "          , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) - CAST(a.QUANTITY AS FLOAT) AS MINQTY " & vbCrLf &
                    "          , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) AS MAXQTY " & vbCrLf &
                    "          , b.ID AS PR_ID " & vbCrLf &
                    "          , b.ROW_STATUS " & vbCrLf &
                    "        FROM TPROC_PO_DETAILS a " & vbCrLf &
                    "        LEFT JOIN TPROC_PR_HEADER b ON b.PR_NO = a.PR_HEADER_NO " & vbCrLf &
                    "        LEFT JOIN TPROC_PR_DETAIL c ON c.PR_HEADER_ID = b.ID AND c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "        WHERE a.PO_DTLS_ITEM_ID IN (SELECT ID FROM TPROC_PO_DETAILS_ITEM WHERE PO_HEADER_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ")) " & vbCrLf &
                    "        ORDER BY a.PO_DTLS_ITEM_ID ASC, a.ID ASC " & vbCrLf &
                    "      ) c ON c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "      AND COALESCE(b.QUANTITY, 0) + COALESCE(a.QUANTITY, 0) >= COALESCE(c.MAXQTY, 0) " & vbCrLf &
                    "      AND c.ROW_STATUS <= " & ListEnum.PRStatus.Complete & " " & vbCrLf &
                    "    WHERE c.PR_ID = TPROC_PR_HEADER.ID " & vbCrLf &
                    "    GROUP BY c.PR_ID " & vbCrLf &
                    "  ) "
                    ConnectionDB.ExecuteQuery(Query)

                    Query = "INSERT INTO TPROC_PR_HISTORICAL " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    PR_HEADER_ID, HISTORICAL_DT, HISTORICAL_STATUS, HISTORICAL_BY " & vbCrLf &
                    "    , CREATED_TIME, CREATED_BY, QUEUE " & vbCrLf &
                    "  ) " & vbCrLf &
                    "SELECT c.PR_ID AS PR_HEADER_ID " & vbCrLf &
                    "  , SYSDATE AS HISTORICAL_DT " & vbCrLf &
                    "  , 'Complete' AS HISTORICAL_STATUS " & vbCrLf &
                    "  , '" & Session("USER_NAME") & "' AS HISTORICAL_BY " & vbCrLf &
                    "  , SYSDATE AS CREATED_TIME " & vbCrLf &
                    "  , '" & Session("USER_ID") & "' AS CREATED_BY " & vbCrLf &
                    "  , 0 AS QUEUE " & vbCrLf &
                    "FROM " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    SELECT b.ITEM_ID " & vbCrLf &
                    "      , b.QUANTITY " & vbCrLf &
                    "    FROM TPROC_GM_HEADERS a, TPROC_GM_DETAILS b " & vbCrLf &
                    "    WHERE a.ID = " & _ID & " " & vbCrLf &
                    "    AND a.ID = b.GM_ID " & vbCrLf &
                    "  ) a " & vbCrLf &
                    "LEFT JOIN " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    SELECT b.ITEM_ID " & vbCrLf &
                    "      , SUM(b.QUANTITY) AS QUANTITY " & vbCrLf &
                    "    FROM TPROC_GM_HEADERS a, TPROC_GM_DETAILS b " & vbCrLf &
                    "    WHERE a.ID <> " & _ID & " " & vbCrLf &
                    "    AND a.PO_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ") " & vbCrLf &
                    "    AND a.ROW_STATUS = " & RowStatus.Complete & vbCrLf &
                    "    AND a.ID = b.GM_ID " & vbCrLf &
                    "    GROUP BY b.ITEM_ID " & vbCrLf &
                    "  ) b On b.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "INNER JOIN " & vbCrLf &
                    "  ( " & vbCrLf &
                    "    Select a.ITEM_ID " & vbCrLf &
                    "      , a.QUANTITY " & vbCrLf &
                    "      , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) - CAST(a.QUANTITY As FLOAT) As MINQTY " & vbCrLf &
                    "      , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) As MAXQTY " & vbCrLf &
                    "      , b.ID As PR_ID " & vbCrLf &
                    "      , b.ROW_STATUS " & vbCrLf &
                    "    FROM TPROC_PO_DETAILS a " & vbCrLf &
                    "    LEFT JOIN TPROC_PR_HEADER b On b.PR_NO = a.PR_HEADER_NO " & vbCrLf &
                    "    LEFT JOIN TPROC_PR_DETAIL c On c.PR_HEADER_ID = b.ID And c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "    WHERE a.PO_DTLS_ITEM_ID In (Select ID FROM TPROC_PO_DETAILS_ITEM WHERE PO_HEADER_ID = (Select PO_ID FROM TPROC_GM_HEADERS WHERE ID = " & _ID & ")) " & vbCrLf &
                    "    ORDER BY a.PO_DTLS_ITEM_ID ASC, a.ID ASC " & vbCrLf &
                    "  ) c On c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                    "  And COALESCE(b.QUANTITY, 0) + COALESCE(a.QUANTITY, 0) >= COALESCE(c.MAXQTY, 0) " & vbCrLf &
                    "  And c.ROW_STATUS <= " & ListEnum.PRStatus.Complete & " "

                    ConnectionDB.ExecuteQuery(Query)

                    fo_ORATransaction.Commit()
                    rs.SetSuccessStatus("Data has been approved")
                Catch ex As Exception
                    fo_ORATransaction.Rollback()
                    Throw
                End Try
            Catch ex As Exception
                rs.SetErrorStatus("Failed approve With Error : " & ex.Message)
            End Try

            Return Json(rs.MessageText)
        End Function

        <CAuthorize(Role:="MNU63")>
        Function ActionSubmit(ByVal _ID As String,
                              ByVal _GM_NUMBER As String,
                              ByVal _PO_TYPE As String,
                              ByVal _PO_ID As String,
                              ByVal _JSONDetailDataTable As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                Dim dtTableDetail As DataTable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(_JSONDetailDataTable)

                Dim fo_ORAConnection As New Oracle.ManagedDataAccess.Client.OracleConnection
                Dim fo_ORACommand As Oracle.ManagedDataAccess.Client.OracleCommand
                Dim fo_ORATransaction As Oracle.ManagedDataAccess.Client.OracleTransaction
                Try
                    fo_ORAConnection = ConnectionDB.CreateConnection
                    fo_ORATransaction = fo_ORAConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    fo_ORACommand = fo_ORAConnection.CreateCommand
                    fo_ORACommand.Transaction = fo_ORATransaction

                    If _ID <> "" Then
                        Query = "UPDATE TPROC_GM_HEADERS " & vbCrLf &
                        "SET GM_NUMBER = '" & _GM_NUMBER & "' " & vbCrLf &
                        "   , PO_ID = '" & _PO_ID & "' " & vbCrLf &
                        "   , ROW_STATUS = '" & RowStatus.Approve & "' " & vbCrLf &
                        "   , LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                        "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                        "WHERE ID = '" & _ID & "' "
                    Else
                        Query =
                        "SELECT 'GM' || TO_CHAR(SYSDATE, 'YYYY') || SUBSTR('00000000', 1, 8 - LENGTH((COALESCE(a.MAXSEQUENCE, 0) + 1))) || (COALESCE(a.MAXSEQUENCE, 0) + 1) AS GENERATENO  " & vbCrLf &
                        "FROM " & vbCrLf &
                        "  ( " & vbCrLf &
                        "    SELECT CAST(MAX(SUBSTR(GM_NUMBER, 7, 8)) As NUMBER) As MAXSEQUENCE " & vbCrLf &
                        "    FROM TPROC_GM_HEADERS " & vbCrLf &
                        "    WHERE SUBSTR(GM_NUMBER, 3, 4) = TO_CHAR(SYSDATE, 'YYYY') " & vbCrLf &
                        ") a "
                        Dim dtGENNO As DataTable = ConnectionDB.GetDataTable(Query)
                        GM_NUMBER = dtGENNO.Rows(0).Item(0) & _PO_TYPE

                        Query = "Select COALESCE((Select MAX(COALESCE(ID, 0)) + 1 As ID FROM TPROC_GM_HEADERS), 1) As ID FROM DUAL"
                        dtTable = ConnectionDB.GetDataTable(Query)
                        Query = "INSERT INTO TPROC_GM_HEADERS " & vbCrLf &
                        "( " & vbCrLf &
                        "   ID, GM_NUMBER, PO_ID, ROW_STATUS, CREATED_BY, CREATED_TIME " & vbCrLf &
                        ") " & vbCrLf &
                        "VALUES " & vbCrLf &
                        "( " & vbCrLf &
                        "   " & dtTable.Rows(0).Item("ID") & " " & vbCrLf &
                        "   , '" & GM_NUMBER & "' " & vbCrLf &
                        "   , '" & _PO_ID & "' " & vbCrLf &
                        "   , '" & RowStatus.Approve & "' " & vbCrLf &
                        "   , '" & Session("USER_ID") & "' " & vbCrLf &
                        "   , SYSDATE " & vbCrLf &
                        ") "
                    End If
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_GM_DETAILS WHERE GM_ID = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        If Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) > 0 Then
                            Query = "INSERT INTO TPROC_GM_DETAILS " & vbCrLf &
                                    "( " & vbCrLf &
                                    "   ID, GM_ID, ITEM_ID, ITEM_NAME, UNITMEASUREMENT, QUANTITY " & vbCrLf &
                                    "   , PRICE, CREATED_TIME, CREATED_BY " & vbCrLf &
                                    ") " & vbCrLf &
                                    "VALUES " & vbCrLf &
                                    "( " & vbCrLf &
                                    "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_GM_DETAILS), 1) AS ID FROM DUAL) " & vbCrLf &
                                    "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(0) & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(1) & "' " & vbCrLf &
                                    "   , '" & dtTableDetail.Rows(i).Item(2) & "' " & vbCrLf &
                                    "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) & "' " & vbCrLf &
                                    "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(5)) & "' " & vbCrLf &
                                    "   , SYSDATE " & vbCrLf &
                                    "   , '" & Session("USER_ID") & "' " & vbCrLf &
                                    ") "
                            ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
                        End If
                    Next

                    fo_ORATransaction.Commit()
                    rs.SetSuccessStatus("Data has been submited")
                Catch ex As Exception
                    fo_ORATransaction.Rollback()
                    Throw
                End Try
            Catch ex As Exception
                rs.SetErrorStatus("Failed submit with error : " & ex.Message)
            End Try

            Return Json(rs.MessageText)
        End Function


        <CAuthorize(Role:="MNU63")>
        Function ActionRejected(ByVal _ID As String,
                                ByVal _RejectedNote As String) As ActionResult
            Dim MsgJSON As String = "Data has been rejected"
            Try
                Query = "UPDATE TPROC_GM_HEADERS " & vbCrLf &
                "SET REJECTED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , REJECTED_TIME = SYSDATE " & vbCrLf &
                "   , REJECTNOTE = '" & _RejectedNote & "' " & vbCrLf &
                "   , ROW_STATUS = '" & RowStatus.Rejected & "' " & vbCrLf &
                "WHERE ID = " & _ID
                ConnectionDB.ExecuteQuery(Query)

                Dim EmailTo As New eProcurementApps.Helpers.ListFieldNameAndValue
                Query = "SELECT a.GM_NUMBER " & vbCrLf &
                "  , COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) AS CREATED_BY " & vbCrLf &
                "  , a.REJECTED_BY " & vbCrLf &
                "  , b.USER_MAIL " & vbCrLf &
                "FROM TPROC_GM_HEADERS a, TPROC_USER b " & vbCrLf &
                "WHERE COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) = b.USER_ID " & vbCrLf &
                "And a.ID = " & _ID
                Dim dtTableEmail As DataTable = ConnectionDB.GetDataTable(Query)
                For i As Integer = 0 To dtTableEmail.Rows.Count - 1
                    EmailTo.AddItem(dtTableEmail.Rows(i).Item(1), dtTableEmail.Rows(i).Item(3))
                Next

                Dim strBodyDetail As String = "FYI your GM Number " & dtTableEmail.Rows(0).Item(0) & " already rejected by " & dtTableEmail.Rows(0).Item(2)
                Dim strBody As String = " <table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                                            <tr>
                                                <td>
                                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                                        <tr> " _
                                                            & strBodyDetail &
                                                        "</tr>
                                                        <tr>
                                                        Reject Reason : " _
                                                             & _RejectedNote &
                                                        "</tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>
                                                                <b><a href='" & Microsoft.VisualBasic.Strings.Replace(Request.Url.AbsoluteUri, "ActionRejected", "FormInput?_Id=" & _ID & "&_Action=Rejected") & "'>Click here to View Details</a></b>
                                                            </td>
                                                        </tr> 
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>"
                eProcurementApps.Helpers.Emails.SendEmail(ConfigurationSettings.AppSettings("EmailSender"),
                                                          EmailTo,
                                                          Nothing,
                                                          "[eProcurement] – Number : " & dtTableEmail.Rows(0).Item(0) & " - GM has been rejected ",
                                                          strBody, "")
            Catch ex As Exception
                MsgJSON = "Failed reject with error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function
    End Class
End Namespace