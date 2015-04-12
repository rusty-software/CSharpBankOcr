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

        private List<string> SwapAndConvert(string preIll, string postIll, StringBuilder digit, int indexToSwapAt, List<char> charsToSwap)
        {
            var accountNums = new List<string>();
            foreach(var otherChar in charsToSwap)
            {
                digit[indexToSwapAt] = otherChar;
                var arabic = DigitConverter.ToArabic(digit.ToString());
                if (arabic != "?")
                {
                    accountNums.Add(preIll + arabic + postIll);
                }
            }
            return accountNums;
        }

        public List<string> OtherAccountNumbersFor(string accountNumber, string illegibleDigit)
        {
            var charsToSwap = new Dictionary<string, List<char>>
                {
                    {" ", new List<char> {'|', '_'}},
                    {"_", new List<char> {'|'}},
                    {"|", new List<char> {'_'}}
                };

            var accountNumParts = accountNumber.Split('?');
            var otherAccountNumbers = new List<string>();
            for (var i = 0; i < illegibleDigit.Length; i++)
            {
                var sb = new StringBuilder(illegibleDigit);
                var s = illegibleDigit.Substring(i, 1);
                otherAccountNumbers.AddRange(SwapAndConvert(accountNumParts[0], accountNumParts[1], sb, i, charsToSwap[s]));
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
