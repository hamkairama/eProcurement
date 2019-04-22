@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_APPROVAL_DT)


<div class="form-group" id="wa_id_table">
    @code
        @If Model IsNot Nothing Then
            @<ul>
                @For each item In Model
                    Dim val As String = item.APPROVAL_NAME.ToString() + "-" + item.USER_NAME + "-" + item.EMAIL.ToString()
                    @<li class="appx"><input type="checkbox" id="@val" value="" /> @val</li>
                Next
            </ul>
            @<Button Class='btn btn-sm btn-success btn-white btn-round' onclick="PassingApprovalToRow()" type='submit'>
                Add to row
            </Button>

        End If
    End Code

</div>