Imports System.IO
Imports System.Web
Imports System.Web.Hosting
Imports eProcurementApps.Models

Public Class CDataFile
    Public Shared Function GetFileName(path As String) As String
        Dim filename As String = ""
        Dim arry As String() = path.Split("\")

        filename = arry(arry.Length - 1)

        Return filename
    End Function

End Class
