// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/PortalCreateShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_LeftPortalTex("Texture", 2D) = "white" {}
		_RightPortalTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Lighting Off
		Cull Back//반대편 폴리곤 안그리는거
		ZWrite On//뎁스 버퍼에 작성될지( 불투명한건 on, 반투명인 경우 off)
		ZTest Less

		Fog{ Mode Off }

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
				//float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
				float4 worldPos : TEXCOORD2;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _LeftPortalTex;
			sampler2D _RightPortalTex;
			float4 _LeftPortalCenterPoint;
			float4 _LeftPortalLocalPos;
			float4 _RightPortalCenterPoint;
			float4 _RightPortalLocalPos;
			fixed4 frag(v2f i) : SV_Target
			{
				i.screenPos /= i.screenPos.w;
				fixed4 col;
				_LeftPortalLocalPos = _LeftPortalCenterPoint - i.worldPos;
				_RightPortalLocalPos = _RightPortalCenterPoint - i.worldPos;

				/*if (_PortalLocalPos.x < 0.1 &&_PortalLocalPos.x > -0.1) {
					if (_PortalLocalPos.y < 0.1 &&_PortalLocalPos.y > -0.1) {
						if (_PortalLocalPos.z < 0.1 &&_PortalLocalPos.z > -0.1)
							col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y));
						else
							col = fixed4(0, 0, 255, 0);
					}
					else
						col = fixed4(0, 255, 255, 0);
				}
				else
					col = fixed4(255, 255, 0, 0);
				*/
				if ((_LeftPortalLocalPos.x* _LeftPortalLocalPos.x) +
					(_LeftPortalLocalPos.y* _LeftPortalLocalPos.y) +
					(_LeftPortalLocalPos.z* _LeftPortalLocalPos.z) < 1 ) {
					col = tex2D(_LeftPortalTex,float2(i.screenPos.x, i.screenPos.y));
				}
				else if ((_RightPortalLocalPos.x* _RightPortalLocalPos.x) +
					(_RightPortalLocalPos.y* _RightPortalLocalPos.y) +
					(_RightPortalLocalPos.z* _RightPortalLocalPos.z) < 1) {
					col = tex2D(_RightPortalTex,float2(i.screenPos.x, i.screenPos.y));
				}
				else {
					col = tex2D(_MainTex, i.uv);
				}
				// ****** 포탈 테두리 만들깅 ****** //
				if ((_LeftPortalLocalPos.x* _LeftPortalLocalPos.x) +
					(_LeftPortalLocalPos.y* _LeftPortalLocalPos.y) +
					(_LeftPortalLocalPos.z* _LeftPortalLocalPos.z) > 0.95&&
					(_LeftPortalLocalPos.x* _LeftPortalLocalPos.x) +
					(_LeftPortalLocalPos.y* _LeftPortalLocalPos.y) +
					(_LeftPortalLocalPos.z* _LeftPortalLocalPos.z) < 1) {
					col = fixed4(0, 255, 0, 0);//tex2D(_LeftPortalTex, float2(i.screenPos.x, i.screenPos.y));
				}
				else if ((_RightPortalLocalPos.x* _RightPortalLocalPos.x) +
					(_RightPortalLocalPos.y* _RightPortalLocalPos.y) +
					(_RightPortalLocalPos.z* _RightPortalLocalPos.z) >0.95&&
					(_RightPortalLocalPos.x* _RightPortalLocalPos.x) +
					(_RightPortalLocalPos.y* _RightPortalLocalPos.y) +
					(_RightPortalLocalPos.z* _RightPortalLocalPos.z) < 1) {
					col = fixed4(0, 255, 0, 0);//tex2D(_RightPortalTex, float2(i.screenPos.x, i.screenPos.y));
				}
				// ****** 포탈 테두리 만들깅 ****** //
				return col;
			
			}
			ENDCG
		}
	}
}
