Imports eProcurementApps.Helpers
Imports eProcurementApps.Models
Public Class EmailFacade
    Public Function InsertLogTransactionWithCommit(username As String, form As String, transactionDescription As String) As ResultStatus
        Dim result As New ResultStatus
        result.Status = False
        Try
            Dim db As New eProcurementEntities
            Dim log_transactions As New TPROC_LOG_TRANSACTIONS
            log_transactions.FORM = form
            log_transactions.TRANSACTION_DESCRIPTION = transactionDescription
            log_transactions.CREATED_BY = username
            log_transactions.CREATED_TIME = Date.Now
            db.TPROC_LOG_TRANSACTIONS.Add(log_transactions)
            db.SaveChanges()
            result.SetSuccessStatus()
        Catch exp As Exception
            result.SetErrorStatus(exp.Message)
        End Try

        Return result
    End Function


    Public Function SendEmail(from As String, [to] As ListFieldNameAndValue, cc As ListFieldNameAndValue, subject As String, body As String, ByRef message As String) As ResultStatus
        Dim result As New ResultStatus
        result.Status = False
        Try
            result = Emails.SendEmail(from, [to], cc, subject, body, message)
            If result.IsSuccess Then
                Dim emailTo As String = String.Empty
                Dim emailCc As String = String.Empty
                If [to] IsNot Nothing Then
                    For i As Integer = 0 To [to].Count - 1
                        If Not emailTo.Contains([to].getValuebyId(i).ToString()) Then
                            emailTo += String.Format("{0}, ", [to].getValuebyId(i).ToString())
                        End If
                    Next
                    If emailTo.Length > 0 Then
                        emailTo = String.Format("{0}", emailTo.Remove(emailTo.Trim().Length - 1))
                    End If
                End If
                If cc IsNot Nothing Then
                    For i As Integer = 0 To cc.Count - 1
                        If Not emailCc.Contains(cc.getValuebyId(i).ToString()) Then
                            emailCc += String.Format("{0}, ", cc.getValuebyId(i).ToString())
                        End If
                    Next
                    If emailCc.Length > 0 Then
                        emailCc = String.Format("{0}", emailCc.Remove(emailCc.Trim().Length - 1))
                    End If
                End If
                'result = InsertLogTransactionWithCommit("System", "Sending Email", String.Format("Sending Date : {0}, Subject : {1}, From : {2}, To : {3}, CC : {4}, Message : {5}", DateTime.Now.ToString(), subject, from, emailTo, emailCc,
                'body))

            End If
            Return result
        Catch exp As Exception
            Throw exp
        End Try
    End Function

    Public Function SendEmailAttach(from As String, [to] As ListFieldNameAndValue, cc As ListFieldNameAndValue, subject As String, body As String, ByRef message As String, attachment As String) As ResultStatus
        Dim result As New ResultStatus
        result.Status = False
        Try
            result = Emails.SendEmailAttach(from, [to], cc, subject, body, message, attachment)
            If result.IsSuccess Then
                Dim emailTo As String = String.Empty
                Dim emailCc As String = String.Empty
                If [to] IsNot Nothing Then
                    For i As Integer = 0 To [to].Count - 1
                        If Not emailTo.Contains([to].getValuebyId(i).ToString()) Then
                            emailTo += String.Format("{0}, ", [to].getValuebyId(i).ToString())
                        End If
                    Next
                    If emailTo.Length > 0 Then
                        emailTo = String.Format("{0}", emailTo.Remove(emailTo.Trim().Length - 1))
                    End If
                End If
                If cc IsNot Nothing Then
                    For i As Integer = 0 To cc.Count - 1
                        If Not emailCc.Contains(cc.getValuebyId(i).ToString()) Then
                            emailCc += String.Format("{0}, ", cc.getValuebyId(i).ToString())
                        End If
                    Next
                    If emailCc.Length > 0 Then
                        emailCc = String.Format("{0}", emailCc.Remove(emailCc.Trim().Length - 1))
                    End If
                End If
                result = InsertLogTransactionWithCommit("System", "Sending Email", String.Format("Sending Date : {0}, Subject : {1}, From : {2}, To : {3}, CC : {4}, Message : {5}", DateTime.Now.ToString(), subject, from, emailTo, emailCc, body))

            End If
            Return result
        Catch exp As Exception
            Throw exp
        End Try
    End Function
End Class
