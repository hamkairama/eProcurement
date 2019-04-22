Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class PPHController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU04")>
        Function Index() As ActionResult
            Dim pPH As New List(Of TPROC_PPH)
            pPH = db.TPROC_PPH.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.PPH_NAME).ToList()

            Return View(pPH)
        End Function

        <CAuthorize(Role:="MNU04")>
        Function List() As ActionResult
            Dim pPH As New List(Of TPROC_PPH)
            pPH = db.TPROC_PPH.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.PPH_NAME).ToList()

            Return PartialView("_List", pPH)
        End Function

        <CAuthorize(Role:="MNU04")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim pPH As TPROC_PPH = db.TPROC_PPH.Find(id)
            If IsNothing(pPH) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", pPH)
        End Function

        <CAuthorize(Role:="MNU04")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU04")>
        Function ActionCreate(ByVal pph_name As Decimal, ByVal start_date As DateTime) As ActionResult
            Dim pPH As New TPROC_PPH
            pPH.PPH_NAME = pph_name
            pPH.START_DATE = start_date
            pPH.CREATED_TIME = Date.Now
            pPH.CREATED_BY = Session("USER_ID")
            db.TPROC_PPH.Add(pPH)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU04")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim pPH As TPROC_PPH = db.TPROC_PPH.Find(id)
            If IsNothing(pPH) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", pPH)
        End Function

        <CAuthorize(Role:="MNU04")>
        Function ActionEdit(ByVal id As Decimal, ByVal pph_name As Decimal, ByVal start_date As DateTime) As ActionResult
            Dim pPH As TPROC_PPH = db.TPROC_PPH.Find(id)
            pPH.PPH_NAME = pph_name
            pPH.START_DATE = start_date
            pPH.LAST_MODIFIED_TIME = Date.Now
            pPH.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(pPH).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU04")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim pPH As TPROC_PPH = db.TPROC_PPH.Find(id)
            If IsNothing(pPH) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", pPH)
        End Function

        <CAuthorize(Role:="MNU04")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim pPH As TPROC_PPH = db.TPROC_PPH.Find(id)
            pPH.ROW_STATUS = ListEnum.RowStat.InActive
            pPH.LAST_MODIFIED_TIME = Date.Now
            pPH.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(pPH).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU04")>
        Function CheckData(ByVal id As Decimal, ByVal pph_name As Decimal) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim pPH As New TPROC_PPH
            'check create
            If id = 0 Then
                pPH = db.TPROC_PPH.Where(Function(x) x.PPH_NAME = pph_name And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If pPH IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                pPH = db.TPROC_PPH.Where(Function(x) x.PPH_NAME = pph_name And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If pPH IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU04")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_PPH.Where(Function(x) x.PPH_NAME = value And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU04")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_PPH = db.TPROC_PPH.Find(id)
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
