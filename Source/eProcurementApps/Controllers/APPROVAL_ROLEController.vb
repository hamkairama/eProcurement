Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports eProcurementApps.DataAccess

Namespace Controllers
    Public Class APPROVAL_ROLEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities
        Public Shared APPROVAL_ROLE_Action As String
        Public Shared dtTable As DataTable
        Public Shared dtTableDetail As DataTable
        Dim Query As String
        Dim fs_ByteArray As String

        Public Sub New()
            Query = "SELECT A.ID " & vbCrLf &
            "  , A.USER_ID " & vbCrLf &
            "  , A.USER_NAME " & vbCrLf &
            "  , A.EMAIL " & vbCrLf &
            "  , A.LEVEL_ID " & vbCrLf &
            "  , B.INDONESIAN_LEVEL " & vbCrLf &
            "  , A.SIGNATURE_IMAGE " & vbCrLf &
            "FROM TPROC_APPROVAL_ROLE A " & vbCrLf &
            "LEFT JOIN TPROC_LEVEL B ON B.ID = A.LEVEL_ID "
        End Sub

        <CAuthorize(Role:="MNU62")>
        Function Index() As ActionResult
            Query = Query & vbCrLf &
            "WHERE a.ROW_STATUS = " & ListEnum.RowStat.Live & vbCrLf &
            "ORDER BY a.ID ASC "
            dtTable = ConnectionDB.GetDataTable(Query)

            Return View()
        End Function

        <CAuthorize(Role:="MNU62")>
        Function FormInput(Optional ByVal _Id As String = "-99",
                           Optional ByVal _Action As String = "Create") As ActionResult
            Session("Active_Directory") = "APPROVAL_ROLE"
            Query = Query & vbCrLf &
            IIf(_Action = "Create", "WHERE 1=2", "WHERE a.ID = " & _Id)
            dtTable = ConnectionDB.GetDataTable(Query)

            Query =
            "SELECT A.FORMNAME, SUM(VERIFY) AS VERIFY, SUM(APPROVER) AS APPROVER, SUM(RECEIVED) AS RECEIVED, SUM(PAID) AS PAID  " & vbCrLf &
            "FROM " & vbCrLf &
            "  (" & vbCrLf &
            "    SELECT A.FORMNAME " & vbCrLf &
            "      , CASE WHEN A.FORMNAME = 'Good Match' THEN NULL " & vbCrLf &
            "        ELSE " & vbCrLf &
            "           CASE WHEN B.AS_IS = 'Verifier' THEN 1 ELSE 0 END " & vbCrLf &
            "        END AS VERIFY " & vbCrLf &
            "      , CASE WHEN B.AS_IS = 'Approver' THEN 1 ELSE 0 END AS APPROVER " & vbCrLf &
            "      , CASE WHEN A.FORMNAME IN ('Good Match', 'PC', 'PO') THEN NULL " & vbCrLf &
            "        ELSE " & vbCrLf &
            "           CASE WHEN B.AS_IS = 'Received' THEN 1 ELSE 0 END " & vbCrLf &
            "        END AS RECEIVED " & vbCrLf &
            "      , CASE WHEN A.FORMNAME IN ('Good Match', 'PC', 'PO') THEN NULL " & vbCrLf &
            "        ELSE " & vbCrLf &
            "           CASE WHEN B.AS_IS = 'Paid' THEN 1 ELSE 0 END " & vbCrLf &
            "        END AS PAID " & vbCrLf &
            "    FROM " & vbCrLf &
            "      ( " & vbCrLf &
            "        SELECT 'PC' AS FORMNAME FROM DUAL " & vbCrLf &
            "        UNION ALL " & vbCrLf &
            "        SELECT 'PO' AS FORMNAME FROM DUAL " & vbCrLf &
            "        UNION ALL " & vbCrLf &
            "        SELECT 'Good Match' AS FORMNAME FROM DUAL " & vbCrLf &
            "        UNION ALL " & vbCrLf &
            "        SELECT 'CRV' AS FORMNAME FROM DUAL " & vbCrLf &
            "      ) A " & vbCrLf &
            "    LEFT JOIN " & vbCrLf &
            "      ( " & vbCrLf &
            "        SELECT ROLE_NAME " & vbCrLf &
            "          , AS_IS " & vbCrLf &
            "        From TPROC_APPROVAL_ROLE_DETAIL " & vbCrLf &
            "        Where ID_APPROVAL_ROLE = '" & _Id & "'" & vbCrLf &
            "      ) B On B.ROLE_NAME = A.FORMNAME " & vbCrLf &
            "  ) A " & vbCrLf &
            "GROUP BY FORMNAME "
            dtTableDetail = ConnectionDB.GetDataTable(Query)
            APPROVAL_ROLE_Action = _Action
            Return View("FormInput")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU62")>
        Function ActionCreate(ByVal USER_ID As String,
                              ByVal USER_NAME As String,
                              ByVal EMAIL As String,
                              ByVal LEVEL_ID As String,
                              ByVal SIGNATURE_IMAGE As String,
                              ByVal _JSONDetailDataTable As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                Query = "Select COALESCE((Select MAX(COALESCE(ID, 0)) + 1 As ID FROM TPROC_APPROVAL_ROLE), 1) As ID FROM DUAL"
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

                    Query = "INSERT INTO TPROC_APPROVAL_ROLE " & vbCrLf &
                    "( " & vbCrLf &
                    "   ID, USER_ID, USER_NAME, EMAIL, LEVEL_ID " & vbCrLf &
                    "   , CREATED_TIME, CREATED_BY, SIGNATURE_IMAGE " & vbCrLf &
                    ") " & vbCrLf &
                    "VALUES " & vbCrLf &
                    "( " & vbCrLf &
                    "   '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                    "   , '" & USER_ID & "' " & vbCrLf &
                    "   , '" & USER_NAME & "' " & vbCrLf &
                    "   , '" & EMAIL & "' " & vbCrLf &
                    "   , '" & LEVEL_ID & "' " & vbCrLf &
                    "   , SYSDATE " & vbCrLf &
                    "   , '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , :SIGNATURE_IMAGE " & vbCrLf &
                    ") "
                    Dim blobParameter As New Oracle.ManagedDataAccess.Client.OracleParameter
                    blobParameter.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.Blob
                    blobParameter.ParameterName = "SIGNATURE_IMAGE"
                    blobParameter.Value = System.Convert.FromBase64String(Split(SIGNATURE_IMAGE, ",")(1))
                    fo_ORACommand.Parameters.Add(blobParameter)
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_APPROVAL_ROLE_DETAIL WHERE ID_APPROVAL_ROLE = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        For a As Integer = 1 To dtTableDetail.Columns.Count - 1
                            If CBool(dtTableDetail.Rows(i).Item(a)) = True Then
                                Query = "INSERT INTO TPROC_APPROVAL_ROLE_DETAIL " & vbCrLf &
                                "( " & vbCrLf &
                                "   ID, ID_APPROVAL_ROLE, ROLE_NAME, AS_IS " & vbCrLf &
                                ") " & vbCrLf &
                                "VALUES " & vbCrLf &
                                "( " & vbCrLf &
                                "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_APPROVAL_ROLE_DETAIL), 1) AS ID FROM DUAL) " & vbCrLf &
                                "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(0) & "' " & vbCrLf
                                If a = 1 Then
                                    Query = Query & "   , 'Verifier' " & vbCrLf
                                ElseIf a = 2 Then
                                    Query = Query & "   , 'Approver' " & vbCrLf
                                ElseIf a = 3 Then
                                    Query = Query & "   , 'Received' " & vbCrLf
                                ElseIf a = 4 Then
                                    Query = Query & "   , 'Paid' " & vbCrLf
                                End If
                                Query = Query & ") "
                                ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
                            End If
                        Next
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

        <CAuthorize(Role:="MNU62")>
        Function ActionEdit(ByVal ID As Decimal,
                            ByVal USER_ID As String,
                            ByVal USER_NAME As String,
                            ByVal EMAIL As String,
                            ByVal LEVEL_ID As String,
                            ByVal SIGNATURE_IMAGE As String,
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

                    Query = "UPDATE TPROC_APPROVAL_ROLE " & vbCrLf &
                    "SET USER_ID = '" & USER_ID & "' " & vbCrLf &
                    "   , USER_NAME = '" & USER_NAME & "' " & vbCrLf &
                    "   , EMAIL = '" & EMAIL & "' " & vbCrLf &
                    "   , LEVEL_ID = '" & LEVEL_ID & "' " & vbCrLf &
                    "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                    "   , LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , SIGNATURE_IMAGE = :SIGNATURE_IMAGE " & vbCrLf &
                    "WHERE ID = '" & ID & "' "

                    Dim blobParameter As New Oracle.ManagedDataAccess.Client.OracleParameter
                    blobParameter.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.Blob
                    blobParameter.ParameterName = "SIGNATURE_IMAGE"
                    blobParameter.Value = System.Convert.FromBase64String(Split(SIGNATURE_IMAGE, ",")(1))
                    fo_ORACommand.Parameters.Add(blobParameter)
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_APPROVAL_ROLE_DETAIL WHERE ID_APPROVAL_ROLE = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        For a As Integer = 1 To dtTableDetail.Columns.Count - 1
                            If CBool(dtTableDetail.Rows(i).Item(a)) = True Then
                                Query = "INSERT INTO TPROC_APPROVAL_ROLE_DETAIL " & vbCrLf &
                                "( " & vbCrLf &
                                "   ID, ID_APPROVAL_ROLE, ROLE_NAME, AS_IS " & vbCrLf &
                                ") " & vbCrLf &
                                "VALUES " & vbCrLf &
                                "( " & vbCrLf &
                                "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_APPROVAL_ROLE_DETAIL), 1) AS ID FROM DUAL) " & vbCrLf &
                                "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(0) & "' " & vbCrLf
                                If a = 1 Then
                                    Query = Query & "   , 'Verifier' " & vbCrLf
                                ElseIf a = 2 Then
                                    Query = Query & "   , 'Approver' " & vbCrLf
                                ElseIf a = 3 Then
                                    Query = Query & "   , 'Received' " & vbCrLf
                                ElseIf a = 4 Then
                                    Query = Query & "   , 'Paid' " & vbCrLf
                                End If
                                Query = Query & ") "
                                ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
                            End If
                        Next
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

        <CAuthorize(Role:="MNU62")>
        Function ActionDelete(ByVal ID As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Dim APPROVAL_ROLE As New TPROC_APPROVAL_ROLE
            Try
                APPROVAL_ROLE = db.TPROC_APPROVAL_ROLE.Find(ID)
                APPROVAL_ROLE.LAST_MODIFIED_BY = Session("USER_ID")
                APPROVAL_ROLE.LAST_MODIFIED_TIME = Date.Now
                APPROVAL_ROLE.ROW_STATUS = ListEnum.RowStat.InActive
                db.Entry(APPROVAL_ROLE).State = EntityState.Modified
                db.SaveChanges()

                rs.SetSuccessStatus("Data has been deleted")
            Catch ex As Exception
                rs.SetErrorStatus("Failed insert to db")
            End Try

            Return Json(rs.MessageText)
        End Function
    End Class
End Namespace