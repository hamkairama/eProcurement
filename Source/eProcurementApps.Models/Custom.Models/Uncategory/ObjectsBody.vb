Public Class OBJECTSBODY
    Private _type As String
    Private _row As String
    Private _item As String
    Private _um As String
    Private _qty As String
    Private _price As String
    Private _total As String

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

    Public Property Item As String
        Get
            Return _item
        End Get
        Set(value As String)
            _item = value
        End Set
    End Property

    Public Property Um As String
        Get
            Return _um
        End Get
        Set(value As String)
            _um = value
        End Set
    End Property

    Public Property Qty As String
        Get
            Return _qty
        End Get
        Set(value As String)
            _qty = value
        End Set
    End Property

    Public Property Price As String
        Get
            Return _price
        End Get
        Set(value As String)
            _price = value
        End Set
    End Property

    Public Property Total As String
        Get
            Return _total
        End Get
        Set(value As String)
            _total = value
        End Set
    End Property
End Class