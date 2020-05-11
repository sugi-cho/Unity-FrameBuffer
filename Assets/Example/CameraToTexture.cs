using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraToTexture : MonoBehaviour
{
    public RenderTexture rt;
    public TextureEvent texEvent;

    private void Start()
    {
        if (rt != null)
            texEvent.Invoke(rt);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (rt != null)
            Graphics.Blit(source, rt);
        Graphics.Blit(source, destination);
    }

    [System.Serializable]
    public class TextureEvent : UnityEvent<Texture> { }
}
