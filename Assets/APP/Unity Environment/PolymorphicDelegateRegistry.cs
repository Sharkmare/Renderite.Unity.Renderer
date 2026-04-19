// PolymorphicDelegateRegistry.cs
//
// IL2CPP compatibility shim for PolymorphicMemoryPackableEntity<T>.InitTypes().
//
// ROOT CAUSE:
//   InitTypes() uses GetMethod + MakeGenericMethod + CreateDelegate to build
//   poolBorrowers / poolReturners at runtime.  In IL2CPP, all reference-type
//   instantiations of a generic method share a single _gshared implementation.
//   CreateDelegate on a _gshared MethodInfo does NOT preserve the runtime type
//   argument, so every delegate invocation calls Allocate_gshared with
//   TisIl2CppFullySharedGenericAny → pool borrow of unknown type → crash.
//
// FIX:
//   Cecil patch (patch-inittypes-replace.ps1) replaces the GetMethod +
//   MakeGenericMethod + CreateDelegate loop in InitTypes with a single call to
//   PolymorphicDelegateRegistry.Setup(typeof(T), poolBorrowers, poolReturners).
//
//   Setup() adds concrete (non-reflection) lambdas that call
//   PolymorphicMemoryPackableEntity<TBase>.Allocate<TSubtype>(pool) and .Return
//   directly.  Because these are explicit static calls with concrete type
//   arguments, IL2CPP dispatches them via the AOT-compiled concrete
//   instantiations (forced by RendererCommandAOTHints.cs), so no _gshared is invoked.
//
// NOTE: The order in each Setup* method MUST exactly match the ldtoken
// registration order in the corresponding type's .cctor.

using System;
using System.Collections.Generic;
using Renderite.Shared;

namespace Renderite.IL2CPPCompat
{
    internal static class PolymorphicDelegateRegistry
    {
        public static void Setup(Type entityType, object borrowerList, object returnerList)
        {
            if (entityType == typeof(RendererCommand))
            {
                SetupRendererCommand(
                    (List<Func<IMemoryPackerEntityPool, RendererCommand>>)borrowerList,
                    (List<Action<IMemoryPackerEntityPool, RendererCommand>>)returnerList);
            }
            else if (entityType == typeof(VR_ControllerState))
            {
                SetupVRControllerState(
                    (List<Func<IMemoryPackerEntityPool, VR_ControllerState>>)borrowerList,
                    (List<Action<IMemoryPackerEntityPool, VR_ControllerState>>)returnerList);
            }
        }

        // RendererCommand: 74 subtypes, must match .cctor registration order exactly.

        private static void SetupRendererCommand(
            List<Func<IMemoryPackerEntityPool, RendererCommand>> b,
            List<Action<IMemoryPackerEntityPool, RendererCommand>> r)
        {
            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitProgressUpdate>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitProgressUpdate>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererInitFinalizeData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererInitFinalizeData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererEngineReady>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererEngineReady>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererShutdownRequest>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererShutdownRequest>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererShutdown>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererShutdown>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<KeepAlive>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<KeepAlive>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RendererParentWindow>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RendererParentWindow>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<FreeSharedMemoryView>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<FreeSharedMemoryView>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetWindowIcon>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetWindowIcon>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetWindowIconResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetWindowIconResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTaskbarProgress>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTaskbarProgress>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<FrameStartData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<FrameStartData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<FrameSubmitData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<FrameSubmitData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PostProcessingConfig>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<PostProcessingConfig>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<QualityConfig>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<QualityConfig>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ResolutionConfig>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<ResolutionConfig>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<DesktopConfig>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<DesktopConfig>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatConfig>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatConfig>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RenderDecouplingConfig>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RenderDecouplingConfig>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MeshUploadData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MeshUploadData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MeshUnload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MeshUnload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MeshUploadResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MeshUploadResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ShaderUpload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<ShaderUpload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ShaderUnload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<ShaderUnload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ShaderUploadResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<ShaderUploadResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialPropertyIdRequest>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialPropertyIdRequest>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialPropertyIdResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialPropertyIdResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialsUpdateBatch>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialsUpdateBatch>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<MaterialsUpdateBatchResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<MaterialsUpdateBatchResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadMaterial>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadMaterial>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadMaterialPropertyBlock>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadMaterialPropertyBlock>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DFormat>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DFormat>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DProperties>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DProperties>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture2DResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture2DResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadTexture2D>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadTexture2D>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DFormat>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DFormat>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DProperties>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DProperties>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetTexture3DResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetTexture3DResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadTexture3D>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadTexture3D>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapFormat>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapFormat>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapProperties>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapProperties>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapData>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapData>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetCubemapResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetCubemapResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadCubemap>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadCubemap>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetRenderTextureFormat>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetRenderTextureFormat>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<RenderTextureResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<RenderTextureResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadRenderTexture>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadRenderTexture>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<SetDesktopTextureProperties>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<SetDesktopTextureProperties>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<DesktopTexturePropertiesUpdate>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<DesktopTexturePropertiesUpdate>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadDesktopTexture>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadDesktopTexture>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PointRenderBufferUpload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<PointRenderBufferUpload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PointRenderBufferConsumed>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<PointRenderBufferConsumed>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<PointRenderBufferUnload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<PointRenderBufferUnload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<TrailRenderBufferUpload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<TrailRenderBufferUpload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<TrailRenderBufferConsumed>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<TrailRenderBufferConsumed>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<TrailRenderBufferUnload>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<TrailRenderBufferUnload>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatUploadRaw>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatUploadRaw>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatUploadEncoded>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatUploadEncoded>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<GaussianSplatResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<GaussianSplatResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadGaussianSplat>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadGaussianSplat>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<LightsBufferRendererSubmission>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<LightsBufferRendererSubmission>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<LightsBufferRendererConsumed>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<LightsBufferRendererConsumed>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<ReflectionProbeRenderResult>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<ReflectionProbeRenderResult>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureLoad>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureLoad>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureUpdate>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureUpdate>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureReady>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureReady>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureChanged>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureChanged>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureProperties>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureProperties>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<VideoTextureStartAudioTrack>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<VideoTextureStartAudioTrack>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<RendererCommand>.Allocate<UnloadVideoTexture>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<RendererCommand>.Return<UnloadVideoTexture>(p, e));
        }

        // VR_ControllerState: 8 subtypes, must match .cctor registration order exactly.

        private static void SetupVRControllerState(
            List<Func<IMemoryPackerEntityPool, VR_ControllerState>> b,
            List<Action<IMemoryPackerEntityPool, VR_ControllerState>> r)
        {
            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<CosmosControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<CosmosControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<GenericControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<GenericControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<HP_ReverbControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<HP_ReverbControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<IndexControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<IndexControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<PicoNeo2ControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<PicoNeo2ControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<TouchControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<TouchControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<ViveControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<ViveControllerState>(p, e));

            b.Add(p => PolymorphicMemoryPackableEntity<VR_ControllerState>.Allocate<WindowsMR_ControllerState>(p));
            r.Add((p, e) => PolymorphicMemoryPackableEntity<VR_ControllerState>.Return<WindowsMR_ControllerState>(p, e));
        }
    }
}
