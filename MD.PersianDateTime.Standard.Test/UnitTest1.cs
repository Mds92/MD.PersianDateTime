using Xunit;

namespace MD.PersianDateTime.Standard.Test
{
    public class UnitTest1
    {
        [Fact]
        public void GetPersianDateOfLastDayOfMonth()
        {
            var date = new PersianDateTime(1399, 5, 1);
            Assert.Equal(new PersianDateTime(1399, 5, 31), date.GetPersianDateOfLastDayOfMonth());
        }
    }
}
