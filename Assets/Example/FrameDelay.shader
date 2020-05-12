Shader "Hidden/FrameDelay"
{
    Properties
    {
        _MainTex ("Texture", 2DArray) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            int _CurrentIdx;
            int _DelayCount;
            UNITY_DECLARE_TEX2DARRAY(_MainTex);

            fixed4 frag(v2f i) : SV_Target
            {
                uint w,h,elements;
                _MainTex.GetDimensions(w, h, elements);
                uint idx = (_CurrentIdx - _DelayCount) % elements;
                fixed4 col = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv, idx));
                return col;
            }
            ENDCG
        }
    }
}
