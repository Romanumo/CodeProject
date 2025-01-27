using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchBlocksManager : QuizManager
{
	[SerializeField][HideInInspector] private Transform panelForAnswerBlocks;
	[SerializeField][HideInInspector] private Transform panelForSequenceBlocks;
	[SerializeField][HideInInspector] private Button buttonForNextLevel;

	[SerializeField][HideInInspector] private int maxAmountOfCorrectBlocksInSequence;
	[SerializeField][HideInInspector] private List<int> orderNumbersInSequence = new List<int>();

	public void AddBlockToSequence(BlockQuiz blockQuiz)
	{
		orderNumbersInSequence.Add(blockQuiz.GetOrderNumberOfBlock());
		blockQuiz.transform.SetParent(panelForSequenceBlocks);

		if (orderNumbersInSequence.Count > 0)
			buttonForNextLevel.interactable = true;
	}

	public void RemoveBlockFromSequence(BlockQuiz blockQuiz)
	{
		orderNumbersInSequence.Remove(blockQuiz.GetOrderNumberOfBlock());
		blockQuiz.transform.SetParent(panelForAnswerBlocks);

		if (orderNumbersInSequence.Count == 0)
			buttonForNextLevel.interactable = false;
	}

	public void CheckSequenceOfCreatedSequence()
	{
		if (maxAmountOfCorrectBlocksInSequence != orderNumbersInSequence.Count)
		{
			onLose?.Invoke();
			return;
		}
		for (int i = 0; i < maxAmountOfCorrectBlocksInSequence; i++)
		{
			if (orderNumbersInSequence[i] != i + 1)
			{
                onLose?.Invoke();
                return;
			}
		}

		onWin?.Invoke();
	}

	public void SetMaximumNumberOfCorrectBlocks(int number) => maxAmountOfCorrectBlocksInSequence = number;
}
