using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameDelay : MonoBehaviour
{
    [SerializeField] int delayCount;
    [SerializeField] RenderTexture output;

    RenderTexture frameBuffer;
    int currentIdx;
    int bufferCount;
    Material mat;

    public void SetBuffer(Texture buffer)
    {
        frameBuffer = (RenderTexture)buffer;
        bufferCount = frameBuffer.volumeDepth;
    }
    public void UpdateCurrentIdx(int index)
    {
        currentIdx = index;
        if (bufferCount <= delayCount)
            delayCount = bufferCount - 1;
        if (delayCount < 0)
            delayCount = 0;

        if (mat == null)
            mat = new Material(Shader.Find("Hidden/FrameDelay"));
        if (frameBuffer == null || output == null)
            return;
        mat.SetInt("_CurrentIdx", currentIdx);
        mat.SetInt("_DelayCount", delayCount);
        Graphics.Blit(frameBuffer, output, mat);
    }
}
