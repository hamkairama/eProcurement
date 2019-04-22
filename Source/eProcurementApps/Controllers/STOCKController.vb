Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports OfficeOpenXml

Namespace Controllers
    Public Class STOCKController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities
        Public Shared dtTable As DataTable

        <CAuthorize(Role:="MNU20")>
        Function Index() As ActionResult
            Dim sTOCK As New List(Of TPROC_STOCK)
            sTOCK = db.TPROC_STOCK.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.ITEM_CODE).ToList()
            ViewBag.Message = TempData("msg")

            Return View(sTOCK)
        End Function

        <CAuthorize(Role:="MNU20")>
        Function List() As ActionResult
            Dim sTOCK As New List(Of TPROC_STOCK)
            sTOCK = db.TPROC_STOCK.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.ITEM_CODE).ToList()

            Return PartialView("_List", sTOCK)
        End Function

        <CAuthorize(Role:="MNU20")>
        Function Details(ByVal id As Decimal) As ActionResult
            ViewBag.FormType = Dropdown.FormType()
            ViewBag.GoodType = Dropdown.GoodType()
            ViewBag.Supplier = Dropdown.Supplier()
            ViewBag.COA = Dropdown.COA()

            Dim sTOCK As TPROC_STOCK = db.TPROC_STOCK.Find(id)
            If IsNothing(sTOCK) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", sTOCK)
        End Function

        <CAuthorize(Role:="MNU20")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU20")>
        Function ActionCreate(ByVal item_code As String, ByVal item_description As String, ByVal lookup_code As String, ByVal unit_of_stock As String, ByVal latest_cost As String,
                              ByVal average_cost As String, ByVal purchase_account As String, ByVal stock_expenses As String, ByVal good_type_id As Decimal, ByVal supplier_id As Decimal,
                              ByVal qty As Decimal, ByVal form_type_id As Decimal, ByVal qty_min As Decimal) As ActionResult
            Dim sTOCK As New TPROC_STOCK
            Try
                sTOCK.ITEM_CODE = item_code
                sTOCK.ITEM_DESCRIPTION = item_description
                sTOCK.LOOKUP_CODE = lookup_code
                sTOCK.UNIT_OF_STOCK = unit_of_stock
                sTOCK.LATEST_COST = latest_cost
                sTOCK.AVERAGE_COST = average_cost
                sTOCK.PURCHASE_ACCOUNT = purchase_account
                sTOCK.STOCK_EXPENSES = stock_expenses
                sTOCK.GOOD_TYPE_ID = good_type_id
                sTOCK.SUPPLIER_ID = supplier_id
                sTOCK.QUANTITY = qty
                sTOCK.FORM_TYPE_ID = form_type_id
                sTOCK.QUANTITY_MIN = qty_min
                sTOCK.CREATED_TIME = Date.Now
                sTOCK.CREATED_BY = Session("USER_ID")
                db.TPROC_STOCK.Add(sTOCK)
                db.SaveChanges()
            Catch ex As Exception

            End Try

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU20")>
        Function Edit(ByVal id As Decimal) As ActionResult
            ViewBag.FormType = Dropdown.FormType()
            ViewBag.GoodType = Dropdown.GoodType()
            ViewBag.Supplier = Dropdown.Supplier()
            ViewBag.COA = Dropdown.COA()
            Dim sTOCK As TPROC_STOCK = db.TPROC_STOCK.Find(id)
            If IsNothing(sTOCK) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", sTOCK)
        End Function

        <CAuthorize(Role:="MNU20")>
        Function ActionEdit(ByVal id As Decimal, ByVal item_code As String, ByVal item_description As String, ByVal lookup_code As String, ByVal unit_of_stock As String,
                            ByVal latest_cost As String, ByVal average_cost As String, ByVal purchase_account As String, ByVal stock_expenses As String, ByVal good_type_id As Decimal,
                            ByVal supplier_id As Decimal, ByVal qty As Decimal, ByVal form_type_id As Decimal, ByVal qty_min As Decimal) As ActionResult
            Try
                Dim sTOCK As TPROC_STOCK = db.TPROC_STOCK.Find(id)
                sTOCK.ITEM_CODE = item_code
                sTOCK.ITEM_DESCRIPTION = item_description
                sTOCK.LOOKUP_CODE = lookup_code
                sTOCK.UNIT_OF_STOCK = unit_of_stock
                sTOCK.LATEST_COST = latest_cost
                sTOCK.AVERAGE_COST = average_cost
                sTOCK.PURCHASE_ACCOUNT = purchase_account
                sTOCK.STOCK_EXPENSES = stock_expenses
                sTOCK.GOOD_TYPE_ID = good_type_id
                sTOCK.SUPPLIER_ID = supplier_id
                sTOCK.QUANTITY = qty
                sTOCK.FORM_TYPE_ID = form_type_id
                sTOCK.QUANTITY_MIN = qty_min
                sTOCK.LAST_MODIFIED_TIME = Date.Now
                sTOCK.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(sTOCK).State = EntityState.Modified
                db.SaveChanges()
            Catch ex As Exception

            End Try

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU20")>
        Function Delete(ByVal id As Decimal) As ActionResult
            Dim sTOCK As TPROC_STOCK = db.TPROC_STOCK.Find(id)
            If IsNothing(sTOCK) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", sTOCK)
        End Function

        <CAuthorize(Role:="MNU20")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim sTOCK As TPROC_STOCK = db.TPROC_STOCK.Find(id)
            sTOCK.ROW_STATUS = ListEnum.RowStat.InActive
            sTOCK.LAST_MODIFIED_TIME = Date.Now
            sTOCK.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(sTOCK).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU20")>
        Function CheckData(ByVal id As Decimal, ByVal item_code As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim sTOCK As New TPROC_STOCK
            'check create
            If id = 0 Then
                sTOCK = db.TPROC_STOCK.Where(Function(x) x.ITEM_CODE.ToUpper() = item_code.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If sTOCK IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                sTOCK = db.TPROC_STOCK.Where(Function(x) x.ITEM_CODE.ToUpper() = item_code.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If sTOCK IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        Function PopStock(ByVal id_gt As Decimal, ByVal id_ft As Decimal) As ActionResult
            Dim stock As New List(Of TPROC_STOCK)
            ' stock = db.TPROC_STOCK.Where(Function(x) x.GOOD_TYPE_ID = id).ToList()
            Session("ID_GT") = id_gt
            Session("ID_FT") = id_ft

            Return PartialView("_PopupStock", stock)
        End Function

        Function PopSearch(ByVal item_desc As String, ByVal id_gt As Decimal, ByVal id_ft As Decimal) As ActionResult
            Dim stock As New List(Of TPROC_STOCK)
            If item_desc Is Nothing Or item_desc = "" Then
                stock = db.TPROC_STOCK.Where(Function(x) x.GOOD_TYPE_ID = id_gt And x.FORM_TYPE_ID = id_ft And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()
            Else
                stock = db.TPROC_STOCK.Where(Function(x) x.ITEM_DESCRIPTION.ToUpper().Contains(item_desc.ToUpper()) And x.GOOD_TYPE_ID = id_gt And x.FORM_TYPE_ID = id_ft And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()
            End If


            Return PartialView("_PopupStockList", stock)
        End Function

        <CAuthorize(Role:="MNU20")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_STOCK.Where(Function(x) x.ITEM_CODE.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU20")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_STOCK = db.TPROC_STOCK.Find(id)
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

        <CAuthorize(Role:="MNU20")>
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
                        Dim stockId = GetGoodTypeStockID()
                        Dim nonStockID = GetGoodTypeNonStockID()
                        Dim printingId = GetFtpPrintingID()
                        Dim osId = GetFtpOsID()

                        Using package = New ExcelPackage(file.InputStream)
                            Dim currentSheet = package.Workbook.Worksheets
                            Dim workSheet = currentSheet.First()
                            Dim noOfCol = workSheet.Dimension.[End].Column
                            Dim noOfRow = workSheet.Dimension.[End].Row

                            For rowIterator As Integer = 2 To noOfRow
                                Dim item As String = workSheet.Cells(rowIterator, 1).Value.ToString()
                                Dim isExist = CheckData(0, item)

                                If isExist = 1 Then
                                    sb.Append("Item " + item + " already exist." + "<br />")
                                    sb.AppendLine()
                                Else
                                    Dim itemx As New TPROC_STOCK
                                    itemx.ITEM_CODE = workSheet.Cells(rowIterator, 1).Value.ToString()
                                    itemx.ITEM_DESCRIPTION = workSheet.Cells(rowIterator, 2).Value.ToString()

                                    If workSheet.Cells(rowIterator, 3).Value Is Nothing Then
                                        itemx.LOOKUP_CODE = ""
                                    Else
                                        itemx.LOOKUP_CODE = workSheet.Cells(rowIterator, 3).Value.ToString()
                                    End If

                                    If workSheet.Cells(rowIterator, 4).Value Is Nothing Then
                                        itemx.UNIT_OF_STOCK = ""
                                    Else
                                        itemx.UNIT_OF_STOCK = workSheet.Cells(rowIterator, 4).Value.ToString()
                                    End If

                                    itemx.LATEST_COST = workSheet.Cells(rowIterator, 5).Value.ToString()
                                    itemx.AVERAGE_COST = workSheet.Cells(rowIterator, 6).Value.ToString()
                                    itemx.PURCHASE_ACCOUNT = workSheet.Cells(rowIterator, 7).Value.ToString()
                                    itemx.STOCK_EXPENSES = workSheet.Cells(rowIterator, 8).Value.ToString()
                                    'itemx.GOOD_TYPE_ID = workSheet.Cells(rowIterator, 9).Value.ToString()
                                    If workSheet.Cells(rowIterator, 9).Value.ToString() = "Stock" Then
                                        itemx.GOOD_TYPE_ID = stockId
                                    Else
                                        itemx.GOOD_TYPE_ID = nonStockID
                                    End If
                                    itemx.SUPPLIER_ID = workSheet.Cells(rowIterator, 10).Value.ToString()
                                    itemx.QUANTITY = workSheet.Cells(rowIterator, 11).Value.ToString()
                                    'itemx.FORM_TYPE_ID = workSheet.Cells(rowIterator, 12).Value.ToString()
                                    If workSheet.Cells(rowIterator, 12).Value.ToString() = "Printing" Then
                                        itemx.FORM_TYPE_ID = printingId
                                    Else
                                        itemx.FORM_TYPE_ID = osId
                                    End If
                                    itemx.QUANTITY_MIN = workSheet.Cells(rowIterator, 13).Value.ToString()
                                    itemx.CREATED_TIME = Date.Now
                                    itemx.CREATED_BY = Session("USER_ID")
                                    Using db2 As New eProcurementEntities
                                        db2.TPROC_STOCK.Add(itemx)
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

        Function GetGoodTypeStockID() As Decimal
            Dim result As Decimal
            result = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME = "Stock").FirstOrDefault.ID

            Return result
        End Function

        Function GetGoodTypeNonStockID() As Decimal
            Dim result As Decimal
            result = db.TPROC_GOOD_TYPE.Where(Function(x) x.GOOD_TYPE_NAME = "Non Stock").FirstOrDefault.ID

            Return result
        End Function

        Function GetFtpPrintingID() As Decimal
            Dim result As Decimal
            result = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME = "Printing").FirstOrDefault.ID

            Return result
        End Function

        Function GetFtpOsID() As Decimal
            Dim result As Decimal
            result = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME = "Office Supplies").FirstOrDefault.ID

            Return result
        End Function

        Function ReportStockmovement() As ActionResult
            Dim Query As String =
            "SELECT b.ITEM_DESCRIPTION AS ""ITEM"" " & vbCrLf &
            "  , b.UNIT_OF_STOCK AS ""UNIT OF MEASURE"" " & vbCrLf &
            "  , a.STOCK_CURRENT AS ""CURRENT STOCK"" " & vbCrLf &
            "  , a.STOCK_IN AS ""STOCK IN"" " & vbCrLf &
            "  , a.STOCK_OUT AS ""STOCK OUT"" " & vbCrLf &
            "  , a.STOCK_LAST AS ""STOCK LAST""" & vbCrLf &
            "  , a.REFNO AS ""REF NO"" " & vbCrLf &
            "  , TO_CHAR(a.CREATED_TIME, 'YYYYMMDD') AS ""REF DATE"" " & vbCrLf &
            "FROM TPROC_STOCKMOVEMENT a " & vbCrLf &
            "LEFT JOIN TPROC_STOCK b ON b.ID = a.ITEM_ID " & vbCrLf &
            "WHERE 1=2"
            dtTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)

            Return View("FormReportStockMovement")
        End Function

        Function ActionReport(ByVal _PeriodeFrom As String,
                              ByVal _PeriodeTo As String) As ActionResult
            Try
                Dim Query As String =
                "SELECT b.ITEM_DESCRIPTION AS ""ITEM"" " & vbCrLf &
                "  , b.UNIT_OF_STOCK AS ""UNIT OF MEASURE"" " & vbCrLf &
                "  , a.STOCK_CURRENT AS ""CURRENT STOCK"" " & vbCrLf &
                "  , a.STOCK_IN AS ""STOCK IN"" " & vbCrLf &
                "  , a.STOCK_OUT AS ""STOCK OUT"" " & vbCrLf &
                "  , a.STOCK_LAST AS ""STOCK LAST""" & vbCrLf &
                "  , a.REFNO AS ""REF NO"" " & vbCrLf &
                "  , TO_CHAR(a.CREATED_TIME, 'YYYYMMDD') AS ""REF DATE"" " & vbCrLf &
                "FROM TPROC_STOCKMOVEMENT a " & vbCrLf &
                "LEFT JOIN TPROC_STOCK b ON b.ID = a.ITEM_ID " & vbCrLf &
                IIf(_PeriodeFrom = "" OrElse _PeriodeTo = "", "WHERE 1=2",
                "WHERE TRUNC(a.CREATED_TIME, 'DD') >= TO_DATE('" & _PeriodeFrom & "', 'DD-MM-YYYY') AND TRUNC(a.CREATED_TIME, 'DD') <= TO_DATE('" & _PeriodeTo & "', 'DD-MM-YYYY')")
                '"WHERE TO_CHAR(a.CREATED_TIME, 'YYYYMMDD') BETWEEN '" & Replace(_PeriodeFrom, "-", "") & "' AND '" & Replace(_PeriodeTo, "-", "") & "' ")
                dtTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)

            Catch ex As Exception
            End Try
            Return PartialView("FormReportStockMovement")
        End Function

        Function ReportStockmovementMonthly() As ActionResult
            Dim Query As String =
            "SELECT * " & vbCrLf &
            "FROM " & vbCrLf &
            "  ( " & vbCrLf &
            "    SELECT a.FORM_TYPE_NAME " & vbCrLf &
            "      , d.ITEM_DESCRIPTION " & vbCrLf &
            "      , SUM(a.STOCK_CURRENT) AS STOCK_CURRENT " & vbCrLf &
            "      , SUM(b.STOCK_IN) AS STOCK_IN " & vbCrLf &
            "      , 0 AS STOCK_OUT " & vbCrLf &
            "      , SUM(a.STOCK_CURRENT + b.STOCK_IN - 0) AS BALANCE_STOCK " & vbCrLf &
            "      , SUM(d.AVERAGE_COST) AS PRICE " & vbCrLf &
            "      , SUM(d.AVERAGE_COST * (a.STOCK_CURRENT + b.STOCK_IN - 0)) AS TOTALPRICE " & vbCrLf &
            "    FROM " & vbCrLf &
            "      ( " & vbCrLf &
            "        SELECT ID, FORM_TYPE_NAME, ITEM_ID, STOCK_CURRENT " & vbCrLf &
            "        FROM " & vbCrLf &
            "          ( " & vbCrLf &
            "            SELECT ROW_NUMBER() OVER (PARTITION BY a.ITEM_ID ORDER BY a.ID, a.CREATED_TIME ASC) AS NO " & vbCrLf &
            "              , a.ITEM_ID " & vbCrLf &
            "              , a.STOCK_CURRENT " & vbCrLf &
            "              , e.ID " & vbCrLf &
            "              , e.FORM_TYPE_NAME " & vbCrLf &
            "            FROM TPROC_STOCKMOVEMENT a, TPROC_GM_HEADERS b, TPROC_PO_HEADERS c " & vbCrLf &
            "            , TPROC_PO_TYPE d, TPROC_FORM_TYPE e " & vbCrLf &
            "            WHERE 1=2" & vbCrLf &
            "            AND a.REFNO = b.GM_NUMBER " & vbCrLf &
            "            AND b.PO_ID = c.ID " & vbCrLf &
            "            AND c.PO_TYPE_ID = d.ID " & vbCrLf &
            "            AND d.FORM_TYPE_ID = e.ID " & vbCrLf &
            "          ) A " & vbCrLf &
            "        WHERE A.NO = 1 " & vbCrLf &
            "      ) a " & vbCrLf &
            "    LEFT JOIN " & vbCrLf &
            "      ( " & vbCrLf &
            "        SELECT a.ITEM_ID " & vbCrLf &
            "        , SUM(a.STOCK_IN) AS STOCK_IN " & vbCrLf &
            "        , e.ID " & vbCrLf &
            "        , e.FORM_TYPE_NAME " & vbCrLf &
            "        FROM TPROC_STOCKMOVEMENT a, TPROC_GM_HEADERS b, TPROC_PO_HEADERS c " & vbCrLf &
            "        , TPROC_PO_TYPE d, TPROC_FORM_TYPE e " & vbCrLf &
            "        WHERE 1=2" & vbCrLf &
            "        AND a.REFNO = b.GM_NUMBER " & vbCrLf &
            "        AND b.PO_ID = c.ID " & vbCrLf &
            "        AND c.PO_TYPE_ID = d.ID " & vbCrLf &
            "        AND d.FORM_TYPE_ID = e.ID " & vbCrLf &
            "        GROUP BY a.ITEM_ID, e.FORM_TYPE_NAME, e.ID " & vbCrLf &
            "      ) b ON b.ITEM_ID = a.ITEM_ID " & vbCrLf &
            "      AND b.ID = a.ID " & vbCrLf &
            "    LEFT JOIN TPROC_STOCK d ON d.ID = a.ITEM_ID " & vbCrLf &
            "    GROUP BY CUBE(a.FORM_TYPE_NAME, d.ITEM_DESCRIPTION) " & vbCrLf &
            "    ORDER BY a.FORM_TYPE_NAME ASC, d.ITEM_DESCRIPTION ASC " & vbCrLf &
            "  ) a " & vbCrLf &
            "WHERE COALESCE(a.FORM_TYPE_NAME, ' ') <> ' ' "
            dtTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)

            Return View("FormReportStockMovementMonthly")
        End Function

        Function ActionReportMonthly(ByVal _PeriodeFrom As String,
                                     ByVal _PeriodeTo As String) As ActionResult
            Try
                Dim Query As String =
                "SELECT * " & vbCrLf &
                "FROM " & vbCrLf &
                "  ( " & vbCrLf &
                "    SELECT a.FORM_TYPE_NAME " & vbCrLf &
                "      , d.ITEM_DESCRIPTION " & vbCrLf &
                "      , SUM(a.STOCK_CURRENT) AS STOCK_CURRENT " & vbCrLf &
                "      , SUM(b.STOCK_IN) AS STOCK_IN " & vbCrLf &
                "      , 0 AS STOCK_OUT " & vbCrLf &
                "      , SUM(a.STOCK_CURRENT + b.STOCK_IN - 0) AS BALANCE_STOCK " & vbCrLf &
                "      , SUM(d.AVERAGE_COST) AS PRICE " & vbCrLf &
                "      , SUM(d.AVERAGE_COST * (a.STOCK_CURRENT + b.STOCK_IN - 0)) AS TOTALPRICE " & vbCrLf &
                "    FROM " & vbCrLf &
                "      ( " & vbCrLf &
                "        SELECT ID, FORM_TYPE_NAME, ITEM_ID, STOCK_CURRENT " & vbCrLf &
                "        FROM " & vbCrLf &
                "          ( " & vbCrLf &
                "            SELECT ROW_NUMBER() OVER (PARTITION BY a.ITEM_ID ORDER BY a.ID, a.CREATED_TIME ASC) AS NO " & vbCrLf &
                "              , a.ITEM_ID " & vbCrLf &
                "              , a.STOCK_CURRENT " & vbCrLf &
                "              , e.ID " & vbCrLf &
                "              , e.FORM_TYPE_NAME " & vbCrLf &
                "            FROM TPROC_STOCKMOVEMENT a, TPROC_GM_HEADERS b, TPROC_PO_HEADERS c " & vbCrLf &
                "            , TPROC_PO_TYPE d, TPROC_FORM_TYPE e " & vbCrLf &
                IIf(_PeriodeFrom = "" OrElse _PeriodeTo = "", "WHERE 1=2",
                "           WHERE TRUNC(a.CREATED_TIME, 'DD') >= TO_DATE('" & _PeriodeFrom & "', 'DD-MM-YYYY') AND TRUNC(a.CREATED_TIME, 'DD') <= TO_DATE('" & _PeriodeTo & "', 'DD-MM-YYYY')") & vbCrLf &
                "            AND a.REFNO = b.GM_NUMBER " & vbCrLf &
                "            AND b.PO_ID = c.ID " & vbCrLf &
                "            AND c.PO_TYPE_ID = d.ID " & vbCrLf &
                "            AND d.FORM_TYPE_ID = e.ID " & vbCrLf &
                "          ) A " & vbCrLf &
                "        WHERE A.NO = 1 " & vbCrLf &
                "      ) a " & vbCrLf &
                "    LEFT JOIN " & vbCrLf &
                "      ( " & vbCrLf &
                "        SELECT a.ITEM_ID " & vbCrLf &
                "        , SUM(a.STOCK_IN) AS STOCK_IN " & vbCrLf &
                "        , e.ID " & vbCrLf &
                "        , e.FORM_TYPE_NAME " & vbCrLf &
                "        FROM TPROC_STOCKMOVEMENT a, TPROC_GM_HEADERS b, TPROC_PO_HEADERS c " & vbCrLf &
                "        , TPROC_PO_TYPE d, TPROC_FORM_TYPE e " & vbCrLf &
                IIf(_PeriodeFrom = "" OrElse _PeriodeTo = "", "WHERE 1=2",
                "           WHERE TRUNC(a.CREATED_TIME, 'DD') >= TO_DATE('" & _PeriodeFrom & "', 'DD-MM-YYYY') AND TRUNC(a.CREATED_TIME, 'DD') <= TO_DATE('" & _PeriodeTo & "', 'DD-MM-YYYY')") & vbCrLf &
                "        AND a.REFNO = b.GM_NUMBER " & vbCrLf &
                "        AND b.PO_ID = c.ID " & vbCrLf &
                "        AND c.PO_TYPE_ID = d.ID " & vbCrLf &
                "        AND d.FORM_TYPE_ID = e.ID " & vbCrLf &
                "        GROUP BY a.ITEM_ID, e.FORM_TYPE_NAME, e.ID " & vbCrLf &
                "      ) b ON b.ITEM_ID = a.ITEM_ID " & vbCrLf &
                "      AND b.ID = a.ID " & vbCrLf &
                "    LEFT JOIN TPROC_STOCK d ON d.ID = a.ITEM_ID " & vbCrLf &
                "    GROUP BY CUBE(a.FORM_TYPE_NAME, d.ITEM_DESCRIPTION) " & vbCrLf &
                "    ORDER BY a.FORM_TYPE_NAME ASC, d.ITEM_DESCRIPTION ASC " & vbCrLf &
                "  ) a " & vbCrLf &
                "WHERE COALESCE(a.FORM_TYPE_NAME, ' ') <> ' ' "
                dtTable = eProcurementApps.DataAccess.ConnectionDB.GetDataTable(Query)
            Catch ex As Exception
            End Try
            Return PartialView("FormReportStockMovementMonthly")
        End Function
    End Class
End Namespace
