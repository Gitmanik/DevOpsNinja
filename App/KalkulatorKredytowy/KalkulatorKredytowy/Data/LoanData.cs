namespace KalkulatorKredytowy.Data
{
	public class LoanData : DebugPrintable
	{
		public string ClientName;
		public DateTime ContractDate;
		public int FinancingSpan;
		public decimal CreditAmount;
		public decimal InterestRate;
		public decimal CommissionRate;
		public decimal InvestmentValue;
		public decimal OwnContribution;
		public int InstallmentsInYear;
		public string LoanName;
	}

	public class LoanSchedule : DebugPrintable
	{
		public LoanData Data;
		public List<ScheduleEntry> Entries = new List<ScheduleEntry>();

		public class ScheduleEntry : DebugPrintable
		{
			public int No;
			public DateTime Date;
			public decimal Payment;
			public decimal Interest;
			public decimal Capital;
		}
	}
}