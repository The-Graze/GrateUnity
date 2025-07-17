Shader "Unlit/ScrollingTextureCutoutX" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
        _Speed ("Scroll Speed", Range(0,10)) = 1
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Cutout"}
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _Cutoff;
            float _Speed;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                float2 scrollOffset = float2(_Time.y * _Speed, 0);
                float4 tex = tex2D(_MainTex, i.uv + scrollOffset);
                clip(tex.a - _Cutoff);
                return tex;
            }
            ENDCG
        }
    }
}