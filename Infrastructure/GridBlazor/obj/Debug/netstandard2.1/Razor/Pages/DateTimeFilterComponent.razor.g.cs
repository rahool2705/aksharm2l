#pragma checksum "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "acdf90aba5a5845042dfb0eb56cf757b830668e1"
// <auto-generated/>
#pragma warning disable 1591
namespace GridBlazor.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\_Imports.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
using GridBlazor.Resources;

#line default
#line hidden
#nullable disable
    public partial class DateTimeFilterComponent<T> : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 5 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
 if (Visible)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "dropdown dropdown-menu grid-dropdown opened");
            __builder.AddAttribute(2, "style", "display:block;position:relative;" + (
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                  "margin-left:" + _offset.ToString() + "px;"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(3, "onkeyup", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                                                                                                FilterKeyup

#line default
#line hidden
#nullable disable
            ));
            __builder.AddEventStopPropagationAttribute(4, "onclick", true);
            __builder.AddEventStopPropagationAttribute(5, "onkeyup", true);
            __builder.AddElementReferenceCapture(6, (__value) => {
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                                                                      dateTimeFilter = __value;

#line default
#line hidden
#nullable disable
            }
            );
            __builder.AddMarkupContent(7, "\r\n    ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "grid-dropdown-arrow");
            __builder.AddAttribute(10, "style", 
#nullable restore
#line 8 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                              "margin-left:" + (-_offset).ToString() + "px;"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n    ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "grid-dropdown-inner");
            __builder.AddMarkupContent(14, "\r\n        ");
            __builder.OpenElement(15, "div");
            __builder.AddAttribute(16, "class", "grid-popup-widget");
            __builder.AddMarkupContent(17, "\r\n            ");
            __builder.OpenElement(18, "div");
            __builder.AddAttribute(19, "class", "grid-filter-body");
            __builder.AddMarkupContent(20, "\r\n");
#nullable restore
#line 12 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                 for (int i = 0; i < _filters.Count(); i++)
                {
                    int j = i;
                    if (j == 1)
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(21, "                        ");
            __builder.OpenElement(22, "div");
            __builder.AddAttribute(23, "class", "form-group");
            __builder.AddAttribute(24, "style", "display:flex;justify-content:center;");
            __builder.AddMarkupContent(25, "\r\n                            ");
            __builder.OpenElement(26, "div");
            __builder.AddMarkupContent(27, "\r\n                                ");
            __builder.OpenElement(28, "select");
            __builder.AddAttribute(29, "class", "grid-filter-cond form-control");
            __builder.AddAttribute(30, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 19 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                     _condition

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(31, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => _condition = __value, _condition));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddMarkupContent(32, "\r\n                                    ");
            __builder.OpenElement(33, "option");
            __builder.AddAttribute(34, "value", "1");
#nullable restore
#line 20 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(35, Strings.And);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n                                    ");
            __builder.OpenElement(37, "option");
            __builder.AddAttribute(38, "value", "2");
#nullable restore
#line 21 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(39, Strings.Or);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(40, "\r\n                                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(41, "\r\n                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(42, "\r\n                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n");
#nullable restore
#line 25 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                    }
                    else if (j > 1)
                    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(44, "                        ");
            __builder.OpenElement(45, "div");
            __builder.AddAttribute(46, "class", "form-group");
            __builder.AddAttribute(47, "style", "display:flex;justify-content:center;");
            __builder.AddMarkupContent(48, "\r\n                            ");
            __builder.OpenElement(49, "div");
            __builder.AddMarkupContent(50, "\r\n                                ");
            __builder.OpenElement(51, "select");
            __builder.AddAttribute(52, "class", "grid-filter-cond form-control");
            __builder.AddAttribute(53, "disabled", "disabled");
            __builder.AddAttribute(54, "value", 
#nullable restore
#line 30 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                          _condition

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(55, "\r\n                                    ");
            __builder.OpenElement(56, "option");
            __builder.AddAttribute(57, "value", "1");
#nullable restore
#line 31 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(58, Strings.And);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(59, "\r\n                                    ");
            __builder.OpenElement(60, "option");
            __builder.AddAttribute(61, "value", "2");
#nullable restore
#line 32 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(62, Strings.Or);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(63, "\r\n                                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(66, "\r\n");
#nullable restore
#line 36 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                    }

#line default
#line hidden
#nullable disable
            __builder.AddContent(67, "                    ");
            __builder.OpenElement(68, "div");
            __builder.AddAttribute(69, "class", "form-group row");
            __builder.AddMarkupContent(70, "\r\n                        ");
            __builder.OpenElement(71, "div");
            __builder.AddAttribute(72, "class", "col-md-6 my-2");
            __builder.AddMarkupContent(73, "\r\n");
#nullable restore
#line 39 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                             if (j == 0)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(74, "                                ");
            __builder.OpenElement(75, "label");
            __builder.AddAttribute(76, "class", "control-label");
            __builder.OpenElement(77, "b");
#nullable restore
#line 41 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(78, Strings.FilterTypeLabel);

#line default
#line hidden
#nullable disable
            __builder.AddContent(79, ":");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(80, "\r\n                                ");
            __builder.OpenElement(81, "div");
            __builder.AddMarkupContent(82, "\r\n                                    ");
            __builder.OpenElement(83, "select");
            __builder.AddAttribute(84, "class", "grid-filter-type form-control");
            __builder.AddAttribute(85, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 43 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                            _filters[j].Type

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(86, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => _filters[j].Type = __value, _filters[j].Type));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddElementReferenceCapture(87, (__value) => {
#nullable restore
#line 43 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                  firstSelect = __value;

#line default
#line hidden
#nullable disable
            }
            );
            __builder.AddMarkupContent(88, "\r\n                                        ");
            __builder.OpenElement(89, "option");
            __builder.AddAttribute(90, "value", "1");
#nullable restore
#line 44 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(91, Strings.Equal);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(92, "\r\n                                        ");
            __builder.OpenElement(93, "option");
            __builder.AddAttribute(94, "value", "10");
#nullable restore
#line 45 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(95, Strings.NotEqual);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(96, "\r\n                                        ");
            __builder.OpenElement(97, "option");
            __builder.AddAttribute(98, "value", "5");
#nullable restore
#line 46 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(99, Strings.GreaterThan);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(100, "\r\n                                        ");
            __builder.OpenElement(101, "option");
            __builder.AddAttribute(102, "value", "6");
#nullable restore
#line 47 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(103, Strings.LessThan);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(104, "\r\n                                        ");
            __builder.OpenElement(105, "option");
            __builder.AddAttribute(106, "value", "7");
#nullable restore
#line 48 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(107, Strings.GreaterThanOrEquals);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(108, "\r\n                                        ");
            __builder.OpenElement(109, "option");
            __builder.AddAttribute(110, "value", "8");
#nullable restore
#line 49 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(111, Strings.LessThanOrEquals);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(112, "\r\n");
#nullable restore
#line 50 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                         if (GridHeaderComponent.Column.Filter.IsNullable)
                                        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(113, "                                            ");
            __builder.OpenElement(114, "option");
            __builder.AddAttribute(115, "value", "11");
#nullable restore
#line 52 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(116, Strings.IsNull);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(117, "\r\n                                            ");
            __builder.OpenElement(118, "option");
            __builder.AddAttribute(119, "value", "12");
#nullable restore
#line 53 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(120, Strings.IsNotNull);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(121, "\r\n");
#nullable restore
#line 54 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(122, "                                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(123, "\r\n                                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(124, "\r\n");
#nullable restore
#line 57 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(125, "                                ");
            __builder.OpenElement(126, "div");
            __builder.AddMarkupContent(127, "\r\n                                    ");
            __builder.OpenElement(128, "select");
            __builder.AddAttribute(129, "class", "grid-filter-type form-control");
            __builder.AddAttribute(130, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 61 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                         _filters[j].Type

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(131, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => _filters[j].Type = __value, _filters[j].Type));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddMarkupContent(132, "\r\n                                        ");
            __builder.OpenElement(133, "option");
            __builder.AddAttribute(134, "value", "1");
#nullable restore
#line 62 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(135, Strings.Equal);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(136, "\r\n                                        ");
            __builder.OpenElement(137, "option");
            __builder.AddAttribute(138, "value", "10");
#nullable restore
#line 63 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(139, Strings.NotEqual);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(140, "\r\n                                        ");
            __builder.OpenElement(141, "option");
            __builder.AddAttribute(142, "value", "5");
#nullable restore
#line 64 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(143, Strings.GreaterThan);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(144, "\r\n                                        ");
            __builder.OpenElement(145, "option");
            __builder.AddAttribute(146, "value", "6");
#nullable restore
#line 65 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(147, Strings.LessThan);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(148, "\r\n                                        ");
            __builder.OpenElement(149, "option");
            __builder.AddAttribute(150, "value", "7");
#nullable restore
#line 66 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(151, Strings.GreaterThanOrEquals);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(152, "\r\n                                        ");
            __builder.OpenElement(153, "option");
            __builder.AddAttribute(154, "value", "8");
#nullable restore
#line 67 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(155, Strings.LessThanOrEquals);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(156, "\r\n");
#nullable restore
#line 68 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                         if (GridHeaderComponent.Column.Filter.IsNullable)
                                        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(157, "                                            ");
            __builder.OpenElement(158, "option");
            __builder.AddAttribute(159, "value", "11");
#nullable restore
#line 70 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(160, Strings.IsNull);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(161, "\r\n                                            ");
            __builder.OpenElement(162, "option");
            __builder.AddAttribute(163, "value", "12");
#nullable restore
#line 71 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(164, Strings.IsNotNull);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(165, "\r\n");
#nullable restore
#line 72 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(166, "                                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(167, "\r\n                                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(168, "\r\n");
#nullable restore
#line 75 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(169, "                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(170, "\r\n                        ");
            __builder.OpenElement(171, "div");
            __builder.AddAttribute(172, "class", "col-md-6 my-2");
            __builder.AddMarkupContent(173, "\r\n");
#nullable restore
#line 78 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                             if (j == 0)
                            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(174, "                                ");
            __builder.OpenElement(175, "label");
            __builder.AddAttribute(176, "class", "control-label");
            __builder.OpenElement(177, "b");
#nullable restore
#line 80 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(178, Strings.FilterValueLabel);

#line default
#line hidden
#nullable disable
            __builder.AddContent(179, ":");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(180, "\r\n");
#nullable restore
#line 81 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(181, "                            ");
            __builder.OpenElement(182, "div");
            __builder.AddMarkupContent(183, "\r\n                                ");
            __builder.OpenElement(184, "input");
            __builder.AddAttribute(185, "type", "date");
            __builder.AddAttribute(186, "placeholder", "yyyy-mm-dd");
            __builder.AddAttribute(187, "class", "grid-filter-input form-control");
            __builder.AddAttribute(188, "value", 
#nullable restore
#line 83 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                                           _filters[j].Value

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(189, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 83 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                                                                                         (ChangeEventArgs __e) => _filters[j].Value = __e.Value.ToString()

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(190, "\r\n                            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(191, "\r\n                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(192, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(193, "\r\n");
#nullable restore
#line 87 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.AddContent(194, "                ");
            __builder.OpenElement(195, "div");
            __builder.AddAttribute(196, "class", "grid-buttons");
            __builder.AddMarkupContent(197, "\r\n                    ");
            __builder.OpenElement(198, "div");
            __builder.AddAttribute(199, "class", "grid-filter-buttons");
            __builder.AddMarkupContent(200, "\r\n                        ");
            __builder.OpenElement(201, "button");
            __builder.AddAttribute(202, "type", "button");
            __builder.AddAttribute(203, "class", "btn btn-primary");
            __builder.AddAttribute(204, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 90 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                ApplyButtonClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(205, "\r\n                            ");
#nullable restore
#line 91 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(206, Strings.ApplyFilterButtonText);

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(207, "\r\n                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(208, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(209, "\r\n                    ");
            __builder.OpenElement(210, "div");
            __builder.AddAttribute(211, "class", "grid-filter-buttons");
            __builder.AddMarkupContent(212, "\r\n                        ");
            __builder.OpenElement(213, "button");
            __builder.AddAttribute(214, "type", "button");
            __builder.AddAttribute(215, "class", "btn btn-primary");
            __builder.AddAttribute(216, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 95 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                () => AddColumnFilterValue()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(217, "<b>+</b>");
            __builder.CloseElement();
            __builder.AddMarkupContent(218, "\r\n");
#nullable restore
#line 96 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                         if (_filters.Length > 1)
                        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(219, "                            ");
            __builder.OpenElement(220, "button");
            __builder.AddAttribute(221, "type", "button");
            __builder.AddAttribute(222, "class", "btn btn-primary");
            __builder.AddAttribute(223, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 98 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                    () => RemoveColumnFilterValue()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(224, "<b>-</b>");
            __builder.CloseElement();
            __builder.AddMarkupContent(225, "\r\n");
#nullable restore
#line 99 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                        }

#line default
#line hidden
#nullable disable
            __builder.AddContent(226, "                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(227, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(228, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(229, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(230, "\r\n        ");
            __builder.OpenElement(231, "div");
            __builder.AddAttribute(232, "class", "grid-popup-additional");
            __builder.AddMarkupContent(233, "\r\n");
#nullable restore
#line 105 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
             if (_clearVisible)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(234, "                ");
            __builder.OpenElement(235, "ul");
            __builder.AddAttribute(236, "class", "menu-list");
            __builder.AddMarkupContent(237, "\r\n                    ");
            __builder.OpenElement(238, "li");
            __builder.AddMarkupContent(239, "\r\n                        ");
            __builder.OpenElement(240, "a");
            __builder.AddAttribute(241, "class", "grid-filter-clear");
            __builder.AddAttribute(242, "href", "javascript:void(0);");
            __builder.AddAttribute(243, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 109 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
                                                                                          ClearButtonClicked

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(244, "\r\n                            ");
#nullable restore
#line 110 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
__builder.AddContent(245, Strings.ClearFilterLabel);

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(246, "\r\n                        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(247, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(248, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(249, "\r\n");
#nullable restore
#line 114 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(250, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(251, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(252, "\r\n");
            __builder.CloseElement();
            __builder.AddMarkupContent(253, "\r\n");
#nullable restore
#line 118 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\DateTimeFilterComponent.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
