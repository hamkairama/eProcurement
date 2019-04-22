Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Class SuperAdmin
    Inherits AuthorizeAttribute
    Protected Overrides Function AuthorizeCore(httpContext__1 As HttpContextBase) As Boolean
        Dim isValid As Boolean = False
        If HttpContext.Current.Session("USER_ID") Is Nothing OrElse HttpContext.Current.Session("IS_SUPER_ADMIN") Is Nothing Then
            isValid = False
        Else
            If TryCast(HttpContext.Current.Session("IS_SUPER_ADMIN"), String) = "1" Then
                isValid = True
            End If
        End If
        Return isValid
    End Function

    Protected Overrides Sub HandleUnauthorizedRequest(filterContext As AuthorizationContext)
        filterContext.Result = New RedirectToRouteResult(New RouteValueDictionary(New With {
            Key .controller = "Login",
            Key .action = "LogOff"
        }))
    End Sub
End Class