using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankOcr;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory4Tests
    {
        [TestMethod]
        public void OneOff_ReturnsAlteredCorrectNumber()
        {
            var lines = new string[4];
            lines[0] = "                           ";
            lines[1] = "  |  |  |  |  |  |  |  |  |";
            lines[2] = "  |  |  |  |  |  |  |  |  |";
            lines[3] = "                           ";

            var translator = new AccountNumberTranslator();
            var expected = new TranslationResult { AccountNumber = "711111111", ErrCode = null };

            var actual = translator.Translate(lines);

            Assert.AreEqual(expected, actual);

        }
    }
}
