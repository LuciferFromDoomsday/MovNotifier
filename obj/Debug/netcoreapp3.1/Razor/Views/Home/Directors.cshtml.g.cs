#pragma checksum "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bb16220692c25c1698585b4c543405034b832e99"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Directors), @"mvc.1.0.view", @"/Views/Home/Directors.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/_ViewImports.cshtml"
using MovNotifier;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/_ViewImports.cshtml"
using MovNotifier.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb16220692c25c1698585b4c543405034b832e99", @"/Views/Home/Directors.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56bd25e31a79f8f8d033cf815a2e03d9049b04f6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Directors : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MovNotifier.Models.Director>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateDirector", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/bower_components/datatables.net/js/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
  
    ViewData["AdminTitle"] = "Movies";
    Layout = "_LayoutAdmin";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""wrapper"" style=""margin-top: 20px; "">

    <div class=""row"">
        <div style=""padding: 30px;"">
            <button type=""button"" class=""btn btn-primary"" data-toggle=""modal"" data-target=""#addTask"">+ ADD Director </button>
            <!-- The Modal -->
            <div class=""modal fade"" id=""addTask"">
                <div class=""modal-dialog modal-xl"">
                    <div class=""modal-content "" style=""width: 1000px;"">

                        <!-- Modal Header -->
                        <div class=""modal-header"">
                            <h4 class=""modal-title"">Adding Actor </h4>
                            <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class=""modal-body"">
                            <div class=""container-fluid"">
                                <div class=""row"">
                                    <div class=""col-md-6 col-sm-6 col-xs-12"">

");
            WriteLiteral("\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb16220692c25c1698585b4c543405034b832e996403", async() => {
                WriteLiteral(@"
                                            <div class=""form-group "">
                                                <label class=""control-label requiredField"" for=""name"">
                                                    Director Full Name
                                                    <span class=""asteriskField"">
                                                        *
                                                    </span>
                                                </label>
                                                <div class=""input-group"">

                                                    <input class=""form-control"" id=""name"" name=""Name"" type=""text"" />
                                                </div>
                                            </div>
                                            <div class=""form-group"">
                                                <div>
                                                    <button class=""btn btn-primary "" name=""submit"" typ");
                WriteLiteral(@"e=""submit"">
                                                        Submit
                                                    </button>
                                                </div>
                                            </div>
                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class=""modal-footer"">
                            <button type=""button"" class=""btn btn-danger"" data-dismiss=""modal"">Close</button>
                        </div>

                    </div>
                </div>




            </div>



        </div>
        <div class=""col-8 col-m-12 col-sm-12"">
            <div class=""card"">
                <div class=""card-header"">
                    <h3>
                        All Directors
                    </h3>
                    <i class=""fas fa-ellipsis-h""></i>
                </div>
                <div class=""card-content"">
                    <table>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Name</th>

                                <");
            WriteLiteral("th>Actions</th>\n                            </tr>\n                        </thead>\n                        <tbody>\n\n");
#nullable restore
#line 92 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\n                                    <td>");
#nullable restore
#line 95 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    <td>");
#nullable restore
#line 96 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\n                                    <td><a");
            BeginWriteAttribute("href", " href=\"", 4047, "\"", 4087, 2);
            WriteAttributeValue("", 4054, "/Home/UpdateDirectorPage/", 4054, 25, true);
#nullable restore
#line 98 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
WriteAttributeValue("", 4079, item.Id, 4079, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Update</a> | <a");
            BeginWriteAttribute("href", " href=\"", 4104, "\"", 4111, 0);
            EndWriteAttribute();
            BeginWriteAttribute("onclick", " onclick=\"", 4112, "\"", 4140, 3);
            WriteAttributeValue("", 4122, "Delete(\'", 4122, 8, true);
#nullable restore
#line 98 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
WriteAttributeValue("", 4130, item.Id, 4130, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4138, "\')", 4138, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Delete</a></td>\n                                </tr>\n");
#nullable restore
#line 100 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\n                    </table>\n                </div>\n            </div>\n        </div>\n\n    </div>\n\n</div>\n<!-- DataTables -->\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb16220692c25c1698585b4c543405034b832e9913208", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb16220692c25c1698585b4c543405034b832e9914232", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<!-- page script -->
<script>
    $(function () {
        $('#example1').DataTable();
    });
    function Delete(id){
        var txt;
        var r = confirm(""Are you sure you want to Delete?"");
        if (r == true) {

            $.ajax(
            {
                type: ""POST"",
                url: '");
#nullable restore
#line 126 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/Directors.cshtml"
                 Write(Url.Action("DeleteDirector", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                data: {
                    id: id
                },
                error: function (result) {
                    alert(""error"");
                },
                success: function (result) {
                    if (result == true) {
                        var baseUrl=""/Home/Directors"";
                        window.location.reload();
                    }
                    else {
                        alert(""There is a problem, Try Later!"");
                    }
                }
            });
        }
    }
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MovNotifier.Models.Director>> Html { get; private set; }
    }
}
#pragma warning restore 1591
