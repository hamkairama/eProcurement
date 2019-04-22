Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Facade
Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class ROLEController
        Inherits System.Web.Mvc.Controller

        Private db As New eProcurementEntities

        <CAuthorize(Role:="MNU16")>
        Function Index() As ActionResult
            Dim rOLE As New List(Of TPROC_ROLE)
            rOLE = db.TPROC_ROLE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.ROLE_NAME).ToList()

            Return View(rOLE)
        End Function

        <CAuthorize(Role:="MNU16")>
        Function List() As ActionResult
            Dim rOLE As New List(Of TPROC_ROLE)
            rOLE = db.TPROC_ROLE.Where(Function(y) y.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(x) x.ROLE_NAME).ToList()

            Return PartialView("_List", rOLE)
        End Function

        <CAuthorize(Role:="MNU16")>
        Function Details(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim rOLE As TPROC_ROLE = db.TPROC_ROLE.Find(id)
            If IsNothing(rOLE) Then
                Return HttpNotFound()
            End If
            Return View("_Details", rOLE)
        End Function

        <CAuthorize(Role:="MNU16")>
        Function Create() As ActionResult
            Dim result As ROLE_HELPER = RoleFacade.newObject()
            Return View("_Create", result)
        End Function

        <CAuthorize(Role:="MNU16")>
        Function ActionCreate(ByVal role_name As String, ByVal role_description As String, ByVal is_inactive As Decimal, ByVal menus As Integer(), ByVal default_selected As Decimal) As ActionResult
            Dim roleHelp As New ROLE_HELPER
            Dim mainMenu As New List(Of TPROC_MENU)
            Dim newMenuItem As New List(Of ROLE_MENU_HELPER)

            Using db As New eProcurementEntities
                mainMenu = db.TPROC_MENU.ToList()
            End Using

            For Each mnu In mainMenu
                Dim item As New ROLE_MENU_HELPER
                item.MENU_ID = mnu.ID
                If mnu.MENU_PARENT_ID Is Nothing Then
                    item.MENU_PARENT_ID = 0
                Else
                    item.MENU_PARENT_ID = mnu.MENU_PARENT_ID
                End If

                item.MENU_NAME = mnu.MENU_NAME
                item.MENU_DESCRIPTION = mnu.MENU_DESCRIPTION
                If menus.Contains(mnu.ID) Then
                    item.IS_ACCESS = 1
                End If
                newMenuItem.Add(item)
            Next

            roleHelp.ROLE_MENU_HELPER = newMenuItem
            roleHelp.ROLE_NAME = role_name
            roleHelp.IS_INACTIVE = is_inactive
            roleHelp.DEFAULT_SELECTED = default_selected
            roleHelp.ROLE_DESCRIPTION = role_description

            If RoleFacade.Create(roleHelp) Then
                TempData("message") = "Data role who was named " + roleHelp.ROLE_NAME + " has successfully saved"
                Return RedirectToAction("Index")
            End If

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU16")>
        Function Edit(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim rOLE As TPROC_ROLE = db.TPROC_ROLE.Find(id)
            If IsNothing(rOLE) Then
                Return HttpNotFound()
            End If
            Return View("_Edit", rOLE)
        End Function

        <CAuthorize(Role:="MNU16")>
        Function ActionEdit(ByVal id As Decimal, ByVal role_name As String, ByVal role_description As String, ByVal is_inactive As Decimal, ByVal menus As Integer(), ByVal default_selected As Decimal) As ActionResult
            Dim role As New TPROC_ROLE

            role.ROLE_NAME = role_name
            role.IS_ACTIVE = is_inactive
            role.DEFAULT_SELECTED = default_selected
            role.ROLE_DESCRIPTION = role_description
            role.LAST_MODIFIED_BY = Session("USER_ID")
            role.LAST_MODIFIED_TIME = DateTime.Now

            If RoleFacade.Edit(id, role, menus) Then
                TempData("message") = "Data role who was named " + role.ROLE_NAME + " has successfully edited"
                Return RedirectToAction("Index")
            End If

            Return RedirectToAction("Index")
        End Function

        <CAuthorize(Role:="MNU16")>
        Function Delete(ByVal id As Decimal) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim rOLE As TPROC_ROLE = db.TPROC_ROLE.Find(id)
            If IsNothing(rOLE) Then
                Return HttpNotFound()
            End If
            Return PartialView("_Delete", rOLE)
        End Function

        <CAuthorize(Role:="MNU16")>
        Function ActionDelete(ByVal id As Decimal) As String
            Dim result As String = ""
            Try
                Dim role = db.TPROC_ROLE.Find(id)
                If role.TPROC_USER_DT.Count > 0 Then
                    result = "0"
                Else
                    'RoleFacade.Detele(id)
                    role.ROW_STATUS = ListEnum.RowStat.InActive
                    role.LAST_MODIFIED_TIME = Date.Now
                    role.LAST_MODIFIED_BY = Session("USER_ID")
                    db.Entry(role).State = EntityState.Modified
                    db.SaveChanges()
                    result = "1"
                End If
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

        <CAuthorize(Role:="MNU16")>
        Function CheckData(ByVal id As Decimal, ByVal role_name As String) As Integer
            Dim result As Integer = 0
            Dim db As New eProcurementEntities
            Dim rOLE As New TPROC_ROLE
            'check create
            If id = 0 Then
                rOLE = db.TPROC_ROLE.Where(Function(x) x.ROLE_NAME.ToUpper() = role_name.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If rOLE IsNot Nothing Then
                    result = 1
                End If
            Else
                'chek edit
                rOLE = db.TPROC_ROLE.Where(Function(x) x.ROLE_NAME.ToUpper() = role_name.ToUpper() And x.ID <> id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                If rOLE IsNot Nothing Then
                    result = 1
                End If
            End If

            Return result
        End Function

        <CAuthorize(Role:="MNU16")>
        Function IsInActive(ByVal value As String) As Decimal
            Dim id As Decimal
            Dim db As New eProcurementEntities

            Dim obj = db.TPROC_ROLE.Where(Function(x) x.ROLE_NAME.ToUpper() = value.ToUpper() And x.ROW_STATUS = ListEnum.RowStat.InActive).FirstOrDefault()

            If obj IsNot Nothing Then
                id = obj.ID
            Else
                id = 0
            End If

            Return id
        End Function

        <CAuthorize(Role:="MNU16")>
        Function ActionActiviting(ByVal id As Decimal) As ActionResult
            Try
                Dim obj As TPROC_ROLE = db.TPROC_ROLE.Find(id)
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
