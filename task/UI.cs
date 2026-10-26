public class ConsoleMenu
{
    private readonly ILibrary _library;

    public ConsoleMenu(ILibrary library)
    {
        _library = library;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            string? choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == null || choice.Trim() == "10")
                return;

            try
            {
                switch (choice.Trim())
                {
                    case "1": AddBook(); break;
                    case "2": ListBooks(); break;
                    case "3": SearchBooks(); break;
                    case "4": RegisterMember(); break;
                    case "5": ListMembers(); break;
                    case "6": BorrowBook(); break;
                    case "7": ReturnBook(); break;
                    case "8": ListActiveLoans(); break;
                    case "9": ListOverdueLoans(); break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("=== Library Management ===");
        Console.WriteLine("1. Add a book");
        Console.WriteLine("2. List all books");
        Console.WriteLine("3. Search books");
        Console.WriteLine("4. Register a member");
        Console.WriteLine("5. List members");
        Console.WriteLine("6. Borrow a book");
        Console.WriteLine("7. Return a book");
        Console.WriteLine("8. List active loans for a member");
        Console.WriteLine("9. List overdue loans");
        Console.WriteLine("10. Exit");
        Console.Write("Choose an option: ");
    }

    // ---- Books ----
    private void AddBook()
    {
        string title = ReadText("Title: ");
        string author = ReadText("Author: ");
        Book book = _library.AddBook(title, author);
        Console.WriteLine("Added: " + DescribeBook(book));
    }

    private void ListBooks()
    {
        PrintBooks(_library.GetBooks());
    }

    private void SearchBooks()
    {
        Console.WriteLine("Search by: 1) Title  2) Author");
        int by = ReadInt("Choice: ");
        string text = ReadText("Search text: ");

        if (by == 1)
            PrintBooks(_library.SearchByTitle(text));
        else if (by == 2)
            PrintBooks(_library.SearchByAuthor(text));
        else
            Console.WriteLine("Invalid choice.");
    }

    // ---- Members ----
    private void RegisterMember()
    {
        string name = ReadText("Name: ");
        Console.WriteLine("Member type: 1) Regular  2) Premium");
        int type = ReadInt("Choice: ");

        IMemberLoanPolicy policy;
        if (type == 1)
            policy = new RegularLoanPolicy();
        else if (type == 2)
            policy = new PremiumLoanPolicy();
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        Member member = _library.RegisterMember(name, policy);
        Console.WriteLine("Registered: " + DescribeMember(member));
    }

    private void ListMembers()
    {
        List<Member> members = _library.GetMembers();
        if (members.Count == 0)
        {
            Console.WriteLine("No members registered.");
            return;
        }

        foreach (Member member in members)
            Console.WriteLine(DescribeMember(member));
    }

    // ---- Loans ----
    private void BorrowBook()
    {
        int memberId = ReadInt("Member ID: ");
        int bookId = ReadInt("Book ID: ");
        Loan loan = _library.Borrow(memberId, bookId);
        Console.WriteLine("Borrowed. Due on " + loan.DueOn.ToString("yyyy-MM-dd") + ".");
    }

    private void ReturnBook()
    {
        int memberId = ReadInt("Member ID: ");
        int bookId = ReadInt("Book ID: ");
        _library.Return(memberId, bookId);
        Console.WriteLine("Book returned.");
    }

    private void ListActiveLoans()
    {
        int memberId = ReadInt("Member ID: ");
        PrintLoans(_library.GetActiveLoans(memberId), "No active loans.");
    }

    private void ListOverdueLoans()
    {
        PrintLoans(_library.GetOverdueLoans(), "No overdue loans.");
    }

    // ---- Printing ----
    private void PrintBooks(List<Book> books)
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach (Book book in books)
            Console.WriteLine(DescribeBook(book));
    }

    private void PrintLoans(List<Loan> loans, string emptyMessage)
    {
        if (loans.Count == 0)
        {
            Console.WriteLine(emptyMessage);
            return;
        }

        foreach (Loan loan in loans)
        {
            Console.WriteLine("Loan #" + loan.Id +
                              ": book " + loan.BookId +
                              ", member " + loan.MemberId +
                              ", due " + loan.DueOn.ToString("yyyy-MM-dd"));
        }
    }

    private string DescribeBook(Book book)
    {
        string status = book.IsAvailable ? "available" : "borrowed";
        return "[" + book.Id + "] " + book.Title + " by " + book.Author + " (" + status + ")";
    }

    private string DescribeMember(Member member)
    {
        return "[" + member.Id + "] " + member.Name +
               " (" + member.Policy.Name + ", max " + member.Policy.MaxActiveLoans + " loans)";
    }

    // ---- Input helpers ----
    private string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Please enter a value.");
        }
    }

    private int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Please enter a whole number.");
        }
    }
}