using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System.Text;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory1Tests
    {
        #region ToArabic tests
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

        [TestMethod]
        public void ToArabic_Two_Returns2()
        {
            var digit = " _ "
                      + " _|"
                      + "|_ ";
            Assert.AreEqual(2, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Three_Returns3()
        {
            var digit = " _ "
                      + " _|"
                      + " _|";
            Assert.AreEqual(3, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Four_Returns4()
        {
            var digit = "   "
                      + "|_|"
                      + "  |";
            Assert.AreEqual(4, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Five_Returns5()
        {
            var digit = " _ "
                      + "|_ "
                      + " _|";
            Assert.AreEqual(5, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Six_Returns6()
        {
            var digit = " _ "
                      + "|_ "
                      + "|_|";
            Assert.AreEqual(6, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Seven_Returns7()
        {
            var digit = " _ "
                      + "  |"
                      + "  |";
            Assert.AreEqual(7, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Eight_Returns8()
        {
            var digit = " _ "
                      + "|_|"
                      + "|_|";
            Assert.AreEqual(8, DigitConverter.ToArabic(digit));
        }

        [TestMethod]
        public void ToArabic_Nine_Returns9()
        {
            var digit = " _ "
                      + "|_|"
                      + " _|";
            Assert.AreEqual(9, DigitConverter.ToArabic(digit));
        }

        #endregion

        [TestMethod]
        public void GetDigitizedAccountNumbers_GoodFile_ReturnsDigitizedAccountNumbers()
        {
            var parser = new Parser();
            var file = "Resources\\UserStory1.txt";
            
            var digitizedAccountNumbers = parser.GetDigitizedAccountNumbers(file);

            Assert.AreEqual(11, digitizedAccountNumbers.Count);
        }

        [TestMethod]
        public void Digits_GoodLines_ReturnsNineDigits()
        {
            var lines = new string[4];
            lines[0] = "    _  _     _  _  _  _  _ ";
            lines[1] = "  | _| _||_||_ |_   ||_||_|";
            lines[2] = "  ||_  _|  | _||_|  ||_| _|";
            lines[3] = "                           ";

            var dan = new DigitizedAccountNumber(lines);

            Assert.AreEqual(9, dan.Digits.Count);
            var one = "   "
                    + "  |"
                    + "  |";
            Assert.AreEqual(one, dan.Digits.First());
            var nine = " _ "
                     + "|_|"
                     + " _|";
            Assert.AreEqual(nine, dan.Digits.Last());
        }
    }

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
    }

    public class Parser
    {
        public List<DigitizedAccountNumber> GetDigitizedAccountNumbers(string file)
        {
            var dans = new List<DigitizedAccountNumber>();
            string[] allLines = System.IO.File.ReadAllLines(file);
            for (var i = 0; i < allLines.Length; i += 4)
            {
                string[] lines = new string[4];
                lines[0] = allLines[i];
                lines[1] = allLines[i + 1];
                lines[2] = allLines[i + 2];
                lines[3] = allLines[i + 3];

                dans.Add(new DigitizedAccountNumber(lines));
            }
            return dans;
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
             + "  |", 1},
            {  " _ "
             + " _|"
             + "|_ ", 2},
            {  " _ "
             + " _|"
             + " _|", 3},
            {  "   "
             + "|_|"
             + "  |", 4},
            {  " _ "
             + "|_ "
             + " _|", 5},
            {  " _ "
             + "|_ "
             + "|_|", 6},
            {  " _ "
             + "  |"
             + "  |", 7},
            {  " _ "
             + "|_|"
             + "|_|", 8},
            {  " _ "
             + "|_|"
             + " _|", 9},
        };

        public static int ToArabic(string digit)
        {
            return digitArabicMap[digit];
        }
    }
}
