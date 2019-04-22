Imports eProcurementApps.Models
Public Class Generate
    Public Shared Function GetNo(table_name As String) As String
        Dim uniqueID As String = ""
        Dim lastID As New TPROC_GENERATOR
        Using db As New eProcurementEntities()
            lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            Dim seqID As Integer = lastID.LAST_SEQ_NO + 1

            uniqueID = lastID.TABLE_CODE + seqID.ToString().PadLeft(8, "0"c)
        End Using
        Return uniqueID
    End Function

    Public Shared Function CommitGenerator(table_name As String) As ResultStatus
        Dim rs As New ResultStatus
        Dim lastID As New TPROC_GENERATOR

        Try
            Using db As New eProcurementEntities()
                lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                Dim seqID As Integer = lastID.LAST_SEQ_NO + 1
                lastID.LAST_SEQ_NO = seqID
                lastID.LAST_MODIFIED_BY = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()

                lastID.LAST_MODIFIED_TIME = DateTime.Now
                db.SaveChanges()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return rs
    End Function

    Public Shared Function GetSeq(table_name As String) As String
        Dim uniqueID As String = ""
        Dim lastID As New TPROC_GENERATOR
        Using db As New eProcurementEntities()
            lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            Dim seqID As Integer = lastID.LAST_SEQ_NO + 1
            'Dim dates As String = DateTime.Now.ToString("yyyyMMdd")
            Dim dates As String = DateTime.Now.ToString("yyyy")

            uniqueID = lastID.TABLE_CODE + dates + seqID.ToString().PadLeft(8, "0"c)
        End Using
        Return uniqueID
    End Function

    Public Shared Function GetPOSeq(table_name As String) As String
        Dim uniqueID As String = ""
        Dim lastID As New TPROC_GENERATOR
        Using db As New eProcurementEntities()
            lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            Dim seqID As Integer = lastID.LAST_SEQ_NO + 1
            'Dim dates As String = DateTime.Now.ToString("yyyyMMdd")
            Dim dates As String = DateTime.Now.ToString("yy")

            'uniqueID = lastID.TABLE_CODE + dates + seqID.ToString().PadLeft(9, "0"c)
            uniqueID = dates + seqID.ToString().PadLeft(6, "0"c)
        End Using
        Return uniqueID
    End Function

    Public Shared Function GetPCSeq(table_name As String) As String
        Dim uniqueID As String = ""
        Dim lastID As New TPROC_GENERATOR
        Using db As New eProcurementEntities()
            lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            Dim seqID As Integer = lastID.LAST_SEQ_NO + 1
            'Dim dates As String = DateTime.Now.ToString("yyyyMMdd")
            Dim dates As String = DateTime.Now.ToString("yy")

            uniqueID = lastID.TABLE_CODE + dates + seqID.ToString().PadLeft(9, "0"c)
        End Using
        Return uniqueID
    End Function

    Public Shared Function GetPRSeq(table_name As String) As String
        Dim uniqueID As String = ""
        Dim lastID As New TPROC_GENERATOR
        Using db As New eProcurementEntities()
            lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
            Dim seqID As Integer = lastID.LAST_SEQ_NO + 1
            'Dim dates As String = DateTime.Now.ToString("yyyyMMdd")
            Dim dates As String = DateTime.Now.ToString("yy")

            uniqueID = lastID.TABLE_CODE + dates + seqID.ToString().PadLeft(6, "0"c)
        End Using
        Return uniqueID
    End Function

    Public Shared Function GetSeqPODetail(table_name As String) As String
        Dim uniqueID As String = ""
        Dim lastID As New TPROC_GENERATOR
        Dim rs As New ResultStatus

        Try
            Using db As New eProcurementEntities()
                lastID = db.TPROC_GENERATOR.Where(Function(a) a.TABLE_NAME = table_name And a.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
                Dim seqID As Integer = lastID.LAST_SEQ_NO + 1

                uniqueID = seqID.ToString()
            End Using
            rs.SetSuccessStatus()
        Catch ex As Exception
            rs.SetErrorStatus(ex.Message)
        End Try

        Return uniqueID
    End Function

End Class
