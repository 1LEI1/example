Shader "Game Jam/Rock" 
{
    Properties {
        _Outline ("Outline", Range(0, 1)) = 0.1
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _Color("Color Tint", color) = (1,1,1,1)
        [NoScaleOffset]_MainTex ("Main Texture (RGB)", 2D) = "white" {}
     
    } 
    SubShader {
 
        Tags { "IgnoreProjector" = "true" "RenderType" = "Opaque"  "RenderPipeline" = "UniversalPipeline"}
 
        pass 
        { 
           
            Tags { "LightMode" = "UniversalForward" }
            ColorMask RGB
 
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
 
            float4  _Forground;
            float _TransVal;
            float4 _Color;
            float _FillCenter;
            CBUFFER_END
 
            struct a2v
            {
                float4 positionOS : POSITION;
                 float3 normalOS      : NORMAL;
                 float4 tangentOS     : TANGENT;
                float2 texcoord : TEXCOORD0;
                 UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
               
                #if defined(_MAIN_LIGHT_SHADOWS)
                float4 shadowCoord:TEXCOORD1;
                #endif
                float3 normalWS:TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
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
 
                          
 
                half4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv) ;
               
                  #if defined(_MAIN_LIGHT_SHADOWS)
                        Light mainLight = GetMainLight(i.shadowCoord);
                        float3 SH = SampleSH(i.normalWS);
                        half NdotL = dot(i.normalWS, mainLight.direction);
                        col.rgb *= _Color.rgb *  mainLight.color.rgb * mainLight.shadowAttenuation * NdotL + SH;
                  #else
 
                        col.rgb *= _Color.rgb;
                  #endif
 
                
                return col;
            }
            ENDHLSL
        }
        Pass 
        {
            Tags{ "LightMode" = "SRPDefaultUnlit" }
            NAME "OUTLINE" 
            Cull Front      
            CGPROGRAM     
            #pragma vertex vert
            #pragma fragment frag
                     
            #include "UnityCG.cginc"
                     
            float _Outline;
            fixed4 _OutlineColor;
                     
            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
                     
            struct v2f {
                float4 pos : SV_POSITION;
            };
                     
            v2f vert (a2v v) {
                v2f o;
                           
                float4 pos = mul(UNITY_MATRIX_MV, v.vertex);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);  
                normal.z = -0.5;
                pos = pos + float4(normalize(normal), 0) * _Outline;
                o.pos = mul(UNITY_MATRIX_P, pos);
                           
                return o;
            }
                     
            float4 frag(v2f i) : SV_Target {
                return float4(_OutlineColor.rgb, 1);               
            }  
            ENDCG
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