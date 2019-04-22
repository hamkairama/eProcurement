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
    Public Class DAILY_OPERATIONController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "DAILY OPERATION STOCK"
        <CAuthorize(Role:="MNU43")>
        Function DailyOperationStock() As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)

            Return View(pr_detail)
        End Function

        <CAuthorize(Role:="MNU43")>
        Function DailyOperationStockByFTandDt(ByVal ft As Integer, ByVal dt As DateTime) As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")
            Dim goo_type_stock_id As Integer

            Using db As New eProcurementEntities
                goo_type_stock_id = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = "STOCK" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
            End Using

            pr_detail = (From _history In db.TPROC_PR_HISTORICAL
                         Join _detail In db.TPROC_PR_DETAIL On _history.PR_HEADER_ID Equals (_detail.PR_HEADER_ID)
                         Where _history.HISTORICAL_STATUS.ToUpper() = "COMPLETE" _
                             And _history.HISTORICAL_DT.Day = dt.Day And _history.HISTORICAL_DT.Month = dt.Month And _history.HISTORICAL_DT.Year = dt.Year _
                             And _history.TPROC_PR_HEADER.FORM_TYPE_ID = ft And _history.TPROC_PR_HEADER.GOOD_TYPE_ID = goo_type_stock_id And _history.TPROC_PR_HEADER.FOR_STORAGE = 0
                         Select _detail
                         Distinct
                         Order By _detail.TPROC_PR_HEADER.PR_NO Descending).ToList()

            ViewBag.ft = ft
            ViewBag.dt = dt.ToString("MM/dd/yyyy")
            ViewBag.ActPrd = CommonFunction.GetActPrd(dt)
            Return PartialView("_ListDOStock", pr_detail)
        End Function

        '<CAuthorize(Role:="MNU43")>
        'Function DailyOperationStockByFTandDtTest(ByVal ft As Integer, ByVal dt As DateTime) As List(Of TPROC_PR_DETAIL)
        '    Dim pr_detail As New List(Of TPROC_PR_DETAIL)
        '    Dim user_id_id As String = Session("USER_ID_ID")
        '    Dim user_id As String = Session("USER_ID")
        '    Dim goo_type_stock_id As Integer

        '    Using db As New eProcurementEntities
        '        goo_type_stock_id = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = "STOCK" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
        '    End Using

        '    pr_detail = (From _history In db.TPROC_PR_HISTORICAL
        '                 Join _detail In db.TPROC_PR_DETAIL On _history.PR_HEADER_ID Equals (_detail.PR_HEADER_ID)
        '                 Where _history.HISTORICAL_STATUS.ToUpper() = "COMPLETE" _
        '                     And _history.HISTORICAL_DT.Day = dt.Day And _history.HISTORICAL_DT.Month = dt.Month And _history.HISTORICAL_DT.Year = dt.Year _
        '                     And _history.TPROC_PR_HEADER.FORM_TYPE_ID = ft And _history.TPROC_PR_HEADER.GOOD_TYPE_ID = goo_type_stock_id
        '                 Select _detail
        '                 Distinct
        '                 Order By _detail.TPROC_PR_HEADER.PR_NO Descending).ToList()

        '    ViewBag.ft = ft
        '    ViewBag.dt = dt.ToString("MM/dd/yyyy")
        '    Return pr_detail
        'End Function
#End Region

#Region "DAILY OPERATION STOCK SUMMARY"
        <CAuthorize(Role:="MNU64")>
        Function DailyOperationStockSummary() As ActionResult
            Dim pr_detail As New List(Of DAILY_STOCK_SUMMARY)

            Return View(pr_detail)
        End Function

        <CAuthorize(Role:="MNU64")>
        Function DailyOperationStockSummaryByFTandDt(ByVal ft As Integer, ByVal dt As DateTime) As ActionResult
            Dim query_dt As New List(Of DAILY_STOCK_SUMMARY)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")
            Dim goo_type_stock_id As Integer

            Using db As New eProcurementEntities
                goo_type_stock_id = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = "STOCK" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
            End Using

            Dim query = (From _history In db.TPROC_PR_HISTORICAL
                         Join _detail In db.TPROC_PR_DETAIL On _history.PR_HEADER_ID Equals (_detail.PR_HEADER_ID)
                         Join _header In db.TPROC_PR_HEADER On _detail.PR_HEADER_ID Equals (_header.ID)
                         Join _ft In db.TPROC_FORM_TYPE On _header.FORM_TYPE_ID Equals (_ft.ID)
                         Where _history.HISTORICAL_STATUS.ToUpper() = "COMPLETE" _
                             And _history.HISTORICAL_DT.Day = dt.Day And _history.HISTORICAL_DT.Month = dt.Month And _history.HISTORICAL_DT.Year = dt.Year _
                             And _history.TPROC_PR_HEADER.FORM_TYPE_ID = ft And _history.TPROC_PR_HEADER.GOOD_TYPE_ID = goo_type_stock_id And _history.TPROC_PR_HEADER.FOR_STORAGE = 0
                         Group _detail By _ft.FORM_TYPE_NAME, _header.ACCOUNT_CODE, _detail.WA_NUMBER Into Group
                         Select FORM_TYPE_NAME, ACCOUNT_CODE, TOTAL = Group.Sum(Function(v) v.TOTAL_PRICE), WA_NUMBER
                         Distinct
                         Order By WA_NUMBER)

            Dim gt As Decimal = 0
            For Each item In query
                Dim query_ As New DAILY_STOCK_SUMMARY
                query_.TYPE = item.FORM_TYPE_NAME
                query_.ACCOUNT_CODE = item.ACCOUNT_CODE
                query_.AMOUNT = item.TOTAL
                query_.WA = item.WA_NUMBER

                query_dt.Add(query_)
                gt = gt + query_.AMOUNT
            Next

            ViewBag.ft = ft
            ViewBag.dt = dt.ToString("MM/dd/yyyy")
            ViewBag.GrandTotal = gt.ToString("###,###")
            Return PartialView("_ListDOStockSummary", query_dt)
        End Function

#End Region

#Region "DAILY OPERATION NON STOCK"
        <CAuthorize(Role:="MNU44")>
        Function DailyOperationnONStock() As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)
            Return View(pr_detail)
        End Function

        <CAuthorize(Role:="MNU44")>
        Function DailyOperationNonStockByFTandDt(ByVal ft As Integer, ByVal dt As DateTime) As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")
            Dim goo_type_stock_id As Integer

            Using db As New eProcurementEntities
                goo_type_stock_id = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = "STOCK" And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault().ID
            End Using

            pr_detail = (From _history In db.TPROC_PR_HISTORICAL
                         Join _detail In db.TPROC_PR_DETAIL On _history.PR_HEADER_ID Equals (_detail.PR_HEADER_ID)
                         Where _history.HISTORICAL_STATUS.ToUpper() = "PRHANDLED" _
                             And _history.HISTORICAL_DT.Day = dt.Day And _history.HISTORICAL_DT.Month = dt.Month And _history.HISTORICAL_DT.Year = dt.Year _
                             And _history.TPROC_PR_HEADER.FORM_TYPE_ID = ft And (_history.TPROC_PR_HEADER.GOOD_TYPE_ID <> goo_type_stock_id Or _history.TPROC_PR_HEADER.GOOD_TYPE_ID = 0)
                         Select _detail
                         Distinct
                         Order By _detail.TPROC_PR_HEADER.PR_NO Descending).ToList()

            ViewBag.ft = ft
            ViewBag.dt = dt
            Return PartialView("_ListDONonStock", pr_detail)
        End Function
#End Region


    End Class
End Namespace