Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Facade
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports System.Transactions

Namespace Controllers
    Public Class REQUESTController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

#Region "REQUEST LIST"
        Function IndexRequestList(ByVal status As Integer) As ActionResult
            Dim req As New List(Of TPROC_REQUEST)
            If status = ListEnum.Request.Submitted Then
                req = ListOutStandingRequest()
            ElseIf status = ListEnum.Request.NeedApprove Then
                req = ListRequestNeedApprove()
            ElseIf status = ListEnum.Request.NeedComplete Then
                req = ListRequestNeedComplete()
            ElseIf status = ListEnum.Request.Completed Then
                req = ListRequestCompleted()
            End If

            ViewBag.IndexList = status

            Return View(req)
        End Function
        'Function List() As ActionResult
        '    Dim req As New List(Of TPROC_REQUEST)
        '    req = db.TPROC_REQUEST.Where(Function(y) y.STATUS = ListEnum.Request.Submitted).OrderByDescending(Function(x) x.CREATED_TIME).ToList()

        '    Return PartialView("_List", User)
        'End Function

        <CAuthorize(Role:="MNU58")>
        Function ListOutStandingRequest() As List(Of TPROC_REQUEST)
            Dim req As New List(Of TPROC_REQUEST)
            req = db.TPROC_REQUEST.Where(Function(y) y.STATUS <> ListEnum.Request.Completed).OrderByDescending(Function(x) x.CREATED_TIME).ToList()

            Return req
        End Function

        <CAuthorize(Role:="MNU40")>
        Function ListRequestNeedApprove() As List(Of TPROC_REQUEST)
            Dim req As New List(Of TPROC_REQUEST)
            Dim mail = Convert.ToString(Session("USER_MAIL"))
            req = db.TPROC_REQUEST.Where(Function(y) y.STATUS = ListEnum.Request.NeedApprove And y.APPROVAL_EMAIL.ToUpper() = mail.ToUpper()).OrderByDescending(Function(x) x.CREATED_TIME).ToList()

            Return req
        End Function

        <CAuthorize(Role:="MNU41")>
        Function ListRequestNeedComplete() As List(Of TPROC_REQUEST)
            Dim req As New List(Of TPROC_REQUEST)
            req = db.TPROC_REQUEST.Where(Function(y) y.STATUS = ListEnum.Request.NeedComplete And y.ROW_STATUS = ListEnum.RowStat.Live).OrderByDescending(Function(x) x.CREATED_TIME).ToList()

            Return req
        End Function

        <CAuthorize(Role:="MNU59")>
        Function ListRequestCompleted() As List(Of TPROC_REQUEST)
            Dim req As New List(Of TPROC_REQUEST)
            req = db.TPROC_REQUEST.Where(Function(y) y.STATUS = ListEnum.Request.Completed And y.ROW_STATUS = ListEnum.RowStat.Live).OrderByDescending(Function(x) x.CREATED_TIME).ToList()

            Return req
        End Function


#End Region

#Region "OTHER"
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Function CheckData(ByVal id As Decimal, ByVal rel_flag As String, ByVal control As String, ByVal actions As String) As Integer
            Dim result As Integer = 0

            'check data in menu setup
            If control = "User" And actions = "Create" Then
                Dim userC As New USERController
                result = GetResult(userC.CheckData(id, rel_flag), 1)
                If result = 0 Then
                    result = GetResult(userC.IsInActive(rel_flag), 3)
                End If
            ElseIf control = "WA" And actions = "Create" Then
                Dim waC As New WAController
                result = GetResult(waC.CheckData(id, rel_flag), 1)
                If result = 0 Then
                    result = GetResult(waC.IsInActive(rel_flag), 3)
                End If
            ElseIf control = "RD" And actions = "Create" Then
                Dim rdC As New RELATED_DEPARTMENTController
                result = GetResult(rdC.CheckData(id, rel_flag), 1)
                If result = 0 Then
                    result = GetResult(rdC.IsInActive(rel_flag), 3)
                End If
            ElseIf control = "CHART_OF_ACCOUNT" And actions = "Create" Then
                Dim coaC As New CHART_OF_ACCOUNTController
                result = GetResult(coaC.CheckData(id, rel_flag), 1)
                If result = 0 Then
                    result = GetResult(coaC.IsInActive(rel_flag), 3)
                End If
            ElseIf control = "FST" And actions = "Create" Then
                Dim fstC As New FORM_SUB_TYPEController
                result = GetResult(fstC.CheckData(id, rel_flag), 1)
                If result = 0 Then
                    result = GetResult(fstC.IsInActive(rel_flag), 3)
                End If
            End If

            If result = 1 Or result = 3 Then Return result

            result = GetResult(CheckRequest(id, rel_flag, control, actions), 2)

            Return result
        End Function

        Function CheckRequest(ByVal id As Decimal, ByVal rel_flag As String, ByVal control As String, ByVal actions As String) As Integer
            Dim db As New eProcurementEntities
            Dim req As New TPROC_REQUEST
            Dim result As Integer = 0

            'check create in request
            If id = 0 Then
                req = db.TPROC_REQUEST.Where(Function(x) x.RELATION_FLAG = rel_flag And x.CONTROL = control And x.ACTION = actions And x.ROW_STATUS = ListEnum.RowStat.Live And x.STATUS <> 4).FirstOrDefault()
                If req IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit in request
                req = db.TPROC_REQUEST.Where(Function(x) x.RELATION_FLAG = rel_flag And x.CONTROL = control And x.ACTION = actions And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live And x.STATUS <> 4).FirstOrDefault()
                If req IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function


        Function GetResult(ining As Integer, outing As Integer) As Integer
            Dim result As Integer = 0
            If ining <> 0 Then
                result = outing
            End If

            Return result
        End Function

#End Region

    End Class
End Namespace
