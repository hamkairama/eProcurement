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

Partial Public Class TPROC_PO_TYPE
    Public Property ID As Integer
    Public Property PO_TYPE_NAME As String
    Public Property PO_TYPE_DESCRIPTION As String
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property FORM_TYPE_ID As Integer

    Public Overridable Property TPROC_FORM_TYPE As TPROC_FORM_TYPE
    Public Overridable Property TPROC_PO_HEADERS As ICollection(Of TPROC_PO_HEADERS) = New HashSet(Of TPROC_PO_HEADERS)

End Class
