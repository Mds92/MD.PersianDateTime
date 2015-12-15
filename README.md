# MD.PersianDateTime
A C# library to use PersianCalendar as easy as DateTime
###Install-Package MD.PersianDateTime
This Library can parse some persian datetime like the followings:
```
13930914
"1393/09/14 12:20:30"
"93/1/1 3:15 ب.ظ"
"1393/02/01 02:03:10:30"
"1393/09/14 12:20:30:300"
"1393/09/14 12:20 ب.ظ"
"1393/09/14"
"د 24 آذر 1393 4:2:5:5 ب.ظ"
"24 آذر 1393"
"جمعه 93/2/1 ساعت 3:2 ب.ظ"
"جمعه 14 آذر 1393 ساعت 11:50:30 ب.ظ"
"جمعه 14 آذر 1393 ساعت 16:50:30"`
```
You can use this class directly instead of DateTime
```
static void Main(string[] args)
{
	PersianDateTime persianDateTime = PersianDateTime.Parse("1393/09/15 12:20:30");
	PrintDateTime(persianDateTime);
	Console.ReadKey();
}

static void PrintDateTime(DateTime dateTime)
{
	Console.WriteLine(dateTime);
}
```
You can change all numbers to Persian Numbers by setting EnglishNumber property to false(default is false)
Some outputs :
```
/// <summary>
/// 1393/09/14   13:49:40
/// </summary>
public override string ToString()

/// <summary>
/// 1393/09/14
/// </summary>
public string ToShortDateString()

/// <summary>
/// 13930914
/// It's greate to save in sql if you want to save persian date in database
/// </summary>
public int ToShortDateInt()

/// <summary>
/// ج 14 آذر 93
/// </summary>
public string ToShortDate1String()

/// <summary>
/// جمعه، 14 آذر 1393
/// </summary>
public string ToLongDateString()

/// <summary>
/// جمعه، 14 آذر 1393 ساعت 13:50:27
/// </summary>
public string ToLongDateTimeString()

/// <summary>
/// جمعه، 14 آذر 1393 13:50
/// </summary>
public string ToShortDateTimeString()

/// <summary>
/// 01:50 ب.ظ
/// </summary>
public string ToShortTimeString()

/// <summary>
/// 01:50:20 ب.ظ
/// </summary>
public string ToLongTimeString()

/// <summary>
/// Supported format
/// yyyy: سال چهار رقمی
/// yy: سال دو رقمی
/// MMMM: نام فارسی ماه
/// MM: عدد دو رقمی ماه
/// M: عدد یک رقمی ماه
/// dddd: نام فارسی روز هفته
/// dd: عدد دو رقمی روز ماه
/// d: عدد یک رقمی روز ماه
/// HH: ساعت دو رقمی با فرمت 00 تا 24
/// H: ساعت یک رقمی با فرمت 0 تا 24
/// hh: ساعت دو رقمی با فرمت 00 تا 12
/// h: ساعت یک رقمی با فرمت 0 تا 12
/// mm: عدد دو رقمی دقیقه
/// m: عدد یک رقمی دقیقه
/// ss: ثانیه دو رقمی
/// s: ثانیه یک رقمی
/// fff: میلی ثانیه 3 رقمی
/// ff: میلی ثانیه 2 رقمی
/// f: میلی ثانیه یک رقمی
/// tt: ب.ظ یا ق.ظ
/// t: حرف اول از ب.ظ یا ق.ظ
/// </summary>
public string ToString(string format)

/// <summary>
/// نمایش زمان سپری شده از زمان حال 
/// مثال: 2 دقیقه قبل 
/// </summary>
public string ElapsedTime()

/// <summary>
/// نمایش فقط تاریخ
/// مثال: 2014/04/13
/// </summary>
public DateTime Date
```
All methods or properties have description.
Also, There is a demo project that you can see how to use this library.

