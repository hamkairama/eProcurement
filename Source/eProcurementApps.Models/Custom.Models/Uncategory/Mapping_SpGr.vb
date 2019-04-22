Public Class MAPPING_SPGR
    Private _col As String
    Private _sp_gr_id As String

    Public Property Col As String
        Get
            Return _col
        End Get
        Set(value As String)
            _col = value
        End Set
    End Property

    Public Property Sp_gr_id As String
        Get
            Return _sp_gr_id
        End Get
        Set(value As String)
            _sp_gr_id = value
        End Set
    End Property
End Class