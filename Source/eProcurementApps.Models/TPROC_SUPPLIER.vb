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

Partial Public Class TPROC_SUPPLIER
    Public Property ID As Integer
    Public Property SUPPLIER_NAME As String
    Public Property SUPPLIER_ALIAS_NAME As String
    Public Property SUPPLIER_ADDRESS As String
    Public Property VENDOR_CODE As String
    Public Property BANK_NAME As String
    Public Property BANK_BRANCH As String
    Public Property BANK_ACCOUNT_NUMBER As String
    Public Property CONTACT_PERSON As String
    Public Property EMAIL_ADDRESS As String
    Public Property MOBILE_NUMBER As String
    Public Property OFFICE_NUMBER As String
    Public Property FAX_NUMBER As String
    Public Property TAX_NUMBER As String
    Public Property WEBSITE As String
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property NPWP As String
    Public Property DESCRIPTION As String
    Public Property CORE_BUSINESS As String
    Public Property NAMA_BARANG As String
    Public Property B_UNIT_OWNER As String
    Public Property CITY As String
    Public Property EFFECTIVE_DATE As String
    Public Property SCHEDULE_EVALUATION As String

    Public Overridable Property TPROC_PO_HEADERS As ICollection(Of TPROC_PO_HEADERS) = New HashSet(Of TPROC_PO_HEADERS)
    Public Overridable Property TPROC_SUPP_DOC As ICollection(Of TPROC_SUPP_DOC) = New HashSet(Of TPROC_SUPP_DOC)
    Public Overridable Property TPROC_SUPP_EVAL As ICollection(Of TPROC_SUPP_EVAL) = New HashSet(Of TPROC_SUPP_EVAL)
    Public Overridable Property TPROC_SUPP_QUAL As ICollection(Of TPROC_SUPP_QUAL) = New HashSet(Of TPROC_SUPP_QUAL)

End Class