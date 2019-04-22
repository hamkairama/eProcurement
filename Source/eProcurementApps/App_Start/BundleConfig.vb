Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        'to clear ignorelist
        bundles.IgnoreList.Clear()

        bundles.Add(New ScriptBundle("~/Bundle/Ace/css").Include(
                        "~/Ace/assets/css/bootstrap.css",
                        "~/Ace/assets/css/font-awesome.css",
                        "~/Ace/assets/css/ace-fonts.css",
                        "~/Ace/assets/css/ace.css",
                        "~/Content/CustomRequired.css"
                   ))

        bundles.Add(New ScriptBundle("~/Bundle/TreeView/css").Include(
                        "~/Ace/assets/css/bootstrap.min.css",
                        "~/Ace/assets/css/font-awesome.min.css",
                        "~/Ace/assets/css/ace.min.css",
                        "~/Ace/assets/css/ace-custom.css"
                   ))

        bundles.Add(New ScriptBundle("~/Bundle/Ace/js").Include(
                        "~/Ace/assets/js/bootstrap.js",
                        "~/Ace/assets/js/ace/elements.scroller.js",
                        "~/Ace/assets/js/ace/elements.colorpicker.js",
                        "~/Ace/assets/js/ace/elements.fileinput.js",
                        "~/Ace/assets/js/ace/elements.typeahead.js",
                        "~/Ace/assets/js/ace/elements.wysiwyg.js",
                        "~/Ace/assets/js/ace/elements.spinner.js",
                        "~/Ace/assets/js/ace/elements.treeview.js",
                        "~/Ace/assets/js/ace/elements.wizard.js",
                        "~/Ace/assets/js/ace/elements.aside.js",
                        "~/Ace/assets/js/ace/ace.js",
                        "~/Ace/assets/js/ace/ace.ajax-content.js",
                        "~/Ace/assets/js/ace/ace.touch-drag.js",
                        "~/Ace/assets/js/ace/ace.sidebar.js",
                        "~/Ace/assets/js/ace/ace.sidebar-scroll-1.js",
                        "~/Ace/assets/js/ace/ace.submenu-hover.js",
                        "~/Ace/assets/js/ace/ace.widget-box.js",
                        "~/Ace/assets/js/ace/ace.settings.js",
                        "~/Ace/assets/js/ace/ace.settings-rtl.js",
                        "~/Ace/assets/js/ace/ace.settings-skin.js",
                        "~/Ace/assets/js/ace/ace.widget-on-reload.js",
                        "~/Ace/assets/js/ace-extra.js",
                        "~/Ace/assets/js/ace/ace.searchbox-autocomplete.js"
                ))

        bundles.Add(New ScriptBundle("~/Bundle/Ace/FormElements/css").Include(
                        "~/Ace/assets/css/jquery-ui.custom.css",
                        "~/Ace/assets/css/chosen.css",
                        "~/Ace/assets/css/datepicker.css",
                        "~/Ace/assets/css/bootstrap-timepicker.css",
                        "~/Ace/assets/css/daterangepicker.css",
                        "~/Ace/assets/css/bootstrap-datetimepicker.css",
                        "~/Ace/assets/css/colorpicker.css"
                    ))

        bundles.Add(New ScriptBundle("~/Bundle/Ace/FormElements/js").Include(
                        "~/Ace/assets/js/jquery-ui.custom.js",
                        "~/Ace/assets/js/jquery.ui.touch-punch.js",
                        "~/Ace/assets/js/chosen.jquery.js",
                        "~/Ace/assets/js/fuelux/fuelux.spinner.js",
                        "~/Ace/assets/js/date-time/bootstrap-datepicker.js",
                        "~/Ace/assets/js/date-time/bootstrap-timepicker.js",
                        "~/Ace/assets/js/date-time/moment.js",
                        "~/Ace/assets/js/date-time/daterangepicker.js",
                        "~/Ace/assets/js/date-time/bootstrap-datetimepicker.js",
                        "~/Ace/assets/js/bootstrap-colorpicker.js",
                        "~/Ace/assets/js/jquery.knob.js",
                        "~/Ace/assets/js/jquery.autosize.js",
                        "~/Ace/assets/js/jquery.inputlimiter.1.3.1.js",
                        "~/Ace/assets/js/jquery.maskedinput.js",
                        "~/Ace/assets/js/bootstrap-tag.js",
                        "~/Scripts/Standard/StandardForm.js"))


        bundles.Add(New ScriptBundle("~/Bundle/Ace/Jquery").Include(
                        "~/Ace/assets/js/jquery.js",
                        "~/Ace/assets/js/jquery1x.js",
                        "~/Ace/assets/js/jquery.mobile.custom.js"
                    ))


        bundles.Add(New ScriptBundle("~/Bundle/Ace/Table/js").Include(
                       "~/Ace/assets/js/dataTables/jquery.dataTables.js",
                        "~/Ace/assets/js/dataTables/jquery.dataTables.bootstrap.js",
                        "~/Ace/assets/js/dataTables/extensions/TableTools/js/dataTables.tableTools.js",
                        "~/Ace/assets/js/dataTables/extensions/ColVis/js/dataTables.colVis.js",
                        "~/Scripts/Standard/StandardTable.js"))


        bundles.Add(New ScriptBundle("~/Bundle/Ace/Profile/cs").Include(
                        "~/Ace/assets/css/jquery-ui.custom.css",
                        "~/Ace/assets/css/jquery.gritter.css",
                        "~/Ace/assets/css/select2.css",
                        "~/Ace/assets/css/datepicker.css",
                        "~/Ace/assets/css/bootstrap-editable.css"
                    ))

        bundles.Add(New ScriptBundle("~/Bundle/Ace/Profile/js").Include(
                        "~/Ace/assets/js/jquery-ui.custom.js",
                        "~/Ace/assets/js/jquery.ui.touch-punch.js",
                        "~/Ace/assets/js/jquery.gritter.js",
                        "~/Ace/assets/js/bootbox.js",
                        "~/Ace/assets/js/jquery.easypiechart.js",
                        "~/Ace/assets/js/date-time/bootstrap-datepicker.js",
                        "~/Ace/assets/js/jquery.hotkeys.js",
                        "~/Ace/assets/js/bootstrap-wysiwyg.js",
                        "~/Ace/assets/js/select2.js",
                        "~/Ace/assets/js/fuelux/fuelux.spinner.js",
                        "~/Ace/assets/js/x-editable/bootstrap-editable.js",
                        "~/Ace/assets/js/x-editable/ace-editable.js",
                        "~/Ace/assets/js/jquery.maskedinput.js"
                    ))

        bundles.Add(New ScriptBundle("~/Bundle/jquery").Include(
                        "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/Bundle/CustomRequired/css").Include(
                        "~/Content/Custom/CustomRequired.css"
                   ))

        bundles.Add(New ScriptBundle("~/Bundle/CustomRequired/js").Include(
                       "~/Scripts/Custom/CustomRequired.js"
                  ))

    End Sub
End Module

