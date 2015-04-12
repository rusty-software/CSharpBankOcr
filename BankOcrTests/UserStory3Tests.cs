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
            lines[0] = " _  _     _  _        _  _ ";
            lines[1] = "|_ |_ |_| _|  |  ||_||_||_ ";
            lines[2] = "|_||_|  | _|  |  |  | _| _|";
            lines[3] = "                           ";
            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "664371495", ErrCode = "ERR" };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Validator_BadDigit_ReturnsILL()
        {
            var lines = new string[4];
            lines[0] = " _  _        _  _  _  _  _ ";
            lines[1] = "|_||_   |  || |     | _||_ ";
            lines[2] = "|_||_|  |  ||_||_| _| _||_|";
            lines[3] = "                           ";

            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "86110??36", ErrCode = "ILL" };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Validator_OK_ReturnsGoodSignal()
        {
            var lines = new string[4];
            lines[0] = "    _  _  _  _  _  _  _  _ ";
            lines[1] = "|_||_   ||_ | ||_|| || || |";
            lines[2] = "  | _|  | _||_||_||_||_||_|";
            lines[3] = "                           ";

            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "457508000", ErrCode = null };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);
        }
    }
}
