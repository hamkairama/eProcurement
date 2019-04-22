Imports System.Web

Public Class CurrentUser
    Public Shared Function GetCurrentUserId() As String
        Dim result As String = HttpContext.Current.Session("USER_ID")

        Return result
    End Function

    Public Shared Function GetCurrentPassword() As String
        Dim result As String = HttpContext.Current.Session("PASSWORD")

        Return result
    End Function

    Public Shared Function GetCurrentUserName() As String
        Dim result As String = HttpContext.Current.Session("USER_NAME")

        Return result
    End Function

    Public Shared Function GetCurrentUserEmail() As String
        Dim result As String = HttpContext.Current.Session("USER_MAIL")

        Return result
    End Function
End Class
