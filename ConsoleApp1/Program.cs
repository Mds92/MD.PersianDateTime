using System;
using MD.PersianDateTime.Core;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var persianDateTime = PersianDateTime.Now;
            persianDateTime.EnglishNumber = true;
            Console.WriteLine(persianDateTime.ToString());
            Console.ReadKey();
        }
    }
}
