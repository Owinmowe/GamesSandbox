Shader "Bottle Game/Unlit Liquid Shader"
{
    Properties
    {
        top_limit ("Top Limit", Float) = 1.001
        bottom_limit ("Bottom Limit", Float) = -4.397
        
        //TODO Add header with material property drawer
        bubbles_texture ("Bubbles Texture", 2D) = "white" {}
        bubbles_transparency ("Bubbles Transparency", Range(0.0, 1.0)) = 1
        bubbles_speed ("Bubbles Texture Speed", Float) = 2
        bubbles_top_fade_limit ("Top Fade Limit", Range(0.0, 5.0)) = 0
        bubbles_bot_fade_limit ("Bot Fade Limit", Range(0.0, 5.0)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct vertexData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct fragmentData
            {
                float4 clip_position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 object_position: TEXCOORD1;
            };
            
            fixed4 liquid_color_array[6];
            float liquid_amount_array[6];
            int liquids_count;
            
            int bottle_capacity;
            int bottle_total_liquid_amount;
            
            float top_limit;
            float bottom_limit;

            sampler2D bubbles_texture;
            float4 bubbles_texture_ST;
            float bubbles_speed;
            float bubbles_transparency;
            float bubbles_top_fade_limit;
            float bubbles_bot_fade_limit;

            float inverse_lerp(const float from, const float to, const float value)
            {
                return (value - from) / (to - from);
            }
            
            fixed4 get_current_liquid_color(const float z_pos, const float z_amount_current_liquid, const float z_amount_per_liquid_amount)
            {
                if(z_pos > z_amount_current_liquid)
                    return fixed4(0,0,0,0);

                float current_amount = 0;
                
                for(int i = 0; i < liquids_count; i++)
                {
                    const float color_limit_bot = bottom_limit + current_amount * z_amount_per_liquid_amount;

                    current_amount += liquid_amount_array[i];
                        
                    const float color_limit_top = bottom_limit + current_amount * z_amount_per_liquid_amount;
                        
                    if(z_pos > color_limit_bot && z_pos < color_limit_top)
                    {
                        return liquid_color_array[i];
                    }
                }
                
                return fixed4(0,0,0,0);
            }

            fixed4 get_current_bubbles_color(const float z_pos, const float2 uv, const float z_amount_current_liquid, const fixed4 liquid_color)
            {
                const float fade_start_top = z_amount_current_liquid - bubbles_top_fade_limit;
                const float fade_start_bot = bottom_limit + bubbles_bot_fade_limit;

                fixed4 bubbles_color = tex2D (bubbles_texture, uv);
                
                if(z_pos > fade_start_top)
                {
                    bubbles_color.a -= inverse_lerp(fade_start_top, z_amount_current_liquid,z_pos);
                }
                else if(z_pos < fade_start_bot)
                {
                    bubbles_color.a -= inverse_lerp(fade_start_bot, bottom_limit, z_pos);
                }
                
                return liquid_color * bubbles_color;
            }

            fixed4 get_liquid_with_bubbles_mix(const fixed4 liquid_color, const fixed4 bubbles_color)
            {
                fixed4 final_color = bubbles_color;
                
                if(final_color.a < bubbles_transparency)
                    final_color = liquid_color;
                
                return final_color;
            }
            
            fragmentData vert (const vertexData vertex_data)
            {
                fragmentData output;
                output.uv = TRANSFORM_TEX(vertex_data.uv, bubbles_texture);
                output.uv.y -= _Time * bubbles_speed;
                output.clip_position = UnityObjectToClipPos(vertex_data.vertex);
                output.object_position = vertex_data.vertex;
                return output;
            }
            
            fixed4 frag (const fragmentData input) : SV_Target
            {
                const float z_full_bottle_range = abs(bottom_limit) + abs(top_limit);
                const float z_amount_per_liquid_amount = z_full_bottle_range / bottle_capacity;
                const float z_amount_current_liquid = bottom_limit + (z_amount_per_liquid_amount * bottle_total_liquid_amount);

                const fixed4 liquid_color = get_current_liquid_color(input.object_position.z, z_amount_current_liquid, z_amount_per_liquid_amount);
                const fixed4 bubbles_color = get_current_bubbles_color(input.object_position.z, input.uv, z_amount_current_liquid, liquid_color);
                
                return get_liquid_with_bubbles_mix(liquid_color, bubbles_color);
            }
            
            ENDCG
        }
    }
}
