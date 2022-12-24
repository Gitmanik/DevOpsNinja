using System;
using System.Collections.Generic;
using System.Linq;

namespace KalkulatorKredytowy.Data
{
	public class LoanData : DebugPrintable
	{
		public string ClientName;
		public DateTime ContractDate;
		public int FinancingSpan;
		public decimal InvestmentValue;
		public decimal InterestRate;
		public decimal CommissionRate;
		public int InstallmentsInYear;
	}
}