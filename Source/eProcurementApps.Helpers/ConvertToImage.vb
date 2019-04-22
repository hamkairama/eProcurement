Imports System.Web.Mvc
Imports eProcurementApps.Models

Public Class ConvertToImage
    Public Function getImg(id As Integer) As FileContentResult
        Dim db As New eProcurementEntities
        'Dim byteArray As Byte() = db.FILEEPROCs.Find(id).CONTENT
        Dim byteArray As Byte()
        Return If(byteArray IsNot Nothing, New FileContentResult(byteArray, "image/jpeg"), Nothing)
    End Function
End Class
