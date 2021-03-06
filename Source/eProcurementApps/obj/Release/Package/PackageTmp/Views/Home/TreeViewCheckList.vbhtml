﻿@ModelType eProcurementApps.Models.ROLE_HELPER
@Imports eProcurementApps.Helpers

@Code
    ViewBag.Breadcrumbs = "Home"
    ViewBag.Title = "TreeView CheckList"
    ViewBag.Home = "active open"
    ViewBag.TreeViewCheckList = "active"
End Code

<!-- /section:basics/navbar.layout -->
<div class="main-container" id="main-container">

    <!-- /section:basics/sidebar -->
    <div class="main-content">
        <div class="main-content-inner">

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">

                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->
                        <!-- #section:plugins/fuelux.treeview -->
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="widget-box widget-color-blue2">
                                    <div class="widget-header">
                                        <h4 class="widget-title lighter smaller">Choose Categories</h4>
                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main padding-8">
                                            <ul id="tree1"></ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- PAGE CONTENT ENDS -->
                    </div><!-- /.col -->
                </div><!-- /.row -->
            </div><!-- /.page-content -->
        </div>
    </div><!-- /.main-content -->


</div><!-- /.main-container -->
<!-- page specific plugin scripts -->
<script src="~/Ace/assets/js/fuelux/fuelux.tree.js"></script>

<!-- inline scripts related to this page -->
<script type="text/javascript">
    jQuery(function ($) {
        var sampleData = initiateDemoData();//see below


        $('#tree1').ace_tree({
            dataSource: sampleData['dataSource1'],
            multiSelect: true,
            cacheItems: true,
            'open-icon': 'ace-icon tree-minus',
            'close-icon': 'ace-icon tree-plus',
            'selectable': true,
            'selected-icon': 'ace-icon fa fa-check',
            'unselected-icon': 'ace-icon fa fa-times',
            loadingHTML: '<div class="tree-loading"><i class="ace-icon fa fa-refresh fa-spin blue"></i></div>'
        });

        function initiateDemoData() {
            var tree_data = {
                'for-sale': { text: 'For Sale', type: 'folder' },
                'vehicles': { text: 'Vehicles', type: 'folder' },
                'rentals': { text: 'Rentals', type: 'folder' },
                'real-estate': { text: 'Real Estate', type: 'folder' },
                'pets': { text: 'Pets', type: 'folder' },
                'tickets': { text: 'Tickets', type: 'item' },
                'services': { text: 'Services', type: 'item' },
                'personals': { text: 'Personals', type: 'item' }
            }
            tree_data['for-sale']['additionalParameters'] = {
                'children': {
                    'appliances': { text: 'Appliances', type: 'item' },
                    'arts-crafts': { text: 'Arts & Crafts', type: 'item' },
                    'clothing': { text: 'Clothing', type: 'item' },
                    'computers': { text: 'Computers', type: 'item' },
                    'jewelry': { text: 'Jewelry', type: 'item' },
                    'office-business': { text: 'Office & Business', type: 'item' },
                    'sports-fitness': { text: 'Sports & Fitness', type: 'item' }
                }
            }
            tree_data['vehicles']['additionalParameters'] = {
                'children': {
                    'cars': { text: 'Cars', type: 'folder' },
                    'motorcycles': { text: 'Motorcycles', type: 'item' },
                    'boats': { text: 'Boats', type: 'item' }
                }
            }
            tree_data['vehicles']['additionalParameters']['children']['cars']['additionalParameters'] = {
                'children': {
                    'classics': { text: 'Classics', type: 'item' },
                    'convertibles': { text: 'Convertibles', type: 'item' },
                    'coupes': { text: 'Coupes', type: 'item' },
                    'hatchbacks': { text: 'Hatchbacks', type: 'item' },
                    'hybrids': { text: 'Hybrids', type: 'item' },
                    'suvs': { text: 'SUVs', type: 'item' },
                    'sedans': { text: 'Sedans', type: 'item' },
                    'trucks': { text: 'Trucks', type: 'item' }
                }
            }

            tree_data['rentals']['additionalParameters'] = {
                'children': {
                    'apartments-rentals': { text: 'Apartments', type: 'item' },
                    'office-space-rentals': { text: 'Office Space', type: 'item' },
                    'vacation-rentals': { text: 'Vacation Rentals', type: 'item' }
                }
            }
            tree_data['real-estate']['additionalParameters'] = {
                'children': {
                    'apartments': { text: 'Apartments', type: 'item' },
                    'villas': { text: 'Villas', type: 'item' },
                    'plots': { text: 'Plots', type: 'item' }
                }
            }
            tree_data['pets']['additionalParameters'] = {
                'children': {
                    'cats': { text: 'Cats', type: 'item' },
                    'dogs': { text: 'Dogs', type: 'item' },
                    'horses': { text: 'Horses', type: 'item' },
                    'reptiles': { text: 'Reptiles', type: 'item' }
                }
            }

            var dataSource1 = function (options, callback) {
                var $data = null
                if (!("text" in options) && !("type" in options)) {
                    $data = tree_data;//the root tree
                    callback({ data: $data });
                    return;
                }
                else if ("type" in options && options.type == "folder") {
                    if ("additionalParameters" in options && "children" in options.additionalParameters)
                        $data = options.additionalParameters.children || {};
                    else $data = {}//no data
                }

                if ($data != null)//this setTimeout is only for mimicking some random delay
                    setTimeout(function () { callback({ data: $data }); }, parseInt(Math.random() * 500) + 200);

                //we have used static data here
                //but you can retrieve your data dynamically from a server using ajax call
                //checkout examples/treeview.html and examples/treeview.js for more info
            }


            return { 'dataSource1': dataSource1 }
        }

    });
</script>
