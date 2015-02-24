sampler MainSampler : register(s0);

struct VSOut {
	float2 TexCoord : TEXCOORD0;
	float4 Color : COLOR0;
};

float4 PS(VSOut input) : COLOR0 {
	return tex2D(MainSampler, input.TexCoord) * input.Color;
}

technique Technique1 {
	pass Pass1 {
		PixelShader = compile ps_2_0 PS();
	}
}
