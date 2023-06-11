using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	[SerializeField] private string menuToLoad;
	[SerializeField] private float timeOfLoad = 0f;

	public void LoadMenu() => StartCoroutine(LoadingLevel());

	private IEnumerator LoadingLevel()
	{
		yield return new WaitForSecondsRealtime(timeOfLoad);
		SceneManager.LoadScene(menuToLoad);
	}
}
