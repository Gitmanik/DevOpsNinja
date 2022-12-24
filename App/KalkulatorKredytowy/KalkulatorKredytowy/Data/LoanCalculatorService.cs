namespace KalkulatorKredytowy.Data
{
	public class LoanCalculatorService
	{
		public LoanSchedule CalculateLoan(LoanData l)
		{
			LoanSchedule sch = new LoanSchedule();
			sch.Data = l;

			DateTime currDate = l.ContractDate;

			l.InterestRate /= 100;

			decimal interest = l.InterestRate / l.InstallmentsInYear;

			decimal x = (decimal)Math.Pow((double)(1 + interest), l.FinancingSpan / 12 * l.InstallmentsInYear);
			decimal monthlyPayment = l.CreditAmount * interest * x / (x - 1);

			monthlyPayment = decimal.Truncate(monthlyPayment * 100) / 100;
			decimal capitalLeftToPay = l.CreditAmount;

			int f = l.FinancingSpan / 12 * l.InstallmentsInYear;

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
					Payment = monthlyPayment,
					Capital = decimal.Truncate(localCapital * 100) / 100,
					Interest = decimal.Truncate(localInterest * 100) / 100
				});

				currDate = currDate.AddMonths(12 / l.InstallmentsInYear);
			}

			return sch;
		}
	}
}