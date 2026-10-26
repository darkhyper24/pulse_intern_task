public class Library : ILibrary
{
    private readonly Books _books = new Books();
    private readonly Members _members = new Members();
    private readonly Loans _loans = new Loans();

    private static DateOnly Today
    {
        get { return DateOnly.FromDateTime(DateTime.Today); }
    }

    // ---- Books ----
    public Book AddBook(string title, string author)
    {
        return _books.Add(title, author);
    }

    public List<Book> GetBooks()
    {
        return _books.GetAll();
    }

    public List<Book> SearchByTitle(string text)
    {
        return _books.SearchByTitle(text);
    }

    public List<Book> SearchByAuthor(string text)
    {
        return _books.SearchByAuthor(text);
    }

    // ---- Members ----
    public Member RegisterMember(string name, IMemberLoanPolicy policy)
    {
        return _members.Register(name, policy);
    }

    public List<Member> GetMembers()
    {
        return _members.GetAll();
    }

    // ---- Loans ----
    public Loan Borrow(int memberId, int bookId)
    {
        Member? member = _members.Find(memberId);
        if (member == null)
            throw new InvalidOperationException("Member not found.");

        Book? book = _books.Find(bookId);
        if (book == null)
            throw new InvalidOperationException("Book not found.");

        if (!book.IsAvailable)
            throw new InvalidOperationException("Book is already borrowed.");

        int activeCount = _loans.GetActiveForMember(memberId).Count;
        if (activeCount >= member.Policy.MaxActiveLoans)
            throw new InvalidOperationException(
                "Member has reached the maximum of " + member.Policy.MaxActiveLoans + " active loans.");

        DateOnly dueOn = Today.AddDays(member.Policy.LoanPeriodDays);
        Loan loan = _loans.Create(bookId, memberId, dueOn);
        book.MarkBorrowed();
        return loan;
    }

    public void Return(int memberId, int bookId)
    {
        Loan? loan = _loans.FindActive(memberId, bookId);
        if (loan == null)
            throw new InvalidOperationException("No active loan found for this member and book.");

        Book? book = _books.Find(bookId);
        if (book == null)
            throw new InvalidOperationException("Book not found.");

        loan.Close(Today);
        book.MarkReturned();
    }

    public List<Loan> GetActiveLoans(int memberId)
    {
        if (_members.Find(memberId) == null)
            throw new InvalidOperationException("Member not found.");

        return _loans.GetActiveForMember(memberId);
    }

    public List<Loan> GetOverdueLoans()
    {
        return _loans.GetOverdue(Today);
    }
}