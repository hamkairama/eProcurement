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

Partial Public Class TPROC_APPROVAL_DT
    Public Property ID As Integer
    Public Property APPROVAL_GROUP_ID As Integer
    Public Property APPROVAL_NAME As String
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property FLOW_NUMBER As Integer
    Public Property LEVEL_ID As Nullable(Of Integer)
    Public Property EMAIL As String
    Public Property USER_NAME As String

    Public Overridable Property TPROC_APPROVAL_GR As TPROC_APPROVAL_GR
    Public Overridable Property TPROC_LEVEL As TPROC_LEVEL

End Class
