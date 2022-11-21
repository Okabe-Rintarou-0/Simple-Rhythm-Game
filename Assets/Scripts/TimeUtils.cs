using System;
using UnityEngine;

public class TimeUtils
{
    public static long GetTimeStampMilliSecond()
    {
        return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
    }
}
