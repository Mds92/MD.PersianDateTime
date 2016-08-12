﻿using System;
using System.Collections.Generic;
using System.Linq;
using MD.PersianDateTime;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
	internal class Program
	{
		private static void Main()
		{
			Test1();
			Test2();
			Test3();
			Console.ReadKey();
		}

		private static void Test1()
		{
			PersianDateTime persianDateTime = PersianDateTime.Parse("دوشنبه 05 مرداد 1395 ساعت 04:03");
			persianDateTime.EnglishNumber = true;
			string serializedPersianDateTime = JsonConvert.SerializeObject(persianDateTime);

			var persianDateTime2 = PersianDateTime.Parse(13901229);
			var dateTIme1 = persianDateTime2.ToDateTime();
			Console.WriteLine(dateTIme1.Equals(persianDateTime2));

			PersianDateTime persianDateTime1 = JsonConvert.DeserializeObject<PersianDateTime>(serializedPersianDateTime);
			Console.WriteLine(persianDateTime1.ToString("yyyy/MM/dd   HH:mm:ss:fff"));

			persianDateTime = PersianDateTime.Parse(13901229);
			persianDateTime.EnglishNumber = true;
			persianDateTime = persianDateTime.AddDays(2);
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("چهارشنبه، ۱۰ دی ۱۳۹۳ ۱۲:۳۸");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("دوشنبه 24 آذر 1393 ساعت 3:59:3 ب.ظ");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("24 آذر 1393");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("د 24 آذر 1393 4:2:5:5 ب.ظ");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("1393/02/01");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("1393/02/01 02:03");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("1393/02/01 02:03:10:30");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("93/1/1 3:15 ب.ظ");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("جمعه 93/2/1 ساعت 3:2 ب.ظ");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = new PersianDateTime(DateTime.Now.AddDays(-8)) { EnglishNumber = true };
			string timeFromNow = persianDateTime.ElapsedTime();
			Console.WriteLine(timeFromNow);

			persianDateTime = PersianDateTime.Today;
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime.GetWeekOfMonth);
			Console.WriteLine(persianDateTime.GetWeekOfYear);

			persianDateTime = PersianDateTime.Now;
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime.Date);

			persianDateTime = new PersianDateTime(1394, 5, 9);
			Console.WriteLine(persianDateTime.Date);

			persianDateTime = new PersianDateTime(1394, 5, 9);
			Console.WriteLine(PersianDateTime.Now.Subtract(persianDateTime));

			// Test IComparable

			Console.WriteLine();
			Console.WriteLine("--------------------");
			Console.WriteLine();

			List<PersianDateTime> persianDateTimes = new List<PersianDateTime>();
			for (int i = 0; i < 5; i++)
			{
				persianDateTime = new PersianDateTime(DateTime.Now)
				{
					EnglishNumber = true
				}.AddDays(i).AddMinutes(i);
				persianDateTimes.Add(persianDateTime);
			}
			persianDateTimes = persianDateTimes.OrderByDescending(q => q).ToList();

			foreach (PersianDateTime item in persianDateTimes)
				Console.WriteLine(item);

		}

		private static void Test2()
		{
			PersianDateTime persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 2000");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 1394");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 94");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 58");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);
		}

		private static void Test3()
		{
			PersianDateTime persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 58");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("سه شنبه, ۲۵ اسفند ۹۴, ۰۹:20:30");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);

			persianDateTime = PersianDateTime.Parse("سه شنبه, ۲۵ اسفند ۹۴ ساعت ۰۹:۰۰");
			persianDateTime.EnglishNumber = true;
			Console.WriteLine(persianDateTime);
		}
	}
}