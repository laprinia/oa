Shader "Custom/Timeshift"
{
    Properties
    {
        _OutterDiffuse ("Outter Diffuse", 2D) = "white" {}
        _OutterSpecular ("Outter Specular", 2D) = "black" {}

        _InnerDiffuse ("Inner Diffuse", 2D) = "black" {}
        _InnerNormal ("Inner Normal",2D) = "bump" {}
        _InnerEmission("Inner Emission", 2D) = "black" {}
        _InnerMetallic("Inner Metallic",2D) = "black" {}
        _InnerSpecular ("Inner Specular",2D) = "black" {}
        _Specularity ("Specularity", Float) = 1.0

        _BorderColor ("Border Color", Color) = (1,1,1,1)
        _BorderWidth ("Border Width", Float) = 0.5
        [HideInInspector]_LightPosition ("Timeshift Start", Vector) = (0,0,0,0)

        _LightRadius ("Timeshift Radius", Float) = 1.0


    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf ColoredSpecular

        sampler2D _OutterDiffuse;
        sampler2D _OutterSpecular;
        sampler2D _InnerDiffuse;
        sampler2D _InnerNormal;
        sampler2D _InnerEmission;
        sampler2D _InnerMetallic;
        sampler2D _InnerSpecular;

        float _LightRadius;
        float _BorderWidth;

        half _Specularity;
        float4 _LightPosition;
        fixed4 _BorderColor;


        struct TimeshiftSurfaceOutput
        {
            half3 Albedo;
            half3 Normal;
            half3 Emission;
            half Metallic;
            half Smoothness;
            half Specular;
            half3 GlossColor;
            half Alpha;
        };

        struct Input
        {
            float2 uv_OutterDiffuse;
            float2 uv_InnerDiffuse;
            float2 uv_InnerNormal;
            float2 uv_InnerMetallic;
            float3 worldPos;
        };


        void surf(Input IN, inout TimeshiftSurfaceOutput o)
        {
            float3 d = (float3)_LightPosition - IN.worldPos;

            float distanceSquared = d.x * d.x + d.z * d.z + d.y * d.y;
            float outterDistanceSq = _LightRadius + _BorderWidth / 2;
            outterDistanceSq *= outterDistanceSq;
            float innerDistanceSq = _LightRadius - _BorderWidth / 2;
            innerDistanceSq *= innerDistanceSq;

            float alpha = saturate((distanceSquared - innerDistanceSq) / (outterDistanceSq - innerDistanceSq));

            float borderAlpha = (int)abs(alpha * 2 - 1);

            half4 outterColor = tex2D(_OutterDiffuse, IN.uv_OutterDiffuse);
            half4 innerColor = tex2D(_InnerDiffuse, IN.uv_InnerDiffuse);
            half4 outterSpecular = tex2D(_OutterSpecular, IN.uv_OutterDiffuse);
            half4 innerSpecular = tex2D(_InnerSpecular, IN.uv_InnerDiffuse);
            half4 innerEmission = tex2D(_InnerEmission, IN.uv_InnerDiffuse);
            half innerMetallic = tex2D(_InnerMetallic, IN.uv_InnerMetallic);
            half3 innerNormal = UnpackNormal(tex2D(_InnerNormal, IN.uv_InnerNormal));

            o.Albedo = lerp(fixed3(0, 0, 0), lerp(innerColor.rgb, outterColor.rgb, alpha), borderAlpha);;
            o.Alpha = lerp(innerColor.a, outterColor.a, alpha);
            o.Metallic = lerp(0, innerMetallic.r, borderAlpha);
            
            o.Specular = lerp(innerSpecular.a, outterSpecular.a, alpha);
            o.Smoothness=1-o.Metallic;
            o.GlossColor = lerp(innerSpecular.rgb, outterSpecular.rgb, alpha) * o.Smoothness;
            o.Emission = lerp((fixed3)_BorderColor, (fixed3)innerEmission, borderAlpha);
            
            o.Normal = lerp(innerNormal.rgb,innerNormal.rgb,alpha);
        }

        inline half4 LightingColoredSpecular(TimeshiftSurfaceOutput o, half3 lightDir, half3 viewDir, half atten)
        {
            half3 H = normalize(lightDir + viewDir);
            half diff = max(0, dot(o.Normal, lightDir));
            float nH = max(0, dot(o.Normal, H));

            float specularFloat = pow(nH, _Specularity) * o.Specular;
            half3 specular = specularFloat * o.GlossColor * o.Metallic;

            half4 color;
            color.rgb = (o.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * specular) * (atten * 2);
            color.a = o.Alpha;

            return color;
        }

        inline half4 LightingColoredSpecular_PrePass(TimeshiftSurfaceOutput o, half4 light)
        {
            half3 specular = light.a * o.GlossColor * o.Metallic;
            half4 color;

            color.rgb = (o.Albedo * light.rgb + light.rgb * specular);
            color.a = o.Alpha + specular * _SpecColor.a;


            return color;
        }
        ENDCG
    }
    FallBack "Diffuse"

}