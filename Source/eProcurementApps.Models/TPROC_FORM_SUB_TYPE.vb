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

Partial Public Class TPROC_FORM_SUB_TYPE
    Public Property ID As Integer
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property SUB_FORMTYPE_GR_ID As Integer

    Public Overridable Property TPROC_PR_HEADER As ICollection(Of TPROC_PR_HEADER) = New HashSet(Of TPROC_PR_HEADER)
    Public Overridable Property TPROC_FORM_SUBTYPE_GR As TPROC_FORM_SUBTYPE_GR

End Class
