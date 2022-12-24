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
			var bigFont = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 18, Font.BOLD);
			var smallFont = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 12, Font.BOLD);
			var title = new Paragraph("Harmonogram spłat", bigFont);
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
				Cell c = new Cell(new Paragraph(header, smallFont));
				c.BackgroundColor = new BaseColor(0xD0, 0xF4, 0xDE);
				c.HorizontalAlignment = Element.ALIGN_CENTER;
				datatable.AddCell(c);
			}

			DateTime currDate = l.ContractDate;

			l.InterestRate /= 100;

			decimal interest = l.InterestRate / l.InstallmentsInYear;

			decimal x = (decimal)Math.Pow((double) (1 + interest), l.FinancingSpan/12*l.InstallmentsInYear);
			decimal monthlyPayment = l.InvestmentValue * interest*x / (x-1);

			monthlyPayment = decimal.Truncate(monthlyPayment * 100) / 100;
			decimal capitalLeftToPay = l.InvestmentValue;

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

				datatable.AddCell((i + 1).ToString());
				datatable.AddCell(currDate.ToString("d"));
				datatable.AddCell(monthlyPayment.ToString("0.00"));
				datatable.AddCell((decimal.Truncate(localCapital*100)/100).ToString("0.00"));
				datatable.AddCell((decimal.Truncate(localInterest*100)/100).ToString("0.00"));
				currDate = currDate.AddMonths(12/l.InstallmentsInYear);
			}

			pdf.Add(datatable);
			pdf.Close();

			return Task.FromResult(ms.ToArray());
		}
	}
}