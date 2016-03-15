using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MD.PersianDateTime.Tests
{
    [TestFixture]
    class PersianDateTimeTest
    {
        /// <summary>
        /// Unit Test for parse method
        /// </summary>
        /// <example>input date contain year 58 -> Should Return 1358</example>
        /// <example>input date contain year 05 -> Should Return 1305</example>
        
        [Test]
        public void Parse_PositiveTwoDigitYearLessThan100As1stParameter_ShouldReturn13xx()
        {
            for (int i = 0; i < 100; i++)
            {
                string date = "چهارشنبه 5 آذر" + " " + i.ToString("00");
                Assert.AreEqual(1300 + i, PersianDateTime.Parse(date).Year);
            }
        }
    }
}
