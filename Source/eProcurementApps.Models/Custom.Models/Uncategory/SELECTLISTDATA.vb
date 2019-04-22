Imports System.ComponentModel.DataAnnotations
Public Class SELECTLISTDATA

    <Key()>
    Public Property VALUE As String
    Public Property TEXT As String

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(ByVal val As String, ByVal txt As String)
        MyBase.New
        Me.TEXT = txt
        Me.VALUE = val
    End Sub
End Class