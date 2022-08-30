Shader "Unlit/AmmoBar"
{
   Properties
    {
        [NoScaleOffSet] _MainTex ("Texture", 2D) = "white" {}
        
        _healthBarColorFull ("HealthBar Color", Color) = (0,0,1,1)
        _healthBarColorEmpty ("HealthBar Empty", Color) = (1,0,0,1)
        _healthBarColorWarning ("HealthBar Warning", Color) = (1,0.5,0,1)
        _bgColor ("Background Color", Color) = (0,0,0,1)
        _Health ("Health", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
            "Queue" = "Transparent" }
        

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Health;
            float4 _healthBarColorFull;
            float4 _healthBarColorEmpty;
            float4 _healthBarColorWarning;
            float4 _bgColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //Tim's code: shakes health bar when it goes below a certain health
                o.vertex.y += ((1 - _Health) *cos(_Time * 500) *0.05) * (_Health < 0.2);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {

                float2 coords = i.uv;
                coords.x *= 8;

                float2 pointOnLineSeg = float2 (clamp(coords.x, 0.5, 7.5),0.5);
                float sdf = distance(coords, pointOnLineSeg) *2 -1;

                clip(-sdf);

                float borderSdf = sdf + 0.2; //here

                float pd = fwidth(borderSdf);//smoothens the lines using steps but smoother
                
                float borderMask = 1 - saturate(borderSdf/pd); //here
                
                //return float4(sdf.xxx, 1); <placed here to test just the black bar
                //float healthBarMask = _Health > i.uv.x;
                float healthBarMaskSteps = _Health > floor(i.uv.x * 8) /8;


                //when using an external image, make sure to clamp the image its inspector
                //float4 healthBarColor = tex2D(_MainTex, float2(_Health, i.uv.y)); //uses the texture in applied in the material
                float4 healthBarColor = lerp(_healthBarColorEmpty, _healthBarColorFull, _Health ); //regular health bar flow
                
                //Tim's code: allows the code to flash
                healthBarColor = lerp(healthBarColor, _healthBarColorWarning, (1 - _Health) * (sin(_Time * 30 * (1 - _Health))*0.5+0.5) );
                
                
                float4 outColor = lerp (_bgColor, healthBarColor, healthBarMaskSteps);

                return float4 (outColor.xyz * borderMask, 1);
                //return healthBarMask;
            }
            ENDCG
        }
    }
}
