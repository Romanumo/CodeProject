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
<<<<<<< Updated upstream:Assets/Scripts/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
=======
	[SerializeField] private GameObject blocker;
	[SerializeField] private ComputerSounds sounds;
>>>>>>> Stashed changes:Assets/ZRubish/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs

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
<<<<<<< Updated upstream:Assets/Scripts/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
=======
		draggableButtonToNextLevel.canDragButton = false;
		//sounds.IncorrectSound();
>>>>>>> Stashed changes:Assets/ZRubish/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
	}

	private void OnWinLevel()
	{
		Debug.Log("Вы выиграли!");
<<<<<<< Updated upstream:Assets/Scripts/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
=======
<<<<<<< HEAD:Assets/Scripts/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
		draggableButtonToNextLevel.canDragButton = true;
		//sounds.CorrectSound();
		//theory.OpenTheWay();
=======
		sounds.CorrectSound();
		blocker.SetActive(false);
>>>>>>> 9c2c739bac5873bafe988ab78b3d4e4ac723ff58:Assets/ZRubish/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
>>>>>>> Stashed changes:Assets/ZRubish/Game mechanics/Teoretical/Test questions/TestQuestionsManager.cs
	}
}
