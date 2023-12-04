using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect_SC : MonoBehaviour
{
    public Shader PostShader;
    private Material PostMaterial;

    private void Awake()
    {
        PostMaterial = new Material(PostShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, PostMaterial);
    }
}
