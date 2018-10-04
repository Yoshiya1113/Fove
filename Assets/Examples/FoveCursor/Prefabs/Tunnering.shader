// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9995,x:33481,y:32760,varname:node_9995,prsc:2|emission-7184-RGB,clip-7480-OUT;n:type:ShaderForge.SFN_Slider,id:866,x:32266,y:33113,ptovrint:False,ptlb:UX,ptin:_UX,varname:node_866,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1406,x:32266,y:33212,ptovrint:False,ptlb:VY,ptin:_VY,varname:node_1406,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:8198,x:32604,y:33077,varname:node_8198,prsc:2|A-9490-V,B-1406-OUT;n:type:ShaderForge.SFN_Add,id:3909,x:32604,y:32935,varname:node_3909,prsc:2|A-9490-U,B-866-OUT;n:type:ShaderForge.SFN_TexCoord,id:9490,x:32423,y:32935,varname:node_9490,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:7184,x:33287,y:32859,ptovrint:False,ptlb:node_7184,ptin:_node_7184,varname:node_7184,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:1551,x:33126,y:33040,varname:node_1551,prsc:2|A-4998-OUT,B-9987-OUT;n:type:ShaderForge.SFN_Clamp01,id:7480,x:33287,y:33040,varname:node_7480,prsc:2|IN-1551-OUT;n:type:ShaderForge.SFN_Distance,id:4998,x:32958,y:33040,varname:node_4998,prsc:2|A-3649-OUT,B-6002-OUT;n:type:ShaderForge.SFN_Slider,id:9987,x:32801,y:33225,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_9987,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:200;n:type:ShaderForge.SFN_Vector2,id:6002,x:32801,y:33077,varname:node_6002,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Append,id:3649,x:32801,y:32935,varname:node_3649,prsc:2|A-3909-OUT,B-8198-OUT;proporder:7184-866-1406-9987;pass:END;sub:END;*/

Shader "Custom/Tunnering" {
    Properties {
        _node_7184 ("node_7184", Color) = (0,0,0,1)
        _UX ("UX", Range(-1, 1)) = 0
        _VY ("VY", Range(-1, 1)) = 0
        _Size ("Size", Range(0, 200)) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _UX;
            uniform float _VY;
            uniform float4 _node_7184;
            uniform float _Size;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip(saturate((distance(float2((i.uv0.r+_UX),(i.uv0.g+_VY)),float2(0.5,0.5))*_Size)) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _node_7184.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _UX;
            uniform float _VY;
            uniform float _Size;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip(saturate((distance(float2((i.uv0.r+_UX),(i.uv0.g+_VY)),float2(0.5,0.5))*_Size)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
