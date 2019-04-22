Imports System.ComponentModel.DataAnnotations
Public Class WA_HELPER

    Public Property WA_NUMBER As String
    Public Property DEPARTMENT_NAME As String
    Public Property DIVISION_ID As Decimal
    Public Property BUDGET_CHECKING As String
    Public Overridable Property ROLE_MENU_HELPER As ICollection(Of APPROVAL_DETAIL_HELPER) = New HashSet(Of APPROVAL_DETAIL_HELPER)
End Class
