using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
    public class AccountNumberTranslator
    {
        public TranslationResult Translate(string[] lines)
        {
            var digitizedAccountNumber = new DigitizedAccountNumber(lines);
            return digitizedAccountNumber.Translate();
        }
    }
}
