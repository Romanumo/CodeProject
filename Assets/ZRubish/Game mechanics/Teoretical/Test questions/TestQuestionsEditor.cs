using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class TestQuestionsEditor : MonoBehaviour
{
	[SerializeField][TextArea(3, 7)][Space] private string question;
	[SerializeField][Space(20)] List<AnswerType> answers = new List<AnswerType>();

	[SerializeField][HideInInspector][Space(20)] private TMP_Text questionText;
	[SerializeField][HideInInspector] private OptionQuiz prefabOptionQuiz;
	[SerializeField][HideInInspector] private Transform layoutForQuizes;
	[SerializeField][HideInInspector] private ToggleGroup toggleGroupOfOptions;

	[SerializeField][HideInInspector] private List<OptionQuiz> instantiatedOptionsOfQuizForEditing = new List<OptionQuiz>();

	private void Update()
	{
		if (instantiatedOptionsOfQuizForEditing.Count < answers.Count)
		{
			OptionQuiz instantiatedOptionQuiz = Instantiate(prefabOptionQuiz);
			instantiatedOptionQuiz.transform.SetParent(layoutForQuizes);
			instantiatedOptionsOfQuizForEditing.Add(instantiatedOptionQuiz);
			instantiatedOptionQuiz.InitializeOptionOfQuiz(toggleGroupOfOptions);

			instantiatedOptionQuiz.name = answers.Count.ToString();
		}
		else if (instantiatedOptionsOfQuizForEditing.Count > answers.Count)
		{
			DestroyImmediate(instantiatedOptionsOfQuizForEditing[instantiatedOptionsOfQuizForEditing.Count - 1].gameObject);
			instantiatedOptionsOfQuizForEditing.RemoveAt(instantiatedOptionsOfQuizForEditing.Count - 1);
		}

		if (!Application.isPlaying)
			questionText.text = question;

		for (int i = 0; i < instantiatedOptionsOfQuizForEditing.Count; i++)
		{
			instantiatedOptionsOfQuizForEditing[i].SetText(answers[i].answer);
			if (answers[i].isCorrect)
				GetComponent<TestQuestionsManager>().SetCorrectChoosingAnswer(i + 1);
		}
	}

	[System.Serializable]
	private struct AnswerType
	{
		[TextArea(3, 7)]
		[Space]
		public string answer;
		public bool isCorrect;
	}
}