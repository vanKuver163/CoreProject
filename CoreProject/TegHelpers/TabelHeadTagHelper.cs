using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreProject.TegHelpers
{
    [HtmlTargetElement("tabelhead")]
    public class TabelHeadTagHelper : TagHelper
    {
        public string BgColor { get; set; } = "dark";     

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-center text-white");

            string content = (await output.GetChildContentAsync()).GetContent();

            //output.Content.SetHtmlContent($"<tr><th colspan=3>{content}</th></tr>");

            TagBuilder header = new TagBuilder("th");
            header.Attributes["colspan"] = "3";
            header.InnerHtml.Append(content);
            TagBuilder row = new TagBuilder("tr");
            row.InnerHtml.AppendHtml(header);

            output.Content.SetHtmlContent(row);
        }
    }
}
