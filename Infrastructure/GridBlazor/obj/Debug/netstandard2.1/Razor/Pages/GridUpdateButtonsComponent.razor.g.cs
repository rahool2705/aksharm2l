#pragma checksum "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c9982044b7a5b6237e39e7b8e9135d1476d056ad"
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
#line 1 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor"
using GridBlazor.Resources;

#line default
#line hidden
#nullable disable
    public partial class GridUpdateButtonsComponent<T> : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 3 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor"
 if (GridUpdateComponent._buttonsVisibility == 0)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(0, "    ");
            __builder.OpenElement(1, "button");
            __builder.AddAttribute(2, "type", "submit");
            __builder.AddAttribute(3, "class", "btn btn-primary btn-md");
#nullable restore
#line 5 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor"
__builder.AddContent(4, Strings.Save);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\r\n    ");
            __builder.OpenElement(6, "button");
            __builder.AddAttribute(7, "type", "button");
            __builder.AddAttribute(8, "class", "btn btn-primary btn-md");
            __builder.AddAttribute(9, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 6 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor"
                                                                   () => GridUpdateComponent.BackButtonClicked()

#line default
#line hidden
#nullable disable
            ));
#nullable restore
#line 6 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor"
__builder.AddContent(10, Strings.Back);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(11, "\r\n");
#nullable restore
#line 7 "C:\Projects\ERP Backups\aksharm2l\Infrastructure\GridBlazor\Pages\GridUpdateButtonsComponent.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
