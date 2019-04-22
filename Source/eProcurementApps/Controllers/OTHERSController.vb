Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class OthersController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        Function IndexDocuments() As ActionResult
            Dim doc As New List(Of TPROC_DOCUMENTS)
            doc = db.TPROC_DOCUMENTS.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.FILE_NAME).ToList()

            ViewBag.Message = TempData("msg")

            Return View(doc)
        End Function

        Function ListDocuments() As ActionResult
            Dim doc As New List(Of TPROC_DOCUMENTS)
            doc = db.TPROC_DOCUMENTS.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.FILE_NAME).ToList()

            Return PartialView("_ListDocuments", doc)
        End Function

        Function Download(ByVal id As Decimal) As FileResult
            Dim file_name As String = db.TPROC_DOCUMENTS.Find(id).FILE_NAME
            Dim url_attach As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), file_name)

            Dim arry_content_type As String() = file_name.Split(".")
            Dim content_type As String = arry_content_type(arry_content_type.Length - 1)

            Return File(url_attach, "application/" + content_type, file_name)
        End Function

        <CAuthorize(Role:="MNU50")>
        Public Function InsertDocuments(formCollection As FormCollection) As ActionResult
            Dim rs As New ResultStatus

            Try
                If Request IsNot Nothing Then
                    Dim file As HttpPostedFileBase = Request.Files("UploadedFile")
                    If (file IsNot Nothing) AndAlso (file.ContentLength > 0) AndAlso Not String.IsNullOrEmpty(file.FileName) Then
                        Dim attach As String = System.IO.Path.GetFileName(file.FileName)
                        Dim path As String = System.IO.Path.Combine(Server.MapPath("~/Attachments"), attach)

                        Dim id_doc = IsInActive(attach)

                        If id_doc > 0 Then
                            rs = ActionActiviting(id_doc)
                        Else
                            Dim doc As New TPROC_DOCUMENTS
                            doc.CREATED_BY = Session("USER_ID")
                            doc.CREATED_TIME = Date.Now
                            doc.FILE_NAME = attach
                            Using db As New eProcurementEntities
                                db.TPROC_DOCUMENTS.Add(doc)
                                db.SaveChanges()
                            End Using
                            'copy file to folder in application
                            file.SaveAs(path)
                            rs.SetSuccessStatus("Data has been uploaded")
                        End If
                    Else
                        rs.SetErrorStatus("Please select the file before")
                    End If
                End If
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            TempData("msg") = rs.MessageText

            Return RedirectToAction("IndexDocuments")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU50")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim doc As TPROC_DOCUMENTS = db.TPROC_DOCUMENTS.Find(id)
            If IsNothing(doc) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", doc)
        End Function

        <CAuthorize(Role:="MNU50")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim doc As TPROC_DOCUMENTS = db.TPROC_DOCUMENTS.Find(id)
            doc.ROW_STATUS = ListEnum.RowStat.InActive
            doc.LAST_MODIFIED_TIME = Date.Now
            doc.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(doc).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("IndexDocuments")
        End Function

        <CAuthorize(Role:="MNU50")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim doc = db.TPROC_DOCUMENTS.Where(Function(x) x.FILE_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If doc IsNot Nothing Then
                id = doc.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU50")>
        Function ActionActiviting(ByVal id As Decimal) As ResultStatus
            Dim rs As New ResultStatus
            Try
                Dim doc As TPROC_DOCUMENTS = db.TPROC_DOCUMENTS.Find(id)
                doc.ROW_STATUS = ListEnum.RowStat.Live
                doc.LAST_MODIFIED_TIME = Date.Now
                doc.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(doc).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus("Data has been reactivated")
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

    End Class
End Namespace
