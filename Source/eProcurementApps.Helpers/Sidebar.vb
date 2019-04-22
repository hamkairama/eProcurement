Imports eProcurementApps.Models
Imports System.Data.Entity
Imports System.Web
Imports System.Web.Mvc

Public Class Sidebar

    Public Shared Function GetSidebar() As List(Of TPROC_MENU)
        Dim db As New eProcurementEntities
        Dim listMenu As New List(Of TPROC_MENU)
        listMenu = db.TPROC_MENU.ToList()
        Return listMenu
    End Function

End Class
