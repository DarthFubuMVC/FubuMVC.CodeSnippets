using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.CodeSnippets.Testing
{
    


    [TestFixture]
    public class HtmlStyleSnippetScannerTester
    {
        private readonly string _sparkText = @"
<use namespace='FubuMVC.CodeSnippets' />
<viewdata model='CodeSnippetHarness.HomeModel' />
<html>
	<head>
		<title>Code Snippet Examples</title>
    !{this.WriteCssTags('prettify.css')}
  </head>
	<body>
		<h1>Snippets!</h1>

		<h4>Javascript</h4>
		!{this.CodeSnippet('nextTick')}
	
	  <!--SAMPLE: UsingCodeSnippetInSpark-->
		<h4>C#</h4>
    !{this.CodeSnippet('AddLine')}
    <!--ENDSAMPLE-->

    !{this.WriteScriptTags()}
    
  </body>
</html>
".Replace("'", "\"");


        [Test]
        public void is_at_start_positive()
        {
            var scanner = new BlockCommentScanner("<!--", "-->", "spark", "lang-htm");

            scanner.DetermineName("    <!--SAMPLE: UsingCodeSnippetInSpark-->").ShouldEqual("UsingCodeSnippetInSpark");
            scanner.DetermineName("<!--SAMPLE: UsingCodeSnippetInSpark-->").ShouldEqual("UsingCodeSnippetInSpark");
            scanner.DetermineName("<!--  SAMPLE:UsingCodeSnippetInSpark  -->").ShouldEqual("UsingCodeSnippetInSpark");
            scanner.DetermineName("<!--  SAMPLE: UsingCodeSnippetInSpark  -->").ShouldEqual("UsingCodeSnippetInSpark");
        }

        [Test]
        public void is_at_start_miss()
        {
            var scanner = new BlockCommentScanner("<!--", "-->", "spark", "lang-htm");

            scanner.DetermineName("<h1>some html</h1>").ShouldBeNull();
            scanner.DetermineName("SAMPLE: UsingCodeSnippetInSpark").ShouldBeNull();
        }

        [Test]
        public void is_at_end()
        {
            var scanner = new BlockCommentScanner("<!--", "-->", "spark", "lang-htm");

            scanner.IsAtEnd("<!-- SAMPLE: something").ShouldBeFalse();
            scanner.IsAtEnd("<p>some html</p>").ShouldBeFalse();
            scanner.IsAtEnd("ENDSAMPLE").ShouldBeFalse();
            scanner.IsAtEnd("<!-- ENDSAMPLE -->").ShouldBeTrue();
        }
    }
}