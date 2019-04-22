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
    Public Class REPORTController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "REPORT MY LIST PR"

        Function ReportMyListPR() As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")

            pr_detail = MyReportListPR(user_id_id)
            Return View(pr_detail)
        End Function

        Function MyReportListPR(user_id_id As Decimal) As List(Of TPROC_PR_DETAIL)
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)

            Return pr_detail
        End Function

        Function MyReportListPRByDate(ByVal date_from As DateTime, ByVal date_to As DateTime) As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")

            pr_detail = db.TPROC_PR_DETAIL.Where(Function(y) y.TPROC_PR_HEADER.USER_ID_ID = user_id_id And (y.TPROC_PR_HEADER.PR_DATE >= date_from And y.TPROC_PR_HEADER.PR_DATE <= date_to And y.TPROC_PR_HEADER.PR_STATUS = ListEnum.PRStatus.SignOff) And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.TPROC_PR_HEADER.PR_NO).ToList()

            Return PartialView("_ListMyReport", pr_detail)
        End Function
#End Region

#Region "REPORT MY LIST PR"
        Function AllReportListPRByDateByAdmin(ByVal date_from As DateTime, ByVal date_to As DateTime) As ActionResult
            Dim pr_detail As New List(Of TPROC_PR_DETAIL)

            pr_detail = db.TPROC_PR_DETAIL.Where(Function(y) (y.TPROC_PR_HEADER.PR_DATE >= date_from And y.TPROC_PR_HEADER.PR_DATE <= date_to) And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.TPROC_PR_HEADER.PR_NO).ToList()

            Return PartialView("_ListMyReport", pr_detail)
        End Function
#End Region


#Region "REPORT TAT"
        <CAuthorize(Role:="MNU38")>
        Function ReportTAT(ByVal flag As Integer) As ActionResult
            Dim pr_header As New List(Of TPROC_PR_HEADER)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")

            If flag = ListEnum.FlagReport.TatComplete Then
                pr_header = ReportTATComplete()
            ElseIf flag = ListEnum.FlagReport.TatSignOff Then
                pr_header = ReportTATSignOff()
            ElseIf flag = ListEnum.FlagReport.TatUnComplete Then
                pr_header = ReportTATUnComplete()
            End If

            ViewBag.IndexList = flag

            Return View(pr_header)
        End Function

        <CAuthorize(Role:="MNU38")>
        Function ReportTATByDate(ByVal flag As Integer, ByVal date_from As DateTime, ByVal date_to As DateTime) As ActionResult
            Dim pr_header As New List(Of TPROC_PR_HEADER)
            Dim user_id_id As String = Session("USER_ID_ID")
            Dim user_id As String = Session("USER_ID")

            If flag = ListEnum.FlagReport.TatComplete Then
                pr_header = ReportTATCompleteByDate(date_from, date_to)
            ElseIf flag = ListEnum.FlagReport.TatSignOff Then
                pr_header = ReportTATSignOffByDate(date_from, date_to)
            ElseIf flag = ListEnum.FlagReport.TatUnComplete Then
                pr_header = ReportTATUnCompleteByDate(date_from, date_to)
            End If

            ViewBag.IndexList = flag

            Return PartialView("_ListReportTAT", pr_header)
        End Function

        <CAuthorize(Role:="MNU38")>
        Function ReportTATUnComplete() As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            'pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.PR_STATUS = ListEnum.PRStatus.PrHandled And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(z) z.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU38")>
        Function ReportTATUnCompleteByDate(ByVal date_from As DateTime, ByVal date_to As DateTime) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.PR_STATUS = ListEnum.PRStatus.PrHandled And (y.PR_DATE >= date_from And y.PR_DATE <= date_to) And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(z) z.PR_NO).ToList()

            Return pr_header
        End Function


        <CAuthorize(Role:="MNU38")>
        Function ReportTATComplete() As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            'pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.PR_STATUS = ListEnum.PRStatus.Complete And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(z) z.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU38")>
        Function ReportTATCompleteByDate(ByVal date_from As DateTime, ByVal date_to As DateTime) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.PR_STATUS = ListEnum.PRStatus.Complete And (y.PR_DATE >= date_from And y.PR_DATE <= date_to) And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(z) z.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU38")>
        Function ReportTATSignOff() As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            'pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.PR_STATUS = ListEnum.PRStatus.SignOff And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(z) z.PR_NO).ToList()

            Return pr_header
        End Function

        <CAuthorize(Role:="MNU38")>
        Function ReportTATSignOffByDate(ByVal date_from As DateTime, ByVal date_to As DateTime) As List(Of TPROC_PR_HEADER)
            Dim pr_header As New List(Of TPROC_PR_HEADER)

            pr_header = db.TPROC_PR_HEADER.Where(Function(y) y.PR_STATUS = ListEnum.PRStatus.SignOff And (y.PR_DATE >= date_from And y.PR_DATE <= date_to) And y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(z) z.PR_NO).ToList()

            Return pr_header
        End Function
#End Region

    End Class
End Namespace