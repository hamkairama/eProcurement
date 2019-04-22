Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Helpers
Imports eProcurementApps.Models

Namespace Controllers
    Public Class FORM_TYPEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU09")>
        Function Index() As ActionResult
            Dim fORM_TYPE As New List(Of TPROC_FORM_TYPE)
            fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.FORM_TYPE_NAME).ToList()

            Return View(fORM_TYPE)
        End Function

        <CAuthorize(Role:="MNU09")>
        Function List() As ActionResult
            Dim fORM_TYPE As New List(Of TPROC_FORM_TYPE)
            fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.FORM_TYPE_NAME).ToList()

            Return PartialView("_List", fORM_TYPE)
        End Function

        <CAuthorize(Role:="MNU09")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim fORM_TYPE As TPROC_FORM_TYPE = db.TPROC_FORM_TYPE.Find(id)
            If IsNothing(fORM_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", fORM_TYPE)
        End Function

        <CAuthorize(Role:="MNU09")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU09")>
        Function ActionCreate(ByVal form_type_name As String, ByVal form_type_description As String, ByVal is_gt As Decimal) As ActionResult
            Dim fORM_TYPE As New TPROC_FORM_TYPE
            fORM_TYPE.FORM_TYPE_NAME = form_type_name
            fORM_TYPE.FORM_TYPE_DESCRIPTION = form_type_description
            fORM_TYPE.IS_GOOD_TYPE = is_gt
            fORM_TYPE.CREATED_TIME = Date.Now
            fORM_TYPE.CREATED_BY = Session("USER_ID")
            db.TPROC_FORM_TYPE.Add(fORM_TYPE)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU09")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim fORM_TYPE As TPROC_FORM_TYPE = db.TPROC_FORM_TYPE.Find(id)
            If IsNothing(fORM_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", fORM_TYPE)
        End Function

        <CAuthorize(Role:="MNU09")>
        Function ActionEdit(ByVal id As Decimal, ByVal form_type_name As String, ByVal form_type_description As String, ByVal is_gt As Decimal) As ActionResult
            Dim fORM_TYPE As TPROC_FORM_TYPE = db.TPROC_FORM_TYPE.Find(id)
            fORM_TYPE.FORM_TYPE_NAME = form_type_name
            fORM_TYPE.FORM_TYPE_DESCRIPTION = form_type_description
            fORM_TYPE.IS_GOOD_TYPE = is_gt
            fORM_TYPE.LAST_MODIFIED_TIME = Date.Now
            fORM_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(fORM_TYPE).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU09")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim fORM_TYPE As TPROC_FORM_TYPE = db.TPROC_FORM_TYPE.Find(id)
            If IsNothing(fORM_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", fORM_TYPE)
        End Function

        <CAuthorize(Role:="MNU09")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim fORM_TYPE As TPROC_FORM_TYPE = db.TPROC_FORM_TYPE.Find(id)
            fORM_TYPE.ROW_STATUS = ListEnum.RowStat.InActive
            fORM_TYPE.LAST_MODIFIED_TIME = Date.Now
            fORM_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(fORM_TYPE).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU09")>
        Function CheckData(ByVal id As Decimal, ByVal form_type_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim fORM_TYPE As New TPROC_FORM_TYPE
            'check create
            If id = 0 Then
                fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME.ToUpper() = form_type_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If fORM_TYPE IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME.ToUpper() = form_type_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If fORM_TYPE IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU09")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU09")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_FORM_TYPE = db.TPROC_FORM_TYPE.Find(id)
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
