using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class NumToWord
	{
		 string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

		 string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

		public  string NumberToWords(long number, string cultureCode)
		{
			string words = "";
			if (cultureCode == "en-US") words = ConvertToWordsEnUS(number);
			if (cultureCode == "da-DK") words = ConvertToWordsDaDK(number);
			if (cultureCode == "en-GB") words = ConvertToWordsEnGB(number);
			if (cultureCode == "ja-JP") words = ConvertToWordsJaJP(number);
			if (cultureCode == "en-IN") words = ConvertToWordsEnIN(number);
			return words.Trim();
		}

		public  string ConvertToWordsEnUS(long number)
		{
			CultureInfo cultureInfo = new CultureInfo("en-US");
			if (number == 0)
				return cultureInfo.TextInfo.ToTitleCase("zero");

			if (number < 0)
				return "minus " + ConvertToWords(Math.Abs(number), cultureInfo);

			string words = "";

			long billion = number / 1000000000;
			if (billion > 0)
			{
				words += ConvertToWords(billion, cultureInfo) + " billion ";
				number %= 1000000000;
			}

			long million = number / 1000000;
			if (million > 0)
			{
				words += ConvertToWords(million, cultureInfo) + " million ";
				number %= 1000000;
			}

			long thousand = number / 1000;
			if (thousand > 0)
			{
				words += ConvertToWords(thousand, cultureInfo) + " thousand ";
				number %= 1000;
			}

			long hundred = number / 100;
			if (hundred > 0)
			{
				words += ConvertToWords(hundred, cultureInfo) + " hundred ";
				number %= 100;
			}

			if (number > 0)
			{
				if (words != "")
					words += "and ";

				if (number < 20)
					words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number]);
				else
				{
					words += cultureInfo.TextInfo.ToTitleCase(tensMap[number / 10]);
					if ((number % 10) > 0)
						words += "-" + cultureInfo.TextInfo.ToTitleCase(unitsMap[number % 10]);
				}
			}

			return words.Trim();
		}

		public  string ConvertToWordsDaDK(long number)
		{
			CultureInfo cultureInfo = new CultureInfo("da-DK");
			if (number == 0)
				return cultureInfo.TextInfo.ToTitleCase("nul");

			if (number < 0)
				return "minus " + ConvertToWords(Math.Abs(number), cultureInfo);

			string words = "";

			long milliard = number / 1000000000;
			if (milliard > 0)
			{
				words += ConvertToWords(milliard, cultureInfo) + " milliard ";
				number %= 1000000000;
			}

			long million = number / 1000000;
			if (million > 0)
			{
				words += ConvertToWords(million, cultureInfo) + " million ";
				number %= 1000000;
			}

			long thousand = number / 1000;
			if (thousand > 0)
			{
				words += ConvertToWords(thousand, cultureInfo) + " tusind ";
				number %= 1000;
			}

			long hundred = number / 100;
			if (hundred > 0)
			{
				words += ConvertToWords(hundred, cultureInfo) + " hundrede ";
				number %= 100;
			}

			if (number > 0)
			{
				if (words != "")
					words += "og ";

				if (number < 20)
					words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number]);
				else
				{
					words += cultureInfo.TextInfo.ToTitleCase(tensMap[number / 10]);
					if ((number % 10) > 0)
						words += "og " + cultureInfo.TextInfo.ToTitleCase(unitsMap[number % 10]);
				}
			}

			return words.Trim();
		}

		public  string ConvertToWordsEnGB(long number)
		{
			CultureInfo cultureInfo = new CultureInfo("en-GB");
			if (number == 0)
				return cultureInfo.TextInfo.ToTitleCase("zero");

			if (number < 0)
				return "minus " + ConvertToWords(Math.Abs(number), cultureInfo);

			string words = "";

			long billion = number / 1000000000;
			if (billion > 0)
			{
				words += ConvertToWords(billion, cultureInfo) + " billion ";
				number %= 1000000000;
			}

			long million = number / 1000000;
			if (million > 0)
			{
				words += ConvertToWords(million, cultureInfo) + " million ";
				number %= 1000000;
			}

			long thousand = number / 1000;
			if (thousand > 0)
			{
				words += ConvertToWords(thousand, cultureInfo) + " thousand ";
				number %= 1000;
			}

			long hundred = number / 100;
			if (hundred > 0)
			{
				words += ConvertToWords(hundred, cultureInfo) + " hundred ";
				number %= 100;
			}

			if (number > 0)
			{
				if (words != "")
					words += "and ";

				if (number < 20)
					words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number]);
				else
				{
					words += cultureInfo.TextInfo.ToTitleCase(tensMap[number / 10]);
					if ((number % 10) > 0)
						words += "-" + cultureInfo.TextInfo.ToTitleCase(unitsMap[number % 10]);
				}
			}

			return words.Trim();
		}

		public  string ConvertToWordsJaJP(long number)
		{
			CultureInfo cultureInfo = new CultureInfo("ja-JP");
			if (number == 0)
				return cultureInfo.TextInfo.ToTitleCase("ゼロ");

			if (number < 0)
				return "マイナス " + ConvertToWords(Math.Abs(number), cultureInfo);

			string words = "";

			long oku = number / 100000000;  // 1 億 = 100 million
			if (oku > 0)
			{
				words += ConvertToWords(oku, cultureInfo) + " 億 ";
				number %= 100000000;
			}

			long man = number / 10000;  // 1 万 = 10,000
			if (man > 0)
			{
				words += ConvertToWords(man, cultureInfo) + " 万 ";
				number %= 10000;
			}

			long sen = number / 1000;
			if (sen > 0)
			{
				words += ConvertToWords(sen, cultureInfo) + " 千 ";
				number %= 1000;
			}

			long hyaku = number / 100;
			if (hyaku > 0)
			{
				words += ConvertToWords(hyaku, cultureInfo) + " 百 ";
				number %= 100;
			}

			if (number > 0)
			{
				if (words != "")
					words += "and ";

				if (number < 20)
					words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number]);
				else
				{
					words += cultureInfo.TextInfo.ToTitleCase(tensMap[number / 10]);
					if ((number % 10) > 0)
						words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number % 10]);
				}
			}

			return words.Trim();
		}

		public  string ConvertToWordsEnIN(long number)
		{
			CultureInfo cultureInfo = new CultureInfo("en-IN");
			if (number == 0)
				return cultureInfo.TextInfo.ToTitleCase("zero");

			if (number < 0)
				return "minus " + ConvertToWords(Math.Abs(number), cultureInfo);

			string words = "";

			long crore = number / 10000000;  // 1 crore = 10 million
			if (crore > 0)
			{
				words += ConvertToWords(crore, cultureInfo) + " crore ";
				number %= 10000000;
			}

			long lakh = number / 100000;  // 1 lakh = 100,000
			if (lakh > 0)
			{
				words += ConvertToWords(lakh, cultureInfo) + " lakh ";
				number %= 100000;
			}

			long thousand = number / 1000;
			if (thousand > 0)
			{
				words += ConvertToWords(thousand, cultureInfo) + " thousand ";
				number %= 1000;
			}

			long hundred = number / 100;
			if (hundred > 0)
			{
				words += ConvertToWords(hundred, cultureInfo) + " hundred ";
				number %= 100;
			}

			if (number > 0)
			{
				if (words != "")
					words += "and ";
			
				if (number < 20)
					words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number]);
				else
				{
					words += cultureInfo.TextInfo.ToTitleCase(tensMap[number / 10]);
					if ((number % 10) > 0)
						words += "-" + cultureInfo.TextInfo.ToTitleCase(unitsMap[number % 10]);
				}
			}

			return words.Trim();
		}

		public string ConvertToWords(long number, CultureInfo cultureInfo)
		{
			string words = "";

			if (number > 0)
			{
				if (words != "")
					words += "and ";

				if (number < 20)
					words += cultureInfo.TextInfo.ToTitleCase(unitsMap[number]);
				else
				{
					words += cultureInfo.TextInfo.ToTitleCase(tensMap[number / 10]);
					if ((number % 10) > 0)
						words += "-" + cultureInfo.TextInfo.ToTitleCase(unitsMap[number % 10]);
				}
			}

			return words.Trim();
		}
	}
}
