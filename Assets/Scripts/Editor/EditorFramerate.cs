using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class EditorFramerate : Editor
{
	static EditorFramerate() => EditorApplication.update += Update;

	static void Update()
	{
		if (Application.isPlaying)
			Application.targetFrameRate = 60;
	}
}
