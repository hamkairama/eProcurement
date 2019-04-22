Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class MAIL_DATA
    Private _sender As String
    Private _cc As String
    Private _bcc As String
    Private _recipients As String
    Private _subject As String
    Private _message As String

    Public Property Sender() As String
        Get
            Return Me._sender
        End Get
        Set
            Me._sender = Value
        End Set
    End Property
    Public Property Cc() As String
        Get
            Return Me._cc
        End Get
        Set
            Me._cc = Value
        End Set
    End Property
    Public Property Bcc() As String
        Get
            Return Me._bcc
        End Get
        Set
            Me._bcc = Value
        End Set
    End Property

    Public Property Recipients() As String
        Get
            Return Me._recipients
        End Get
        Set
            Me._recipients = Value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return Me._subject
        End Get
        Set
            Me._subject = Value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return Me._message
        End Get
        Set
            Me._message = Value
        End Set
    End Property

End Class

