#pragma checksum "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c378b24912516c1c9f11dfd9ef77d57cd811989c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_Partial_ShippingIntervals), @"mvc.1.0.view", @"/Views/Order/Partial/ShippingIntervals.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c378b24912516c1c9f11dfd9ef77d57cd811989c", @"/Views/Order/Partial/ShippingIntervals.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2d30cf7c1d6b26fe04ddad4dd9ba2f82def1db6", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_Partial_ShippingIntervals : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<htm>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c378b24912516c1c9f11dfd9ef77d57cd811989c3213", async() => {
                WriteLiteral("\r\n    <style>\r\n        .table-centered-header th{\r\n            text-align: center;\r\n            background-color: #c2e3ff;\r\n            border: 1px solid blue;\r\n        }\r\n    </style>\r\n");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c378b24912516c1c9f11dfd9ef77d57cd811989c4377", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 12 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
     if (ViewData["ShippingIntervals"] != null)
    {
        System.Data.DataSet shippingIntervals = (System.Data.DataSet)ViewData["ShippingIntervals"];
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
         if (shippingIntervals.Tables[0].Rows.Count > 0)
        {
            //string str = Newtonsoft.Json.JsonConvert.SerializeObject(shippingIntervals);
            //System.Data.DataSet deserial = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(str);
            DateTime startDate = DateTime.Now.Date.AddDays(7 * ViewBag.Week);

#line default
#line hidden
#nullable disable
                WriteLiteral("            <table class=\"table-centered-header table-select clickable\" style=\"font-size: 16px; border-collapse: collapse;\" id=\"shippingIntervalsTable1\" onmousemove=\"showCell(this)\">\r\n                <tr>\r\n                    <th>Зона / число</th>\r\n");
                WriteLiteral("                    <th><input hidden id=\"d1\"");
                BeginWriteAttribute("value", " value=\"", 1074, "\"", 1123, 1);
#nullable restore
#line 24 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 1082, startDate.ToString("yyyy-MM-dd 0:00:00"), 1082, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 24 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                             Write(startDate.ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 24 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                               Write(startDate.ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th><input hidden id=\"d2\"");
                BeginWriteAttribute("value", " value=\"", 1239, "\"", 1299, 1);
#nullable restore
#line 25 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 1247, startDate.AddDays(1).ToString("yyyy-MM-dd 0:00:00"), 1247, 52, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 25 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                        Write(startDate.AddDays(1).ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 25 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                     Write(startDate.AddDays(1).ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th><input hidden id=\"d3\"");
                BeginWriteAttribute("value", " value=\"", 1437, "\"", 1497, 1);
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 1445, startDate.AddDays(2).ToString("yyyy-MM-dd 0:00:00"), 1445, 52, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                        Write(startDate.AddDays(2).ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 26 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                     Write(startDate.AddDays(2).ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th><input hidden id=\"d4\"");
                BeginWriteAttribute("value", " value=\"", 1635, "\"", 1695, 1);
#nullable restore
#line 27 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 1643, startDate.AddDays(3).ToString("yyyy-MM-dd 0:00:00"), 1643, 52, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 27 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                        Write(startDate.AddDays(3).ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 27 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                     Write(startDate.AddDays(3).ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th><input hidden id=\"d5\"");
                BeginWriteAttribute("value", " value=\"", 1833, "\"", 1893, 1);
#nullable restore
#line 28 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 1841, startDate.AddDays(4).ToString("yyyy-MM-dd 0:00:00"), 1841, 52, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 28 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                        Write(startDate.AddDays(4).ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 28 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                     Write(startDate.AddDays(4).ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th><input hidden id=\"d6\"");
                BeginWriteAttribute("value", " value=\"", 2031, "\"", 2091, 1);
#nullable restore
#line 29 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 2039, startDate.AddDays(5).ToString("yyyy-MM-dd 0:00:00"), 2039, 52, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 29 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                        Write(startDate.AddDays(5).ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 29 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                     Write(startDate.AddDays(5).ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th><input hidden id=\"d7\"");
                BeginWriteAttribute("value", " value=\"", 2229, "\"", 2289, 1);
#nullable restore
#line 30 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 2237, startDate.AddDays(6).ToString("yyyy-MM-dd 0:00:00"), 2237, 52, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />");
#nullable restore
#line 30 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                        Write(startDate.AddDays(6).ToString("dd.MM"));

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />");
#nullable restore
#line 30 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                     Write(startDate.AddDays(6).ToString("ddd"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n");
                WriteLiteral("                </tr>\r\n");
#nullable restore
#line 40 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                 for (int shpIntrv = 0; shpIntrv < shippingIntervals.Tables[0].Rows.Count; shpIntrv++)
                {
                    string title = "";

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n");
#nullable restore
#line 44 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                           title = "Зона " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[15] + " ID(Уровень): " +
                                             @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[14] + "(" + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[16] + ")";
                            string[] timeFromTo = @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[0].ToString().Split(" - ");
                            string timeFrom = "0";
                            string timeTo = "0";
                            if (timeFromTo.Length > 1)
                            {
                                timeFrom = @timeFromTo[0].Split(":").Length > 1 ? @timeFromTo[0].Split(":")[0] : "0";
                                timeTo = @timeFromTo[1].Split(":").Length > 1 ? @timeFromTo[1].Split(":")[0] : "0";
                            }
                        

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <th");
                BeginWriteAttribute("title", " title=\"", 3859, "\"", 3873, 1);
#nullable restore
#line 55 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 3867, title, 3867, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n");
#nullable restore
#line 56 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                               
                                string idTimeFrom = "timeFrom" + (shpIntrv + 1);
                                string idTimeTo = "timeTo" + (shpIntrv + 1);
                            

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <input hidden");
                BeginWriteAttribute("value", " value=\"", 4142, "\"", 4159, 1);
#nullable restore
#line 60 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 4150, timeFrom, 4150, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("id", " id=\"", 4160, "\"", 4176, 1);
#nullable restore
#line 60 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 4165, idTimeFrom, 4165, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            <input hidden");
                BeginWriteAttribute("value", " value=\"", 4223, "\"", 4238, 1);
#nullable restore
#line 61 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 4231, timeTo, 4231, 7, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("id", " id=\"", 4239, "\"", 4253, 1);
#nullable restore
#line 61 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 4244, idTimeTo, 4244, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            ");
#nullable restore
#line 62 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                       Write(shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[0]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </th>\r\n");
#nullable restore
#line 65 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[2])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 4717, "\"", 4731, 1);
#nullable restore
#line 69 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 4725, title, 4725, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 69 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[18];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 5084, "\"", 5098, 1);
#nullable restore
#line 73 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 5092, title, 5092, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 73 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        ; break;
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
#nullable restore
#line 76 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[3])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 5486, "\"", 5500, 1);
#nullable restore
#line 80 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 5494, title, 5494, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 80 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[19];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 5853, "\"", 5867, 1);
#nullable restore
#line 84 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 5861, title, 5861, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 84 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        break;
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
#nullable restore
#line 87 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[4])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 6253, "\"", 6267, 1);
#nullable restore
#line 91 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 6261, title, 6261, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 91 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[20];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 6620, "\"", 6634, 1);
#nullable restore
#line 95 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 6628, title, 6628, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 95 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        break;
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[5])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 7018, "\"", 7032, 1);
#nullable restore
#line 101 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 7026, title, 7026, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 101 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[21];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 7385, "\"", 7399, 1);
#nullable restore
#line 105 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 7393, title, 7393, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 105 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        break;
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 107 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[6])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 7783, "\"", 7797, 1);
#nullable restore
#line 111 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 7791, title, 7791, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 111 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[22];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 8150, "\"", 8164, 1);
#nullable restore
#line 115 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 8158, title, 8158, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 115 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        break;
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 117 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[7])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 8548, "\"", 8562, 1);
#nullable restore
#line 121 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 8556, title, 8556, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 121 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[23];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 8915, "\"", 8929, 1);
#nullable restore
#line 125 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 8923, title, 8923, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 125 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        break;
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 127 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                         switch (@shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[8])
                        {
                            case 0:
                                title = "Недоступно";

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 9313, "\"", 9327, 1);
#nullable restore
#line 131 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 9321, title, 9321, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: lightgray; border: 1px solid gray; text-align: center;\"></td>");
#nullable restore
#line 131 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                         ; break;
                            case 1:
                                title = "Доступно";
                                //title = "Доступно, уже клиентов " + @shippingIntervals.Tables[0].Rows[shpIntrv].ItemArray[24];

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <td");
                BeginWriteAttribute("title", " title=\"", 9680, "\"", 9694, 1);
#nullable restore
#line 135 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
WriteAttributeValue("", 9688, title, 9688, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" style=\"background-color: palegreen; border: 1px solid green; text-align: center;\"><b>&times;</b></td>");
#nullable restore
#line 135 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                                                                                                                                                        break;
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    </tr>\r\n");
#nullable restore
#line 146 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"            </table><br />
            <div id=""hiddenElementsShippingIntervals"" hidden style=""text-align:center;"">
                <label id=""lbl_TimeFrom"">От: 
                    <input id=""rangeTimeFrom"" type=""range"" style=""width: calc(100% - 6px); max-width:440px;"" step=""1"" onchange=""changeIntervalManual()""/>
                </label><br />
                <label id=""lbl_TimeTo"">До: 
                    <input  id=""rangeTimeTo"" type=""range"" style=""width: calc(100% - 6px); max-width:440px;"" step=""1"" onchange=""changeIntervalManual()""/>
                </label><br />
                <input hidden id=""selecteDeliveryDate""/><br />
                <button type=""button"" ");
                WriteLiteral(" class=\"orderButton\" onclick=\"setShippingIntervals()\">Назначить интервал</button>\r\n            </div>\r\n");
#nullable restore
#line 158 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div style=\"font-size: 24px; text-align: center;\"><label>Нет доступных интервалов</label></div>\r\n");
#nullable restore
#line 162 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 162 "C:\ProjectsCLR\MOweb5\Views\Order\Partial\ShippingIntervals.cshtml"
         

    }

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</htm>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
