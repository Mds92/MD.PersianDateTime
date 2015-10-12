using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MD.PersianDateTime
{
	/// <summary>
	/// Created By Mohammad Dayyan, mds_soft@yahoo.com
	/// 1393/09/14
	/// </summary>
	[Serializable]
	public struct PersianDateTime : 
		ISerializable, 
		IComparable<PersianDateTime>, IComparable<DateTime>,
		IEquatable<PersianDateTime>, IEquatable<DateTime>
	{
		#region properties and fields

		static PersianCalendar _persianCalendar;
		static PersianCalendar PersianCalendar
		{
			get
			{
				if (_persianCalendar != null) return _persianCalendar;
				_persianCalendar = new PersianCalendar();
				return _persianCalendar;
			}
		}
		readonly DateTime _dateTime;

		/// <summary>
		/// آیا اعداد در خروجی به صورت انگلیسی نمایش داده شوند؟
		/// </summary>
		public bool EnglishNumber { get; set; }

		/// <summary>
		/// سال شمسی
		/// </summary>
		public int Year
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return DateTime.MinValue.Year;
				return PersianCalendar.GetYear(_dateTime);
			}
		}

		/// <summary>
		/// ماه شمسی
		/// </summary>
		public int Month
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return DateTime.MinValue.Month;
				return PersianCalendar.GetMonth(_dateTime);
			}
		}

		/// <summary>
		/// نام فارسی ماه
		/// </summary>
		public string MonthName
		{
			get { return ((PersianDateTimeMonthEnum)Month).ToString(); }
		}

		/// <summary>
		/// روز ماه
		/// </summary>
		public int Day
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return DateTime.MinValue.Day;
				return PersianCalendar.GetDayOfMonth(_dateTime);
			}
		}

		/// <summary>
		/// روز هفته
		/// </summary>
		public DayOfWeek DayOfWeek
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return DateTime.MinValue.DayOfWeek;
				return PersianCalendar.GetDayOfWeek(_dateTime);
			}
		}

		/// <summary>
		/// روز هفته یا ایندکس شمسی
		/// <para />
		/// شنبه دارای ایندکس صفر است
		/// </summary>
		public PersianDayOfWeek PersianDayOfWeek
		{
			get
			{
				DayOfWeek dayOfWeek = PersianCalendar.GetDayOfWeek(_dateTime);
				PersianDayOfWeek persianDayOfWeek;
				switch (dayOfWeek)
				{
					case DayOfWeek.Sunday:
						persianDayOfWeek = PersianDayOfWeek.Sunday;
						break;
					case DayOfWeek.Monday:
						persianDayOfWeek = PersianDayOfWeek.Monday;
						break;
					case DayOfWeek.Tuesday:
						persianDayOfWeek = PersianDayOfWeek.Tuesday;
						break;
					case DayOfWeek.Wednesday:
						persianDayOfWeek = PersianDayOfWeek.Wednesday;
						break;
					case DayOfWeek.Thursday:
						persianDayOfWeek = PersianDayOfWeek.Thursday;
						break;
					case DayOfWeek.Friday:
						persianDayOfWeek = PersianDayOfWeek.Friday;
						break;
					case DayOfWeek.Saturday:
						persianDayOfWeek = PersianDayOfWeek.Saturday;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				return persianDayOfWeek;
			}
		}

		/// <summary>
		/// ساعت
		/// </summary>
		public int Hour
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 12;
				return PersianCalendar.GetHour(_dateTime);
			}
		}

		/// <summary>
		/// ساعت دو رقمی
		/// </summary>
		public int ShortHour
		{
			get
			{
				int shortHour;
				if (Hour > 12)
					shortHour = Hour - 12;
				else
					shortHour = Hour;
				return shortHour;
			}
		}

		/// <summary>
		/// دقیقه
		/// </summary>
		public int Minute
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 0;
				return PersianCalendar.GetMinute(_dateTime);
			}
		}

		/// <summary>
		/// ثانیه
		/// </summary>
		public int Second
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 0;
				return PersianCalendar.GetSecond(_dateTime);
			}
		}

		/// <summary>
		/// میلی ثانیه
		/// </summary>
		public int MiliSecond
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 0;
				return (int)PersianCalendar.GetMilliseconds(_dateTime);
			}
		}

		/// <summary>
		/// تعداد روز در ماه
		/// </summary>
		public int GetMonthDays
		{
			get
			{
				int days;
				switch (this.Month)
				{
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
						days = 31;
						break;

					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
						days = 30;
						break;

					case 12:
						{
							if (IsLeapYear) days = 30;
							else days = 29;
							break;
						}

					default:
						throw new Exception("Month number is wrong !!!");
				}
				return days;
			}
		}

		/// <summary>
		/// هفته چندم سال
		/// </summary>
		public int GetWeekOfYear
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 0;
				return PersianCalendar.GetWeekOfYear(_dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Saturday);
			}
		}

		/// <summary>
		/// هفته چندم ماه
		/// </summary>
		public int GetWeekOfMonth
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 0;
				PersianDateTime persianDateTime = this.AddDays(1 - this.Day);
				return this.GetWeekOfYear - persianDateTime.GetWeekOfYear + 1;
			}
		}

		/// <summary>
		/// روز چندم ماه
		/// </summary>
		public int GetDayOfYear
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return 0;
				return PersianCalendar.GetDayOfYear(_dateTime);
			}
		}

		/// <summary>
		/// آیا سال کبیسه است؟
		/// </summary>
		public bool IsLeapYear
		{
			get
			{
				if (_dateTime <= DateTime.MinValue) return false;
				return PersianCalendar.IsLeapYear(this.Year);
			}
		}

		/// <summary>
		/// قبل از ظهر، بعد از ظهر
		/// </summary>
		private AmPmEnum PersianAmPm
		{
			get
			{
				return _dateTime.ToString("tt") == "PM" ? AmPmEnum.PM : AmPmEnum.AM;
			}
		}

		/// <summary>
		/// قبل از ظهر، بعد از ظهر به شکل مخفف . کوتاه
		/// </summary>
		public string GetPersianAmPm
		{
			get
			{
				string result = string.Empty;
				switch (PersianAmPm)
				{
					case AmPmEnum.AM:
						result = "ق.ظ";
						break;

					case AmPmEnum.PM:
						result = "ب.ظ";
						break;
				}
				return result;
			}
		}

		/// <summary>
		/// نام کامل ماه
		/// </summary>
		public string GetLongMonthName
		{
			get
			{
				string monthName = null;
				switch (Month)
				{
					case 1:
						monthName = "فروردین";
						break;

					case 2:
						monthName = "اردیبهشت";
						break;

					case 3:
						monthName = "خرداد";
						break;

					case 4:
						monthName = "تیر";
						break;

					case 5:
						monthName = "مرداد";
						break;

					case 6:
						monthName = "شهریور";
						break;

					case 7:
						monthName = "مهر";
						break;

					case 8:
						monthName = "آبان";
						break;

					case 9:
						monthName = "آذر";
						break;

					case 10:
						monthName = "دی";
						break;

					case 11:
						monthName = "بهمن";
						break;

					case 12:
						monthName = "اسفند";
						break;
				}

				return monthName;
			}
		}

		/// <summary>
		/// سال دو رقمی
		/// </summary>
		public int GetShortYear
		{
			get { return Year % 100; }
		}

		/// <summary>
		/// نام کامل روز
		/// </summary>
		public string GetLongDayOfWeekName
		{
			get
			{
				string weekDayName = null;
				switch (DayOfWeek)
				{
					case DayOfWeek.Sunday:
						weekDayName = PersianWeekDaysStruct.یکشنبه.Value;
						break;

					case DayOfWeek.Monday:
						weekDayName = PersianWeekDaysStruct.دوشنبه.Value;
						break;

					case DayOfWeek.Tuesday:
						weekDayName = PersianWeekDaysStruct.سه_شنبه.Value;
						break;

					case DayOfWeek.Wednesday:
						weekDayName = PersianWeekDaysStruct.چهارشنبه.Value;
						break;

					case DayOfWeek.Thursday:
						weekDayName = PersianWeekDaysStruct.پنجشنبه.Value;
						break;

					case DayOfWeek.Friday:
						weekDayName = PersianWeekDaysStruct.جمعه.Value;
						break;

					case DayOfWeek.Saturday:
						weekDayName = PersianWeekDaysStruct.شنبه.Value;
						break;
				}
				return weekDayName;
			}
		}

		/// <summary>
		/// نام یک حرفی روز، حرف اول روز
		/// </summary>
		public string GetShortDayOfWeekName
		{
			get
			{
				string weekDayName = null;
				switch (DayOfWeek)
				{
					case DayOfWeek.Sunday:
						weekDayName = "ی";
						break;

					case DayOfWeek.Monday:
						weekDayName = "د";
						break;

					case DayOfWeek.Tuesday:
						weekDayName = "س";
						break;

					case DayOfWeek.Wednesday:
						weekDayName = "چ";
						break;

					case DayOfWeek.Thursday:
						weekDayName = "پ";
						break;

					case DayOfWeek.Friday:
						weekDayName = "ج";
						break;

					case DayOfWeek.Saturday:
						weekDayName = "ش";
						break;
				}

				return weekDayName;
			}
		}

		/// <summary>
		/// تاریخ و زمان همین الان
		/// </summary>
		public static PersianDateTime Now
		{
			get
			{
				return new PersianDateTime(DateTime.Now);
			}
		}

		/// <summary>
		/// تاریخ امروز
		/// </summary>
		public static PersianDateTime Today
		{
			get
			{
				return new PersianDateTime(DateTime.Today);
			}
		}

		/// <summary>
		/// زمان به فرمتی مشابه 
		/// <para />
		/// 13:47:40:530
		/// </summary>
		public string TimeOfDay
		{
			get
			{
				//if (_dateTime <= DateTime.MinValue) return null;
				string result = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", Hour, Minute, Second, MiliSecond);
				if (EnglishNumber) return result;
				return ToPersianNumber(result);
			}
		}

		/// <summary>
		/// زمان به فرمتی مشابه زیر 
		/// <para />
		/// ساعت 01:47:40:530 ب.ظ
		/// </summary>
		public string LongTimeOfDay
		{
			get
			{
				//if (_dateTime <= DateTime.MinValue) return null;
				string result = string.Format("ساعت {0:00}:{1:00}:{2:00}:{3:000} {4}", ShortHour, Minute, Second, MiliSecond, GetPersianAmPm);
				if (EnglishNumber) return result;
				return ToPersianNumber(result);
			}
		}

		/// <summary>
		/// زمان به فرمتی مشابه زیر
		/// <para />
		/// 01:47:40 ب.ظ
		/// </summary>
		public string ShortTimeOfDay
		{
			get
			{
				//if (_dateTime <= DateTime.MinValue) return null;
				string result = string.Format("{0:00}:{1:00}:{2:00} {3}", ShortHour, Minute, Second, GetPersianAmPm);
				if (EnglishNumber) return result;
				return ToPersianNumber(result);
			}
		}

		/// <summary>
		/// تاریخ بدون احتساب زمان
		/// </summary>
		public PersianDateTime Date
		{
			get
			{
				return new PersianDateTime(_dateTime)
				{
					EnglishNumber = this.EnglishNumber
				};
			}
		}

		/// <summary>
		/// حداقل مقدار
		/// </summary>
		public static PersianDateTime MinValue
		{
			get { return new PersianDateTime(DateTime.MinValue); }
		}

		/// <summary>
		/// حداکثر مقدار
		/// </summary>
		public static PersianDateTime MaxValue
		{
			get
			{
				return new PersianDateTime(DateTime.MaxValue);
			}
		}

		#endregion

		#region ctor

		/// <summary>
		/// متد سازنده برای دی سریالایز شدن
		/// </summary>
		private PersianDateTime(SerializationInfo info, StreamingContext context) : this()
		{
			_dateTime = info.GetDateTime("DateTime");
			EnglishNumber = info.GetBoolean("EnglishNumber");
		}

		/// <summary>
		/// مقدار دهی اولیه با استفاده از دیت تایم میلادی
		/// </summary>
		/// <param name="dateTime">DateTime</param>
		/// <param name="englishNumber">آیا اعداد در خروجی های این آبجکت به صورت انگلیسی نمایش داده شوند یا فارسی؟</param>
		private PersianDateTime(DateTime dateTime, bool englishNumber)
			: this()
		{
			_dateTime = dateTime;
			EnglishNumber = englishNumber;
		}

		/// <summary>
		/// مقدار دهی اولیه با استفاده از دیت تایم میلادی
		/// </summary>
		public PersianDateTime(DateTime dateTime)
			: this()
		{
			_dateTime = dateTime;
		}

		/// <summary>
		/// مقدار دهی اولیه با استفاده از دیت تایم قابل نال میلادی
		/// </summary>
		public PersianDateTime(DateTime? nullableDateTime)
			: this()
		{
			if (!nullableDateTime.HasValue)
			{
				_dateTime = DateTime.MinValue;
				return;
			}
			_dateTime = nullableDateTime.Value;
		}

		/// <summary>
		/// مقدار دهی اولیه
		/// </summary>
		/// <param name="persianYear">سال شمسی</param>
		/// <param name="persianMonth">ماه شمسی</param>
		/// <param name="persianDay">روز شمسی</param>
		public PersianDateTime(int persianYear, int persianMonth, int persianDay)
			: this()
		{
			_dateTime = PersianCalendar.ToDateTime(persianYear, persianMonth, persianDay, 0, 0, 0, 0);
		}

		/// <summary>
		/// مقدار دهی اولیه
		/// </summary>
		/// <param name="persianYear">سال شمسی</param>
		/// <param name="persianMonth">ماه شمسی</param>
		/// <param name="persianDay">روز شمسی</param>
		/// <param name="hour">ساعت</param>
		/// <param name="minute">دقیقه</param>
		/// <param name="second">ثانیه</param>
		public PersianDateTime(int persianYear, int persianMonth, int persianDay, int hour, int minute, int second)
			: this()
		{
			_dateTime = PersianCalendar.ToDateTime(persianYear, persianMonth, persianDay, hour, minute, second, 0);
		}

		/// <summary>
		/// مقدار دهی اولیه
		/// </summary>
		/// <param name="persianYear">سال شمسی</param>
		/// <param name="persianMonth">ماه شمسی</param>
		/// <param name="persianDay">روز شمسی</param>
		/// <param name="hour">سال</param>
		/// <param name="minute">دقیقه</param>
		/// <param name="second">ثانیه</param>
		/// <param name="miliSecond">میلی ثانیه</param>
		public PersianDateTime(int persianYear, int persianMonth, int persianDay, int hour, int minute, int second, int miliSecond)
			: this()
		{
			_dateTime = PersianCalendar.ToDateTime(persianYear, persianMonth, persianDay, hour, minute, second, miliSecond);
		}

		#endregion

		#region Types

		enum AmPmEnum
		{
			AM = 0,
			PM = 1,
			None = 2,
		}

		enum PersianDateTimeMonthEnum
		{
			فروردین = 1,
			اردیبهشت = 2,
			خرداد = 3,
			تیر = 4,
			مرداد = 5,
			شهریور = 6,
			مهر = 7,
			آبان = 8,
			آذر = 9,
			دی = 10,
			بهمن = 11,
			اسفند = 12,
		}

		struct PersianWeekDaysStruct
		{
			public static KeyValuePair<int, string> شنبه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Saturday, "شنبه"); }
			}

			public static KeyValuePair<int, string> یکشنبه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Sunday, "یکشنبه"); }
			}

			public static KeyValuePair<int, string> دوشنبه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Monday, "دوشنبه"); }
			}

			public static KeyValuePair<int, string> سه_شنبه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Tuesday, "سه شنبه"); }
			}

			public static KeyValuePair<int, string> چهارشنبه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Thursday, "چهارشنبه"); }
			}

			public static KeyValuePair<int, string> پنجشنبه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Wednesday, "پنج شنبه"); }
			}

			public static KeyValuePair<int, string> جمعه
			{
				get { return new KeyValuePair<int, string>((int)DayOfWeek.Friday, "جمعه"); }
			}
		}

		#endregion

		#region override

		/// <summary>
		/// تبدیل تاریخ به رشته با فرمت مشابه زیر
		/// <para />
		/// 1393/09/14   13:49:40
		/// </summary>
		public override string ToString()
		{
			//if (_dateTime <= DateTime.MinValue) return string.Empty;
			string result = string.Format("{0:0000}/{1:00}/{2:00}   {3:00}:{4:00}:{5:00}", Year, Month, Day, Hour, Minute, Second);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PersianDateTime)) return false;
			PersianDateTime persianDateTime = (PersianDateTime)obj;
			return _dateTime == persianDateTime.ToDateTime();
		}

		public override int GetHashCode()
		{
			return _dateTime.GetHashCode();
		}

		/// <summary>
		/// مقایسه با تاریخ دیگر
		/// </summary>
		/// <returns>مقدار بازگشتی همانند مقدار بازگشتی متد کامپیر در دیت تایم دات نت است</returns>
		public int CompareTo(PersianDateTime otherPersianDateTime)
		{
			return this._dateTime.CompareTo(otherPersianDateTime.ToDateTime());
		}

		/// <summary>
		/// مقایسه با تاریخ دیگر
		/// </summary>
		/// <returns>مقدار بازگشتی همانند مقدار بازگشتی متد کامپیر در دیت تایم دات نت است</returns>
		public int CompareTo(DateTime otherDateTime)
		{
			return this._dateTime.CompareTo(otherDateTime);
		}

		#region operators

		/// <summary>
		/// تبدیل خودکار بع دیت تایم میلادی
		/// </summary>
		public static implicit operator DateTime(PersianDateTime persiandateTime)
		{
			return persiandateTime.ToDateTime();
		}

		/// <summary>
		/// اپراتور برابر
		/// </summary>
		public static bool operator ==(PersianDateTime persianDateTime1, PersianDateTime persianDateTime2)
		{
			return persianDateTime1.Equals(persianDateTime2);
		}

		/// <summary>
		/// اپراتور نامساوی
		/// </summary>
		public static bool operator !=(PersianDateTime persianDateTime1, PersianDateTime persianDateTime2)
		{
			return !persianDateTime1.Equals(persianDateTime2);
		}

		/// <summary>
		/// اپراتور بزرگتری
		/// </summary>
		public static bool operator >(PersianDateTime persianDateTime1, PersianDateTime persianDateTime2)
		{
			return persianDateTime1.ToDateTime() > persianDateTime2.ToDateTime();
		}

		/// <summary>
		/// اپراتور کوچکتری
		/// </summary>
		public static bool operator <(PersianDateTime persianDateTime1, PersianDateTime persianDateTime2)
		{
			return persianDateTime1.ToDateTime() < persianDateTime2.ToDateTime();
		}

		/// <summary>
		/// اپراتور بزرگتر مساوی
		/// </summary>
		public static bool operator >=(PersianDateTime persianDateTime1, PersianDateTime persianDateTime2)
		{
			return persianDateTime1.ToDateTime() >= persianDateTime2.ToDateTime();
		}

		/// <summary>
		/// اپراتور کوچکتر مساوی
		/// </summary>
		public static bool operator <=(PersianDateTime persianDateTime1, PersianDateTime persianDateTime2)
		{
			return persianDateTime1.ToDateTime() <= persianDateTime2.ToDateTime();
		}

		/// <summary>
		/// اپراتور جمع تو زمان
		/// </summary>
		public static PersianDateTime operator +(PersianDateTime persiandateTime1, TimeSpan timeSpanToAdd)
		{
			DateTime dateTime1 = persiandateTime1;
			return new PersianDateTime(dateTime1.Add(timeSpanToAdd));
		}

		/// <summary>
		/// اپراتور کم کردن دو زمان از هم
		/// </summary>
		public static TimeSpan operator -(PersianDateTime persiandateTime1, PersianDateTime persiandateTime2)
		{
			DateTime dateTime1 = persiandateTime1;
			DateTime dateTime2 = persiandateTime2;
			return dateTime1 - dateTime2;
		}

		#endregion

		#endregion

		#region ISerializable

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DateTime", ToDateTime());
			info.AddValue("EnglishNumber", EnglishNumber);
		}

		#endregion

		#region IComparable

		public bool Equals(PersianDateTime other)
		{
			return this == other;
		}

		public bool Equals(DateTime other)
		{
			return this == other;
		}

		#endregion

		#region methods

		/// <summary>
		/// تاریخ شروع ماه رمضان 
		/// <para />
		/// چون ممکن است در یک سال شمسی دو شروع ماه رمضان داشته باشیم 
		/// <para />
		/// مقدار بازگشتی آرایه است که حداکثر دو آیتم دارد 
		/// </summary>
		public PersianDateTime[] GetStartDayOfRamadan(int hijriAdjustment)
		{
			List<PersianDateTime> result = new List<PersianDateTime>();
			HijriCalendar hijriCalendar = new HijriCalendar { HijriAdjustment = hijriAdjustment };

			int currentHijriYear = hijriCalendar.GetYear(_dateTime);

			PersianDateTime startDayOfRamadan1 = new PersianDateTime(hijriCalendar.ToDateTime(currentHijriYear, 9, 1, 0, 0, 0, 0));
			result.Add(startDayOfRamadan1);

			PersianDateTime startDayOfRamadan2 = new PersianDateTime(hijriCalendar.ToDateTime(++currentHijriYear, 9, 1, 0, 0, 0, 0));
			if (startDayOfRamadan1.Year == startDayOfRamadan2.Year)
				result.Add(startDayOfRamadan2);
			
			return result.ToArray();
		}

		/// <summary>
		/// پارس کردن رشته و تبدیل به نوع PersianDateTime
		/// </summary>
		/// <param name="persianDateTimeInString">متنی که باید پارس شود</param>
		/// <param name="dateSeperatorPattern">کاراکتری که جدا کننده تاریخ ها است</param>
		public static PersianDateTime Parse(string persianDateTimeInString, string dateSeperatorPattern = @"\/|-")
		{
			string month = "", year, day,
				hour = "0",
				minute = "0",
				second = "0",
				miliSecond = "0";

			AmPmEnum amPmEnum = AmPmEnum.None;

			string dateSeperatorPatternString = string.Format(@"{0}", dateSeperatorPattern);
			bool containMonthSeperator = Regex.IsMatch(persianDateTimeInString, dateSeperatorPatternString);

			persianDateTimeInString = ToEnglishNumber(persianDateTimeInString.Replace("&nbsp;", " ").Replace(" ", "-").Replace("\\", "-"));
			persianDateTimeInString = Regex.Replace(persianDateTimeInString, dateSeperatorPatternString, "-");
			persianDateTimeInString = persianDateTimeInString.Replace("ك", "ک").Replace("ي", "ی");

			persianDateTimeInString = string.Format("-{0}-", persianDateTimeInString);

			// بدست آوردن ب.ظ یا ق.ظ
			if (persianDateTimeInString.Contains("ق.ظ"))
				amPmEnum = AmPmEnum.AM;
			else if (persianDateTimeInString.Contains("ب.ظ"))
				amPmEnum = AmPmEnum.PM;

			if (persianDateTimeInString.Contains(":")) // رشته ورودی شامل ساعت نیز هست
			{
				persianDateTimeInString = Regex.Replace(persianDateTimeInString, @"-*:-*", ":");
				hour = Regex.Match(persianDateTimeInString, @"(?<=-)\d{1,2}(?=:)", RegexOptions.IgnoreCase).Value;
				minute = Regex.Match(persianDateTimeInString, @"(?<=-\d{1,2}:)\d{1,2}(?=:?)", RegexOptions.IgnoreCase).Value;
				if (persianDateTimeInString.IndexOf(':') != persianDateTimeInString.LastIndexOf(':'))
				{
					second = Regex.Match(persianDateTimeInString, @"(?<=-\d{1,2}:\d{1,2}:)\d{1,2}(?=(\d{1,2})?)", RegexOptions.IgnoreCase).Value;
					miliSecond = Regex.Match(persianDateTimeInString, @"(?<=-\d{1,2}:\d{1,2}:\d{1,2}:)\d{1,4}(?=(\d{1,2})?)", RegexOptions.IgnoreCase).Value;
					if (string.IsNullOrEmpty(miliSecond)) miliSecond = "0";
				}
			}

			if (containMonthSeperator)
			{
				// بدست آوردن ماه
				month = Regex.Match(persianDateTimeInString, @"(?<=\d{2,4}-)\d{1,2}(?=-\d{1,2}[^:])", RegexOptions.IgnoreCase).Value;

				// بدست آوردن روز
				day = Regex.Match(persianDateTimeInString, @"(?<=\d{2,4}-\d{1,2}-)\d{1,2}(?=-)", RegexOptions.IgnoreCase).Value;

				// بدست آوردن سال
				year = Regex.Match(persianDateTimeInString, @"(?<=-)\d{2,4}(?=-\d{1,2}-\d{1,2})", RegexOptions.IgnoreCase).Value;
			}
			else
			{
				foreach (PersianDateTimeMonthEnum item in Enum.GetValues(typeof(PersianDateTimeMonthEnum)))
				{
					string itemValueInString = item.ToString();
					if (!persianDateTimeInString.Contains(itemValueInString)) continue;
					month = ((int)item).ToString();
					break;
				}

				if (string.IsNullOrEmpty(month))
					throw new Exception("عدد یا حرف ماه در رشته ورودی وجود ندارد");

				// بدست آوردن روز
				day = Regex.Match(persianDateTimeInString, @"(?<=-)\d{1,2}(?=-)", RegexOptions.IgnoreCase).Value;

				// بدست آوردن سال
				if (Regex.IsMatch(persianDateTimeInString, @"(?<=-)\d{4}(?=-)"))
					year = Regex.Match(persianDateTimeInString, @"(?<=-)\d{4}(?=-)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace).Value;
				else
					year = Regex.Match(persianDateTimeInString, @"(?<=-)\d{2,4}(?=-)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace).Value;
			}

			if (year.Length <= 2 && year[0] == '9') year = string.Format("13{0}", year);
			else if (year.Length <= 2) year = string.Format("14{0}", year);

			int numericYear = int.Parse(year);
			int numericMonth = int.Parse(month);
			int numericDay = int.Parse(day);
			int numericHour = int.Parse(hour);
			int numericMinute = int.Parse(minute);
			int numericSecond = int.Parse(second);
			int numericMiliSecond = int.Parse(miliSecond);

			switch (amPmEnum)
			{
				case AmPmEnum.PM:
					if (numericHour < 12)
						numericHour = numericHour + 12;
					break;
				case AmPmEnum.AM:
				case AmPmEnum.None:
					break;
			}

			return new PersianDateTime(numericYear, numericMonth, numericDay, numericHour, numericMinute, numericSecond, numericMiliSecond);
		}

		/// <summary>
		/// پارس کردن یک رشته برای یافتن تاریخ شمسی
		/// </summary>
		public static bool TryParse(string persianDateTimeInString, out PersianDateTime? result, string dateSeperatorPattern = @"\/|-")
		{
			if (string.IsNullOrEmpty(persianDateTimeInString))
			{
				result = null;
				return false;
			}
			try
			{
				result = Parse(persianDateTimeInString, dateSeperatorPattern);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}

		/// <summary>
		/// پارس کردن عددی در فرمت تاریخ شمسی
		/// <para />
		/// همانند 13920305
		/// </summary>
		public static PersianDateTime Parse(int numericPersianDate)
		{
			if (numericPersianDate.ToString().Length != 8)
				throw new InvalidCastException("Numeric persian date time must have a format like 13920101.");
			int year = numericPersianDate / 10000;
			int day = numericPersianDate % 100;
			int month = (numericPersianDate / 100) % 100;
			return new PersianDateTime(year, month, day);
		}
		/// <summary>
		/// پارس کردن عددی در فرمت تاریخ شمسی
		/// <para />
		/// همانند 13920305
		/// </summary>
		public static bool TryParse(int numericPersianDate, out PersianDateTime? result)
		{
			try
			{
				result = Parse(numericPersianDate);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}

		static readonly List<string> GregorianWeekDayNames = new List<string> { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
		static readonly List<string> GregorianMonthNames = new List<string> { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };
		static readonly List<string> PmAm = new List<string> { "pm", "am" };

		/// <summary>
		/// فرمت های که پشتیبانی می شوند
		/// <para />
		/// yyyy: سال چهار رقمی
		/// <para />
		/// yy: سال دو رقمی
		/// <para />
		/// MMMM: نام فارسی ماه
		/// <para />
		/// MM: عدد دو رقمی ماه
		/// <para />
		/// M: عدد یک رقمی ماه
		/// <para />
		/// dddd: نام فارسی روز هفته
		/// <para />
		/// dd: عدد دو رقمی روز ماه
		/// <para />
		/// d: عدد یک رقمی روز ماه
		/// <para />
		/// HH: ساعت دو رقمی با فرمت 00 تا 24
		/// <para />
		/// H: ساعت یک رقمی با فرمت 0 تا 24
		/// <para />
		/// hh: ساعت دو رقمی با فرمت 00 تا 12
		/// <para />
		/// h: ساعت یک رقمی با فرمت 0 تا 12
		/// <para />
		/// mm: عدد دو رقمی دقیقه
		/// <para />
		/// m: عدد یک رقمی دقیقه
		/// <para />
		/// ss: ثانیه دو رقمی
		/// <para />
		/// s: ثانیه یک رقمی
		/// <para />
		/// fff: میلی ثانیه 3 رقمی
		/// <para />
		/// ff: میلی ثانیه 2 رقمی
		/// <para />
		/// f: میلی ثانیه یک رقمی
		/// <para />
		/// tt: ب.ظ یا ق.ظ
		/// <para />
		/// t: حرف اول از ب.ظ یا ق.ظ
		/// </summary>
		public string ToString(string format)
		{
			//if (_dateTime <= DateTime.MinValue) return null;

			string dateTimeString = format.Trim();

			dateTimeString = dateTimeString.Replace("yyyy", Year.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("yy", GetShortYear.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("MMMM", MonthName);
			dateTimeString = dateTimeString.Replace("MM", Month.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("M", Month.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("dddd", GetLongDayOfWeekName);
			dateTimeString = dateTimeString.Replace("dd", Day.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("d", Day.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("HH", Hour.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("H", Hour.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("hh", ShortHour.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("h", ShortHour.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("mm", Minute.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("m", Minute.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("ss", Second.ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("s", Second.ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("fff", MiliSecond.ToString("000", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("ff", (MiliSecond / 10).ToString("00", CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("f", (MiliSecond / 100).ToString(CultureInfo.InvariantCulture));
			dateTimeString = dateTimeString.Replace("tt", GetPersianAmPm);
			dateTimeString = dateTimeString.Replace("t", GetPersianAmPm[0].ToString(CultureInfo.InvariantCulture));

			if (!EnglishNumber)
				dateTimeString = ToPersianNumber(dateTimeString);

			return dateTimeString;
		}

		/// <summary>
		/// بررسی میکند آیا تاریخ ورودی تاریخ میلادی است یا نه
		/// </summary>
		public static bool IsChristianDate(string inputString)
		{
			inputString = inputString.ToLower();
			bool result;

			foreach (string gregorianWeekDayName in GregorianWeekDayNames)
			{
				result = inputString.Contains(gregorianWeekDayName);
				if (result) return true;
			}

			foreach (string gregorianMonthName in GregorianMonthNames)
			{
				result = inputString.Contains(gregorianMonthName);
				if (result) return true;
			}

			foreach (string item in PmAm)
			{
				result = inputString.Contains(item);
				if (result) return true;
			}

			result = Regex.IsMatch(inputString, @"(1[8-9]|[2-9][0-9])\d{2}", RegexOptions.IgnoreCase);

			return result;
		}

		/// <summary>
		/// بررسی میکند آیا تاریخ ورودی مطابق  تاریخ اس کسو ال سرور می باشد یا نه
		/// </summary>
		public static bool IsSqlDateTime(DateTime dateTime)
		{
			DateTime minSqlDateTimeValue = new DateTime(1753, 1, 1);
			return dateTime >= minSqlDateTimeValue;
		}

		/// <summary>
		/// تبدیل نام ماه شمسی به عدد معادل آن
		/// <para />
		/// به طور مثال آذر را به 9 تبدیل می کند
		/// </summary>
		public int GetMonthEnum(string longMonthName)
		{
			PersianDateTimeMonthEnum monthEnum = (PersianDateTimeMonthEnum)Enum.Parse(typeof(PersianDateTimeMonthEnum), longMonthName);
			return (int)monthEnum;
		}

		/// <summary>
		/// نمایش تاریخ به فرمتی مشابه زیر
		/// <para />
		/// 1393/09/14
		/// </summary>
		public string ToShortDateString()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0:0000}/{1:00}/{2:00}", Year, Month, Day);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش تاریخ به فرمتی مشابه زیر
		/// <para />
		/// ج 14 آذر 93
		/// </summary>
		public string ToShortDate1String()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0} {1:00} {2} {3}", GetShortDayOfWeekName, Day, GetLongMonthName, GetShortYear);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش تاریخ به فرمتی مشابه زیر
		/// <para />
		/// 13930914
		/// </summary>
		public int ToShortDateInt()
		{
			string result = string.Format("{0:0000}{1:00}{2:00}", Year, Month, Day);
			return int.Parse(result);
		}

		/// <summary>
		/// نمایش تاریخ به فرمتی مشابه زیر
		/// <para />
		/// جمعه، 14 آذر 1393
		/// </summary>
		public string ToLongDateString()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0}، {1:00} {2} {3:0000}", GetLongDayOfWeekName, Day, GetLongMonthName, Year);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش تاریخ و زمان به فرمتی مشابه زیر
		/// <para />
		/// جمعه، 14 آذر 1393 ساعت 13:50:27
		/// </summary>
		public string ToLongDateTimeString()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0}، {1:00} {2} {3:0000} ساعت {4:00}:{5:00}:{6:00}", GetLongDayOfWeekName, Day, GetLongMonthName, Year, Hour, Minute, Second);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش تاریخ و زمان به فرمتی مشابه زیر
		/// <para />
		/// جمعه، 14 آذر 1393 13:50
		/// </summary>
		public string ToShortDateTimeString()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0}، {1:00} {2} {3:0000} {4:00}:{5:00}", GetLongDayOfWeekName, Day, GetLongMonthName, Year, Hour, Minute);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش زمان به فرمتی مشابه زیر
		/// <para />
		/// 01:50 ب.ظ
		/// </summary>
		public string ToShortTimeString()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0:00}:{1:00} {2}", ShortHour, Minute, GetPersianAmPm);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش زمان به فرمتی مشابه زیر
		/// <para />
		/// 13:50:20
		/// </summary>
		public string ToLongTimeString()
		{
			//if (_dateTime <= DateTime.MinValue) return null;
			string result = string.Format("{0:00}:{1:00}:{2:00}", Hour, Minute, Second);
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// نمایش زمان به فرمتی مشابه زیر
		/// <para />
		/// 1 دقیقه قبل
		/// </summary>
		public string ElapsedTime()
		{
			//if (_dateTime <= DateTime.MinValue) return null;

			PersianDateTime persianDateTimeNow = new PersianDateTime(DateTime.Now);
			TimeSpan timeSpan = this - persianDateTimeNow;
			if (timeSpan.TotalDays > 90)
				return this.ToShortDateTimeString();

			string result = string.Empty;
			if (timeSpan.TotalDays > 30)
			{
				double month = timeSpan.TotalDays / 30;
				if (month > 0)
					result = string.Format("{0:0} ماه قبل", month);
			}
			else if (timeSpan.TotalDays >= 1)
			{
				result = string.Format("{0:0} روز قبل", timeSpan.TotalDays);
			}
			else if (timeSpan.TotalHours >= 1)
			{
				result = string.Format("{0:0} ساعت قبل", timeSpan.TotalHours);
			}
			else
			{
				double minute = timeSpan.TotalMinutes;
				if (minute <= 1) minute = 1;
				result = string.Format("{0:0} دقیقه قبل", minute);
			}
			if (EnglishNumber) return result;
			return ToPersianNumber(result);
		}

		/// <summary>
		/// تبدیل یه تاریخ میلادی
		/// </summary>
		public DateTime ToDateTime()
		{
			return _dateTime;
		}

		/// <summary>
		/// کم کردن دو تاریخ از هم
		/// </summary>
		public TimeSpan Subtract(PersianDateTime persianDateTime)
		{
			return _dateTime - persianDateTime.ToDateTime();
		}

		/// <summary>
		/// اضافه کردن مدت زمانی به تاریخ
		/// </summary>
		public PersianDateTime Add(TimeSpan timeSpan)
		{
			return new PersianDateTime(_dateTime.Add(timeSpan), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن سال به تاریخ
		/// </summary>
		public PersianDateTime AddYears(int years)
		{
			return new PersianDateTime(PersianCalendar.AddYears(_dateTime, years), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن روز به تاریخ
		/// </summary>
		public PersianDateTime AddDays(int days)
		{
			return new PersianDateTime(PersianCalendar.AddDays(_dateTime, days), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن ماه به تاریخ
		/// </summary>
		public PersianDateTime AddMonths(int months)
		{
			return new PersianDateTime(PersianCalendar.AddMonths(_dateTime, months), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن ساعت به تاریخ
		/// </summary>
		public PersianDateTime AddHours(int hours)
		{
			return new PersianDateTime(_dateTime.AddHours(hours), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن دقیقه به تاریخ
		/// </summary>
		public PersianDateTime AddMinutes(int minuts)
		{
			return new PersianDateTime(_dateTime.AddMinutes(minuts), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن ثانیه به تاریخ
		/// </summary>
		public PersianDateTime AddSeconds(int seconds)
		{
			return new PersianDateTime(_dateTime.AddSeconds(seconds), EnglishNumber);
		}

		/// <summary>
		/// اضافه کردن میلی ثانیه به تاریخ
		/// </summary>
		public PersianDateTime AddMilliseconds(int miliseconds)
		{
			return new PersianDateTime(_dateTime.AddMilliseconds(miliseconds), EnglishNumber);
		}

		static string ToPersianNumber(string input)
		{
			if (string.IsNullOrEmpty(input)) return null;
			input = input.Replace("ي", "ی").Replace("ك", "ک");

			//۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹
			return
				input
					.Replace("0", "۰")
					.Replace("1", "۱")
					.Replace("2", "۲")
					.Replace("3", "۳")
					.Replace("4", "۴")
					.Replace("5", "۵")
					.Replace("6", "۶")
					.Replace("7", "۷")
					.Replace("8", "۸")
					.Replace("9", "۹");
		}
		static string ToEnglishNumber(string input)
		{
			if (string.IsNullOrEmpty(input)) return null;
			input = input.Replace("ي", "ی").Replace("ك", "ک");

			//۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹
			return input
				.Replace(",", "")
				.Replace("۰", "0")
				.Replace("۱", "1")
				.Replace("۲", "2")
				.Replace("۳", "3")
				.Replace("۴", "4")
				.Replace("۵", "5")
				.Replace("۶", "6")
				.Replace("۷", "7")
				.Replace("۸", "8")
				.Replace("۹", "9");
		}

		#endregion
	}
}