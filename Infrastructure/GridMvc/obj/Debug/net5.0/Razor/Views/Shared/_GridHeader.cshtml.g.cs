#pragma checksum "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "53900b991a00f76277e362f4b09303e1f54a7a6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__GridHeader), @"mvc.1.0.view", @"/Views/Shared/_GridHeader.cshtml")]
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
#line 1 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridCore.Filtering;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridCore.Pagination;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridCore.Resources;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridCore.Sorting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridShared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridShared.Columns;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridShared.Filtering;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridShared.Sorting;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using GridShared.Utility;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using Microsoft.Extensions.Primitives;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
using System.Text.Json;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53900b991a00f76277e362f4b09303e1f54a7a6d", @"/Views/Shared/_GridHeader.cshtml")]
    #nullable restore
    public class Views_Shared__GridHeader : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IGridColumn>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 17 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
  
    const string ThClass = "grid-header";
    const string ThStyle = "display:none;";

    const string FilteredButtonCssClass = "filtered";
    const string FilterButtonCss = "fa fa-filter fa-fw";
    //grid - filter - btn
    List<ColumnFilterValue> _filterSettings;
    string _url;
    string _cssStyles;
    string _cssClass;
    string _cssFilterClass;
    string _cssSortingClass;
    bool _isColumnFiltered;
    StringValues _clearInitFilter;


    _filterSettings = new List<ColumnFilterValue>();
    IGridSettingsProvider _settings = ((ISGrid)(Model.ParentGrid)).Settings;
    if (_settings.FilterSettings.IsInitState(Model) && Model.InitialFilterSettings != ColumnFilterValue.Null)
    {
        _filterSettings.Add(Model.InitialFilterSettings);
    }
    else
    {
        _filterSettings.AddRange(_settings.FilterSettings.FilteredColumns.GetByColumn(Model));
    }

    _isColumnFiltered = _filterSettings.Any(r => r.FilterType != GridFilterType.Condition);

    //determine current url:
    var queryBuilder = new CustomQueryStringBuilder(_settings.FilterSettings.Query);

    var exceptQueryParameters = new List<string>
{
        QueryStringFilterSettings.DefaultTypeQueryParameter,
        QueryStringFilterSettings.DefaultClearInitFilterQueryParameter
    };

    string pagerParameterName = GetPagerQueryParameterName(((ISGrid)(Model.ParentGrid)).Pager);
    if (!string.IsNullOrEmpty(pagerParameterName))
    {
        exceptQueryParameters.Add(pagerParameterName);
    }

    _url = queryBuilder.GetQueryStringExcept(exceptQueryParameters);

    _clearInitFilter = _settings.FilterSettings.Query.Get(QueryStringFilterSettings.DefaultClearInitFilterQueryParameter);

    if (Model.Hidden)
    {
        _cssStyles = ((GridStyledColumn)Model).GetCssStylesString() + " " + ThStyle;
    }
    else
    {
        _cssStyles = ((GridStyledColumn)Model).GetCssStylesString();
    }
    _cssClass = ((GridStyledColumn)Model).GetCssClassesString() + " " + ThClass;

    // tables with fixed layout don't need to set up column width on the header
    if (Model.ParentGrid.TableLayout == TableLayout.Auto)
    {
        if (!string.IsNullOrWhiteSpace(Model.Width))
        {
            _cssStyles = string.Concat(_cssStyles, " width:", Model.Width, ";").Trim();
        }
    }

    if (Model.ParentGrid.Direction == GridDirection.RTL)
        _cssStyles = string.Concat(_cssStyles, " text-align:right;direction:rtl;").Trim();

    List<string> cssFilterClasses = new List<string>();
    cssFilterClasses.Add(FilterButtonCss);
    if (_isColumnFiltered)
    {
        cssFilterClasses.Add(FilteredButtonCssClass);
    }
    _cssFilterClass = string.Join(" ", cssFilterClasses);

    List<string> cssSortingClass = new List<string>();
    cssSortingClass.Add("grid-header-title");

    if (Model.IsSorted)
    {
        cssSortingClass.Add("sorted");
        cssSortingClass.Add(Model.Direction == GridSortDirection.Ascending ? "sorted-asc" : "sorted-desc");
    }
    _cssSortingClass = string.Join(" ", cssSortingClass);

    string _href = Context.Request.PathBase + Context.Request.Path + GetSortUrl(Model.Name, Model.Direction);

    string GetPagerQueryParameterName(IGridPager pager)
    {
        var defaultPager = pager as GridPager;
        if (defaultPager == null)
            return string.Empty;
        return defaultPager.ParameterName;
    }

    string GetSortUrl(string columnName, GridSortDirection? direction)
    {
        //determine current url:
        var builder = new CustomQueryStringBuilder(_settings.SortSettings.Query);
        string url = builder.GetQueryStringExcept(new[]
        {
            GridPager.DefaultPageQueryParameter,
            ((QueryStringSortSettings)_settings.SortSettings).ColumnQueryParameterName,
            ((QueryStringSortSettings)_settings.SortSettings).DirectionQueryParameterName
        });

        if (Model.IsSorted)
        {
            if (Model.Direction == GridSortDirection.Ascending)
            {
                if (string.IsNullOrEmpty(url))
                {
                    url = "?";
                }
                else
                {
                    url += "&";
                }

                return string.Format("{0}{1}={2}&{3}={4}", url,
                    ((QueryStringSortSettings)_settings.SortSettings).ColumnQueryParameterName, columnName,
                    ((QueryStringSortSettings)_settings.SortSettings).DirectionQueryParameterName,
                    ((int)GridSortDirection.Descending).ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                if (Model.InitialDirection.HasValue)
                {
                    if (string.IsNullOrEmpty(url))
                    {
                        url = "?";
                    }
                    else
                    {
                        url += "&";
                    }

                    return string.Format("{0}{1}={2}&{3}={4}", url,
                        ((QueryStringSortSettings)_settings.SortSettings).ColumnQueryParameterName, columnName,
                        ((QueryStringSortSettings)_settings.SortSettings).DirectionQueryParameterName,
                        ((int)GridSortDirection.Ascending).ToString(CultureInfo.InvariantCulture));
                }
                else if (string.IsNullOrEmpty(url))
                {
                    url = "?";
                }
                return url;
            }
        }
        else
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "?";
            }
            else
            {
                url += "&";
            }

            return string.Format("{0}{1}={2}&{3}={4}", url,
                ((QueryStringSortSettings)_settings.SortSettings).ColumnQueryParameterName, columnName,
                ((QueryStringSortSettings)_settings.SortSettings).DirectionQueryParameterName,
                ((int)GridSortDirection.Ascending).ToString(CultureInfo.InvariantCulture));
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<th");
            BeginWriteAttribute("class", " class=\"", 6596, "\"", 6614, 1);
#nullable restore
#line 189 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 6604, _cssClass, 6604, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 6615, "\"", 6634, 1);
#nullable restore
#line 189 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 6623, _cssStyles, 6623, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n    <div class=\"grid-header-group\">\r\n");
#nullable restore
#line 191 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
         if (Model.ParentGrid.ExtSortingEnabled)
        {
            if (Model.IsSorted)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div");
            BeginWriteAttribute("class", " class=\"", 6796, "\"", 6844, 2);
            WriteAttributeValue("", 6804, "grid-extsort-draggable", 6804, 22, true);
#nullable restore
#line 195 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue(" ", 6826, _cssSortingClass, 6827, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" draggable=\"true\" data-column=\"");
#nullable restore
#line 195 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                       Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n");
#nullable restore
#line 196 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
             if (Model.SortEnabled)
            {
                if (Model.IsSorted)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <a");
            BeginWriteAttribute("href", " href=\"", 7009, "\"", 7022, 1);
#nullable restore
#line 200 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 7016, _href, 7016, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-column=\"");
#nullable restore
#line 200 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                 Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-sorted=\"");
#nullable restore
#line 200 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                            Write(Model.Direction == GridSortDirection.Ascending ? "asc" : "desc");

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 200 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                               Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a> ");
#nullable restore
#line 200 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                                     }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <a");
            BeginWriteAttribute("href", " href=\"", 7234, "\"", 7247, 1);
#nullable restore
#line 203 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 7241, _href, 7241, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-column=\"");
#nullable restore
#line 203 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                 Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 203 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                              Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a> ");
#nullable restore
#line 203 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                    }
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <span data-column=\"");
#nullable restore
#line 207 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                      Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 207 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                   Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>");
#nullable restore
#line 207 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                           }

#line default
#line hidden
#nullable disable
#nullable restore
#line 208 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
             if (Model.Direction == GridSortDirection.Ascending)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-sort-up\"></i>\r\n");
#nullable restore
#line 211 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                    }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <i class=\"fa fa-sort-down\"></i>\r\n");
#nullable restore
#line 215 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div> \r\n");
#nullable restore
#line 217 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
            }
            else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div");
            BeginWriteAttribute("class", " class=\"", 7866, "\"", 7944, 5);
            WriteAttributeValue("", 7874, "sorted", 7874, 6, true);
            WriteAttributeValue(" ", 7880, "sorted-asc", 7881, 11, true);
            WriteAttributeValue(" ", 7891, "sorted-desc", 7892, 12, true);
            WriteAttributeValue(" ", 7903, "grid-extsort-draggable", 7904, 23, true);
#nullable restore
#line 220 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue(" ", 7926, _cssSortingClass, 7927, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" draggable=\"true\" data-column=\"");
#nullable restore
#line 220 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                 Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n");
#nullable restore
#line 221 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                         if (Model.SortEnabled)
                            {
                                if (Model.IsSorted)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a class=\"notsorted\"");
            BeginWriteAttribute("href", " href=\"", 8207, "\"", 8220, 1);
#nullable restore
#line 225 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 8214, _href, 8214, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-column=\"");
#nullable restore
#line 225 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                   Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-sorted=\"");
#nullable restore
#line 225 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                              Write(Model.Direction == GridSortDirection.Ascending ? "asc" : "desc");

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 225 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                                                 Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("  <i class=\"icon dripicons-sort-up-down\"></i></a> \r\n");
#nullable restore
#line 226 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                    }
                                else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a class=\"notsorted\"");
            BeginWriteAttribute("href", " href=\"", 8549, "\"", 8562, 1);
#nullable restore
#line 229 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 8556, _href, 8556, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-column=\"");
#nullable restore
#line 229 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                               Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 229 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                            Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("  <i class=\"icon dripicons-sort-up-down\"></i></a> \r\n");
#nullable restore
#line 230 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                     }
                             }
                         else
                             {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span data-column=\"");
#nullable restore
#line 234 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                  Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 234 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                               Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 235 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                             }

#line default
#line hidden
#nullable disable
#nullable restore
#line 236 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                          if (Model.IsSorted)
                             {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <span class=\"grid-sort-arrow\"></span> \r\n");
#nullable restore
#line 239 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                             }
                          else
                             {
                               
                             }

#line default
#line hidden
#nullable disable
            WriteLiteral("                     </div>\r\n");
#nullable restore
#line 245 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                }

         }
         else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 9322, "\"", 9347, 1);
#nullable restore
#line 250 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 9330, _cssSortingClass, 9330, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 251 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
         if (Model.SortEnabled && Model.IsSorted == false)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a");
            BeginWriteAttribute("href", " href=\"", 9444, "\"", 9457, 1);
#nullable restore
#line 253 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 9451, _href, 9451, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 253 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fa fa-sort-down\"></i></a>\r\n");
#nullable restore
#line 254 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
        }
        else if (Model.SortEnabled && Model.Direction == GridSortDirection.Descending)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a");
            BeginWriteAttribute("href", " href=\"", 9641, "\"", 9654, 1);
#nullable restore
#line 257 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 9648, _href, 9648, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 257 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fa fa-sort-down\"></i></a>\r\n");
#nullable restore
#line 258 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
        }
        else if (Model.SortEnabled && Model.Direction == GridSortDirection.Ascending)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a");
            BeginWriteAttribute("href", " href=\"", 9837, "\"", 9850, 1);
#nullable restore
#line 261 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 9844, _href, 9844, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 261 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fa fa-sort-up\"></i></a>\r\n");
#nullable restore
#line 262 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span>");
#nullable restore
#line 265 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
             Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 266 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 272 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
             }

#line default
#line hidden
#nullable disable
#nullable restore
#line 273 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
             if (Model.FilterEnabled)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"grid-filter\" data-type=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                       Write(Model.FilterWidgetTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-isnullable=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                     Write(Model.Filter.IsNullable);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-name=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                          Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-widgetdata=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                                                        Write(JsonSerializer.Serialize(Model.FilterWidgetData));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-filterdata=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                                                                                                                            Write(JsonSerializer.Serialize(_filterSettings));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-url=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                                                                                                                                                                                  Write(_url);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-clearinitfilter=\"");
#nullable restore
#line 275 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                                                                                                                                                                                                                                                                                                                               Write(_clearInitFilter.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                            <span");
            BeginWriteAttribute("class", " class=\"", 10568, "\"", 10592, 1);
#nullable restore
#line 276 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 10576, _cssFilterClass, 10576, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 10593, "\"", 10633, 1);
#nullable restore
#line 276 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
WriteAttributeValue("", 10601, Strings.FilterButtonTooltipText, 10601, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></span>\r\n                        </div>\r\n");
#nullable restore
#line 278 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridMvc\Views\Shared\_GridHeader.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</th>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IGridColumn> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
