public class PremiumLoanPolicy : IMemberLoanPolicy
{
    public string Name { get { return "Premium"; } }
    public int MaxActiveLoans { get { return 5; } }   
    public int LoanPeriodDays { get { return 14; } }
}