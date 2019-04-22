Imports eProcurementApps.Models
Imports eProcurementApps.Helpers

Namespace Controllers
    Public Class SEARCHController
        Inherits System.Web.Mvc.Controller

        Public Shared JsonDataTable As DataTable
        Public Shared HeaderForm As String
        Public Shared InnerHtmlData As String
        Public Shared HiddenHtml As String
        Function Index(Optional ByVal _JsonDataTable As String = "",
                       Optional ByVal _HeaderForm As String = "",
                       Optional ByVal _InnerHtmlData As String = "") As ActionResult
            JsonDataTable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(_JsonDataTable)
            Dim FilterData As String = ""
            For i As Integer = 0 To JsonDataTable.Columns.Count - 1
                If i = 0 Then
                    FilterData = "[" & JsonDataTable.Columns(i).ColumnName & "] IS NOT NULL "
                Else
                    FilterData = FilterData & "OR [" & JsonDataTable.Columns(i).ColumnName & "] IS NOT NULL "
                End If
            Next
            Dim dtView As New DataView(JsonDataTable)
            dtView.RowFilter = FilterData
            JsonDataTable = dtView.ToTable
            HeaderForm = _HeaderForm
            For i As Integer = 0 To Split(_InnerHtmlData, "|").Count - 1
                If i = 0 Then
                    InnerHtmlData = Split(Split(_InnerHtmlData, "|")(i), ":")(0)
                    HiddenHtml = Split(Split(_InnerHtmlData, "|")(i), ":")(1)
                Else
                    InnerHtmlData = InnerHtmlData & "|" & Split(Split(_InnerHtmlData, "|")(i), ":")(0)
                    HiddenHtml = HiddenHtml & ":" & Split(Split(_InnerHtmlData, "|")(i), ":")(1)
                End If
            Next

            Return PartialView("FormSearch")
        End Function
    End Class
End Namespace
