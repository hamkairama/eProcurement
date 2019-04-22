Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class PR_HEADER_PARAM
    Private Shared m_PRHeaderId As Integer
    Private Shared m_PRFlag As String

    Private Shared m_PRHeaderId_temp As Integer
    Private Shared m_PRNoCompleted_temp As String
    Private Shared m_PRFlag_temp As String

    Public Property PRHeaderId() As Integer
        Get
            Return m_PRHeaderId
        End Get
        Set
            m_PRHeaderId = Value
        End Set
    End Property

    Public Property PRFlag() As String
        Get
            Return m_PRFlag
        End Get
        Set
            m_PRFlag = Value
        End Set
    End Property

    Public Property PRNoCompleted_temp As String
        Get
            Return m_PRNoCompleted_temp
        End Get
        Set
            m_PRNoCompleted_temp = Value
        End Set
    End Property

    Public Property PRHeaderId_temp As Integer
        Get
            Return m_PRHeaderId_temp
        End Get
        Set
            m_PRHeaderId_temp = Value
        End Set
    End Property

    Public Shared ReadOnly Property GetPRHeaderId() As Integer
        Get
            Return m_PRHeaderId
        End Get
    End Property

    Public Shared ReadOnly Property GetPRFlag() As Integer
        Get
            Return m_PRFlag
        End Get
    End Property

    Public Shared ReadOnly Property GetPRNoCompleted_Temp() As String
        Get
            Return m_PRNoCompleted_temp
        End Get
    End Property

    Public Shared ReadOnly Property GetPRHeaderId_Temp() As String
        Get
            Return m_PRHeaderId_temp
        End Get
    End Property

    Public Property PRFlag_temp As String
        Get
            Return m_PRFlag_temp
        End Get
        Set
            m_PRFlag_temp = Value
        End Set
    End Property

    Public Shared ReadOnly Property GetFlag_Temp() As String
        Get
            Return m_PRFlag_temp
        End Get
    End Property
End Class
