Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Class CAuthorize
    Inherits AuthorizeAttribute

    Public Property Role() As String
        Get
            Return m_Role
        End Get
        Set
            m_Role = Value
        End Set
    End Property
    Private m_Role As String

    Protected Overrides Function AuthorizeCore(httpContext__1 As HttpContextBase) As Boolean
        Dim isValid As Boolean = False
        If HttpContext.Current.Session.Count > 0 Then
            If HttpContext.Current.Session("IS_SUPER_ADMIN") = "1" Then
                isValid = True
            ElseIf HttpContext.Current.Session("IS_SUPER_ADMIN") = "0" Then

                Dim menus As List(Of String) = HttpContext.Current.Session("MY_ACCESS")
                menus.Add("MNU28") 'MNU28 : PO CREATE
                menus.Add("MNU48") 'MNU28 : PO LIST
                menus.Add("MNU60") 'MNU60 : PC
                If menus.Where(Function(a) a = Role).Count() > 0 Then
                    isValid = True
                End If
            Else
                Dim menus As String() = New String() {"MNU21", "MNU22", "MNU24", "MNU25", "MNU26", "MNU39", "MNU49", "MNU50", "MNU51", "MNU52", "MNU19", "MNU53", "MNU05", "MNU54", "MNU23", "MNU55", "MNU10", "MNU40", "MNU17", "MNU28", "MNU48", "MNU60"}
                If menus.Where(Function(a) a = Role).Count() > 0 Then
                    isValid = True
                End If
            End If
        Else
            isValid = False
        End If

        Return isValid
    End Function

    Protected Overrides Sub HandleUnauthorizedRequest(ByVal filterContext As AuthorizationContext)
        filterContext.Result = New RedirectToRouteResult(New RouteValueDictionary(New With {.controller = "Login", .action = "LogOff", .id = UrlParameter.Optional}))
    End Sub
End Class
