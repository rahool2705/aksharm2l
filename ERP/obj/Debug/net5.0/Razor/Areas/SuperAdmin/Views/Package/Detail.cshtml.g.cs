#pragma checksum "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "087b1e53a88871055ed91d278208f6494541e072"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_SuperAdmin_Views_Package_Detail), @"mvc.1.0.view", @"/Areas/SuperAdmin/Views/Package/Detail.cshtml")]
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
#line 10 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\_ViewImports.cshtml"
using Business.Entities.Dynamic;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
using Business.SQL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
using GridCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"087b1e53a88871055ed91d278208f6494541e072", @"/Areas/SuperAdmin/Views/Package/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9944a1c594c39df98753be59f62bb100aa180b8b", @"/Areas/SuperAdmin/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_SuperAdmin_Views_Package_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ISGrid>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-info px-5 ladda-button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "SuperAdmin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Package", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AssingFormToPackage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("AssignFormPackageMaster"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/gridmvc.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::GridMvc.TagHelpers.GridTagHelper __GridMvc_TagHelpers_GridTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
  
    ViewData["Title"] = "Package List..";
    Layout = "~/Views/Shared/_LayoutMaster.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-sm-12 col-md-12 col-lg-12 mx-auto\">\r\n    <div class=\"card border-1 shadow rounded-7 p-1\">\r\n        <div class=\"row border-bottom\">\r\n\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("\r\n        </div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "087b1e53a88871055ed91d278208f6494541e0729751", async() => {
                WriteLiteral("\r\n\r\n            <div class=\"row\">\r\n\r\n                <div class=\"col-md-12 mt-3\">\r\n                    <h3>\r\n                        <h1>Package :");
#nullable restore
#line 51 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
                                Write(ViewBag.PackageName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </h1>\r\n                    </h3>\r\n                </div>\r\n                <div class=\"col-md-12\">\r\n");
                WriteLiteral("                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("grid", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "087b1e53a88871055ed91d278208f6494541e07210592", async() => {
                }
                );
                __GridMvc_TagHelpers_GridTagHelper = CreateTagHelper<global::GridMvc.TagHelpers.GridTagHelper>();
                __tagHelperExecutionContext.Add(__GridMvc_TagHelpers_GridTagHelper);
#nullable restore
#line 56 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
__GridMvc_TagHelpers_GridTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("model", __GridMvc_TagHelpers_GridTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                </div>
                <div class=""col-md-12 text-center mb-3"">
                    <button type =""button"" class=""btn btn-primary btn-info px-5 ladda-button"" id=""CheckedAllForm"" value=""Select All"">Select All</button>
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "087b1e53a88871055ed91d278208f6494541e07212180", async() => {
                    WriteLiteral("Submit");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Area = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    <button type=\"reset\" class=\"btn btn-primary btn-info px-5 ladda-button\">Reset</button>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "087b1e53a88871055ed91d278208f6494541e07214159", async() => {
                    WriteLiteral("Back");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

        <div class=""offcanvas offcanvas-end"" tabindex=""-1"" id=""canvas_Package"">
            <div class=""offcanvas-header"">
                <h5 class=""offcanvas-title"" id=""canvasHeader""></h5>
                <button type=""button"" class=""btn-close text-reset"" data-bs-dismiss=""offcanvas"" aria-l abel=""Close""></button>
            </div>
            <div class=""offcanvas-body pt-3 pb-5"">
                <div id=""dvInfo"">
                </div>
            </div>
        </div>


    </div>
</div>
<script type=""text/javascript"">
    var urladd = '");
#nullable restore
#line 82 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
             Write(Url.Action("Get", "Package"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
    function fnPackage(obj) {
        var _key = $(obj).data('key');
        var _id = $(obj).data('id');
        if (_id > 0)
            document.getElementById(""canvasHeader"").innerHTML = ""Update Package"";
        else
            document.getElementById(""canvasHeader"").innerHTML = ""Add Package"";
        var _parameters = { id: _id, key: _key };
        $.ajax({
            url: urladd,
            type: ""POST"",
            data: _parameters,
            success: function(data, textStatus, jqXHR) {
                $('#dvInfo').html(data);
                $(""#canvas_Package"").show();//('hide');
            }
        });
    }

    var urlisActiveInActive = '");
#nullable restore
#line 102 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
                          Write(Url.Action("ActiveInActivePackageForm","Package"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
    function fnPackageFormActiveInactive(PackageID, FormID) {
        var _packageID = PackageID;//$(this).data('id');
        var _formID = FormID; //$(""#EmployeeID"").val();
        var _checkBox = $(""#packageFormActiveInactive"").is(':checked');
        var _docParameters = { packageID: _packageID, formID: _formID, isActive: _checkBox };
        $.ajax({
            url: urlisActiveInActive,
            type: 'POST',
            data: _docParameters,
            success: function(data) {
                if (data.status) {
                    var test = Lobibox.notify('success', {
                        pauseDelayOnHover: true,
                        size: 'mini',
                        icon: 'bx bx-check-circle',
                        continueDelayOnInactiveTab: false,
                        position: 'bottom right',
                        msg: data.message,
                    });
                }
                else {
                    Lobibox.notify('error', {
        ");
            WriteLiteral(@"                pauseDelayOnHover: true,
                        size: 'mini',
                        icon: 'bx bx-check-circle',
                        continueDelayOnInactiveTab: false,
                        position: 'bottom right',
                        msg: '");
#nullable restore
#line 130 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
                         Write(MessageHelper.Error);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                    });
                }
                $(""#packageFormActiveInactive"").load(window.location.href + "" #packageFormActiveInactive"");
            },
            error: function(error) {
                laddaStop(ls);
                Lobibox.notify('error', {
                    pauseDelayOnHover: true,
                    size: 'mini',
                    icon: 'bx bx-check-circle',
                    continueDelayOnInactiveTab: false,
                    position: 'bottom right',
                    msg: '");
#nullable restore
#line 143 "C:\Projects\ERP Backups\aksharm2l\ERP\Areas\SuperAdmin\Views\Package\Detail.cshtml"
                     Write(MessageHelper.Error);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                });
            }
        }).always(function() {
            hideloader();
        });
    }
    //$('#CheckedAllForm').click(function(){ $('.packageFormActiveInactive').prop('checked', true); }};

    $(document).ready(function() {
        $(""#CheckedAllForm"").click(function() {
            $('.packageFormActiveInactive').prop('checked', true);
            //$(""#packageFormActiveInactive"").val($(""#packageFormActiveInactive"").val() === ""Select All"" ? ""Diselect All"" : ""Select All"");

        });
    });

</script>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "087b1e53a88871055ed91d278208f6494541e07221933", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ISGrid> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
