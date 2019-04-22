Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class Labels
    Public Shared Function ButtonLabel(mode As String) As String
        Dim label As String = ""
        Select Case mode
            Case "Edit"
                label = "Edit"
                Exit Select
            Case "Create"
                label = "Create"
                Exit Select
            Case "Approve"
                label = "Approve"
                Exit Select
            Case "Reject"
                label = "Reject"
                Exit Select
            Case "Accept"
                label = "Accept"
                Exit Select
            Case "Submit"
                label = "Submit"
                Exit Select
            Case "Back"
                label = "Back"
                Exit Select
            Case "Close"
                label = "Close"
                Exit Select
            Case "Cancel"
                label = "Cancel"
                Exit Select
        End Select
        Return label
    End Function

    Public Shared Function IconWidget(mode As String) As String
        Dim label As String = ""
        Select Case mode
            Case "Edit"
                label = "<i class='ace-icon fa fa-pencil icon-on-right bigger-110'></i>"
                Exit Select
            Case "Create"
                label = "<i class='ace-icon fa fa-arrow-right icon-on-right bigger-110'>"
                Exit Select
            Case "Approve"
                label = "<i class='ace-icon fa fa-arrow-right icon-on-right bigger-110'>"
                Exit Select
            Case "List"
                label = "<i class='ace-icon fa fa-list-alt icon-on-right bigger-110''></i>"
                Exit Select
            Case "Details"
                label = "<i class='ace-icon fa fa-search-plus icon-on-right bigger-110'></i>"
                Exit Select
            Case "Back"
                label = "<i class='ace-icon fa fa-arrow-left icon-on-right bigger-110'>"
                Exit Select
            Case "Cancel"
                label = "<i class='ace-icon fa fa-arrow-left icon-on-right bigger-110'>"
                Exit Select
            Case "Close"
                label = "<i class='ace-icon fa fa-times bigger-110'>"
                Exit Select
        End Select
        Return label
    End Function

    Public Shared Function IconAction(mode As String) As String
        Dim label As String = ""
        Select Case mode
            Case "Create"
                label = "<i class='ace-icon fa fa-plus-square bigger-130'></i>"
                Exit Select
            Case "Edit"
                label = "<i class='ace-icon fa fa-pencil bigger-130'></i>"
                Exit Select
            Case "Delete"
                label = "<i class='ace-icon fa fa-trash-o bigger-130'></i>"
                Exit Select
            Case "Details"
                label = "<i class='ace-icon fa fa-search-plus bigger-130'></i>"
                Exit Select
            Case "Approve"
                label = "<i class='ace-icon fa fa-check bigger-130' title='Approve'></i>"
                Exit Select
            Case "Complete"
                label = "<i class='ace-icon fa fa-check bigger-130' title='Approve'></i>"
                Exit Select
            Case "Reject"
                label = "<i class='ace-icon fa fa-times bigger-130'></i>"
                Exit Select
            Case "Review"
                label = "<i class='ace-icon fa fa-eye bigger-130'></i>"
                Exit Select
            Case "Search"
                label = "<i class='ace-icon fa fa-search-plus bigger-130'></i>"
                Exit Select
            Case "Download"
                label = "<i class='ace-icon fa fa-cloud-download bigger-130'></i>"
                Exit Select
            Case "Released"
                label = "<i class='ace-icon fa fa-usd bigger-130'></i>"
                Exit Select
            Case "Received"
                label = "<i Class='ace-icon fa fa-arrow-right bigger-130' title='Received'></i>"
                Exit Select
            Case "Paid"
                label = "<i Class='ace-icon fa fa-dollar bigger-130' title='Paid'></i>"
                Exit Select
        End Select
        Return label
    End Function

    Public Shared Function LongString(value As String, min As Integer) As String
        Dim result As String = If(value.Length >= min, value.Substring(0, (min - 3)) + "...", value)
        Return result
    End Function

    Public Shared Function ButtonForm(mode As String) As String
        Dim label As String = ""
        Select Case mode
            Case "Upload"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' type='submit'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Upload
                        </button>"
                Exit Select
            Case "PushEmailByWAItem"
                label = "<button type='submit' onclick=""PushEmail('WA')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Push Email
                        </button>"
                Exit Select
            Case "PushEmailByRD"
                label = "<button type='submit' onclick=""PushEmail('RD')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Push Email Related Department
                        </button>"
                Exit Select
            Case "PushEmailByPO"
                label = "<button type='submit' onclick=""PushEmailByPO()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Push Email
                        </button>"
                Exit Select
            Case "PushEmailByPC"
                label = "<button type='submit' onclick=""PushEmailByPC()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Push Email
                        </button>"
                Exit Select
            Case "PushEmailByCRV"
                label = "<button type='submit' onclick=""PushEmailByCRV()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Push Email
                        </button>"
                Exit Select
            Case "Save"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Save
                        </button>"
                Exit Select
            Case "Close"
                label = "<button class='btn btn-sm btn-danger btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red2'></i>
                            Close
                        </button>"
                Exit Select

            Case "ChooseOneApproval"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil-square-o bigger-110 green'></i>
                            Choose One Approval
                        </button>"
                Exit Select

            Case "Exit"
                label = "<button type='button' class='close' data-dismiss='modal' aria-hidden='True'>
                            <span class='white'>&times;</span>
                        </button>"
                Exit Select
            Case "Create"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil-square-o  bigger-110 green'></i>
                            Create
                        </button>"
                Exit Select
            Case "Submit"
                label = "<button type='submit' onclick=""BtnAction('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Submit 
                        </button>"
                Exit Select
            Case "SubmitPRWithChooseOneApproval"
                label = "<button type='submit' onclick=""BtnAction('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Submit 
                        </button>"
                Exit Select
            Case "Approve"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check bigger-110 green'></i>
                            Approve
                        </button>"
                Exit Select

            Case "Details"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-search-plus bigger-110 green'></i>
                            Details
                        </button>"
                Exit Select

            Case "Verify"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check bigger-110 green'></i>
                            Verify
                        </button>"
                Exit Select

            Case "Review"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-eye bigger-110 green'></i>
                            Review
                        </button>"
                Exit Select

            Case "Completing"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Completed 
                        </button>"
                Exit Select

            Case "Reject"
                label = "<button type='submit' class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject
                        </button>"
                Exit Select

            Case "RejectByReviewer"
                label = "<button type='submit' class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject By Reviewer
                        </button>"
                Exit Select

            Case "RejectByVerifier"
                label = "<button type='submit' class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject By Verifier
                        </button>"
                Exit Select

            Case "RejectByApprover"
                label = "<button type='submit' class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject By Approver
                        </button>"
                Exit Select

            Case "RejectByEproc"
                label = "<button type='submit' class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject By Admin Eproc
                        </button>"
                Exit Select

            Case "SaveCreate"
                label = "<button type='submit' onclick=""BtnAction('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Save
                        </button>"
                Exit Select

            Case "SaveEdit"
                label = "<button type='submit' onclick=""BtnAction('edit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Save
                        </button>"
                Exit Select
            Case "SaveDelete"
                label = "<button type='submit' onclick=""BtnAction('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Delete
                        </button>"
                Exit Select
            Case "SaveApprove"
                label = "<button type='submit' onclick=""BtnAction('approve')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check bigger-110 green'></i>
                            Approve
                        </button>"
                Exit Select
            Case "SaveReleased"
                label = "<button type='submit' onclick=""BtnAction('released')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-usd bigger-110 green'></i>
                            Released
                        </button>"
                Exit Select
            Case "SaveSubmit"
                label = "<button type='submit' onclick=""BtnAction('submit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check bigger-110 green'></i>
                            Submit
                        </button>"
                Exit Select
            Case "SaveExport"
                label = "<button type='submit' onclick=""BtnAction('export')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Export
                        </button>"
                Exit Select
            Case "ConfirmDelete"
                label = "<button type='submit' onclick=""BtnAction('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Yes
                        </button>"
                Exit Select
            Case "No"
                label = "<button class='btn btn-sm btn-danger btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red2'></i>
                            No
                        </button>"
                Exit Select
            Case "FindAD"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='glyphicon glyphicon-search  bigger-110 green'></i>
                            Find
                        </button>"
                Exit Select
            Case "ActiveDirectory"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='glyphicon glyphicon-search  bigger-110 blue'></i>
                        </button>"
                Exit Select
            Case "Select"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil  bigger-110 blue'></i>
                            Select
                        </button>"
                Exit Select

            Case "Remove"
                label = "<button class='btn btn-sm btn-danger btn-white btn-round' onclick='DeleteRow(this)' title='Remove'>
                            <i class='ace-icon fa fa-times bigger-110 red2'></i>
                            Remove
                        </button>"
                Exit Select

            Case "RemoveRowFstBudget"
                label = "<button class='btn btn-sm btn-danger btn-white btn-round' onclick='DeleteRowFstBudget(this)' title='Remove'>
                            <i class='ace-icon fa fa-times bigger-110 red2'></i>
                            Remove
                        </button>"
                Exit Select

            Case "AddRowApprovalWa"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowApprovalWa()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select
            Case "AddRowApprovalRelDept"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowApprovalRelDept()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select
            Case "AddRowRelDept"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowRelDept()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select

            Case "AddRowFstBudgetAdditional"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowFstBudgetAdditional()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select

            Case "AddRowItem"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowItem()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select


            Case "GetDataPRBySubmitter"
                label = "<button type='submit' onclick=""GetDataPRBySubmitter()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select
            Case "AddRowItemPO"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowItemPO()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select

            Case "AddRowAcknowledgeUser"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowAcknowledgeUser()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select

            Case "AddRowChooseOneApproval"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowChooseOneApproval()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select

            Case "AddRowItemPC"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowItemPC()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select
            Case "AddRowItemPODetail"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' onclick='InsRowItemPODetail()' title='Add row'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Add Rows
                        </button>"
                Exit Select
            Case "SubmitPoDetail"
                label = "<button type='submit' onclick=""BtnActionPO('temp')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Ok 
                        </button>"
                Exit Select
            Case "OpenCrvDetail"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' title='Crv Details'>
                            <i class='ace-icon fa fa-search-plus bigger-110 green'></i>
                            CRV DETAILS
                        </button>"
                Exit Select

            Case "OpenPCDetail"
                label = "<button class='btn btn-sm btn-success btn-white btn-round' title='Pc Details'>
                            <i class='ace-icon fa fa-search-plus bigger-110 green'></i>
                            PC DETAILS
                        </button>"
                Exit Select

            Case "Verified"
                label = "<button type='submit' onclick=""BtnAction('Verified')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Verified 
                        </button>"
                Exit Select

            Case "Rejected"
                label = "<button type='submit' onclick=""BtnAction('Rejected')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Rejected 
                        </button>"
                Exit Select

            Case "RejectedByVerifier"
                label = "<button type='submit' onclick=""BtnAction('RejectedByVerifier')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Rejected By Verifier
                        </button>"
                Exit Select

            Case "RejectedByApprover"
                label = "<button type='submit' onclick=""BtnAction('RejectedByApprover')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Rejected By Approver
                        </button>"
                Exit Select


            Case "RejectedByAdminEproc"
                label = "<button type='submit' onclick=""BtnAction('RejectedByAdminEproc')"" class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                             Rejected By Admin Eproc
                        </button>"
                Exit Select

            Case "Reviewed"
                label = "<button type='submit' onclick=""BtnAction('Reviewed')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Reviewed 
                        </button>"
                Exit Select
            Case "Approved"
                label = "<button type='submit' onclick=""BtnAction('Approved')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Approved 
                        </button>"
                Exit Select
            Case "Handled"
                label = "<button type='submit' onclick=""BtnAction('Handled')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Handled 
                        </button>"
                Exit Select
            Case "Completed"
                label = "<button type='submit' onclick=""BtnAction('Completed')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Completed PO
                        </button>"
                Exit Select
            Case "Printing"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Print 
                        </button>"
                Exit Select

            Case "ActionExportToPdf"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            ExportToPdf 
                        </button>"
                Exit Select

            Case "ViewPdf"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            View Pdf 
                        </button>"
                Exit Select

            Case "ActionExportToTxt"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            ExportToTxt 
                        </button>"
                Exit Select

            Case "SentEmailToSupplier"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check-square-o bigger-110 green'></i>
                            Sent email to supplier 
                        </button>"
                Exit Select

            Case "GetDataByRowStat"
                label = "<button type='submit' onclick=""GetDataByRowStat()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data 
                        </button>"
                Exit Select

            Case "UpdateHibernate"
                label = "<button type='submit' onclick=""UpdateHibernate()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Update Hibernate
                        </button>"
                Exit Select


#Region "PR APPR ITEM/WORK AREA"
            Case "SaveItemPRApprove"
                label = "<button type='submit' onclick=""ActionApproveItem()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check bigger-110 green'></i>
                            Approve
                        </button>"
                Exit Select

            Case "SaveItemPRReview"
                label = "<button type='submit' onclick=""ActionReviewItem()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-eye bigger-110 green'></i>
                            Review
                        </button>"
                Exit Select

            Case "SaveItemPRRejectByReviewer"
                label = "<button type='submit' onclick=""ActionRejectItem('Waiting for review')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject
                        </button>"
                Exit Select

            Case "SaveItemPRRejectByApprover"
                label = "<button type='submit' onclick=""ActionRejectItem('Waiting for approve')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject
                        </button>"
                Exit Select
#End Region

#Region "PR APPR RELATED DEPARTMENT"
            Case "SaveRDPRApprove"
                label = "<button type='submit' onclick=""ActionApproveRD()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-check bigger-110 green'></i>
                            Approve
                        </button>"
                Exit Select

            Case "SaveRDPRReview"
                label = "<button type='submit' onclick=""ActionReviewRD()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-eye bigger-110 green'></i>
                            Review
                        </button>"
                Exit Select

            Case "SaveRDPRRejectByReviewer"
                label = "<button type='submit' onclick=""ActionRejectRD('Waiting for review')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject
                        </button>"
                Exit Select

            Case "SaveRDPRRejectByApprover"
                label = "<button type='submit' onclick=""ActionRejectRD('Waiting for approve')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-times bigger-110 red'></i>
                            Reject
                        </button>"
                Exit Select
#End Region

#Region "Handle"
            Case "SaveHandlePR"
                label = "<button type='submit' onclick=""ActionHandlePR()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Handle
                        </button>"
                Exit Select
#End Region

#Region "revise"
            Case "Revise"
                label = "<button class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 yellow'></i>
                            Revise
                        </button>"
                Exit Select

            Case "SaveRevise"
                label = "<button type='submit' onclick=""ActionSaveRevise()"" class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 yellow'></i>
                            Save Revise
                        </button>"
                Exit Select
#End Region

#Region "Edit PR (Remark dan po/supplier field)"
            Case "EditPRRemarkOrSupplier"
                label = "<button class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil bigger-110 purple'></i>
                            Edit
                        </button>"
                Exit Select

            Case "SaveEditPRRemarkOrSupplier"
                label = "<button type='submit' onclick=""ActionSaveEditPRRemarkOrSupplier()"" class='btn btn-sm btn-warning btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 purple'></i>
                            Save Edit
                        </button>"
                Exit Select
#End Region

#Region "Ready To Create PO"
            Case "SaveReadyToCreatePO"
                label = "<button type='submit' onclick=""ActionReadyToCreatePO()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-briefcase  bigger-110 green'></i>
                            Create PO
                        </button>"
                Exit Select
#End Region

#Region "Ready To Create Complete"
            Case "SaveReadyToComplete"
                label = "<button type='submit' onclick=""ActionReadyToComplete()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Complete
                        </button>"
                Exit Select
#End Region

#Region "Ready To Create Sign Off"
            Case "SignOff"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Sign Off
                        </button>"
                Exit Select
            Case "Finish"
                label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 blue'></i>
                            Finish
                        </button>"
                Exit Select
#End Region

#Region "Daily Operation"
            Case "GetDataDOStock"
                label = "<button type='submit' onclick=""GetDataDOStock()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select

            Case "GetDataDOStockSummary"
                label = "<button type='submit' onclick=""GetDataDOStockSummary()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select

            Case "GetDataDONonStock"
                label = "<button type='submit' onclick=""GetDataDONonStock()"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select
#End Region

#Region "Report"
            Case "GetTatComplete"
                label = "<button type='submit' onclick=""GetTat(0)"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select

            Case "GetTatSignOff"
                label = "<button type='submit' onclick=""GetTat(1)"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select

            Case "GetTatUnComplete"
                label = "<button type='submit' onclick=""GetTat(2)"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select

            Case "GetMyReportPrList"
                label = "<button type='submit' onclick=""GetReportPrList(0)"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get Data
                        </button>"
                Exit Select

            Case "GetAllReportPrList"
                label = "<button type='submit' onclick=""GetReportPrList(1)"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-gavel bigger-110 green'></i>
                            Get all data by admin
                        </button>"
                Exit Select

#End Region

#Region "Print PR"
            Case "PrintDetailHeader"
                label = "<button class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-print bigger-110 green'></i>
                            Print
                        </button>"
                Exit Select
#End Region

#Region "Request User"
            Case "SendRequestUserCreate"
                label = "<button type='submit' onclick=""BtnActionRequestUser('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestUserEdit"
                label = "<button type='submit' onclick=""BtnActionRequestUser('edit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestUserDelete"
                label = "<button type='submit' onclick=""BtnActionRequestUser('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-trash-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "ActionRequestUserComplete"
                label = "<button type='submit' onclick=""ActionRequestUserComplete()"" id='complete_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Complete Request
                        </button>"
                Exit Select
#End Region

#Region "Request WA"
            Case "SendRequestWACreate"
                label = "<button type='submit' onclick=""BtnActionRequestWA('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestWAEdit"
                label = "<button type='submit' onclick=""BtnActionRequestWA('edit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestWADelete"
                label = "<button type='submit' onclick=""BtnActionRequestWA('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-trash-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "ActionRequestWAComplete"
                label = "<button type='submit' onclick=""ActionRequestWAComplete()"" id='complete_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Complete Request
                        </button>"
                Exit Select

            Case "ActionRequestWAApprove"
                label = "<button type='submit' onclick=""ActionRequestWAApprove()"" id='approve_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Approve Request
                        </button>"
                Exit Select
#End Region

#Region "Request COA"
            Case "SendRequestCOACreate"
                label = "<button type='submit' onclick=""BtnActionRequestCOA('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestCOAEdit"
                label = "<button type='submit' onclick=""BtnActionRequestCOA('edit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestCOADelete"
                label = "<button type='submit' onclick=""BtnActionRequestCOA('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-trash-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "ActionRequestCOAComplete"
                label = "<button type='submit' onclick=""ActionRequestCOAComplete()"" id='complete_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Complete Request
                        </button>"
                Exit Select

            Case "ActionRequestCOAApprove"
                label = "<button type='submit' onclick=""ActionRequestCOAApprove()"" id='approve_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Approve Request
                        </button>"
                Exit Select
#End Region

#Region "Request RD"
            Case "SendRequestRDCreate"
                label = "<button type='submit' onclick=""BtnActionRequestRD('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestRDEdit"
                label = "<button type='submit' onclick=""BtnActionRequestRD('edit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestRDDelete"
                label = "<button type='submit' onclick=""BtnActionRequestRD('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-trash-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "ActionRequestRDComplete"
                label = "<button type='submit' onclick=""ActionRequestRDComplete()"" id='complete_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Complete Request
                        </button>"
                Exit Select

            Case "ActionRequestRDApprove"
                label = "<button type='submit' onclick=""ActionRequestRDApprove()"" id='approve_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Approve Request
                        </button>"
                Exit Select
#End Region

#Region "Request FST"
            Case "SendRequestFSTCreate"
                label = "<button type='submit' onclick=""BtnActionRequestFST('create')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestFSTEdit"
                label = "<button type='submit' onclick=""BtnActionRequestFST('edit')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-pencil bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "SendRequestFSTDelete"
                label = "<button type='submit' onclick=""BtnActionRequestFST('delete')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-trash-o bigger-110 green'></i>
                            Send Request
                        </button>"
                Exit Select

            Case "ActionRequestFSTComplete"
                label = "<button type='submit' onclick=""ActionRequestFSTComplete()"" id='complete_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Complete Request
                        </button>"
                Exit Select

            Case "ActionRequestFSTApprove"
                label = "<button type='submit' onclick=""ActionRequestFSTApprove()"" id='approve_request' class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-floppy-o bigger-110 green'></i>
                            Approve Request
                        </button>"
                Exit Select
#End Region

#Region "CRV"
            Case "SaveReceived"
                label = "<button type='submit' onclick=""BtnAction('received')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-arrow-right bigger-110 green'></i>
                            Receive
                        </button>"
                Exit Select
            Case "SavePaid"
                label = "<button type='submit' onclick=""BtnAction('paid')"" class='btn btn-sm btn-success btn-white btn-round'>
                            <i class='ace-icon fa fa-dollar bigger-110 green'></i>
                            Paid
                        </button>"
                Exit Select
#End Region

        End Select
        Return label
    End Function

End Class