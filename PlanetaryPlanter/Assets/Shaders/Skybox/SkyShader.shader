Shader "Hidden/HDRP/Sky/SkyShader"
{
    HLSLINCLUDE

#pragma vertex Vert

#pragma editor_sync_compilation
#pragma target 4.5
#pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonLighting.hlsl"
#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Sky/SkyUtils.hlsl"

        TEXTURECUBE(_Cubemap);
    SAMPLER(sampler_Cubemap);
   // TEXTURE(_Skymap);

    float4 _SkyParam; // x exposure, y multiplier, zw rotation (cosPhi and sinPhi)

#define _Intensity          _SkyParam.x
#define _CosPhi             _SkyParam.z
#define _SinPhi             _SkyParam.w
#define _CosSinPhi          _SkyParam.zw

    struct Attributes
    {
        uint vertexID : SV_VertexID;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
        output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID, UNITY_RAW_FAR_CLIP_VALUE);
        return output;
    }

    float3 RotationUp(float3 p, float2 cos_sin)
    {
        float3 rotDirX = float3(cos_sin.x, 0, -cos_sin.y);
        float3 rotDirY = float3(cos_sin.y, 0, cos_sin.x);

        return float3(dot(rotDirX, p), p.y, dot(rotDirY, p));
    }

    float4 GetColorWithRotation(float3 dir, float exposure, float2 cos_sin)
    {
        dir = RotationUp(dir, cos_sin);
        float3 skyColor = float3(1.0, 1.0, 1.0);// SAMPLE_TEXTURECUBE_LOD(_Cubemap, sampler_Cubemap, dir, 0).rgb * _Intensity * exposure;
        skyColor = ClampToFloat16Max(skyColor);

        return float4(skyColor, 1.0);
    }

    float4 RenderSky(Varyings input, float exposure)
    {
        float3 viewDirWS = GetSkyViewDirWS(input.positionCS.xy);

        // Reverse it to point into the scene
        float3 dir = -viewDirWS;

        return GetColorWithRotation(dir, exposure, _CosSinPhi);
    }

    float4 FragBaking(Varyings input) : SV_Target
    {
        return RenderSky(input, 1.0);
    }

        float4 FragRender(Varyings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        return RenderSky(input, GetCurrentExposureMultiplier());
    }

        ENDHLSL

        SubShader
    {
        // Regular New Sky
        // For cubemap
        Pass
        {
            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment FragBaking
            ENDHLSL
        }

            // For fullscreen Sky
            Pass
        {
            ZWrite Off
            ZTest LEqual
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment FragRender
            ENDHLSL
        }
    }
    Fallback Off
}