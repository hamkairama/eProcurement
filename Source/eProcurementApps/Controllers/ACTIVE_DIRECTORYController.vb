Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class ACTIVE_DIRECTORYController
        Inherits System.Web.Mvc.Controller
        Function Index() As ActionResult
            Dim user As New List(Of USER_HELPER)
            user = ActiveDirectory.GetActiveDir("")

            Return PartialView("_Index", user)
        End Function

        Function FindByUserId(user_id As String) As ActionResult
            Dim user As New List(Of USER_HELPER)
            user = ActiveDirectory.GetActiveDir(user_id)

            Return PartialView("_List", user)
        End Function

        Function FindByEmail(email As String) As ActionResult
            Dim user As New List(Of USER_HELPER)
            user = ActiveDirectory.GetActiveDirByEmail(email)

            Return PartialView("_List", user)
        End Function


    End Class
End Namespace
