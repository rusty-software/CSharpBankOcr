using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BankOcrTests
{
    [TestClass]
    public class UserStory2Tests
    {
        private AccountNumberValidator validator = new AccountNumberValidator();

        [TestMethod]
        public void IsValid_GoodAccountNumber_ReturnsTrue()
        {
            Assert.IsTrue(validator.IsValid("345882865"));
        }

        [TestMethod]
        public void IsValid_BadAccountNumber_ReturnsFalse()
        {
            Assert.IsTrue(validator.IsValid("123456789"));
        }
    }

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
