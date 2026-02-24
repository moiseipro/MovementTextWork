Shader "TTE/2D/SpriteSheetUnlit"
{
    Properties
    {
        _MainTex ("Sprite Atlas", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _GridSize ("Grid Size (X=Cols, Y=Rows)", Vector) = (8,8,0,0)
        _FrameIndex ("Current Frame Index", Float) = 0
        [Toggle(PIXEL_SNAP)] _PixelSnap ("Pixel Snap", Float) = 0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="TransparentCutout" "PreviewType"="Plane" }
        Lighting Off
        Cull Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXEL_SNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _GridSize;
            float _FrameIndex;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                
                #ifdef PIXEL_SNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif

                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Логика переключения кадров
                float2 gridSize = _GridSize;
                float totalFrames = gridSize.x * gridSize.y;
                
                // Нормализация индекса (защита от вылета)
                float frame = fmod(_FrameIndex, totalFrames);
                if(frame < 0) frame += totalFrames;

                // Вычисляем X и Y ячейки
                float cellX = fmod(frame, gridSize.x);
                float cellY = floor(frame / gridSize.x);

                // Размер одной ячейки в UV
                float2 cellSize = 1.0 / gridSize;

                // Сдвиг UV
                float2 uvOffset = float2(cellX * cellSize.x, cellY * cellSize.y);
                
                // Применяем к исходным UV (для Quad они 0..1)
                float2 finalUV = IN.texcoord * cellSize + uvOffset;
                
                // Инверсия V для спрайтов (если текстура перевернута)
                // Обычно в Unity 0,0 это низ, но в атласах бывает по-разному. 
                // Если спрайты "плывут", раскомментируйте строку ниже:
                // finalUV.y = 1.0 - finalUV.y; 

                fixed4 c = tex2D(_MainTex, finalUV) * IN.color;
                clip(c.a - 0.01); // Alpha Clip для прозрачности
                return c;
            }
            ENDCG
        }
    }
}