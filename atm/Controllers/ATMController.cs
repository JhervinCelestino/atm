using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class ATMController : Controller
{
    private DatabaseHelper dbHelper = new DatabaseHelper();

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string cardNumber, string pin)
    {
        if (dbHelper.ValidateUser(cardNumber, pin))
        {
            HttpContext.Session.SetString("CardNumber", cardNumber);
            return RedirectToAction("MainMenu");
        }

        ViewBag.Message = "Invalid Card Number or PIN";
        return View();
    }

    public IActionResult Welcome()
    {
        return View();
    }

    public IActionResult MainMenu()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        return View();
    }

    // Withdraw Cash
    public IActionResult WithdrawCash()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    [HttpPost]
    public IActionResult WithdrawCash(decimal amount)
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        decimal currentBalance = dbHelper.GetBalance(cardNumber);

        if (amount >= 100 && currentBalance >= amount)
        {
            dbHelper.UpdateBalance(cardNumber, amount, false);
            dbHelper.RecordTransaction(cardNumber, "Withdraw", amount);
            ViewBag.Message = "Withdrawal successful!";
        }
        else
        {
            ViewBag.Message = "Invalid amount! Minimum withdrawal is 100, or insufficient balance.";
        }

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    // Cash Deposit
    public IActionResult CashDeposit()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    [HttpPost]
    public IActionResult CashDeposit(decimal amount)
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        if (amount <= 0)
        {
            ViewBag.Message = "Invalid deposit amount.";
        }
        else
        {
            dbHelper.UpdateBalance(cardNumber, amount, true);
            dbHelper.RecordTransaction(cardNumber, "Deposit", amount);
            ViewBag.Message = "Deposit successful!";
        }

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    // Check Balance
    public IActionResult CheckBalance()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    // Change PIN
    public IActionResult ChangePin()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        return View();
    }

    [HttpPost]
    public IActionResult ChangePin(string currentPin, string newPin, string confirmPin)
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        if (string.IsNullOrEmpty(currentPin) || string.IsNullOrEmpty(newPin) || string.IsNullOrEmpty(confirmPin))
        {
            ViewBag.Message = "All fields are required.";
        }
        else if (newPin != confirmPin)
        {
            ViewBag.Message = "New PIN and Confirm PIN do not match.";
        }
        else if (!dbHelper.ValidateUser(cardNumber, currentPin))
        {
            ViewBag.Message = "Current PIN is incorrect.";
        }
        else
        {
            dbHelper.UpdatePin(cardNumber, newPin);
            ViewBag.Message = "PIN successfully changed!";
        }

        return View();
    }

    // Transfer Funds
    public IActionResult TransferFunds()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    [HttpPost]
    public IActionResult TransferFunds(string recipientCardNumber, decimal amount)
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        decimal currentBalance = dbHelper.GetBalance(cardNumber);

        if (!string.IsNullOrEmpty(recipientCardNumber) && dbHelper.UserExists(recipientCardNumber) && amount > 0 && currentBalance >= amount)
        {
            dbHelper.TransferFunds(cardNumber, recipientCardNumber, amount);
            dbHelper.RecordTransaction(cardNumber, "Transfer", amount, recipientCardNumber);
            ViewBag.Message = "Transfer successful!";
        }
        else
        {
            ViewBag.Message = "Invalid details or insufficient balance.";
        }

        ViewBag.Balance = dbHelper.GetBalance(cardNumber);
        return View();
    }

    // Mini Statement
    public IActionResult MiniStatement()
    {
        string cardNumber = HttpContext.Session.GetString("CardNumber");
        if (string.IsNullOrEmpty(cardNumber)) return RedirectToAction("Welcome");

        ViewBag.Transactions = dbHelper.GetTransactions(cardNumber);
        return View();
    }

    // Logout (Redirects to Welcome Page)
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Welcome");
    }
}
