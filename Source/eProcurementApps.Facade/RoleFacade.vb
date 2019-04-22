Imports eProcurementApps.Models
Imports System.Transactions
Imports eProcurementApps.Helpers


Public Class RoleFacade
    Public Shared Function newObject() As ROLE_HELPER
        Dim result As New ROLE_HELPER()
        Dim roleItem As New List(Of ROLE_MENU_HELPER)()
        Dim listMenu As New List(Of TPROC_MENU)()
        Using db As New eProcurementEntities()
            listMenu = db.TPROC_MENU.ToList()
        End Using
        For Each item As TPROC_MENU In listMenu
            Dim rm As New ROLE_MENU_HELPER()
            rm.MENU_ID = item.ID
            rm.MENU_NAME = item.MENU_NAME
            rm.MENU_DESCRIPTION = item.MENU_DESCRIPTION
            If item.MENU_PARENT_ID Is Nothing Then
                rm.MENU_PARENT_ID = 0
            Else
                rm.MENU_PARENT_ID = item.MENU_PARENT_ID
            End If


            roleItem.Add(rm)
        Next
        result.ROLE_MENU_HELPER = roleItem
        Return result
    End Function


    Public Shared Function Create2(newRole As ROLE_HELPER) As Boolean
        Dim status As Boolean = False
        newRole.CREATED_BY = CurrentUser.GetCurrentUserId()
        newRole.CREATED_TIME = DateTime.Now
        Using db As New eProcurementEntities()
            Dim Role As New TPROC_ROLE()
            Role.ROLE_NAME = newRole.ROLE_NAME
            Role.ROLE_DESCRIPTION = newRole.ROLE_DESCRIPTION
            Role.IS_ACTIVE = newRole.IS_INACTIVE
            Role.CREATED_BY = newRole.CREATED_BY
            Role.CREATED_TIME = newRole.CREATED_TIME
            db.TPROC_ROLE.Add(Role)
            db.SaveChanges()

            Dim role_id As Integer = db.TPROC_ROLE.Where(Function(x) x.ROLE_NAME = newRole.ROLE_NAME).FirstOrDefault().ID
            For Each item As ROLE_MENU_HELPER In newRole.ROLE_MENU_HELPER
                Dim newRoleItem As New TPROC_ROLE_MENU()
                newRoleItem.ROLE_ID = role_id
                newRoleItem.MENU_ID = item.MENU_ID
                newRoleItem.IS_ACCESS = item.IS_ACCESS
                newRoleItem.CREATED_BY = newRole.CREATED_BY
                newRoleItem.CREATED_TIME = newRole.CREATED_TIME

                db.TPROC_ROLE_MENU.Add(newRoleItem)
                db.SaveChanges()
            Next
            Try
                status = True
            Catch generatedExceptionName As Exception
                status = False
            End Try
        End Using
        Return status
    End Function

    Public Shared Function Create(newRole As ROLE_HELPER) As Boolean
        Dim status As Boolean = False
        newRole.CREATED_BY = CurrentUser.GetCurrentUserId()
        newRole.CREATED_TIME = DateTime.Now
        Dim role_id As Integer

        Using scope As New TransactionScope()
            Try
                Using db As New eProcurementEntities()
                    Dim TPROC_ROLE As New TPROC_ROLE()
                    TPROC_ROLE.ROLE_NAME = newRole.ROLE_NAME
                    TPROC_ROLE.ROLE_DESCRIPTION = newRole.ROLE_DESCRIPTION
                    TPROC_ROLE.IS_ACTIVE = newRole.IS_INACTIVE
                    TPROC_ROLE.DEFAULT_SELECTED = newRole.DEFAULT_SELECTED
                    TPROC_ROLE.CREATED_BY = newRole.CREATED_BY
                    TPROC_ROLE.CREATED_TIME = newRole.CREATED_TIME
                    db.TPROC_ROLE.Add(TPROC_ROLE)
                    db.SaveChanges()
                End Using

                role_id = GetRoleId(newRole.ROLE_NAME)

                For Each item As ROLE_MENU_HELPER In newRole.ROLE_MENU_HELPER
                    Dim newRoleItem As New TPROC_ROLE_MENU()
                    newRoleItem.ROLE_ID = role_id
                    newRoleItem.MENU_ID = item.MENU_ID
                    newRoleItem.IS_ACCESS = item.IS_ACCESS
                    newRoleItem.CREATED_BY = newRole.CREATED_BY
                    newRoleItem.CREATED_TIME = newRole.CREATED_TIME

                    Using db2 As New eProcurementEntities()
                        db2.TPROC_ROLE_MENU.Add(newRoleItem)
                        db2.SaveChanges()
                    End Using
                Next

                If newRole.DEFAULT_SELECTED = 1 Then
                    UpdateDefaultAllRoleToZero(newRole.ROLE_NAME)
                End If

                scope.Complete()
                status = True
            Catch ex As Exception
                status = False
            End Try
        End Using

        Return status
    End Function

    Private Shared Sub UpdateDefaultAllRoleToZero(role_name As String)
        Dim rs As New ResultStatus

        Try
            Using dbx As New eProcurementEntities
                Dim roles = dbx.TPROC_ROLE.Where(Function(x) x.ROLE_NAME <> role_name).ToList()
                For Each item In roles
                    item.DEFAULT_SELECTED = 0
                    dbx.Entry(item).State = EntityState.Modified
                    dbx.SaveChanges()
                Next
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

    End Sub

    Private Shared Function GetRoleId(role_name As String) As Integer
        Dim db As New eProcurementEntities
        Return db.TPROC_ROLE.Where(Function(x) x.ROLE_NAME = role_name).FirstOrDefault().ID
    End Function

    Public Shared Function Detele(roleId As Decimal) As Boolean
        Dim result As Boolean
        Dim db As New eProcurementEntities

        Using scope As New TransactionScope()
            Try
                Dim lRole_menu As List(Of TPROC_ROLE_MENU) = db.TPROC_ROLE_MENU.Where(Function(x) x.ROLE_ID = roleId).ToList()
                For Each item As TPROC_ROLE_MENU In lRole_menu
                    db.TPROC_ROLE_MENU.Remove(item)
                Next

                db.TPROC_ROLE.Remove(db.TPROC_ROLE.Find(roleId))
                'db.lTPROC_ROLE_MENU.Remove(lRole_menu)
                db.SaveChanges()

                scope.Complete()
                result = True
            Catch ex As Exception
                result = False
            End Try
        End Using

        Return result
    End Function


    Public Shared Function Edit(id As Decimal, newRole As TPROC_ROLE, ByVal menus As Integer()) As Boolean
        Dim status As Boolean = False


        Using scope As New TransactionScope()
            Try
                'update role
                Using db As New eProcurementEntities()
                    Dim TPROC_ROLE = db.TPROC_ROLE.Find(id)
                    TPROC_ROLE.ROLE_NAME = newRole.ROLE_NAME
                    TPROC_ROLE.ROLE_DESCRIPTION = newRole.ROLE_DESCRIPTION
                    TPROC_ROLE.IS_ACTIVE = newRole.IS_ACTIVE
                    TPROC_ROLE.DEFAULT_SELECTED = newRole.DEFAULT_SELECTED
                    TPROC_ROLE.LAST_MODIFIED_BY = newRole.LAST_MODIFIED_BY
                    TPROC_ROLE.LAST_MODIFIED_TIME = newRole.LAST_MODIFIED_TIME
                    db.Entry(TPROC_ROLE).State = EntityState.Modified
                    db.SaveChanges()
                End Using

                'get all role menu base on role_id
                Using db2 As New eProcurementEntities
                    Dim TPROC_ROLE_MENU = db2.TPROC_ROLE_MENU.Where(Function(x) x.ROLE_ID = id).ToList()
                    If TPROC_ROLE_MENU.Count > 0 Then
                        For Each item In TPROC_ROLE_MENU
                            If menus.Contains(item.MENU_ID) Then
                                item.IS_ACCESS = 1
                            Else
                                item.IS_ACCESS = 0
                            End If

                            db2.Entry(item).State = EntityState.Modified
                            db2.SaveChanges()
                        Next
                    End If
                End Using

                If newRole.DEFAULT_SELECTED = 1 Then
                    UpdateDefaultAllRoleToZero(newRole.ROLE_NAME)
                End If

                scope.Complete()
                status = True
            Catch ex As Exception
                status = False
            End Try
        End Using

        Return status
    End Function

End Class
