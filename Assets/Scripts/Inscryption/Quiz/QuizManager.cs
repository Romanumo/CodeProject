using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
	[SerializeField] private GameObject[] quizez;

	private int currentIndexOfQuiz = 0;

	public void NextLevel()
	{
		quizez[(currentIndexOfQuiz + 1) % quizez.Length].SetActive(true);
		quizez[currentIndexOfQuiz % quizez.Length].SetActive(false);
		currentIndexOfQuiz++;

		if (currentIndexOfQuiz == quizez.Length)
			StartCoroutine(StartExitingToMainMenu());
	}

	private IEnumerator StartExitingToMainMenu()
	{
		yield return new WaitForSeconds(0.5f);
		GetComponent<LevelLoader>().LoadMenu();
	}
}
