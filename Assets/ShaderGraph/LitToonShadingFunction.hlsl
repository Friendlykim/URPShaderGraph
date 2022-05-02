void LitToonShadingFunction_float(in float3 normal,in float ToonRampSmoothness,in float3 ClipSpacePos,in float3 worldPos,in float4 ToonRampTint,
in float ToonRampOffset,out float3 ToonRampOutput,out float3 Direction)
{
    #ifdef SHADERGRAPH_PREVIEW
        ToonRampOutput=float3(0.5,0.5,0);
        Direction=float3(0.5,0.5,0);
    #else
        #if SHADOWS_SCREEN
        half4 shadowCoord=ComputeScreenPos(ClipSpacePos);
        #else
        //half4 shadowCoord = TransformWorldtoShadowCoord(worldPos);
        #endif

        #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
        LitToonShadingFunction_float 

}