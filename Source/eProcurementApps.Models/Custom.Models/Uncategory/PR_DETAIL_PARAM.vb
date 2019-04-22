Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class PR_DETAIL_PARAM
    Private Shared m_PRDetialId As Integer
    Private Shared m_PRDetailName As String

    Public Property PRDetialId() As Integer
        Get
            Return m_PRDetialId
        End Get
        Set
            m_PRDetialId = Value
        End Set
    End Property

    Public Property PRDetailName() As String
        Get
            Return m_PRDetailName
        End Get
        Set
            m_PRDetailName = Value
        End Set
    End Property

    Public Shared ReadOnly Property GetDetailId() As Integer
        Get
            Return m_PRDetialId
        End Get
    End Property

    Public Shared ReadOnly Property GetDetailName() As String
        Get
            Return m_PRDetailName
        End Get
    End Property


End Class
