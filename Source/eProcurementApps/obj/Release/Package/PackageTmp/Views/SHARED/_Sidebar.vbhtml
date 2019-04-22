@imports eProcurementApps.Helpers

<script type="text/javascript">
    try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
</script>
<div class="sidebar-shortcuts" id="sidebar-shortcuts">
    <div class="sidebar-shortcuts-large" id="sidebar-shortcuts-large">
        <a class="btn btn-success" href="@Url.Action("Index", "DASHBOARD")" title="Dashboard">
            <i class="menu-icon fa fa-tachometer"></i>
        </a>

        @If Session("USER_ID_ID") Is Nothing Then
            @<a Class="btn btn-info" href="@Url.Action("Create", "User", New With {.flag = 1})" title="Create PR">
                <i Class="menu-icon fa fa-pencil"></i>
            </a>
        Else
            @<a Class="btn btn-info" href="@Url.Action("Create", "PURCHASING_REQUEST")" title="Create PR">
                <i Class="menu-icon fa fa-pencil"></i>
            </a>
        End If

        <a Class="btn btn-warning" href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = 0, .SubTitle = ""})" title="My List PR">
            <i Class="menu-icon fa fa-list"></i>
        </a>
        <a Class="btn btn-danger" href="@Url.Action("IndexJobLists", "DASHBOARD")" title="Inbox Job Lists">
            <i Class="menu-icon fa fa-inbox "></i>
        </a>
    </div>
    <div Class="sidebar-shortcuts-mini" id="sidebar-shortcuts-mini">
        <span Class="btn btn-success"></span><span class="btn btn-info"></span><span class="btn btn-warning">
        </span><span class="btn btn-danger"></span>
    </div>
</div>

<ul Class="nav nav-list">
    @If UserAccess.IsInRole("MNU01") Then
        @<li Class="@ViewBag.Setup">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-cogs"></i>
                <span Class="menu-text">Setup</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU02") Then
                    @<li Class="@ViewBag.IndexCurrency">
                        <a href="@Url.Action("Index", "CURRENCY")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Currency </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU03") Then
                    @<li Class="@ViewBag.IndexVat">
                        <a href="@Url.Action("Index", "VAT")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Vat </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU04") Then
                    @<li Class="@ViewBag.IndexPph">
                        <a href="@Url.Action("Index", "PPH")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Pph </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU05") Then
                    @<li Class="@ViewBag.IndexRelatedDepartment">
                        <a href="@Url.Action("Index", "RELATED_DEPARTMENT", New With {.flag = 0})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Related Department </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU06") Then
                    @<li Class="@ViewBag.IndexDivision">
                        <a href="@Url.Action("Index", "DIVISION")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Division </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU07") Then
                    @<li Class="@ViewBag.IndexBudgetCode">
                        <a href="@Url.Action("Index", "BUDGET_CODE")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> SUN Budget </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU08") Then
                    @<li Class="@ViewBag.IndexGoodType">
                        <a href="@Url.Action("Index", "GOOD_TYPE")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Good Type </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU09") Then
                    @<li Class="@ViewBag.IndexFormType">
                        <a href="@Url.Action("Index", "FORM_TYPE")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Form Type </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU10") Then
                    @<li Class="@ViewBag.IndexFormSubType">
                        <a href="@Url.Action("Index", "FORM_SUB_TYPE", New With {.flag = 0})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Form Sub Type </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU11") Then
                    @<li Class="@ViewBag.IndexHoliday">
                        <a href="@Url.Action("Index", "HOLIDAY")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Holiday </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU13") Then
                    @<li Class="@ViewBag.IndexPoType">
                        <a href="@Url.Action("Index", "PO_TYPE")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> PO Type </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU14") Then
                    @<li Class="@ViewBag.IndexDeliveryAddress">
                        <a href="@Url.Action("Index", "DELIVERY_ADDRESS")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Delivery Address </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU15") Then
                    @<li Class="@ViewBag.IndexSupplier">
                        <a href="@Url.Action("Index", "SUPPLIER")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Vendor </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU16") Then
                    @<li Class="@ViewBag.IndexRole">
                        <a href="@Url.Action("Index", "Role")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Role </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU17") Then
                    @<li Class="@ViewBag.IndexUser">
                        <a href="@Url.Action("Index", "User", New With {.flag = 0})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> User </span>
                        </a>
                         @*<a href="#" onclick="GetDataByRowStat()">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> User </span>
                         </a>*@
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU18") Then
                    @<li Class="@ViewBag.IndexLevel">
                        <a href="@Url.Action("Index", "Level")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Level </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU19") Then
                    @<li Class="@ViewBag.IndexWA">
                        <a href="@Url.Action("Index", "WA", New With {.flag = 0})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Work Area </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU20") Then
                    @<li Class="@ViewBag.IndexStock">
                        <a href="@Url.Action("Index", "Stock")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Item </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU23") Then
                    @<li Class="@ViewBag.IndexCOA">
                        <a href="@Url.Action("Index", "CHART_OF_ACCOUNT", New With {.flag = 0})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Chart of Account </span>
                        </a>

                        <b Class="arrow"></b>
                    </li>
                End If

                 @If UserAccess.IsInRole("MNU62") Then
                    @<li Class="@ViewBag.IndexApprovalRole">
                         <a href="@Url.Action("Index", "APPROVAL_ROLE")">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> Verify/Approval </span>
                         </a>

                        <b Class="arrow"></b>
                    </li>
                 End If

            </ul>
        </li>
    End If

    @If UserAccess.IsInRole("MNU21") Then
        @<li Class="@ViewBag.PurchasingRequest">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-pencil"></i>
                <span Class="menu-text">Purchasing Request</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU22") Then
                    @<li Class="@ViewBag.IndexCreatePR">
                        @If Session("USER_ID_ID") Is Nothing Then
                            @<a href="@Url.Action("Create", "User", New With {.flag = 1})" title="Create PR">
                                <i Class="icon-chevron-right"></i>
                                <span Class="menu-text"> Create PR </span>
                            </a>
                        Else
                            @<a href="@Url.Action("Create", "PURCHASING_REQUEST")">
                                <i Class="icon-chevron-right"></i>
                                <span Class="menu-text"> Create PR </span>
                            </a>
                        End If
                        <b Class="arrow"></b>
                    </li>
                End If

                <li Class="@ViewBag.MyListPR">
                    <a href="#" Class="dropdown-toggle">
                        <i Class="menu-icon fa fa-list"></i>
                        <span Class="menu-text">My List PR</span>
                        <b Class="arrow icon-angle-down"></b>
                    </a>
                    <ul Class="submenu">
                        @If UserAccess.IsInRole("MNU24") Then
                            @<li Class="@ViewBag.IndexListPR">
                                <a href = "@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListPR), .SubTitle = ""})">
                                    <i Class="icon-chevron-right"></i>
                                    <span Class="menu-text"> My List PR </span>
                                </a>
                                <b Class="arrow"></b>
                            </li>
                        End If

                        @If UserAccess.IsInRole("MNU25") Then
                             @<li Class="@ViewBag.IndexListApprPRWA">
                                <a href = "@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRWA), .SubTitle = "Approval/Review WA Of Each item"})">
                                    <i Class="icon-chevron-right"></i>
                                    <span Class="menu-text"> My List Approval PR WA </span>
                                </a>
                                <b Class="arrow"></b>
                            </li>
                        End If

                        @If UserAccess.IsInRole("MNU26") Then
                            @<li Class="@ViewBag.IndexListApprPRRelDept">
                                <a href = "@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyListApprovalPRRD), .SubTitle = "Approval Rel. Dept Of PR"})">
                                    <i Class="icon-chevron-right"></i>
                                    <span Class="menu-text"> My List Approval PR RD </span>
                                </a>
                                <b Class="arrow"></b>
                            </li>
                        End If

                        @If UserAccess.IsInRole("MNU24") Then
                             @<li Class="@ViewBag.IndexMyPRReadyToSignOff">
                                <a href = "@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.MyPRReadyToSignOff), .SubTitle = "My PRs Ready To Sign Off"})">
                                    <i Class="icon-chevron-right"></i>
                                    <span Class="menu-text"> My PRs Ready To Sign Off </span>
                                </a>
                                <b Class="arrow"></b>
                            </li>
                        End If                             
                    </ul>
                </li>


                @If UserAccess.IsInRole("MNU29") Then
                    @<li Class="@ViewBag.EprocListPR">
                        <a href="#" Class="dropdown-toggle">
                            <i Class="menu-icon fa fa-list"></i>
                            <span Class="menu-text">eProc List PR</span>
                            <b Class="arrow icon-angle-down"></b>
                        </a>
                        <ul Class="submenu">

                            @If UserAccess.IsInRole("MNU29") Then
                                @<li Class="@ViewBag.IndexAllListPR">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.AllListPR), .SubTitle = "All List PR"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> All List PR </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU30") Then
                                @<li Class="@ViewBag.IndexAllPRReadyToHandle">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToHandle), .SubTitle = "PRs Ready To Handle"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> PRs Ready To Handle </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU31") Then
                                @<li Class="@ViewBag.IndexAllPRReadyToCreatePO">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToCreatePO), .SubTitle = "PRs Ready To Create PO"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> PRs Ready To Create PO </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU45") Then
                                @<li Class="@ViewBag.IndexAllPRReadyToComplete">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToComplete), .SubTitle = "PRs Ready To Complete"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> PRs Ready To Complete </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU46") Then
                                @<li Class="@ViewBag.IndexAllPRReadyToSignOff">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.PRsReadyToSignOff), .SubTitle = "PRs Ready To Sign Off"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> PRs Ready To Sign Off </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If


                        </ul>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU32") Then
                    @<li Class="@ViewBag.GroupListPR">
                        <a href="#" Class="dropdown-toggle">
                            <i Class="menu-icon fa fa-list"></i>
                            <span Class="menu-text">Group List PR</span>
                            <b Class="arrow icon-angle-down"></b>
                        </a>
                        <ul Class="submenu">
                            @If UserAccess.IsInRole("MNU32") Then
                                @<li Class="@ViewBag.IndexListPRBySubmitter">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRBySubmitter), .SubTitle = "List PR By Submitter"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> List PR By Submitter </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If


                            @If UserAccess.IsInRole("MNU33") Then
                                @<li Class="@ViewBag.IndexListPRComplete">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRComplete), .SubTitle = "List PR Complete"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> List PR Complete </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If


                            @If UserAccess.IsInRole("MNU34") Then
                                @<li Class="@ViewBag.IndexListPRSignOff">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRSignOff), .SubTitle = "List PR Sign Off"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> List PR Sign Off </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If


                            @If UserAccess.IsInRole("MNU35") Then
                                @<li Class="@ViewBag.IndexListPRRejected">
                                    <a href="@Url.Action("IndexListPR", "PURCHASING_REQUEST", New With {.FlagDetail = Convert.ToInt32(ListEnum.FlagDetail.ListPRReject), .SubTitle = "List PR Reject"})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> List PR Reject </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                        </ul>
                    </li>
                End If

            </ul>
        </li>
    End If

     @If UserAccess.IsInRole("MNU56") Then
        @<li Class="@ViewBag.PriceComparison">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-glass"></i>
                <span Class="menu-text">Price comparison</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU60") Then
                    @<li Class="@ViewBag.IndexCreateComparison">
                         <a href="@Url.Action("Create", "PRICE_COMPARISON")">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> Create Comparison </span>
                         </a>
                        <b Class="arrow"></b>
                    </li>
                End If

            
                    @*@<li Class="@ViewBag.IndexListPriceComDraft">
                         <a href="#" onclick="ModalCommon('/Error/Maintaince/', '.dialogForm')" data-toggle="modal">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> List PriceCom Draft </span>
                         </a>
                        <b Class="arrow"></b>
                    </li>*@
            

                 @If UserAccess.IsInRole("MNU65") Then
                    @<li Class="@ViewBag.IndexListPriceCom">
                         <a href="@Url.Action("_ListPC", "PRICE_COMPARISON")" >
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> List PriceCom </span>
                         </a>
                        <b Class="arrow"></b>
                    </li>
                 End If
            </ul>
        </li>
     End If


    @If UserAccess.IsInRole("MNU27") Then
        @<li Class="@ViewBag.PurchaseOrder">
                <a href="#" Class="dropdown-toggle">
                    <i Class="menu-icon fa  fa-laptop"></i>
                    <span Class="menu-text">Purchase Order</span>
                    <b Class="arrow icon-angle-down"></b>
                </a>
                <ul Class="submenu">
                    @If UserAccess.IsInRole("MNU28") Then
                        @<li Class="@ViewBag.IndexCreatePO">
                            <a href="@Url.Action("Create", "PURCHASE_ORDER")">
                                <i Class="icon-chevron-right"></i>
                                <span Class="menu-text"> Create PO </span>
                            </a>
                            <b Class="arrow"></b>
                        </li>

                    End If

                    @If UserAccess.IsInRole("MNU48") Then
                        @<li Class="@ViewBag.IndexListPO">
                            <a href="@Url.Action("_ListPO", "PURCHASE_ORDER")">
                                <i Class="icon-chevron-right"></i>
                                <span Class="menu-text"> List PO </span>
                            </a>
                            <b Class="arrow"></b>
                        </li>

                    End If
                </ul>
            </li>
    End If   

    @If UserAccess.IsInRole("MNU63") Then
        @<li Class="@ViewBag.Matching">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa 	fa-key"></i>
                <span Class="menu-text">Matching</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU67") Then
                    @<li Class="@ViewBag.CreateGoodMatch">
                         <a href="@Url.Action("FormInput", "GM")">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> Create Good Match </span>
                         </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU67") Then
                    @<li Class="@ViewBag.IndexGoodMatch">
                        <a href="@Url.Action("Index", "GM")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> List Good Match </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If
                @If UserAccess.IsInRole("MNU68") Then
                    @<li Class="@ViewBag.IndexInvoiceMatch">
                         <a href="@Url.Action()">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> Invoice Match </span>
                         </a>
                         <b Class="arrow"></b>
                    </li>
                End If
            </ul>
        </li>
    End If


    @If UserAccess.IsInRole("MNU57") Then
        @<li Class="@ViewBag.CRV">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-credit-card"></i>
                <span Class="menu-text">CRV</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU61") Then
                    @<li Class="@ViewBag.IndexCreateCRV">
                         <a href="@Url.Action("FormInput", "CRV")">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> Create CRV </span>
                         </a>
                        <b Class="arrow"></b>
                    </li>
                End If
                @If UserAccess.IsInRole("MNU66") Then
                    @<li Class="@ViewBag.IndexListCRV">
                         <a href="@Url.Action("Index", "CRV")">
                             <i Class="icon-chevron-right"></i>
                             <span Class="menu-text"> List CRV </span>
                         </a>
                         <b Class="arrow"></b>
                    </li>
                End If
            </ul>
        </li>
    End If

    @If UserAccess.IsInRole("MNU36") Then
        @<li Class="@ViewBag.Report">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa  fa-folder-open "></i>
                <span Class="menu-text">Report</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU69") Then
                    @<li Class="@ViewBag.IndexReportTrans">
                        <a href="#" Class="dropdown-toggle">
                            <i Class="menu-icon fa fa-list"></i>
                            <span Class="menu-text">Trans</span>
                            <b Class="arrow icon-angle-down"></b>
                        </a>
                        <ul Class="submenu">
                            @If UserAccess.IsInRole("MNU69") Then
                                @<li Class="@ViewBag.IndexReportCRV">
                                    <a href="@Url.Action("ReportCRV", "CRV")">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> Report CRV </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU69") Then
                                @<li Class="@ViewBag.IndexReportPURCHASE_ORDER">
                                    <a href="@Url.Action("ReportPurchaseOrder", "PURCHASE_ORDER")">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> Report Purchase Order </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU69") Then
                                @<li Class="@ViewBag.IndexReportStockMovement">
                                    <a href="@Url.Action("ReportStockmovement", "STOCK")">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> Report Stockmovement </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU69") Then
                                @<li Class="@ViewBag.IndexReportStockmovementMonthly">
                                    <a href="@Url.Action("ReportStockmovementMonthly", "STOCK")">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> Report Stockmovement Monthly </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If
                        </ul>
                    </li>
                End If
                
                @If UserAccess.IsInRole("MNU37") Then
                    @<li Class="@ViewBag.IndexReportMyListPR">
                        <a href="@Url.Action("ReportMyListPR", "REPORT")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text">Report My List PR</span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU38") Then
                    @<li Class="@ViewBag.IndexReportTAT">
                        <a href="#" Class="dropdown-toggle">
                            <i Class="menu-icon fa fa-list"></i>
                            <span Class="menu-text">TAT</span>
                            <b Class="arrow icon-angle-down"></b>
                        </a>
                        <ul Class="submenu">
                            @If UserAccess.IsInRole("MNU38") Then
                                @<li Class="@ViewBag.IndexReportTATUnComplete">
                                    <a href="@Url.Action("ReportTAT", "REPORT", New With {.flag = Convert.ToInt32(ListEnum.FlagReport.TatUnComplete)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> TAT InProgress </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU38") Then
                                @<li Class="@ViewBag.IndexReportTATComplete">
                                    <a href="@Url.Action("ReportTAT", "REPORT", New With {.flag = Convert.ToInt32(ListEnum.FlagReport.TatComplete)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> TAT Complete </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU38") Then
                                @<li Class="@ViewBag.IndexReportTATSignOff">
                                    <a href="@Url.Action("ReportTAT", "REPORT", New With {.flag = Convert.ToInt32(ListEnum.FlagReport.TatSignOff)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text"> TAT Sign Off </span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If
                        </ul>
                    </li>
                End If
            </ul>
        </li>
    End If

    @If UserAccess.IsInRole("MNU42") Then
        @<li Class="@ViewBag.DailyOperation">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-cutlery"></i>
                <span Class="menu-text">Daily Operation</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU43") Then
                    @<li Class="@ViewBag.IndexDailyOperationStock">
                        <a href="@Url.Action("DailyOperationStock", "DAILY_OPERATION")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Stock </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU64") Then
                    @<li Class="@ViewBag.IndexDailyOperationStockSummary">
                        <a href="@Url.Action("DailyOperationStockSummary", "DAILY_OPERATION")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Stock Summary </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU44") Then
                    @<li Class="@ViewBag.IndexDailyOperationNonStock">
                        <a href="@Url.Action("DailyOperationNonStock", "DAILY_OPERATION")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Non Stock </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If
            </ul>
        </li>
    End If

    @If UserAccess.IsInRole("MNU39") Then
        @<li Class="@ViewBag.Request">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-bullhorn "></i>
                <span Class="menu-text">Request Self Service</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU51") Then
                    @<li Class="@ViewBag.IndexRequestUser">
                        <a href="@Url.Action("IndexRequestUser", "USER")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> User </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU52") Then
                    @<li Class="@ViewBag.IndexRequestWA">
                        <a href="@Url.Action("Index", "WA", New With {.flag = 1})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Work Area </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU53") Then
                    @<li Class="@ViewBag.IndexRequestRD">
                        <a href="@Url.Action("Index", "RELATED_DEPARTMENT", New With {.flag = 1})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Related Department </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU54") Then
                    @<li Class="@ViewBag.IndexRequestCOA">
                        <a href="@Url.Action("Index", "CHART_OF_ACCOUNT", New With {.flag = 1})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Chart of Account </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU55") Then
                    @<li Class="@ViewBag.IndexRequestFST">
                        <a href="@Url.Action("Index", "FORM_SUB_TYPE", New With {.flag = 1})">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Form Subtype </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If

                @If UserAccess.IsInRole("MNU39") Then
                    @<li Class="@ViewBag.ListRequest">
                        <a href="#" Class="dropdown-toggle">
                            <i Class="menu-icon fa fa-list"></i>
                            <span Class="menu-text">List Request</span>
                            <b Class="arrow icon-angle-down"></b>
                        </a>
                        <ul Class="submenu">
                            @If UserAccess.IsInRole("MNU58") Then
                                @<li Class="@ViewBag.IndexListOutstanding">
                                    <a href="@Url.Action("IndexRequestList", "Request", New With {.status = Convert.ToDecimal(ListEnum.Request.Submitted)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text">Req Outstanding</span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU40") Then
                                @<li Class="@ViewBag.IndexListNeedApprove">
                                    <a href="@Url.Action("IndexRequestList", "Request", New With {.status = Convert.ToDecimal(ListEnum.Request.NeedApprove)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text">Req Need Approve</span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU41") Then
                                @<li Class="@ViewBag.IndexListNeedComplete">
                                    <a href="@Url.Action("IndexRequestList", "Request", New With {.status = Convert.ToDecimal(ListEnum.Request.NeedComplete)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text">Req Need Complete</span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If

                            @If UserAccess.IsInRole("MNU59") Then
                                @<li Class="@ViewBag.IndexListAlreadyCompleted">
                                    <a href="@Url.Action("IndexRequestList", "Request", New With {.status = Convert.ToDecimal(ListEnum.Request.Completed)})">
                                        <i Class="icon-chevron-right"></i>
                                        <span Class="menu-text">Req already Completed</span>
                                    </a>
                                    <b Class="arrow"></b>
                                </li>
                            End If
                        </ul>
                    </li>
                End If

            </ul>
        </li>
    End If

    @If UserAccess.IsInRole("MNU49") Then
        @<li Class="@ViewBag.Others">
            <a href="#" Class="dropdown-toggle">
                <i Class="menu-icon fa fa-file-o"></i>
                <span Class="menu-text">Others</span>
                <b Class="arrow icon-angle-down"></b>
            </a>
            <ul Class="submenu">
                @If UserAccess.IsInRole("MNU50") Then
                     @<li Class="@ViewBag.IndexDocuments">
                        <a href="@Url.Action("IndexDocuments", "Others")">
                            <i Class="icon-chevron-right"></i>
                            <span Class="menu-text"> Documents </span>
                        </a>
                        <b Class="arrow"></b>
                    </li>
                End If               
            </ul>
        </li>
    End If   


</ul>

<div Class="sidebar-toggle sidebar-collapse" id="sidebar-collapse">
    <i Class="ace-icon fa fa-angle-double-left" data-icon1="ace-icon fa fa-angle-double-left"
       data-icon2="ace-icon fa fa-angle-double-right"></i>
</div>

<Script type="text/javascript">
    try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
</Script>

@*<script>
    var linkProc = "http://localhost:16543"
    function GetDataByRowStat() {
        $.ajax({
            url: linkProc + '/SUPPLIER/List',
            type: 'Post',
            data: {
                //flag: 0,
            },
            cache: false,
            traditional: true,
            beforeSend:
                function () {
                    $("#loadingRole").toggle();
                },
            success: function (data) {
                $("#renderBody").html(data);
                $("#loadingRole").toggle();
            },
        });
    }
</script>*@

