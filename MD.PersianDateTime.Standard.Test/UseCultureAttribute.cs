using System;
using System.Globalization;
using System.Reflection;
using Xunit.Sdk;

namespace MD.PersianDateTime.Standard.Test;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class UseCultureAttribute(string cultureName) : BeforeAfterTestAttribute
{
    private readonly CultureInfo _testCulture = new(cultureName);
    private CultureInfo _originalCulture;
    private CultureInfo _originalUiCulture;

    public override void Before(MethodInfo methodUnderTest)
    {
        // ذخیره Culture فعلی
        _originalCulture = CultureInfo.CurrentCulture;
        _originalUiCulture = CultureInfo.CurrentUICulture;

        // تنظیم Culture جدید برای Thread جاری
        CultureInfo.CurrentCulture = _testCulture;
        CultureInfo.CurrentUICulture = _testCulture;
    }

    public override void After(MethodInfo methodUnderTest)
    {
        // بازگردانی به Culture قبلی
        if (_originalCulture != null)
            CultureInfo.CurrentCulture = _originalCulture;
        if (_originalUiCulture != null)
            CultureInfo.CurrentUICulture = _originalUiCulture;
    }
}