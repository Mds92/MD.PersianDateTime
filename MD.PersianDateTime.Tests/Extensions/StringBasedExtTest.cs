using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD.PersianDateTime.Extensions;
using NUnit.Framework;

namespace MD.PersianDateTime.Tests.Extensions
{
    [TestFixture]
    public class StringBasedExtTest
    {
        [Test]
        public void ConvertDigitsToLatin()
        {
            char zeroInPersian = '\u06f0';  //0 in persian
            char zeroInArabic  = '\u0660';  //0 in arabic
            char zeroInEnglish = '\u0030';  //0 in english
            
            string sampleContainDigit = string.Format("English{0}-Persian{1}-Arabic{2}", zeroInEnglish, zeroInPersian,zeroInArabic);
            string empty = string.Empty;
            
            string result1 = sampleContainDigit.ConvertDigitsToLatin();
            string result2 = empty.ConvertDigitsToLatin();
            
            Assert.AreEqual("English0-Persian0-Arabic0",result1);
            Assert.IsEmpty(result2);
        }
    }
}
