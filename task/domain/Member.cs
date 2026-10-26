public class Member
{
    public int Id { get; }
    public string Name { get; }
    public IMemberLoanPolicy Policy { get; }

    public Member(int id, string name, IMemberLoanPolicy policy)
    {
        Id = id;
        Name = name;
        Policy = policy;
    }
}