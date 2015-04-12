using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
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
}
