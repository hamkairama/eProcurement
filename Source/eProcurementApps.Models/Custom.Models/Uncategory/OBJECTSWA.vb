Public Class OBJECTSWA
    Private _wa_id As String
    Private _wa_number As String
    Private _wa_approval As String

    Public Property Wa_id As String
        Get
            Return _wa_id
        End Get
        Set(value As String)
            _wa_id = value
        End Set
    End Property

    Public Property Wa_number As String
        Get
            Return _wa_number
        End Get
        Set(value As String)
            _wa_number = value
        End Set
    End Property

    Public Property Wa_approval As String
        Get
            Return _wa_approval
        End Get
        Set(value As String)
            _wa_approval = value
        End Set
    End Property
End Class