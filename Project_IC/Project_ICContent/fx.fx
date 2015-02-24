//sampler mainSampler : register(s0);
/*
struct PsIn {
	float4 texCoord : TEXCOORD0;
	float4 position : POSITION0;
};

float4 PsRadLight(PsIn input) : COLOR0 {
	float2 coord = input.texCoord;

	if(input.position.x == 1) {
		return float4(0, 1, 0, 1);
	}

	if(coord.x < .3) {
		return float4(0, 0, 1, 1);
	}

	//return float4(1, 0, 0, 1);
	return tex2D(mainSampler, coord);
}
*/



float4x4 world;
float4x4 view;
float4x4 projection;

struct VSIn {
	float4 position : POSITION0;
};

struct VSOut {
	float4 position : POSITION0;
};

VSOut VSFunc(VSIn input) {
	VSOut output;
	
	float4 worldPosition = mul(input.position, world);
	float4 viewPosition = mul(worldPosition, view);
	output.position = mul(viewPosition, projection);

	return input;
}

float4 PSFunc(VSOut input) : COLOR0 {
	return float4(1, 0, 0, 1);
}

technique Technique1 {
	pass Pass1 {
		//VertexShader = compile vs_2_0 VSFunc();
		PixelShader = compile ps_2_0 PSFunc();
	}
}