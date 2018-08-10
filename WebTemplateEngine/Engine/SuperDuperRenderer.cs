using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WebTemplateEngine
{
    public class SuperDuperRenderer
    {
        public string Process(string template)
        {
            //template = Regex.Replace(template, @"\s+", string.Empty);
            template = Regex.Replace(template, @"\@([^\)]+)\@", ProcessBlock);

            return template;
        }

        private string ProcessBlock(Match match)
        {
            return Interpret(match.Groups[1].Value);
        }

        public static string Interpret(string input)
        {
            var stringBuilder = new StringBuilder();
            var tape = new char[300000];
            var pointer = 0;
            var loopBeginIndex = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '>':
                        pointer++;
                        break;
                    case '<':
                        pointer--;
                        break;
                    case '+':
                        tape[pointer]++;
                        break;
                    case '-':
                        tape[pointer]--;
                        break;
                    case ',':
                        //tape[pointer] = (char)Console.Read();
                        break;
                    case '.':
                        stringBuilder.Append(tape[pointer]);
                        break;
                    case ']':
                        if (tape[pointer] != 0)
                        {
                            i = loopBeginIndex[0];
                        }
                        else
                        {
                            loopBeginIndex.RemoveAt(0);
                        }
                        break;
                    case '[':
                        loopBeginIndex.Insert(0, i);
                        break;
                }
            }

            return stringBuilder.ToString();
        }
    }
}