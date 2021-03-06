﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class eProcurementEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=eProcurementEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Overridable Property MenuSidebars() As DbSet(Of MenuSidebar)
    Public Overridable Property TPROC_ACKNOW_APPR() As DbSet(Of TPROC_ACKNOW_APPR)
    Public Overridable Property TPROC_APPR_PC() As DbSet(Of TPROC_APPR_PC)
    Public Overridable Property TPROC_APPR_PO() As DbSet(Of TPROC_APPR_PO)
    Public Overridable Property TPROC_APPR_RELDEPT_DT() As DbSet(Of TPROC_APPR_RELDEPT_DT)
    Public Overridable Property TPROC_APPR_RELDEPT_GR() As DbSet(Of TPROC_APPR_RELDEPT_GR)
    Public Overridable Property TPROC_APPROVAL_DT() As DbSet(Of TPROC_APPROVAL_DT)
    Public Overridable Property TPROC_APPROVAL_GR() As DbSet(Of TPROC_APPROVAL_GR)
    Public Overridable Property TPROC_APPROVAL_ROLE() As DbSet(Of TPROC_APPROVAL_ROLE)
    Public Overridable Property TPROC_BUDGET_CODE() As DbSet(Of TPROC_BUDGET_CODE)
    Public Overridable Property TPROC_CHART_OF_ACCOUNT_GR() As DbSet(Of TPROC_CHART_OF_ACCOUNT_GR)
    Public Overridable Property TPROC_CHART_OF_ACCOUNTS() As DbSet(Of TPROC_CHART_OF_ACCOUNTS)
    Public Overridable Property TPROC_CONTROL_PARAMETERS() As DbSet(Of TPROC_CONTROL_PARAMETERS)
    Public Overridable Property TPROC_CURRENCY() As DbSet(Of TPROC_CURRENCY)
    Public Overridable Property TPROC_DELIVERY_ADDRESS() As DbSet(Of TPROC_DELIVERY_ADDRESS)
    Public Overridable Property TPROC_DIVISION() As DbSet(Of TPROC_DIVISION)
    Public Overridable Property TPROC_DOCUMENTS() As DbSet(Of TPROC_DOCUMENTS)
    Public Overridable Property TPROC_FIELD_DESCRIPTIONS() As DbSet(Of TPROC_FIELD_DESCRIPTIONS)
    Public Overridable Property TPROC_FIELD_VALUES() As DbSet(Of TPROC_FIELD_VALUES)
    Public Overridable Property TPROC_FORM_SUB_TYPE() As DbSet(Of TPROC_FORM_SUB_TYPE)
    Public Overridable Property TPROC_FORM_SUBTYPE_DT() As DbSet(Of TPROC_FORM_SUBTYPE_DT)
    Public Overridable Property TPROC_FORM_SUBTYPE_GR() As DbSet(Of TPROC_FORM_SUBTYPE_GR)
    Public Overridable Property TPROC_FORM_TYPE() As DbSet(Of TPROC_FORM_TYPE)
    Public Overridable Property TPROC_FST_BUDGET_CD_ADD() As DbSet(Of TPROC_FST_BUDGET_CD_ADD)
    Public Overridable Property TPROC_GENERATOR() As DbSet(Of TPROC_GENERATOR)
    Public Overridable Property TPROC_GM_HEADERS() As DbSet(Of TPROC_GM_HEADERS)
    Public Overridable Property TPROC_GOOD_TYPE() As DbSet(Of TPROC_GOOD_TYPE)
    Public Overridable Property TPROC_HOLIDAY() As DbSet(Of TPROC_HOLIDAY)
    Public Overridable Property TPROC_LEVEL() As DbSet(Of TPROC_LEVEL)
    Public Overridable Property TPROC_LOG_TRANSACTIONS() As DbSet(Of TPROC_LOG_TRANSACTIONS)
    Public Overridable Property TPROC_MENU() As DbSet(Of TPROC_MENU)
    Public Overridable Property TPROC_PARAMETERS() As DbSet(Of TPROC_PARAMETERS)
    Public Overridable Property TPROC_PC() As DbSet(Of TPROC_PC)
    Public Overridable Property TPROC_PC_DT() As DbSet(Of TPROC_PC_DT)
    Public Overridable Property TPROC_PC_HISTORICAL() As DbSet(Of TPROC_PC_HISTORICAL)
    Public Overridable Property TPROC_PC_SP_DT() As DbSet(Of TPROC_PC_SP_DT)
    Public Overridable Property TPROC_PC_SP_GR() As DbSet(Of TPROC_PC_SP_GR)
    Public Overridable Property TPROC_PO_DETAILS() As DbSet(Of TPROC_PO_DETAILS)
    Public Overridable Property TPROC_PO_DETAILS_ITEM() As DbSet(Of TPROC_PO_DETAILS_ITEM)
    Public Overridable Property TPROC_PO_HEADERS() As DbSet(Of TPROC_PO_HEADERS)
    Public Overridable Property TPROC_PO_TYPE() As DbSet(Of TPROC_PO_TYPE)
    Public Overridable Property TPROC_PPH() As DbSet(Of TPROC_PPH)
    Public Overridable Property TPROC_PR_APPR_RELDEPT_DT() As DbSet(Of TPROC_PR_APPR_RELDEPT_DT)
    Public Overridable Property TPROC_PR_APPR_RELDEPT_GR() As DbSet(Of TPROC_PR_APPR_RELDEPT_GR)
    Public Overridable Property TPROC_PR_APPR_WA() As DbSet(Of TPROC_PR_APPR_WA)
    Public Overridable Property TPROC_PR_ATTACHMENT() As DbSet(Of TPROC_PR_ATTACHMENT)
    Public Overridable Property TPROC_PR_DETAIL() As DbSet(Of TPROC_PR_DETAIL)
    Public Overridable Property TPROC_PR_HEADER() As DbSet(Of TPROC_PR_HEADER)
    Public Overridable Property TPROC_REL_DEPT() As DbSet(Of TPROC_REL_DEPT)
    Public Overridable Property TPROC_REQUEST() As DbSet(Of TPROC_REQUEST)
    Public Overridable Property TPROC_ROLE() As DbSet(Of TPROC_ROLE)
    Public Overridable Property TPROC_ROLE_MENU() As DbSet(Of TPROC_ROLE_MENU)
    Public Overridable Property TPROC_STOCK() As DbSet(Of TPROC_STOCK)
    Public Overridable Property TPROC_SUPP_DOC() As DbSet(Of TPROC_SUPP_DOC)
    Public Overridable Property TPROC_SUPP_EVAL() As DbSet(Of TPROC_SUPP_EVAL)
    Public Overridable Property TPROC_SUPP_QUAL() As DbSet(Of TPROC_SUPP_QUAL)
    Public Overridable Property TPROC_SUPPLIER() As DbSet(Of TPROC_SUPPLIER)
    Public Overridable Property TPROC_USER() As DbSet(Of TPROC_USER)
    Public Overridable Property TPROC_USER_DT() As DbSet(Of TPROC_USER_DT)
    Public Overridable Property TPROC_USER_PROFILE() As DbSet(Of TPROC_USER_PROFILE)
    Public Overridable Property TPROC_VAT() As DbSet(Of TPROC_VAT)
    Public Overridable Property TPROC_VRF_PC() As DbSet(Of TPROC_VRF_PC)
    Public Overridable Property TPROC_VRF_PO() As DbSet(Of TPROC_VRF_PO)
    Public Overridable Property TPROC_WA() As DbSet(Of TPROC_WA)
    Public Overridable Property TPROC_WA_ALLOWED_DT() As DbSet(Of TPROC_WA_ALLOWED_DT)
    Public Overridable Property TPROC_WA_ALLOWED_GR() As DbSet(Of TPROC_WA_ALLOWED_GR)
    Public Overridable Property TPROC_ACKNOW_APPR_DT() As DbSet(Of TPROC_ACKNOW_APPR_DT)
    Public Overridable Property TPROC_APPROVAL_ROLE_DETAIL() As DbSet(Of TPROC_APPROVAL_ROLE_DETAIL)
    Public Overridable Property TPROC_CRV() As DbSet(Of TPROC_CRV)
    Public Overridable Property TPROC_CRV_DETAILS() As DbSet(Of TPROC_CRV_DETAILS)
    Public Overridable Property TPROC_GM_DETAILS() As DbSet(Of TPROC_GM_DETAILS)
    Public Overridable Property TPROC_PR_HISTORICAL() As DbSet(Of TPROC_PR_HISTORICAL)
    Public Overridable Property TPROC_STOCKMOVEMENT() As DbSet(Of TPROC_STOCKMOVEMENT)

End Class
