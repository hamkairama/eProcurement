Imports System.Data.Entity
Imports System.Net
Imports eProcurementApps.Helpers
Imports eProcurementApps.Models

Namespace Controllers
    Public Class ValidationController
        Inherits System.Web.Mvc.Controller

        Public Function ValidateDuplicate(VALUE As String, COLUMN As String) As JsonResult
            Dim isValid As Boolean = Validation.isUnique(VALUE, COLUMN)

            Dim pesan As String = "Email is already exist"

            If COLUMN = "EMAIL" Then
                pesan = "E-mail is already exist"
            ElseIf COLUMN = "NIK" Then
                pesan = "NIK is already exist"
            End If

            If Not isValid Then
                Return Json(pesan, JsonRequestBehavior.AllowGet)
            Else
                Return Json(Nothing, JsonRequestBehavior.AllowGet)
            End If
        End Function

        Public Function ValidateDuplicateInCompany(VALUE As String, COLUMN As String, COMPANY_ID As String) As JsonResult
            Dim isValid As Boolean = Validation.isUniqueInCompany(VALUE, COLUMN, COMPANY_ID)

            Dim pesan As String = "Email is already exist"

            If COLUMN = "TPROC_ROLE" Then
                pesan = "Role name is already exist"
            ElseIf COLUMN = "CUSTOMER" Then
                pesan = "Customer name is already exist"
            End If
            If Not isValid Then
                Return Json(pesan, JsonRequestBehavior.AllowGet)
            Else
                Return Json(Nothing, JsonRequestBehavior.AllowGet)
            End If
        End Function

        Public Function ValidateDuplicateForEdit(VALUE As String, COLUMN As String, MyID As String) As JsonResult
            Dim isValid As Boolean = Validation.isUnique(VALUE, COLUMN, MyID)

            Dim pesan As String = "Email is already exist"

            If COLUMN = "EMAIL" Then
                pesan = "E-mail is already exist"
            ElseIf COLUMN = "NIK" Then
                pesan = "NIK is already exist"
            End If

            If Not isValid Then
                Return Json(pesan, JsonRequestBehavior.AllowGet)
            Else
                Return Json(Nothing, JsonRequestBehavior.AllowGet)
            End If
        End Function



        Public Function CekHoliday([date] As String) As JsonResult
            Dim holiday As New TPROC_HOLIDAY()
            Dim time As System.Nullable(Of DateTime) = TimeFormat.StringToDate([date])
            Dim message As String = ""
            Dim MyCompany As String = TryCast(Session("COMPANY_ID"), String)
            Using db As New eProcurementEntities()
                holiday = db.TPROC_HOLIDAY.Where(Function(a) a.HOLIDAY_DATE = time).FirstOrDefault()
            End Using
            If holiday IsNot Nothing Then
                message = holiday.HOLIDAY_DESCRIPTION
            Else
                message = Nothing
            End If
            Return Json(message, JsonRequestBehavior.AllowGet)
        End Function

        Public Function CekPassword(password As String) As JsonResult
            Dim message As String = "wrong"
            Dim MyId As String = TryCast(Session("USER_ID"), String)
            Dim myUser As New TPROC_USER()
            Using db As New eProcurementEntities()
                myUser = db.TPROC_USER.Find(MyId)
            End Using
            If myUser IsNot Nothing Then
                'Dim passHash As String = Hashing.CreatePasswordAndSaltHash(password, myUser.PASSWORD_SALT)
                'If myUser.PASSWORD = passHash Then
                '    message = "OK"
                'End If
            End If
            Return Json(message, JsonRequestBehavior.AllowGet)
        End Function


    End Class
End Namespace
