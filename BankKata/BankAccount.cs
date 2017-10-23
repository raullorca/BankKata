using BankKata.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKata
{
    public class BankAccount : IBankAccount
    {
        private IWrite writer;
        private decimal balance;
        private Stack<Movement> movements;
        private IDate date;

        public BankAccount(IWrite writer, IDate date)
        {
            this.writer = writer;
            balance = 0;
            movements = new Stack<Movement>();
            this.date = date;
        }

        public void Deposit(int amount)
        {
            UpdateAccount(amount);

        }

        private void UpdateAccount(int amount)
        {
            UpdateBalance(amount);

            AddMovement(amount);
        }

        private void UpdateBalance(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            amount = Math.Abs(amount) * -1;

            UpdateAccount(amount);
        }

        private void AddMovement(int amount)
        {
            movements.Push(new Movement { Date = date.Date, Amount = amount });
        }

        public void PrintStatement()
        {
            writer.Print("DATE       | AMOUNT  | BALANCE");
            foreach (var item in movements)
            {
                string date = item.Date.ToShortDateString();
                string amount = item.Amount.ToString("#.00");
                string balance = amount;

                writer.Print($"{date} | {amount} | {balance}");
            }
        }


    }
}
