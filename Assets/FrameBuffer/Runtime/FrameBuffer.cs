using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace sugi.cc.FrameBuffer
{
    public class FrameBuffer : MonoBehaviour
    {
        [SerializeField] Texture source;
        [SerializeField] int bufferCount = 512;
        [SerializeField] float duration = 1f / 30f;
        [SerializeField] RenderTextureFormat textureFormat = RenderTextureFormat.ARGB32;

        [SerializeField] RenderTexture frameBuffer;
        [SerializeField] TextureEvent onCreateTexture;
        [SerializeField] IntEvent onUpdateIndex;

        public int index { get; private set; }

        public Texture SoutceTexture { set { source = value; Initialize(); } }
        public float fps { get { return 1f / duration; } set { duration = 1f / value; } }

        private void Start()
        {
            if (source != null)
                Initialize();
        }
        void Initialize()
        {
            if (frameBuffer != null)
                if (frameBuffer.width != source.width || frameBuffer.height != source.height || frameBuffer.volumeDepth != bufferCount)
                {
                    frameBuffer.Release();
                    Destroy(frameBuffer);
                    frameBuffer = null;
                }
            if (frameBuffer == null)
            {
                frameBuffer = new RenderTexture(source.width, source.height, 0, textureFormat);
                frameBuffer.dimension = UnityEngine.Rendering.TextureDimension.Tex2DArray;
                frameBuffer.volumeDepth = bufferCount;
                frameBuffer.Create();
                onCreateTexture.Invoke(frameBuffer);
            }

            Record();
        }
        void Record()
        {
            if (source == null) return;
            index %= bufferCount;
            Graphics.Blit(source, frameBuffer, 0, index);
            onUpdateIndex.Invoke(index);
            index++;
            Invoke("Record", duration);
        }

        [System.Serializable]
        public class TextureEvent : UnityEvent<Texture> { }
        [System.Serializable]
        public class IntEvent : UnityEvent<int> { }
    }
}