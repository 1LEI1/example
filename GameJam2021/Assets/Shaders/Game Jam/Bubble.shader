Shader "Game Jam/Bubble" 
{
    Properties {
        _AlphaScale ("AlphaScale", Range(0,1)) = 1
        [NoScaleOffset]_MainTex ("Main Texture (RGB)", 2D) = "white" {}
    } 
    SubShader {
 
        Tags { "IgnoreProjector" = "true" "RenderType" = "Transparent"  "RenderPipeline" = "UniversalPipeline" "Queue" = "Transparent"}
    

        pass 
        { 
           
            Tags { "LightMode" = "UniversalForward" }
            ColorMask RGB
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
 
            //不加不接受投影
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            //下面2个可选
            #pragma multi_compile _ _MAIN_LIGHT_CALCULATE_SHADOWS
           
            #pragma multi_compile _ _SHADOWS_SOFT
 
            #pragma multi_compile_instancing
            #pragma vertex vert
            #pragma fragment frag
 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
 
            
 
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
 
            CBUFFER_START(UnityPerMaterial)
            float _AlphaScale;
            CBUFFER_END
 
            struct a2v
            {
                float4 positionOS : POSITION;
                float3 normalOS      : NORMAL;
                float4 tangentOS     : TANGENT;
                float2 texcoord : TEXCOORD0;
                float4 col : COLOR;
            };
            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 col : COLOR;
                #if defined(_MAIN_LIGHT_SHADOWS)
                float4 shadowCoord:TEXCOORD1;
                #endif
                float3 normalWS:TEXCOORD2;

            };
 
            
 
            v2f vert(a2v v)
            {
                v2f o;
 
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(v.normalOS, v.tangentOS);
                o.positionCS = vertexInput.positionCS;
 
                o.uv = v.texcoord;
                o.col = v.col;
             
                o.normalWS = normalInput.normalWS;
               
              #if defined(_MAIN_LIGHT_SHADOWS)
                     o.shadowCoord = GetShadowCoord(vertexInput);
              #endif 
                return o;
            }
 
            half4 frag(v2f i) : COLOR
            {
           
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                half4 col = i.col;
               
                #if defined(_MAIN_LIGHT_SHADOWS)
                    Light mainLight = GetMainLight(i.shadowCoord);
                    float3 SH = SampleSH(i.normalWS);
                    half NdotL = dot(i.normalWS, mainLight.direction);
                    col.rgb *= mainLight.color.rgb * mainLight.shadowAttenuation * NdotL + SH;
                #endif
                clip(col.a - i.uv.y);
                col.a *= _AlphaScale;
                return col;
            }
            ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            ENDHLSL
        }
    }
   
}