using System;
using System.IO;

/// <summary>
/// Utility class that provides helper methods for input handling.
/// </summary>
class Utility
{
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="Parser">The parser function that converts the input string to the desired type.</param>
    /// <param name="Prompt">The prompt to display to the user.</param>
    /// <param name="ErrorPrompt">The prompt to display when the input is invalid.</param>
    /// <returns>The parsed input value.</returns>
    /// <summary>
    /// Gets input from the user and parses it to the desired type.
    /// </summary>
    public static T GetInput<T>(Func<string, T> Parser, string Prompt, string ErrorPrompt = "Please enter a valid input!")
    {
        if (!Prompt.EndsWith(": "))
        {
            Prompt += ": ";
        }

        while (true)
        {
            Console.Write(Prompt);
            string Line = Console.ReadLine();

            if (Line is not null)
            {
                if (Line.Length > 0)
                {
                    T Value;
                    try
                    {
                        Value = Parser(Line);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(ErrorPrompt);
                        continue;
                    }

                    return Value;
                }
                else
                {
                    Console.WriteLine(ErrorPrompt);
                };
            }
            else
            {
                Console.WriteLine(ErrorPrompt);
            };
        }

    }
}

class Program
{
    // 1. Create a C# console application that defines a class called Book with properties Title and Author. Instantiate an object of this class, set values for the properties, and display the information on the console.
    class Book
    {
        public string Title;
        public string Author;

        public Book(string Title, string Author)
        {
            this.Title = Title;
            this.Author = Author;
        }
    }

    static void Question1()
    {
        Book CustomBook = new Book(
            Utility.GetInput((string x) => x, "Enter the title of the book"),
            Utility.GetInput((string x) => x, "Enter the author of the book")
        );

        Console.WriteLine($"The book's title is {CustomBook.Title} and the author is {CustomBook.Author}");
    }

    // 2. Develop a console program that models a simple bank account. Create a class named ‘BankAccount’ with properties AccountNumber and Balance. Implement a method ‘Deposit’ that allows the user to deposit money into the account. Instantiate an object, perform a deposit, and display the updated balance.
    class BankAccount
    {
        public string AccountNumber;
        private decimal Balance = 0;

        public BankAccount(string AccountNumber = "0000000000")
        {
            this.AccountNumber = AccountNumber;
        }

        public void Deposit(decimal Amount)
        {
            if (Amount <= 0)
            {
                throw new InvalidOperationException("Invalid amount");
            }

            Balance += Amount;
        }

        public void Withdraw(decimal Amount)
        {
            if (Amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            Balance -= Amount;
        }

        public decimal GetBalance()
        {
            return this.Balance;
        }
    }

    static void Question2()
    {
        BankAccount account = new BankAccount(
            Utility.GetInput((string x) => x, "Enter the account number")
        );
        Console.WriteLine();


        while (true)
        {
            Console.WriteLine($"The current balance is {account.GetBalance()}");

            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Exit");

            Console.WriteLine();
            string Action = Utility.GetInput((string x) => x, "Enter the action to perform");
            Console.WriteLine();
            try
            {
                switch (Action)
                {
                    case "1":
                        account.Deposit(Utility.GetInput(decimal.Parse, "Enter the amount to deposit"));
                        break;
                    case "2":
                        account.Withdraw(Utility.GetInput(decimal.Parse, "Enter the amount to withdraw"));
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid action");
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    // 3. Build an application to store and display the temperatures of a week using an array. Create a class named ‘TemperatureTracker’ with an array to store daily temperatures. Implement a method to display the temperatures. Instantiate an object, input temperatures, and display the weekly temperature report.
    class TemperatureTracker
    {
        public int[] Temperatures = new int[7];

        public void DisplayTemperatures()
        {
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"Day {i + 1}: {Temperatures[i]}");
            }
        }

        public void AverageTemperature()
        {
            int Total = 0;
            for (int i = 0; i < 7; i++)
            {
                Total += Temperatures[i];
            }

            Console.WriteLine($"The average temperature for the week is {Total / 7}");
        }
    }

    static void Question3()
    {
        TemperatureTracker Tracker = new TemperatureTracker();

        for (int i = 0; i < 7; i++)
        {
            Tracker.Temperatures[i] = Utility.GetInput(int.Parse, $"Enter the temperature for day {i + 1}");
        }

        Tracker.DisplayTemperatures();
        Tracker.AverageTemperature();
    }

    // 4. Construct a C# program for a basic inventory system. Define a class named Product with properties ProductName and Price. Implement a parameterized constructor to initialize these properties. Instantiate objects using the constructor and display the product details.
    class Product
    {
        public string ProductName;
        public decimal Price;

        public Product(string ProductName, decimal Price)
        {
            this.ProductName = ProductName;
            this.Price = Price;
        }
    }

    static void Question4()
    {
        int InventorySize = Utility.GetInput(int.Parse, "Enter the size of the inventory. Note that this cannot be changed later!");
        Console.WriteLine();

        Product[] Inventory = new Product[InventorySize];

        while (true)
        {
            Console.WriteLine("1. Add a product");
            Console.WriteLine("2. Remove a product");
            Console.WriteLine("3. Display products");
            Console.WriteLine("4. Exit");

            Console.WriteLine();
            string Action = Utility.GetInput((string x) => x, "Enter the action to perform");
            Console.WriteLine();

            switch (Action)
            {
                case "1":
                    int Index = Array.FindIndex(Inventory, product => product is null);
                    if (Index == -1)
                    {
                        Console.WriteLine("The inventory is full");
                        break;
                    }

                    Inventory[Index] = new Product(
                        Utility.GetInput((string x) => x, "Enter the product name"),
                        Utility.GetInput(decimal.Parse, "Enter the product price")
                    );
                    break;
                case "2":
                    Index = Utility.GetInput(int.Parse, "Enter the index of the product to remove") - 1;

                    if (Index < 0 || Index >= InventorySize)
                    {
                        Console.WriteLine("Invalid index");
                        break;
                    }

                    Inventory[Index] = null;
                    break;
                case "3":
                    Console.WriteLine("Inventory,\n");
                    for (int i = 0; i < InventorySize; i++)
                    {
                        if (Inventory[i] is not null)
                        {
                            Console.WriteLine($"{i + 1}. Product: {Inventory[i].ProductName}, Price: {Inventory[i].Price}");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. Empty");
                        }
                    }
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid action");
                    break;
            }

            Console.WriteLine();
        }
    }

    // 5. `Develop an application that simulates a library system. Create a class named LibraryBook with properties Title, Author, and Available. Implement a method BorrowBook that updates the availability status. Instantiate multiple book objects, perform book borrowing, and display the updated library status.
    class LibraryBook
    {
        public string Title;
        public string Author;
        public bool Available = true;

        public LibraryBook(string Title, string Author)
        {
            this.Title = Title;
            this.Author = Author;
        }

        public void BorrowBook()
        {
            if (Available)
            {
                Available = false;
            }
            else
            {
                Console.WriteLine("The book is not available");
            }
        }
    }

    static void Question5()
    {
        const int LibrarySize = 10;
        LibraryBook[] Library = new LibraryBook[LibrarySize];

        for (int i = 0; i < LibrarySize; i++)
        {
            Library[i] = new LibraryBook(
                "Book " + (i + 1),
                "Author " + (i + 1)
            );
        }

        while (true)
        {
            Console.WriteLine("1. Borrow a book");
            Console.WriteLine("2. Return a book");
            Console.WriteLine("3. Display library status");
            Console.WriteLine("4. Exit");

            Console.WriteLine();
            string Action = Utility.GetInput((string x) => x, "Enter the action to perform");
            Console.WriteLine();

            switch (Action)
            {
                case "1":
                    int Index = Utility.GetInput(int.Parse, "Enter the index of the book to borrow") - 1;
                    Library[Index].BorrowBook();
                    break;
                case "2":
                    Index = Utility.GetInput(int.Parse, "Enter the index of the book to return") - 1;
                    Library[Index].Available = true;
                    break;
                case "3":
                    Console.WriteLine("Library status,\n");
                    for (int i = 0; i < LibrarySize; i++)
                    {
                        Console.WriteLine($"{i + 1}. Name: {Library[i].Title}, Author: {Library[i].Author}, Available: {Library[i].Available}");
                    }
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid action");
                    break;
            }

            Console.WriteLine();
        }
    }


    static void Main(string[] args)
    {
        // Question1();
        // Question2();
        // Question3();
        // Question4();
        Question5();
    }
}