// RendererCommandAOTHints.cs
// BAEL: IL2CPP AOT stub for PolymorphicMemoryPackableEntity<RendererCommand>
//
// WHY THIS FILE EXISTS:
//   PolymorphicMemoryPackableEntity<T>.InitTypes() uses MakeGenericMethod + CreateDelegate
//   at runtime to build poolBorrowers/poolReturners for each RendererCommand subtype.
//   IL2CPP only supports MakeGenericMethod for methods that were AOT-compiled.
//   This file forces IL2CPP to emit concrete code for Allocate<T> and Return<T>
//   for every registered subtype by referencing them in unreachable code.
//
//   NEVER call ForceAOT(). It exists only so the compiler sees the calls.
//
//   The Allocate and Return methods were patched from private to public in
//   Renderite.Shared.dll by patch-polymorphic-aot-visibility.ps1.

using Renderite.Shared;
using UnityEngine.Scripting;

namespace Renderite.IL2CPPCompat
{
    [Preserve]
    internal static class RendererCommandAOTHints
    {
        [Preserve]
        static void ForceAOT()
        {
            // This method is NEVER called at runtime.
            // The if(false) block forces IL2CPP to compile concrete generic method
            // instantiations for every RendererCommand subtype.
#pragma warning disable CS0162 // Unreachable code detected
            if (false)
            {
                IMemoryPackerEntityPool p = null;

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitProgressUpdate>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitProgressUpdate>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitFinalizeData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitFinalizeData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererEngineReady>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererEngineReady>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererShutdownRequest>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererShutdownRequest>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererShutdown>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererShutdown>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<KeepAlive>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<KeepAlive>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererParentWindow>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererParentWindow>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<FreeSharedMemoryView>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<FreeSharedMemoryView>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetWindowIcon>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetWindowIcon>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetWindowIconResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetWindowIconResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTaskbarProgress>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTaskbarProgress>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<FrameStartData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<FrameStartData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<FrameSubmitData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<FrameSubmitData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PostProcessingConfig>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<PostProcessingConfig>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<QualityConfig>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<QualityConfig>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ResolutionConfig>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<ResolutionConfig>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<DesktopConfig>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<DesktopConfig>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatConfig>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatConfig>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RenderDecouplingConfig>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RenderDecouplingConfig>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MeshUploadData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MeshUploadData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MeshUnload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MeshUnload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MeshUploadResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MeshUploadResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ShaderUpload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<ShaderUpload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ShaderUnload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<ShaderUnload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ShaderUploadResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<ShaderUploadResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialPropertyIdRequest>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialPropertyIdRequest>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialPropertyIdResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialPropertyIdResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialsUpdateBatch>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialsUpdateBatch>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialsUpdateBatchResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialsUpdateBatchResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadMaterial>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadMaterial>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadMaterialPropertyBlock>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadMaterialPropertyBlock>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DFormat>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DFormat>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DProperties>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DProperties>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadTexture2D>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadTexture2D>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DFormat>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DFormat>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DProperties>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DProperties>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadTexture3D>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadTexture3D>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapFormat>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapFormat>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapProperties>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapProperties>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapData>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapData>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadCubemap>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadCubemap>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetRenderTextureFormat>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetRenderTextureFormat>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RenderTextureResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<RenderTextureResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadRenderTexture>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadRenderTexture>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetDesktopTextureProperties>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetDesktopTextureProperties>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<DesktopTexturePropertiesUpdate>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<DesktopTexturePropertiesUpdate>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadDesktopTexture>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadDesktopTexture>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PointRenderBufferUpload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<PointRenderBufferUpload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PointRenderBufferConsumed>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<PointRenderBufferConsumed>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PointRenderBufferUnload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<PointRenderBufferUnload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<TrailRenderBufferUpload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<TrailRenderBufferUpload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<TrailRenderBufferConsumed>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<TrailRenderBufferConsumed>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<TrailRenderBufferUnload>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<TrailRenderBufferUnload>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatUploadRaw>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatUploadRaw>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatUploadEncoded>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatUploadEncoded>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadGaussianSplat>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadGaussianSplat>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<LightsBufferRendererSubmission>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<LightsBufferRendererSubmission>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<LightsBufferRendererConsumed>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<LightsBufferRendererConsumed>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ReflectionProbeRenderResult>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<ReflectionProbeRenderResult>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureLoad>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureLoad>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureUpdate>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureUpdate>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureReady>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureReady>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureChanged>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureChanged>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureProperties>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureProperties>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureStartAudioTrack>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureStartAudioTrack>(p, default);

                PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadVideoTexture>(p);
                PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadVideoTexture>(p, default);

                // VR_ControllerState polymorphic subtypes
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<CosmosControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<CosmosControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<GenericControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<GenericControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<HP_ReverbControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<HP_ReverbControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<IndexControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<IndexControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<PicoNeo2ControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<PicoNeo2ControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<TouchControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<TouchControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<ViveControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<ViveControllerState>(p, default);

                PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<WindowsMR_ControllerState>(p);
                PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<WindowsMR_ControllerState>(p, default);
            }
#pragma warning restore CS0162
        }
    }
}
