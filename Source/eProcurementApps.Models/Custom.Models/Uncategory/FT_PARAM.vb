Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class FT_PARAM
    Private Shared m_FTId As Integer
    Private Shared m_FTValue As String

    Public Property FTId() As Integer
        Get
            Return m_FTId
        End Get
        Set
            m_FTId = Value
        End Set
    End Property

    Public Property FTValue() As String
        Get
            Return m_FTValue
        End Get
        Set
            m_FTValue = Value
        End Set
    End Property


    Public Shared ReadOnly Property GetFTId() As Integer
        Get
            Return m_FTId
        End Get
    End Property

    Public Shared ReadOnly Property GetFTValue() As String
        Get
            Return m_FTValue
        End Get
    End Property
End Class
