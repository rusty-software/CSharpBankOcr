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

        public List<string> OtherAccountNumbersFor(string accountNumber, string illegibleDigit)
        {
            var accountNumParts = accountNumber.Split('?');
            var otherAccountNumbers = new List<string>();
            for (var i = 0; i < illegibleDigit.Length; i++)
            {
                var sb = new StringBuilder(illegibleDigit);
                var s = illegibleDigit.Substring(i, 1);
                if (s == " ")
                {
                    sb[i] = '|';
                    var arabic = DigitConverter.ToArabic(sb.ToString());
                    if (arabic != "?")
                    {
                        otherAccountNumbers.Add(accountNumParts[0] + arabic + accountNumParts[1]);
                    }
                    sb[i] = '_';
                    arabic = DigitConverter.ToArabic(sb.ToString());
                    if (arabic != "?")
                    {
                        otherAccountNumbers.Add(accountNumParts[0] + arabic + accountNumParts[1]);
                    }
                }
                else
                {
                    if (s == "|")
                    {
                        sb[i] = '_';
                    }
                    else if (s == "_")
                    {
                        sb[i] = '|';
                    }
                    var arabic = DigitConverter.ToArabic(sb.ToString());
                    if (arabic != "?")
                    {
                        otherAccountNumbers.Add(accountNumParts[0] + arabic + accountNumParts[1]);
                    }
                }
            }
            return otherAccountNumbers;
        }

        public string AmbiguousDescriptionFor(List<string> accountNums)
        {
            return "";
        }

        public TranslationResult Translate()
        {
            var translated = ToArabicAccountNumber();
            var validator = new AccountNumberValidator();
            if (translated.Contains("?"))
            {
                var illegibleDigit = Digits[translated.IndexOf('?')];
                var others = OtherAccountNumbersFor(translated, illegibleDigit);
                var numsWithValidChecksums = new List<string>();
                foreach(var accountNum in others)
                {
                    if (validator.IsValid(accountNum))
                    {
                        numsWithValidChecksums.Add(accountNum);
                    }
                }
                if (numsWithValidChecksums.Count == 1)
                {
                    return new TranslationResult { AccountNumber = numsWithValidChecksums.First() };
                } 
                else if (numsWithValidChecksums.Count > 1)
                {
                    return new TranslationResult { AccountNumber = translated, ErrCode = AmbiguousDescriptionFor(numsWithValidChecksums) };
                }
                return new TranslationResult { AccountNumber = translated, ErrCode = "ILL" };
            }
            else 
            {
                if (validator.IsValid(translated))
                {
                    return new TranslationResult { AccountNumber = translated };
                }
            return new TranslationResult { AccountNumber = translated, ErrCode = "ERR" };
            }
        }
    }
}
