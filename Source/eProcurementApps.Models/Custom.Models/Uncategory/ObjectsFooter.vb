Public Class OBJECTSFOOTER
    Private _type As String
    Private _sub_total As String
    Private _disc_temp As String
    Private _disc As String
    Private _vat_temp As String
    Private _vat As String
    Private _pph_temp As String
    Private _pph As String
    Private _grand_total As String
    Private _supp_id As String
    Private _is_used As String
    Private _col_num As String
    Private _supp_nm As String
    Private _desc As String

    Public Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
        End Set
    End Property

    Public Property Sub_total As String
        Get
            Return _sub_total
        End Get
        Set(value As String)
            _sub_total = value
        End Set
    End Property

    Public Property Disc_temp As String
        Get
            Return _disc_temp
        End Get
        Set(value As String)
            _disc_temp = value
        End Set
    End Property

    Public Property Disc As String
        Get
            Return _disc
        End Get
        Set(value As String)
            _disc = value
        End Set
    End Property

    Public Property Vat_temp As String
        Get
            Return _vat_temp
        End Get
        Set(value As String)
            _vat_temp = value
        End Set
    End Property

    Public Property Vat As String
        Get
            Return _vat
        End Get
        Set(value As String)
            _vat = value
        End Set
    End Property

    Public Property Pph_temp As String
        Get
            Return _pph_temp
        End Get
        Set(value As String)
            _pph_temp = value
        End Set
    End Property

    Public Property Pph As String
        Get
            Return _pph
        End Get
        Set(value As String)
            _pph = value
        End Set
    End Property

    Public Property Grand_total As String
        Get
            Return _grand_total
        End Get
        Set(value As String)
            _grand_total = value
        End Set
    End Property

    Public Property Supp_id As String
        Get
            Return _supp_id
        End Get
        Set(value As String)
            _supp_id = value
        End Set
    End Property

    Public Property Is_used As String
        Get
            Return _is_used
        End Get
        Set(value As String)
            _is_used = value
        End Set
    End Property

    Public Property Col_num As String
        Get
            Return _col_num
        End Get
        Set(value As String)
            _col_num = value
        End Set
    End Property

    Public Property Supp_nm As String
        Get
            Return _supp_nm
        End Get
        Set(value As String)
            _supp_nm = value
        End Set
    End Property

    Public Property Desc As String
        Get
            Return _desc
        End Get
        Set(value As String)
            _desc = value
        End Set
    End Property
End Class