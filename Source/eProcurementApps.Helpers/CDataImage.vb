Imports System.Web
Imports eProcurementApps.Models

Public Class CDataImage
    Private Shared _getImageFile As HttpPostedFileBase
    Private Shared _getImageFiles As IEnumerable(Of HttpPostedFileBase)
    Private Shared _getImageByte As Byte()
    Public Shared ReadOnly Property GetImageFile() As HttpPostedFileBase
        Get
            Return CDataImage._getImageFile
        End Get
        'Set
        '    CDataImage._getImageFile = Value
        'End Set
    End Property

    Public Shared ReadOnly Property GetImageFiles() As IEnumerable(Of HttpPostedFileBase)
        Get
            Return CDataImage._getImageFiles
        End Get
        'Set
        '    CDataImage._getImageFiles = Value
        'End Set
    End Property

    Public Shared ReadOnly Property GetImageByte() As Byte()
        Get
            Return CDataImage._getImageByte
        End Get
        'Set
        '    CDataImage._getImageByte = Value
        'End Set
    End Property

    Public Shared Function DataImage(file As HttpPostedFileBase) As ResultStatus
        CDataImage._getImageFile = file
        Dim rs As New ResultStatus

        Try
            If file IsNot Nothing And file.ContentLength > 0 Then
                Dim xBytes As New System.IO.BinaryReader(file.InputStream)
                CDataImage._getImageByte = xBytes.ReadBytes(file.ContentLength)
            End If
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Sub CleanDataImage()
        CDataImage._getImageByte = Nothing
    End Sub

    Public Shared Sub CleanDataImageFiles()
        _getImageFiles = Nothing
    End Sub

    Public Shared Sub DataFiles(files As IEnumerable(Of HttpPostedFileBase))
        CDataImage._getImageFiles = files
    End Sub



End Class
