#pragma checksum "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3af38ac4797ffa518d4fb14eff00a4e6905b8975"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_CustomerOrdersList), @"mvc.1.0.view", @"/Views/Order/CustomerOrdersList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3af38ac4797ffa518d4fb14eff00a4e6905b8975", @"/Views/Order/CustomerOrdersList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2d30cf7c1d6b26fe04ddad4dd9ba2f82def1db6", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_CustomerOrdersList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Orders.CustomerOrders>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<table border=""1"" id=""customerOrdersTable"" class=""table table-condensed table-select"" style=""font-size: 11px;"">
    <tr>
        <th></th>
        <th>Номер</th>
        <th>Тип заказа</th>
        <th>Дата заказа</th>
        <th>Дата смены статуса</th>
        <th>Статус</th>
        <th>Сумма заказа</th>
        <th>Доставка</th>
        <th>Акция</th>
        <th>Адрес</th>
    </tr>
");
#nullable restore
#line 19 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
     if (Model.Orders.Count > 0)
    {
        var itmLineNo = 1;
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
         foreach (var item in Model.Orders)
        {
            

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr onclick=\"setSingleRowSelect(this)\"");
            BeginWriteAttribute("ondblclick", "                \r\n                ondblclick=\"", 815, "\"", 1046, 15);
            WriteAttributeValue("", 861, "location.href", 861, 13, true);
            WriteAttributeValue(" ", 874, "=", 875, 2, true);
            WriteAttributeValue(" ", 876, "\'", 877, 2, true);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
WriteAttributeValue("", 878, Url.Action("ShowOrders", "Order"), 878, 34, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 912, "?CustomerId=", 912, 12, true);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
WriteAttributeValue("", 924, Model.Customer.ID, 924, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 942, "&Host=", 942, 6, true);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
WriteAttributeValue("", 948, Model.Customer.Host, 948, 20, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 968, "&OrderNo=", 968, 9, true);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
WriteAttributeValue("", 977, item.OrderNo, 977, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 990, "&CurrentUser=", 990, 13, true);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
WriteAttributeValue("", 1003, Model.CurrentUser, 1003, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1021, "&NextForm=", 1021, 10, true);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
WriteAttributeValue("", 1031, item.NextForm, 1031, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1045, "\'", 1045, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <td hidden name=\"orderNextForm\">");
#nullable restore
#line 27 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
                                           Write(item.NextForm);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td hidden name=\"orderCanChange\">");
#nullable restore
#line 28 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
                                            Write(item.CanChange);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td name=\"itemLineNo\">");
#nullable restore
#line 29 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
                                 Write(itmLineNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td name=\"customerOrdersOrderNo\">");
#nullable restore
#line 30 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
                                            Write(item.OrderNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 31 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.OrderType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 32 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 33 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.DateStateChange);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 34 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.State);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 35 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.OrderAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 35 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
                                 Write(Model.Currency);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 36 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.ShippingAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 36 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
                                    Write(Model.Currency);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 37 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.Campaign);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 38 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
               Write(item.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 40 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
            itmLineNo++;
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\ProjectsCLR\MOweb5\Views\Order\CustomerOrdersList.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Orders.CustomerOrders> Html { get; private set; }
    }
}
#pragma warning restore 1591
