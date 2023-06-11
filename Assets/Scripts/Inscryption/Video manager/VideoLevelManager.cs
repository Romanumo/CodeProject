using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoLevelManager : MonoBehaviour
{
	[SerializeField] private float timeToExiting = 1f;

	[SerializeField] private VideoPlayer videoPlayer;
	[SerializeField] private RawImage renderTextureForVideo;

	public void ShowVideo() => StartCoroutine(StartShowingVideo());

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			StartCoroutine(StartExitingToMenu());
	}

	private IEnumerator StartExitingToMenu()
	{
		yield return new WaitForSecondsRealtime(timeToExiting);
		GetComponent<LevelLoader>().LoadMenu();
	}

	private IEnumerator StartShowingVideo()
	{
		yield return new WaitForSecondsRealtime(0.4f);
		videoPlayer.enabled = true;
		renderTextureForVideo.enabled = true;
	}
}
