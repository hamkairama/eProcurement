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

Partial Public Class TPROC_GOOD_TYPE
    Public Property ID As Integer
    Public Property GOOD_TYPE_NAME As String
    Public Property GOOD_TYPE_DESCRIPTION As String
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer

    Public Overridable Property TPROC_STOCK As ICollection(Of TPROC_STOCK) = New HashSet(Of TPROC_STOCK)
    Public Overridable Property TPROC_PR_HEADER As ICollection(Of TPROC_PR_HEADER) = New HashSet(Of TPROC_PR_HEADER)

End Class