Imports System.Web
Imports eProcurementApps.Models

Public Class Validation
    ' menghasilkan nilai true jika value yang dikirim unique
    Public Shared Function isUnique(value As String, column As String) As Boolean
        Dim isValid As Boolean = False
        Using db As New eProcurementEntities()
            value = value.Replace("_"c, " "c)

            Dim charToTrim As Char() = {" "c}
            value = value.Trim(charToTrim)

            If column = "EMAIL" Then
                If db.TPROC_USER.Where(Function(e) e.USER_MAIL.ToLower() = value.ToLower()).Count() > 0 Then
                    isValid = False
                Else
                    isValid = True
                End If
            ElseIf column = "NIK" Then
                'If db.TPROC_USER.Where(Function(e) e.EMP_NIK.ToLower() = value.ToLower()).Count() > 0 Then
                '    isValid = False
                'Else
                '    isValid = True
                'End If
            End If
        End Using
        Return isValid
    End Function
    Public Shared Function isUnique(value As String, column As String, myID As String) As Boolean
        value = value.Replace("_"c, " "c)
        Dim charToTrim As Char() = {" "c}
        value = value.Trim(charToTrim)

        Dim isValid As Boolean = False
        Using db As New eProcurementEntities()
            If column = "EMAIL" Then
                If db.TPROC_USER.Where(Function(e) e.USER_MAIL.ToLower() = value.ToLower() AndAlso e.USER_MAIL <> myID).Count() > 0 Then
                    isValid = False
                Else
                    isValid = True
                End If
            ElseIf column = "NIK" Then
                'If db.TPROC_USER.Where(Function(e) e.EMP_NIK.ToLower() = value.ToLower() AndAlso e.EMP_ID <> myID).Count() > 0 Then
                '    isValid = False
                'Else
                '    isValid = True
                'End If
            End If
        End Using
        Return isValid
    End Function


    ' menghasilkan nilai true jika value yang dikirim unique dalam satu company
    Public Shared Function isUniqueInCompany(value As String, column As String, company As String) As Boolean
        value = value.Replace("_"c, " "c)

        Dim charToTrim As Char() = {" "c}
        value = value.Trim(charToTrim)

        Dim isValid As Boolean = False
        Using db As New eProcurementEntities()
            If column = "TPROC_ROLE" Then
                If db.TPROC_ROLE.Where(Function(a) a.ROLE_NAME.ToLower() = value.ToLower()).Count() > 0 Then
                    isValid = False
                Else
                    isValid = True
                End If
            ElseIf column = "CUSTOMER" Then
                'If db.TB_M_CUSTOMER.Where(Function(a) a.CUST_NM.ToLower() = value.ToLower() AndAlso a.COMPANY_ID = company).Count() > 0 Then
                '    isValid = False
                'Else
                '    isValid = True
                'End If
            End If
        End Using
        Return isValid
    End Function

End Class
