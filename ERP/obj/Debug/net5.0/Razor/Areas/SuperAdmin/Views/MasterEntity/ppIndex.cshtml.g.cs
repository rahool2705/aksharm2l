#pragma checksum "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d38cb74adb972e45125b709549e51b0d72478c9e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_SuperAdmin_Views_MasterEntity_ppIndex), @"mvc.1.0.view", @"/Areas/SuperAdmin/Views/MasterEntity/ppIndex.cshtml")]
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
#line 1 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using ERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using ERP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using Business.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using Business.SQL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using ERP.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using ERP.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using ERP.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
using Business.Entities.Dynamic;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d38cb74adb972e45125b709549e51b0d72478c9e", @"/Areas/SuperAdmin/Views/MasterEntity/ppIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9944a1c594c39df98753be59f62bb100aa180b8b", @"/Areas/SuperAdmin/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_SuperAdmin_Views_MasterEntity_ppIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MasterEntityListMetadata>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
  
    ViewData["Title"] = "Master Entity List";
    Layout = "~/Views/Shared/_LayoutMaster.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<input type=\"hidden\" class=\"form-control\" id=\"txtID\" value=\"0\" />\r\n<input type=\"hidden\" class=\"form-control\" id=\"hdnKey\"");
            BeginWriteAttribute("value", " value=\"", 296, "\"", 304, 0);
            EndWriteAttribute();
            WriteLiteral(@" />

<div class=""page-content-inner"">
    <div class=""row"" id=""row-update"">
        <div class=""col-sm-12"">
            <div class=""portlet box blue"">
                <div class=""portlet-title"">
                    <div class=""caption"">
                        <i class=""fa fa-plus-circle""></i><span>Add Master Key</span>
                    </div>
                    <div class=""tools"">
                        <a href=""javascript:;"" class=""collapse"" id=""collapse"" data-original-title=""""");
            BeginWriteAttribute("title", " title=\"", 805, "\"", 813, 0);
            EndWriteAttribute();
            WriteLiteral("> </a>\r\n                    </div>\r\n                </div>\r\n                <div class=\"portlet-body form\">\r\n                    <!-- BEGIN FORM-->\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d38cb74adb972e45125b709549e51b0d72478c9e7084", async() => {
                WriteLiteral(@"
                        <div class=""form-body"">
                            <div class=""row"">
                                <div class=""col-md-6"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-3"">Name</label>
                                        <div class=""col-md-9"">
                                            <input type=""text"" maxlength=""100"" class=""form-control"" id=""txtName"" />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-md-6"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-3"">Value</label>
                                        <div class=""col-md-9"">
                                            <input type=""text"" maxlength=""100"" class=""form-control"" id=""txtValue"" />
          ");
                WriteLiteral(@"                              </div>
                                    </div>
                                </div>
                                <div class=""col-md-6"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-3"">Display Order</label>
                                        <div class=""col-md-9"">
                                            <input type=""text"" maxlength=""4"" class=""form-control mask_number"" id=""txtSortOrder"" value=""1"" />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-md-6"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-3"">Entry Type</label>
                                        <div class=""col-md-9"">
                                            <select class=""form-c");
                WriteLiteral("ontrol\" id=\"drpListEntryType\">\r\n");
#nullable restore
#line 57 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                                 foreach (var item in Model.EntryTypeLists)
                                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d38cb74adb972e45125b709549e51b0d72478c9e9959", async() => {
#nullable restore
#line 59 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                                                                           Write(item.Name);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 59 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                                       WriteLiteral(item.MasterListEntryTypeID);

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
                WriteLiteral("\r\n");
#nullable restore
#line 60 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-md-6"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-3"">Active</label>
                                        <div class=""col-md-9"">
                                            <input type=""checkbox"" id=""chkIsActive"" class=""make-switch"" data-on-text=""Yes"" data-off-text=""No"" />
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-md-6"">
                                    <div class=""form-group"">
                                        <label class=""control-label col-md-3"">Default Selected</label>
                                        <div class=""col-md-9""");
                WriteLiteral(@">
                                            <input type=""checkbox"" id=""IsDefaultSelected"" class=""make-switch"" data-on-text=""Yes"" data-off-text=""No"" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""form-actions right"">
                            <button type=""button"" class=""btn default"" onclick=""showList()"">Cancel</button>
                            <button type=""button"" class=""btn blue"" id=""btn-filter"" onclick=""SaveMasterList()""><i class=""fa fa-check""></i> Save</button>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <!-- END FORM-->
                </div>
            </div>
        </div>
    </div>

    <div class=""row"" id=""row-list"">
        <div class=""col-sm-12"">
            <div class=""portlet light"">
                <div class=""portlet-title"">
                    <div class=""caption"">
                        <i class=""fa fa-list-alt""></i>
                        <span class=""caption-subject bold uppercase""> Master Key List</span>
                    </div>
                    <div class=""actions"">
                        <a href=""javascript:showForm();defaultValueSet();"" class=""btn btn-default btn-sm"">
                            <i class=""fa fa-plus-circle""></i> Add Master Entry
                        </a>
                    </div>
                </div>
                <div class=""portlet-body form"">
                    <div class=""form-horizontal"" role=""form"">
                        <div class=""form-body"">
                            <div class=""row"">
            ");
            WriteLiteral(@"                    <div class=""col-md-12"">
                                    <table class=""table table-striped table-bordered table-hover"" id=""ListTable"">
                                        <thead>
                                            <tr>
                                                <th class=""text-left"">Name</th>
                                                <th style=""width:150px"" class=""text-center"">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
");
#nullable restore
#line 121 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                             foreach (var item in Model.MasterLists)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <tr>\r\n                                                    <td class=\"text-left\">");
#nullable restore
#line 124 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                                                     Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                    <td class=\"text-center\">\r\n                                                        <a href=\"javascript:void(0);\"");
            BeginWriteAttribute("onclick", " onclick=\"", 7234, "\"", 7272, 3);
            WriteAttributeValue("", 7244, "EditKeyMaster(\'", 7244, 15, true);
#nullable restore
#line 126 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
WriteAttributeValue("", 7259, item.Name, 7259, 10, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7269, "\');", 7269, 3, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-success btn-sm\">Edit</a>\r\n\r\n                                                        <a href=\"javascript:void(0);\"");
            BeginWriteAttribute("onclick", " onclick=\"", 7402, "\"", 7442, 3);
            WriteAttributeValue("", 7412, "DeleteKeyMaster(\'", 7412, 17, true);
#nullable restore
#line 128 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
WriteAttributeValue("", 7429, item.Name, 7429, 10, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7439, "\');", 7439, 3, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger btn-sm\">Delete</a>\r\n                                                    </td>\r\n                                                </tr>\r\n");
#nullable restore
#line 131 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\MasterEntity\ppIndex.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class=""row"" id=""row-detail-list"">
        <div class=""col-sm-12"" id=""dvValueList"">
");
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MasterEntityListMetadata> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
