public class Loan
{
    public int Id { get; }
    public int BookId { get; }
    public int MemberId { get; }
    public DateOnly DueOn { get; }
    public DateOnly? ReturnedOn { get; private set; }

    public bool IsActive
    {
        get { return ReturnedOn == null; }
    }

    public Loan(int id, int bookId, int memberId, DateOnly dueOn)
    {
        Id = id;
        BookId = bookId;
        MemberId = memberId;
        DueOn = dueOn;
    }

    public void Close(DateOnly returnedOn)
    {
        ReturnedOn = returnedOn;
    }
}