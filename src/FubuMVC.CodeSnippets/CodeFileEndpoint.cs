﻿using FubuMVC.Core.Resources.PathBased;
using FubuMVC.Core.UI;
using HtmlTags;

namespace FubuMVC.CodeSnippets
{
    public class CodeFileEndpoint
    {
        private readonly FubuHtmlDocument<CodeFileResponse> _document;

        public CodeFileEndpoint(FubuHtmlDocument<CodeFileResponse> document)
        {
            _document = document;
        }

        public HtmlDocument get_code_file(CodeFileRequest request)
        {
            _document.Asset("prettify.js", "bootstrap-prettify.js", "prettify.css");
            _document.WriteAssetsToHead();

            _document.Title = request.Path;

            _document.Add("h1").Text(request.Path);

            var codeTag = _document.CodeFile(request.Path).Id("snippet");

            _document.Add(codeTag);


            return _document;
        }
    }

    public class CodeFileResponse
    {
    }

    public class CodeFileRequest : ResourcePath
    {
        public CodeFileRequest(string path) : base(path)
        {
        }
    }
}