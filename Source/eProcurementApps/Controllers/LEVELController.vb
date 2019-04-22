Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class LEVELController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU18")>
        Function Index() As ActionResult
            Dim lEVEL As New List(Of TPROC_LEVEL)
            lEVEL = db.TPROC_LEVEL.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.RUPIAH_LIMIT).ToList()

            Return View(lEVEL)
        End Function

        <CAuthorize(Role:="MNU18")>
        Function List() As ActionResult
            Dim lEVEL As New List(Of TPROC_LEVEL)
            lEVEL = db.TPROC_LEVEL.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.RUPIAH_LIMIT).ToList()

            Return PartialView("_List", lEVEL)
        End Function

        <CAuthorize(Role:="MNU18")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim lEVEL As TPROC_LEVEL = db.TPROC_LEVEL.Find(id)
            If IsNothing(lEVEL) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", lEVEL)
        End Function

        <CAuthorize(Role:="MNU18")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU18")>
        Function ActionCreate(ByVal indonesian_level As String, ByVal rupiah_limit As Decimal, ByVal canadian_dollars As Decimal) As ActionResult
            Dim lEVEL As New TPROC_LEVEL
            lEVEL.INDONESIAN_LEVEL = indonesian_level
            lEVEL.RUPIAH_LIMIT = rupiah_limit
            lEVEL.CANADIAN_DOLLARS = canadian_dollars
            lEVEL.CREATED_TIME = Date.Now
            lEVEL.CREATED_BY = Session("USER_ID")
            db.TPROC_LEVEL.Add(lEVEL)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU18")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim lEVEL As TPROC_LEVEL = db.TPROC_LEVEL.Find(id)
            If IsNothing(lEVEL) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", lEVEL)
        End Function

        <CAuthorize(Role:="MNU18")>
        Function ActionEdit(ByVal id As Decimal, ByVal indonesian_level As String, ByVal rupiah_limit As Decimal, ByVal canadian_dollars As Decimal) As ActionResult
            Dim lEVEL As TPROC_LEVEL = db.TPROC_LEVEL.Find(id)
            lEVEL.INDONESIAN_LEVEL = indonesian_level
            lEVEL.RUPIAH_LIMIT = rupiah_limit
            lEVEL.CANADIAN_DOLLARS = canadian_dollars
            lEVEL.LAST_MODIFIED_TIME = Date.Now
            lEVEL.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(lEVEL).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU18")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim lEVEL As TPROC_LEVEL = db.TPROC_LEVEL.Find(id)
            If IsNothing(lEVEL) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", lEVEL)
        End Function

        <CAuthorize(Role:="MNU18")>
        Function ActionDelete(ByVal id As Decimal) As String
            Dim result As String = ""
            Dim lEVEL As TPROC_LEVEL = db.TPROC_LEVEL.Find(id)
            Try
                If lEVEL.TPROC_APPROVAL_DT.Count > 0 Or lEVEL.TPROC_APPR_RELDEPT_DT.Count > 0 Then
                    result = "0"
                Else
                    lEVEL.ROW_STATUS = ListEnum.RowStat.InActive
                    lEVEL.LAST_MODIFIED_TIME = Date.Now
                    lEVEL.LAST_MODIFIED_BY = Session("USER_ID")
                    db.Entry(lEVEL).State = EntityState.Modified
                    db.SaveChanges()
                    result = "1"
                End If
            Catch ex As Exception
                result = "0"
            End Try

            Return result
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU18")>
        Function CheckData(ByVal id As Decimal, ByVal indonesian_level As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim lEVEL As New TPROC_LEVEL
            'check create
            If id = 0 Then
                lEVEL = db.TPROC_LEVEL.Where(Function(x) x.INDONESIAN_LEVEL.ToUpper() = indonesian_level.ToUpper()).FirstOrDefault()
                If lEVEL IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                lEVEL = db.TPROC_LEVEL.Where(Function(x) x.INDONESIAN_LEVEL.ToUpper() = indonesian_level.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If lEVEL IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU18")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_LEVEL.Where(Function(x) x.INDONESIAN_LEVEL.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU18")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_LEVEL = db.TPROC_LEVEL.Find(id)
                obj.ROW_STATUS = ListEnum.RowStat.Live
                obj.LAST_MODIFIED_TIME = Date.Now
                obj.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(obj).State = EntityState.Modified
                db.SaveChanges()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace
