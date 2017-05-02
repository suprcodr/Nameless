using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nameless.Skeleton.Framework.Web.UI {

    [HtmlTargetElement("span", Attributes = DisplayForAttributeName)]
    public class SpanTagHelper : TagHelper {

        #region Private Constants

        private const string DisplayForAttributeName = "asp-display-for";

        #endregion Private Constants

        #region Public Properties

        [HtmlAttributeName(DisplayForAttributeName)]
        public ModelExpression For { get; set; }

        #endregion Public Properties

        #region Public Override Methods

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var text = For.ModelExplorer.GetSimpleDisplayText();

            output.Content.SetContent(text);
        }

        #endregion Public Override Methods
    }
}