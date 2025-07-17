Shader "Custom/LineShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(0, 10)) = 1
        _Scale ("Scale", Vector) = (1, 1, 0, 0)
    }
    SubShader {
        Tags { "RenderType"="Cutoff" "Queue"="Transparent" }
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
            float _Speed;
            float2 _Scale;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 scrollOffset = float2(_Time.y * _Speed * -1, 0);
                i.uv.x += _Time.y * _Speed;
                i.uv.x *= _Scale.x;
                i.uv.y *= _Scale.y;

                float2 uv = i.uv;
                float4 tex = tex2D(_MainTex, uv + scrollOffset);
                clip(tex.a - .9);
                return tex;
            }
            ENDCG
        }
    }
}