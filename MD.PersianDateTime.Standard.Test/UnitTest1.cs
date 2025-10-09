using System;
using System.Globalization;
using Xunit;

namespace MD.PersianDateTime.Standard.Test;

public class UnitTest1
{
    [Fact]
    public void GetPersianDateOfLastDayOfMonth1()
    {
        var date = new PersianDateTime(1399, 5, 1);
        Assert.Equal(new PersianDateTime(date.Year, date.Month, date.GetMonthDays), date.GetPersianDateOfLastDayOfMonth());
    }

    [Fact]
    public void GetPersianDateOfLastDayOfMonth2()
    {
        var date = new PersianDateTime(1399, 10, 1);
        Assert.Equal(new PersianDateTime(date.Year, date.Month, date.GetMonthDays), date.GetPersianDateOfLastDayOfMonth());
    }

    [Fact]
    public void GetPersianDateOfLastDayOfMonth3()
    {
        var date = new PersianDateTime(1399, 12, 1);
        Assert.Equal(new PersianDateTime(date.Year, date.Month, date.GetMonthDays), date.GetPersianDateOfLastDayOfMonth());
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

    [Fact]
    public void ToTimeShort1()
    {
        var date = new PersianDateTime(1399, 5, 1, 12, 20, 33);
        Assert.Equal(1220, date.ToTimeShort());
    }

    [Fact]
    public void ToTimeShort2()
    {
        var date = new PersianDateTime(1402, 3, 22, 9, 8, 5);
        Assert.Equal(908, date.ToTimeShort());
    }

    [Fact]
    public void ToTimeShort3()
    {
        var date = new PersianDateTime(1402, 3, 22, 9, 22, 15);
        Assert.Equal(922, date.ToTimeShort());
    }

    [Fact]
    [UseCulture("fa-IR")]
    public void TestPersianCulture1()
    {
        var persianDate = new PersianDateTime(1404, 07, 20);
        var result = persianDate.ToString("yyyy");
        Assert.Contains("1404", result);
    }

    [Fact]
    [UseCulture("fa-IR")]
    public void TestPersianCulture2()
    {
        var persianDate = new PersianDateTime(1404, 07, 20, 1, 1, 1, 123);
        var result = persianDate.ToString("fff");
        Assert.Contains("123", result);
    }

    [Fact]
    [UseCulture("fa-IR")]
    public void TestPersianCulture3()
    {
        var persianDate = new PersianDateTime(new DateTime(2025, 10, 09));
        Assert.Equal(1404, persianDate.Year);
        Assert.Equal(7, persianDate.Month);
        Assert.Equal(17, persianDate.Day);
    }

    [Theory]
    [InlineData("fr-CA")]
    [InlineData("fa-IR")]
    [InlineData("en-US")]
    [InlineData("ar-SA")]
    [InlineData("ar-LB")]
    public void TestPersianCulture4(string cultureName)
    {
        using (new CultureScope(cultureName))
        {
            var dateTime = DateTime.Parse("2025-10-09", CultureInfo.InvariantCulture);
            var persianDate = new PersianDateTime(dateTime);
            Assert.Equal(1404, persianDate.Year);
            Assert.Equal(7, persianDate.Month);
            Assert.Equal(17, persianDate.Day);
        }
    }

    [Theory]
    [InlineData("fr-CA")]
    [InlineData("fa-IR")]
    [InlineData("en-US")]
    [InlineData("ar-SA")]
    [InlineData("ar-LB")]
    public void TestPersianCulture5(string cultureName)
    {
        using (new CultureScope(cultureName))
        {
            var dateTime = DateTime.Parse("2025-10-09", CultureInfo.InvariantCulture);
            Assert.Equal(2025, dateTime.Year);
            Assert.Equal(10, dateTime.Month);
            Assert.Equal(09, dateTime.Day);
        }
    }

}