using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GeneralFunctions : MonoBehaviour
{
    [SerializeField] private RawImage blackScreen;

    private static GeneralFunctions _instance;
    public static GeneralFunctions instance { get => _instance; }

    private void Awake()
    {
        if (instance == null)
            _instance = this;
    }

    public void StopTime() => Time.timeScale = 0;

    public void ResumeTime() => Time.timeScale = 1;

    #region BlackOut
    public static void BlackScreenBoth(float time, Action postBlack)
    {
        TimerManager.manager.AddProgressiveTimer(() =>
        {
            TimerManager.manager.AddProgressiveTimer(null, BlackScreenFadeOut, time);
            postBlack?.Invoke();
        }, BlackScreenFadeIn, time);
    }

    public static void BlackScreen(float time, Action postBlack, bool isInverted = false)
    {
        Action<float> update = (isInverted) ? BlackScreenFadeOut : BlackScreenFadeIn;
        TimerManager.manager.AddProgressiveTimer(postBlack, update, time);
    }

    private static void BlackScreenFadeIn(float percentage)
    {
        Color color = GeneralFunctions.instance.blackScreen.color;
        color.a = 1 - percentage;

        GeneralFunctions.instance.blackScreen.color = color;
    }

    private static void BlackScreenFadeOut(float percentage)
    {
        Color color = GeneralFunctions.instance.blackScreen.color;
        color.a = percentage;

        GeneralFunctions.instance.blackScreen.color = color;
    } 
    #endregion
}
