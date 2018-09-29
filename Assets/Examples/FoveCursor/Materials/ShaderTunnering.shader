// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:0,qpre:4,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32879,y:32770,varname:node_3138,prsc:2|emission-7241-RGB,clip-7710-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32488,y:32812,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:1651,x:31697,y:32716,varname:node_1651,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:2547,x:32029,y:32827,varname:node_2547,prsc:2|A-1651-U,B-9245-OUT;n:type:ShaderForge.SFN_Add,id:541,x:32058,y:33076,varname:node_541,prsc:2|A-1651-V,B-6672-OUT;n:type:ShaderForge.SFN_Slider,id:9245,x:31649,y:33024,ptovrint:False,ptlb:UX,ptin:_UX,varname:node_9245,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:6672,x:31649,y:33184,ptovrint:False,ptlb:VY,ptin:_VY,varname:node_6672,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Append,id:1650,x:32297,y:32961,varname:node_1650,prsc:2|A-2547-OUT,B-541-OUT;n:type:ShaderForge.SFN_Vector2,id:4017,x:32226,y:33313,varname:node_4017,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Distance,id:1689,x:32488,y:33167,varname:node_1689,prsc:2|A-1650-OUT,B-4017-OUT;n:type:ShaderForge.SFN_Clamp01,id:7710,x:32857,y:33352,varname:node_7710,prsc:2|IN-9956-OUT;n:type:ShaderForge.SFN_Multiply,id:9956,x:32643,y:33352,varname:node_9956,prsc:2|A-1689-OUT,B-1885-OUT;n:type:ShaderForge.SFN_Slider,id:1885,x:31988,y:33468,ptovrint:False,ptlb:scale,ptin:_scale,varname:node_1885,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:200;proporder:7241-9245-6672-1885;pass:END;sub:END;*/

Shader "Shader Forge/ShaderTunnering" {
    Properties {
        _Color ("Color", Color) = (0,0,0,1)
        _UX ("UX", Range(-1, 1)) = 0
        _VY ("VY", Range(-1, 1)) = 0
        _scale ("scale", Range(0, 200)) = 4
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Overlay"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _UX;
            uniform float _VY;
            uniform float _scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip(saturate((distance(float2((i.uv0.r+_UX),(i.uv0.g+_VY)),float2(0.5,0.5))*_scale)) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _UX;
            uniform float _VY;
            uniform float _scale;
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
                clip(saturate((distance(float2((i.uv0.r+_UX),(i.uv0.g+_VY)),float2(0.5,0.5))*_scale)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
