Shader "Unlit/02_ADS"
{
	Properties
	{
		_Color("Color",Color) = (1,0,0,1)
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _Color;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldPosition : TEXCOORD1;
				float3 normal : NORMAL;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
			{

				float3 eyeDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPosition);
				float3 HarfVec = normalize(_WorldSpaceLightPos0 + eyeDir);
				float intensity = saturate(dot(normalize(i.normal), HarfVec));
				float Powintensity = pow(intensity, 30);

				//Ambient(ˆÃ‚ß‚Ì_Color)
				fixed4 Ambient = _Color * 0.1;

				//Diffuse‚ÌŒvŽZ
				fixed4 Diffuse = fixed4(intensity, intensity, intensity, 1) * _Color;
				
				//Specular‚ÌŒvŽZ
				fixed4 SpecColor = fixed4(1, 1, 1, 1);
				fixed4 Specular = SpecColor * Powintensity;

				fixed4 ADS = Ambient + Diffuse + Specular;
				return ADS;
			}
			ENDCG
		}
	}
}
