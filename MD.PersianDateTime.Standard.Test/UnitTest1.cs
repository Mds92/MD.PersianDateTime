using Xunit;

namespace MD.PersianDateTime.Standard.Test;

public class UnitTest1
{
    [Fact]
    public void GetPersianDateOfLastDayOfMonth1()
    {
        var date = new PersianDateTime(1399, 5, 1);
        Assert.Equal(new PersianDateTime(date.Year, date.Month, date.GetMonthDays),
            date.GetPersianDateOfLastDayOfMonth());
    }

    [Fact]
    public void GetPersianDateOfLastDayOfMonth2()
    {
        var date = new PersianDateTime(1399, 10, 1);
        Assert.Equal(new PersianDateTime(date.Year, date.Month, date.GetMonthDays),
            date.GetPersianDateOfLastDayOfMonth());
    }

    [Fact]
    public void GetPersianDateOfLastDayOfMonth3()
    {
        var date = new PersianDateTime(1399, 12, 1);
        Assert.Equal(new PersianDateTime(date.Year, date.Month, date.GetMonthDays),
            date.GetPersianDateOfLastDayOfMonth());
    }

    [Fact]
    public void GetPersianDateOfLastDayOfYear1()
    {
        var date1 = new PersianDateTime(1400, 1, 1);
        var date2 = new PersianDateTime(1400, 12, 29);
        Assert.Equal(date1.GetPersianDateOfLastDayOfYear(), date2);
    }

    [Fact]
    public void GetPersianWeekend()
    {
        var persianDateFriday1 = new PersianDateTime(1401, 7, 22); // جمعه
        var persianDateFriday2 = new PersianDateTime(1401, 7, 29); // جمعه
        var persianDateFriday3 = new PersianDateTime(1401, 8, 6); // جمعه
        var persianDateFriday4 = new PersianDateTime(1401, 8, 13); // جمعه

        Assert.Equal(persianDateFriday1.AddDays(-1).GetPersianWeekend(), persianDateFriday1);
        Assert.Equal(persianDateFriday1.AddDays(-5).GetPersianWeekend(), persianDateFriday1);
        Assert.Equal(persianDateFriday2.AddDays(-4).GetPersianWeekend(), persianDateFriday2);
        Assert.Equal(persianDateFriday2.AddDays(-6).GetPersianWeekend(), persianDateFriday2);
        Assert.Equal(persianDateFriday3.AddDays(-3).GetPersianWeekend(), persianDateFriday3);
        Assert.Equal(persianDateFriday4.AddDays(-6).GetPersianWeekend(), persianDateFriday4);
        Assert.Equal(persianDateFriday4.GetPersianWeekend(), persianDateFriday4);
    }

    [Fact]
    public void GetFirstDayOfWeek()
    {
        var persianDateSaturday1 = new PersianDateTime(1401, 7, 23); // شنبه
        var persianDateSaturday2 = new PersianDateTime(1401, 7, 30); // شنبه
        var persianDateSaturday3 = new PersianDateTime(1401, 8, 7); // شنبه
        var persianDateSaturday4 = new PersianDateTime(1401, 8, 14); // شنبه

        Assert.Equal(persianDateSaturday1.AddDays(1).GetFirstDayOfWeek(), persianDateSaturday1);
        Assert.Equal(persianDateSaturday1.AddDays(5).GetFirstDayOfWeek(), persianDateSaturday1);
        Assert.Equal(persianDateSaturday2.AddDays(4).GetFirstDayOfWeek(), persianDateSaturday2);
        Assert.Equal(persianDateSaturday2.AddDays(6).GetFirstDayOfWeek(), persianDateSaturday2);
        Assert.Equal(persianDateSaturday3.AddDays(3).GetFirstDayOfWeek(), persianDateSaturday3);
        Assert.Equal(persianDateSaturday4.AddDays(6).GetFirstDayOfWeek(), persianDateSaturday4);
        Assert.Equal(persianDateSaturday4.GetFirstDayOfWeek(), persianDateSaturday4);
    }
}