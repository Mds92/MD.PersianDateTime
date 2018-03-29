# MD.PersianDateTime
A C# library to use PersianCalendar as easy as DateTime
تقویم شمسی و هجری قمری در C#

### Installing:

.Net Framework 2+
```
Install-Package MD.PersianDateTime
```

.Net Core 2+ `deprecated`
```
Install-Package MD.PersianDateTime.Core
```

.Net Standard 2+
```
Install-Package MD.PersianDateTime.Standard
```

In future `MD.PersianDateTime.Core` will be remove. Please use `MD.PersianDateTime.Standard`

### Coding:

Creating new object:
1. new
```C#
var persianDateTime = new PersianDateTime(DateTime.Now);
```

2. Parse
```C#
var persianDateTime1 = PersianDateTime.Parse("دوشنبه 05 مرداد 1395 ساعت 04:03");
var persianDateTime2 = PersianDateTime.Parse(13901229); // تاریخ
var persianDateTime2 = PersianDateTime.Parse(13901229231232102); // تاریخ به همراه زمان تا دقت میلی ثانیه
var persianDateTime3 = PersianDateTime.Parse("چهارشنبه، ۱۰ دی ۱۳۹۳ ۱۲:۳۸");
var persianDateTime4 = PersianDateTime.Parse("24 آذر 1393");
var persianDateTime5 = PersianDateTime.Parse("د 24 آذر 1393 4:2:5:5 ب.ظ");
var persianDateTime6 = PersianDateTime.Parse("1393/02/01");
var persianDateTime7 = PersianDateTime.Parse("1393/02/01 02:03");
var persianDateTime8 = PersianDateTime.Parse("1393-02-01 02:03:10:30");
var persianDateTime9 = PersianDateTime.Parse("93-1-1 3:15 ب.ظ");
var persianDateTime10 = PersianDateTime.Parse("جمعه 93/2/1 ساعت 3:2 ب.ظ");
```

3. Today, Now
```C#
var persianDateTime1 = PersianDateTime.Now;
var persianDateTime2 = PersianDateTime.Today; // without time
```

4. new with persian date
```C#
var persianDateTime = new PersianDateTime(1394, 5, 9);
var persianDateTime = new PersianDateTime(1394, 5, 9, 10, 5, 3);
var persianDateTime = new PersianDateTime(1394, 5, 9, 10, 5, 3, 103);
```
------------------------------------------
### Disable persian chars
```C#
var persianDateTime = PersianDateTime.Parse("چهارشنبه 5 آذر 58");
persianDateTime.EnglishNumber = true;
```
------------------------------------------
### Convert to DateTime
PersianDateTime object can convert automatically to datetime without any redundant code
```C#
DateTime dateTime = PersianDateTime.Now
```
Also you can use `ToDateTime` method
```C#
DateTime datetime = persianDateTime.ToDateTime();
```
------------------------------------------
### Get HijriDate
```C#
var persianDateTimeNow = PersianDateTime.Now;
persianDateTimeNow.EnglishNumber = true;
var hijriDateTime = persianDateTimeNow.ToHijri(-1);
Console.WriteLine($"{hijriDateTime.Year}-{hijriDateTime.Month}-{hijriDateTime.Day}");
```

------------------------------------------
### Formats
you can use the following formats in `ToString` method.
```C#
yyyy: سال چهار رقمی
yy: سال دو رقمی
MMMM: نام فارسی ماه
MM: عدد دو رقمی ماه
M: عدد یک رقمی ماه
dddd: نام فارسی روز هفته
dd: عدد دو رقمی روز ماه
d: عدد یک رقمی روز ماه
HH: ساعت دو رقمی با فرمت 00 تا 24
H: ساعت یک رقمی با فرمت 0 تا 24
hh: ساعت دو رقمی با فرمت 00 تا 12
h: ساعت یک رقمی با فرمت 0 تا 12
mm: عدد دو رقمی دقیقه
m: عدد یک رقمی دقیقه
ss: ثانیه دو رقمی
s: ثانیه یک رقمی
fff: میلی ثانیه 3 رقمی
ff: میلی ثانیه 2 رقمی
f: میلی ثانیه یک رقمی
tt: ب.ظ یا ق.ظ
t: حرف اول از ب.ظ یا ق.ظ
```
------------------------------------------
### Some useful methods
```
IsSqlDateTime \\ Check datetime and return a boolean if it is valid for SQL
IsChristianDate \\ Check if the input string is a christian datetime
ElapsedTime \\ Get past time until now
Add*** \\ add day, month, year, ... to persian datetime
Subtract*** \\ Subtract a datetime from current object
```
------------------------------------------
### Comparing
You can compare two PersianDateTime object together with C# operators
```C#
var persianDateTime1 = new PersianDateTime(1396, 03, 28);
var persianDateTime2 = new PersianDateTime(1396, 03, 29);
var persianDateTime3 = new PersianDateTime(1396, 03, 28);

persianDateTime1 > persianDateTime2; // false
persianDateTime1 < persianDateTime2; // true
persianDateTime1 == persianDateTime3; // true
persianDateTime1 != persianDateTime3; // false
```

------------------------------------------
### Add and Subtract with operator
You can use +  -  operators
```C#
var persianDateTime1 = new PersianDateTime(1396, 03, 28);

persianDateTime1 = persianDateTime1 + new TimeSpan(0, 0, 12); // add 12 minutes
persianDateTime1 = persianDateTime1 - new TimeSpan(0, 0, 1); // subtract 1 minutes

```
