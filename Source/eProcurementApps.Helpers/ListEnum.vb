Public Class ListEnum
#Region "PR STATUS"
    Public Enum PRStatus As Integer
        Submitted = 0
        PrRejected = 1
        PrApprovedComplete = 2
        PrHandled = 3
        CreatePo = 4
        PoApproved = 5
        PoRejected = 6
        PoSent = 7
        GoodReceive = 8
        Complete = 9
        SignOff = 10
        Revise = 11
        Edit = 12
    End Enum

#End Region

#Region "STATUS REQUES INDICATOR"
    Public Enum ReqIndc As Integer
        ExtraMiles = 0
        Standard = 1
        Ideal = 2
    End Enum
#End Region

#Region "ITEM IN PR"
    Public Enum ItemStatus As Integer
        Submitted = 0
        ReadyToApprove = 1
        Complete = 2
        Rejected = 3
    End Enum

    Public Enum ApprItemStatus As Integer
        Reviewed = 0
        Approved = 1
        Rejected = 2
        ReviewWA = 3
        ApproveWA = 4
        RejectWA = 5
    End Enum
#End Region

#Region "RELATED DEPARTMENT IN PR"
    Public Enum RDStatus As Integer
        Submitted = 0
        ReadyToApprove = 1
        Complete = 2
        Rejected = 3
    End Enum

    Public Enum ApprRDStatus As Integer
        Reviewed = 0
        Approved = 1
        Rejected = 2
        ReviewRD = 3
        ApproveRD = 4
        RejectRD = 5
    End Enum
#End Region

#Region "REQUEST"
    Public Enum Request As Integer
        Submitted = 0
        NeedApprove = 1
        Approved = 2
        NeedComplete = 3
        Completed = 4
        NeedReview = 5
        Reviewed = 6
    End Enum
#End Region

#Region "ENUM FOR EMAIL EXECUTOR"
    Public Enum eProcApprAction As Integer
        review = 0
        approve = 1
        handle = 2
        verify = 3
        complete = 4
    End Enum
#End Region

#Region "FLAG DETAIL"
    Public Enum FlagDetail As Integer
        MyListPR = 0
        MyListApprovalPRWA = 1
        MyListApprovalPRRD = 2
        AllListPR = 3
        PRsReadyToHandle = 4
        PRsReadyToCreatePO = 5
        ListPRBySubmitter = 6
        ListPRComplete = 7
        ListPRSignOff = 8
        ListPRReject = 9
        PRsReadyToComplete = 10
        PRsReadyToSignOff = 11
        PRPrinting = 12
        PRRevise = 13
        MyPRReadyToSignOff = 14
        PREdit = 15
    End Enum
#End Region

#Region "INBOX"
    Public Enum FlagInbox As Integer
        ApprWA = 100
        ApprRD = 101
        ApprReq = 102
        CompReq = 103
        ApprAckPC = 104
        ApprPC = 105
        ApprPO = 106
        VrfPC = 107
        VrfPO = 108
        ApprGM = 109
        VrCRV = 110
        ApprCRV = 111
        OnlyView = 112
    End Enum

#End Region

#Region "REPORT"
    Public Enum FlagReport As Integer
        TatComplete = 0
        TatSignOff = 1
        TatUnComplete = 2
    End Enum

#End Region

#Region "General"
    Public Enum RowStat As Integer
        Ilegal = -3
        Hibernate = -2
        InActive = -1
        Live = 0
        Create = 1
        Edit = 2
        Delete = 3
    End Enum
#End Region

#Region "PriceCom"
    Public Enum PriceCom As Integer
        Submitted = 0
        Verified = 1
        ApprovedByAcknowledge = 2
        Reviewed = 3
        Approved = 4
        Completed = 5
        Rejected = 6
    End Enum

    Public Enum ViewPageArea As Integer
        Viewed = 1
        AcknowledgeUser = 2
        Approval = 3
        Complete = 4
        Verify = 5
    End Enum
#End Region

#Region "PO"
    Public Enum PO As Integer
        Submitted = 0
        Verified = 1
        Reviewed = 2
        Approved = 3
        Completed = 4
        Closed = 5
        Rejected = 6
    End Enum
#End Region

#Region "Approval Role"
    Public Enum ApprovalRole As Integer
        PC = 1
        CRV = 2
        PO = 3
    End Enum

#End Region

#Region "CRV"
    Public Enum Crv As Integer
        Rejected = -2
        InActive = -1
        Active = 0
        Verify = 1
        Approve = 2
        Received = 3
        Paid = 4
        Complete = 5
    End Enum
#End Region


#Region "GM"
    Public Enum Gm As Integer
        Rejected = -2
        InActive = -1
        Active = 0
        Approve = 1
        Complete = 2
    End Enum
#End Region

End Class
