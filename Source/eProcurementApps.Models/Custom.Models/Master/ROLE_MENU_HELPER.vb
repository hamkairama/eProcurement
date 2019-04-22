Imports System.ComponentModel.DataAnnotations
Public Class ROLE_MENU_HELPER

    <Key>
    <Display(Name:="Menu Role")>
    Public Property MENU_ID As String

    <Display(Name:="Role Id")>
    Public Property ROLE_ID As String

    <Display(Name:="Is Access")>
    Public Property IS_ACCESS As String

    <Display(Name:="Menu Name")>
    Public Property MENU_NAME As String

    <Display(Name:="Menu Description")>
    Public Property MENU_DESCRIPTION As String

    <Display(Name:="Menu Parent")>
    Public Property MENU_PARENT_ID As String
End Class
