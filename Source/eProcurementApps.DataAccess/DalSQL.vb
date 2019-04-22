' Decompiled with JetBrains decompiler
' Type: dal.dal
' Assembly: dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 63E2E02F-82CD-4EA6-9379-AA94CC96FCC3
' Assembly location: D:\Users\hamkair\Desktop\Dal.dll

Imports System.Configuration
Imports System.Data.Entity.Core.EntityClient
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports Oracle.ManagedDataAccess.Client

Public Class DalSQL
    Private mvar_conn As String
    Private mvar_return_val As String
    Private oConn As SqlConnection

    Public Property RETURN_VALU() As String
        Get
            Return Me.mvar_return_val
        End Get
        Set
            Me.mvar_return_val = Value
        End Set
    End Property

    Public Property p_constring() As String
        Get
            Return Me.mvar_conn
        End Get
        Set
            Me.mvar_conn = Value
        End Set
    End Property

    Private ReadOnly Property p_GetConStringRegistry(Entities As String) As Object
        Get
            Return DirectCast(Me.f_getConString(Entities), Object)
        End Get
    End Property

    Public Sub New()
        Me.oConn = New SqlConnection()
    End Sub

    Protected Function WriteToEventLog(Entry As String, Optional AppName As String = "VB.NET Application", Optional EventType As EventLogEntryType = EventLogEntryType.Information, Optional LogName As String = "Application") As Boolean
        Dim eventLog__1 As New EventLog()
        Dim flag As Boolean
        Try
            If Not EventLog.SourceExists(AppName) Then
                EventLog.CreateEventSource(AppName, LogName)
            End If
            eventLog__1.Source = AppName
            eventLog__1.WriteEntry(Entry, EventType)
            flag = True
        Catch ex As Exception
            ProjectData.SetProjectError(ex)
            flag = False
            ProjectData.ClearProjectError()
        End Try
        Return flag
    End Function

    Protected Sub sbCloseConnection()
        If Me.oConn.State <> ConnectionState.Open Then
            Return
        End If
        Me.oConn.Close()
    End Sub

    Public Function fn_GetSQLDataReaderByQuery(query As String, ByRef sqldr As SqlDataReader, Entities As String) As Long
        Dim num1 As Long = 0
        Dim cmd As New SqlCommand()
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        End If

        Me.oConn = New SqlConnection(Conversions.ToString(Me.p_GetConStringRegistry(Entities)))
        Try
            Me.oConn.Open()
        Catch ex As Exception
            ProjectData.SetProjectError(ex)
            Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
            ProjectData.ClearProjectError()
            GoTo label_6
        End Try

        cmd.Connection = Me.oConn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = query

        sqldr = cmd.ExecuteReader()

        Dim num2 As Long = num1
label_6:
        Return num2
    End Function


    Private Function f_getConString(Entities As String) As String
        Dim curCon As String = BaseConnstringSql.ConnString(Entities)

        Dim sConnectionString As String = curCon
        Return sConnectionString
    End Function

End Class


