using KalkulatorKredytowy.Data;
using KalkulatorKredytowy.Elements;

namespace KKTest
{
	public class Tests
	{
		[Test]
		public void Test1()
		{
			LoanCalculatorService svc = new LoanCalculatorService();

			LoanData loan = new LoanData();

			loan.LoanName = "Kredyt";
			loan.ClientName = "Imie Naz";
			loan.ContractDate = DateTime.Today;
			loan.FinancingSpan = int.Parse(LoanCreator.FinancingSpanRegex.Match("12").Groups[1].ValueSpan, LoanCreator.DefaultCulture);
			loan.CreditAmount = decimal.Parse(LoanCreator.CreditAmountRegex.Match("1000").Groups[1].Value.Replace(" ", ""), LoanCreator.DefaultCulture);
			loan.InterestRate = decimal.Parse(LoanCreator.InterestRateRegex.Match("1").Groups[1].ValueSpan, LoanCreator.DefaultCulture);
			loan.CommissionRate = decimal.Parse(LoanCreator.CommissionRateRegex.Match("0").Groups[1].ValueSpan, LoanCreator.DefaultCulture);
			loan.InstallmentsInYear = 12;
			loan.InvestmentValue = decimal.Parse(LoanCreator.CreditAmountRegex.Match("1000").Groups[1].ValueSpan, LoanCreator.DefaultCulture);
			loan.OwnContribution = decimal.Parse(LoanCreator.CreditAmountRegex.Match("0").Groups[1].ValueSpan, LoanCreator.DefaultCulture);

			var x = svc.CalculateLoan(loan);
			ScheduleGeneratorService gen = new ScheduleGeneratorService();
			gen.GeneratePDF(x);

			Assert.Pass();
		}
	}
}