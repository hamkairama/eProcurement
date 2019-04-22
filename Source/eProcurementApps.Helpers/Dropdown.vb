Imports System.Web.Mvc
Imports eProcurementApps.Models
Imports System.Linq
Imports System.Data.Entity

Public Class Dropdown

    Public Shared Function Months(ByVal year As Integer) As SelectList
        Dim monthsList As List(Of MONTHS) = New List(Of MONTHS)
        Dim nameOfMonths() As String = New String() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
        Dim i As Integer = 0
        Do While (i < 12)
            Dim month As MONTHS = New MONTHS
            month.TEXT = nameOfMonths(i)
            month.VALUE = (DateTime.DaysInMonth(year, (i + 1)).ToString + ("-" + (i + 1).ToString.PadLeft(2, Microsoft.VisualBasic.ChrW(48))))
            monthsList.Add(month)
            i = (i + 1)
        Loop

        Dim selectList As SelectList = New SelectList(monthsList, "VALUE", "TEXT")
        Return selectList
    End Function

    Public Shared Function Year() As SelectList
        Dim result As List(Of Integer) = New List(Of Integer)
        result.Add(DateTime.Now.Year)
        result.Add((DateTime.Now.Year + 1))
        result.Add((DateTime.Now.Year + 2))
        Dim selectList As SelectList = New SelectList(result)
        Return selectList
    End Function

    Public Shared Function FormType2() As SelectList
        Dim fORM_TYPE As List(Of TPROC_FORM_TYPE) = New List(Of TPROC_FORM_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim selectList As SelectList
        selectList = New SelectList(fORM_TYPE, "ID", "FORM_TYPE_NAME")

        Return selectList
    End Function

    Public Shared Function FormType() As List(Of SelectListItem)
        Dim fORM_TYPE As List(Of TPROC_FORM_TYPE) = New List(Of TPROC_FORM_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(y) y.FORM_TYPE_NAME).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < fORM_TYPE.Count)
            a.Add(New SelectListItem() With {.Value = fORM_TYPE(i).ID, .Text = fORM_TYPE(i).FORM_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function Currency() As List(Of SelectListItem)
        Dim curr As List(Of TPROC_CURRENCY) = New List(Of TPROC_CURRENCY)
        Dim db As eProcurementEntities = New eProcurementEntities
        curr = db.TPROC_CURRENCY.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).OrderBy(Function(y) y.CURRENCY_NAME).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < curr.Count)
            If curr(i).CURRENCY_NAME.Contains("IDR") Then
                a.Add(New SelectListItem() With {.Value = curr(i).CONVERSION_RP, .Text = curr(i).CURRENCY_NAME, .Selected = True}) 'set idr as default
            Else
                a.Add(New SelectListItem() With {.Value = curr(i).CONVERSION_RP, .Text = curr(i).CURRENCY_NAME})
            End If

            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function FormTypeStock() As List(Of SelectListItem)
        Dim fORM_TYPE As List(Of TPROC_FORM_TYPE) = New List(Of TPROC_FORM_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        'fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.FORM_TYPE_NAME.ToUpper() = "PRINTING" Or x.FORM_TYPE_NAME.ToUpper() = "OFFICE SUPPLIES" And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()
        fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.IS_GOOD_TYPE = 1 And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < fORM_TYPE.Count)
            a.Add(New SelectListItem() With {.Value = fORM_TYPE(i).ID, .Text = fORM_TYPE(i).FORM_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function FormTypeNonStock() As List(Of SelectListItem)
        Dim fORM_TYPE As List(Of TPROC_FORM_TYPE) = New List(Of TPROC_FORM_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        fORM_TYPE = db.TPROC_FORM_TYPE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < fORM_TYPE.Count)
            a.Add(New SelectListItem() With {.Value = fORM_TYPE(i).ID, .Text = fORM_TYPE(i).FORM_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function FormSubType() As List(Of SelectListItem)
        Dim fORM_SUB_TYPE As List(Of TPROC_FORM_SUB_TYPE) = New List(Of TPROC_FORM_SUB_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        fORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(y) y.TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < fORM_SUB_TYPE.Count)
            a.Add(New SelectListItem() With {.Value = fORM_SUB_TYPE(i).ID, .Text = fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function GetFormSubTypeFromFormTypeId(form_type_id As Decimal) As List(Of SelectListItem)
        Dim fORM_SUB_TYPE As List(Of TPROC_FORM_SUB_TYPE) = New List(Of TPROC_FORM_SUB_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        fORM_SUB_TYPE = db.TPROC_FORM_SUB_TYPE.Where(Function(x) x.TPROC_FORM_SUBTYPE_GR.FORM_TYPE_ID = form_type_id And (x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete)).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < fORM_SUB_TYPE.Count)
            Dim valuex As String = fORM_SUB_TYPE(i).ID.ToString() + "|" + fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.SLA.ToString() + "|" + fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.POPUP_ACCOUNT.ToString() _
            + "|" + fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.BUDGET_CODE + "|" + fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_START + "|" + fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_END


            a.Add(New SelectListItem() With {.Value = valuex, .Text = fORM_SUB_TYPE(i).TPROC_FORM_SUBTYPE_GR.SUB_FORM_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function GetBcFromFormSubTypeId(form_sub_type_id As Decimal) As List(Of SelectListItem)
        Dim fst_bc As List(Of TPROC_FST_BUDGET_CD_ADD) = New List(Of TPROC_FST_BUDGET_CD_ADD)
        Dim fst As New TPROC_FORM_SUB_TYPE
        Dim db As eProcurementEntities = New eProcurementEntities
        fst = db.TPROC_FORM_SUB_TYPE.Find(form_sub_type_id)

        fst_bc = fst.TPROC_FORM_SUBTYPE_GR.TPROC_FST_BUDGET_CD_ADD.ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        a.Add(New SelectListItem() With {.Value = fst.TPROC_FORM_SUBTYPE_GR.BUDGET_CODE + "^" + fst.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_START + "^" + fst.TPROC_FORM_SUBTYPE_GR.ACCOUNT_CODE_END, .Text = fst.TPROC_FORM_SUBTYPE_GR.BUDGET_CODE})

        If fst_bc IsNot Nothing Then
            Do While (i < fst_bc.Count)
                Dim valuex As String = fst_bc(i).BUDGET_CODE + "^" + fst_bc(i).ACCOUNT_CODE_START + "^" + fst_bc(i).ACCOUNT_CODE_END

                a.Add(New SelectListItem() With {.Value = valuex, .Text = fst_bc(i).BUDGET_CODE})
                i = (i + 1)
            Loop
        End If

        Return a
    End Function

    Public Shared Function Role() As List(Of SelectListItem)
        Dim rOLE_ As List(Of TPROC_ROLE) = New List(Of TPROC_ROLE)
        Dim db As eProcurementEntities = New eProcurementEntities
        rOLE_ = db.TPROC_ROLE.Where(Function(x) x.IS_ACTIVE = 1 And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < rOLE_.Count)
            a.Add(New SelectListItem() With {.Value = rOLE_(i).ID, .Text = rOLE_(i).ROLE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function RoleDefaultSelected() As List(Of SelectListItem)
        Dim rOLE_ As List(Of TPROC_ROLE) = New List(Of TPROC_ROLE)
        Dim db As eProcurementEntities = New eProcurementEntities
        rOLE_ = db.TPROC_ROLE.Where(Function(x) x.IS_ACTIVE = 1 And x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < rOLE_.Count)
            If rOLE_(i).DEFAULT_SELECTED = 1 Then
                a.Add(New SelectListItem() With {.Value = rOLE_(i).ID, .Text = rOLE_(i).ROLE_NAME, .Selected = True})
                'Else
                '    a.Add(New SelectListItem() With {.Value = rOLE_(i).ID, .Text = rOLE_(i).ROLE_NAME})
            End If

            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function Division() As List(Of SelectListItem)
        Dim dIVISI As List(Of TPROC_DIVISION) = New List(Of TPROC_DIVISION)
        Dim db As eProcurementEntities = New eProcurementEntities
        dIVISI = db.TPROC_DIVISION.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < dIVISI.Count)
            a.Add(New SelectListItem() With {.Value = dIVISI(i).ID, .Text = dIVISI(i).DIVISION_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function Division(Optional ByVal Value As String = "") As List(Of SelectListItem)
        Dim ldiv As List(Of TPROC_DIVISION) = New List(Of TPROC_DIVISION)
        Dim db As eProcurementEntities = New eProcurementEntities
        ldiv = db.TPROC_DIVISION.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < ldiv.Count)
            a.Add(New SelectListItem() With {.Value = ldiv(i).ID, .Text = ldiv(i).DIVISION_NAME})
            If Value <> "" Then
                If ldiv(i).ID = CInt(Value) Then
                    a.Item(i + 1).Selected = True
                End If
            End If
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function Rolename2() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})

        For Each ArEnum As ListEnum.PO In System.Enum.GetValues(GetType(ListEnum.ApprovalRole))
            a.Add(New SelectListItem() With {.Value = CInt(ArEnum).ToString(), .Text = ArEnum.ToString()})
        Next

        Return a
    End Function

    Public Shared Function Rolename(Optional ByVal Value As String = "") As List(Of SelectListItem)
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        a.Add(New SelectListItem() With {.Value = "PC", .Text = "PC"})
        a.Add(New SelectListItem() With {.Value = "CRV", .Text = "CRV"})
        a.Add(New SelectListItem() With {.Value = "PO", .Text = "PO"})
        'a.Add(New SelectListItem() With {.Value = "4", .Text = "PC PO CRV"})
        For i As Integer = 0 To a.Count - 1
            If a.Item(i).Value = Value Then
                a.Item(i).Selected = True
            End If
        Next
        Return a
    End Function

    Public Shared Function AsIs(Optional ByVal Value As String = "") As List(Of SelectListItem)
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        a.Add(New SelectListItem() With {.Value = "Approver", .Text = "Approver"})
        a.Add(New SelectListItem() With {.Value = "Verifier", .Text = "Verifier"})
        For i As Integer = 0 To a.Count - 1
            If a.Item(i).Value = Value Then
                a.Item(i).Selected = True
            End If
        Next
        Return a
    End Function

    Public Shared Function LevelApproval(Optional ByVal Value As String = "") As List(Of SelectListItem)
        Dim lvel As List(Of TPROC_LEVEL) = New List(Of TPROC_LEVEL)
        Dim db As eProcurementEntities = New eProcurementEntities
        lvel = db.TPROC_LEVEL.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < lvel.Count)
            a.Add(New SelectListItem() With {.Value = lvel(i).ID, .Text = lvel(i).INDONESIAN_LEVEL})
            If Value <> "" Then
                If lvel(i).ID = CInt(Value) Then
                    a.Item(i + 1).Selected = True
                End If
            End If
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function Level() As List(Of SelectListItem)
        Dim lvel As List(Of TPROC_LEVEL) = New List(Of TPROC_LEVEL)
        Dim db As eProcurementEntities = New eProcurementEntities
        lvel = db.TPROC_LEVEL.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < lvel.Count)
            a.Add(New SelectListItem() With {.Value = lvel(i).ID, .Text = lvel(i).INDONESIAN_LEVEL})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function WorkAreaWithOther() As List(Of SelectListItem)
        Dim wa As List(Of TPROC_WA) = New List(Of TPROC_WA)
        Dim db As eProcurementEntities = New eProcurementEntities
        wa = db.TPROC_WA.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < wa.Count)
            Dim valuex As String = wa(i).ID.ToString() + "|" + wa(i).TPROC_APPROVAL_GR.DEPARTMENT_NAME + "|" + wa(i).TPROC_APPROVAL_GR.TPROC_DIVISION.DIVISION_NAME
            a.Add(New SelectListItem() With {.Value = valuex, .Text = wa(i).WA_NUMBER})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function WorkArea() As List(Of SelectListItem)
        Dim wa As List(Of TPROC_WA) = New List(Of TPROC_WA)
        Dim db As eProcurementEntities = New eProcurementEntities
        wa = db.TPROC_WA.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < wa.Count)
            a.Add(New SelectListItem() With {.Value = wa(i).ID, .Text = wa(i).WA_NUMBER})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function GetWorkArea() As List(Of TPROC_WA)
        Dim wa As New List(Of TPROC_WA)
        Dim db As eProcurementEntities = New eProcurementEntities
        wa = db.TPROC_WA.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(y) y.WA_NUMBER).ToList()

        Return wa
    End Function

    Public Shared Function WorkArea(id As Decimal) As List(Of SelectListItem)
        Dim wa_dt As List(Of TPROC_WA_ALLOWED_DT) = New List(Of TPROC_WA_ALLOWED_DT)
        Dim db As eProcurementEntities = New eProcurementEntities

        'Dim usr_dt As New TPROC_USER_DT
        Dim usr As New TPROC_USER
        Dim wa_gr As New TPROC_WA_ALLOWED_GR

        'usr_dt = db.TPROC_USER_DT.Where(Function(x) x.USER_ID_ID = id And x.ROW_STATUS = ListEnum.RowStat.Live).FirstOrDefault()
        usr = db.TPROC_USER.Find(id)
        wa_dt = db.TPROC_WA_ALLOWED_DT.Where(Function(y) y.WA_ALLOWED_GROUP_ID = usr.TPROC_USER_DT.WA_ALLOWED_GR_ID And y.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < wa_dt.Count)
            a.Add(New SelectListItem() With {.Value = wa_dt(i).TPROC_WA.ID, .Text = wa_dt(i).TPROC_WA.WA_NUMBER})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function WorkAreaForCreateUser() As List(Of String)
        Dim wa As List(Of TPROC_WA) = New List(Of TPROC_WA)
        Dim db As eProcurementEntities = New eProcurementEntities
        wa = db.TPROC_WA.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of String)
        Do While (i < wa.Count)
            a.Add(wa(i).WA_NUMBER.ToString() + " - " + wa(i).TPROC_APPROVAL_GR.DEPARTMENT_NAME.ToString())
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function WorkAreaWithDetail() As List(Of SelectListItem)
        Dim wa As List(Of TPROC_WA) = New List(Of TPROC_WA)
        Dim db As eProcurementEntities = New eProcurementEntities
        wa = db.TPROC_WA.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(y) y.WA_NUMBER).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < wa.Count)
            a.Add(New SelectListItem() With {.Value = wa(i).ID.ToString() + " - " + wa(i).WA_NUMBER.ToString(), .Text = wa(i).WA_NUMBER.ToString() + " - " + wa(i).TPROC_APPROVAL_GR.DEPARTMENT_NAME.ToString()})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function WorkAreaWithDetailByUser(id As Decimal) As List(Of SelectListItem)
        Dim wa As List(Of TPROC_WA) = New List(Of TPROC_WA)
        Dim db As eProcurementEntities = New eProcurementEntities
        Dim usr As New TPROC_USER
        Dim wa_dt As List(Of TPROC_WA_ALLOWED_DT) = New List(Of TPROC_WA_ALLOWED_DT)

        usr = db.TPROC_USER.Find(id)
        wa_dt = db.TPROC_WA_ALLOWED_DT.Where(Function(y) y.WA_ALLOWED_GROUP_ID = usr.TPROC_USER_DT.WA_ALLOWED_GR_ID And y.ROW_STATUS = ListEnum.RowStat.Live).ToList()


        'wa = db.TPROC_WA.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(y) y.WA_NUMBER).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < wa_dt.Count)
            a.Add(New SelectListItem() With {.Value = wa_dt(i).TPROC_WA.ID.ToString() + " - " + wa_dt(i).TPROC_WA.WA_NUMBER.ToString(), .Text = wa_dt(i).TPROC_WA.WA_NUMBER.ToString() + " - " + wa_dt(i).TPROC_WA.TPROC_APPROVAL_GR.DEPARTMENT_NAME.ToString()})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function RelatedDepartment() As List(Of SelectListItem)
        Dim rel_dept As List(Of TPROC_REL_DEPT) = New List(Of TPROC_REL_DEPT)
        Dim db As eProcurementEntities = New eProcurementEntities
        rel_dept = db.TPROC_REL_DEPT.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(y) y.RELATED_DEPARTMENT_NAME).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < rel_dept.Count)
            a.Add(New SelectListItem() With {.Value = rel_dept(i).ID, .Text = rel_dept(i).RELATED_DEPARTMENT_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function RelatedDepartment(Optional ByVal Value As String = "") As List(Of SelectListItem)
        Dim lrd As List(Of TPROC_REL_DEPT) = New List(Of TPROC_REL_DEPT)
        Dim db As eProcurementEntities = New eProcurementEntities
        lrd = db.TPROC_REL_DEPT.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < lrd.Count)
            a.Add(New SelectListItem() With {.Value = lrd(i).ID, .Text = lrd(i).RELATED_DEPARTMENT_NAME})
            If Value <> "" Then
                If lrd(i).ID = CInt(Value) Then
                    a.Item(i + 1).Selected = True
                End If
            End If
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function GoodType() As List(Of SelectListItem)
        Dim good_type As List(Of TPROC_GOOD_TYPE) = New List(Of TPROC_GOOD_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        good_type = db.TPROC_GOOD_TYPE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < good_type.Count)
            a.Add(New SelectListItem() With {.Value = good_type(i).ID, .Text = good_type(i).GOOD_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function COA() As List(Of SelectListItem)
        Dim c As List(Of TPROC_CHART_OF_ACCOUNTS) = New List(Of TPROC_CHART_OF_ACCOUNTS)
        Dim db As eProcurementEntities = New eProcurementEntities
        c = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < c.Count)
            a.Add(New SelectListItem() With {.Value = c(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM, .Text = c(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM + " - " + c(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function AccountCode() As List(Of SelectListItem)
        Dim cOA As List(Of TPROC_CHART_OF_ACCOUNTS) = New List(Of TPROC_CHART_OF_ACCOUNTS)
        Dim db As eProcurementEntities = New eProcurementEntities
        cOA = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < cOA.Count)
            a.Add(New SelectListItem() With {.Value = cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM, .Text = cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM + " - " + cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function AccountCodewithValue(Optional ByVal Value As String = "") As List(Of SelectListItem)
        Dim cOA As List(Of TPROC_CHART_OF_ACCOUNTS) = New List(Of TPROC_CHART_OF_ACCOUNTS)
        Dim db As eProcurementEntities = New eProcurementEntities
        cOA = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < cOA.Count)
            a.Add(New SelectListItem() With {.Value = cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM, .Text = cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM + " - " + cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_DESC})
            If Value <> "" Then
                If cOA(i).TPROC_CHART_OF_ACCOUNT_GR.ACCT_NUM = Value Then
                    a.Item(i + 1).Selected = True
                End If
            End If
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function GetAllUser() As List(Of SelectListItem)
        Dim user As List(Of TPROC_USER) = New List(Of TPROC_USER)
        Dim db As eProcurementEntities = New eProcurementEntities
        user = db.TPROC_USER.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live Or x.ROW_STATUS = ListEnum.RowStat.Edit Or x.ROW_STATUS = ListEnum.RowStat.Delete).OrderBy(Function(x) x.USER_NAME).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < user.Count)
            a.Add(New SelectListItem() With {.Value = user(i).ID, .Text = user(i).USER_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function POTypeId() As List(Of SelectListItem)
        Dim PO_TYPE As List(Of TPROC_PO_TYPE) = New List(Of TPROC_PO_TYPE)
        Dim db As eProcurementEntities = New eProcurementEntities
        PO_TYPE = db.TPROC_PO_TYPE.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < PO_TYPE.Count)
            Dim valuex As String = PO_TYPE(i).ID.ToString() + "|" + PO_TYPE(i).FORM_TYPE_ID.ToString() + "|" + PO_TYPE(i).TPROC_FORM_TYPE.FORM_TYPE_NAME.ToString().ToUpper().Replace(" ", "")
            a.Add(New SelectListItem() With {.Value = valuex, .Text = PO_TYPE(i).PO_TYPE_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function Supplier() As List(Of SelectListItem)
        Dim c As List(Of TPROC_SUPPLIER) = New List(Of TPROC_SUPPLIER)
        Dim db As eProcurementEntities = New eProcurementEntities
        c = db.TPROC_SUPPLIER.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < c.Count)
            a.Add(New SelectListItem() With {.Value = c(i).ID, .Text = c(i).SUPPLIER_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function ListSupplierId() As List(Of SelectListItem)
        Dim SUPPLIER_NM As List(Of TPROC_SUPPLIER) = New List(Of TPROC_SUPPLIER)
        Dim db As eProcurementEntities = New eProcurementEntities
        SUPPLIER_NM = db.TPROC_SUPPLIER.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < SUPPLIER_NM.Count)
            Dim valuex As String = SUPPLIER_NM(i).ID.ToString() + "|" + SUPPLIER_NM(i).MOBILE_NUMBER + "|" + SUPPLIER_NM(i).SUPPLIER_ADDRESS.ToString() +
                "|" + SUPPLIER_NM(i).CONTACT_PERSON.ToString() + "|" + SUPPLIER_NM(i).FAX_NUMBER + "|" + SUPPLIER_NM(i).SUPPLIER_NAME.ToString()
            a.Add(New SelectListItem() With {.Value = valuex, .Text = SUPPLIER_NM(i).SUPPLIER_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function ListDeliveryId() As List(Of SelectListItem)
        Dim DELIVERY_NM As List(Of TPROC_DELIVERY_ADDRESS) = New List(Of TPROC_DELIVERY_ADDRESS)
        Dim db As eProcurementEntities = New eProcurementEntities
        DELIVERY_NM = db.TPROC_DELIVERY_ADDRESS.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < DELIVERY_NM.Count)
            Dim last As String = ""
            If DELIVERY_NM(i).LAST_MODIFIED_TIME.HasValue Then
                last = DateTime.Parse(DELIVERY_NM(i).LAST_MODIFIED_TIME).ToString("dd-MM-yyyy")
            Else
                last = DELIVERY_NM(i).CREATED_TIME.ToString("dd-MM-yyyy")
            End If
            Dim valuex As String = DELIVERY_NM(i).ID.ToString() + "|" + DELIVERY_NM(i).DELIVERY_PHONE.ToString() + "|" + DELIVERY_NM(i).DELIVERY_ADDRESS.ToString() + "|" + last + "|" + DELIVERY_NM(i).DELIVERY_FAX.ToString() + "|" + DELIVERY_NM(i).DELIVERY_NAME.ToString()
            a.Add(New SelectListItem() With {.Value = valuex, .Text = DELIVERY_NM(i).DELIVERY_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function ListCurrencyId() As List(Of SelectListItem)
        Dim CURRENCY_ID As List(Of TPROC_CURRENCY) = New List(Of TPROC_CURRENCY)
        Dim db As eProcurementEntities = New eProcurementEntities
        CURRENCY_ID = db.TPROC_CURRENCY.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < CURRENCY_ID.Count)
            a.Add(New SelectListItem() With {.Value = CURRENCY_ID(i).ID, .Text = CURRENCY_ID(i).CURRENCY_NAME})
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function ListStatusPC() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "", .Text = "-select-"})
        For Each PcEnum As ListEnum.PriceCom In System.Enum.GetValues(GetType(ListEnum.PriceCom))
            a.Add(New SelectListItem() With {.Value = CInt(PcEnum).ToString(), .Text = PcEnum.ToString()})
        Next

        Return a
    End Function

    Public Shared Function ListStatusPR() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "", .Text = "-select-"})
        For Each PrEnum As ListEnum.PRStatus In System.Enum.GetValues(GetType(ListEnum.PRStatus))
            a.Add(New SelectListItem() With {.Value = CInt(PrEnum).ToString(), .Text = PrEnum.ToString()})
        Next

        Return a
    End Function

    Public Shared Function SetStatusSubmitPoPc() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        a.Add(New SelectListItem() With {.Value = CInt(ListEnum.PO.Submitted).ToString(), .Text = ListEnum.PO.Submitted.ToString()})

        Return a
    End Function

    Public Shared Function ListStatusPO() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "", .Text = "-select-"})
        For Each PoEnum As ListEnum.PO In System.Enum.GetValues(GetType(ListEnum.PO))
            a.Add(New SelectListItem() With {.Value = CInt(PoEnum).ToString(), .Text = PoEnum.ToString()})
        Next

        Return a
    End Function

    Public Shared Function ListStatusCrv() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "", .Text = "-select-"})
        For Each crvEnum As ListEnum.Crv In System.Enum.GetValues(GetType(ListEnum.Crv))
            Dim txt = ""

            If CInt(crvEnum) = ListEnum.Crv.Active Then
                txt = "Still preparation"
            End If
            If CInt(crvEnum) = ListEnum.Crv.InActive Then
                txt = "Not active"
            End If
            If CInt(crvEnum) = ListEnum.Crv.Rejected Then
                txt = "Rejected"
            End If
            If CInt(crvEnum) = ListEnum.Crv.Verify Then
                txt = "Waiting to verify"
            End If
            If CInt(crvEnum) = ListEnum.Crv.Approve Then
                txt = "Waiting to approve"
            End If
            If CInt(crvEnum) = ListEnum.Crv.Received Then
                txt = "Waiting receive by finance"
            End If
            If CInt(crvEnum) = ListEnum.Crv.Paid Then
                txt = "Waiting paid by finance"
            End If
            If CInt(crvEnum) = ListEnum.Crv.Complete Then
                txt = "Completed"
            End If

            a.Add(New SelectListItem() With {.Value = CInt(crvEnum).ToString(), .Text = txt})
        Next

        Return a
    End Function

    Public Shared Function ListStatusGm() As List(Of SelectListItem)
        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "", .Text = "-select-"})
        For Each gmEnum As ListEnum.Gm In System.Enum.GetValues(GetType(ListEnum.Gm))
            Dim txt = ""

            If CInt(gmEnum) = ListEnum.Gm.Active Then
                txt = "Still preparation"
            End If
            If CInt(gmEnum) = ListEnum.Gm.InActive Then
                txt = "Not active"
            End If
            If CInt(gmEnum) = ListEnum.Gm.Rejected Then
                txt = "Rejected"
            End If
            If CInt(gmEnum) = ListEnum.Gm.Approve Then
                txt = "Waiting to approve"
            End If
            If CInt(gmEnum) = ListEnum.Gm.Complete Then
                txt = "Completed"
            End If

            a.Add(New SelectListItem() With {.Value = CInt(gmEnum).ToString(), .Text = txt})
        Next

        Return a
    End Function

    Public Shared Function ListItemPOId(is_for_storage As Integer, form_type_id As Decimal) As List(Of SelectListItem)
        Dim a As New List(Of SelectListItem)
        Dim ITEM_PO As List(Of TPROC_PR_DETAIL) = New List(Of TPROC_PR_DETAIL)
        Dim db As eProcurementEntities = New eProcurementEntities

        ITEM_PO = (From _detail In db.TPROC_PR_DETAIL
                   Join _header In db.TPROC_PR_HEADER On _header.ID Equals (_detail.PR_HEADER_ID)
                   Where _header.PR_STATUS = ListEnum.PRStatus.CreatePo And _detail.ROW_STATUS = ListEnum.RowStat.Live _
                        And _header.ROW_STATUS = ListEnum.RowStat.Live And _detail.PO_NUMBER Is Nothing _
                        And _header.FOR_STORAGE = is_for_storage And _detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete _
                        And _header.FORM_TYPE_ID = form_type_id
                   Select _detail
                   Distinct
                   Order By _detail.ITEM_NAME Ascending).ToList()

        Dim i As Integer = 0
        Dim itemOld As String = Nothing
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < ITEM_PO.Count)
            Dim valuex As String = ITEM_PO(i).ITEM_ID.ToString() + "|" + ITEM_PO(i).PR_HEADER_ID.ToString() + "|" + Trim(ITEM_PO(i).ITEM_NAME).ToString() 
            If (itemOld <> Trim(ITEM_PO(i).ITEM_NAME).ToString()) Then
                a.Add(New SelectListItem() With {.Value = valuex, .Text = Trim(ITEM_PO(i).ITEM_NAME).ToString()})
            End If
            itemOld = Trim(ITEM_PO(i).ITEM_NAME).ToString()
            i = (i + 1)
        Loop

        Return a
    End Function

    Public Shared Function GetPrNumberId(item_nm_id As String) As List(Of SelectListItem)
        Dim po_header As List(Of TPROC_PR_HEADER) = New List(Of TPROC_PR_HEADER)
        Dim db As eProcurementEntities = New eProcurementEntities
        po_header = (From _header In db.TPROC_PR_HEADER
                     Join _detail In db.TPROC_PR_DETAIL On _detail.PR_HEADER_ID Equals (_header.ID)
                     Where _header.PR_STATUS = ListEnum.PRStatus.CreatePo And Trim(_detail.ITEM_NAME) = item_nm_id And _detail.PO_NUMBER Is Nothing _
                         And _header.ROW_STATUS = ListEnum.RowStat.Live And _detail.ROW_STATUS = ListEnum.RowStat.Live And _detail.PR_DETAIL_STATUS = ListEnum.ItemStatus.Complete
                     Select _header
                     Distinct
                     Order By _header.ID Ascending).ToList()

        Dim i As Integer = 0
        Dim a As New List(Of SelectListItem)
        a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
        Do While (i < po_header.Count)
            Dim valuex As String = po_header(i).ID.ToString()
            a.Add(New SelectListItem() With {.Value = valuex, .Text = po_header(i).PR_NO.ToString()})
            i = (i + 1)
        Loop

        Return a
    End Function

    'Public Shared Function GetCoaList() As List(Of TPROC_CHART_OF_ACCOUNTS)
    '    Dim lcoa_ As List(Of TPROC_CHART_OF_ACCOUNTS) = New List(Of TPROC_CHART_OF_ACCOUNTS)
    '    Dim db As eProcurementEntities = New eProcurementEntities
    '    lcoa_ = db.TPROC_CHART_OF_ACCOUNTS.Where(Function(x) x.ROW_STATUS = ListEnum.RowStat.Live).ToList()

    '    Return lcoa_
    'End Function

    'Public Shared Function GetCoaByNumber(number As String) As List(Of SelectListItem)





    '    Dim i As Integer = 0
    '    Dim a As New List(Of SelectListItem)
    '    a.Add(New SelectListItem() With {.Value = "0", .Text = "-select-"})
    '    Do While (i < coa_.Count)
    '        If coa_(i).DEFAULT_SELECTED = 1 Then
    '            a.Add(New SelectListItem() With {.Value = coa_(i).ACCT_NUM, .Text = coa_(i).ROLE_NAME, .Selected = True})
    '            'Else
    '            '    a.Add(New SelectListItem() With {.Value = rOLE_(i).ID, .Text = rOLE_(i).ROLE_NAME})
    '        End If

    '        i = (i + 1)
    '    Loop

    '    Return a
    'End Function

End Class