Shader "Unlit/Tex2DArray"

{
	Properties
	{
		_MainTex("Texture", 2DArray) = "white" {}
		_Idx("array index", Range(0,126)) = 0
	}
	SubShader
	{

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			UNITY_DECLARE_TEX2DARRAY(_MainTex);

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv * 8.;
				int idx = floor(uv.x) + floor(uv.y)*8;
				uv = frac(uv);

				fixed4 col = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(uv, idx));

				return col;
			}
			ENDCG
		}
	}
}