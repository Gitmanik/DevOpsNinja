namespace KalkulatorKredytowy.Data
{
	public enum LoanSchedule
	{
		Monthly,
		Quarterly
	}

	public class LoanData
	{
		public string? ClientName;
		public DateTime? ContractDate;
		public uint? FinancingSpan;
		public decimal? InvestmentValue;
		public decimal? InterestRate;
		public decimal? Commission;
		public LoanSchedule Schedule;
	}
}