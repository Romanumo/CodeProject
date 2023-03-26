using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private List<WinCondition> winConditions;
    [SerializeField] private TextMeshProUGUI conditionsText;

    private Player player;
    private bool hasWon = false;
    private bool hasLost = false;

    private static WinConditionManager _instance;
    public static WinConditionManager instance { get => _instance; }

    private void Awake()
    {
        if (instance == null)
            _instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        conditionsText.text = "Win Conditions: \n";
        foreach (WinCondition condition in winConditions)
        {
            conditionsText.text += "- " + condition.ToObjective() + "\n";
        }
    }

    private void Update()
    {
        if (hasWon)
            return;

        bool isConditionsMet = true;
        foreach (WinCondition condition in winConditions)
        {
            if (!condition.Check())
                isConditionsMet = false;
        }

        if (isConditionsMet)
            Win();
    }

    public void Lose()
    {
        if (hasLost)
            return;

        player.ChangeMovement(false);
        GeneralFunctions.BlackScreen(1f, GameRestart);
        hasLost = true;
    }

    private void Win()
    {
        GeneralFunctions.BlackScreen(1f, NextLevel);
        hasWon = true;
    }

    private void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
