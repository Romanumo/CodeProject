using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class ColorGrading : MonoBehaviour
{
    public Shader colorGradingShader;
    Material colorGrading;
    public Vector4 color;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        colorGrading = new Material(colorGradingShader);
        colorGrading.SetVector("_Color", color);
        Graphics.Blit(source, destination, colorGrading, 0);
    }
}
