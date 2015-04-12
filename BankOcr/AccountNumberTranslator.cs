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
            var dan = new DigitizedAccountNumber(lines);
            var validator = new AccountNumberValidator();

            var accountNumber = dan.ToArabicAccountNumber();
            if (accountNumber.Contains("?"))
            {
                return new TranslationResult { AccountNumber = accountNumber, ErrCode = "ILL" };
            }

            var isValid = validator.IsValid(accountNumber);
            return new TranslationResult { AccountNumber = accountNumber, ErrCode = (isValid ? null : "ERR") };
        }
    }
}
