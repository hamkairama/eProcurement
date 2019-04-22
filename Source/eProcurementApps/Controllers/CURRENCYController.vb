Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class CURRENCYController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU02")>
        Function Index() As ActionResult
            Dim cURRENCY As New List(Of TPROC_CURRENCY)
            cURRENCY = db.TPROC_CURRENCY.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.CURRENCY_NAME).ToList()

            Return View(cURRENCY)
        End Function

        <CAuthorize(Role:="MNU02")>
        Function List() As ActionResult
            Dim cURRENCY As New List(Of TPROC_CURRENCY)
            cURRENCY = db.TPROC_CURRENCY.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.CURRENCY_NAME).ToList()

            Return PartialView("_List", cURRENCY)
        End Function

        <CAuthorize(Role:="MNU02")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cURRENCY As TPROC_CURRENCY = db.TPROC_CURRENCY.Find(id)
            If IsNothing(cURRENCY) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", cURRENCY)
        End Function

        <CAuthorize(Role:="MNU02")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU02")>
        Function ActionCreate(ByVal currency_name As String, ByVal rate As Decimal, ByVal start_date As DateTime, ByVal convertion As Decimal) As ActionResult
            Dim cURRENCY As New TPROC_CURRENCY
            cURRENCY.CURRENCY_NAME = currency_name
            cURRENCY.RATE = rate
            cURRENCY.START_DATE = start_date
            cURRENCY.CONVERSION_RP = convertion
            cURRENCY.CREATED_TIME = Date.Now
            cURRENCY.CREATED_BY = Session("USER_ID")
            db.TPROC_CURRENCY.Add(cURRENCY)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU02")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cURRENCY As TPROC_CURRENCY = db.TPROC_CURRENCY.Find(id)
            If IsNothing(cURRENCY) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", cURRENCY)
        End Function

        <CAuthorize(Role:="MNU02")>
        Function ActionEdit(ByVal id As Decimal, ByVal currency_name As String, ByVal rate As Decimal, ByVal start_date As DateTime, ByVal convertion As Decimal) As ActionResult
            Dim cURRENCY As TPROC_CURRENCY = db.TPROC_CURRENCY.Find(id)
            cURRENCY.CURRENCY_NAME = currency_name
            cURRENCY.RATE = rate
            cURRENCY.START_DATE = start_date
            cURRENCY.CONVERSION_RP = convertion
            cURRENCY.LAST_MODIFIED_TIME = Date.Now
            cURRENCY.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(cURRENCY).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU02")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim cURRENCY As TPROC_CURRENCY = db.TPROC_CURRENCY.Find(id)
            If IsNothing(cURRENCY) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", cURRENCY)
        End Function

        <CAuthorize(Role:="MNU02")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim cURRENCY As TPROC_CURRENCY = db.TPROC_CURRENCY.Find(id)
            cURRENCY.ROW_STATUS = ListEnum.RowStat.InActive
            cURRENCY.LAST_MODIFIED_TIME = Date.Now
            cURRENCY.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(cURRENCY).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU02")>
        Function CheckData(ByVal id As Decimal, ByVal currency_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim cURRENCY As New TPROC_CURRENCY
            'check create
            If id = 0 Then
                cURRENCY = db.TPROC_CURRENCY.Where(Function(x) x.CURRENCY_NAME.ToUpper() = currency_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If cURRENCY IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                cURRENCY = db.TPROC_CURRENCY.Where(Function(x) x.CURRENCY_NAME.ToUpper() = currency_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If cURRENCY IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU02")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim Currency = db.TPROC_CURRENCY.Where(Function(x) x.CURRENCY_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If Currency IsNot Nothing Then
                id = Currency.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU02")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim cURRENCY As TPROC_CURRENCY = db.TPROC_CURRENCY.Find(id)
                cURRENCY.ROW_STATUS = ListEnum.RowStat.Live
                cURRENCY.LAST_MODIFIED_TIME = Date.Now
                cURRENCY.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(cURRENCY).State = EntityState.Modified
                db.SaveChanges()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            Return RedirectToAction("Index")
        End Function


    End Class
End Namespace
