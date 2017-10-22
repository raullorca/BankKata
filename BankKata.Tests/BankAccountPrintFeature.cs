using BankKata.IO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankKata.Tests
{
    public class BankAccountPrintFeature
    {
        [Fact]
        public void PrintAccountStatment()
        {
            Mock<IWrite> write = new Mock<IWrite>();

            IBankAccount bankAccount = new BankAccount();

            bankAccount.Deposit(1000);
            bankAccount.Withdraw(100); // or -100
            bankAccount.Deposit(500);
            bankAccount.PrintStatement();

            var values = new List<string>();

            write
                .Setup(x => x.Print(It.IsAny<string>()))
                .Callback<string>((text) => values.Add(text));

            write.Verify(x => x.Print("DATE       | AMOUNT  | BALANCE"));
            write.Verify(x => x.Print("10/04/2014 | 500.00  | 1400.00"));
            write.Verify(x => x.Print("02/04/2014 | -100.00 | 900.00"));
            write.Verify(x => x.Print("01/04/2014 | 1000.00 | 1000.00"));
        }
    }
}
