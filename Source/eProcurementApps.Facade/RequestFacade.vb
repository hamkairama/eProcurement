Imports eProcurementApps.Models
Imports eProcurementApps.Helpers
Imports System.Transactions
Imports System.Web
Imports System.Configuration

Public Class RequestFacade

    Public Shared created_time As Date = Date.Now

#Region "REQUEST"
    Public Shared Function SaveRequest(ByVal req As TPROC_REQUEST, ByRef new_req As TPROC_REQUEST) As ResultStatus
        Dim rs As New ResultStatus
        Try
            Using db As New eProcurementEntities
                db.TPROC_REQUEST.Add(req)
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
            new_req = req
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function UpdateRequest(reqNumber As String, modifed_by As String, status0 As Decimal, status1 As Decimal, ByRef request As TPROC_REQUEST) As ResultStatus
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities
                Dim req = db.TPROC_REQUEST.Where(Function(x) x.REQUEST_NO = reqNumber And x.STATUS = status0).FirstOrDefault()
                request = req

                req.LAST_MODIFIED_BY = modifed_by
                req.LAST_MODIFIED_TIME = Date.Now
                req.STATUS = status1

                If status1 = ListEnum.Request.NeedComplete Then
                    req.APPROVED_TIME = Date.Now
                End If

                db.Entry(req).State = EntityState.Modified
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function
#End Region


#Region "SEND EMAIL REQUEST"
    Public Shared Function SendEmailRequest(req As TPROC_REQUEST, data_flag As Decimal, emailTo As ListFieldNameAndValue) As ResultStatus
        Dim rs As New ResultStatus
        rs = SendEmailToAdminOrApprover(req, data_flag, emailTo)
        If rs.IsSuccess Then
            rs = SendEmailProofToUser(req)
        End If

        Return rs
    End Function

    Public Shared Function SendEmailToAdminOrApprover(req As TPROC_REQUEST, data_flag As Decimal, emailTo As ListFieldNameAndValue) As ResultStatus
        Dim rs As New ResultStatus
        Dim mailFacade As New EmailFacade

        Dim linkEproc As String = ConfigurationSettings.AppSettings("LinkeProc")
        Dim link As String = String.Format("<a href='{0}'>Click here to View Details</a>", linkEproc + req.CONTROL + "/RequestApproveComplete/?" + "reqno=" + req.REQUEST_NO + "&rel_flag=" + req.RELATION_FLAG + "&control=" + req.CONTROL + "&actions=" + req.ACTION + "&data_flag=" + data_flag.ToString)

        'to adminstaff/approver
        rs = mailFacade.SendEmail("eProcurement@manulife.com", emailTo, Nothing, String.Format(EmailTemplate.eProcSubjectToCreator, req.REQUEST_NO, req.REQUEST_BY, GetStatusRequest(data_flag), req.ACTION, req.CONTROL),
                                    String.Format(EmailTemplate.eProcEmailTemplateUserNotificationForCreator, req.REQUEST_BY, GetStatusRequest(data_flag), req.ACTION, req.CONTROL, link, req.REQUEST_NO, req.CONTROL, req.RELATION_FLAG, req.REQUEST_DT),
                                        Nothing)


        Return rs
    End Function

    Public Shared Function SendEmailProofToUser(req As TPROC_REQUEST) As ResultStatus
        Dim rs As New ResultStatus
        Dim reqDate As Date = Date.Now
        Dim mailFacade As New EmailFacade

        Dim emailRequestor As New ListFieldNameAndValue
        emailRequestor.AddItem("Email", CurrentUser.GetCurrentUserEmail)

        rs = mailFacade.SendEmail("eProcurement@manulife.com", emailRequestor, Nothing, String.Format(EmailTemplate.eProcSubjectToRequestor, req.REQUEST_NO, req.ACTION, req.CONTROL),
                                    String.Format(EmailTemplate.eProcEmailTemplateUserNotificationForRequestor, req.ACTION, req.CONTROL, req.REQUEST_NO, CurrentUser.GetCurrentUserName, req.CONTROL, req.RELATION_FLAG, req.REQUEST_DT),
                                        Nothing)

        Return rs
    End Function


    Public Shared Function SendEmailToUserApprovedCompleted(ByVal reqNumber As String, ByVal user As TPROC_REQUEST, ByVal flag As Decimal) As ResultStatus
        Dim rs As New ResultStatus
        Dim mailFacade As New EmailFacade
        Dim emailTo As New ListFieldNameAndValue

        Dim emailRequestor As New ListFieldNameAndValue
        emailRequestor.AddItem("Email", user.REQUESTOR_EMAIL)
        rs = mailFacade.SendEmail("eProcurement@manulife.com", emailRequestor, Nothing, String.Format(EmailTemplate.eProcSubjectToRequestorUserApprovedCompleted, reqNumber, GetStatusRequest(flag)),
                                    String.Format(EmailTemplate.eProcEmailTemplateUserNotificationForRequestorUserApprovedCompleted, GetStatusRequest(flag), reqNumber, user.REQUEST_BY),
                                      Nothing)

        Return rs
    End Function

    Public Shared Function GetEmailEprocStaff() As ListFieldNameAndValue
        Dim db As New eProcurementEntities
        Dim emailTo As New ListFieldNameAndValue
        Dim uSer As New List(Of TPROC_USER)
        uSer = db.TPROC_USER.Where(Function(x) x.TPROC_USER_DT.IS_SUPER_ADMIN = 1 And x.TPROC_USER_DT.IS_EPROC_ADMIN = 1 And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        For Each usr In uSer
            emailTo.AddItem("Email", usr.USER_MAIL)
        Next

        Return emailTo
    End Function

    Public Shared Function GetApproverByHighestLevel(ByVal lapp_detail As String()) As HIGHEST_LEVEL
        Dim lHighestLevel As New List(Of HIGHEST_LEVEL)
        Dim result As New HIGHEST_LEVEL

        Try
            For Each item As String In lapp_detail
                Dim arry = item.Split("|")
                Dim highesLevel As New HIGHEST_LEVEL
                highesLevel.USER_NAME = arry(1).ToString()
                highesLevel.EMAIL = arry(2).ToString()
                highesLevel.RUPIAH_LIMIT_LEVEL = GetRupiahLimitByLevel(Convert.ToDecimal(arry(3)))

                lHighestLevel.Add(highesLevel)
            Next

            Dim orderByDesc = lHighestLevel.OrderByDescending(Function(x) x.RUPIAH_LIMIT_LEVEL)
            result = orderByDesc(0)

        Catch ex As Exception
        End Try

        Return result
    End Function

    Public Shared Function GetRupiahLimitByLevel(ByVal id As Decimal) As Decimal
        Dim rs As New ResultStatus
        Dim rupiahLimit As Decimal
        Try
            Using db As New eProcurementEntities
                rupiahLimit = db.TPROC_LEVEL.Find(id).RUPIAH_LIMIT
            End Using
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rupiahLimit
    End Function

    Private Shared Function GetStatusRequest(ByVal values As Integer) As String
        Dim result As String = ""
        If values = ListEnum.Request.Submitted Then
            result = "Submitted"
        ElseIf values = ListEnum.Request.NeedApprove Then
            result = "Need Approve"
        ElseIf values = ListEnum.Request.Approved Then
            result = "Approved"
        ElseIf values = ListEnum.Request.NeedComplete Then
            result = "Need Complete"
        ElseIf values = ListEnum.Request.Completed Then
            result = "Completed"
        End If

        Return result
    End Function

#End Region
End Class