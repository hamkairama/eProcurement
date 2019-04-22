Imports System.Configuration

Public NotInheritable Class WebConfigKey
    Private Sub New()
    End Sub
    Public Shared ReadOnly Property ImgLoading() As String
        Get
            Return ConfigurationSettings.AppSettings("ImgLoading").ToString()
        End Get
    End Property
    Public Shared ReadOnly Property KeyUrl() As String
        Get
            Return ConfigurationSettings.AppSettings("KeyUrl").ToString()
        End Get
    End Property
    Public Shared ReadOnly Property templateName() As String
        Get
            Return ConfigurationSettings.AppSettings("templateName").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property Environment() As String
        Get
            Return ConfigurationSettings.AppSettings("Environment").ToString()
        End Get
    End Property

End Class