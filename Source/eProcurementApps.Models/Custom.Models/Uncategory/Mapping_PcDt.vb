Public Class MAPPING_PCDT
    Private _row As String
    Private _pc_dt_id As String

    Public Property Row As String
        Get
            Return _row
        End Get
        Set(value As String)
            _row = value
        End Set
    End Property
    Public Property Pc_dt_id As String
        Get
            Return _pc_dt_id
        End Get
        Set(value As String)
            _pc_dt_id = value
        End Set
    End Property
End Class