using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
    public class DigitConverter
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
