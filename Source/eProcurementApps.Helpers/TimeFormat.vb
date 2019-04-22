Public Class TimeFormat
    Public Shared Function ConcateHourAndDate([date] As String, hours As String) As DateTime
        Dim result As DateTime
        result = DateTime.ParseExact(Convert.ToString([date] & Convert.ToString(" ")) & hours, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture)
        Return result
    End Function

    Public Shared Function GetTotal(start As String, [end] As String) As Decimal
        Dim result As Decimal = 0
        If start IsNot Nothing AndAlso [end] IsNot Nothing Then
            Dim arrStart As String() = start.Split(":"c)
            Dim HStart As Integer = Integer.Parse(arrStart(0))
            Dim MStart As Integer = Integer.Parse(arrStart(1))

            Dim arrEnd As String() = [end].Split(":"c)
            Dim HEnd As Integer = Integer.Parse(arrEnd(0))
            Dim MEnd As Integer = Integer.Parse(arrEnd(1))
            If MEnd < MStart Then
                HEnd = HEnd - 1
                MEnd = MEnd + 60
            End If
            'int hour = HEnd - HStart;
            'int minute = MEnd - MStart;
            result = CDec(HEnd - HStart) + (CDec(MEnd - MStart) / 60)
        End If
        Return result
    End Function

    Public Shared Function StringToDate(value As String) As System.Nullable(Of DateTime)
        Dim result As System.Nullable(Of DateTime) = Nothing
        If value <> "" Then
            result = DateTime.ParseExact(value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        End If
        Return result
    End Function

End Class
