Public Class OBJECTSPC
    Private _type As String
    Private _po_type_nm As String
    Private _recom_supplier_nm As String
    Private _recom_supplier_id As String
    Private _recom_supplier_cp As String
    Private _recom_supplier_phone As String
    Private _recom_supplier_fax As String
    Private _recom_supplier_address As String
    Private _delivery_nm As String
    Private _delivery_id As String
    Private _delivery_date As String
    Private _delivery_phone As String
    Private _delivery_fax As String
    Private _delivery_address As String
    Private _note_by_user As String
    Private _note_by_eproc As String
    Private _is_disc_perc As String
    Private _is_vat_perc As String
    Private _is_pph_perc As String
    Private _grand_total As String
    Private _notes As String
    Private _currency As String
    Private _is_acknowledge_user As String

    Public Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
        End Set
    End Property

    Public Property Po_type_nm As String
        Get
            Return _po_type_nm
        End Get
        Set(value As String)
            _po_type_nm = value
        End Set
    End Property

    Public Property Recom_supplier_nm As String
        Get
            Return _recom_supplier_nm
        End Get
        Set(value As String)
            _recom_supplier_nm = value
        End Set
    End Property

    Public Property Recom_supplier_id As String
        Get
            Return _recom_supplier_id
        End Get
        Set(value As String)
            _recom_supplier_id = value
        End Set
    End Property

    Public Property Recom_supplier_cp As String
        Get
            Return _recom_supplier_cp
        End Get
        Set(value As String)
            _recom_supplier_cp = value
        End Set
    End Property

    Public Property Recom_supplier_phone As String
        Get
            Return _recom_supplier_phone
        End Get
        Set(value As String)
            _recom_supplier_phone = value
        End Set
    End Property

    Public Property Recom_supplier_fax As String
        Get
            Return _recom_supplier_fax
        End Get
        Set(value As String)
            _recom_supplier_fax = value
        End Set
    End Property

    Public Property Recom_supplier_address As String
        Get
            Return _recom_supplier_address
        End Get
        Set(value As String)
            _recom_supplier_address = value
        End Set
    End Property

    Public Property Delivery_nm As String
        Get
            Return _delivery_nm
        End Get
        Set(value As String)
            _delivery_nm = value
        End Set
    End Property

    Public Property Delivery_id As String
        Get
            Return _delivery_id
        End Get
        Set(value As String)
            _delivery_id = value
        End Set
    End Property

    Public Property Delivery_date As String
        Get
            Return _delivery_date
        End Get
        Set(value As String)
            _delivery_date = value
        End Set
    End Property

    Public Property Delivery_phone As String
        Get
            Return _delivery_phone
        End Get
        Set(value As String)
            _delivery_phone = value
        End Set
    End Property

    Public Property Delivery_fax As String
        Get
            Return _delivery_fax
        End Get
        Set(value As String)
            _delivery_fax = value
        End Set
    End Property

    Public Property Delivery_address As String
        Get
            Return _delivery_address
        End Get
        Set(value As String)
            _delivery_address = value
        End Set
    End Property

    Public Property Note_by_user As String
        Get
            Return _note_by_user
        End Get
        Set(value As String)
            _note_by_user = value
        End Set
    End Property

    Public Property Note_by_eproc As String
        Get
            Return _note_by_eproc
        End Get
        Set(value As String)
            _note_by_eproc = value
        End Set
    End Property

    Public Property Is_disc_perc As String
        Get
            Return _is_disc_perc
        End Get
        Set(value As String)
            _is_disc_perc = value
        End Set
    End Property

    Public Property Is_vat_perc As String
        Get
            Return _is_vat_perc
        End Get
        Set(value As String)
            _is_vat_perc = value
        End Set
    End Property

    Public Property Is_pph_perc As String
        Get
            Return _is_pph_perc
        End Get
        Set(value As String)
            _is_pph_perc = value
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

    Public Property Notes As String
        Get
            Return _notes
        End Get
        Set(value As String)
            _notes = value
        End Set
    End Property

    Public Property Currency As String
        Get
            Return _currency
        End Get
        Set(value As String)
            _currency = value
        End Set
    End Property

    Public Property Is_acknowledge_user As String
        Get
            Return _is_acknowledge_user
        End Get
        Set(value As String)
            _is_acknowledge_user = value
        End Set
    End Property
End Class