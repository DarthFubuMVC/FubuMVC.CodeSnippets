using System.IO;
using FubuMVC.Core.Assets;
using FubuMVC.Core.Runtime.Files;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuMVC.CodeSnippets
{
    public static class SnippetPageExtensions
    {


        /// <summary>
        /// Embed a code snippet into a FubuMVC view
        /// </summary>
        /// <param name="page"></param>
        /// <param name="snippet"></param>
        /// <returns></returns>
        public static HtmlTag CodeSnippet(this IFubuPage page, Snippet snippet)
        {
            var assets = page.Get<IAssetRequirements>();
        
            assets.Require("prettify.js");
            assets.Require("bootstrap-prettify.js");
            assets.Require("prettify.css");

            return new SnippetTag(snippet);
        }

        /// <summary>
        /// Embed a named code snippet into a FubuMVC view
        /// </summary>
        /// <param name="page"></param>
        /// <param name="snippetName"></param>
        /// <returns></returns>
        public static HtmlTag CodeSnippet(this IFubuPage page, string snippetName)
        {
            var snippet = page.Get<ISnippetCache>().Find(snippetName);
            return page.CodeSnippet(snippet);
        }


        /// <summary>
        /// Embed the contents of an entire code file into a FubuMVC view
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fileName">The name of the file relative to the root of the web application or Bottle</param>
        /// <param name="languageClass">Optional.  Overrides the lang-* class on the element for prettify.css formatting</param>
        /// <param name="levelIndentation">Not yet used.  Will force the display to remove extra indentation for deeply nested code</param>
        /// <returns></returns>
        public static HtmlTag CodeFile(this IFubuPage page, string fileName, string languageClass = null, bool levelIndentation = false)
        {
            var formatter = new CodeFormatter(levelIndentation);
            var file = page.Get<IFubuApplicationFiles>().Find(fileName);

            if (file == null)
            {
                return new HtmlTag("p").Text("Could not find file " + fileName);
            }

            var snippet = formatter.Format(file);

            return page.CodeSnippet(snippet);
        }

        /// <summary>
        /// Writes a link to a page that will display the entire contents of the file 
        /// containing the named snippet
        /// </summary>
        /// <param name="page"></param>
        /// <param name="snippetName"></param>
        /// <returns></returns>
        public static HtmlTag LinkToSnippet(this IFubuPage page, string snippetName)
        {
            var snippet = page.Get<ISnippetCache>().Find(snippetName);
            var fileName = snippet.File;

            return page.LinkToCodeFile(fileName);
        }

        /// <summary>
        /// Writes a link to a page that will display the entire contents of a code file
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fileName">The name of the file relative to the root of the web application or Bottle</param>
        /// <returns></returns>
        public static HtmlTag LinkToCodeFile(this IFubuPage page, string fileName)
        {
            var url = page.Urls.UrlFor(new CodeFileRequest(fileName));
            return new LinkTag(Path.GetFileName(fileName), url, "code-link").Title(fileName).Attr("target", "_blank");
        }
    }
}