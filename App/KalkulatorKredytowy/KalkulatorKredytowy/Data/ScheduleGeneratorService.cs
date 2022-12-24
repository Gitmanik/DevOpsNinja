using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KalkulatorKredytowy.Data
{
	public class ScheduleGeneratorService
	{
		public Task<byte[]> GeneratePDF(LoanData loan)
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

			float[] headerwidths = { 6, 10, 10, 16, 16 };

			datatable.Widths = headerwidths;
			datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

			Cell c = new Cell(new Paragraph("Rata nr", new Font(Font.HELVETICA, 12, Font.BOLD)));
			c.BackgroundColor = new BaseColor(0xD0, 0xF4, 0xDE);
			datatable.AddCell(c);
			datatable.AddCell("Termin płatności");
			datatable.AddCell("Wartość raty [PLN]");
			datatable.AddCell("Część kapitałowa raty [PLN]");
			datatable.AddCell("Część odsetkowa raty [PLN]");
			DateTime currDate = loan.ContractDate;

			loan.InterestRate /= 100;

			decimal monthlyPayment = (loan.InvestmentValue * loan.InterestRate) /
				(decimal)(loan.InstallmentsInYear * (1 - Math.Pow((double)(loan.InstallmentsInYear / (loan.InstallmentsInYear + loan.InterestRate)), loan.FinancingSpan * 12 / loan.InstallmentsInYear)));

			for (int i = 0; i < loan.FinancingSpan; i++)
			{
				datatable.AddCell((i + 1).ToString());
				datatable.AddCell(currDate.ToString("d"));
				datatable.AddCell(monthlyPayment.ToString());
				datatable.AddCell("0");
				datatable.AddCell("0");
				currDate = currDate.AddMonths(1);	
			}

			pdf.Add(datatable);
			pdf.Close();

			return Task.FromResult(ms.ToArray());
		}
	}
}