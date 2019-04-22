@ModelType IEnumerable(Of eProcurementApps.Models.USER_HELPER)
@Imports eProcurementApps.Helpers

@code
    Dim flag As String = Session("Active_Directory")
    Dim no As Integer = 1
    @If Model.Count() = 0 Then
        @<tr style='color:red'><td colspan='6'> No records founds</td></tr>
    Else
        @For Each item In Model
            @<tr>
                <td>@no</td>
                <td id="txt_user_@no">@Html.DisplayFor(Function(modelItem) item.USER_ID)</td>
                <td id="txt_name_@no">@Html.DisplayFor(Function(modelItem) item.USER_NAME)</td>
                <td id="txt_email_@no">@Html.DisplayFor(Function(modelItem) item.EMAIL)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.DEPARTMENT)</td>
                <td><a onclick="Passing('@flag','@no')" title="Select" href="#">@Html.Raw(Labels.ButtonForm("Select"))</a></td>
            </tr>
            no = no + 1
        Next

    End If
End Code

