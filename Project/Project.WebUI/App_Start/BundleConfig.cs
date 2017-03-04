using System.Web;
using System.Web.Optimization;

namespace Project.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/cssNew").Include(
                     "~/Content/Inspinia/Work/css/bootstrap.min.css",
                     "~/Content/Inspinia/Work/css/animate.css",
                     "~/Content/Inspinia/Work/css/style.min.css"));
            bundles.Add(new StyleBundle("~/Content/site").Include(
                   "~/Content/Site.css"));
            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/Content/Inspinia/Work/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jqueryNew").Include(
                        "~/Content/Inspinia/Work/js/jquery-2.1.1.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Content/Inspinia/Work/js/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI
            bundles.Add(new StyleBundle("~/bundles/jqueryuiNew").Include(
                        "~/Content/Inspinia/Work/js/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrapNew").Include(
                      "~/Content/Inspinia/Work/js/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Content/Inspinia/Work/js/plugins/metisMenu/jquery.metisMenu.js",
                     // "~/Content/Inspinia/Work/js/plugins/pace/pace.min.js",
                      "~/Content/Inspinia/Work/js/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Content/Inspinia/Work/js/app/skin.config.min.js"));

            // SlimScroll
            // Удобная прокрутка внутри Div http://rocha.la/jQuery-slimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Content/Inspinia/Work/js/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            // Графики иконки http://benpickles.github.io/peity/
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Content/Inspinia/Work/js/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Content/Inspinia/Work/js/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/plugins/blueimp/css/").Include(
                      "~/Content/Inspinia/Work/css/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/lightboxGallery").Include(
                      "~/Content/Inspinia/Work/js/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline 
            // Снова графики http://omnipotent.net/jquery.sparkline/#s-about
            bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
                      "~/Content/Inspinia/Work/js/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/morris").Include(
                      "~/Content/Inspinia/Work/js/plugins/morris/raphael-2.1.0.min.js",
                      "~/Content/Inspinia/Work/js/plugins/morris/morris.js"));

            // Flot chart 
            // Графики http://www.flotcharts.org/
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Content/Inspinia/Work/js/plugins/flot/jquery.flot.js",
                      "~/Content/Inspinia/Work/js/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Content/Inspinia/Work/js/plugins/flot/jquery.flot.resize.js",
                      "~/Content/Inspinia/Work/js/plugins/flot/jquery.flot.pie.js",
                      "~/Content/Inspinia/Work/js/plugins/flot/jquery.flot.time.js",
                      "~/Content/Inspinia/Work/js/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/rickshaw").Include(
                      "~/Content/Inspinia/Work/js/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Content/Inspinia/Work/js/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            // Графики http://www.chartjs.org/
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Content/Inspinia/Work/js/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            // Чеки
            bundles.Add(new StyleBundle("~/plugins/iCheckStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/iCheck/custom.css"));

            // iCheck
            // Чеки
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Content/Inspinia/Work/js/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            // Плагин таблиц для jquery https://www.datatables.net/
            bundles.Add(new StyleBundle("~/plugins/dataTablesStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/dataTables/dataTables.bootstrap.css",
                      "~/Content/Inspinia/Work/css/plugins/dataTables/dataTables.responsive.css",
                      "~/Content/Inspinia/Work/css/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables
            // Плагин таблиц для jquery https://www.datatables.net/
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Content/Inspinia/Work/js/plugins/dataTables/jquery.dataTables.js",
                      "~/Content/Inspinia/Work/js/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Content/Inspinia/Work/js/plugins/dataTables/dataTables.responsive.js",
                      "~/Content/Inspinia/Work/js/plugins/dataTables/dataTables.tableTools.min.js"));

            // jeditable
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                      "~/Content/Inspinia/Work/js/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Content/plugins/jqGrid/jqGridStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Content/Inspinia/Work/js/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Content/Inspinia/Work/js/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/codeEditorStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/codemirror/codemirror.css",
                      "~/Content/Inspinia/Work/css/plugins/codemirror/ambiance.css"));

            // codeEditor
            bundles.Add(new ScriptBundle("~/plugins/codeEditor").Include(
                      "~/Content/Inspinia/Work/js/plugins/codemirror/codemirror.js",
                      "~/Content/Inspinia/Work/js/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor
            bundles.Add(new ScriptBundle("~/plugins/nestable").Include(
                      "~/Content/Inspinia/Work/js/plugins/nestable/jquery.nestable.js"));

            // validate
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Content/Inspinia/Work/js/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Content/Inspinia/Work/js/plugins/fullcalendar/moment.min.js",
                      "~/Content/Inspinia/Work/js/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Content/Inspinia/Work/js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Content/Inspinia/Work/js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/Content/plugins/ionRangeSlider/ionRangeStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Content/Inspinia/Work/css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange
            bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
                      "~/Content/Inspinia/Work/js/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/datapicker/datepicker3.css"));

            // dataPicker
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Content/Inspinia/Work/js/plugins/datapicker/bootstrap-datepicker.js",
                      "~/Content/Inspinia/Work/js/plugins/datapicker/locales/bootstrap-datepicker.ru.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/nouiSliderStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider
            bundles.Add(new ScriptBundle("~/plugins/nouiSlider").Include(
                      "~/Content/Inspinia/Work/js/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap
            bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                      "~/Content/Inspinia/Work/js/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/switchery/switchery.css"));

            // switchery
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Content/Inspinia/Work/js/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/plugins/chosen/chosenStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/chosen/chosen.css"));

            // chosen
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Content/Inspinia/Work/js/plugins/chosen/chosen.jquery.js"));

            // knob
            bundles.Add(new ScriptBundle("~/plugins/knob").Include(
                      "~/Content/Inspinia/Work/js/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/steps/jquery.steps.css"));

            // wizardSteps
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Content/Inspinia/Work/js/plugins/staps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/Content/plugins/dropzone/dropZoneStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/dropzone/basic.css",
                      "~/Content/Inspinia/Work/css/plugins/dropzone/dropzone.css"));

            // dropZone
            bundles.Add(new ScriptBundle("~/plugins/dropZone").Include(
                      "~/Content/Inspinia/Work/js/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/summernote/summernote.css",
                      "~/Content/Inspinia/Work/css/plugins/summernote/summernote-bs3.css"));

            // summernote
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Content/Inspinia/Work/js/plugins/summernote/summernote.min.js"));

            // toastr notification
            // Нотификации http://codeseven.github.io/toastr/demo.html
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Content/Inspinia/Work/js/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/toastr/toastr.min.css"));

            // gritter notification
            bundles.Add(new ScriptBundle("~/plugins/gritter").Include(
                      "~/Content/Inspinia/Work/js/plugins/gritter/jquery.gritter.min.js"));
            // gritter notification
            bundles.Add(new StyleBundle("~/plugins/gritterStyles").Include(
                      "~/Content/Inspinia/Work/js/plugins/gritter/jquery.gritter.css"));
           

            // color picker
            bundles.Add(new ScriptBundle("~/plugins/colorpicker").Include(
                      "~/Content/Inspinia/Work/js/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/Content/plugins/colorpicker/colorpickerStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Content/Inspinia/Work/js/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/Inspinia/Work/css/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Content/Inspinia/Work/js/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/Inspinia/Work/css/plugins/jsTree/style.css"));

            // Diff
            bundles.Add(new ScriptBundle("~/plugins/diff").Include(
                      "~/Content/Inspinia/Work/js/plugins/diff_match_patch/javascript/diff_match_patch.js",
                      "~/Content/Inspinia/Work/js/plugins/preetyTextDiff/jquery.pretty-text-diff.min.js"));

            // Idle timer
            bundles.Add(new ScriptBundle("~/plugins/idletimer").Include(
                      "~/Content/Inspinia/Work/js/plugins/idle-timer/idle-timer.min.js"));

            // Tinycon
            bundles.Add(new ScriptBundle("~/plugins/tinycon").Include(
                      "~/Content/Inspinia/Work/js/plugins/tinycon/tinycon.min.js"));
            // Tinycon
            bundles.Add(new ScriptBundle("~/plugins/wow").Include(
                      "~/Content/Inspinia/Work/js/wow.min.js"));
            

            bundles.Add(new StyleBundle("~/Content/landing").Include(
                     "~/scripts/LandingPage/css/bootstrap.min.css",
                     "~/scripts/LandingPage/css/animate.min.css",
                     "~/scripts/LandingPage/css/font-awesome.css",
                     "~/scripts/LandingPage/css/style.css"));
            bundles.Add(new ScriptBundle("~/scripts/landing").Include(
                      "~/scripts/LandingPage/scripts/jquery-2.1.1.js",
                       "~/scripts/LandingPage/scripts/bootstrap.min.js",
                       "~/scripts/LandingPage/scripts/classie.js",
                       "~/scripts/LandingPage/scripts/cbpAnimatedHeader.js",
                       "~/scripts/LandingPage/scripts/wow.min.js",
                       "~/scripts/LandingPage/scripts/inspinia.js"));

            BundleTable.EnableOptimizations = false;

        }
    }
}
