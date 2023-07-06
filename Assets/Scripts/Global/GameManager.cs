using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum GameState { Player, Computer}
public class GameManager : MonoBehaviour
{
    [SerializeField] private RawImage blackScreen;

    private static GameManager _instance;
    public static GameManager instance { get => _instance; }
    public GameState state { private set; get; }

    private void Awake()
    {
        if (instance == null)
            _instance = this;
    }

    public void ChangeGameState(GameState state)
    {
        this.state = state;

        switch(state)
        {
            case GameState.Computer:
                break;
            case GameState.Player:
                if (Time.timeScale == 0)
                    Time.timeScale = 1;
                break;
        }
    }

    #region BlackScreen
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
        Color color = GameManager.instance.blackScreen.color;
        color.a = 1 - percentage;

        GameManager.instance.blackScreen.color = color;
    }

    private static void BlackScreenFadeOut(float percentage)
    {
        Color color = GameManager.instance.blackScreen.color;
        color.a = percentage;

        GameManager.instance.blackScreen.color = color;
    } 
    #endregion
}
