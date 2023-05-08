using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class BlockQuiz : MonoBehaviour
{
	[SerializeField] private TMP_Text textOfBlock;
	[SerializeField] private MatchBlocksManager matchBlocksManager;
	[SerializeField] private LayoutElement layoutElementOfBlock;
	[SerializeField] private int orderNumberInCorrectSequence;

	private bool flaqOfClickedBlock;

	private void Update()
	{
		layoutElementOfBlock.minWidth = textOfBlock.rectTransform.rect.width + 50;
		layoutElementOfBlock.minHeight = textOfBlock.rectTransform.rect.height + 10;
	}

	public void InitializeAndSetAnswerAndOrderOfBlock(MatchBlocksManager matchBlocksManager, string text, int orderNumber)
	{
		this.matchBlocksManager = matchBlocksManager;
		textOfBlock.text = text;
		orderNumberInCorrectSequence = orderNumber;
	}

	public int GetOrderNumberOfBlock() => orderNumberInCorrectSequence;

	public void OnBlockClicked()
	{
		flaqOfClickedBlock = !flaqOfClickedBlock;
		if (flaqOfClickedBlock)
			matchBlocksManager.AddBlockToSequence(this);
		else
			matchBlocksManager.RemoveBlockFromSequence(this);
	}
}
