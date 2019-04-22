Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

Public Class EmailValidator
    Shared ValidEmailRegex As Regex = CreateValidEmailRegex()

    Private Shared Function CreateValidEmailRegex() As Regex
        Dim validEmailPattern As String = "^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + "([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + "@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"

        Return New Regex(validEmailPattern, RegexOptions.IgnoreCase)
    End Function

    Public Shared Function EmailIsValid(emailAddress As String) As Boolean
        Dim isValid As Boolean = ValidEmailRegex.IsMatch(emailAddress)

        Return isValid
    End Function
End Class

