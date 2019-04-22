Imports System.Web

Public Class UserAccess
    Public Shared Function IsInRole(role As String) As Boolean
        Dim isValid As Boolean = False
        If HttpContext.Current.Session.Count > 0 Then
            If HttpContext.Current.Session("IS_SUPER_ADMIN") = "1" Then
                isValid = True
            ElseIf HttpContext.Current.Session("IS_SUPER_ADMIN") = "0" Then
                Dim menus As List(Of String) = HttpContext.Current.Session("MY_ACCESS")
                If menus.Where(Function(a) a = role).Count() > 0 Then
                    isValid = True
                End If
            Else
                Dim menus As String() = New String() {"MNU21", "MNU22", "MNU24", "MNU25", "MNU26", "MNU39", "MNU49", "MNU50", "MNU51", "MNU52", "MNU53", "MNU54", "MNU55", "MNU40"}
                If menus.Where(Function(a) a = role).Count() > 0 Then
                    isValid = True
                End If
            End If
        Else
            isValid = False
        End If

        Return isValid
    End Function

End Class
