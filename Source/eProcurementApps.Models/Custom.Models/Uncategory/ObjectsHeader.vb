Public Class OBJECTSHEADER
    Private _type As String
    Private _supp_id As String
    Private _is_check As String
    Private _supp_nm As String

    Public Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
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

    Public Property Is_check As String
        Get
            Return _is_check
        End Get
        Set(value As String)
            _is_check = value
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
End Class