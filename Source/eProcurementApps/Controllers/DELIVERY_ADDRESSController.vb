Imports System.Data.Entity
Imports System.Net
Imports System.Transactions
Imports eProcurementApps.Helpers
Imports eProcurementApps.Models

Namespace Controllers
    Public Class DELIVERY_ADDRESSController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU14")>
        Function Index() As ActionResult
            Dim dELIVERY_ADDRESS As New List(Of TPROC_DELIVERY_ADDRESS)
            dELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.DELIVERY_NAME).ToList()

            Return View(dELIVERY_ADDRESS)
        End Function

        <CAuthorize(Role:="MNU14")>
        Function List() As ActionResult
            Dim dELIVERY_ADDRESS As New List(Of TPROC_DELIVERY_ADDRESS)
            dELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.DELIVERY_NAME).ToList()

            Return PartialView("_List", dELIVERY_ADDRESS)
        End Function

        <CAuthorize(Role:="MNU14")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Find(id)
            If IsNothing(dELIVERY_ADDRESS) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Details", dELIVERY_ADDRESS)
        End Function

        <CAuthorize(Role:="MNU14")>
        Function Create() As ActionResult
            Return PartialView("_Create")
        End Function

        <CAuthorize(Role:="MNU14")>
        Function ActionCreate(ByVal delivery_name As String, ByVal delivery_address1 As String, ByVal delivery_phone As String, ByVal delivery_fax As String, ByVal default_indicator As Integer) As ActionResult
            Dim dELIVERY_ADDRESS As New TPROC_DELIVERY_ADDRESS
            Dim rs As New ResultStatus

            dELIVERY_ADDRESS.DELIVERY_NAME = delivery_name
            dELIVERY_ADDRESS.DELIVERY_ADDRESS = delivery_address1
            dELIVERY_ADDRESS.DELIVERY_PHONE = delivery_phone
            dELIVERY_ADDRESS.DELIVERY_FAX = delivery_fax
            dELIVERY_ADDRESS.DEFAULT_IND = default_indicator
            dELIVERY_ADDRESS.CREATED_TIME = Date.Now
            dELIVERY_ADDRESS.CREATED_BY = Session("USER_ID")


            Using scope As New TransactionScope()
                Try
                    rs = UpdateDeliveryAddresInd(default_indicator)
                    If rs.IsSuccess Then
                        rs = SaveActionCreate(dELIVERY_ADDRESS)
                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using


            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU14")>
        Function SaveActionCreate(dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS) As ResultStatus
            Dim rs As New ResultStatus
            Try
                db.TPROC_DELIVERY_ADDRESS.Add(dELIVERY_ADDRESS)
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function


        <CAuthorize(Role:="MNU14")>
        Private Function UpdateDeliveryAddresInd(default_ind_in As Integer) As ResultStatus
            Dim rs As New ResultStatus
            rs.SetSuccessStatus()

            Try
                If default_ind_in = 1 Then
                    Using db2 As New eProcurementEntities
                        Dim lDelivery_address = db2.TPROC_DELIVERY_ADDRESS.Where(Function(x) x.DEFAULT_IND = 1).ToList()
                        If lDelivery_address IsNot Nothing Then
                            For Each item In lDelivery_address
                                Dim dev_address As New TPROC_DELIVERY_ADDRESS
                                dev_address = item
                                dev_address.DEFAULT_IND = 0
                                db2.Entry(dev_address).State = EntityState.Modified
                                db2.SaveChanges()
                            Next

                            rs.SetSuccessStatus()
                        End If
                    End Using
                End If

            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function


        <CAuthorize(Role:="MNU14")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Find(id)
            If IsNothing(dELIVERY_ADDRESS) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Edit", dELIVERY_ADDRESS)
        End Function

        <CAuthorize(Role:="MNU14")>
        Function ActionEdit(ByVal id As Decimal, ByVal delivery_name As String, ByVal delivery_address1 As String, ByVal delivery_phone As String, ByVal delivery_fax As String, ByVal default_indicator As Integer) As ActionResult
            Dim dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Find(id)
            Dim rs As New ResultStatus

            dELIVERY_ADDRESS.DELIVERY_NAME = delivery_name
            dELIVERY_ADDRESS.DELIVERY_ADDRESS = delivery_address1
            dELIVERY_ADDRESS.DELIVERY_PHONE = delivery_phone
            dELIVERY_ADDRESS.DELIVERY_FAX = delivery_fax
            dELIVERY_ADDRESS.DEFAULT_IND = default_indicator
            dELIVERY_ADDRESS.LAST_MODIFIED_TIME = Date.Now
            dELIVERY_ADDRESS.LAST_MODIFIED_BY = Session("USER_ID")

            Using scope As New TransactionScope()
                Try
                    rs = UpdateDeliveryAddresInd(default_indicator)
                    If rs.IsSuccess Then
                        rs = SaveActionEdit(dELIVERY_ADDRESS)
                        If rs.IsSuccess Then
                            scope.Complete()
                            rs.SetSuccessStatus()
                        End If
                    End If
                Catch ex As Exception
                    rs.SetErrorStatus(ex.Message)
                End Try
            End Using


            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU14")>
        Function SaveActionEdit(dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS) As ResultStatus
            Dim rs As New ResultStatus
            Try
                db.Entry(dELIVERY_ADDRESS).State = EntityState.Modified
                db.SaveChanges()
                rs.SetSuccessStatus()
            Catch ex As Exception
                rs.SetErrorStatus(ex.Message)
            End Try

            Return rs
        End Function

        <CAuthorize(Role:="MNU14")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Find(id)
            If IsNothing(dELIVERY_ADDRESS) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", dELIVERY_ADDRESS)
        End Function

        <CAuthorize(Role:="MNU14")>
        Function ActionDelete(ByVal id As Decimal) As String
            Dim result As String = ""
            Try
                Dim dELIVERY_ADDRESS As TPROC_DELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Find(id)
                dELIVERY_ADDRESS.ROW_STATUS = ListEnum.RowStat.InActive
                dELIVERY_ADDRESS.LAST_MODIFIED_TIME = Date.Now
                dELIVERY_ADDRESS.LAST_MODIFIED_BY = Session("USER_ID")
                db.Entry(dELIVERY_ADDRESS).State = EntityState.Modified
                db.SaveChanges()
                result = "1"
            Catch ex As Exception
                result = "0"
            End Try

            Return result
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        <CAuthorize(Role:="MNU14")>
        Function CheckData(ByVal id As Decimal, ByVal delivery_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim dELIVERY_ADDRESS As New TPROC_DELIVERY_ADDRESS
            'check create
            If id = 0 Then
                dELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Where(Function(x) x.DELIVERY_NAME.ToUpper() = delivery_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If dELIVERY_ADDRESS IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                dELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Where(Function(x) x.DELIVERY_NAME.ToUpper() = delivery_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If dELIVERY_ADDRESS IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function


        <CAuthorize(Role:="MNU14")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_DELIVERY_ADDRESS.Where(Function(x) x.DELIVERY_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU14")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_DELIVERY_ADDRESS = db.TPROC_DELIVERY_ADDRESS.Find(id)
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
