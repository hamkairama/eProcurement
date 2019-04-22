Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Helpers
Imports eProcurementApps.Models
Imports OfficeOpenXml

Namespace Controllers
    Public Class HOLIDAYController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU11")>
        Function Index() As ActionResult
            Dim hOLIDAY As New List(Of TPROC_HOLIDAY)
            hOLIDAY = db.TPROC_HOLIDAY.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.HOLIDAY_DATE).ToList()

            ViewBag.Message = TempData("msg")
            Return View(hOLIDAY)
        End Function

        <CAuthorize(Role:="MNU11")>
        Function List() As ActionResult
            Dim hOLIDAY As New List(Of TPROC_HOLIDAY)
            hOLIDAY = db.TPROC_HOLIDAY.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.HOLIDAY_DATE).ToList()

            Return PartialView("_List", hOLIDAY)
        End Function

        <CAuthorize(Role:="MNU11")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim hOLIDAY As TPROC_HOLIDAY = db.TPROC_HOLIDAY.Find(id)
            If IsNothing(hOLIDAY) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", hOLIDAY)
        End Function

        <CAuthorize(Role:="MNU11")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU11")>
        Function ActionCreate(ByVal holiday_date As DateTime, ByVal holiday_description As String) As ActionResult
            Dim hOLIDAY As New TPROC_HOLIDAY
            hOLIDAY.HOLIDAY_DATE = holiday_date
            Dim curr_year As Decimal = holiday_date.Year
            Dim curr_month As Decimal = holiday_date.Month
            hOLIDAY.HOLIDAY_DESCRIPTION = holiday_description
            hOLIDAY.CURR_YEAR = curr_year
            hOLIDAY.CURR_MONTH = curr_month
            hOLIDAY.CREATED_TIME = Date.Now
            hOLIDAY.CREATED_BY = Session("USER_ID")
            db.TPROC_HOLIDAY.Add(hOLIDAY)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU11")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim hOLIDAY As TPROC_HOLIDAY = db.TPROC_HOLIDAY.Find(id)
            If IsNothing(hOLIDAY) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", hOLIDAY)
        End Function

        <CAuthorize(Role:="MNU11")>
        Function ActionEdit(ByVal id As Decimal, ByVal holiday_date As DateTime, ByVal holiday_description As String) As ActionResult
            Dim hOLIDAY As TPROC_HOLIDAY = db.TPROC_HOLIDAY.Find(id)
            hOLIDAY.HOLIDAY_DATE = holiday_date
            hOLIDAY.HOLIDAY_DESCRIPTION = holiday_description
            hOLIDAY.LAST_MODIFIED_TIME = Date.Now
            hOLIDAY.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(hOLIDAY).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU11")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim hOLIDAY As TPROC_HOLIDAY = db.TPROC_HOLIDAY.Find(id)
            If IsNothing(hOLIDAY) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", hOLIDAY)
        End Function

        <CAuthorize(Role:="MNU11")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim hOLIDAY As TPROC_HOLIDAY = db.TPROC_HOLIDAY.Find(id)
            hOLIDAY.ROW_STATUS = ListEnum.RowStat.InActive
            hOLIDAY.LAST_MODIFIED_TIME = Date.Now
            hOLIDAY.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(hOLIDAY).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU11")>
        Function CheckData(ByVal id As Decimal, ByVal holiday_date As DateTime) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim hOLIDAY As New TPROC_HOLIDAY
            'check create
            If id = 0 Then
                hOLIDAY = db.TPROC_HOLIDAY.Where(Function(x) x.HOLIDAY_DATE = holiday_date And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If hOLIDAY IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                hOLIDAY = db.TPROC_HOLIDAY.Where(Function(x) x.HOLIDAY_DATE = holiday_date And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If hOLIDAY IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU11")>
        Public Function Upload(formCollection As FormCollection) As ActionResult
            Dim sb As New StringBuilder

            Try
                If Request IsNot Nothing Then
                    Dim file As HttpPostedFileBase = Request.Files("UploadedFile")
                    If (file IsNot Nothing) AndAlso (file.ContentLength > 0) AndAlso Not String.IsNullOrEmpty(file.FileName) Then
                        Dim fileName As String = file.FileName
                        Dim fileContentType As String = file.ContentType
                        Dim fileBytes As Byte() = New Byte(file.ContentLength - 1) {}
                        Dim data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength))

                        Using package = New ExcelPackage(file.InputStream)
                            Dim currentSheet = package.Workbook.Worksheets
                            Dim workSheet = currentSheet.First()
                            Dim noOfCol = workSheet.Dimension.[End].Column
                            Dim noOfRow = workSheet.Dimension.[End].Row

                            For rowIterator As Integer = 2 To noOfRow
                                Dim holi_day As Date = workSheet.Cells(rowIterator, 1).Value.ToString()
                                Dim isExist = CheckData(0, holi_day)

                                If isExist = 1 Then
                                    sb.Append("Date for " + holi_day + " already exist." + "<br />")
                                    sb.AppendLine()
                                Else
                                    Dim hOLIDAY As New TPROC_HOLIDAY
                                    hOLIDAY.HOLIDAY_DATE = holi_day
                                    Dim curr_year As Decimal = holi_day.Year
                                    Dim curr_month As Decimal = holi_day.Month
                                    hOLIDAY.HOLIDAY_DESCRIPTION = workSheet.Cells(rowIterator, 2).Value.ToString()
                                    hOLIDAY.CURR_YEAR = curr_year
                                    hOLIDAY.CURR_MONTH = curr_month
                                    hOLIDAY.CREATED_TIME = Date.Now
                                    hOLIDAY.CREATED_BY = Session("USER_ID")
                                    Using db2 As New eProcurementEntities
                                        db2.TPROC_HOLIDAY.Add(hOLIDAY)
                                        db2.SaveChanges()
                                    End Using
                                End If
                            Next
                        End Using
                    Else
                        sb.Append("Please select the file before")
                    End If
                End If
            Catch ex As Exception
                sb.Append(ex.Message + " please check format and available of value" + "<br />")
            End Try

            TempData("msg") = sb.ToString()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU11")>
        Function IsInActive(ByVal value As DateTime) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_HOLIDAY.Where(Function(x) x.HOLIDAY_DATE = value And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU11")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_HOLIDAY = db.TPROC_HOLIDAY.Find(id)
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
