using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestQuestionsManager : MonoBehaviour
{
	[SerializeField][HideInInspector] private ToggleGroup toggleGroupOfOptions;
	[SerializeField][HideInInspector] private Button buttonToNextLevel;
	[SerializeField][HideInInspector] private int correctOptionOfAnswer;

	private void Update()
	{
		if (toggleGroupOfOptions.AnyTogglesOn())
			buttonToNextLevel.interactable = true;
		else
			buttonToNextLevel.interactable = false;
	}

	public void OnClickButtonToNextLevel()
	{
		Toggle chosenAnswer = toggleGroupOfOptions.GetFirstActiveToggle();
		if (chosenAnswer.name == correctOptionOfAnswer.ToString())
		{
			OnWinLevel();
		}
		else
		{
			OnLoseLevel();
		}
	}

	public void SetCorrectChoosingAnswer(int number) => correctOptionOfAnswer = number;

	private void OnLoseLevel()
	{
		Debug.Log("Неправильно!");
	}

	private void OnWinLevel()
	{
		Debug.Log("Вы выиграли!");
	}
}
