﻿using System;
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
