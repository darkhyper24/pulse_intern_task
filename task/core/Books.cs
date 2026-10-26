public class Books
{
    private readonly List<Book> _items = new List<Book>();
    private int _nextId = 1;

    public List<Book> GetAll()
    {
        return _items;
    }

    public Book Add(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new InvalidOperationException("Title is required.");
        if (string.IsNullOrWhiteSpace(author))
            throw new InvalidOperationException("Author is required.");

        Book book = new Book(_nextId, title.Trim(), author.Trim());
        _nextId++;
        _items.Add(book);
        return book;
    }

    public Book? Find(int id)
    {
        foreach (Book book in _items)
        {
            if (book.Id == id)
                return book;
        }
        return null;
    }

    public List<Book> SearchByTitle(string text)
    {
        List<Book> results = new List<Book>();
        foreach (Book book in _items)
        {
            if (book.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
                results.Add(book);
        }
        return results;
    }

    public List<Book> SearchByAuthor(string text)
    {
        List<Book> results = new List<Book>();
        foreach (Book book in _items)
        {
            if (book.Author.Contains(text, StringComparison.OrdinalIgnoreCase))
                results.Add(book);
        }
        return results;
    }
}