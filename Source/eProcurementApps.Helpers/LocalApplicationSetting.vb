Imports System.Configuration

Public Class LocalApplicationSetting
    Inherits ConfigurationSection

    Public Const BANNER_BRANCH_NONE = "None"
    Public Const BANNER_BRANCH_SINGLE = "Single"
    Public Const BANNER_BRANCH_MULTIPLE = "Multiple"

    Public Const CHANNEL_AGENT = "AGENT"
    Public Const CHANNEL_BANK = "BANK"
    Public Const CHANNEL_BROKER = "BROKER"

    Private Shared _localAppSetting As LocalApplicationSetting = Nothing

    Public Shared Function GetInstance() As LocalApplicationSetting
        If (_localAppSetting Is Nothing) Then
            _localAppSetting = ConfigurationManager.GetSection("LocalApplicationSetting")
        End If
        Return _localAppSetting
    End Function

    <ConfigurationProperty("BannerImagePhysicalPath")>
    Public Property BannerImagePhysicalPath As String
        Get
            Return ("" & Me("BannerImagePhysicalPath"))
        End Get
        Set(ByVal value As String)
            Me("BannerImagePhysicalPath") = value
        End Set
    End Property

    <ConfigurationProperty("BannerSettingFilePath")>
    Public Property BannerSettingFilePath As String
        Get
            Return ("" & Me("BannerSettingFilePath"))
        End Get
        Set(ByVal value As String)
            Me("BannerSettingFilePath") = value
        End Set
    End Property

    <ConfigurationProperty("SiteDistributionChannel", DefaultValue:=LocalApplicationSetting.CHANNEL_AGENT)>
    Public Property SiteDistributionChannel As String
        Get
            Return ("" & Me("SiteDistributionChannel"))
        End Get
        Set(ByVal value As String)
            Me("SiteDistributionChannel") = value
        End Set
    End Property

    <ConfigurationProperty("DateFormat", DefaultValue:="MM/dd/yyyy")>
    Public Property DateFormat As String
        Get
            Return Me("DateFormat")
        End Get
        Set(ByVal value As String)
            Me("DateFormat") = value
        End Set
    End Property

    <ConfigurationProperty("DateTimeFormat", DefaultValue:="dd-MMM-yyyy HH:mm")>
    Public Property DateTimeFormat As String
        Get
            Return Me("DateTimeFormat")
        End Get
        Set(ByVal value As String)
            Me("DateTimeFormat") = value
        End Set
    End Property

    <ConfigurationProperty("DatePickerDateFormat", DefaultValue:="mm/dd/yy")>
    Public Property DatePickerDateFormat As String
        Get
            Return Me("DatePickerDateFormat")
        End Get
        Set(ByVal value As String)
            Me("DatePickerDateFormat") = value
        End Set
    End Property

    <ConfigurationProperty("TimePickerTimeFormat", DefaultValue:="HH:mm")>
    Public Property TimePickerTimeFormat As String
        Get
            Return Me("TimePickerTimeFormat")
        End Get
        Set(ByVal value As String)
            Me("TimePickerTimeFormat") = value
        End Set
    End Property

    <ConfigurationProperty("SensitiveInfomationController", DefaultValue:="")>
    Public Property SensitiveInfomationController As String
        Get
            Return ("" & Me("SensitiveInfomationController"))
        End Get
        Set(ByVal value As String)
            Me("SensitiveInfomationController") = value
        End Set
    End Property

    <ConfigurationProperty("CustomerSearchResultDisplayRows", DefaultValue:=20)>
    Public Property CustomerSearchResultDisplayRows As Integer
        Get
            Return Me("CustomerSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("CustomerSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("ProductivitySearchResultDisplayRows", DefaultValue:=20)>
    Public Property ProductivitySearchResultDisplayRows As Integer
        Get
            Return Me("ProductivitySearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("ProductivitySearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("AnsSearchResultDisplayRows", DefaultValue:=20)>
    Public Property AnsSearchResultDisplayRows As Integer
        Get
            Return Me("AnsSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("AnsSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("WipSearchResultDisplayRows", DefaultValue:=20)>
    Public Property WipSearchResultDisplayRows As Integer
        Get
            Return Me("WipSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("WipSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("DefaultSearchResultDisplayRows", DefaultValue:=20)>
    Public Property DefaultSearchResultDisplayRows As Integer
        Get
            Return Me("DefaultSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("DefaultSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("IncludeSubordinate", DefaultValue:=False)>
    Public Property IncludeSubordinate As Boolean
        Get
            Return Me("IncludeSubordinate")
        End Get
        Set(ByVal value As Boolean)
            Me("IncludeSubordinate") = value
        End Set
    End Property

    <ConfigurationProperty("BannerBranchDimension", DefaultValue:=LocalApplicationSetting.BANNER_BRANCH_NONE)>
    Public Property BannerBranchDimension As String
        Get
            Return Me("BannerBranchDimension")
        End Get
        Set(ByVal value As String)
            Me("BannerBranchDimension") = value
        End Set
    End Property

    <ConfigurationProperty("BannerSize", DefaultValue:=750000L)>
    Public Property BannerSize As Long
        Get
            Return Me("BannerSize")
        End Get
        Set(ByVal value As Long)
            Me("BannerSize") = value
        End Set
    End Property

    <ConfigurationProperty("ContentSearchResultDisplayRows", DefaultValue:=20)>
    Public Property ContentSearchResultDisplayRows As Integer
        Get
            Return Me("ContentSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("ContentSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("IsEmailPopUp", DefaultValue:=False)>
    Public Property IsEmailPopUp As Boolean
        Get
            Return Me("IsEmailPopUp")
        End Get
        Set(ByVal value As Boolean)
            Me("IsEmailPopUp") = value
        End Set
    End Property

    <ConfigurationProperty("IsInternalSite", DefaultValue:=False)>
    Public Property IsInternalSite As Boolean
        Get
            Return Me("IsInternalSite")
        End Get
        Set(ByVal value As Boolean)
            Me("IsInternalSite") = value
        End Set
    End Property

    <ConfigurationProperty("StaffLoginUrl", DefaultValue:="~/StaffLogin/Login")>
    Public Property StaffLoginUrl As String
        Get
            Return Me("StaffLoginUrl")
        End Get
        Set(ByVal value As String)
            Me("StaffLoginUrl") = value
        End Set
    End Property

    <ConfigurationProperty("SecretaryChangePasswordUrl", DefaultValue:="~/Secretary/ChangePassword")>
    Public Property SecretaryChangePasswordUrl As String
        Get
            Return Me("SecretaryChangePasswordUrl")
        End Get
        Set(ByVal value As String)
            Me("SecretaryChangePasswordUrl") = value
        End Set
    End Property

    <ConfigurationProperty("EApplicationWebUrl", DefaultValue:="")>
    Public Property EApplicationWebUrl As String
        Get
            Return Me("EApplicationWebUrl")
        End Get
        Set(ByVal value As String)
            Me("EApplicationWebUrl") = value
        End Set
    End Property

    <ConfigurationProperty("WebsiteMailWebUrl", DefaultValue:="")>
    Public Property WebsiteMailWebUrl As String
        Get
            Return Me("WebsiteMailWebUrl")
        End Get
        Set(ByVal value As String)
            Me("WebsiteMailWebUrl") = value
        End Set
    End Property

    <ConfigurationProperty("EApplicationJavascriptUrl", DefaultValue:="")>
    Public Property EApplicationJavascriptUrl As String
        Get
            Return Me("EApplicationJavascriptUrl")
        End Get
        Set(ByVal value As String)
            Me("EApplicationJavascriptUrl") = value
        End Set
    End Property

    <ConfigurationProperty("ContactUsUrl", DefaultValue:="")>
    Public Property ContactUsUrl As String
        Get
            Return Me("ContactUsUrl")
        End Get
        Set(ByVal value As String)
            Me("ContactUsUrl") = value
        End Set
    End Property

    <ConfigurationProperty("EmailProxy", DefaultValue:="MLILHKAPP02")>
    Public Property EmailProxy As String
        Get
            Return Me("EmailProxy")
        End Get
        Set(ByVal value As String)
            Me("EmailProxy") = value
        End Set
    End Property

    <ConfigurationProperty("EmailPort", DefaultValue:="25")>
    Public Property EmailPort As String
        Get
            Return Me("EmailPort")
        End Get
        Set(ByVal value As String)
            Me("EmailPort") = value
        End Set
    End Property

    <ConfigurationProperty("EmailSupport", DefaultValue:="Y")>
    Public Property EmailSupport As String
        Get
            Return Me("EmailSupport")
        End Get
        Set(ByVal value As String)
            Me("EmailSupport") = value
        End Set
    End Property

    <ConfigurationProperty("CompressionTempLocation", DefaultValue:="C:/temp")>
    Public Property CompressionTempLocation As String
        Get
            Return Me("CompressionTempLocation")
        End Get
        Set(ByVal value As String)
            Me("CompressionTempLocation") = value
        End Set
    End Property

    <ConfigurationProperty("CompressionLevel", DefaultValue:="5")>
    Public Property CompressionLevel As String
        Get
            Return Me("CompressionLevel")
        End Get
        Set(ByVal value As String)
            Me("CompressionLevel") = value
        End Set
    End Property

    <ConfigurationProperty("CompressionPassword", DefaultValue:="password")>
    Public Property CompressionPassword As String
        Get
            Return Me("CompressionPassword")
        End Get
        Set(ByVal value As String)
            Me("CompressionPassword") = value
        End Set
    End Property

    <ConfigurationProperty("PretendAfterLogin", DefaultValue:="")>
    Public Property PretendAfterLogin As String
        Get
            Return Me("PretendAfterLogin")
        End Get
        Set(ByVal value As String)
            Me("PretendAfterLogin") = value
        End Set
    End Property

    '<ConfigurationProperty("DefaultSiteLanguage", DefaultValue:=Manulife.Core.Mvc.Views.EnhancedViewEngine.LANGUAGE_EN)>
    'Public Property DefaultSiteLanguage As String
    '    Get
    '        Return Me("DefaultSiteLanguage")
    '    End Get
    '    Set(ByVal value As String)
    '        Me("DefaultSiteLanguage") = value
    '    End Set
    'End Property

    <ConfigurationProperty("EAppsSearchResultDisplayRows", DefaultValue:=10)>
    Public Property EAppsSearchResultDisplayRows As Integer
        Get
            Return Me("EAppsSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("EAppsSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("LogoutUrl", DefaultValue:="")>
    Public Property LogoutUrl As String
        Get
            Return Me("LogoutUrl")
        End Get
        Set(ByVal value As String)
            Me("LogoutUrl") = value
        End Set
    End Property

    <ConfigurationProperty("TimeoutUrl", DefaultValue:="")>
    Public Property TimeoutUrl As String
        Get
            Return Me("TimeoutUrl")
        End Get
        Set(ByVal value As String)
            Me("TimeoutUrl") = value
        End Set
    End Property

    <ConfigurationProperty("DialogResultDisplayRows", DefaultValue:=10)>
    Public Property DialogResultDisplayRows As Integer
        Get
            Return Me("DialogResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("DialogResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("ProspectToDoRetentionPeriodInDays", DefaultValue:=365)>
    Public Property ProspectToDoRetentionPeriodInDays As Integer
        Get
            Return Me("ProspectToDoRetentionPeriodInDays")
        End Get
        Set(ByVal value As Integer)
            Me("ProspectToDoRetentionPeriodInDays") = value
        End Set
    End Property

    <ConfigurationProperty("ProspectAppointmentRetentionPeriodInDays", DefaultValue:=365)>
    Public Property ProspectAppointmentRetentionPeriodInDays As Integer
        Get
            Return Me("ProspectAppointmentRetentionPeriodInDays")
        End Get
        Set(ByVal value As Integer)
            Me("ProspectAppointmentRetentionPeriodInDays") = value
        End Set
    End Property

    <ConfigurationProperty("ProspectProposalRetentionPeriodInDays", DefaultValue:=365)>
    Public Property ProspectProposalRetentionPeriodInDays As Integer
        Get
            Return Me("ProspectProposalRetentionPeriodInDays")
        End Get
        Set(ByVal value As Integer)
            Me("ProspectProposalRetentionPeriodInDays") = value
        End Set
    End Property

    <ConfigurationProperty("ProspectApplicationRetentionPeriodInDays", DefaultValue:=365)>
    Public Property ProspectApplicationRetentionPeriodInDays As Integer
        Get
            Return Me("ProspectApplicationRetentionPeriodInDays")
        End Get
        Set(ByVal value As Integer)
            Me("ProspectApplicationRetentionPeriodInDays") = value
        End Set
    End Property

    <ConfigurationProperty("ProspectActivityRetentionPeriodInDays", DefaultValue:=365)>
    Public Property ProspectActivityRetentionPeriodInDays As Integer
        Get
            Return Me("ProspectActivityRetentionPeriodInDays")
        End Get
        Set(ByVal value As Integer)
            Me("ProspectActivityRetentionPeriodInDays") = value
        End Set
    End Property

    <ConfigurationProperty("ProposalUserRolesWorkingWithOtherAgent", DefaultValue:="")>
    Public Property ProposalUserRolesWorkingWithOtherAgent As String
        Get
            Return Me("ProposalUserRolesWorkingWithOtherAgent")
        End Get
        Set(ByVal value As String)
            Me("ProposalUserRolesWorkingWithOtherAgent") = value
        End Set
    End Property

    <ConfigurationProperty("CarryClientInformationToProposal", DefaultValue:=False)>
    Public Property CarryClientInformationToProposal As Boolean
        Get
            Return Me("CarryClientInformationToProposal")
        End Get
        Set(ByVal value As Boolean)
            Me("CarryClientInformationToProposal") = value
        End Set
    End Property

    <ConfigurationProperty("ShowEnhancedClientInformationScreen", DefaultValue:=False)>
    Public Property ShowEnhancedClientInformationScreen As Boolean
        Get
            Return Me("ShowEnhancedClientInformationScreen")
        End Get
        Set(ByVal value As Boolean)
            Me("ShowEnhancedClientInformationScreen") = value
        End Set
    End Property

    <ConfigurationProperty("ShowProposalPreviewShortcut", DefaultValue:=False)>
    Public Property ShowProposalPreviewShortcut As Boolean
        Get
            Return Me("ShowProposalPreviewShortcut")
        End Get
        Set(ByVal value As Boolean)
            Me("ShowProposalPreviewShortcut") = value
        End Set
    End Property

    <ConfigurationProperty("EnableProposalCompression", DefaultValue:=False)>
    Public Property EnableProposalCompression As Boolean
        Get
            Return Me("EnableProposalCompression")
        End Get
        Set(ByVal value As Boolean)
            Me("EnableProposalCompression") = value
        End Set
    End Property

    <ConfigurationProperty("PermanentSessionObjects", DefaultValue:="")>
    Public Property PermanentSessionObjects As String
        Get
            Return Me("PermanentSessionObjects")
        End Get
        Set(ByVal value As String)
            Me("PermanentSessionObjects") = value
        End Set
    End Property

    <ConfigurationProperty("ProposalShowChartOption", DefaultValue:=False)>
    Public Property ProposalShowChartOption As Boolean
        Get
            Return Me("ProposalShowChartOption")
        End Get
        Set(ByVal value As Boolean)
            Me("ProposalShowChartOption") = value
        End Set
    End Property

    <ConfigurationProperty("ToDoAssignmentSearchResultDisplayRows", DefaultValue:=20)>
    Public Property ToDoAssignmentSearchResultDisplayRows As Integer
        Get
            Return Me("ToDoAssignmentSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("ToDoAssignmentSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("WorksiteClientPageDisplayRows", DefaultValue:=50)>
    Public Property WorksiteClientPageDisplayRows As Integer
        Get
            Return Me("WorksiteClientPageDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("WorksiteClientPageDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("HttpAsynchronousWebRequestProxy", DefaultValue:="10.129.255.41")>
    Public Property HttpAsynchronousWebRequestProxy As String
        Get
            Return Me("HttpAsynchronousWebRequestProxy")
        End Get
        Set(ByVal value As String)
            Me("HttpAsynchronousWebRequestProxy") = value
        End Set
    End Property

    <ConfigurationProperty("HttpAsynchronousWebRequestProxyPort", DefaultValue:=8080)>
    Public Property HttpAsynchronousWebRequestProxyPort As Integer
        Get
            Return Me("HttpAsynchronousWebRequestProxyPort")
        End Get
        Set(ByVal value As Integer)
            Me("HttpAsynchronousWebRequestProxyPort") = value
        End Set
    End Property

    <ConfigurationProperty("HttpAsynchronousWebRequestTimeout", DefaultValue:=15)>
    Public Property HttpAsynchronousWebRequestTimeout As Integer
        Get
            Return Me("HttpAsynchronousWebRequestTimeout")
        End Get
        Set(ByVal value As Integer)
            Me("HttpAsynchronousWebRequestTimeout") = value
        End Set
    End Property

    <ConfigurationProperty("BPMServiceEndpointAddress", DefaultValue:="")>
    Public Property BPMServiceEndpointAddress As String
        Get
            Return Me("BPMServiceEndpointAddress")
        End Get
        Set(ByVal value As String)
            Me("BPMServiceEndpointAddress") = value
        End Set
    End Property

    <ConfigurationProperty("BPMServiceEndpointIsSPAJ", DefaultValue:="Y")>
    Public Property BPMServiceEndpointIsSPAJ As String
        Get
            Return Me("BPMServiceEndpointIsSPAJ")
        End Get
        Set(ByVal value As String)
            Me("BPMServiceEndpointIsSPAJ") = value
        End Set
    End Property

    <ConfigurationProperty("BPMServiceEndpointFileStatus", DefaultValue:="1")>
    Public Property BPMServiceEndpointFileStatus As String
        Get
            Return Me("BPMServiceEndpointFileStatus")
        End Get
        Set(ByVal value As String)
            Me("BPMServiceEndpointFileStatus") = value
        End Set
    End Property

    <ConfigurationProperty("BPMUserGenerateReport", DefaultValue:="")>
    Public Property BPMUserGenerateReport As String
        Get
            Return Me("BPMUserGenerateReport")
        End Get
        Set(ByVal value As String)
            Me("BPMUserGenerateReport") = value
        End Set
    End Property

    <ConfigurationProperty("BPMPasswordGenerateReport", DefaultValue:="")>
    Public Property BPMPasswordGenerateReport As String
        Get
            Return Me("BPMPasswordGenerateReport")
        End Get
        Set(ByVal value As String)
            Me("BPMPasswordGenerateReport") = value
        End Set
    End Property

    <ConfigurationProperty("DistributionChannelExportToIEDMS", DefaultValue:="")>
    Public Property DistributionChannelExportToIEDMS As String
        Get
            Return Me("DistributionChannelExportToIEDMS")
        End Get
        Set(ByVal value As String)
            Me("DistributionChannelExportToIEDMS") = value
        End Set

    End Property

    <ConfigurationProperty("DistributionChannelRequiredReferenceNumber", DefaultValue:="")>
    Public Property DistributionChannelRequiredReferenceNumber As String
        Get
            Return Me("DistributionChannelRequiredReferenceNumber")
        End Get
        Set(ByVal value As String)
            Me("DistributionChannelRequiredReferenceNumber") = value
        End Set

    End Property

    <ConfigurationProperty("StaffKillSessionUrl", DefaultValue:="~/StaffLogin/KillSession")>
    Public Property StaffKillSessionUrl As String
        Get
            Return Me("StaffKillSessionUrl")
        End Get
        Set(ByVal value As String)
            Me("StaffKillSessionUrl") = value
        End Set
    End Property

    <ConfigurationProperty("LMSSearchResultDisplayRows", DefaultValue:=20)>
    Public Property LMSSearchResultDisplayRows As Integer
        Get
            Return Me("LMSSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("LMSSearchResultDisplayRows") = value
        End Set
    End Property

    <ConfigurationProperty("TrainerAgentCodeDemo", DefaultValue:="99B089")>
    Public Property TrainerAgentCodeDemo As String
        Get
            Return Me("TrainerAgentCodeDemo")
        End Get
        Set(ByVal value As String)
            Me("TrainerAgentCodeDemo") = value
        End Set
    End Property

    <ConfigurationProperty("BPMPlanPackage", DefaultValue:="MVAP0;")>
    Public Property BPMPlanPackage As String
        Get
            Return Me("BPMPlanPackage")
        End Get
        Set(ByVal value As String)
            Me("BPMPlanPackage") = value
        End Set
    End Property

    <ConfigurationProperty("CIRiderType", DefaultValue:="MCI75;MCC75;")>
    Public Property CIRiderType As String
        Get
            Return Me("CIRiderType")
        End Get
        Set(ByVal value As String)
            Me("CIRiderType") = value
        End Set
    End Property

#Region "ERecruitment"
    <ConfigurationProperty("ERecruitmentSearchResultDisplayRows", DefaultValue:=20)>
    Public Property ERecruitmentSearchResultDisplayRows As Integer
        Get
            Return Me("ERecruitmentSearchResultDisplayRows")
        End Get
        Set(ByVal value As Integer)
            Me("ERecruitmentSearchResultDisplayRows") = value
        End Set
    End Property
#End Region

End Class
