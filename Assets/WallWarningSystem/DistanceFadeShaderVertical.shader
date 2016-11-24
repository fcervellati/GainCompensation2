Shader "Custom/DistanceFadeShaderVertical" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Cutoff ("Cutoff", Float) = 0.5
		_Full("Full", Float) = 0.4
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Transparent"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha:fade 

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
			float3 worldPos;
			float3 worldNormal;
		};
		
		uniform fixed _Cutoff;
		uniform fixed _Full;
		uniform fixed4 _Color;
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			float3 pos2cam = _WorldSpaceCameraPos - IN.worldPos;
			
			o.Albedo = _Color.rgb;

			float dist = length(pos2cam.xz);
			//clip(_Cutoff - dist + _Middlewidth);
			float normDist = dot(pos2cam, IN.worldNormal);
			
			
			clip(_Cutoff - dist);
			

			o.Alpha = abs(_Cutoff - dist) / (_Cutoff - _Full);
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
