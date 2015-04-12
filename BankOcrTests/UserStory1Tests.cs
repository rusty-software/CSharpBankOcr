using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System.Text;
using BankOcr;

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

        [TestMethod]
        public void ToArabicAccountNumber_GoodLines_ReturnsArabic()
        {
            var lines = new string[4];
            lines[0] = "    _  _     _  _  _  _  _ ";
            lines[1] = "  | _| _||_||_ |_   ||_||_|";
            lines[2] = "  ||_  _|  | _||_|  ||_| _|";
            lines[3] = "                           ";

            var dan = new DigitizedAccountNumber(lines);

            Assert.AreEqual("123456789", dan.ToArabicAccountNumber());
        }
    }
}
