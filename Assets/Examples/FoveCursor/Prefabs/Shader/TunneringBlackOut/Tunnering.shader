// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:False,qofs:0,qpre:4,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9995,x:33481,y:32760,varname:node_9995,prsc:2|emission-7184-RGB,alpha-7937-OUT;n:type:ShaderForge.SFN_Slider,id:866,x:32266,y:33113,ptovrint:False,ptlb:UX,ptin:_UX,varname:node_866,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1406,x:32266,y:33212,ptovrint:False,ptlb:VY,ptin:_VY,varname:node_1406,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:8198,x:32604,y:33077,varname:node_8198,prsc:2|A-9490-V,B-1406-OUT;n:type:ShaderForge.SFN_Add,id:3909,x:32604,y:32935,varname:node_3909,prsc:2|A-9490-U,B-866-OUT;n:type:ShaderForge.SFN_TexCoord,id:9490,x:32423,y:32935,varname:node_9490,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:7184,x:33121,y:32743,ptovrint:False,ptlb:node_7184,ptin:_node_7184,varname:node_7184,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:1551,x:33126,y:33040,varname:node_1551,prsc:2|A-4998-OUT,B-9987-OUT;n:type:ShaderForge.SFN_Clamp01,id:7480,x:33287,y:33040,varname:node_7480,prsc:2|IN-1551-OUT;n:type:ShaderForge.SFN_Distance,id:4998,x:32958,y:33040,varname:node_4998,prsc:2|A-3649-OUT,B-6002-OUT;n:type:ShaderForge.SFN_Slider,id:9987,x:32801,y:33225,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_9987,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:200;n:type:ShaderForge.SFN_Vector2,id:6002,x:32801,y:33077,varname:node_6002,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Append,id:3649,x:32801,y:32935,varname:node_3649,prsc:2|A-3909-OUT,B-8198-OUT;n:type:ShaderForge.SFN_Tex2d,id:6849,x:33111,y:32582,ptovrint:False,ptlb:node_6849,ptin:_node_6849,varname:node_6849,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3a5a96df060a5cf4a9cc0c59e13486b7,ntxv:0,isnm:False|UVIN-2314-OUT;n:type:ShaderForge.SFN_Multiply,id:2314,x:32860,y:32650,varname:node_2314,prsc:2|A-3649-OUT,B-9987-OUT;n:type:ShaderForge.SFN_Clamp01,id:3324,x:33285,y:32599,varname:node_3324,prsc:2|IN-6849-R;n:type:ShaderForge.SFN_OneMinus,id:7937,x:33498,y:32526,varname:node_7937,prsc:2|IN-3324-OUT;proporder:7184-866-1406-9987-6849;pass:END;sub:END;*/

Shader "Custom/Tunnering" {
    Properties {
        _node_7184 ("node_7184", Color) = (0,0,0,1)
        _UX ("UX", Range(-1, 1)) = 0
        _VY ("VY", Range(-1, 1)) = 0
        _Size ("Size", Range(0, 200)) = 1
        _node_6849 ("node_6849", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Overlay"
            "RenderType"="TransparentCutout"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Always
            ZWrite Off
            
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
            uniform sampler2D _node_6849; uniform float4 _node_6849_ST;
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
////// Lighting:
////// Emissive:
                float3 emissive = _node_7184.rgb;
                float3 finalColor = emissive;
                float2 node_3649 = float2((i.uv0.r+_UX),(i.uv0.g+_VY));
                float2 node_2314 = (node_3649*_Size);
                float4 _node_6849_var = tex2D(_node_6849,TRANSFORM_TEX(node_2314, _node_6849));
                fixed4 finalRGBA = fixed4(finalColor,(1.0 - saturate(_node_6849_var.r)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
