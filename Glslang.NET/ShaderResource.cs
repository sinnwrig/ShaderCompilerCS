using System.Reflection;
using System.Runtime.InteropServices;

namespace Glslang;


[StructLayout(LayoutKind.Sequential)]
public struct ShaderResource 
{
    public int maxLights;
    public int maxClipPlanes;
    public int maxTextureUnits;
    public int maxTextureCoords;
    public int maxVertexAttribs;
    public int maxVertexUniformComponents;
    public int maxVaryingFloats;
    public int maxVertexTextureImageUnits;
    public int maxCombinedTextureImageUnits;
    public int maxTextureImageUnits;
    public int maxFragmentUniformComponents;
    public int maxDrawBuffers;
    public int maxVertexUniformVectors;
    public int maxVaryingVectors;
    public int maxFragmentUniformVectors;
    public int maxVertexOutputVectors;
    public int maxFragmentInputVectors;
    public int minProgramTexelOffset;
    public int maxProgramTexelOffset;
    public int maxClipDistances;
    public int maxComputeWorkGroupCountX;
    public int maxComputeWorkGroupCountY;
    public int maxComputeWorkGroupCountZ;
    public int maxComputeWorkGroupSizeX;
    public int maxComputeWorkGroupSizeY;
    public int maxComputeWorkGroupSizeZ;
    public int maxComputeUniformComponents;
    public int maxComputeTextureImageUnits;
    public int maxComputeImageUniforms;
    public int maxComputeAtomicCounters;
    public int maxComputeAtomicCounterBuffers;
    public int maxVaryingComponents;
    public int maxVertexOutputComponents;
    public int maxGeometryInputComponents;
    public int maxGeometryOutputComponents;
    public int maxFragmentInputComponents;
    public int maxImageUnits;
    public int maxCombinedImageUnitsAndFragmentOutputs;
    public int maxCombinedShaderOutputResources;
    public int maxImageSamples;
    public int maxVertexImageUniforms;
    public int maxTessControlImageUniforms;
    public int maxTessEvaluationImageUniforms;
    public int maxGeometryImageUniforms;
    public int maxFragmentImageUniforms;
    public int maxCombinedImageUniforms;
    public int maxGeometryTextureImageUnits;
    public int maxGeometryOutputVertices;
    public int maxGeometryTotalOutputComponents;
    public int maxGeometryUniformComponents;
    public int maxGeometryVaryingComponents;
    public int maxTessControlInputComponents;
    public int maxTessControlOutputComponents;
    public int maxTessControlTextureImageUnits;
    public int maxTessControlUniformComponents;
    public int maxTessControlTotalOutputComponents;
    public int maxTessEvaluationInputComponents;
    public int maxTessEvaluationOutputComponents;
    public int maxTessEvaluationTextureImageUnits;
    public int maxTessEvaluationUniformComponents;
    public int maxTessPatchComponents;
    public int maxPatchVertices;
    public int maxTessGenLevel;
    public int maxViewports;
    public int maxVertexAtomicCounters;
    public int maxTessControlAtomicCounters;
    public int maxTessEvaluationAtomicCounters;
    public int maxGeometryAtomicCounters;
    public int maxFragmentAtomicCounters;
    public int maxCombinedAtomicCounters;
    public int maxAtomicCounterBindings;
    public int maxVertexAtomicCounterBuffers;
    public int maxTessControlAtomicCounterBuffers;
    public int maxTessEvaluationAtomicCounterBuffers;
    public int maxGeometryAtomicCounterBuffers;
    public int maxFragmentAtomicCounterBuffers;
    public int maxCombinedAtomicCounterBuffers;
    public int maxAtomicCounterBufferSize;
    public int maxTransformFeedbackBuffers;
    public int maxTransformFeedbackInterleavedComponents;
    public int maxCullDistances;
    public int maxCombinedClipAndCullDistances;
    public int maxSamples;
    public int maxMeshOutputVerticesNV;
    public int maxMeshOutputPrimitivesNV;
    public int maxMeshWorkGroupSizeX_NV;
    public int maxMeshWorkGroupSizeY_NV;
    public int maxMeshWorkGroupSizeZ_NV;
    public int maxTaskWorkGroupSizeX_NV;
    public int maxTaskWorkGroupSizeY_NV;
    public int maxTaskWorkGroupSizeZ_NV;
    public int maxMeshViewCountNV;
    public int maxMeshOutputVerticesExt;
    public int maxMeshOutputPrimitivesExt;
    public int maxMeshWorkGroupSizeXExt;
    public int maxMeshWorkGroupSizeYExt;
    public int maxMeshWorkGroupSizeZExt;
    public int maxTaskWorkGroupSizeXExt;
    public int maxTaskWorkGroupSizeYExt;
    public int maxTaskWorkGroupSizeZExt;
    public int maxMeshViewCountExt;
    public int maxDualSourceDrawBuffersExt;
    public ShaderLimits Limits;


    [DllImport(ShaderCompiler.libraryPath, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr GlslangGetDefaultResource();
    public static readonly ShaderResource DefaultResource = Marshal.PtrToStructure<ShaderResource>(GlslangGetDefaultResource());


    [DllImport(ShaderCompiler.libraryPath, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr GlslangDefaultResourceString();
    private static string GetDefaultString()
    {   
        IntPtr strPtr = GlslangDefaultResourceString();
        string managedString = AllocUtility.AutoString(strPtr);
        AllocUtility.Free(strPtr);
        return managedString;
    }

    public static readonly string DefaultString = GetDefaultString();


    [DllImport(ShaderCompiler.libraryPath, CallingConvention = CallingConvention.Cdecl)]
    private static extern void GlslangDecodeResourceLimits(ref ShaderResource resource, string str);
    public void DecodeFromString(string str) => GlslangDecodeResourceLimits(ref this, str);
}