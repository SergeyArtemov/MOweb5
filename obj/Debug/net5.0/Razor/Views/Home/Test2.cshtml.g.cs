#pragma checksum "C:\ProjectsCLR\MOweb5\Views\Home\Test2.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54b319cc3cf211ce855b6f4ba2f2590f15a27f85"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Test2), @"mvc.1.0.view", @"/Views/Home/Test2.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54b319cc3cf211ce855b6f4ba2f2590f15a27f85", @"/Views/Home/Test2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2d30cf7c1d6b26fe04ddad4dd9ba2f82def1db6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Test2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("charset", new global::Microsoft.AspNetCore.Html.HtmlString("utf-8"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/yandexmap.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("yandex_initialize()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!--");
#nullable restore
#line 1 "C:\ProjectsCLR\MOweb5\Views\Home\Test2.cshtml"
      
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<!DOCTYPE html>
<html>
<head>
    <environment include=""Development"">
        <script src=""~/lib/jquery/dist/jquery.js""></script>
        <script src=""~/lib/jquery/dist/jquery-ui.js""></script>
        <script src=""~/lib/jquery.unobtrusive-ajax/jquery.unobtrusive-ajax.js""></script>
    </environment>
    <style type=""text/css"">
        div.dragElement {
            font-size: large;
            border: thin solid black;
            padding: 16px;
            width: 8em;
            text-align: center;
            background-color: lightgray;
            margin: 4px
        }

        #container {
            border: medium double black;
            width: 100vw;
            height: 100vh;
        }
    </style>
    <script type=""text/javascript"">
        $(function () {

            $('.dragElement').draggable({
                containment: ""parent""
            }).filter('#dragH').draggable(""option"", ""axis"", ""x"");

        });
    </script>
</head>
<body>
    <div id=""co");
            WriteLiteral(@"ntainer"">
        <div id=""dragH"" class=""dragElement ui-widget ui-corner-all ui-state-error"">
            Перетащить по горизонтали
        </div>
        <div class=""dragElement ui-widget ui-corner-all ui-state-error"">
            Перетащить внутри родителя
        </div>
    </div>
</body>
</html>
-->
<!--
<style type=""text/css"">
    nav {
        background: lightpink;
        user-select: none;
    }

        nav ul {
            margin: 0;
            padding: 0;
            list-style: none;
        }

            nav ul:after {
                content: """";
                display: none;
                clear: both;
            }

        nav a {
            text-decoration: none;
            display: block;
        }

    .topmenu > li {
        float: left;
        position: relative;
        border: 1px solid black;
    }

        .topmenu > li > a {
            padding: 5px 10px;
            font-size: 12px;
            text-transform: uppercase;
        ");
            WriteLiteral(@"    color: black;
            letter-spacing: 4px;
        }

            .topmenu > li > a.active,
            .submenu a:hover {
                color: red;
            }

    .topmenu .fa,
    .submenu {
        position: absolute;
      left: 50%;
        z-index: 5;
        min-width: 200px;
        visibility: hidden;
        opacity: 0;
        transform-origin: 0% 0%;
        transform: rotateX(-90deg);
        transition: .1s linear;
        border-top: 1px solid #CBCBCC;
        border-left: 1px solid #CBCBCC;
        border-right: 1px solid #CBCBCC;
    }

        .submenu li {
            position: relative;
        }

            .submenu li a {
                color: #282828;
                padding: 5px 10px;
                font-size: 13px;
                border-bottom: 1px solid #CBCBCC;
            }

        .submenu .submenu {
            position: absolute;
            left: calc(100% + 1px);
            top: -1px;
            transition: .1s linea");
            WriteLiteral(@"r;
        }

    nav li:active > .submenu {
        transform: rotateX(0deg);
        visibility: visible;
        opacity: 1;
    }

  .submenu:hover {
        transform: rotateX(0deg);
        visibility: visible;
        opacity: 1;
    }

  .submenu li:hover > .submenu {
        transform: rotateX(0deg);
        visibility: visible;
        opacity: 1;
    }
</style>
<input /><br />
<input /><br />
<input />
<div>
    <nav>
        <ul class=""topmenu"">
            <li>
                <a>действия</a>
                <ul class=""submenu"">
                    <li>
                        <a>меню второго уровня</a>
                    </li>
                    <li>
                        <a>меню второго уровня</a>
                        <ul class=""submenu"">
                            <li><a>меню третьего уровня</a></li>
                            <li><a>меню третьего уровня</a></li>
                            <li><a>меню третьего уровня</a></li>
                   ");
            WriteLiteral(@"     </ul>
                    </li>
                    <li><a>меню второго уровня</a></li>
                </ul>
            </li>
        </ul>
    </nav>
</div>
-->
<!--
<style type=""text/css"">
    #nav, #nav ul {
        list-style: none;
        margin: 0;
        padding: 0;
    }

    #nav {
        padding-left: 0;
        padding-top: 0;
        position: relative;
        z-index: 0;
    }

        #nav ul {
            left: -9999px;
            position: absolute;
            top: 30px;
            width: auto;
        }

        #nav li {
            float: left;
            margin-right: 5px;
            position: relative;
        }

            #nav li a {
                background: #eafaff;
                color: #444;
                display: block;
                float: left;
                font-size: 13px;
                box-shadow: 0 0 1px 1px lightblue;
                padding: 4px 10px;
                text-decoration: none;
           ");
            WriteLiteral(@"     -moz-border-radius: 6px;
                -webkit-border-radius: 6px;
                -o-border-radius: 6px;
                border-radius: 6px;
                border: 1px solid blue;
            }

        #nav > li > a {
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            -o-border-radius: 6px;
            border-radius: 6px;
            overflow: hidden;
        }

        #nav li a.fly {
            padding-right: 10px;
            border: 1px solid blue;
        }

        #nav ul li {
            margin: 1px;
        }

            #nav ul li a {
                width: 100px;
            }

        #nav li:hover > a {
            background-color: #caf2ff;
            color: #000;
        }
        /*#nav li a:focus { outline-width:100px; background-color:red;}*/
        #nav li a:active + ul.dd, #nav li a:focus + ul.dd, #nav li ul.dd:hover {
            left: 30px;
        }

        #nav ul.dd li a:active + ul, #nav ul.dd l");
            WriteLiteral(@"i a:hover + ul, #nav ul.dd li ul:hover {
            top: -1px;
            left: 125px;
        }
</style>

<label>2222</label>
<div>
    <ul id=""nav"">
        <li>
            <a href=""#""><b>Действия</b></a>
            <ul class=""dd"">
                <li><a href=""#"">уровень1</a></li>
                <li>
                    <a href=""#"">уровень1</a>
                    <ul>
                        <li><a href=""#"">уровень2</a></li>
                        <li><a href=""#"">уровень2</a></li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>
</div>
<br /><br />
<label>3333</label>

<script>

</script>
-->
");
            WriteLiteral("\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "54b319cc3cf211ce855b6f4ba2f2590f15a27f8511625", async() => {
                WriteLiteral("\r\n    <title>Добавление метки с собственным изображением</title>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <script src=\"https://api-maps.yandex.ru/2.1.78/?lang=ru_RU\" type=\"text/javascript\"></script>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "54b319cc3cf211ce855b6f4ba2f2590f15a27f8512139", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <style>\r\n\r\n        html, body, #map {\r\n            width: 100%;\r\n            height: 95%;\r\n            padding: 0;\r\n            margin: 0;\r\n        }\r\n    </style>\r\n    <script>\r\n    </script>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "54b319cc3cf211ce855b6f4ba2f2590f15a27f8514250", async() => {
                WriteLiteral(@"
    <input type=""button"" onclick=""btn_click()"" />
    <input type=""button"" onclick=""yandex2_CreateNewArea(56.04528829466855, 37.76602721773088, 5, 5000, '000000', 'FFFFFF', 'Новая область 02.03.2021 11:32:09', '{8D61478A-4CAF-44F6-8B84-C84244862342}')"" />
    <input id=""AreaXML""");
                BeginWriteAttribute("value", " value=\"", 9114, "\"", 9122, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n    <input id=\"ClickCoord\"");
                BeginWriteAttribute("value", " value=\"", 9152, "\"", 9160, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n    <input id=\"forDebug\"");
                BeginWriteAttribute("value", " value=\"", 9188, "\"", 9196, 0);
                EndWriteAttribute();
                WriteLiteral(" style=\"width: 98%;\" />\r\n    <div id=\"map\" style=\"width:100%; height:92%\"></div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n\r\n");
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
