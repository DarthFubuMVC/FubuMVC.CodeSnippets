using System.Linq;
using FubuCore;

namespace FubuMVC.CodeSnippets
{
    public class SimpleCommentSnippetScanner : ISnippetScanner
    {
        private readonly string _extension;
        private readonly string _start;
        private readonly string _end;

        public SimpleCommentSnippetScanner(string extension, string commentMark)
        {
            _extension = extension;
            _start = commentMark + Snippets.SAMPLE;
            _end = commentMark + Snippets.END;
        }

        public string DetermineName(string line)
        {
            if (line.TrimStart().StartsWith(_start))
            {
                return line.Split(':').Last().Trim();
            }

            return null;
        }

        public bool IsAtEnd(string line)
        {
            return line.Trim().StartsWith(_end);
        }

        public string LanguageClass
        {
            get { return "lang-" + _extension; }
        }

        public FileSet MatchingFileSet
        {
            get
            {
                return new FileSet
                {
                    DeepSearch = true,
                    Include = "*." + _extension
                };
            }
        }
    }
}