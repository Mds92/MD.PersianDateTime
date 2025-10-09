using System;
using System.Globalization;

namespace MD.PersianDateTime.Standard.Test;

public sealed class CultureScope : IDisposable
{
    private readonly CultureInfo _originalCulture;
    private readonly CultureInfo _originalUiCulture;

    public CultureScope(string cultureName)
    {
        _originalCulture = CultureInfo.CurrentCulture;
        _originalUiCulture = CultureInfo.CurrentUICulture;

        var newCulture = new CultureInfo(cultureName);
        CultureInfo.CurrentCulture = newCulture;
        CultureInfo.CurrentUICulture = newCulture;
    }

    public void Dispose()
    {
        CultureInfo.CurrentCulture = _originalCulture;
        CultureInfo.CurrentUICulture = _originalUiCulture;
    }
}