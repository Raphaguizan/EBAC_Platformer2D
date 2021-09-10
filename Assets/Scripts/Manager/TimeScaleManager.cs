using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class TimeScaleManager : Singleton<TimeScaleManager>
{
    private float _currentTimeScale = 1;

    public static float GetCurrentTimeScale() => Instance._currentTimeScale;

    public static void SetTimeScale(float t)
    {
        Instance._currentTimeScale = t;
        Time.timeScale = t;
    }
}
