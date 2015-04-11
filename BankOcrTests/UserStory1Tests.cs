using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory1Tests
    {
        [TestMethod]
        public void ToArabic_Zero_Returns0()
        {
            var digit = " _ "
                      + "| |"
                      + "|_|";
            Assert.AreEqual(0, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_One_Returns1()
        {
            var digit = "   "
                      + "  |"
                      + "  |";
            Assert.AreEqual(1, DigitConverter.ToArabic(digit));
        }
    }

    public static class DigitConverter
    {
        private static Dictionary<string, int> digitArabicMap = new Dictionary<string, int>
        {
            {  " _ "
             + "| |"
             + "|_|", 0},
            {  "   "
             + "  |"
             + "  |", 1}

        };

        public static int ToArabic(string digit)
        {
            return digitArabicMap[digit];
        }

        public static int ToArabic(string[] digit)
        {
            return ToArabic(string.Join("", digit));
        }
    }
}
