Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class GOOD_TYPEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities
        <CAuthorize(Role:="MNU08")>
        Function Index() As ActionResult
            Dim gOOD_TYPE As New List(Of TPROC_GOOD_TYPE)
            gOOD_TYPE = db.TPROC_GOOD_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.GOOD_TYPE_NAME).ToList()

            Return View(gOOD_TYPE)
        End Function

        <CAuthorize(Role:="MNU08")>
        Function List() As ActionResult
            Dim gOOD_TYPE As New List(Of TPROC_GOOD_TYPE)
            gOOD_TYPE = db.TPROC_GOOD_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.GOOD_TYPE_NAME).ToList()

            Return PartialView("_List", gOOD_TYPE)
        End Function

        <CAuthorize(Role:="MNU08")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim gOOD_TYPE As TPROC_GOOD_TYPE = db.TPROC_GOOD_TYPE.Find(id)
            If IsNothing(gOOD_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", gOOD_TYPE)
        End Function

        <CAuthorize(Role:="MNU08")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU08")>
        Function ActionCreate(ByVal good_type_name As String, ByVal good_type_description As String) As ActionResult
            Dim gOOD_TYPE As New TPROC_GOOD_TYPE
            gOOD_TYPE.GOOD_TYPE_NAME = good_type_name
            gOOD_TYPE.GOOD_TYPE_DESCRIPTION = good_type_description
            gOOD_TYPE.CREATED_TIME = Date.Now
            gOOD_TYPE.CREATED_BY = Session("USER_ID")
            db.TPROC_GOOD_TYPE.Add(gOOD_TYPE)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU08")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim gOOD_TYPE As TPROC_GOOD_TYPE = db.TPROC_GOOD_TYPE.Find(id)
            If IsNothing(gOOD_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", gOOD_TYPE)
        End Function

        <CAuthorize(Role:="MNU08")>
        Function ActionEdit(ByVal id As Decimal, ByVal good_type_name As String, ByVal good_type_description As String) As ActionResult
            Dim gOOD_TYPE As TPROC_GOOD_TYPE = db.TPROC_GOOD_TYPE.Find(id)
            gOOD_TYPE.GOOD_TYPE_NAME = good_type_name
            gOOD_TYPE.GOOD_TYPE_DESCRIPTION = good_type_description
            gOOD_TYPE.LAST_MODIFIED_TIME = Date.Now
            gOOD_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(gOOD_TYPE).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU08")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim gOOD_TYPE As TPROC_GOOD_TYPE = db.TPROC_GOOD_TYPE.Find(id)
            If IsNothing(gOOD_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", gOOD_TYPE)
        End Function

        <CAuthorize(Role:="MNU08")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim gOOD_TYPE As TPROC_GOOD_TYPE = db.TPROC_GOOD_TYPE.Find(id)
            gOOD_TYPE.ROW_STATUS = ListEnum.RowStat.InActive
            gOOD_TYPE.LAST_MODIFIED_TIME = Date.Now
            gOOD_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(gOOD_TYPE).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU08")>
        Function CheckData(ByVal id As Decimal, ByVal good_type_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim gOOD_TYPE As New TPROC_GOOD_TYPE
            'check create
            If id = 0 Then
                gOOD_TYPE = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = good_type_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If gOOD_TYPE IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                gOOD_TYPE = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = good_type_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If gOOD_TYPE IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function


        <CAuthorize(Role:="MNU08")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU08")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_GOOD_TYPE = db.TPROC_GOOD_TYPE.Find(id)
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
