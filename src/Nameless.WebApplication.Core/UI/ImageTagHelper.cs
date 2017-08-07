using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nameless.WebApplication.Core.UI {
    [HtmlTargetElement("img")]
    public sealed class ImageTagHelper : TagHelper {

        #region Public Constants

        public const string PATH_ATTRIBUTE_NAME = "asp-img-path";
        public const string BLOB_ATTRIBUTE_NAME = "asp-img-blob";
        public const string FALLBACK_ATTRIBUTE_NAME = "asp-img-fallback";
        public const string TYPE_ATTRIBUTE_NAME = "asp-img-type";

        #endregion Public Constants

        #region Public Properties

        [HtmlAttributeName(PATH_ATTRIBUTE_NAME)]
        public string ImgPath { get; set; }

        [HtmlAttributeName(BLOB_ATTRIBUTE_NAME)]
        public byte[] ImgBlob { get; set; }

        [HtmlAttributeName(FALLBACK_ATTRIBUTE_NAME)]
        public string ImgFallback { get; set; }

        [HtmlAttributeName(TYPE_ATTRIBUTE_NAME)]
        public string ImgType { get; set; } = "png";

        #endregion Public Properties

        #region Public Override Methods

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var src = string.Empty;

            if (!string.IsNullOrWhiteSpace(ImgPath)) {
                src = ImgPath;
            } else if (ImgBlob != null) {
                src = $"data:image/{ImgType};base64,{Convert.ToBase64String(ImgBlob)}";
            } else {
                src = ImgFallback;
            }

            output.Attributes.SetAttribute("src", src);

            base.Process(context, output);
        }

        #endregion Public Override Methods
    }
}