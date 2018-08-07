using System.Text.RegularExpressions;

namespace WebTemplateEngine
{
    public class SuperDuperRenderer
    {
        public string Process(string template)
        {
            Regex.Replace(template, @"\@([^\)]+)\@", ProcessBlock);

            return template;
        }

        private string ProcessBlock(Match match)
        {
            return match.Value;
        }
    }
}