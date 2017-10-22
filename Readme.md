### Bank kata

Create a simple bank application with the following features:

- Deposit into Account
- Withdraw from an Account
- Print a bank statement to the console.

The entry point should be the following interface, which you can not change. Don't worry about introducing abstractions on Money or Date as this is not the point of the exercise.

```
public class BankAccount {
	public void deposit(int amount);
	public void withdraw(int amount);
	public void printStatement();
}
```

Statement should have the following format:

```
	DATE       | AMOUNT  | BALANCE
	10/04/2014 | 500.00  | 1400.00
	02/04/2014 | -100.00 | 900.00
	01/04/2014 | 1000.00 | 1000.00
```


By Robert Firek @robertfirek original description: https://gist.github.com/robertfirek/381a286ff43a3bb80e957cae58321085
