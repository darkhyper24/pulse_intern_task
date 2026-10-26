public interface ILibrary
{
    Book AddBook(string title, string author);
    List<Book> GetBooks();
    List<Book> SearchByTitle(string text);
    List<Book> SearchByAuthor(string text);

    Member RegisterMember(string name, IMemberLoanPolicy policy);
    List<Member> GetMembers();

    Loan Borrow(int memberId, int bookId);
    void Return(int memberId, int bookId);
    List<Loan> GetActiveLoans(int memberId);
    List<Loan> GetOverdueLoans();
}