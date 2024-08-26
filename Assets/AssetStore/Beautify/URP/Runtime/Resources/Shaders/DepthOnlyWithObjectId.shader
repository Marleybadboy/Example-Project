Shader "Hidden/Beautify2/DepthOnlyWithObjectId"
{
    Properties
    {
        [MainTexture] _BaseMap ("Texture", 2D) = "white" {}
        [MainColor] _BaseColor("Color", Color) = (1, 1, 1, 1)
        _Cutoff("AlphaCutout", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        ZWrite On
        Cull [_Cull]

        Pass
        {
            Name "Beautify DepthOnly With ObjectId Pass"
            HLSLPROGRAM
            #pragma target 2.0
            #pragma vertex UnlitPassVertex
            #pragma fragment UnlitPassFragment
            #pragma multi_compile_local _ DEPTH_PREPASS_ALPHA_TEST
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            #if DEPTH_PREPASS_ALPHA_TEST
                CBUFFER_START(UnityPerMaterial)
                half _Cutoff;
                CBUFFER_END
            #endif


            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

struct Attributes
{
    float4 positionOS : POSITION;
    #if DEPTH_PREPASS_ALPHA_TEST
        float2 uv : TEXCOORD0;
    #endif
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float4 positionCS : SV_POSITION;

    #if DEPTH_PREPASS_ALPHA_TEST
        float2 uv : TEXCOORD0;
    #endif

    float objectDepth : TEXCOORD1;

    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

Varyings UnlitPassVertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);

    output.positionCS = vertexInput.positionCS;
    #if DEPTH_PREPASS_ALPHA_TEST
        output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
    #endif

    VertexPositionInputs vertexInput0 = GetVertexPositionInputs(float3(0,0,0));
    float objectId = dot(vertexInput0.positionWS, 1);
    output.objectDepth = objectId;

    return output;
}

void UnlitPassFragment(
    Varyings input
    , out half4 outColor : SV_Target0
)
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

     #if DEPTH_PREPASS_ALPHA_TEST
         half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);
          clip(color.a - _Cutoff);
     #endif

     outColor = input.objectDepth;
}

            ENDHLSL
        }
    }
}
