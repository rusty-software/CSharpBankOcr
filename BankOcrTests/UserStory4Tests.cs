using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankOcr;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory4Tests
    {
        [TestMethod]
        public void ILL_OneOff_ReturnsAlteredCorrectNumber()
        {
            var lines = new string[4];
            lines[0] = " _     _  _  _  _  _  _    ";
            lines[1] = "| || || || || || || ||_   |";
            lines[2] = "|_||_||_||_||_||_||_| _|  |";
            lines[3] = "                           ";
            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "000000051" };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ILL_OneOff_ReturnsAlteredCorrectNumber_2()
        {
            var lines = new string[4];
            lines[0] = "    _  _  _  _  _  _     _ ";
            lines[1] = "|_||_|| ||_||_   |  |  | _ ";
            lines[2] = "  | _||_||_||_|  |  |  | _|";
            lines[3] = "                           ";
            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "490867715" };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ERR_OneOff_ReturnsAlteredCorrectNumber()
        {
            var lines = new string[4];
            lines[0] = "                           ";
            lines[1] = "  |  |  |  |  |  |  |  |  |";
            lines[2] = "  |  |  |  |  |  |  |  |  |";
            lines[3] = "                           ";

            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "711111111" };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);
        }
    }
}
