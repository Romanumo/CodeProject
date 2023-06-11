using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class OptionQuiz : MonoBehaviour
{
	[SerializeField][HideInInspector] private TMP_Text textOfOption;

	public void InitializeOptionOfQuiz(ToggleGroup toggleGroup) => GetComponent<Toggle>().group = toggleGroup;

	public void SetText(string text) => textOfOption.text = text;
}
