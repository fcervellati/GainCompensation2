﻿Shader "Custom/DistanceFadeShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Cutoff ("Cutoff", Float) = 0.5
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
		};
		
		uniform fixed _Cutoff;
		uniform fixed4 _Color;
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			o.Albedo = _Color.rgb;
			
			clip (c.a-0.2);
			clip (_Cutoff-IN.screenPos.z);
			o.Alpha = (cos(IN.screenPos.z/_Cutoff*3.14)+1)/2;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
