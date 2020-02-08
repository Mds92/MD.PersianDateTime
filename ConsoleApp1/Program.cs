using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MD.PersianDateTime.Standard;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main()
        {
            // Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            //Test6();
            //Test7();
            //Test8();
            // Test9();
            Test10();
            Console.ReadKey();
        }

        private static void Test1()
        {
            var persianDateTime = PersianDateTime.Parse("دوشنبه 05 مرداد 1395 ساعت 04:03");
            var serializedPersianDateTime = JsonConvert.SerializeObject(persianDateTime);

            var persianDateTime2 = PersianDateTime.Parse(13901229);
            var dateTIme1 = persianDateTime2.ToDateTime();
            Console.WriteLine(dateTIme1.Equals(persianDateTime2));

            var persianDateTime1 = JsonConvert.DeserializeObject<PersianDateTime>(serializedPersianDateTime);
            Console.WriteLine(persianDateTime1.ToString("yyyy/MM/dd   HH:mm:ss:fff"));

            persianDateTime = PersianDateTime.Parse(13901229);
            persianDateTime = persianDateTime.AddDays(2);
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("چهارشنبه، ۱۰ دی ۱۳۹۳ ۱۲:۳۸");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("دوشنبه 24 آذر 1393 ساعت 3:59:3 ب.ظ");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("24 آذر 1393");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("د 24 آذر 1393 4:2:5:5 ب.ظ");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("1393/02/01");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("1393/02/01 02:03");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("1393/02/01 02:03:10:30");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("93/1/1 3:15 ب.ظ");
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("جمعه 93/2/1 ساعت 3:2 ب.ظ");
            Console.WriteLine(persianDateTime);

            persianDateTime = new PersianDateTime(DateTime.Now.AddDays(-8));
            var timeFromNow = persianDateTime.ElapsedTime();
            Console.WriteLine(timeFromNow);

            persianDateTime = PersianDateTime.Today;
            
            Console.WriteLine(persianDateTime.GetWeekOfMonth);
            Console.WriteLine(persianDateTime.GetWeekOfYear);

            persianDateTime = PersianDateTime.Now;
            
            Console.WriteLine(persianDateTime.Date);

            persianDateTime = new PersianDateTime(1394, 5, 9);
            Console.WriteLine(persianDateTime.Date);

            persianDateTime = new PersianDateTime(1394, 5, 9);
            Console.WriteLine(PersianDateTime.Now.Subtract(persianDateTime));

            // Test IComparable

            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine();

            var persianDateTimes = new List<PersianDateTime>();
            for (var i = 0; i < 5; i++)
            {
                persianDateTime = new PersianDateTime(DateTime.Now).AddDays(i).AddMinutes(i);
                persianDateTimes.Add(persianDateTime);
            }
            persianDateTimes = persianDateTimes.OrderByDescending(q => q).ToList();

            foreach (var item in persianDateTimes)
                Console.WriteLine(item);

        }

        private static void Test2()
        {
            var persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 2000");
            
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 1394");
            
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 94");
            
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 58");
            
            Console.WriteLine(persianDateTime);
        }

        private static void Test3()
        {
            var persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 58");
            
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("سه شنبه, ۲۵ اسفند ۹۴, ۰۹:20:30");
            
            Console.WriteLine(persianDateTime);

            persianDateTime = PersianDateTime.Parse("سه شنبه, ۲۵ اسفند ۹۴ ساعت ۰۹:۰۰");
            
            Console.WriteLine(persianDateTime);
        }

        private static void Test4()
        {
            var persianDateTime = PersianDateTime.Parse("سه شنبه, ۲۵ اسفند ۹۴, 09:20:30");
            
            Console.WriteLine(persianDateTime.ToShortDateInt());
            Console.WriteLine(persianDateTime.ToTimeInt());
        }

        private static void Test5()
        {
            var persianDateTime = PersianDateTime.Now;
            
            var hijriDateTime = persianDateTime.ToHijri(-1);
            var result = $"{hijriDateTime.Day} - {hijriDateTime.MonthName} - {hijriDateTime.Year}";
            Console.WriteLine(result);
        }

        private static void Test6()
        {
            var persianDateTime = PersianDateTime.Now;
            
            var oneYearBeforeDateTime = PersianDateTime.Now.AddMonths(-6);
            Console.WriteLine(oneYearBeforeDateTime.MonthDifference(persianDateTime));
        }

        private static void Test7()
        {
            // IFormattable Test
            var persianDateTime = PersianDateTime.Now;
            
            Console.WriteLine("{0:yyyy-mm-dd}", persianDateTime);
        }

        private static void Test8()
        {
            var longDateTime = PersianDateTime.Now.ToLongDateTimeInt();
            Console.WriteLine(longDateTime);
            var persianDateTime = PersianDateTime.Parse(longDateTime);
            
            Console.WriteLine(persianDateTime.ToString());
        }

        private static void Test9()
        {
            var persianDateTime1 = PersianDateTime.Now;
            var persianDateTime2 = PersianDateTime.Now.AddYears(-1);
            Console.WriteLine(persianDateTime1.GetDifferenceQuarter(persianDateTime2));
        }

        private static void Test10()
        {
            var persianDateTimeNow = PersianDateTime.Now;
            var persianDateTimeNowString = persianDateTimeNow.ToString(CultureInfo.InvariantCulture);
            var persianDateTimeConverted = (PersianDateTime)Convert.ChangeType(persianDateTimeNowString, typeof(DateTime));
            Console.WriteLine(persianDateTimeConverted.ToEpochTime());
        }
    }
}


