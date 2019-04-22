Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports eProcurementApps.Models

Public Class AuthorizeLogin
    Inherits AuthorizeAttribute

    Protected Overrides Function AuthorizeCore(ByVal httpContext As HttpContextBase) As Boolean
        Dim isValid As Boolean = False
        If httpContext.Session.Keys.Count > 0 Then
            isValid = True
        End If

        Return isValid
    End Function

    Protected Overrides Sub HandleUnauthorizedRequest(ByVal filterContext As AuthorizationContext)
        filterContext.Result = New RedirectToRouteResult(New RouteValueDictionary(New With {.controller = "Login", .action = "LogOff", .id = UrlParameter.Optional}))
    End Sub
End Class
