Shader "UI/ParallaxStripes"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (0.1, 0.3, 0.5, 1)
        _SecondColor ("Second Color", Color) = (0.2, 0.4, 0.6, 1)
        
        _Layer1Speed ("Layer 1 Speed", Range(0, 2)) = 0.1
        _Layer2Speed ("Layer 2 Speed", Range(0, 2)) = 0.2
        
        _Layer1Scale ("Layer 1 Scale", Range(1, 50)) = 10
        _Layer2Scale ("Layer 2 Scale", Range(1, 50)) = 20
        
        _Layer1Opacity ("Layer 1 Opacity", Range(0, 1)) = 1.0
        _Layer2Opacity ("Layer 2 Opacity", Range(0, 1)) = 0.7

        _RotationAngle ("Rotation Angle", Range(0, 360)) = 0
        

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    
    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp] 
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]
        
        Pass
        {
            Name "Default"
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            float4 _MainColor;
            float4 _SecondColor;
            
            float _Layer1Speed;
            float _Layer2Speed;
            
            float _Layer1Scale;
            float _Layer2Scale;
            
            float _Layer1Opacity;
            float _Layer2Opacity;

            float _RotationAngle;
            
            sampler2D _MainTex;
            float4 _ClipRect;
            float4 _MainTex_ST;
            
         
            float2 rotateUV(float2 uv, float angle)
            {
                float angleRad = angle * (3.1415926535 / 180.0);
                
            
                float2 uvCentered = uv - 0.5;
                

                float c = cos(angleRad);
                float s = sin(angleRad);
                float2x2 rotationMatrix = float2x2(c, -s, s, c);
                
                float2 rotatedUV = mul(rotationMatrix, uvCentered);
                
                return rotatedUV + 0.5;
            }
            
            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                
                OUT.color = v.color;
                return OUT;
            }
            
            float stripes(float2 uv, float scale, float offset)
            {
                return step(0.5, frac((uv.x + offset) * scale));
            }
            
            fixed4 frag(v2f IN) : SV_Target
            {
                float2 rotatedUV = rotateUV(IN.texcoord, _RotationAngle);
                
                float offset1 = _Time.y * _Layer1Speed;
                float offset2 = _Time.y * _Layer2Speed;
                
                float layer1 = stripes(rotatedUV, _Layer1Scale, offset1);
                float layer2 = stripes(rotatedUV, _Layer2Scale, offset2);

                float pattern = layer1 * _Layer1Opacity;
                pattern = lerp(pattern, layer2, _Layer2Opacity * 0.7);
                           
                fixed4 color = lerp(_MainColor, _SecondColor, pattern);

                color *= IN.color;
                
                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif
                
                return color;
            }
            ENDCG
        }
    }
    
    FallBack "UI/Default"
}