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

Partial Public Class TPROC_PC_SP_GR
    Public Property ID As Integer
    Public Property PC_ID As Integer
    Public Property SUPP_ID As Integer
    Public Property SUPP_NM As String
    Public Property URL_ATTACHMENT As String
    Public Property IS_USED As Nullable(Of Integer)
    Public Property COL_NUM As Integer
    Public Property GRAND_TOTAL As Integer
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property DISCOUNT As Nullable(Of Integer)
    Public Property VAT As Nullable(Of Integer)
    Public Property PPH As Nullable(Of Integer)
    Public Property DISCOUNT_TEMP As Nullable(Of Integer)
    Public Property VAT_TEMP As Nullable(Of Integer)
    Public Property PPH_TEMP As Nullable(Of Integer)
    Public Property SUB_TOTAL As Nullable(Of Integer)
    Public Property DESCRIPTION As String

    Public Overridable Property TPROC_PC As TPROC_PC
    Public Overridable Property TPROC_PC_SP_DT As ICollection(Of TPROC_PC_SP_DT) = New HashSet(Of TPROC_PC_SP_DT)

End Class