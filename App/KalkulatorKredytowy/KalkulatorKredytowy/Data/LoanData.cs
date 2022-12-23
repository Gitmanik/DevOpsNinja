namespace KalkulatorKredytowy.Data
{
	public enum LoanSchedule
	{
		Monthly,
		Quarterly
	}

	public class LoanData
	{
		public string ClientName;
		public DateTime ContractDate;
		public int FinancingSpan;
		public decimal InvestmentValue;
		public decimal InterestRate;
		public decimal Commission;
		public LoanSchedule Schedule;
	}
}