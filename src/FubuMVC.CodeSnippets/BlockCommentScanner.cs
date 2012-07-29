using FubuCore;

namespace FubuMVC.CodeSnippets
{
    public class BlockCommentScanner : ISnippetScanner
    {
        private readonly string _commentStart;
        private readonly string _commentEnd;
        private readonly string _extension;
        private readonly string _languageClass;

        public BlockCommentScanner(string commentStart, string commentEnd, string extension, string languageClass)
        {
            _commentStart = commentStart;
            _commentEnd = commentEnd;
            _extension = extension;
            _languageClass = languageClass;
        }

        public string LanguageClass
        {
            get { return _languageClass; }
        }

        public FileSet MatchingFileSet
        {
            get { return new FileSet{
                DeepSearch = true,
                Include = "*." + _extension
            }; }
        }

        public string DetermineName(string line)
        {
            if (!line.TrimStart().StartsWith(_commentStart)) return null;
            if (!line.Contains(Snippets.SAMPLE)) return null;

            var start = line.IndexOf(Snippets.SAMPLE) + Snippets.SAMPLE.Length;
            var end = line.IndexOf(_commentEnd);

            return line.Substring(start, end - start).Trim();
        }

        public bool IsAtEnd(string line)
        {
            var text = line.Trim();
            return text.StartsWith(_commentStart) && text.Contains(Snippets.END) && text.EndsWith(_commentEnd);
        }
    }
}