using System.Collections.Generic;
using UnityEngine;

//https://pastebin.com/nNiFVmrY

public static class Wait
{
    private static readonly Dictionary<float, WaitForSeconds> WaitTimes = new Dictionary<float, WaitForSeconds>();
    private static readonly Dictionary<float, WaitForSecondsRealtime> WaitTimesRealtime = new Dictionary<float, WaitForSecondsRealtime>();

    private static object m_Null = default;

    public static WaitForEndOfFrame ForEndOfFrame { get; } = new  WaitForEndOfFrame();
    public static WaitForFixedUpdate ForFixedUpdate { get; } = new WaitForFixedUpdate();
    public static object ForNextFrame { get; } = null;


    public static WaitForSecondsRealtime ForSecondsRealtime(float seconds) =>
        WaitTimesRealtime.TryGetValue(seconds, out var wait)
            ? wait
            : WaitTimesRealtime[seconds] = new WaitForSecondsRealtime(seconds);
    public static WaitForSeconds ForSeconds(float seconds) => WaitTimes.TryGetValue(seconds, out var wait) ? wait : WaitTimes[seconds] = new WaitForSeconds(seconds);

    public static WaitUntil Until(System.Func<bool> condition) => new WaitUntil(condition);

    public static WaitWhile While(System.Func<bool> condition) => new WaitWhile(condition);

    public static object Null => m_Null;

}