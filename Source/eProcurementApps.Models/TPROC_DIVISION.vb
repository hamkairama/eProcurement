'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class TPROC_DIVISION
    Public Property ID As Integer
    Public Property DIVISION_NAME As String
    Public Property DIVISION_DESCRIPTION As String
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer

    Public Overridable Property TPROC_APPROVAL_GR As ICollection(Of TPROC_APPROVAL_GR) = New HashSet(Of TPROC_APPROVAL_GR)
    Public Overridable Property TPROC_USER_DT As ICollection(Of TPROC_USER_DT) = New HashSet(Of TPROC_USER_DT)

End Class