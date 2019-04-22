Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class PO_HEADER_PARAM
    Private Shared m_POHeaderId As Integer
    Private Shared m_PONumber As String

    Public Property POHeaderId() As Integer
        Get
            Return m_POHeaderId
        End Get
        Set
            m_POHeaderId = Value
        End Set
    End Property

    Public Property PONumber() As String
        Get
            Return m_PONumber
        End Get
        Set
            m_PONumber = Value
        End Set
    End Property


    Public Shared ReadOnly Property GetPOHeaderId() As Integer
        Get
            Return m_POHeaderId
        End Get
    End Property

    Public Shared ReadOnly Property GetPONumber() As String
        Get
            Return m_PONumber
        End Get
    End Property
End Class
