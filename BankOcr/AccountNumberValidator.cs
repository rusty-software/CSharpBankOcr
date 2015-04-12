using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
    public class AccountNumberValidator
    {
        public bool IsValid(string accountNumber)
        {
            // clarity over brevity
            var checksum = 0;
            var numbers = accountNumber.Select(c => c.ToString()).ToArray();
            for (var i = 0; i < 9; i++)
            {
                checksum += Convert.ToInt16(numbers[i]) * (9 - i);
            }
            checksum = checksum % 11;
            return checksum == 0;
        }
    }
}
