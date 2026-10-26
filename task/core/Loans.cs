public class Loans
{
    private readonly List<Loan> _items = new List<Loan>();
    private int _nextId = 1;

    public Loan Create(int bookId, int memberId, DateOnly dueOn)
    {
        Loan loan = new Loan(_nextId, bookId, memberId, dueOn);
        _nextId++;
        _items.Add(loan);
        return loan;
    }

    public Loan? FindActive(int memberId, int bookId)
    {
        foreach (Loan loan in _items)
        {
            if (loan.IsActive && loan.MemberId == memberId && loan.BookId == bookId)
                return loan;
        }
        return null;
    }

    public List<Loan> GetActiveForMember(int memberId)
    {
        List<Loan> results = new List<Loan>();
        foreach (Loan loan in _items)
        {
            if (loan.IsActive && loan.MemberId == memberId)
                results.Add(loan);
        }
        return results;
    }

    public List<Loan> GetOverdue(DateOnly today)
    {
        return _items.Where(loan => loan.IsActive && loan.DueOn < today).ToList();
    }
}