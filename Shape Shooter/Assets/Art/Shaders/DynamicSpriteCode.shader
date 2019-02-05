Shader "Unlit/DynamicSpriteCode"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Direction("Direction", Vector) = (0.707, 0.707, 0, 0)
		_Speed("Speed", Range(0, 1)) = 0
		_MinMax("MinMax", Vector) = (0, 1, 0, 0)
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;
				float4 _Direction;
				float _Speed;
				float2 _MinMax;

				float map(float s, float a1, float a2, float b1, float b2)
				{
					return b1 + (s - a1)*(b2 - b1) / (a2 - a1);
				}

				v2f vert(appdata_t IN)
				{
					v2f OUT;

					float4 v = IN.vertex;

					float dist = length(v - _Direction);
					float remapedDist = map(dist, _MinMax.x, _MinMax.y, 0, 1);
					float4 targetPos = lerp(v, _Direction, remapedDist);

					v = lerp(v, targetPos, _Speed);

					OUT.vertex = UnityObjectToClipPos(v);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;

					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
					if (_AlphaSplitEnabled)
						color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					return color;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
					c.rgb *= c.a;
					return c;
				}
			ENDCG
			}
		}
}
