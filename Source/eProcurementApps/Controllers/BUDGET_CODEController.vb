Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class BUDGET_CODEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU07")>
        Function Index() As ActionResult
            Dim bUDGET_CODE As New List(Of TPROC_BUDGET_CODE)
            bUDGET_CODE = db.TPROC_BUDGET_CODE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.TABLE_BUDGET).ToList()

            Return View(bUDGET_CODE)
        End Function

        <CAuthorize(Role:="MNU07")>
        Function List() As ActionResult
            Dim bUDGET_CODE As New List(Of TPROC_BUDGET_CODE)
            bUDGET_CODE = db.TPROC_BUDGET_CODE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.TABLE_BUDGET).ToList()

            Return PartialView("_List", bUDGET_CODE)
        End Function

        <CAuthorize(Role:="MNU07")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim bUDGET_CODE As TPROC_BUDGET_CODE = db.TPROC_BUDGET_CODE.Find(id)
            If IsNothing(bUDGET_CODE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", bUDGET_CODE)
        End Function

        <CAuthorize(Role:="MNU07")>
        Function Create() As ActionResult
            Return View("_Create")
        End Function

        <CAuthorize(Role:="MNU07")>
        Function ActionCreate(ByVal table_budget As String, ByVal table_budget_usage As String, ByVal cd_printing_usage As String, ByVal cd_printing_start As String,
        ByVal cd_printing_end As String, ByVal cd_office_supplie_usage As String, ByVal cd_office_supplie_start As String, ByVal cd_office_supplie_end As String,
        ByVal cd_asset_nonasset_usage As String, ByVal cd_asset_nonasset_start As String, ByVal cd_asset_nonasset_end As String,
        ByVal cd_promotional_item_usage As String, ByVal cd_promotional_item_start As String, ByVal cd_promotional_item_end As String,
        ByVal is_active As Decimal, ByVal cd_office_supplie_mt As String, ByVal cd_printing_mt As String, ByVal table_t1 As String, ByVal table_t2 As String,
        ByVal table_t3 As String, ByVal table_t5 As String, ByVal cd_issued_usage As String) As ActionResult
            Dim bUDGET_CODE As New TPROC_BUDGET_CODE
            bUDGET_CODE.TABLE_BUDGET = table_budget
            bUDGET_CODE.TABLE_BUDGET_USAGE = table_budget_usage
            bUDGET_CODE.CD_PRINTING_USAGE = cd_printing_usage
            bUDGET_CODE.CD_PRINTING_START = cd_printing_start
            bUDGET_CODE.CD_PRINTING_END = cd_printing_end
            bUDGET_CODE.CD_OFFICE_SUPPLIE_USAGE = cd_office_supplie_usage
            bUDGET_CODE.CD_OFFICE_SUPPLIE_START = cd_office_supplie_start
            bUDGET_CODE.CD_OFFICE_SUPPLIE_END = cd_office_supplie_end
            bUDGET_CODE.CD_ASSET_NONASSET_USAGE = cd_asset_nonasset_usage
            bUDGET_CODE.CD_ASSET_NONASSET_START = cd_asset_nonasset_start
            bUDGET_CODE.CD_ASSET_NONASSET_END = cd_asset_nonasset_end
            bUDGET_CODE.CD_PROMOTIONAL_ITEM_USAGE = cd_promotional_item_usage
            bUDGET_CODE.CD_PROMOTIONAL_ITEM_START = cd_promotional_item_start
            bUDGET_CODE.CD_PROMOTIONAL_ITEM_END = cd_promotional_item_end
            bUDGET_CODE.IS_ACTIVE = is_active
            bUDGET_CODE.CD_OFFICE_SUPPLIE_MT = cd_office_supplie_mt
            bUDGET_CODE.CD_PRINTING_MT = cd_printing_mt
            bUDGET_CODE.TABLE_T1 = table_t1
            bUDGET_CODE.TABLE_T2 = table_t2
            bUDGET_CODE.TABLE_T3 = table_t3
            bUDGET_CODE.TABLE_T5 = table_t5
            bUDGET_CODE.CD_ISSUED_USAGE = cd_issued_usage
            bUDGET_CODE.CREATED_TIME = Date.Now
            bUDGET_CODE.CREATED_BY = Session("USER_ID")
            db.TPROC_BUDGET_CODE.Add(bUDGET_CODE)
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU07")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim bUDGET_CODE As TPROC_BUDGET_CODE = db.TPROC_BUDGET_CODE.Find(id)
            If IsNothing(bUDGET_CODE) Then
                Return HttpNotFound()
            End If
            Return View("_Edit", bUDGET_CODE)
        End Function

        <CAuthorize(Role:="MNU07")>
        Function ActionEdit(ByVal id As Decimal, ByVal table_budget As String, ByVal table_budget_usage As String, ByVal cd_printing_usage As String,
        ByVal cd_printing_start As String, ByVal cd_printing_end As String, ByVal cd_office_supplie_usage As String, ByVal cd_office_supplie_start As String,
        ByVal cd_office_supplie_end As String,
        ByVal cd_asset_nonasset_usage As String, ByVal cd_asset_nonasset_start As String, ByVal cd_asset_nonasset_end As String,
        ByVal cd_promotional_item_usage As String, ByVal cd_promotional_item_start As String, ByVal cd_promotional_item_end As String,
        ByVal is_active As Decimal, ByVal cd_office_supplie_mt As String, ByVal cd_printing_mt As String, ByVal table_t1 As String, ByVal table_t2 As String,
        ByVal table_t3 As String, ByVal table_t5 As String, ByVal cd_issued_usage As String) As ActionResult
            Dim bUDGET_CODE As TPROC_BUDGET_CODE = db.TPROC_BUDGET_CODE.Find(id)
            bUDGET_CODE.TABLE_BUDGET = table_budget
            bUDGET_CODE.TABLE_BUDGET_USAGE = table_budget_usage
            bUDGET_CODE.CD_PRINTING_USAGE = cd_printing_usage
            bUDGET_CODE.CD_PRINTING_START = cd_printing_start
            bUDGET_CODE.CD_PRINTING_END = cd_printing_end
            bUDGET_CODE.CD_OFFICE_SUPPLIE_USAGE = cd_office_supplie_usage
            bUDGET_CODE.CD_OFFICE_SUPPLIE_START = cd_office_supplie_start
            bUDGET_CODE.CD_OFFICE_SUPPLIE_END = cd_office_supplie_end
            bUDGET_CODE.CD_ASSET_NONASSET_USAGE = cd_asset_nonasset_usage
            bUDGET_CODE.CD_ASSET_NONASSET_START = cd_asset_nonasset_start
            bUDGET_CODE.CD_ASSET_NONASSET_END = cd_asset_nonasset_end
            bUDGET_CODE.CD_PROMOTIONAL_ITEM_USAGE = cd_promotional_item_usage
            bUDGET_CODE.CD_PROMOTIONAL_ITEM_START = cd_promotional_item_start
            bUDGET_CODE.CD_PROMOTIONAL_ITEM_END = cd_promotional_item_end
            bUDGET_CODE.IS_ACTIVE = is_active
            bUDGET_CODE.CD_OFFICE_SUPPLIE_MT = cd_office_supplie_mt
            bUDGET_CODE.CD_PRINTING_MT = cd_printing_mt
            bUDGET_CODE.TABLE_T1 = table_t1
            bUDGET_CODE.TABLE_T2 = table_t2
            bUDGET_CODE.TABLE_T3 = table_t3
            bUDGET_CODE.TABLE_T5 = table_t5
            bUDGET_CODE.CD_ISSUED_USAGE = cd_issued_usage
            bUDGET_CODE.LAST_MODIFIED_TIME = Date.Now
            bUDGET_CODE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(bUDGET_CODE).State = EntityState.Modified
            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU07")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim bUDGET_CODE As TPROC_BUDGET_CODE = db.TPROC_BUDGET_CODE.Find(id)
            If IsNothing(bUDGET_CODE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", bUDGET_CODE)
        End Function

        <CAuthorize(Role:="MNU07")>
        Function ActionDelete(ByVal id As Decimal) As ActionResult
            Dim bUDGET_CODE As TPROC_BUDGET_CODE = db.TPROC_BUDGET_CODE.Find(id)
            bUDGET_CODE.ROW_STATUS = ListEnum.RowStat.InActive
            bUDGET_CODE.LAST_MODIFIED_TIME = Date.Now
            bUDGET_CODE.LAST_MODIFIED_BY = Session("USER_ID")
            db.Entry(bUDGET_CODE).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
