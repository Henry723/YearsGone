Shader "Custom/ItemShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _LightPoint("Light point position", Vector) = (0, 0, 0, 0)
        _LightIntesity("Light Intensity", Float) = 1.0
        _Color1("Color1", Color) = (1, 1, 1, 1)
        _Color2("Color2", Color) = (1, 1, 1, 1)
        _Color3("Color3", Color) = (1, 1, 1, 1)
        _Color4("Color4", Color) = (1, 1, 1, 1)
    }
        
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPosition : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightPoint;
            float _LightIntensity;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float4 _Color4;

            v2f vert(appdata v)
            {
                v2f o;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 lightDifference = i.worldPosition - _LightPoint.xyz;
                fixed3 lightDirection = normalize(lightDifference);
                fixed intensity = -0.5 * dot(lightDirection, i.worldNormal) + 0.5f;
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 color = col * intensity;

                float lum = 0.3 * color.r + 0.59 * color.g + 0.11 * color.b;


                if (lum > 0.81) color = color * _Color1;
                else if (lum > 0.74) color = color * ((_Color1 + _Color2) / 2);
                else if (lum > 0.55) color = color * _Color2;
                else if (lum > 0.48) color = color * ((_Color2 + _Color3) / 2);
                else if (lum > 0.29) color = color * _Color3;
                else if (lum > 0.22) color = color * ((_Color3 + _Color4) / 2);
                else color = color * _Color4;


                color = color + (_LightIntensity / distance(i.worldPosition, _LightPoint));
                return color;
            }
            ENDCG
        }
    }
}
