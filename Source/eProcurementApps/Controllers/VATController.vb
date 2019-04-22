Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class VATController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU03")>
        Function Index() As ActionResult
            Dim vAT As New List(Of TPROC_VAT)
            vAT = db.TPROC_VAT.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.VAT_NAME).ToList()

            Return View(vAT)
        End Function

        <CAuthorize(Role:="MNU03")>
        Function List() As ActionResult
            Dim vAT As New List(Of TPROC_VAT)
            vAT = db.TPROC_VAT.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.VAT_NAME).ToList()

            Return PartialView("_List", vAT)
        End Function

        <CAuthorize(Role:="MNU03")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim vAT As TPROC_VAT = db.TPROC_VAT.Find(id)
            If IsNothing(vAT) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", vAT)
        End Function

        <CAuthorize(Role:="MNU03")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU03")>
        Function ActionCreate(ByVal vat_name As Decimal, ByVal start_date As DateTime) As ActionResult
            Dim vAT As New TPROC_VAT
            vAT.VAT_NAME = vat_name
            vAT.START_DATE = start_date
            vAT.CREATED_TIME = Date.Now
            vAT.CREATED_BY = Session("USER_ID")
            db.TPROC_VAT.Add(vAT)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU03")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim vAT As TPROC_VAT = db.TPROC_VAT.Find(id)
            If IsNothing(vAT) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", vAT)
        End Function

        <CAuthorize(Role:="MNU03")>
        Function ActionEdit(ByVal id As Decimal, ByVal vat_name As Decimal, ByVal start_date As DateTime) As ActionResult
            Dim vAT As TPROC_VAT = db.TPROC_VAT.Find(id)
            vAT.VAT_NAME = vat_name
            vAT.START_DATE = start_date
            vAT.LAST_MODIFIED_TIME = Date.Now
            vAT.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(vAT).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU03")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim vAT As TPROC_VAT = db.TPROC_VAT.Find(id)
            If IsNothing(vAT) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", vAT)
        End Function

        <CAuthorize(Role:="MNU03")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim vAT As TPROC_VAT = db.TPROC_VAT.Find(id)
            vAT.ROW_STATUS = ListEnum.RowStat.InActive
            vAT.LAST_MODIFIED_TIME = Date.Now
            vAT.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(vAT).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU03")>
        Function CheckData(ByVal id As Decimal, ByVal vat_name As Decimal) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim vAT As New TPROC_VAT
            'check create
            If id = 0 Then
                vAT = db.TPROC_VAT.Where(Function(x) x.VAT_NAME = vat_name And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If vAT IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                vAT = db.TPROC_VAT.Where(Function(x) x.VAT_NAME = vat_name And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If vAT IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU03")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_VAT.Where(Function(x) x.VAT_NAME = value And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU03")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_VAT = db.TPROC_VAT.Find(id)
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
