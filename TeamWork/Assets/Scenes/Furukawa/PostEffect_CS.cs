using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect_CS : MonoBehaviour
{
    public Shader PostShader;
    private Material PostMaterial;
    private Camera cam;

    private void Awake()
    {
        PostMaterial = new Material(PostShader);
        cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.DepthNormals;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, PostMaterial);
    }
}
