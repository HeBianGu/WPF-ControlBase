//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- PointLight
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float4 color : register(C0);
float2 light1Position : register(C1);
float4 light1Color : register(C2);
float2 light2Position : register(C3);
float4 light2Color : register(C4);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D implicitInputSampler : register(S0);


//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

static const float2 fillPosition = { 0.5, 0.5 };
static const float fillRadius = 0.8;
static const float4 fillLightColor = { 1, 1, 1, 1 };
static const float3 fillAttenuation = { 1, 0, 0 };
static const float light1Radius = 0.2;
static const float3 light1Attenuation = { 0.2, 0, 3 };
static const float light2Radius = 0.2;
static const float3 light2Attenuation = { 0.2, 0, 3 };
static const float desaturate = 0.9;
static const float contrast = 0.95;

float4 createLight(float4 color : COLOR, float2 lightPosition, float2 viewPosition, float radius, float3 attenuation) : COLOR
{
	float distance = length(lightPosition - viewPosition);
	float falloff = saturate(radius / (attenuation.x + distance * attenuation.y + distance * distance * attenuation.z));
	
	return color * falloff;
}

float4 createBlend(float4 color1 : COLOR, float4 color2 : COLOR) : COLOR
{
	float3 half = ceil(color1.rgb - 0.5);
    float4 result;
    result.rgb = half.rgb * (1 - (1 - 2 * (color1.rgb - 0.5)) * (1 - color2.rgb)) +
						    (1 - half.rgb) * (2 * color1.rgb * color2.rgb);
    result.a = color1.a + color2.a;
    
    return result;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
	// lights
    float4 fillLight = createLight(fillLightColor, fillPosition, uv, fillRadius, fillAttenuation);
    float4 light1 = createLight(light1Color, light1Position, uv, light1Radius, light1Attenuation);
    float4 blend1 = createBlend(fillLight, light1);
    float4 light2 = createLight(light2Color, light2Position, uv, light2Radius, light2Attenuation);    
    float4 blend2 = createBlend(blend1, light2);

    float4 pixelColor = tex2D(implicitInputSampler, uv);
    
    // desaturate
    float3 luminanceWeights = float3(0.299, 0.587, 0.114);
    float luminance = dot(pixelColor.rgb, luminanceWeights);
    pixelColor.rgb = lerp(pixelColor.rgb, luminance, desaturate);
    
    // contrast
    pixelColor.rgb -= float3(0.5, 0.5, 0.5);
    pixelColor.rgb *= float3(contrast, contrast, contrast);
    pixelColor.rgb += float3(0.5, 0.5, 0.5);

	// colorize
    float4 blend3;
    blend3.rgb = pixelColor.rgb * color.rgb;
    blend3.a = color.a;

    return createBlend(blend2, blend3);
}
