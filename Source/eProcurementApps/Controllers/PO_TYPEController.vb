Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Helpers
Imports eProcurementApps.Models

Namespace Controllers
    Public Class PO_TYPEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU13")>
        Function Index() As ActionResult
            Dim pO_TYPE As New List(Of TPROC_PO_TYPE)
            pO_TYPE = db.TPROC_PO_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.PO_TYPE_NAME).ToList()

            Return View(pO_TYPE)
        End Function

        <CAuthorize(Role:="MNU13")>
        Function List() As ActionResult
            Dim pO_TYPE As New List(Of TPROC_PO_TYPE)
            pO_TYPE = db.TPROC_PO_TYPE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.PO_TYPE_NAME).ToList()

            Return PartialView("_List", pO_TYPE)
        End Function

        <CAuthorize(Role:="MNU13")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            ViewBag.FormType = Dropdown.FormType()
            Dim pO_TYPE As TPROC_PO_TYPE = db.TPROC_PO_TYPE.Find(id)
            If IsNothing(pO_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", pO_TYPE)
        End Function

        <CAuthorize(Role:="MNU13")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        Function ActionCreate(ByVal po_type_name As String, ByVal po_type_description As String, ByVal form_type_id As Decimal) As ActionResult
            Dim rs As New ResultStatus
            Try
                Dim pO_TYPE As New TPROC_PO_TYPE
                pO_TYPE.PO_TYPE_NAME = po_type_name
                pO_TYPE.PO_TYPE_DESCRIPTION = po_type_description
                pO_TYPE.FORM_TYPE_ID = form_type_id
                pO_TYPE.CREATED_TIME = Date.Now
                pO_TYPE.CREATED_BY = Session("USER_ID")
                db.TPROC_PO_TYPE.Add(pO_TYPE)
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU13")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            ViewBag.FormType = Dropdown.FormType()
            Dim pO_TYPE As TPROC_PO_TYPE = db.TPROC_PO_TYPE.Find(id)
            If IsNothing(pO_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", pO_TYPE)
        End Function

        <CAuthorize(Role:="MNU13")>
        Function ActionEdit(ByVal id As Decimal, ByVal po_type_name As String, ByVal po_type_description As String, ByVal form_type_id As Decimal) As ActionResult
            Dim pO_TYPE As TPROC_PO_TYPE = db.TPROC_PO_TYPE.Find(id)
            pO_TYPE.PO_TYPE_NAME = po_type_name
            pO_TYPE.PO_TYPE_DESCRIPTION = po_type_description
            pO_TYPE.FORM_TYPE_ID = form_type_id
            pO_TYPE.LAST_MODIFIED_TIME = Date.Now
            pO_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(pO_TYPE).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU13")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            ViewBag.FormType = Dropdown.FormType()
            Dim pO_TYPE As TPROC_PO_TYPE = db.TPROC_PO_TYPE.Find(id)
            If IsNothing(pO_TYPE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", pO_TYPE)
        End Function

        <CAuthorize(Role:="MNU13")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim pO_TYPE As TPROC_PO_TYPE = db.TPROC_PO_TYPE.Find(id)
            pO_TYPE.ROW_STATUS = ListEnum.RowStat.InActive
            pO_TYPE.LAST_MODIFIED_TIME = Date.Now
            pO_TYPE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(pO_TYPE).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU13")>
        Function CheckData(ByVal id As Decimal, ByVal po_type_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim pO_TYPE As New TPROC_PO_TYPE
            'check create
            If id = 0 Then
                pO_TYPE = db.TPROC_PO_TYPE.Where(Function(x) x.PO_TYPE_NAME.ToUpper() = po_type_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If pO_TYPE IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                pO_TYPE = db.TPROC_PO_TYPE.Where(Function(x) x.PO_TYPE_NAME.ToUpper() = po_type_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If pO_TYPE IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function


        <CAuthorize(Role:="MNU13")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_PO_TYPE.Where(Function(x) x.PO_TYPE_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU13")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_PO_TYPE = db.TPROC_PO_TYPE.Find(id)
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
