using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class MatchBlocksEditor : MonoBehaviour
{
	[SerializeField][TextArea(3, 7)][Space] private string question;
	[SerializeField][Space(20)] List<AnswerType> variationsOfAnswers = new List<AnswerType>();

	[SerializeField][HideInInspector] private TMP_Text questionText;
	[SerializeField][HideInInspector] private BlockQuiz prefabBlockQuiz;
	[SerializeField][HideInInspector] private Transform panelForAnswerBlocks;

	[SerializeField][HideInInspector] private List<BlockQuiz> instantiatedBlocksAnswers = new List<BlockQuiz>();

	private void Update()
	{
		questionText.text = question;
		if (instantiatedBlocksAnswers.Count < variationsOfAnswers.Count)
		{
			BlockQuiz prefab = Instantiate(prefabBlockQuiz);
			prefab.transform.SetParent(panelForAnswerBlocks);
			instantiatedBlocksAnswers.Add(prefab);
		}
		else if (instantiatedBlocksAnswers.Count > variationsOfAnswers.Count)
		{
			DestroyImmediate(instantiatedBlocksAnswers[instantiatedBlocksAnswers.Count - 1].gameObject);
			for (int i = 0; i < instantiatedBlocksAnswers.Count; i++)
			{
				if (instantiatedBlocksAnswers[i] == null)
				{
					instantiatedBlocksAnswers.RemoveAt(i);
				}
			}
		}

		int amountOfCorrectBlocks = 0;
		MatchBlocksManager matchBlocksManager = GetComponent<MatchBlocksManager>();
		for (int i = 0; i < variationsOfAnswers.Count; i++)
		{
			instantiatedBlocksAnswers[i].InitializeAndSetAnswerAndOrderOfBlock(matchBlocksManager, variationsOfAnswers[i].answer, variationsOfAnswers[i].orderNumber);

			if (variationsOfAnswers[i].orderNumber > 0)
				amountOfCorrectBlocks++;
		}
		matchBlocksManager.SetMaximumNumberOfCorrectBlocks(amountOfCorrectBlocks);
	}

	[System.Serializable]
	private struct AnswerType
	{
		[TextArea(3, 7)]
		[Space]
		public string answer;
		public int orderNumber;
	}
}