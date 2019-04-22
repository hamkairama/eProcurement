Imports System.Web.Mvc
Imports eProcurementApps.Models
Imports eProcurementApps.Facade
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class ErrorController
        Inherits Controller

        'The 404 actin handler
        ' GET: Error FailWhale
        Function FailWhale() As ActionResult
            Response.StatusCode = 404
            Response.TrySkipIisCustomErrors = True
            Return View()
        End Function

        Function Maintaince() As ActionResult
            Return PartialView("_Maintaince")
        End Function

    End Class
End Namespace