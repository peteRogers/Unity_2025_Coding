Shader "Hidden/RippleCausticsCookie"
{
    Properties
    {
        _Tiling ("Tiling", Float) = 3.0
        _Speed ("Speed", Float) = 0.6
        _Swirl ("Swirl", Float) = 0.25
        _Sharpness ("Sharpness", Float) = 2.2
        _Brightness ("Brightness", Float) = 0.9
        _MinDark ("Min Dark", Float) = 0.05
        _LineSoftness ("Line Softness", Float) = 0.4
        _Jitter ("Jitter", Float) = 0.15
        _AngleRad ("Pattern Angle (rad)", Float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background" }
        Cull Off ZWrite Off ZTest Always Blend Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   Vert
            #pragma fragment Frag
            #include "UnityCG.cginc"

            float _Tiling, _Speed, _Swirl, _Sharpness, _Brightness, _MinDark, _AngleRad, _LineSoftness, _Jitter;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            v2f Vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = v.uv; // 0..1
                return o;
            }

            // Simple rotation
            float2 rotate(float2 p, float a)
            {
                float s = sin(a), c = cos(a);
                return float2(c*p.x - s*p.y, s*p.x + c*p.y);
            }

            // Cheap hash / noise helpers
            float hash21(float2 p){ p = frac(p*float2(123.34,345.45)); p += dot(p,p+34.345); return frac(p.x*p.y); }

            float2 jitter2(float2 p){
                float n = hash21(p);
                float m = hash21(p + 17.23);
                return float2(n, m);
            }

            // Interference-based “caustic-like” pattern (no textures)
            float caustics(float2 uv, float t)
            {
                // Centered UV, scaled, gentle swirl
                uv = (uv * 2.0 - 1.0);

                // Break straight bands with tiny domain warp + temporal jitter
                float2 j = jitter2(uv * 8.0 + t);
                uv += (j - 0.5) * (_Jitter * 0.025);
                uv += 0.035 * float2(sin(uv.y * 6.0 + t), cos(uv.x * 6.0 - t));

                float r = length(uv) + 1e-5;
                float swirl = _Swirl * 0.75;
                float a = atan2(uv.y, uv.x) + swirl * sin(r*6.0 - t*0.7);
                uv = float2(cos(a), sin(a)) * r;

                // Rotate + tile
                uv = rotate(uv, _AngleRad) * _Tiling;

                // Three moving wave directions (compute phases for AA)
                float2 d1 = normalize(float2( 0.8,  0.6));
                float2 d2 = normalize(float2(-0.7,  0.7));
                float2 d3 = normalize(float2( 0.2, -0.98));

                float p1 = dot(uv, d1)*6.0 + t*1.1;
                float p2 = dot(uv, d2)*7.2 + t*0.9;
                float p3 = dot(uv, d3)*5.4 + t*1.3;

                float s1 = sin(p1);
                float s2 = sin(p2);
                float s3 = sin(p3);
                float baseWave = (s1 + s2 + s3) / 3.0;

                // Anti-alias: widen threshold where frequency is high
                float w = _LineSoftness * (fwidth(p1) + fwidth(p2) + fwidth(p3));

                // Convert to bright ridges with AA smoothing (remove speckle sparkle)
                float ridge = saturate(0.5 - 0.5 * baseWave);
                ridge = smoothstep(0.5 - w, 0.5 + w, ridge);
                float val = pow(ridge, _Sharpness);

                // Clamp dark floor
                val = max(val, _MinDark);

                // Final brightness
                return saturate(val * _Brightness);
            }

            float4 Frag (v2f i) : SV_Target
            {
                // Time in seconds
                float t = _Time.y * _Speed;

                // Two octaves for richer pattern
                float v1 = caustics(i.uv, t);
                // Slightly offset & scaled UV for a second octave
                float2 uv2 = (i.uv * 1.7 + float2(0.13, -0.09));
                float v2 = caustics(uv2, t * 1.12);
                float v = saturate(v1 * 0.55 + v2 * 0.45);

                // Output grayscale; cookie uses RGB
                return float4(v, v, v, 1.0);
            }
            ENDHLSL
        }
    }
}