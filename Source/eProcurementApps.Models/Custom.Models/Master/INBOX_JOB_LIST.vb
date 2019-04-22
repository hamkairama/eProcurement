Imports System.ComponentModel.DataAnnotations
Public Class INBOX_JOB_LIST
    <Key>
    Public Property JOB_ID As Decimal
    Public Property REQUEST_NO As String
    Public Property REQUEST_BY As String
    Public Property REQUEST_DATE As DateTime

    'MENU REQUEST
    Public Property CONTROL As String
    Public Property RELATION_FLAG As String
    Public Property ACTION As String
    Public Property STATUS As String
End Class
