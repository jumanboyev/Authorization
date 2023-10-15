using Authorization.Desktop.Constans;
using System;

namespace Authorization.Desktop.Helpers;

public class TimeHelper
{
    public static DateTime GetDateTime()
    {
        var dtTime = DateTime.UtcNow;

        dtTime.AddHours(TimeConstans.UTC);

        return dtTime;
    }
}
