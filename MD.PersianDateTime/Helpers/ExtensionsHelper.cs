using System.Text;

namespace MD.PersianDateTime
{
    internal static class ExtensionsHelper
    {
        /// <summary>
        /// Convert all persian and arabic digit to english in any string  
        /// <!-- http://stackoverflow.com/a/28905353/579381 --> 
        /// </summary>
        /// <param name="inputString">input string that maybe contain persian or arabic digit</param>
        /// <returns>a string with english digit</returns>
        internal static string ConvertDigitsToLatin(string inputString)
        {
            var sb = new StringBuilder();

            foreach (var c in inputString)
            {
                switch (c)
                {
                    case '\u06f0': //Persian digit
                    case '\u0660': //Arabic  digit
                        sb.Append('0');
                        break;
                    case '\u06f1':
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                    case '\u0669':
                        sb.Append('9');
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }
    }
}