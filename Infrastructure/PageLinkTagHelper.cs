using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MOweb.Models;

namespace MOweb.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public int PageForShow { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string s1 = "Паспорт РФ";
            string s2 = "Нерезидент";
            string s3 = "Загранпаспорт";
            string s4 = "Отказ";
            string s5 = "Обязательные поля";


            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            TagBuilder tag = new TagBuilder("a");
            //tag.Attributes["href"] = urlHelper.Action(PageAction, new { Page = 1});//PageAction,// new { Page = 1, FirstName = 1 }); 
            //tag.Attributes["id"] = "id1";
            tag.Attributes["onmousedown"] = "func1(1)"; //urlHelper.Action(PageAction);
            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                tag.AddCssClass(ViewContext.ViewBag.PageForShowVB == 1 ? PageClassSelected : PageClassNormal);
            }
            tag.InnerHtml.Append(s1 + "    ");
            result.InnerHtml.AppendHtml(tag);

            tag = new TagBuilder("a");
            //tag.Attributes["href"] = urlHelper.Action(PageAction, new { Page = 2 });
            tag.Attributes["onmousedown"] = "func1(2)";
            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                tag.AddCssClass(ViewContext.ViewBag.PageForShowVB == 2 ? PageClassSelected : PageClassNormal);
            }
            tag.InnerHtml.Append(s2 + "    ");
            result.InnerHtml.AppendHtml(tag);

            tag = new TagBuilder("a");
            //tag.Attributes["href"] = urlHelper.Action(PageAction, new { Page = 3 });
            tag.Attributes["onmousedown"] = "func1(3)";
            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                tag.AddCssClass(ViewContext.ViewBag.PageForShowVB == 3 ? PageClassSelected : PageClassNormal);
            }
            tag.InnerHtml.Append(s3 + "    ");
            result.InnerHtml.AppendHtml(tag);


            tag = new TagBuilder("a");
            //tag.Attributes["href"] = urlHelper.Action(PageAction, new { Page = 4 });
            tag.Attributes["onmousedown"] = "func1(4)";
            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                tag.AddCssClass(ViewContext.ViewBag.PageForShowVB == 4 ? PageClassSelected : PageClassNormal);
            }

            tag.InnerHtml.Append(s4 + "    ");
            result.InnerHtml.AppendHtml(tag);


            tag = new TagBuilder("a");
            //tag.Attributes["href"] = urlHelper.Action(PageAction, new { Page = 5 });
            tag.Attributes["onmousedown"] = "func1(5)";
            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                tag.AddCssClass(ViewContext.ViewBag.PageForShowVB == 5 ? PageClassSelected : PageClassNormal);
            }

            tag.InnerHtml.Append(s5 + "    ");
            result.InnerHtml.AppendHtml(tag);

            output.Content.AppendHtml(result.InnerHtml);
        }
    }

}
