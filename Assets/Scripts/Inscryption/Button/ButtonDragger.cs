using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonDragger : MonoBehaviour
{
	public bool canDragButton = true;
	[SerializeField] private float speedOfMovingButton = 1f;
	[SerializeField] private float timeOfActivatingAction = 1f;

	[SerializeField] private Button holeButtonToDrag;
	[SerializeField] private AudioClip soundButtonClicked;
	[SerializeField] private AudioClip soundButtonError;
	[SerializeField] private AudioClip soundButtonMoved;

	[SerializeField] private UnityEvent onDraggedButton;

	private AudioSource audioSource;

	private bool flaqIsButtonClicked;

	private void Start()
	{
		holeButtonToDrag.onClick.AddListener(OnButtonHoleClicked);
		audioSource = GetComponent<AudioSource>();
	}

	public void OnButtonClicked()
	{
		if (canDragButton)
		{
			flaqIsButtonClicked = !flaqIsButtonClicked;
			holeButtonToDrag.interactable = flaqIsButtonClicked;
			audioSource.clip = soundButtonClicked;
			audioSource.Play();
		}
		else
		{
			audioSource.clip = soundButtonError;
			audioSource.Play();
		}
	}

	public void OnButtonHoleClicked()
	{
		if (flaqIsButtonClicked)
			StartCoroutine(MakeAnimationOfMovingButtonToHole());

		flaqIsButtonClicked = false;
	}

	private IEnumerator MakeAnimationOfMovingButtonToHole()
	{
		float distanceBetweenButtonAndHole = Mathf.Infinity;
		float linearInterpolationCoefficient = speedOfMovingButton * Time.deltaTime;
		Vector3 startPositionOfButton = transform.position;
		while (distanceBetweenButtonAndHole > 1.0f)
		{
			distanceBetweenButtonAndHole = Vector2.Distance(transform.position, holeButtonToDrag.transform.position);
			transform.position = Vector3.Lerp(startPositionOfButton, holeButtonToDrag.transform.position, linearInterpolationCoefficient);
			linearInterpolationCoefficient += speedOfMovingButton;
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(StartActionAfterDraggingButton());
		audioSource.clip = soundButtonMoved;
		audioSource.Play();
	}

	private IEnumerator StartActionAfterDraggingButton()
	{
		yield return new WaitForSecondsRealtime(timeOfActivatingAction);
		onDraggedButton.Invoke();
	}
}
