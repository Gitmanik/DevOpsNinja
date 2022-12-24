using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KalkulatorKredytowy.Data
{
	public class ScheduleGeneratorService
	{
		public Task<byte[]> GeneratePDF(LoanSchedule sch)
		{
			MemoryStream ms = new MemoryStream();
			Document pdf = new Document(PageSize.A4);
			pdf.AddTitle("Harmonogram spłat");
			pdf.AddCreationDate();
			pdf.AddAuthor("Paweł Reich, gitmanik.dev");
			PdfWriter writer = PdfWriter.GetInstance(pdf, ms);
			pdf.Open();
			var bigFont = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 18, Font.BOLD);
			var mediumFont = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 15, Font.BOLD);
			var smallFont = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 12, Font.BOLD);
			pdf.Add(new Paragraph($"Harmonogram spłat \"{sch.Data.LoanName}\"", bigFont));
			pdf.Add(new Paragraph($"Wygenerowano {DateTime.Now} dla {sch.Data.ClientName}", mediumFont));

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

			foreach (var e in sch.Entries)
			{
				datatable.AddCell(e.No.ToString());
				datatable.AddCell(e.Date.ToString("d"));
				datatable.AddCell(e.Payment.ToString("0.00"));
				datatable.AddCell(e.Capital.ToString("0.00"));
				datatable.AddCell(e.Interest.ToString("0.00"));
			}

			pdf.Add(datatable);
			pdf.Close();

			return Task.FromResult(ms.ToArray());
		}
	}
}