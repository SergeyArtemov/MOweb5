#pragma checksum "C:\ProjectsCLR\MOweb5\Views\Order\Partial\OutletList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6079014eddfd39f19a0498c666493248212d0f38"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_Partial_OutletList), @"mvc.1.0.view", @"/Views/Order/Partial/OutletList.cshtml")]
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
#line 1 "C:\ProjectsCLR\MOweb5\Views\_ViewImports.cshtml"
using MOweb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\ProjectsCLR\MOweb5\Views\_ViewImports.cshtml"
using MOweb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6079014eddfd39f19a0498c666493248212d0f38", @"/Views/Order/Partial/OutletList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2d30cf7c1d6b26fe04ddad4dd9ba2f82def1db6", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_Partial_OutletList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<nav id=\"outletListMenu\">\r\n    <ul class=\"event-menu__items\">\r\n");
#nullable restore
#line 3 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\OutletList.cshtml"
         if (ViewData["OutletList"] != null)
        {
            System.Data.DataSet dataSet = (System.Data.DataSet)ViewData["OutletList"];
            for (int rw = 0; rw < dataSet.Tables["OutletList"].Rows.Count; rw++)
            {
                var funcstr = "getNameAndPriceOfAgent(" + dataSet.Tables["OutletList"].Rows[rw].ItemArray[14] + ")";

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"event-menu__item\"");
            BeginWriteAttribute("onclick", " onclick=\"", 467, "\"", 485, 1);
#nullable restore
#line 9 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\OutletList.cshtml"
WriteAttributeValue("", 477, funcstr, 477, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <label hidden>");
#nullable restore
#line 10 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\OutletList.cshtml"
                             Write(dataSet.Tables["OutletList"].Rows[rw].ItemArray[14]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                    ");
#nullable restore
#line 11 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\OutletList.cshtml"
               Write(dataSet.Tables["OutletList"].Rows[rw].ItemArray[12]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </li>\r\n");
#nullable restore
#line 13 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\OutletList.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n</nav>\r\n");
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
