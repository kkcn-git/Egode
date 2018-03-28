using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Egode
{
	public class PdfParser
	{
		private PdfReader _reader;
	
		public PdfParser(string filename)
		{
			if (System.IO.File.Exists(filename))
				_reader = new PdfReader(filename);
		}
		
		public void Close()
		{
			if (null == _reader)
				return;
			_reader.Close();
		}
		
		// Get all text contained in the pdf.
		public string GetText()
		{
			if (null == _reader)
				return string.Empty;
		
			ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
			StringBuilder sb = new StringBuilder();
			for (int page = 0; page < _reader.NumberOfPages; page++)
				sb.Append(PdfTextExtractor.GetTextFromPage(_reader, page + 1, strategy));
			return sb.ToString();
		}
	}
}