
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class EmailTemplate

#Region "CREATE NEW PR"
    '<<zoer20171001
    'ori --> Public Const eProcSubjectEmailUserCreatePR As String = "[eProcurement] {0} – has been {1}"
    Public Const eProcSubjectEmailUserCreatePR As String = "[eProcurement] {0} - {1} – has been {2}"
    '>>zoer20171001
    Public Const eProcEmailTemplateUserNotificationCreatePR As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> you has been create PR with number : {0}.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{1}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "PR HAS BEEN COMPLETED"
    '<<zoer20171001
    'ori --> Public Const eProcSubjectEmailUserCreatePR As String = "[eProcurement] {0} – has been {1}"
    Public Const eProcSubjectEmailUserPRCompleted As String = "[eProcurement] {0} - {1} – has been {2}"
    '>>zoer20171001
    Public Const eProcEmailTemplateUserNotificationPRCompleted As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> Your item request PR with number : {0}. are ready. </br>
                                                 Please go to Proc Warehouse Monday to Thursday at 8.30 -12 AM, Friday at 8.30 to 11.30 AM on 16th FL to pick up the goods. </br>
                                                 For outsite Jakarta, Goods have been delivered
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{1}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO APPROVER/REVIEW BY WA"
    'Public Const eProcSubjectEmailPR_WA As String = "[eProcurement] {0} – WORK AREA - ITEM NAME : {1} - Request need to be {2}"
    'Public Const eProcEmailTemplatePRNotification_WA As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
    '                        <tr>
    '                            <td>
    '                                <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
    '                                    <tr>
    '                                        <td style='width:100%;' colspan='2'>
    '                                            <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
    '                                        </td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%' colspan='2'>&nbsp</td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%' colspan='2'>
    '                                            <b>{0}</b>
    '                                        </td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%'>
    '                                            Request Number
    '                                        </td>
    '                                        <td style='width:80%'>
    '                                            : {1}                        
    '                                        </td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%'>
    '                                            Request Date
    '                                        </td>
    '                                        <td style='width:80%'>
    '                                            : {2}                        
    '                                        </td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%'>
    '                                            Requested By
    '                                        </td>
    '                                        <td style='width:80%'>
    '                                            : {3}                        
    '                                        </td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%'>
    '                                            Status
    '                                        </td>
    '                                        <td style='width:80%'>
    '                                            : {4}                        
    '                                        </td>
    '                                    </tr>
    '                                    <tr>
    '                                        <td style='width:20%'>
    '                                            Item Name
    '                                        </td>
    '                                        <td style='width:80%'>
    '                                            : {5}                        
    '                                        </td>
    '                                    </tr>
    '                                </table>
    '                            </td>
    '                        </tr>
    '                    </table>"


    Public Const eProcSubjectEmailPR_WA As String = "[eProcurement] {0} - {1} – Request need to {2}"
    Public Const eProcEmailTemplatePRNotification_WA As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Requested By
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Status
                                            </td>
                                            <td style='width:80%'>
                                                : {4}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO USER BY WA"
    Public Const eProcSubjectEmailUser_WA As String = "[eProcurement] {0} for item name {1} – has been {2}"
    Public Const eProcEmailTemplateUserNotification_WA As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> Your request has been {0}.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{1}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Item Name
                                            </td>
                                            <td style='width:80%'>
                                                : {4}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                {5} by
                                            </td>
                                            <td style='width:80%'>
                                                : {6}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "REQUEST EPROC"
    Public Const eProcSubjectToCreator As String = "[eProcurement] {0} - {1} {2} {3} - {4}"
    Public Const eProcEmailTemplateUserNotificationForCreator As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> {0} {1} {2} - {3} in eprocurement.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{4}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {5}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                {6}
                                            </td>
                                            <td style='width:80%'>
                                                : {7}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {8}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"


    Public Const eProcSubjectToRequestor As String = "[eProcurement] {0} - you has sent the request for {1} {2}"
    Public Const eProcEmailTemplateUserNotificationForRequestor As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> you has sent the request for {0} {1} in eprocurement.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Requestor
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                {4}
                                            </td>
                                            <td style='width:80%'>
                                                : {5}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {6}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Note : this email is proof that you have requested</b>.
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"

    Public Const eProcSubjectToRequestorUserApprovedCompleted As String = "[eProcurement] {0} - your request has been {1}"
    Public Const eProcEmailTemplateUserNotificationForRequestorUserApprovedCompleted As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> your request has been {0}.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Requestor
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Note : this email is proof that you have requested</b>.
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO APPROVER/REVIEW BY RD"
    '<<zoer20171001
    'Public Const eProcSubjectEmailPR_RD As String = "[eProcurement] {0} - Related Department – Request need to {1}"
    Public Const eProcSubjectEmailPR_RD As String = "[eProcurement] {0} - {1} - Related Department – Request need to {2}"
    '>>zoer20171001
    Public Const eProcEmailTemplatePRNotification_RD As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Note : </b> To Approve or Review or Reject a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Requested By
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Status
                                            </td>
                                            <td style='width:80%'>
                                                : {4}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO HANDLE"
    Public Const eProcSubjectEmailPR_Handle As String = "[eProcurement] {0} - {1} – Request need to {2}"
    Public Const eProcEmailTemplatePRNotification_Handle As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Note : </b> To handle a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Requested By
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Status
                                            </td>
                                            <td style='width:80%'>
                                                : {4}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO USER BY RD"
    Public Const eProcSubjectEmailUser_RD As String = "[eProcurement] {0} – has been {1} by related department"
    Public Const eProcEmailTemplateUserNotification_RD As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> Your request has been {0} by related department.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{1}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                {4} by
                                            </td>
                                            <td style='width:80%'>
                                                : {5}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO USER BY EPROC"
    Public Const eProcSubjectEmailUser_EPROC As String = "[eProcurement] {0} – has been {1} by eprocurement admin"
    Public Const eProcEmailTemplateUserNotification_EPROC As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> Your request has been {0} by eprocurement admin.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{1}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                {4} by
                                            </td>
                                            <td style='width:80%'>
                                                : {5}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Reject reason by eproc
                                            </td>
                                            <td style='width:80%'>
                                                : {6}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "PUSH EMAIL TO USER"
    '<<zoer20171001
    'ori --> Public Const eProcSubjectEmailUser_Push As String = "[eProcurement] {0} - Push Email has been sent"
    Public Const eProcSubjectEmailUser_Push As String = "[eProcurement] {0} - {1} - Push Email has been sent"
    '>>zoer20171001
    Public Const eProcEmailTemplateUserNotification_Push As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> Your push email has been sent.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                               Sent push email date :
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "PO OR PC"
    Public Const eProcSubjectEmailPCPO As String = "[eProcurement] – Number : {0} - Request need to be {1}"
    Public Const eProcEmailTemplatePCPONotification As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Note : </b> To Approve a request do not use your 'Reply' button, you MUST use the LINKS below. A response using the 'Reply' button is not a valid approval or denial and will be ignored.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Status
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"


    Public Const eProcSubjectEmailPCPONotif As String = "[eProcurement] – Number : {0} - has been {1}"
    Public Const eProcEmailTemplatePCPONotification_2 As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Date
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Status
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"
#End Region

#Region "TO SUPPPLIER"
    Public Const eProcSubjectToSupplier As String = "[eProcurement] - {0}"
    Public Const eProcEmailTemplateUserNotificationForSupplier As String = "<table style='border-color:#000000' cellpadding='0' cellspacing='0' border='1' width='70%'>
                            <tr>
                                <td>
                                    <table style='font-family:Century Gothic;font-size:11px;color:#000000' cellpadding='5' cellspacing='5' border='0' width='100%' align='left'>
                                        <tr>
                                            <td style='width:100%;' colspan='2'>
                                                <b>Fyi, </b> Attachment of my purchase order.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%' colspan='2'>
                                                <b>{0}</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Request Number
                                            </td>
                                            <td style='width:80%'>
                                                : {1}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Order By
                                            </td>
                                            <td style='width:80%'>
                                                : {2}                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='width:20%'>
                                                Order Date
                                            </td>
                                            <td style='width:80%'>
                                                : {3}                        
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style='width:20%'>
                                                Please Delivery To
                                            </td>
                                            <td style='width:80%'>
                                                : {4}                        
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style='width:20%'>
                                                Please Dalivery on
                                            </td>
                                            <td style='width:80%'>
                                                : {5}                        
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>"

#End Region

End Class
