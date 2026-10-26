public interface IMemberLoanPolicy
{
    string Name { get; }
    int MaxActiveLoans { get; }
    int LoanPeriodDays { get; }
}