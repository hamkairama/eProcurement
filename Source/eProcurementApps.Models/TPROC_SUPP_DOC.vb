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

Partial Public Class TPROC_SUPP_DOC
    Public Property ID As Integer
    Public Property SUPPLIER_ID As Integer
    Public Property WEIGHT_FACTOR As Nullable(Of Integer)
    Public Property BRIDGER_SCAN As Nullable(Of Integer)
    Public Property NDA As Nullable(Of Integer)
    Public Property CIDCI As Nullable(Of Integer)
    Public Property LEGAL_DOC As Nullable(Of Integer)
    Public Property AGGREEMENT As Nullable(Of Integer)
    Public Property VALIDITY_CHECKING As Nullable(Of Integer)
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer

    Public Overridable Property TPROC_SUPPLIER As TPROC_SUPPLIER

End Class
