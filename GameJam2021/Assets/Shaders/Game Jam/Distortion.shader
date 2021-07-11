Shader "Game Jam/Distortion" 
{
    Properties   
    {  
        _MainTex ("Base (RGB)", 2D) = "white" {}  
        _NoiseTex ("Noise Texture (RG)", 2D) = "white" {}  
        _MaskTex ("Mask Texture", 2D) = "white" {}  
        _HeatTime  ("Heat Time", range (0,1.5)) = 1  
        _HeatForce  ("Heat Force", range (0,0.1)) = 0.1  
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
            float _HeatForce;  
            float _HeatTime;  
            uniform sampler2D _NoiseTex;  
            uniform sampler2D _MaskTex;  
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
                // 扭曲效果  
                half4 offsetColor1 = tex2D(_NoiseTex, i.uv + _Time.xz*_HeatTime);  
                half4 offsetColor2 = tex2D(_NoiseTex, i.uv - _Time.yx*_HeatTime);  
                float mask = tex2D(_MaskTex, i.uv).a;  
                i.uv.x += ((offsetColor1.r + offsetColor2.r) - 1) * _HeatForce * mask;  
                i.uv.y += ((offsetColor1.g + offsetColor2.g) - 1) * _HeatForce * mask;  
                half4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
                #if defined(_MAIN_LIGHT_SHADOWS)
                    Light mainLight = GetMainLight(i.shadowCoord);
                    float3 SH = SampleSH(i.normalWS);
                    half NdotL = dot(i.normalWS, mainLight.direction);
                    col.rgb *= mainLight.color.rgb * mainLight.shadowAttenuation * NdotL + SH;
                #endif
                return col;
            }
            ENDHLSL
        }
        
    }
   
}