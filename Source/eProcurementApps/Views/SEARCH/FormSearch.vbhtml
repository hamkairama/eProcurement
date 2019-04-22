@Imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.SearchController
 
<div class="modal-dialog" style="width:1000px">
    <div class="modal-content">
        <div class="modal-header no-padding">
            <div class="table-header">
                @Html.Raw(Labels.ButtonForm("Exit"))
                Search @HeaderForm Form
            </div>
        </div>
        <div class="modal-body no-padding"> 
            <div Class="page-content">
                <div id="loadingRole" style="display:none"><img src="@WebConfigKey.ImgLoading" style="font-size: 7.0em;" /> </div>
                <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            @For i = 0 To JsonDataTable.Columns.Count - 1
                                @if Split(HiddenHtml, ":")(i).ToString = "Y" Then
                                    @<th class="hidden"> @JsonDataTable.Columns(i).Caption.ToString() </th>
                                Else
                                    @<th> @JsonDataTable.Columns(i).Caption.ToString() </th>
                                End If
                            Next
                            <th></th>
                        </tr>
                    </thead>
                    <tbody id="table_ad">
                        @For i = 0 To JsonDataTable.Rows.Count - 1
                            @<tr id="@i">
                                @For a = 0 To JsonDataTable.Columns.Count - 1
                                    @if Split(HiddenHtml, ":")(a).ToString = "Y" Then
                                        @<td class="hidden" id=@("SEARCHDATA_" + i.ToString + a.ToString)> @JsonDataTable.Rows(i).Item(a) </td> 
                                    Else
                                        @<td id=@("SEARCHDATA_" + i.ToString + a.ToString)> @JsonDataTable.Rows(i).Item(a) </td>
                                    End If
                                Next
                                <td><a onclick="Passing('@i', '@InnerHtmlData')" title="Select" href="#">@Html.Raw(Labels.ButtonForm("Select"))</a></td>
                            </tr>
                        Next
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>


<Script src="~/Scripts/Standard/StandardModal.js"></Script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>


<script type="text/javascript">
    var oTable1 =
    $('#dynamic-table').DataTable({
        bAutoWidth: true,
        "aaSorting": [],
    });

    function Passing(no, innerhtmldata) { 
        var inhtmldata = innerhtmldata.split("|");
        for (indexData in inhtmldata) {
            var HeaderData = document.getElementById(inhtmldata[indexData]).innerHTML.trim();
            var SearchData = $('#SEARCHDATA_' + no + indexData).text().trim();
            if (HeaderData === SearchData) {
                $(".dialogForm").dialog("close");
                return;
            } 
            document.getElementById(inhtmldata[indexData]).innerHTML = SearchData;
        }
        $(".dialogForm").dialog("close");
    }
</script>