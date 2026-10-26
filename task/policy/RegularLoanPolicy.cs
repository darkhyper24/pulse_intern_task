public class RegularLoanPolicy : IMemberLoanPolicy
{
    public string Name { get { return "Regular"; } }
    public int MaxActiveLoans { get { return 3; } }
    public int LoanPeriodDays { get { return 14; } }
}