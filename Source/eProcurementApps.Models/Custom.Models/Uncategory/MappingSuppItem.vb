Public Class MAPPINGSUPPITEM
    Private _type As String
    Private _row As String
    Private _supp_each_price As String
    Private _supp_total_price As String
    Private _header As ObjectsHeader

    Public Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
        End Set
    End Property

    Public Property Row As String
        Get
            Return _row
        End Get
        Set(value As String)
            _row = value
        End Set
    End Property

    Public Property Supp_each_price As String
        Get
            Return _supp_each_price
        End Get
        Set(value As String)
            _supp_each_price = value
        End Set
    End Property

    Public Property Supp_total_price As String
        Get
            Return _supp_total_price
        End Get
        Set(value As String)
            _supp_total_price = value
        End Set
    End Property

    Public Property Header As ObjectsHeader
        Get
            Return _header
        End Get
        Set(value As ObjectsHeader)
            _header = value
        End Set
    End Property
End Class