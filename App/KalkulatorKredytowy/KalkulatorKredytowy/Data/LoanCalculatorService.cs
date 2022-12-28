namespace KalkulatorKredytowy.Data
{
	public class LoanCalculatorService
	{
		public LoanSchedule CalculateLoan(LoanData l)
		{
			l.CreditAmount -= l.OwnContribution;

			LoanSchedule sch = new LoanSchedule();
			sch.Data = l;

			decimal commisionAmount = 0;

			if (l.CommissionRate != 0)
				commisionAmount = l.CreditAmount * l.CommissionRate / 100m / l.FinancingSpan * l.InstallmentsInYear / 12;

			DateTime currDate = l.ContractDate;

			l.InterestRate /= 100;

			decimal interest = l.InterestRate / ((decimal)l.InstallmentsInYear);

			decimal x = (decimal)Math.Pow((double)(1 + interest), l.FinancingSpan * l.InstallmentsInYear / 12);
			decimal monthlyPayment = l.CreditAmount * interest * x / (x - 1);

			monthlyPayment = decimal.Truncate(monthlyPayment * 100) / 100;
			decimal capitalLeftToPay = l.CreditAmount;

			int f = l.FinancingSpan * l.InstallmentsInYear / 12 ;

			for (int i = 0; i < f; i++)
			{
				decimal localInterest = capitalLeftToPay * interest;
				decimal localCapital = monthlyPayment - localInterest;

				capitalLeftToPay -= localCapital;

				if (i == f - 1)
				{
					monthlyPayment += capitalLeftToPay;
					localInterest = monthlyPayment * interest;
				}

				sch.Entries.Add(new LoanSchedule.ScheduleEntry
				{
					No = i + 1,
					Date = currDate,
					Payment = monthlyPayment + decimal.Truncate(commisionAmount),
					Capital = decimal.Truncate(localCapital * 100) / 100,
					Interest = decimal.Truncate((localInterest + commisionAmount) * 100) / 100
				});

				currDate = currDate.AddMonths(12 / l.InstallmentsInYear);
			}

			return sch;
		}
	}
}