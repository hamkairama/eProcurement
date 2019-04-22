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

Partial Public Class TPROC_PR_HEADER
    Public Property ID As Integer
    Public Property PR_NO As String
    Public Property USER_ID_ID As Integer
    Public Property PR_DATE As Date
    Public Property FORM_TYPE_ID As Integer
    Public Property SUB_TYPE_ID As Integer
    Public Property GOOD_TYPE_ID As Integer
    Public Property DELIVERY_DAYS As Integer
    Public Property EXP_DEV_DT As Date
    Public Property PR_INDICATOR As String
    Public Property ACCOUNT_CODE As String
    Public Property PR_STATUS As Integer
    Public Property SUB_TOTAL As Integer
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property COMP_CD As Integer
    Public Property FOR_STORAGE As Nullable(Of Integer)
    Public Property REJECT_REASON As String

    Public Overridable Property TPROC_FORM_SUB_TYPE As TPROC_FORM_SUB_TYPE
    Public Overridable Property TPROC_FORM_TYPE As TPROC_FORM_TYPE
    Public Overridable Property TPROC_GOOD_TYPE As TPROC_GOOD_TYPE
    Public Overridable Property TPROC_PR_APPR_RELDEPT_GR As ICollection(Of TPROC_PR_APPR_RELDEPT_GR) = New HashSet(Of TPROC_PR_APPR_RELDEPT_GR)
    Public Overridable Property TPROC_PR_ATTACHMENT As ICollection(Of TPROC_PR_ATTACHMENT) = New HashSet(Of TPROC_PR_ATTACHMENT)
    Public Overridable Property TPROC_PR_DETAIL As ICollection(Of TPROC_PR_DETAIL) = New HashSet(Of TPROC_PR_DETAIL)
    Public Overridable Property TPROC_PR_HISTORICAL As ICollection(Of TPROC_PR_HISTORICAL) = New HashSet(Of TPROC_PR_HISTORICAL)
    Public Overridable Property TPROC_USER As TPROC_USER

End Class
