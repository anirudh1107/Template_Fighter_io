Shader "Custom/URPShadowCatcher"
{
    Properties
    {
        _ShadowStrength("Shadow Strength", Range(0, 1)) = 1.0
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" 
            "Queue"="Transparent" 
            "RenderPipeline" = "UniversalPipeline" 
        }
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            // Standard Multiplicative Blending
            // This replaces 'Blend Multiply Zero' which caused your error
            Blend DstColor Zero
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
            };

            struct Varyings {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float _ShadowStrength;
            CBUFFER_END

            Varyings vert (Attributes IN) {
                Varyings OUT;
                // Using standard URP transformation macro
                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = vertexInput.positionCS;
                OUT.positionWS = vertexInput.positionWS;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target {
                // Get the shadow coordinates
                #if (defined(_MAIN_LIGHT_SHADOWS) || defined(_MAIN_LIGHT_SHADOWS_CASCADE))
                    float4 shadowCoord = TransformWorldToShadowCoord(IN.positionWS);
                #else
                    float4 shadowCoord = float4(0,0,0,0);
                #endif
                
                Light mainLight = GetMainLight(shadowCoord);
                half shadow = mainLight.shadowAttenuation;

                // Adjust shadow by strength
                // 1.0 = White (No effect in Multiply blend)
                // < 1.0 = Darkens the background
                float finalShadow = lerp(1.0, shadow, _ShadowStrength);
                
                return half4(finalShadow, finalShadow, finalShadow, 1.0);
            }
            ENDHLSL
        }
    }
}