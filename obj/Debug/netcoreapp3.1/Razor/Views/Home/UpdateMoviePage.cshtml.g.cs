#pragma checksum "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7a96fcbbe1ccefe60ff22e5edc84af771b8b4043"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UpdateMoviePage), @"mvc.1.0.view", @"/Views/Home/UpdateMoviePage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a96fcbbe1ccefe60ff22e5edc84af771b8b4043", @"/Views/Home/UpdateMoviePage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56bd25e31a79f8f8d033cf815a2e03d9049b04f6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UpdateMoviePage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateMovie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
  
    ViewData["AdminTitle"] = "Movies";
    Layout = "_LayoutAdmin";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"wrapper\" style=\"margin-top: 20px; \">\n    <div class=\"main\">\n        <div class=\"container-1\">\n\n\n            <div class=\"row\">\n                <div style=\"padding: 30px;\">\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a96fcbbe1ccefe60ff22e5edc84af771b8b40434688", async() => {
                WriteLiteral(@"
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""Title"">
                                Title
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <input class=""form-control"" id=""Title"" name=""Title"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 799, "\"", 832, 1);
#nullable restore
#line 22 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
WriteAttributeValue("", 807, Model.CurrentMovie.Title, 807, 25, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""Runtime"">
                                Runtime
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <input class=""form-control"" id=""Runtime"" name=""Runtime"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 1315, "\"", 1350, 1);
#nullable restore
#line 31 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
WriteAttributeValue("", 1323, Model.CurrentMovie.Runtime, 1323, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""Director"">
                                Select a Director
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <select class=""select form-control"" id=""Directors"" name=""Directors"">
");
#nullable restore
#line 41 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                 foreach (Director c in Model.Directors)
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a96fcbbe1ccefe60ff22e5edc84af771b8b40437496", async() => {
                    WriteLiteral("\n                                        ");
#nullable restore
#line 44 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                   Write(Html.DisplayFor(modelItem => c.name));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\n                                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 43 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                       WriteLiteral(c.Id);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
#nullable restore
#line 46 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                            </select>
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""Country"">
                                Select a Made Country
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <select class=""select form-control"" id=""Country"" name=""Country"">
");
#nullable restore
#line 57 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                 foreach (Country c in Model.Countries)
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a96fcbbe1ccefe60ff22e5edc84af771b8b404310606", async() => {
                    WriteLiteral("\n                                        ");
#nullable restore
#line 60 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                   Write(Html.DisplayFor(modelItem => c.name));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\n                                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 59 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                       WriteLiteral(c.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
#nullable restore
#line 62 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                            </select>
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""Poster"">
                                Poster URL
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <input class=""form-control"" id=""Poster"" name=""Poster"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 3537, "\"", 3571, 1);
#nullable restore
#line 72 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
WriteAttributeValue("", 3545, Model.CurrentMovie.Poster, 3545, 26, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""ReleasDate"">
                                Release Date
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <input class=""form-control"" id=""ReleasDate"" name=""ReleasDate"" placeholder=""MM/DD/YYYY"" type=""date""");
                BeginWriteAttribute("value", " value=\"", 4093, "\"", 4131, 1);
#nullable restore
#line 81 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
WriteAttributeValue("", 4101, Model.CurrentMovie.ReleasDate, 4101, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""AvgRating"">
                                Average Rating
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <input class=""form-control"" id=""AvgRating"" name=""AvgRating"" type=""text""");
                BeginWriteAttribute("value", " value=\"", 4627, "\"", 4664, 1);
#nullable restore
#line 90 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
WriteAttributeValue("", 4635, Model.CurrentMovie.AvgRating, 4635, 29, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        </div>
                        <div class=""form-group "">
                            <label class=""control-label requiredField"" for=""textarea"">
                                Movie Description
                                <span class=""asteriskField"">
                                    *
                                </span>
                            </label>
                            <textarea class=""form-control"" cols=""40"" id=""textarea"" name=""Description"" placeholder=""Description"" rows=""10"">

                                    ");
#nullable restore
#line 101 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
                               Write(Model.CurrentMovie.Description);

#line default
#line hidden
#nullable disable
                WriteLiteral("\n\n                            </textarea>\n                        </div>\n\n                        <div class=\"form-group\">\n                            <div>\n                                <input type=\"hidden\" name=\"Id\"");
                BeginWriteAttribute("value", " value=\"", 5489, "\"", 5507, 1);
#nullable restore
#line 108 "/Users/admin/Projects/MovNotifier/MovNotifier/Views/Home/UpdateMoviePage.cshtml"
WriteAttributeValue(" ", 5497, Model.Id, 5498, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                                <button class=""btn btn-primary "" name=""submit"" type=""submit"">
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </div>\n            </div>\n        </div>\n    </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
