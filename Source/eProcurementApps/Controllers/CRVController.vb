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
    Public Class CRVController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities
        Public Shared CRV_Action As String
        Public Shared dtTable As DataTable
        Public Shared dtTableDetail As DataTable
        Public Shared JSONSupplierDataTable As String
        Public Shared PrintID As String
        Public Shared FLAGFORM As String
        Dim Query As String

        Public Enum RowStatus As Integer
            Rejected = -2
            InActive = -1
            Active = 0
            Verify = 1
            Approve = 2
            Received = 3
            Paid = 4
            Complete = 5
        End Enum

        Public Sub New()
            Query =
            "SELECT a.ID " & vbCrLf &
            "  , a.CRV_NUM " & vbCrLf &
            "  , a.SUPPLIER_ID " & vbCrLf &
            "  , b.SUPPLIER_NAME " & vbCrLf &
            "  , b.SUPPLIER_ADDRESS " & vbCrLf &
            "  , a.PO_ID " & vbCrLf &
            "  , c.GM_NUMBER " & vbCrLf &
            "  , d.PO_NUMBER " & vbCrLf &
            "  , a.PAYMENTMETHOD " & vbCrLf &
            "  , a.BANK_ACCOUNT_NUMBER " & vbCrLf &
            "  , a.BANK_NAME " & vbCrLf &
            "  , a.BANK_BRANCH " & vbCrLf &
            "  , a.REFTAXNO " & vbCrLf &
            "  , CAST(a.SUB_TOTAL AS NUMBER) AS SUB_TOTAL " & vbCrLf &
            "  , CAST(a.DSCNT_AMT AS NUMBER) AS DSCNT_AMT " & vbCrLf &
            "  , CAST(a.VAT AS NUMBER) AS VAT " & vbCrLf &
            "  , CAST(a.WTH_TAX AS NUMBER) AS WTH_TAX " & vbCrLf &
            "  , CAST(a.GRAND_TOTAL AS NUMBER) AS GRAND_TOTAL " & vbCrLf &
            "  , a.CREATED_BY " & vbCrLf &
            "  , a.CREATED_TIME " & vbCrLf &
            "  , a.VERIFY_BY " & vbCrLf &
            "  , a.VERIFY_TIME " & vbCrLf &
            "  , a.COMPLETED_BY " & vbCrLf &
            "  , a.COMPLETED_DATE " & vbCrLf &
            "  , a.REJECTED_BY " & vbCrLf &
            "  , a.REJECTED_TIME " & vbCrLf &
            "  , a.REJECTNOTE " & vbCrLf &
            "  , a.ROW_STATUS " & vbCrLf &
            "  , a.KLIRINGNO " & vbCrLf &
            "  , a.LAST_MODIFIED_TIME " & vbCrLf &
            "  , a.APPROVE_BY " & vbCrLf &
            "  , a.APPROVE_DATE " & vbCrLf &
            "  , a.RECEIVED_BY " & vbCrLf &
            "  , a.RECEIVED_DATE " & vbCrLf &
            "  , CASE WHEN a.ROW_STATUS = '" & RowStatus.Active & "' THEN 'Still preparation' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.InActive & "' THEN 'Not active' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Rejected & "' THEN 'Rejected' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Verify & "' THEN 'Waiting to verify' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Approve & "' THEN 'Waiting to approve' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Received & "' THEN 'Waiting receive by finance' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Paid & "' THEN 'Waiting paid by finance' " & vbCrLf &
            "    WHEN a.ROW_STATUS = '" & RowStatus.Complete & "' THEN 'Completed' END AS STATUS " & vbCrLf &
            "FROM TPROC_CRV a " & vbCrLf &
            "LEFT JOIN TPROC_SUPPLIER b ON b.ID = a.SUPPLIER_ID " & vbCrLf &
            "LEFT JOIN TPROC_GM_HEADERS c ON c.ID = a.PO_ID " & vbCrLf &
            "LEFT JOIN TPROC_PO_HEADERS d ON d.ID = c.PO_ID "
        End Sub


        <CAuthorize(Role:="MNU57")>
        Function ListCrvByStatus(txt_status_crv_val As String, txt_date_from As String, txt_date_to As String) As ActionResult
            TempData("message") = txt_status_crv_val
            TempData("date_from") = txt_date_from
            TempData("date_to") = txt_date_to

            If txt_status_crv_val = "" Then
                TempData("message") = "null"
            End If


            Return RedirectToAction("Index")
        End Function


        <CAuthorize(Role:="MNU57")>
        Function Index() As ActionResult
            dtTable = Nothing
            Dim query_where = ""

            ViewBag.Message = TempData("message")

            If TempData("message") = "null" Then
                ViewBag.Message = "Please select status"
                Return View()
            End If

            If TempData("message") IsNot Nothing And TempData("message") <> "null" Then
                query_where = GetDataCrvByStatus(TempData("message"), TempData("date_from"), TempData("date_to"))

                Query = Query & vbCrLf & query_where & vbCrLf &
                        "ORDER BY a.ID ASC "
                '"WHERE a.ROW_STATUS Not In ('" & RowStatus.InActive & "', '" & RowStatus.Rejected & "', '" & RowStatus.Complete & "')" & vbCrLf &

                dtTable = ConnectionDB.GetDataTable(Query)

                ViewBag.Message = Nothing
            End If

            Return View()
        End Function


        <CAuthorize(Role:="MNU57")>
        Function GetDataCrvByStatus(txt_status_crv_val As String, from As String, tto As String) As String
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

        <CAuthorize(Role:="MNU57")>
        Function FormInput(Optional ByVal _Id As String = "-99",
                           Optional ByVal _Action As String = "Create",
                           Optional ByVal _FlagForm As String = "1") As ActionResult
            Query = Query & vbCrLf &
            IIf(_Action = "Create", "WHERE 1=2", "WHERE a.ID = " & _Id)
            dtTable = ConnectionDB.GetDataTable(Query)

            Query =
            "SELECT SUBSTR('000', 1, 3 - LENGTH(ROW_NUMBER() OVER (ORDER BY a.CRV_HEADER_ID ASC))) || ROW_NUMBER() OVER (ORDER BY a.CRV_HEADER_ID ASC) AS NO " & vbCrLf &
            "  , a.ITEM_ID AS ITEM_ID " & vbCrLf &
            "  , a.UNITMEASUREMENT AS UNITMEASUREMENT " & vbCrLf &
            "  , a.QUANTITY AS QUANTITY " & vbCrLf &
            "  , a.PRICE AS PRICE " & vbCrLf &
            "  , a.ITEM_NAME AS DESCRIPTION " & vbCrLf &
            "  , b.CURRENCY_NAME AS CURR " & vbCrLf &
            "  , a.ACCOUNT_CODE AS ""ACCOUNT_DEBIT"" " & vbCrLf &
            "  , CAST(a.SUBTOTAL AS NUMBER) AS ""OTH_AMOUNT"" " & vbCrLf &
            "  , CAST(a.SUBTOTAL AS NUMBER) AS ""RUPIAH_AMOUNT"" " & vbCrLf &
            "  , a.FUND_T1 AS ""FUND_T1"" " & vbCrLf &
            "  , a.LOB1_T2 AS ""LOB1_T2"" " & vbCrLf &
            "  , a.PLAN_T3 AS ""PLAN_T3"" " & vbCrLf &
            "  , a.WA_T4 AS ""WA_T4"" " & vbCrLf &
            "  , a.LOB2_T5 AS ""LOB2_T5"" " & vbCrLf &
            "  , a.CURRENCY " & vbCrLf &
            "FROM TPROC_CRV_DETAILS a " & vbCrLf &
            "LEFT JOIN TPROC_CURRENCY b ON b.ID = a.CURRENCY " & vbCrLf &
            "WHERE a.CRV_HEADER_ID = " & _Id & ""
            dtTableDetail = ConnectionDB.GetDataTable(Query)

            Query = "SELECT ID " & vbCrLf &
                    "   , SUPPLIER_NAME " & vbCrLf &
                    "   , BANK_NAME " & vbCrLf &
                    "   , BANK_BRANCH " & vbCrLf &
                    "   , BANK_ACCOUNT_NUMBER " & vbCrLf &
                    "   , NPWP AS TAX_NUMBER " & vbCrLf &
                    "FROM TPROC_SUPPLIER " & vbCrLf &
                    "WHERE ROW_STATUS = '" & ListEnum.RowStat.Live & "'"
            Dim dtSupplierTable As DataTable = ConnectionDB.GetDataTable(Query)
            dtSupplierTable.Rows.Add()
            JSONSupplierDataTable = Newtonsoft.Json.JsonConvert.SerializeObject(dtSupplierTable)

            CRV_Action = _Action
            FLAGFORM = _FlagForm
            Return View("FormInput")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU57")>
        Function GetDetail(ByVal REF_ID As String,
                           ByVal REF_NUMBER As String) As ActionResult
            Try
                If REF_ID = "" Then
                    REF_ID = "-99"
                End If
                dtTableDetail = Nothing

                Query =
                "SELECT SUBSTR('000', 1, 3 - LENGTH(ROW_NUMBER() OVER (ORDER BY a.ID ASC))) || ROW_NUMBER() OVER (ORDER BY a.ID ASC) AS NO " & vbCrLf &
                "  , b.ITEM_ID " & vbCrLf &
                "  , b.UNITMEASUREMENT " & vbCrLf &
                "  , CAST(b.QUANTITY AS VARCHAR2(50)) AS QUANTITY " & vbCrLf &
                "  , CAST(b.PRICE AS VARCHAR2(50)) AS PRICE " & vbCrLf &
                "  , b.DESCRIPTION " & vbCrLf &
                "  , a.CURRENCY " & vbCrLf &
                "  , c.CURRENCY_NAME AS CURR " & vbCrLf &
                "  , b.ACCOUNT_DEBIT " & vbCrLf &
                "  , b.OTH_AMOUNT " & vbCrLf &
                "  , b.RUPIAH_AMOUNT " & vbCrLf &
                "  , b.FUND_T1 " & vbCrLf &
                "  , b.LOB1_T2 " & vbCrLf &
                "  , b.PLAN_T3 " & vbCrLf &
                "  , b.WA_T4 " & vbCrLf &
                "  , b.LOB2_T5 " & vbCrLf &
                "  , SUM(b.RUPIAH_AMOUNT) OVER() SUB_TOTAL " & vbCrLf &
                "  , a.DSCNT_AMT " & vbCrLf &
                "  , a.VAT " & vbCrLf &
                "  , a.WTH_TAX " & vbCrLf &
                "  ,((SUM(b.RUPIAH_AMOUNT) OVER()) - a.DSCNT_AMT  + a.VAT - a.WTH_TAX) AS GRAND_TOTAL " & vbCrLf &
                " -- , a.GRAND_TOTAL " & vbCrLf &
                "  , b.ID " & vbCrLf &
                "  , a.ID AS PO_ID " & vbCrLf &
                "FROM TPROC_PO_HEADERS a " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "      SELECT b.ID " & vbCrLf &
                "        , a.PO_HEADER_ID " & vbCrLf &
                "        , b.ITEM_ID " & vbCrLf &
                "        , c.UNITMEASUREMENT " & vbCrLf &
                "        , c.QUANTITY " & vbCrLf &
                "        , c.PRICE " & vbCrLf &
                "        , c.ITEM_NAME AS DESCRIPTION " & vbCrLf &
                "        , b.ACCOUNT_CODE AS ACCOUNT_DEBIT " & vbCrLf &
                "        , c.QUANTITY * c.PRICE AS OTH_AMOUNT " & vbCrLf &
                "        , c.QUANTITY * c.PRICE AS RUPIAH_AMOUNT " & vbCrLf &
                "        , b.FUND_T1 " & vbCrLf &
                "        , b.LOB1_T2 " & vbCrLf &
                "        , b.PLAN_T3 " & vbCrLf &
                "        , b.WA_NUMBER AS WA_T4 " & vbCrLf &
                "        , b.LOB2_T5 " & vbCrLf &
                "      FROM TPROC_PO_DETAILS_ITEM a " & vbCrLf &
                "      LEFT JOIN " & vbCrLf &
                "      ( " & vbCrLf &
                "        SELECT a.ID " & vbCrLf &
                "          , a.PO_DTLS_ITEM_ID " & vbCrLf &
                "          , a.ITEM_ID " & vbCrLf &
                "          , a.ITEM_NAME " & vbCrLf &
                "          , a.USERMEASUREMENT " & vbCrLf &
                "          , a.QUANTITY " & vbCrLf &
                "          , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) - CAST(a.QUANTITY AS FLOAT) AS MINQTY " & vbCrLf &
                "          , SUM(a.QUANTITY) OVER (PARTITION BY a.PO_DTLS_ITEM_ID ORDER BY a.ID ASC) AS MAXQTY " & vbCrLf &
                "          , a.FUND_T1 " & vbCrLf &
                "          , a.LOB1_T2 " & vbCrLf &
                "          , a.PLAN_T3 " & vbCrLf &
                "          , a.LOB2_T5 " & vbCrLf &
                "          , b.ACCOUNT_CODE " & vbCrLf &
                "          , c.WA_NUMBER " & vbCrLf &
                "        FROM TPROC_PO_DETAILS a " & vbCrLf &
                "        LEFT JOIN TPROC_PR_HEADER b ON b.PR_NO = a.PR_HEADER_NO " & vbCrLf &
                "        LEFT JOIN TPROC_PR_DETAIL c ON c.PR_HEADER_ID = b.ID AND c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                "        WHERE a.PO_DTLS_ITEM_ID IN (SELECT ID FROM TPROC_PO_DETAILS_ITEM WHERE PO_HEADER_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = '" & REF_ID & "' AND GM_NUMBER = '" & REF_NUMBER & "')) " & vbCrLf &
                "        ORDER BY a.PO_DTLS_ITEM_ID ASC, a.ID ASC " & vbCrLf &
                "      ) b ON b.PO_DTLS_ITEM_ID = a.ID " & vbCrLf &
                "      AND a.ITEM_ID = b.ITEM_ID " & vbCrLf &
                "      LEFT JOIN " & vbCrLf &
                "        ( " & vbCrLf &
                "          SELECT a.PO_ID " & vbCrLf &
                "            , b.ITEM_ID " & vbCrLf &
                "            , b.ITEM_NAME " & vbCrLf &
                "            , b.UNITMEASUREMENT " & vbCrLf &
                "            , CAST(b.QUANTITY AS NUMBER) AS QUANTITY " & vbCrLf &
                "            , COALESCE(CAST(c.QUANTITY AS NUMBER), 0) AS MINQTY " & vbCrLf &
                "            , COALESCE(CAST(c.QUANTITY AS NUMBER), 0) + b.QUANTITY AS MAXQTY " & vbCrLf &
                "            , b.PRICE " & vbCrLf &
                "            , CAST(b.QUANTITY AS NUMBER) * CAST(b.PRICE AS NUMBER) AS SUBTOTAL " & vbCrLf &
                "          FROM TPROC_GM_HEADERS a " & vbCrLf &
                "          LEFT JOIN TPROC_GM_DETAILS b ON b.GM_ID = a.ID " & vbCrLf &
                "          LEFT JOIN " & vbCrLf &
                "            ( " & vbCrLf &
                "              SELECT -9999 AS ITEM_ID, 0 AS QUANTITY FROM DUAL " & vbCrLf &
                "              UNION ALL " & vbCrLf &
                "              SELECT b.ITEM_ID " & vbCrLf &
                "                , CAST(b.QUANTITY AS NUMBER) AS QUANTITY " & vbCrLf &
                "              FROM TPROC_GM_HEADERS a " & vbCrLf &
                "              LEFT JOIN TPROC_GM_DETAILS b ON b.GM_ID = a.ID " & vbCrLf &
                "              INNER JOIN " & vbCrLf &
                "                ( " & vbCrLf &
                "                  SELECT PO_ID " & vbCrLf &
                "                  FROM TPROC_CRV " & vbCrLf &
                "                  WHERE ID <> '-99' " & vbCrLf &
                "                  AND ROW_STATUS NOT IN ('" & RowStatus.InActive & "', '" & RowStatus.Rejected & "') " & vbCrLf &
                "                ) c ON c.PO_ID = a.ID " & vbCrLf &
                "              WHERE a.PO_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = '" & REF_ID & "' AND GM_NUMBER = '" & REF_NUMBER & "') " & vbCrLf &
                "            ) c ON c.ITEM_ID = b.ITEM_ID " & vbCrLf &
                "          WHERE a.ID = '" & REF_ID & "' " & vbCrLf &
                "          AND a.GM_NUMBER = '" & REF_NUMBER & "' " & vbCrLf &
                "          ORDER BY PO_ID ASC, ITEM_ID ASC " & vbCrLf &
                "        ) c ON c.PO_ID = a.PO_HEADER_ID " & vbCrLf &
                "        AND c.ITEM_ID = a.ITEM_ID " & vbCrLf &
                "      WHERE a.PO_HEADER_ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = '" & REF_ID & "' AND GM_NUMBER = '" & REF_NUMBER & "') " & vbCrLf &
                "      AND " & vbCrLf &
                "      ( " & vbCrLf &
                "        CASE WHEN (c.MINQTY >= b.MINQTY AND  c.MINQTY < b.MAXQTY) " & vbCrLf &
                "        AND c.MAXQTY BETWEEN b.MINQTY AND b.MAXQTY " & vbCrLf &
                "        THEN c.QUANTITY " & vbCrLf &
                "        WHEN (c.MINQTY >= b.MINQTY AND c.MINQTY < b.MAXQTY) " & vbCrLf &
                "        AND NOT (c.MAXQTY BETWEEN b.MINQTY AND b.MAXQTY) " & vbCrLf &
                "        THEN (b.MAXQTY - c.MINQTY) " & vbCrLf &
                "        WHEN b.MINQTY > C.MINQTY " & vbCrLf &
                "        AND b.MAXQTY < c.MAXQTY " & vbCrLf &
                "        THEN b.QUANTITY " & vbCrLf &
                "        WHEN NOT ((c.MINQTY >= b.MINQTY AND c.MINQTY < b.MAXQTY)) " & vbCrLf &
                "        AND c.MAXQTY BETWEEN b.MINQTY AND b.MAXQTY " & vbCrLf &
                "        AND (c.QUANTITY - (b.MINQTY - c.MINQTY)) > 0 " & vbCrLf &
                "        THEN (c.QUANTITY - (b.MINQTY - c.MINQTY)) " & vbCrLf &
                "        ELSE NULL END " & vbCrLf &
                "      ) IS NOT NULL " & vbCrLf &
                "  ) b ON b.PO_HEADER_ID = a.ID " & vbCrLf &
                "LEFT JOIN TPROC_CURRENCY c ON c.ID = a.CURRENCY " & vbCrLf &
                "WHERE a.ID = (SELECT PO_ID FROM TPROC_GM_HEADERS WHERE ID = '" & REF_ID & "' AND GM_NUMBER = '" & REF_NUMBER & "') "
                dtTableDetail = ConnectionDB.GetDataTable(Query)
            Catch ex As Exception
            End Try
            Return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dtTableDetail))
        End Function

        Function GetPO(ByVal SupplierID As String,
                       ByVal CRV_ID As String) As ActionResult
            Dim dtTablePO As New DataTable
            Try
                Query =
                "SELECT a.ID " & vbCrLf &
                "   , a.GM_NUMBER AS ""GM NUMBER"" " & vbCrLf &
                "   , b.PO_NUMBER AS ""PO NUMBER"" " & vbCrLf &
                "FROM TPROC_GM_HEADERS a " & vbCrLf &
                "LEFT JOIN TPROC_PO_HEADERS b ON b.ID = a.PO_ID " & vbCrLf &
                "WHERE b.SUPPLIER_ID = '" & SupplierID & "' " & vbCrLf &
                "AND a.ROW_STATUS = '" & GMController.RowStatus.Complete & "' " & vbCrLf &
                "AND (NOT EXISTS (SELECT c.PO_ID FROM TPROC_CRV c WHERE c.PO_ID = a.ID AND c.ROW_STATUS NOT IN ('" & RowStatus.InActive & "', '" & RowStatus.Rejected & "')) " & vbCrLf &
                "OR EXISTS (SELECT c.PO_ID FROM TPROC_CRV c WHERE c.ID= '" & CRV_ID & "' AND c.PO_ID = a.ID)) "
                dtTablePO = ConnectionDB.GetDataTable(Query)
                dtTablePO.Rows.Add()
            Catch ex As Exception
            End Try
            Return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dtTablePO))
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionCreate(ByVal _CRV_NUMBER As String,
                              ByVal _PO_ID As String,
                              ByVal _GMNUMBER As String,
                              ByVal _BANK_ACCOUNT_NUMBER As String,
                              ByVal _BANK_NAME As String,
                              ByVal _SUB_TOTAL As String,
                              ByVal _DSCNT_AMT As String,
                              ByVal _VAT As String,
                              ByVal _WTH_TAX As String,
                              ByVal _GRAND_TOTAL As String,
                              ByVal _SUPPLIER_ID As String,
                              ByVal _BANK_BRANCH As String,
                              ByVal _REFTAXNO As String,
                              ByVal _PAYMENTMETHOD As String,
                              ByVal _KLIRINGNO As String,
                              ByVal _JSONDetailDataTable As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                Query =
                "SELECT 'CRV' || TO_CHAR(SYSDATE, 'YYYY') || SUBSTR('00000000', 1, 8 - LENGTH((COALESCE(a.MAXSEQUENCE, 0) + 1))) || (COALESCE(a.MAXSEQUENCE, 0) + 1) AS GENERATENO " & vbCrLf &
                "FROM " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT CAST(MAX(SUBSTR(CRV_NUM, 8, 8)) As NUMBER) As MAXSEQUENCE " & vbCrLf &
                "    FROM TPROC_CRV " & vbCrLf &
                "    WHERE SUBSTR(CRV_NUM, 4, 4) = TO_CHAR(SYSDATE, 'YYYY') " & vbCrLf &
                ") a "
                Dim dtGENERATENO As DataTable = ConnectionDB.GetDataTable(Query)
                Dim GENERATENO As String = dtGENERATENO.Rows(0).Item(0) & Microsoft.VisualBasic.Mid(_GMNUMBER, 15, Microsoft.VisualBasic.Len(_GMNUMBER) - 1)

                Query = "Select COALESCE((Select MAX(COALESCE(ID, 0)) + 1 As ID FROM TPROC_CRV), 1) As ID FROM DUAL"
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

                    Query = "INSERT INTO TPROC_CRV " & vbCrLf &
                    "( " & vbCrLf &
                    "   ID, CRV_NUM, PO_ID, BANK_ACCOUNT_NUMBER, BANK_NAME " & vbCrLf &
                    "   , CREATED_TIME, CREATED_BY, SUB_TOTAL, DSCNT_AMT, VAT " & vbCrLf &
                    "   , WTH_TAX, GRAND_TOTAL, SUPPLIER_ID, BANK_BRANCH " & vbCrLf &
                    "   , REFTAXNO, PAYMENTMETHOD, KLIRINGNO " & vbCrLf &
                    ") " & vbCrLf &
                    "VALUES " & vbCrLf &
                    "( " & vbCrLf &
                    "   " & dtTable.Rows(0).Item("ID") & " " & vbCrLf &
                    "   , '" & GENERATENO & "' " & vbCrLf &
                    "   , '" & _PO_ID & "' " & vbCrLf &
                    "   , '" & _BANK_ACCOUNT_NUMBER & "' " & vbCrLf &
                    "   , '" & _BANK_NAME & "' " & vbCrLf &
                    "   , SYSDATE " & vbCrLf &
                    "   , '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , '" & Convert.ToDecimal(_SUB_TOTAL) & "' " & vbCrLf &
                    "   , '" & Convert.ToDecimal(_DSCNT_AMT) & "' " & vbCrLf &
                    "   , '" & Convert.ToDecimal(_VAT) & "' " & vbCrLf &
                    "   , '" & Convert.ToDecimal(_WTH_TAX) & "' " & vbCrLf &
                    "   , '" & Convert.ToDecimal(_GRAND_TOTAL) & "' " & vbCrLf &
                    "   , '" & _SUPPLIER_ID & "' " & vbCrLf &
                    "   , '" & _BANK_BRANCH & "' " & vbCrLf &
                    "   , '" & _REFTAXNO & "' " & vbCrLf &
                    "   , '" & _PAYMENTMETHOD & "' " & vbCrLf &
                    "   , '" & _KLIRINGNO & "' " & vbCrLf &
                    ") "
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_CRV_DETAILS WHERE CRV_HEADER_ID = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        Query = "INSERT INTO TPROC_CRV_DETAILS " & vbCrLf &
                                "( " & vbCrLf &
                                "   ID, CRV_HEADER_ID, ITEM_ID, ITEM_NAME, UNITMEASUREMENT, QUANTITY " & vbCrLf &
                                "   , PRICE, SUBTOTAL, FUND_T1, LOB1_T2, PLAN_T3, WA_T4, LOB2_T5 " & vbCrLf &
                                "   , CREATED_TIME, CREATED_BY, CURRENCY, ACCOUNT_CODE " & vbCrLf &
                                ") " & vbCrLf &
                                "VALUES " & vbCrLf &
                                "( " & vbCrLf &
                                "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_CRV_DETAILS), 1) AS ID FROM DUAL) " & vbCrLf &
                                "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(1) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(5) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(2) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(3)) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(9)) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(10) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(11) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(12) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(13) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(14) & "' " & vbCrLf &
                                "   , SYSDATE " & vbCrLf &
                                "   , '" & Session("USER_ID") & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(15)) & "' " & vbCrLf &
                                "   , '" & dtTableDetail.Rows(i).Item(7) & "' " & vbCrLf &
                                ") "
                        ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
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


        <CAuthorize(Role:="MNU57")>
        Function ActionEdit(ByVal _ID As Decimal,
                            ByVal _CRV_NUMBER As String,
                            ByVal _PO_ID As String,
                            ByVal _BANK_ACCOUNT_NUMBER As String,
                            ByVal _BANK_NAME As String,
                            ByVal _SUB_TOTAL As String,
                            ByVal _DSCNT_AMT As String,
                            ByVal _VAT As String,
                            ByVal _WTH_TAX As String,
                            ByVal _GRAND_TOTAL As String,
                            ByVal _SUPPLIER_ID As String,
                            ByVal _BANK_BRANCH As String,
                            ByVal _REFTAXNO As String,
                            ByVal _PAYMENTMETHOD As String,
                            ByVal _KLIRINGNO As String,
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

                    Query = "UPDATE TPROC_CRV " & vbCrLf &
                    "SET CRV_NUM = '" & _CRV_NUMBER & "' " & vbCrLf &
                    "   , PO_ID = '" & _PO_ID & "' " & vbCrLf &
                    "   , BANK_ACCOUNT_NUMBER = '" & _BANK_ACCOUNT_NUMBER & "' " & vbCrLf &
                    "   , BANK_NAME = '" & _BANK_NAME & "' " & vbCrLf &
                    "   , SUB_TOTAL = '" & Convert.ToDecimal(_SUB_TOTAL) & "' " & vbCrLf &
                    "   , DSCNT_AMT = '" & Convert.ToDecimal(_DSCNT_AMT) & "' " & vbCrLf &
                    "   , VAT = '" & Convert.ToDecimal(_VAT) & "' " & vbCrLf &
                    "   , WTH_TAX = '" & Convert.ToDecimal(_WTH_TAX) & "' " & vbCrLf &
                    "   , GRAND_TOTAL = '" & Convert.ToDecimal(_GRAND_TOTAL) & "' " & vbCrLf &
                    "   , SUPPLIER_ID = '" & _SUPPLIER_ID & "' " & vbCrLf &
                    "   , BANK_BRANCH = '" & _BANK_BRANCH & "' " & vbCrLf &
                    "   , REFTAXNO = '" & _REFTAXNO & "' " & vbCrLf &
                    "   , PAYMENTMETHOD = '" & _PAYMENTMETHOD & "' " & vbCrLf &
                    "   , KLIRINGNO = '" & _KLIRINGNO & "' " & vbCrLf &
                    "   , LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                    "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                    "WHERE ID = '" & _ID & "' "
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_CRV_DETAILS WHERE CRV_HEADER_ID = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        Query = "INSERT INTO TPROC_CRV_DETAILS " & vbCrLf &
                                "( " & vbCrLf &
                                "   ID, CRV_HEADER_ID, ITEM_ID, ITEM_NAME, UNITMEASUREMENT, QUANTITY " & vbCrLf &
                                "   , PRICE, SUBTOTAL, FUND_T1, LOB1_T2, PLAN_T3, WA_T4, LOB2_T5 " & vbCrLf &
                                "   , CREATED_TIME, CREATED_BY, CURRENCY, ACCOUNT_CODE " & vbCrLf &
                                ") " & vbCrLf &
                                "VALUES " & vbCrLf &
                                "( " & vbCrLf &
                                "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_CRV_DETAILS), 1) AS ID FROM DUAL) " & vbCrLf &
                                "   , '" & _ID & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(1) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(5) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(2) & "") & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(3)) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(9)) & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(10) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(11) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(12) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(13) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(14) & "") & "' " & vbCrLf &
                                "   , SYSDATE " & vbCrLf &
                                "   , '" & Session("USER_ID") & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(15)) & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(7) & "") & "' " & vbCrLf &
                                ") "
                        ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
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

        <CAuthorize(Role:="MNU57")>
        Function ActionSubmit(ByVal _ID As String,
                              ByVal _CRV_NUMBER As String,
                              ByVal _PO_ID As String,
                              ByVal _GMNUMBER As String,
                              ByVal _BANK_ACCOUNT_NUMBER As String,
                              ByVal _BANK_NAME As String,
                              ByVal _SUB_TOTAL As String,
                              ByVal _DSCNT_AMT As String,
                              ByVal _VAT As String,
                              ByVal _WTH_TAX As String,
                              ByVal _GRAND_TOTAL As String,
                              ByVal _SUPPLIER_ID As String,
                              ByVal _BANK_BRANCH As String,
                              ByVal _REFTAXNO As String,
                              ByVal _PAYMENTMETHOD As String,
                              ByVal _KLIRINGNO As String,
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
                        Query = "UPDATE TPROC_CRV " & vbCrLf &
                        "SET CRV_NUM = '" & _CRV_NUMBER & "' " & vbCrLf &
                        "   , PO_ID = '" & _PO_ID & "' " & vbCrLf &
                        "   , BANK_ACCOUNT_NUMBER = '" & _BANK_ACCOUNT_NUMBER & "' " & vbCrLf &
                        "   , BANK_NAME = '" & _BANK_NAME & "' " & vbCrLf &
                        "   , SUB_TOTAL = '" & Convert.ToDecimal(_SUB_TOTAL) & "' " & vbCrLf &
                        "   , DSCNT_AMT = '" & Convert.ToDecimal(_DSCNT_AMT) & "' " & vbCrLf &
                        "   , VAT = '" & Convert.ToDecimal(_VAT) & "' " & vbCrLf &
                        "   , WTH_TAX = '" & Convert.ToDecimal(_WTH_TAX) & "' " & vbCrLf &
                        "   , GRAND_TOTAL = '" & Convert.ToDecimal(_GRAND_TOTAL) & "' " & vbCrLf &
                        "   , SUPPLIER_ID = '" & _SUPPLIER_ID & "' " & vbCrLf &
                        "   , BANK_BRANCH = '" & _BANK_BRANCH & "' " & vbCrLf &
                        "   , REFTAXNO = '" & _REFTAXNO & "' " & vbCrLf &
                        "   , PAYMENTMETHOD = '" & _PAYMENTMETHOD & "' " & vbCrLf &
                        "   , KLIRINGNO = '" & _KLIRINGNO & "' " & vbCrLf &
                        "   , LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                        "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                        "   , ROW_STATUS = '" & RowStatus.Verify & "' " & vbCrLf &
                        "WHERE ID = '" & _ID & "' "
                    Else
                        Query =
                        "SELECT 'CRV' || TO_CHAR(SYSDATE, 'YYYY') || SUBSTR('00000000', 1, 8 - LENGTH((COALESCE(a.MAXSEQUENCE, 0) + 1))) || (COALESCE(a.MAXSEQUENCE, 0) + 1) AS GENERATENO " & vbCrLf &
                        "FROM " & vbCrLf &
                        "  ( " & vbCrLf &
                        "    SELECT CAST(MAX(SUBSTR(CRV_NUM, 8, 8)) As NUMBER) As MAXSEQUENCE " & vbCrLf &
                        "    FROM TPROC_CRV " & vbCrLf &
                        "    WHERE SUBSTR(CRV_NUM, 4, 4) = TO_CHAR(SYSDATE, 'YYYY') " & vbCrLf &
                        ") a "
                        Dim dtGENERATENO As DataTable = ConnectionDB.GetDataTable(Query)
                        Dim GENERATENO As String = dtGENERATENO.Rows(0).Item(0) & Microsoft.VisualBasic.Mid(_GMNUMBER, 15, Microsoft.VisualBasic.Len(_GMNUMBER) - 1)
                        _CRV_NUMBER = GENERATENO
                        Query = "Select COALESCE((Select MAX(COALESCE(ID, 0)) + 1 As ID FROM TPROC_CRV), 1) As ID FROM DUAL"
                        dtTable = ConnectionDB.GetDataTable(Query)

                        Query = "INSERT INTO TPROC_CRV " & vbCrLf &
                        "( " & vbCrLf &
                        "   ID, CRV_NUM, PO_ID, BANK_ACCOUNT_NUMBER, BANK_NAME " & vbCrLf &
                        "   , CREATED_TIME, CREATED_BY, SUB_TOTAL, DSCNT_AMT, VAT " & vbCrLf &
                        "   , WTH_TAX, GRAND_TOTAL, SUPPLIER_ID, BANK_BRANCH " & vbCrLf &
                        "   , REFTAXNO, PAYMENTMETHOD, KLIRINGNO, ROW_STATUS " & vbCrLf &
                        ") " & vbCrLf &
                        "VALUES " & vbCrLf &
                        "( " & vbCrLf &
                        "   " & dtTable.Rows(0).Item("ID") & " " & vbCrLf &
                        "   , '" & GENERATENO & "' " & vbCrLf &
                        "   , '" & _PO_ID & "' " & vbCrLf &
                        "   , '" & _BANK_ACCOUNT_NUMBER & "' " & vbCrLf &
                        "   , '" & _BANK_NAME & "' " & vbCrLf &
                        "   , SYSDATE " & vbCrLf &
                        "   , '" & Session("USER_ID") & "' " & vbCrLf &
                        "   , '" & Convert.ToDecimal(_SUB_TOTAL) & "' " & vbCrLf &
                        "   , '" & Convert.ToDecimal(_DSCNT_AMT) & "' " & vbCrLf &
                        "   , '" & Convert.ToDecimal(_VAT) & "' " & vbCrLf &
                        "   , '" & Convert.ToDecimal(_WTH_TAX) & "' " & vbCrLf &
                        "   , '" & Convert.ToDecimal(_GRAND_TOTAL) & "' " & vbCrLf &
                        "   , '" & _SUPPLIER_ID & "' " & vbCrLf &
                        "   , '" & _BANK_BRANCH & "' " & vbCrLf &
                        "   , '" & _REFTAXNO & "' " & vbCrLf &
                        "   , '" & _PAYMENTMETHOD & "' " & vbCrLf &
                        "   , '" & _KLIRINGNO & "' " & vbCrLf &
                        "   , '" & RowStatus.Verify & "' " & vbCrLf &
                        ") "
                    End If
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    Query = "DELETE FROM TPROC_CRV_DETAILS WHERE CRV_HEADER_ID = " & dtTable.Rows(0).Item("ID")
                    ConnectionDB.ExecuteQuery(Query, fo_ORACommand)

                    For i As Integer = 0 To dtTableDetail.Rows.Count - 1
                        Query = "INSERT INTO TPROC_CRV_DETAILS " & vbCrLf &
                                "( " & vbCrLf &
                                "   ID, CRV_HEADER_ID, ITEM_ID, ITEM_NAME, UNITMEASUREMENT, QUANTITY " & vbCrLf &
                                "   , PRICE, SUBTOTAL, FUND_T1, LOB1_T2, PLAN_T3, WA_T4, LOB2_T5 " & vbCrLf &
                                "   , CREATED_TIME, CREATED_BY, CURRENCY, ACCOUNT_CODE " & vbCrLf &
                                ") " & vbCrLf &
                                "VALUES " & vbCrLf &
                                "( " & vbCrLf &
                                "   (SELECT COALESCE((SELECT MAX(COALESCE(ID, 0)) + 1 AS ID FROM TPROC_CRV_DETAILS), 1) AS ID FROM DUAL) " & vbCrLf &
                                "   , '" & dtTable.Rows(0).Item("ID") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(1) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(5) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(2) & "") & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(3)) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(4)) & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(9)) & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(10) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(11) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(12) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(13) & "") & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(14) & "") & "' " & vbCrLf &
                                "   , SYSDATE " & vbCrLf &
                                "   , '" & Session("USER_ID") & "' " & vbCrLf &
                                "   , '" & Convert.ToDecimal(dtTableDetail.Rows(i).Item(15)) & "' " & vbCrLf &
                                "   , '" & Trim(dtTableDetail.Rows(i).Item(7) & "") & "' " & vbCrLf &
                                ") "
                        ConnectionDB.ExecuteQuery(Query, fo_ORACommand)
                    Next
                    fo_ORATransaction.Commit()

                    Query = "SELECT a.CRV_NUM AS ""CRV NUMBER"" " & vbCrLf &
                    "  , a.CREATED_TIME AS ""CRV DATE"" " & vbCrLf &
                    "  , c.SUPPLIER_NAME ""SUPPLIER NAME"" " & vbCrLf &
                    "  , b.GM_NUMBER ""GM NUMBER"" " & vbCrLf &
                    "  , a.BANK_NAME ""BANK NAME"" " & vbCrLf &
                    "  , a.BANK_BRANCH ""BANK BRANCH"" " & vbCrLf &
                    "  , a.BANK_ACCOUNT_NUMBER ""BANK ACCOUNT NUMBER"" " & vbCrLf &
                    "  , COALESCE(d.USER_NAME, a.LAST_MODIFIED_BY, a.CREATED_BY) AS ""SUBMIT BY"" " & vbCrLf &
                    "  , COALESCE(a.LAST_MODIFIED_TIME, a.CREATED_TIME) ""SUBMIT TIME"" " & vbCrLf &
                    "FROM TPROC_CRV a " & vbCrLf &
                    "LEFT JOIN TPROC_GM_HEADERS b ON b.ID = a.PO_ID " & vbCrLf &
                    "LEFT JOIN TPROC_SUPPLIER c ON c.ID = a.SUPPLIER_ID " & vbCrLf &
                    "LEFT JOIN TPROC_USER d ON d.USER_ID = COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) " & vbCrLf &
                    "WHERE a.CRV_NUM = '" & _CRV_NUMBER & "'"
                    Dim dtTableInfoEmail As DataTable = ConnectionDB.GetDataTable(Query)

                    Dim EmailTo As New eProcurementApps.Helpers.ListFieldNameAndValue
                    Query = "SELECT USER_ID " & vbCrLf &
                    "  , EMAIL " & vbCrLf &
                    "FROM TPROC_APPROVAL_ROLE a, TPROC_APPROVAL_ROLE_DETAIL b " & vbCrLf &
                    "WHERE a.ID = b.ID_APPROVAL_ROLE " & vbCrLf &
                    "And b.ROLE_NAME = 'CRV' " & vbCrLf &
                    "AND b.AS_IS = 'Verifier' "
                    Dim dtTableEmail As DataTable = ConnectionDB.GetDataTable(Query)
                    For i As Integer = 0 To dtTableEmail.Rows.Count - 1
                        EmailTo.AddItem(dtTableEmail.Rows(i).Item(0), dtTableEmail.Rows(i).Item(1))
                    Next
                    Dim strBodyDetail As String = ""
                    For i As Integer = 0 To dtTableInfoEmail.Columns.Count - 1
                        Dim ValueEmail As String = ""
                        If IsDate(dtTableInfoEmail.Rows(0).Item(i)) Then
                            ValueEmail = Format(CDate(dtTableInfoEmail.Rows(0).Item(i)), "dd-MM-yyyy")
                        Else
                            ValueEmail = Trim(dtTableInfoEmail.Rows(0).Item(i) & " ")
                        End If

                        strBodyDetail &= "<tr> 
                                             <td style='width:20%'>
                                                " & dtTableInfoEmail.Columns(i).Caption & " 
                                             </td>
                                             <td style='width:80%'>
                                                : " & ValueEmail & "                        
                                             </td>
                                         </tr> "
                    Next
                    Dim strBody As String = " <table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                                                <tr>
                                                    <td>
                                                        <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                                            <tr>
                                                                <td style='width:100%;' colspan='2'>
                                                                    <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:20%' colspan='2'>&nbsp</td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:20%' colspan='2'>
                                                                    <b><a href='" & Microsoft.VisualBasic.Strings.Replace(Request.Url.AbsoluteUri, "ActionSubmit", "FormInput?_Id=" & _ID & "&_Action=Verified") & "'>Click here to View Details</a></b>
                                                                </td>
                                                            </tr> " & strBodyDetail & "
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>"
                    eProcurementApps.Helpers.Emails.SendEmail(ConfigurationSettings.AppSettings("EmailSender"),
                                                              EmailTo,
                                                              Nothing,
                                                              "[eProcurement] – Number : " & dtTableInfoEmail.Rows(0).Item("CRV NUMBER") & " - CRV need to be Verify",
                                                              strBody, "")
                    rs.SetSuccessStatus("Data has been submited")
                Catch ex As Exception
                    fo_ORATransaction.Rollback()
                    Throw
                End Try
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return Json(rs.MessageText)
        End Function

        <CAuthorize(Role:="MNU57")>
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

                    Query = "UPDATE TPROC_CRV " & vbCrLf &
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

        <CAuthorize(Role:="MNU57")>
        Function ActionApprove(ByVal _ID As String) As ActionResult
            Dim MsgJSON As String = "Data has been approved"
            Try
                Query = "UPDATE TPROC_CRV " & vbCrLf &
                "SET APPROVE_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , APPROVE_DATE = SYSDATE " & vbCrLf &
                "   ,  LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                "   , ROW_STATUS = '" & RowStatus.Received & "' " & vbCrLf &
                "WHERE ID = " & _ID
                ConnectionDB.ExecuteQuery(Query)

                Query = "SELECT a.CRV_NUM AS ""CRV NUMBER"" " & vbCrLf &
                "  , a.CREATED_TIME AS ""CRV DATE"" " & vbCrLf &
                "  , c.SUPPLIER_NAME ""SUPPLIER NAME"" " & vbCrLf &
                "  , b.GM_NUMBER ""GM NUMBER"" " & vbCrLf &
                "  , a.BANK_NAME ""BANK NAME"" " & vbCrLf &
                "  , a.BANK_BRANCH ""BANK BRANCH"" " & vbCrLf &
                "  , a.BANK_ACCOUNT_NUMBER ""BANK ACCOUNT NUMBER"" " & vbCrLf &
                "  , COALESCE(d.USER_NAME, a.APPROVE_BY) ""APPROVE BY"" " & vbCrLf &
                "  , a.APPROVE_DATE ""APPROVE TIME"" " & vbCrLf &
                "FROM TPROC_CRV a " & vbCrLf &
                "LEFT JOIN TPROC_GM_HEADERS b ON b.ID = a.PO_ID " & vbCrLf &
                "LEFT JOIN TPROC_SUPPLIER c ON c.ID = a.SUPPLIER_ID " & vbCrLf &
                "LEFT JOIN TPROC_USER d ON d.USER_ID = a.APPROVE_BY " & vbCrLf &
                "WHERE a.ID = " & _ID
                Dim dtTableInfoEmail As DataTable = ConnectionDB.GetDataTable(Query)

                Dim EmailTo As New eProcurementApps.Helpers.ListFieldNameAndValue
                Query = "Select USER_ID " & vbCrLf &
                "  , EMAIL " & vbCrLf &
                "FROM TPROC_APPROVAL_ROLE a, TPROC_APPROVAL_ROLE_DETAIL b " & vbCrLf &
                "WHERE a.ID = b.ID_APPROVAL_ROLE " & vbCrLf &
                "And b.ROLE_NAME = 'CRV' " & vbCrLf &
                "AND b.AS_IS = 'Received' "
                Dim dtTableEmail As DataTable = ConnectionDB.GetDataTable(Query)
                For i As Integer = 0 To dtTableEmail.Rows.Count - 1
                    EmailTo.AddItem(dtTableEmail.Rows(i).Item(0), dtTableEmail.Rows(i).Item(1))
                Next
                Dim strBodyDetail As String = ""
                For i As Integer = 0 To dtTableInfoEmail.Columns.Count - 1
                    Dim ValueEmail As String = ""
                    If IsDate(dtTableInfoEmail.Rows(0).Item(i)) Then
                        ValueEmail = Format(CDate(dtTableInfoEmail.Rows(0).Item(i)), "dd-MM-yyyy")
                    Else
                        ValueEmail = Trim(dtTableInfoEmail.Rows(0).Item(i) & " ")
                    End If

                    strBodyDetail &= "<tr> 
                                         <td style='width:20%'>
                                            " & dtTableInfoEmail.Columns(i).Caption & " 
                                         </td>
                                         <td style='width:80%'>
                                            : " & ValueEmail & "                        
                                         </td>
                                     </tr> "
                Next
                Dim strBody As String = " <table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                                            <tr>
                                                <td>
                                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                                        <tr>
                                                            <td style='width:100%;' colspan='2'>
                                                                <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>
                                                                <b><a href='" & Microsoft.VisualBasic.Strings.Replace(Request.Url.AbsoluteUri, "ActionApprove", "FormInput?_Id=" & _ID & "&_Action=Received") & "'>Click here to View Details</a></b>
                                                            </td>
                                                        </tr> " & strBodyDetail & "
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>"
                eProcurementApps.Helpers.Emails.SendEmail(ConfigurationSettings.AppSettings("EmailSender"),
                                                          EmailTo,
                                                          Nothing,
                                                          "[eProcurement] – Number : " & dtTableInfoEmail.Rows(0).Item("CRV NUMBER") & " - CRV need to be received",
                                                          strBody, "")
            Catch ex As Exception
                MsgJSON = "Failed With Error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionReceived(ByVal _ID As String) As ActionResult
            Dim MsgJSON As String = "Data has been received"
            Try
                Query = "UPDATE TPROC_CRV " & vbCrLf &
                "SET RECEIVED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , RECEIVED_DATE = SYSDATE " & vbCrLf &
                 "   ,  LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                "   , ROW_STATUS = '" & RowStatus.Paid & "' " & vbCrLf &
                "WHERE ID = " & _ID
                ConnectionDB.ExecuteQuery(Query)

                Query = "SELECT a.CRV_NUM AS ""CRV NUMBER"" " & vbCrLf &
                "  , a.CREATED_TIME AS ""CRV DATE"" " & vbCrLf &
                "  , c.SUPPLIER_NAME ""SUPPLIER NAME"" " & vbCrLf &
                "  , b.GM_NUMBER ""GM NUMBER"" " & vbCrLf &
                "  , a.BANK_NAME ""BANK NAME"" " & vbCrLf &
                "  , a.BANK_BRANCH ""BANK BRANCH"" " & vbCrLf &
                "  , a.BANK_ACCOUNT_NUMBER ""BANK ACCOUNT NUMBER"" " & vbCrLf &
                "  , COALESCE(d.USER_NAME, a.APPROVE_BY) ""APPROVE BY"" " & vbCrLf &
                "  , a.APPROVE_DATE ""APPROVE TIME"" " & vbCrLf &
                "FROM TPROC_CRV a " & vbCrLf &
                "LEFT JOIN TPROC_GM_HEADERS b ON b.ID = a.PO_ID " & vbCrLf &
                "LEFT JOIN TPROC_SUPPLIER c ON c.ID = a.SUPPLIER_ID " & vbCrLf &
                "LEFT JOIN TPROC_USER d ON d.USER_ID = a.APPROVE_BY " & vbCrLf &
                "WHERE a.ID = " & _ID
                Dim dtTableInfoEmail As DataTable = ConnectionDB.GetDataTable(Query)

                Dim EmailTo As New eProcurementApps.Helpers.ListFieldNameAndValue
                Query = "Select USER_ID " & vbCrLf &
                "  , EMAIL " & vbCrLf &
                "FROM TPROC_APPROVAL_ROLE a, TPROC_APPROVAL_ROLE_DETAIL b " & vbCrLf &
                "WHERE a.ID = b.ID_APPROVAL_ROLE " & vbCrLf &
                "And b.ROLE_NAME = 'CRV' " & vbCrLf &
                "AND b.AS_IS = 'Paid' "
                Dim dtTableEmail As DataTable = ConnectionDB.GetDataTable(Query)
                For i As Integer = 0 To dtTableEmail.Rows.Count - 1
                    EmailTo.AddItem(dtTableEmail.Rows(i).Item(0), dtTableEmail.Rows(i).Item(1))
                Next
                Dim strBodyDetail As String = ""
                For i As Integer = 0 To dtTableInfoEmail.Columns.Count - 1
                    Dim ValueEmail As String = ""
                    If IsDate(dtTableInfoEmail.Rows(0).Item(i)) Then
                        ValueEmail = Format(CDate(dtTableInfoEmail.Rows(0).Item(i)), "dd-MM-yyyy")
                    Else
                        ValueEmail = Trim(dtTableInfoEmail.Rows(0).Item(i) & " ")
                    End If

                    strBodyDetail &= "<tr> 
                                         <td style='width:20%'>
                                            " & dtTableInfoEmail.Columns(i).Caption & " 
                                         </td>
                                         <td style='width:80%'>
                                            : " & ValueEmail & "                        
                                         </td>
                                     </tr> "
                Next
                Dim strBody As String = " <table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                                            <tr>
                                                <td>
                                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                                        <tr>
                                                            <td style='width:100%;' colspan='2'>
                                                                <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>
                                                                <b><a href='" & Microsoft.VisualBasic.Strings.Replace(Request.Url.AbsoluteUri, "ActionReceived", "FormInput?_Id=" & _ID & "&_Action=Paid") & "'>Click here to View Details</a></b>
                                                            </td>
                                                        </tr> " & strBodyDetail & "
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>"
                eProcurementApps.Helpers.Emails.SendEmail(ConfigurationSettings.AppSettings("EmailSender"),
                                                          EmailTo,
                                                          Nothing,
                                                          "[eProcurement] – Number : " & dtTableInfoEmail.Rows(0).Item("CRV NUMBER") & " - CRV need to be paid",
                                                          strBody, "")
            Catch ex As Exception
                MsgJSON = "Failed With Error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionPaid(ByVal _ID As String,
                            ByVal _FileName As String,
                            ByVal _FileDoc As String) As ActionResult
            Dim MsgJSON As String = "Data has been paid"
            Try
                Dim pathServer As String = ""
                If _FileName <> "" Then
                    Query = "SELECT PARAMETER_VALUE " & vbCrLf &
                            "FROM TPROC_PARAMETERS " & vbCrLf &
                            "WHERE PARAMETER_CODE = 'DIR_UPLOAD_DOC_CRV' "
                    Dim dtParameter As DataTable = ConnectionDB.GetDataTable(Query)
                    pathServer = dtParameter.Rows(0).Item(0) & _FileName

                    If System.IO.File.Exists(pathServer) Then
                        System.IO.File.Delete(pathServer)
                    End If

                    Dim FileByte As Byte() = System.Convert.FromBase64String(_FileDoc)
                    Dim streamFile As System.IO.FileStream
                    streamFile = New System.IO.FileStream(pathServer, System.IO.FileMode.CreateNew)
                    streamFile.Write(FileByte, 0, FileByte.Length)
                    streamFile.Flush()
                    streamFile.Dispose()
                    streamFile.Close()
                End If

                Query = "UPDATE TPROC_CRV " & vbCrLf &
                "SET COMPLETED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , COMPLETED_DATE = SYSDATE " & vbCrLf &
                 "   ,  LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                "   , FILEPATH = '" & pathServer & "' " & vbCrLf &
                "   , ROW_STATUS = '" & RowStatus.Complete & "' " & vbCrLf &
                "WHERE ID = " & _ID
                ConnectionDB.ExecuteQuery(Query)
            Catch ex As Exception
                MsgJSON = "Failed With Error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionExportPdf(ByVal _ID As String) As FileResult
            Dim MsgJSON As String = "Export file success"
            Dim PathDirectory As String = ""
            Dim rs As New ResultStatus
            Dim resultView As FileResult = Nothing

            Try
                Dim renderedBytes As Byte() = Nothing
                Dim lclReport As New LocalReport
                Dim source As ReportDataSource
                lclReport.ReportPath = "RDLC\TRANS_CRV_RPT.rdlc"

                Query =
                "SELECT a.ID " & vbCrLf &
                "  , COALESCE(a.CRV_NUM, ' ') AS CRV_NUMBER " & vbCrLf &
                "  , COALESCE(b.GM_NUMBER, ' ') AS GM_NUMBER " & vbCrLf &
                "  , COALESCE(k.PO_NUMBER, ' ') AS PO_NUMBER " & vbCrLf &
                "  , COALESCE(a.REFTAXNO, ' ') AS FAKTUR_PAJAK " & vbCrLf &
                "  , a.PAYMENTMETHOD AS PAYMENT_BY " & vbCrLf &
                "  , c.SUPPLIER_NAME " & vbCrLf &
                "  , c.SUPPLIER_ADDRESS " & vbCrLf &
                "  , COALESCE(a.BANK_NAME, ' ') AS BANK_NAME " & vbCrLf &
                "  , COALESCE(a.BANK_BRANCH, ' ') AS BANK_BRANCH " & vbCrLf &
                "  , COALESCE(a.BANK_ACCOUNT_NUMBER, ' ') AS BANK_ACCOUNT_NUMBER " & vbCrLf &
                "  , d.NO " & vbCrLf &
                "  , d.DESCRIPTION " & vbCrLf &
                "  , d.CURR " & vbCrLf &
                "  , d.ACCOUNT_CODE AS ACCOUNT_DEBIT " & vbCrLf &
                "  , d.OTH_AMOUNT AS OTHER_AMOUNT " & vbCrLf &
                "  , d.OTH_RUPIAH AS RUPIAH_AMOUNT " & vbCrLf &
                "  , d.FUND_T1 AS T1_FUND " & vbCrLf &
                "  , d.LOB1_T2 AS T2_LOB1 " & vbCrLf &
                "  , d.PLAN_T3 AS T3_PLAN " & vbCrLf &
                "  , d.WA_T4 AS T4_WA " & vbCrLf &
                "  , d.LOB2_T5 AS T5_LOB2 " & vbCrLf &
                "  , a.SUB_TOTAL AS SUBTOTAL " & vbCrLf &
                "  , a.DSCNT_AMT AS DISCOUNT " & vbCrLf &
                "  , a.VAT " & vbCrLf &
                "  , a.WTH_TAX AS TAX " & vbCrLf &
                "  , a.GRAND_TOTAL AS GRANDTOTAL " & vbCrLf &
                "  , l.USER_NAME AS REQUESTEDBY " & vbCrLf &
                "  , m.USER_NAME AS APPROVEBY " & vbCrLf &
                "  , e.SIGNATURE_IMAGE AS SIGREQUESTEDBY " & vbCrLf &
                "  , CASE WHEN a.VAT > 0 THEN g.PARAMETER_VALUE END AS ACCTCODEVAT " & vbCrLf &
                "  , CASE WHEN a.WTH_TAX > 0 THEN h.PARAMETER_VALUE END AS ACCTCODEPPH " & vbCrLf &
                "  , CASE WHEN a.WTH_TAX > 0 THEN i.PARAMETER_VALUE END AS WAPPH " & vbCrLf &
                "  , CASE WHEN a.VAT > 0 THEN j.PARAMETER_VALUE END AS WAVAT " & vbCrLf &
                "FROM TPROC_CRV a " & vbCrLf &
                "LEFT JOIN TPROC_GM_HEADERS b ON b.ID = a.PO_ID " & vbCrLf &
                "LEFT JOIN TPROC_SUPPLIER c ON c.ID = a.SUPPLIER_ID " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "(" & vbCrLf &
                "  SELECT a.CRV_HEADER_ID " & vbCrLf &
                "    , SUBSTR('000', 1, 3 - LENGTH(ROW_NUMBER() OVER (ORDER BY a.CRV_HEADER_ID ASC))) || ROW_NUMBER() OVER (ORDER BY a.CRV_HEADER_ID ASC) AS NO " & vbCrLf &
                "    , a.ITEM_NAME AS DESCRIPTION " & vbCrLf &
                "    , b.CURRENCY_NAME AS CURR " & vbCrLf &
                "    , a.ACCOUNT_CODE " & vbCrLf &
                "    , a.SUBTOTAL AS OTH_AMOUNT " & vbCrLf &
                "    , a.SUBTOTAL AS OTH_RUPIAH " & vbCrLf &
                "    , a.FUND_T1 " & vbCrLf &
                "    , a.LOB1_T2 " & vbCrLf &
                "    , a.PLAN_T3 " & vbCrLf &
                "    , a.WA_T4 " & vbCrLf &
                "    , a.LOB2_T5 " & vbCrLf &
                "  FROM TPROC_CRV_DETAILS a " & vbCrLf &
                "  LEFT JOIN TPROC_CURRENCY b ON b.ID = a.CURRENCY " & vbCrLf &
                "  WHERE a.CRV_HEADER_ID = '" & _ID & "' " & vbCrLf &
                ") d ON d.CRV_HEADER_ID = a.ID " & vbCrLf &
                "LEFT JOIN TPROC_APPROVAL_ROLE e ON e.USER_ID = COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) AND e.ROW_STATUS = '0' " & vbCrLf &
                "LEFT JOIN TPROC_APPROVAL_ROLE f ON f.USER_ID = a.COMPLETED_BY AND f.ROW_STATUS = '0' " & vbCrLf &
                "CROSS JOIN (SELECT PARAMETER_VALUE FROM TPROC_PARAMETERS WHERE PARAMETER_CODE = 'ACCT_VAT') g " & vbCrLf &
                "CROSS JOIN (SELECT PARAMETER_VALUE FROM TPROC_PARAMETERS WHERE PARAMETER_CODE = 'ACCT_PPH') h " & vbCrLf &
                "CROSS JOIN (SELECT PARAMETER_VALUE FROM TPROC_PARAMETERS WHERE PARAMETER_CODE = 'WA_PPH') i " & vbCrLf &
                "CROSS JOIN (SELECT PARAMETER_VALUE FROM TPROC_PARAMETERS WHERE PARAMETER_CODE = 'WA_VAT') j " & vbCrLf &
                "LEFT JOIN TPROC_PO_HEADERS k ON k.ID = b.PO_ID " & vbCrLf &
                "LEFT JOIN TPROC_USER l ON l.USER_ID = COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) " & vbCrLf &
                "LEFT JOIN TPROC_USER m ON m.USER_ID = a.COMPLETED_BY " & vbCrLf &
                "WHERE a.ID = '" & _ID & "' "
                Dim dtTablePrint As DataTable = ConnectionDB.GetDataTable(Query)
                source = New ReportDataSource("DS_CRV_RPT", dtTablePrint)

                lclReport.DataSources.Clear()
                lclReport.DataSources.Add(source)
                lclReport.Refresh()

                'Query = "SELECT PARAMETER_VALUE " & vbCrLf &
                '        "FROM TPROC_PARAMETERS " & vbCrLf &
                '        "WHERE PARAMETER_CODE = 'DIR_TRANS_CRV' "
                'Dim dtParameter As DataTable = ConnectionDB.GetDataTable(Query)

                Query = "SELECT CRV_NUM FROM TPROC_CRV WHERE ID = '" & _ID & "' "
                Dim dtTableNameFile As DataTable = ConnectionDB.GetDataTable(Query)

                Dim streamFile As System.IO.FileStream
                Dim warnings As Warning() = Nothing
                Dim streamids As String() = Nothing
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim extension As String = Nothing
                Dim DeviceInfo As String = " <DeviceInfo> " +
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

                'If System.IO.File.Exists(dtParameter.Rows(0).Item(0) & dtTableNameFile.Rows(0).Item("CRV_NUM") & ".pdf") Then
                '    System.IO.File.Delete(dtParameter.Rows(0).Item(0) & dtTableNameFile.Rows(0).Item("CRV_NUM") & ".pdf")
                'End If

                Dim path = CommonFunction.GetPathParam("DIR_TRANS_CRV")
                rs = CommonFunction.CheckFolderExisting(path + "\CRV\Pdf\")

                If rs.IsSuccess Then
                    Dim path_file As String = path & "\CRV\Pdf\" & dtTableNameFile.Rows(0).Item("CRV_NUM") & ".pdf"
                    streamFile = New System.IO.FileStream(path_file, System.IO.FileMode.Create)
                    streamFile.Write(renderedBytes, 0, renderedBytes.Length)
                    streamFile.Flush()
                    streamFile.Dispose()
                    streamFile.Close()

                    resultView = ActionViewFileObject(path_file, "application/pdf")
                End If


                'streamFile = New System.IO.FileStream(dtParameter.Rows(0).Item(0) & dtTableNameFile.Rows(0).Item("CRV_NUM") & ".pdf", System.IO.FileMode.CreateNew)
                'streamFile.Write(renderedBytes, 0, renderedBytes.Length)
                'streamFile.Flush()
                'streamFile.Dispose()
                'streamFile.Close()

                'PathDirectory = dtParameter.Rows(0).Item(0) & dtTableNameFile.Rows(0).Item("CRV_NUM") & ".pdf"
            Catch ex As Exception
                MsgJSON = "Failed with error : " & ex.Message
            End Try

            Return resultView
        End Function

        Function ActionViewFileObject(ByVal path_file As String, ByVal type_file As String) As FileResult
            Dim path_exec As String = path_file
            Return File(path_exec, type_file)
        End Function

        Function ViewPdf(ByVal po_number As String) As FileResult
            Dim path_file = CommonFunction.GetPathParam("DIR_TRANS_CRV") + "/CRV/Pdf/" + po_number + ".pdf"
            Dim result = ActionViewFileObject(path_file, "application/pdf")

            Return result
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionRejected(ByVal _ID As String,
                                ByVal _RejectedNote As String) As ActionResult
            Dim MsgJSON As String = "Data has been rejected"
            Try
                Query = "UPDATE TPROC_CRV " & vbCrLf &
                "SET REJECTED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , REJECTED_TIME = SYSDATE " & vbCrLf &
                "   , REJECTNOTE = '" & _RejectedNote & "' " & vbCrLf &
                "   ,  LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                "   , ROW_STATUS = '" & RowStatus.Rejected & "' " & vbCrLf &
                "WHERE ID = " & _ID
                ConnectionDB.ExecuteQuery(Query)

                Dim EmailTo As New eProcurementApps.Helpers.ListFieldNameAndValue
                Query = "Select a.CRV_NUM " & vbCrLf &
                "  , COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) AS CREATED_BY " & vbCrLf &
                "  , c.USER_NAME " & vbCrLf &
                "  , b.USER_MAIL " & vbCrLf &
                "FROM TPROC_CRV a, TPROC_USER b, TPROC_USER c " & vbCrLf &
                "WHERE COALESCE(a.LAST_MODIFIED_BY, a.CREATED_BY) = b.USER_ID " & vbCrLf &
                "AND a.REJECTED_BY = c.USER_ID " & vbCrLf &
                "And a.ID = " & _ID
                Dim dtTableEmail As DataTable = ConnectionDB.GetDataTable(Query)
                For i As Integer = 0 To dtTableEmail.Rows.Count - 1
                    EmailTo.AddItem(dtTableEmail.Rows(i).Item(1), dtTableEmail.Rows(i).Item(3))
                Next

                Dim strBodyDetail As String = "FYI your CRV Number " & dtTableEmail.Rows(0).Item(0) & " already rejected by " & dtTableEmail.Rows(0).Item(2)
                Dim strBody As String = " <table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                                            <tr>
                                                <td>
                                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                                        <tr>
                                                            <td style='width:100%;' colspan='2'>
                                                                <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>
                                                                <b><a href='" & Microsoft.VisualBasic.Strings.Replace(Request.Url.AbsoluteUri, "ActionRejected", "FormInput?_Id=" & _ID & "&_Action=Rejected") & "'>Click here to View Details</a></b>
                                                            </td>
                                                        </tr> " & strBodyDetail & "
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>"
                eProcurementApps.Helpers.Emails.SendEmail(ConfigurationSettings.AppSettings("EmailSender"),
                                                          EmailTo,
                                                          Nothing,
                                                          "[eProcurement] – Number : " & dtTableEmail.Rows(0).Item(0) & " - CRV has been rejected ",
                                                          strBody, "")
            Catch ex As Exception
                MsgJSON = "Failed with error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionVerified(ByVal _ID As String) As ActionResult
            Dim MsgJSON As String = "Data has been verified"
            Try
                Query = "UPDATE TPROC_CRV " & vbCrLf &
                "SET VERIFY_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , VERIFY_TIME = SYSDATE " & vbCrLf &
                "   ,  LAST_MODIFIED_BY = '" & Session("USER_ID") & "' " & vbCrLf &
                "   , LAST_MODIFIED_TIME = SYSDATE " & vbCrLf &
                "   , ROW_STATUS = '" & RowStatus.Approve & "' " & vbCrLf &
                "WHERE ID = " & _ID
                ConnectionDB.ExecuteQuery(Query)

                Query = "SELECT a.CRV_NUM AS ""CRV NUMBER"" " & vbCrLf &
                "  , a.CREATED_TIME AS ""CRV DATE"" " & vbCrLf &
                "  , c.SUPPLIER_NAME ""SUPPLIER NAME"" " & vbCrLf &
                "  , b.GM_NUMBER ""GM NUMBER"" " & vbCrLf &
                "  , a.BANK_NAME ""BANK NAME"" " & vbCrLf &
                "  , a.BANK_BRANCH ""BANK BRANCH"" " & vbCrLf &
                "  , a.BANK_ACCOUNT_NUMBER ""BANK ACCOUNT NUMBER"" " & vbCrLf &
                "  , COALESCE(d.USER_NAME, a.VERIFY_BY) ""VERIFY BY"" " & vbCrLf &
                "  , a.VERIFY_TIME ""VERIFY TIME"" " & vbCrLf &
                "FROM TPROC_CRV a " & vbCrLf &
                "LEFT JOIN TPROC_GM_HEADERS b ON b.ID = a.PO_ID " & vbCrLf &
                "LEFT JOIN TPROC_SUPPLIER c ON c.ID = a.SUPPLIER_ID " & vbCrLf &
                "LEFT JOIN TPROC_USER d ON d.USER_ID = a.VERIFY_BY " & vbCrLf &
                "WHERE a.ID = " & _ID
                Dim dtTableInfoEmail As DataTable = ConnectionDB.GetDataTable(Query)

                Dim EmailTo As New eProcurementApps.Helpers.ListFieldNameAndValue
                Query = "Select USER_ID " & vbCrLf &
                "  , EMAIL " & vbCrLf &
                "FROM TPROC_APPROVAL_ROLE a, TPROC_APPROVAL_ROLE_DETAIL b " & vbCrLf &
                "WHERE a.ID = b.ID_APPROVAL_ROLE " & vbCrLf &
                "And b.ROLE_NAME = 'CRV' " & vbCrLf &
                "AND b.AS_IS = 'Approver' "
                Dim dtTableEmail As DataTable = ConnectionDB.GetDataTable(Query)
                For i As Integer = 0 To dtTableEmail.Rows.Count - 1
                    EmailTo.AddItem(dtTableEmail.Rows(i).Item(0), dtTableEmail.Rows(i).Item(1))
                Next
                Dim strBodyDetail As String = ""
                For i As Integer = 0 To dtTableInfoEmail.Columns.Count - 1
                    Dim ValueEmail As String = ""
                    If IsDate(dtTableInfoEmail.Rows(0).Item(i)) Then
                        ValueEmail = Format(CDate(dtTableInfoEmail.Rows(0).Item(i)), "dd-MM-yyyy")
                    Else
                        ValueEmail = Trim(dtTableInfoEmail.Rows(0).Item(i) & " ")
                    End If

                    strBodyDetail &= "<tr> 
                                         <td style='width:20%'>
                                            " & dtTableInfoEmail.Columns(i).Caption & " 
                                         </td>
                                         <td style='width:80%'>
                                            : " & ValueEmail & "                        
                                         </td>
                                     </tr> "
                Next
                Dim strBody As String = " <table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                                            <tr>
                                                <td>
                                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                                        <tr>
                                                            <td style='width:100%;' colspan='2'>
                                                                <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%' colspan='2'>
                                                                <b><a href='" & Microsoft.VisualBasic.Strings.Replace(Request.Url.AbsoluteUri, "ActionVerified", "FormInput?_Id=" & _ID & "&_Action=Approve") & "'>Click here to View Details</a></b>
                                                            </td>
                                                        </tr> " & strBodyDetail & "
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>"
                eProcurementApps.Helpers.Emails.SendEmail(ConfigurationSettings.AppSettings("EmailSender"),
                                                          EmailTo,
                                                          Nothing,
                                                          "[eProcurement] – Number : " & dtTableInfoEmail.Rows(0).Item("CRV NUMBER") & " - CRV need to be Approve",
                                                          strBody, "")
            Catch ex As Exception
                MsgJSON = "Failed with error : " & ex.Message
            End Try

            Return Json(MsgJSON)
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ReportCRV() As ActionResult
            Return View("FormReport")
        End Function

        <CAuthorize(Role:="MNU57")>
        Function ActionReport(ByVal _PeriodeFrom As String,
                              ByVal _PeriodeTo As String) As ActionResult
            Dim rs As New ResultStatus
            Try
                'Query = "SELECT PARAMETER_VALUE " & vbCrLf &
                '        "FROM TPROC_PARAMETERS " & vbCrLf &
                '        "WHERE PARAMETER_CODE = 'DIR_REPORT_CRV' "
                'Dim dtTablePrint As DataTable = ConnectionDB.GetDataTable(Query)

                Query =
                "SELECT a.KLIRINGNO " & vbCrLf &
                "  , a.BANK_ACCOUNT_NUMBER " & vbCrLf &
                "  , b.SUPPLIER_NAME " & vbCrLf &
                "  , SUBSTR('000000000000000000', 1, 18 - LENGTH(CAST(a.GRAND_TOTAL AS VARCHAR(18)))) || CAST(a.GRAND_TOTAL AS VARCHAR(18)) AS GRANDTOTAL " & vbCrLf &
                "  , 'C' AS DEBITCREDIT " & vbCrLf &
                "  , 'QQWQ' AS REMARK " & vbCrLf &
                "  , TO_CHAR(a.CREATED_TIME, 'DD/MM/YYYY') AS CRV_DATE " & vbCrLf &
                "  , b.VENDOR_CODE " & vbCrLf &
                "  , c.CURRENCY_NAME " & vbCrLf &
                "  , SUBSTR('000000000000000000', 1, 18 - LENGTH(CAST(a.GRAND_TOTAL AS VARCHAR(18)))) || CAST(a.GRAND_TOTAL AS VARCHAR(18)) || 'C' AS GRANDTOTALDEBITCREDIT " & vbCrLf &
                "FROM TPROC_CRV a " & vbCrLf &
                "LEFT JOIN TPROC_SUPPLIER b On b.ID = a.SUPPLIER_ID " & vbCrLf &
                "LEFT JOIN " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT a.ID " & vbCrLf &
                "      , b.CURRENCY_NAME " & vbCrLf &
                "    FROM TPROC_PO_HEADERS a, TPROC_CURRENCY b " & vbCrLf &
                "    WHERE a.CURRENCY = b.ID " & vbCrLf &
                "  ) c ON c.ID = a.PO_ID " & vbCrLf &
                "WHERE TO_CHAR(a.COMPLETED_DATE, 'DDMMYYYY') BETWEEN '" & Replace(_PeriodeFrom, "-", "") & "' AND '" & Replace(_PeriodeTo, "-", "") & "' "
                Dim dtParameter As DataTable = ConnectionDB.GetDataTable(Query)
                Dim fs_GenerateContain As String = ""
                For i As Integer = 0 To dtParameter.Rows.Count - 1
                    fs_GenerateContain &= dtParameter.Rows(i).Item("KLIRINGNO").ToString & Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("KLIRINGNO").ToString), " ") &
                                          dtParameter.Rows(i).Item("BANK_ACCOUNT_NUMBER").ToString & Strings.StrDup(34 - Len(dtParameter.Rows(i).Item("BANK_ACCOUNT_NUMBER").ToString), " ") &
                                          dtParameter.Rows(i).Item("SUPPLIER_NAME").ToString & Strings.StrDup(35 - Len(dtParameter.Rows(i).Item("SUPPLIER_NAME").ToString), " ") &
                                          dtParameter.Rows(i).Item("GRANDTOTAL").ToString & Strings.StrDup(22 - Len(dtParameter.Rows(i).Item("GRANDTOTAL").ToString), " ") &
                                          dtParameter.Rows(i).Item("DEBITCREDIT").ToString & Strings.StrDup(5 - Len(dtParameter.Rows(i).Item("DEBITCREDIT").ToString), " ") &
                                          dtParameter.Rows(i).Item("REMARK").ToString & Strings.StrDup(25 - Len(dtParameter.Rows(i).Item("REMARK").ToString), " ") &
                                          dtParameter.Rows(i).Item("CRV_DATE").ToString & Strings.StrDup(14 - Len(dtParameter.Rows(i).Item("CRV_DATE").ToString), " ") &
                                          dtParameter.Rows(i).Item("VENDOR_CODE").ToString & Strings.StrDup(15 - Len(dtParameter.Rows(i).Item("VENDOR_CODE").ToString), " ") &
                                          dtParameter.Rows(i).Item("CURRENCY_NAME").ToString & Strings.StrDup(5 - Len(dtParameter.Rows(i).Item("CURRENCY_NAME").ToString), " ") &
                                          dtParameter.Rows(i).Item("GRANDTOTALDEBITCREDIT").ToString & Strings.StrDup(19 - Len(dtParameter.Rows(i).Item("GRANDTOTALDEBITCREDIT").ToString), " ") & vbCrLf
                Next

                'Dim WriteTextFile As New System.IO.StreamWriter(dtTablePrint.Rows(0).Item(0) & "REPORTCRV_" & Format(Now, "yyyyMMdd") & ".txt", True)
                'WriteTextFile.WriteLine(fs_GenerateContain)
                'WriteTextFile.Close()

                Dim path = CommonFunction.GetPathParam("DIR_REPORT_CRV")
                rs = CommonFunction.CheckFolderExisting(path + "\CRV\Txt\")

                If rs.IsSuccess Then
                    Dim path_file As String = path & "\CRV\Txt\" & "REPORTCRV_" & Replace(_PeriodeFrom, "-", "") & "_" & Replace(_PeriodeTo, "-", "") & ".txt"

                    Dim WriteTextFile As New System.IO.StreamWriter(path_file, True)
                    WriteTextFile.WriteLine(fs_GenerateContain)
                    WriteTextFile.Close()
                    rs.SetSuccessStatus("Export file success")

                    Process.Start(path_file)
                End If
            Catch ex As Exception
            End Try

            Return Json("Export file success")
        End Function
    End Class
End Namespace