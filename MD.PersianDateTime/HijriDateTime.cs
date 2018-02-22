namespace MD.PersianDateTime
{
	/// <summary>
	/// تاریخ هجری قمری
	/// </summary>
	public struct HijriDateTime
	{
	    /// <summary>
	    /// سال قمری
	    /// </summary>
	    public int Year { get; set; }

        /// <summary>
        /// ماه قمری
        /// </summary>
	    public int Month { get; set; }

        /// <summary>
        /// نام ماه قمری
        /// </summary>
	    public string MonthName { get; set; }

        /// <summary>
        /// روز ماه قمری
        /// </summary>
	    public int Day { get; set; }
    }
}
