Imports System.ComponentModel.DataAnnotations
Public Class ROLE_HELPER

    Public Property ROLE_NAME As String
    Public Property ROLE_DESCRIPTION As String
    Public Property IS_INACTIVE As Decimal
    Public Property DEFAULT_SELECTED As Decimal
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Decimal

    Public Overridable Property ROLE_MENU_HELPER As ICollection(Of ROLE_MENU_HELPER) = New HashSet(Of ROLE_MENU_HELPER)
End Class
