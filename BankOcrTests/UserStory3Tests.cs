using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankOcr;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory3Tests
    {
        [TestMethod]
        public void Validator_BadCheckSum_ReturnsERR()
        {
            var lines = new string[4];
            lines[0] = " _  _  _  _  _  _  _  _  _ ";
            lines[1] = " _| _| _| _| _| _| _| _| _|";
            lines[2] = "|_ |_ |_ |_ |_ |_ |_ |_ |_ ";
            lines[3] = "                           ";
            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "222222222", ErrCode = "ERR" };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Validator_BadDigit_ReturnsILL()
        {
            throw new NotImplementedException("Implement me!");

        }

        [TestMethod]
        public void Validator_OK_ReturnsGoodSignal()
        {
            throw new NotImplementedException("Implement me!");

        }
    }

    public class AccountNumberTranslator
    {
        public TranslationResult Translate(string[] lines)
        {
            var dan = new DigitizedAccountNumber(lines);
            var validator = new AccountNumberValidator();

            var accountNumber = dan.ToArabicAccountNumber();
            var isValid = validator.IsValid(accountNumber);

            return new TranslationResult { AccountNumber = accountNumber, ErrCode = (isValid ? null : "ERR") };
        }
    }

    public struct TranslationResult
    {
        public string AccountNumber { get; set; }
        public string ErrCode { get; set; }
    }
}
