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

        public List<string> OtherAccountNumbersFor(string accountNumber, string digit, int digitIndex)
        {
            var charsToSwap = new Dictionary<string, List<char>>
                {
                    {" ", new List<char> {'|', '_'}},
                    {"_", new List<char> {'|'}},
                    {"|", new List<char> {'_'}}
                };

            var preDigit = "";
            var postDigit = "";
            if (digitIndex == 0)
            {
                postDigit = accountNumber.Substring(1);
            } else if (digitIndex == accountNumber.Length)
            {
                preDigit = accountNumber.Substring(0, digitIndex - 1);
            }
            else
            {
                preDigit = accountNumber.Substring(0, digitIndex);
                postDigit = accountNumber.Substring(digitIndex + 1);
            }
            var otherAccountNumbers = new List<string>();
            for (var i = 0; i < digit.Length; i++)
            {
                var sb = new StringBuilder(digit);
                var s = digit.Substring(i, 1);
                otherAccountNumbers.AddRange(SwapAndConvert(preDigit, postDigit, sb, i, charsToSwap[s]));
            }
            return otherAccountNumbers;
        }

        public string AmbiguousDescriptionFor(List<string> accountNums)
        {
            return "";
        }

        // HACK: the nullable int is terrible.  I'm ashamed.
        private TranslationResult FiddleWithDigits(string sourceAccountNum, List<string> digitsToTry, int? digitIndexInSourceAccountNum)
        {
            var result = new TranslationResult();
            var validator = new AccountNumberValidator();

            var numsWithValidChecksums = new List<string>();
            for (var i = 0; i < digitsToTry.Count; i++)
            {
                var others = OtherAccountNumbersFor(sourceAccountNum, digitsToTry[i], digitIndexInSourceAccountNum ?? i);
                foreach (var accountNum in others)
                {
                    if (validator.IsValid(accountNum))
                    {
                        numsWithValidChecksums.Add(accountNum);
                    }
                }
            }
            if (numsWithValidChecksums.Count == 1)
            {
                result.AccountNumber = numsWithValidChecksums.First();
            }
            else if (numsWithValidChecksums.Count > 1)
            {
                result.ErrCode = AmbiguousDescriptionFor(numsWithValidChecksums);
            }
            else
            {
                result.ErrCode = "ILL";
            }
            return result;
        }

        private TranslationResult FiddleWithDigits(string sourceAccountNum, List<string> digitsToTry)
        {
            return FiddleWithDigits(sourceAccountNum, digitsToTry, null);
        }

        public TranslationResult Translate()
        {
            var translated = ToArabicAccountNumber();
            var validator = new AccountNumberValidator();
            var result = new TranslationResult { AccountNumber = translated };
            var illCount = translated.Count(c => c == '?');
            if (illCount > 1)
            {
                result.ErrCode = "ILL";
            }
            else if (illCount == 1)
            {
                var digitIndex = translated.IndexOf('?');
                result = FiddleWithDigits(translated, new List<string> { Digits[digitIndex] }, digitIndex);
            }
            else 
            {
                if (!validator.IsValid(translated))
                {
                    result = FiddleWithDigits(translated, Digits);
                }
            }

            return result;
        }
    }
}
