public class Members
{
    private readonly List<Member> _items = new List<Member>();
    private int _nextId = 1;

    public List<Member> GetAll()
    {
        return _items;
    }

    public Member Register(string name, IMemberLoanPolicy policy)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("Name is required.");

        Member member = new Member(_nextId, name.Trim(), policy);
        _nextId++;
        _items.Add(member);
        return member;
    }

    public Member? Find(int id)
    {
        foreach (Member member in _items)
        {
            if (member.Id == id)
                return member;
        }
        return null;
    }
}