#pragma checksum "C:\ProjectsCLR\MOweb5\Views\Order\Partial\Menu\CustomerActions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "861f91833b2f18b874991c4816791f7b83ecb9b6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_Partial_Menu_CustomerActions), @"mvc.1.0.view", @"/Views/Order/Partial/Menu/CustomerActions.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"861f91833b2f18b874991c4816791f7b83ecb9b6", @"/Views/Order/Partial/Menu/CustomerActions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2d30cf7c1d6b26fe04ddad4dd9ba2f82def1db6", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_Partial_Menu_CustomerActions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- >> Меню действий с заказом -->
<nav id=""contextMenuOrderActions"" class=""event-menu dragElement"">
    <ul class=""menuHead""><a class=""menuClose"">x</a></ul>
    <ul class=""event-menu__items"">
        <li class=""event-menu__item"">
            Заявки
            <nav class=""subMenu"">
                <ul class=""event-menu__items"" id=""customerRequests"">
                    <!--<li class=""event-menu__item"" onclick=""alert('111_1')"">111_1</li>
                    <li class=""event-menu__item"" onclick=""alert('222_1')"">222_1</li>-->
                </ul>
            </nav>
        </li>
        <li class=""event-menu__item"" onclick=""showmodal('personalDiscount', '', '')"">Персональная скидка</li>
        <li class=""event-menu__item"">
            Другие аккаунты
            <nav class=""subMenu"">
                <ul class=""event-menu__items"">
                    <li class=""event-menu__item"" id=""anotherAccounts0"">
                        Подтверждения
                        <nav class=""subMenu"">
    ");
            WriteLiteral(@"                        <ul class=""event-menu__items"" id=""anotherAccounts0List""></ul>
                        </nav>
                    </li>
                    <li class=""event-menu__item"" id=""anotherAccounts1"">
                        Входящие
                        <nav class=""subMenu"">
                            <ul class=""event-menu__items"" id=""anotherAccounts1List""></ul>
                        </nav>
                    </li>
                    <li class=""event-menu__item"" id=""anotherAccounts2"">
                        Доставка
                        <nav class=""subMenu"">
                            <ul class=""event-menu__items"" id=""anotherAccounts2List""></ul>
                        </nav>
                    </li>
                </ul>
            </nav>
        </li>
        <li class=""event-menu__item"">
            Телефоны
            <nav class=""subMenu"">
                <ul class=""event-menu__items"" id=""customerPhones""></ul>
            </nav>
        </li>
    </ul");
            WriteLiteral(">\r\n</nav>\r\n<!-- << Меню действий с заказом -->\r\n");
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
