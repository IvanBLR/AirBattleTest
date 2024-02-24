Shader "Custom/SandDune"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float) = 1.0
        _Scale ("Scale", Float) = 0.1
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        float _Speed;
        float _Scale;

        void surf(Input IN, inout SurfaceOutput o)
        {
            float angleOffset = 20.0f;
            float verticalOffset = _Time.y * _Speed; // Изменяем вертикальное смещение в зависимости от времени
            float newY = IN.uv_MainTex.y - verticalOffset; // Смещаем текстуру по вертикали
            float noise = tex2D(_MainTex, float2(IN.uv_MainTex.x * _Scale, newY)).r;
            o.Albedo = float3(noise, noise, noise);
        }
        ENDCG
    }

    FallBack "Diffuse"
}
