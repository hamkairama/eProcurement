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

Partial Public Class TPROC_PC_DT
    Public Property ID As Integer
    Public Property ITEM_NAME As String
    Public Property UNIT_MEASUREMENT As String
    Public Property QUANTITY As Integer
    Public Property PRICE As Integer
    Public Property CREATED_TIME As Date
    Public Property CREATED_BY As String
    Public Property LAST_MODIFIED_TIME As Nullable(Of Date)
    Public Property LAST_MODIFIED_BY As String
    Public Property ROW_STATUS As Integer
    Public Property TOTAL As Nullable(Of Integer)
    Public Property ROW_NUM As Nullable(Of Integer)
    Public Property PC_ID As Nullable(Of Integer)

    Public Overridable Property TPROC_PC_SP_DT As ICollection(Of TPROC_PC_SP_DT) = New HashSet(Of TPROC_PC_SP_DT)

End Class
