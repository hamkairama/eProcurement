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

Partial Public Class TPROC_PR_APPR_RELDEPT_GR
    Public Property ID As Integer
    Public Property PR_HEADER_ID As Integer
    Public Property RELDEPT_NAME As String
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property RELDEPT_GR_STATUS As String
    Public Property REJECT_REASON As String

    Public Overridable Property TPROC_PR_APPR_RELDEPT_DT As ICollection(Of TPROC_PR_APPR_RELDEPT_DT) = New HashSet(Of TPROC_PR_APPR_RELDEPT_DT)
    Public Overridable Property TPROC_PR_HEADER As TPROC_PR_HEADER

End Class
