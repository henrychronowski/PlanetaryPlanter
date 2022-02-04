Shader "Custom/CurvatureTest"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v, out Input o)
        {
            //v.vertex.x += sin(_Time * 30) * .3;

            float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

            //worldPos -= _WorldSpaceCameraPos;

            float curveAmount = distance(worldPos, _WorldSpaceCameraPos);//worldPos.z * worldPos.z;

            //float3 objectPos = 0;
            //objectPos.y -= curveAmount;

            //v.vertex.xyz += objectPos;

            /*curveAmount = max(curveAmount, 10.0f);
            curveAmount -= 10.0f;

            curveAmount = lerp(1.0f, 10.0f, curveAmount * 0.1);*/
            curveAmount = lerp(0.0f, 10.0f, curveAmount / 20);

            v.vertex.y -= curveAmount;

            //worldPos.y *= worldPos.y;

            //v.vertex.y -= worldPos.y;

            UNITY_INITIALIZE_OUTPUT(Input, o);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
