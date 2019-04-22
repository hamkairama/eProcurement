Imports System.Net
Imports eProcurementApps.DataAccess

Public Class Form1
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Application.Exit()
    End Sub

    Private Sub BtnEnc_Click(sender As Object, e As EventArgs) Handles BtnEnc.Click
        Dim getTxtInput As String = ""
        getTxtInput = TxtInput.Text
        Dim result = Cryptoghrap.EncryptString(getTxtInput)

        TxtOutput.Text = result
    End Sub

    Private Sub BtnDec_Click(sender As Object, e As EventArgs) Handles BtnDec.Click
        Dim getTxtInput As String = ""
        getTxtInput = TxtInput.Text
        Dim result = Cryptoghrap.DecryptString(getTxtInput)

        TxtOutput.Text = result
    End Sub
End Class
