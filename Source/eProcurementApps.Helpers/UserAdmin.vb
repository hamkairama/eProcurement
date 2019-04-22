Imports System.Collections.Generic
Imports System.Web

Public Class UserAdmin
    Public Shared Function IsSuperAdmin() As Boolean
        Dim isValid As Boolean = False
        If HttpContext.Current.Session("IS_SUPER_ADMIN") = "1" Then
            isValid = True
        End If
        Return isValid
    End Function
End Class