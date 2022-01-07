//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- Blend
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

//float4 colorFilter : register(C0);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D input1 : register(S0);
sampler2D input2 : register(S1);


//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

float4 main(float2 uv : TEXCOORD) : COLOR
{
   float4 color1 = tex2D(input2, uv);
   float4 color2 = tex2D(input1, uv);
   
   float3 half = ceil(color2.rgb - 0.5);
   float4 result;
   result.rgb = half.rgb * (1 - (1 - 2 * (color2.rgb - 0.5)) * (1 - color1.rgb))
                 + (1 - half.rgb) * (2 * color2.rgb * color1.rgb);
   result.a = color1.a;
    
   return result;
}
