using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
    public class DigitizedAccountNumber
    {
        public List<string> Digits { get; private set; }
        public DigitizedAccountNumber(string[] lines)
        {
            Digits = new List<string>();
            for (var i = 0; i < 27; i += 3)
            {
                var sb = new StringBuilder();
                sb.Append(lines[0].Substring(i, 3)).Append(lines[1].Substring(i, 3)).Append(lines[2].Substring(i, 3));
                Digits.Add(sb.ToString());
            }
        }

        public string ToArabicAccountNumber()
        {
            var sb = new StringBuilder();
            Digits.ForEach(digit => sb.Append(DigitConverter.ToArabic(digit)));
            return sb.ToString();
        }
    }
}
