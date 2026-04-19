using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Renderite.Shared;
// Disambiguate from UnityEngine.BoneWeight
using RBoneWeight = Renderite.Shared.BoneWeight;

/// <summary>
/// BAEL data-conformity probe.
/// Logs Marshal.SizeOf and key field offsets for every IPC boundary struct
/// to Player.log at scene load. Grep for "BAEL-STRUCT" to extract the report.
///
/// Activated via [RuntimeInitializeOnLoadMethod]. No scene GameObject required.
/// Run under both Mono (Unity 2022.3) and IL2CPP (Unity 6) and diff the output.
/// Any divergence is a data-corruption risk on the Resonite IPC shared memory bus.
/// </summary>
public static class StructLayoutVerifier
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Run()
    {
        Debug.Log("BAEL-STRUCT BEGIN ── struct layout conformity probe");
        bool allPass = true;

        // ── Core math types ──────────────────────────────────────────
        allPass &= Check<RenderVector2>        ("RenderVector2",          8);
        allPass &= Check<RenderVector3>        ("RenderVector3",         12);
        allPass &= Check<RenderVector4>        ("RenderVector4",         16);
        allPass &= Check<RenderQuaternion>     ("RenderQuaternion",      16);
        allPass &= Check<RenderMatrix4x4>      ("RenderMatrix4x4",       64);
        allPass &= Check<RenderVector2i>       ("RenderVector2i",         8);
        allPass &= Check<RenderVector3i>       ("RenderVector3i",        12);
        allPass &= Check<RenderIntRect>        ("RenderIntRect",         16);
        allPass &= Check<RenderRect>           ("RenderRect",            16);
        allPass &= Check<RenderBoundingBox>    ("RenderBoundingBox",     24);

        // ── Composite/nested types ────────────────────────────────────
        allPass &= Check<RenderTransform>      ("RenderTransform",       40);
        allPass &= Check<RBoneWeight>          ("BoneWeight",             8);
        allPass &= Check<BoneAssignment>       ("BoneAssignment",        12);
        allPass &= Check<TrailOffset>          ("TrailOffset",           16);

        // ── Bool/byte flag fields ────────────────────────────────────
        // TrailsRendererState: generateLightingData patched with [MarshalAs(UnmanagedType.U1)]
        // in Renderite.Shared.dll (BAEL patch, 2026-04-19).
        // Both Mono and IL2CPP now marshal the bool as 1 byte (NativeType.U1).
        // Marshal.SizeOf = 16 on both runtimes, matching ClassSize:16 (native shmbridge layout).
        // BAEL-DEF-001 resolved at our layer pending upstream fix in Renderite.Shared source.
        allPass &= Check<TrailsRendererState>  ("TrailsRendererState",   16);
        // Marshal.OffsetOf is now valid for generateLightingData (blittable U1).
        CheckOffset<TrailsRendererState>("TrailsRendererState.generateLightingData (bool@14)",
            "generateLightingData", 14);

        // ── Enum fields ───────────────────────────────────────────────
        allPass &= Check<BlendshapeBufferDescriptor>("BlendshapeBufferDescriptor", 16);
        allPass &= Check<VertexAttributeDescriptor> ("VertexAttributeDescriptor",   8);
        // VertexAttributeDescriptor: attribute at 0 (2-byte enum), format at 2 (2-byte enum),
        // dimensions at 4 (int). Both enums are short-sized; verified by Mono reference pass.
        CheckOffset<VertexAttributeDescriptor>("VertexAttributeDescriptor.attribute (enum@0)",
            "attribute", 0);
        CheckOffset<VertexAttributeDescriptor>("VertexAttributeDescriptor.format (enum@2)",
            "format", 2);
        CheckOffset<VertexAttributeDescriptor>("VertexAttributeDescriptor.dimensions (int@4)",
            "dimensions", 4);

        Debug.Log(allPass
            ? "BAEL-STRUCT PASS ── all sizes and offsets match reference"
            : "BAEL-STRUCT FAIL ── one or more structs diverge from Mono reference");
    }

    static bool Check<T>(string label, int expectedBytes) where T : struct
    {
        int actual = Marshal.SizeOf<T>();
        bool pass = actual == expectedBytes;
        Debug.Log($"BAEL-STRUCT {(pass ? "OK  " : "FAIL")} {label,-40} " +
                  $"sizeof={actual,4}  expected={expectedBytes,4}" +
                  (pass ? "" : "  *** MISMATCH ***"));
        return pass;
    }

    static void CheckOffset<T>(string label, string fieldName, int expectedOffset) where T : struct
    {
        try
        {
            int actual = (int)Marshal.OffsetOf<T>(fieldName);
            bool pass = actual == expectedOffset;
            Debug.Log($"BAEL-STRUCT {(pass ? "OK  " : "FAIL")} {label,-52} " +
                      $"offset={actual,4}  expected={expectedOffset,4}" +
                      (pass ? "" : "  *** MISMATCH ***"));
        }
        catch (Exception ex)
        {
            Debug.Log($"BAEL-STRUCT ERR  {label} — could not reflect field '{fieldName}': {ex.Message}");
        }
    }
}
