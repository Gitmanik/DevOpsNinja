using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KalkulatorKredytowy.Data
{
	public class ScheduleGeneratorService
	{
		public Task<byte[]> GeneratePDF(LoanData l)
		{
			MemoryStream ms = new MemoryStream();
			Document pdf = new Document(PageSize.A4);
			pdf.AddTitle("Harmonogram spłat");
			pdf.AddCreationDate();
			pdf.AddAuthor("Paweł Reich, gitmanik.dev");
			PdfWriter writer = PdfWriter.GetInstance(pdf, ms);
			pdf.Open();

			var title = new Paragraph("Harmonogram spłat", new Font(Font.HELVETICA, 20, Font.BOLD));
			pdf.Add(title);

			Table datatable = new Table(5);
			datatable.Padding = 2;
			datatable.Spacing = 0;

			float[] headerwidths = { 10, 10, 10, 10, 10 };

			datatable.Widths = headerwidths;
			datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

			string[] headers = { "Rata", "Termin płatności", "Wartość raty [PLN]", "Część kapitałowa raty [PLN]", "Część odsetkowa raty [PLN]" };

			foreach (string header in headers)
			{
				Cell c = new Cell(new Paragraph(header, new Font(Font.HELVETICA, 12, Font.BOLD)));
				c.BackgroundColor = new BaseColor(0xD0, 0xF4, 0xDE);
				datatable.AddCell(c);
			}

			DateTime currDate = l.ContractDate;

			l.InterestRate /= 100;

			decimal interest = l.InterestRate / l.InstallmentsInYear;

			Console.WriteLine(interest);

			decimal x = (decimal)Math.Pow((double) (1 + interest), (double)l.FinancingSpan);
			Console.WriteLine(x);
			decimal monthlyPayment = l.InvestmentValue * interest*x / (x-1);
			Console.WriteLine(monthlyPayment);

			monthlyPayment = decimal.Truncate(monthlyPayment * 100) / 100;
			decimal capitalLeftToPay = l.InvestmentValue;
			decimal sum = 0;
			for (int i = 0; i < l.FinancingSpan; i++)
			{
				decimal localInterest = capitalLeftToPay * interest;
				decimal localCapital = monthlyPayment - localInterest;

				capitalLeftToPay -= localCapital;	

				if (i == l.FinancingSpan - 1)
				{
					monthlyPayment += sum - l.InvestmentValue;
				}

				datatable.AddCell((i + 1).ToString());
				datatable.AddCell(currDate.ToString("d"));
				datatable.AddCell(monthlyPayment.ToString());
				datatable.AddCell((decimal.Truncate(localCapital*100)/100).ToString());
				datatable.AddCell((decimal.Truncate(localInterest*100)/100).ToString());
				currDate = currDate.AddMonths(1);
				sum += monthlyPayment;
			}

			pdf.Add(datatable);
			pdf.Close();

			return Task.FromResult(ms.ToArray());
		}
	}
}