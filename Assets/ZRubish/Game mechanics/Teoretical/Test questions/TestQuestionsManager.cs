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

	[SerializeField] private GameObject blocker;
	[SerializeField] private ComputerSounds sounds;

	[SerializeField] private ButtonDragger draggableButtonToNextLevel;

	private void Update()
	{
		/*if (toggleGroupOfOptions.AnyTogglesOn())
			buttonToNextLevel.interactable = true;
		else
			buttonToNextLevel.interactable = false;*/
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
		draggableButtonToNextLevel.canDragButton = false;
		//sounds.IncorrectSound();
	}

	private void OnWinLevel()
	{
		Debug.Log("Вы выиграли!");
		draggableButtonToNextLevel.canDragButton = true;
		//sounds.CorrectSound();
		//theory.OpenTheWay();
		sounds.CorrectSound();
		blocker.SetActive(false);
	}
}
