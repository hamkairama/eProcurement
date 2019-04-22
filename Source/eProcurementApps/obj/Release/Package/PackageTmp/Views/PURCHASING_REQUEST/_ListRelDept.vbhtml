@ModelType IEnumerable(Of eProcurementApps.Models.TPROC_FORM_SUBTYPE_DT)


<div class="form-group" id="dept_id_table">
    @code
        @If Model IsNot Nothing Then
            @<Table Class="table table-striped table-bordered table-hover">
                <tr>

                    @For each item_gr In Model
                        @<td>
                            @*Flow : @item_gr.FLOW_NUMBER &nbsp; &nbsp; &nbsp; &nbsp;  Department : *@ <b>@item_gr.TPROC_REL_DEPT.RELATED_DEPARTMENT_NAME</b> 
                            <table>
                                @For Each item_dt In item_gr.TPROC_REL_DEPT.TPROC_APPR_RELDEPT_GR.TPROC_APPR_RELDEPT_DT
                                    @<tr>
                                        <td>@item_dt.USER_NAME &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                        <td>@item_dt.TPROC_LEVEL.INDONESIAN_LEVEL</td>
                                    </tr>
                                Next
                            </table>
                        </td>
                    Next
                </tr>
            </Table>
        End If
    End Code

</div>