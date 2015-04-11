using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory1Tests
    {
        [TestMethod]
        public void ToArabic_Zero_Returns0()
        {
            var c = new DigitConverter();
            var zeroDigit = new string[3];
            zeroDigit[0] = " _ ";
            zeroDigit[1] = "| |";
            zeroDigit[2] = "|_|";

            var zeroArabic = c.ToArabic(zeroDigit);
            Assert.AreEqual(0, zeroArabic);
        }
    }

    public class DigitConverter
    {
        private Dictionary<int, string[]> arabicDigitMap;

        public DigitConverter()
        {
            var digit = new string[3];
            digit[0] = " _ ";
            digit[1] = "| |";
            digit[2] = "|_|";

            arabicDigitMap = new Dictionary<int, string[]>();
            this.arabicDigitMap.Add(0, digit);
        }

        public int ToArabic(string[] digit)
        {
            return arabicDigitMap.FirstOrDefault(kv => kv.Value == digit).Key;
        }
    }
}
