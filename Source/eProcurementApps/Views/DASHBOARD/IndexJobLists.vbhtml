@imports eProcurementApps.Models
@imports eProcurementApps.Helpers
@Imports eProcurementApps.Controllers.DASHBOARDController

@Code
    ViewBag.Breadcrumbs = "Dashboard"
    ViewBag.Title = "Inbox Job Lists"
End Code

        

@*<span id="txt_linkProc" class="hidden">@CommonFunction.GetLinkEproc</span>*@
<div class="hidden">
    <span id="txt_user_id">@ViewBag.UserId</span>
    <span id="txt_user_mail">@ViewBag.UserMail</span>
</div>

<div class="tabbable">
    <ul class="nav nav-tabs" id="myTab"> 
        <li class="@Split(isActive, ";")(0)">
            <a data-toggle="tab" href="#wa" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprWA))">
                <i class="green ace-icon fa fa-puzzle-piece bigger-120"></i>
                Approval Item PR - WA
            </a>
        </li>

        <li class="@Split(isActive, ";")(1)">
            <a data-toggle="tab" href="#rd" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprRD))">
                <i Class="blue ace-icon fa fa-file-text bigger-120"></i>
                Approval PR - RelDept
            </a>
        </li>

        <li class="@Split(isActive, ";")(2)">
            <a data-toggle="tab" href="#rq_a" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprReq))">
                <i Class="green ace-icon fa fa-bullhorn bigger-120"></i>
                Need Approval Request
            </a>
        </li>

        <li class="@Split(isActive, ";")(3)">
            <a data-toggle="tab" href="#rq_c" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.CompReq))">
                <i Class="blue ace-icon fa fa-bullhorn bigger-120"></i>
                Need Complete Request
            </a>
        </li>

        <li class="@Split(isActive, ";")(4)">
            <a data-toggle="tab" href="#pc_vrf" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.VrfPC))">
                <i Class="red ace-icon fa fa-glass bigger-120"></i>
                Verify PC
            </a>
        </li>

        <li class="@Split(isActive, ";")(5)">
            <a data-toggle="tab" href="#pc_ack" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprAckPC))">
                <i Class="yellow ace-icon fa fa-glass bigger-120"></i>
                Approval Acknow PC
            </a>
        </li>

        <li class="@Split(isActive, ";")(6)">
            <a data-toggle="tab" href="#pc_appr" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprPC))">
                <i Class="green ace-icon fa fa-glass bigger-120"></i>
                Approval PC
            </a>
        </li>

        <li class="@Split(isActive, ";")(7)">
            <a data-toggle="tab" href="#po_vrf" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.VrfPO))">
                <i Class="red ace-icon fa fa-laptop bigger-120"></i>
                Verify PO
            </a>
        </li>

        <li class="@Split(isActive, ";")(8)">
            <a data-toggle="tab" href="#po" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprPO))">
                <i Class="blue ace-icon fa fa-laptop bigger-120"></i>
                Approval/Review PO
            </a>
        </li>

        <li class="@Split(isActive, ";")(9)">
            <a data-toggle="tab" href="#gm" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprGM))">
                <i Class="blue ace-icon fa fa-laptop bigger-120"></i>
                Approval Good Match
            </a>
        </li>

        <li class="@Split(isActive, ";")(10)">
            <a data-toggle="tab" href="#vr_crv" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.VrCRV))">
                <i Class="blue ace-icon fa fa-laptop bigger-120"></i>
                Verify CRV
            </a>
        </li>

        <li class="@Split(isActive, ";")(11)">
            <a data-toggle="tab" href="#appr_crv" onclick="IndexJobListsjs(@Convert.ToInt32(ListEnum.FlagInbox.ApprCRV))">
                <i Class="blue ace-icon fa fa-laptop bigger-120"></i>
                Approval CRV
            </a>
        </li>
    </ul>

    <div Class="tab-content">
        <div id = "wa" Class="tab-pane fade In active">           
        </div>

        <div id = "rd" Class="tab-pane fade">
        </div>     
        
        <div id = "rq_a" Class="tab-pane fade">
        </div>    

        <div id = "rq_c" Class="tab-pane fade">
        </div> 

        <div id = "pc_vrf" Class="tab-pane fade">
        </div>

        <div id = "pc_ack" Class="tab-pane fade">
        </div>

        <div id = "pc_appr" Class="tab-pane fade">
        </div> 

        <div id = "po_vrf" Class="tab-pane fade">
        </div>

        <div id = "po" Class="tab-pane fade">
        </div>

        <div id = "gm" Class="tab-pane fade">
        </div>

        <div id = "vr_crv" Class="tab-pane fade">
        </div>

        <div id = "appr_crv" Class="tab-pane fade">
        </div>

        <table id = "dynamic-table" Class="table table-striped table-bordered table-hover">
            <thead>
                <tr>
                    <th> Request No</th>
                    <th> Request By</th>
                    <th> Request Date</th>
                    <th style = "color:red" >
                        View
                    </th>
                </tr>
            </thead>

            <tbody id = "renderInbox" >
                @Html.Partial("_ListApprPR")
            </tbody>

        </table>

    </div>
</div>

<script src="~/Scripts/Controllers/DASHBOARDController.js" ></script>

@*<script src="~/Ace/assets/js/dataTables/jquery.dataTables.js"></script>
<script src="~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js"></script>
<script src="~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js"></script>
<script src="~/Scripts/Standard/StandardTable.js"></script>

<script type="text/javascript">
    // initiate dataTables plugin
    var oTable1 =
    $('#dynamic-table')
    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
    .dataTable({
        bAutoWidth: false,
        "aoColumns": [
          { "bSortable": false },
          { "bSortable": false }
        ],
        "aaSorting": [],

        //,
        //"sScrollY": "200px",
        //"bPaginate": false,

        //"sScrollX" "100%",
        //"sScrollXInner": "120%",
        //"bScrollCollapse": true,
        //Note: If you Then are applying horizontal scrolling (sScrollX) On a ".table-bordered"
        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

        //"iDisplayLength" 50
    });
</script>*@

