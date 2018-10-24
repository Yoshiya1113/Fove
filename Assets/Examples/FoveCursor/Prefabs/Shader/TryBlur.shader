// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33314,y:32717,varname:node_3138,prsc:2|custl-5407-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:5407,x:32864,y:33046,varname:node_5407,prsc:2|IN-4387-OUT,IMIN-3637-OUT,IMAX-3389-OUT,OMIN-1605-OUT,OMAX-7140-OUT;n:type:ShaderForge.SFN_Vector1,id:3389,x:32601,y:33277,varname:node_3389,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3637,x:32601,y:33152,varname:node_3637,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1605,x:32601,y:33391,varname:node_1605,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4387,x:32601,y:33029,varname:node_4387,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:7140,x:32599,y:33509,varname:node_7140,prsc:2,v1:1;pass:END;sub:END;*/

Shader "Shader Forge/TryBlur" {
    Properties {
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float node_3637 = 0.0;
                float node_1605 = 0.0;
                float node_5407 = (node_1605 + ( (0.5 - node_3637) * (1.0 - node_1605) ) / (1.0 - node_3637));
                float3 finalColor = float3(node_5407,node_5407,node_5407);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
