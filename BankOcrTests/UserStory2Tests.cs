using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BankOcr;

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
            Assert.IsFalse(validator.IsValid("222222222"));
        }
    }
}
