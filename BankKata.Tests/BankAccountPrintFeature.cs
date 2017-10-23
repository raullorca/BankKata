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
        private Mock<IWrite> writer;
        private Mock<IDate> date;
        private IBankAccount bankAccount;

        public BankAccountPrintFeature()
        {
            writer = new Mock<IWrite>();
            date = new Mock<IDate>();
            bankAccount = new BankAccount(writer.Object, date.Object);

        }

        [Fact]
        public void PrintAccountStatment()
        {
            bankAccount.Deposit(1000);
            bankAccount.Withdraw(100); // or -100
            bankAccount.Deposit(500);
            bankAccount.PrintStatement();

            var values = new List<string>();

            writer
                .Setup(x => x.Print(It.IsAny<string>()))
                .Callback<string>((text) => values.Add(text));

            writer.Verify(x => x.Print("DATE       | AMOUNT  | BALANCE"));
            writer.Verify(x => x.Print("10/04/2014 | 500.00  | 1400.00"));
            writer.Verify(x => x.Print("02/04/2014 | -100.00 | 900.00"));
            writer.Verify(x => x.Print("01/04/2014 | 1000.00 | 1000.00"));
        }

        [Fact]
        public void ShouldPrintAnEmptyBalanceWhenThereAreNotMovements()
        {
            bankAccount.PrintStatement();

            writer.Verify(x => x.Print("DATE       | AMOUNT  | BALANCE"));
        }
        [Fact]
        public void ShouldByAddDepositShowNewBalancePositive()
        {
            date.Setup(d => d.Date).Returns(new DateTime(2014, 4, 1));
            bankAccount.Deposit(1000);
            bankAccount.PrintStatement();

            writer.Verify(x => x.Print("DATE       | AMOUNT  | BALANCE"));
            writer.Verify(x => x.Print("01/04/2014 | 1000,00 | 1000,00"));
        }

        [Fact]
        public void ShouldByAddWithdrawShowNewBalanceNegative()
        {
            date.Setup(d => d.Date).Returns(new DateTime(2014, 4, 2));
            bankAccount.Withdraw(-100);
            bankAccount.PrintStatement();

            writer.Verify(x => x.Print("DATE       | AMOUNT  | BALANCE"));
            writer.Verify(x => x.Print("02/04/2014 | -100,00 | -100,00"));
        }

        [Fact]
        public void ShouldByAddWithdrawShowNewBalanceNegative_2()
        {
            date.Setup(d => d.Date).Returns(new DateTime(2014, 4, 2));
            bankAccount.Withdraw(100);
            bankAccount.PrintStatement();

            writer.Verify(x => x.Print("DATE       | AMOUNT  | BALANCE"));
            writer.Verify(x => x.Print("02/04/2014 | -100,00 | -100,00"));
        }
    }
}
