Imports System.ComponentModel.DataAnnotations
Public Class LOGIN_MODEL
    <Required>
    <Display(Name:="user Id")>
    Public Property USER_ID As String
    <Required>
    <DataType(DataType.Password)>
    <Display(Name:="Password")>
    Public Property PASSWORD As String
    <Display(Name:="Remember Me ?")>
    Public Property REMEMBERME As String

End Class
