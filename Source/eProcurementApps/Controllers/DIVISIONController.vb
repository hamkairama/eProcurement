Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class DIVISIONController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU06")>
        Function Index() As ActionResult
            Dim dIVISION As New List(Of TPROC_DIVISION)
            dIVISION = db.TPROC_DIVISION.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.DIVISION_NAME).ToList()

            ViewBag.Message = TempData("message")

            Return View(dIVISION)
        End Function

        <CAuthorize(Role:="MNU06")>
        Function List() As ActionResult
            Dim dIVISION As New List(Of TPROC_DIVISION)
            dIVISION = db.TPROC_DIVISION.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.DIVISION_NAME).ToList()

            Return PartialView("_List", dIVISION)
        End Function

        <CAuthorize(Role:="MNU06")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim dIVISION As TPROC_DIVISION = db.TPROC_DIVISION.Find(id)
            If IsNothing(dIVISION) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", dIVISION)
        End Function

        <CAuthorize(Role:="MNU06")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU06")>
        Function ActionCreate(ByVal division_name As String, ByVal division_description As String) As ActionResult
            Dim dIVISION As New TPROC_DIVISION
            dIVISION.DIVISION_NAME = division_name
            dIVISION.DIVISION_DESCRIPTION = division_description
            dIVISION.CREATED_TIME = Date.Now
            dIVISION.CREATED_BY = Session("USER_ID")
            db.TPROC_DIVISION.Add(dIVISION)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU06")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim dIVISION As TPROC_DIVISION = db.TPROC_DIVISION.Find(id)
            If IsNothing(dIVISION) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", dIVISION)
        End Function

        <CAuthorize(Role:="MNU06")>
        Function ActionEdit(ByVal id As Decimal, ByVal division_name As String, ByVal division_description As String) As ActionResult
            Dim dIVISION As TPROC_DIVISION = db.TPROC_DIVISION.Find(id)
            dIVISION.DIVISION_NAME = division_name
            dIVISION.DIVISION_DESCRIPTION = division_description
            dIVISION.LAST_MODIFIED_TIME = Date.Now
            dIVISION.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(dIVISION).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU06")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim dIVISION As TPROC_DIVISION = db.TPROC_DIVISION.Find(id)
            If IsNothing(dIVISION) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", dIVISION)
        End Function

        <CAuthorize(Role:="MNU06")>
        Function ActionDelete(ByVal id As Decimal) As String
            Dim rs As String = ""

            Try
                Dim dIVISION As TPROC_DIVISION = db.TPROC_DIVISION.Find(id)
                If dIVISION.TPROC_APPROVAL_GR.Count > 0 Then
                    rs = "0"
                Else
                    dIVISION.ROW_STATUS = ListEnum.RowStat.InActive
                    dIVISION.LAST_MODIFIED_TIME = Date.Now
                    dIVISION.LAST_MODIFIED_BY = Session("USER_ID")
                    db.Entry(dIVISION).State = EntityState.Modified
                    db.SaveChanges()
                    rs = "1"
                End If
            Catch ex As Exception
                rs = ex.Message
            End Try

            Return rs
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU06")>
        Function CheckData(ByVal id As Decimal, ByVal division_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim dIVISION As New TPROC_DIVISION
            'check create
            If id = 0 Then
                dIVISION = db.TPROC_DIVISION.Where(Function(x) x.DIVISION_NAME.ToUpper() = division_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If dIVISION IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                dIVISION = db.TPROC_DIVISION.Where(Function(x) x.DIVISION_NAME.ToUpper() = division_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If dIVISION IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function


        <CAuthorize(Role:="MNU06")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_DIVISION.Where(Function(x) x.DIVISION_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU06")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_DIVISION = db.TPROC_DIVISION.Find(id)
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
