using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class MonoVersion : MonoBehaviour
{
    // BAEL build stamp. Grep Player.log for "BAEL-BUILD" to confirm the patched DLL is live.
    // Bump the date string on every release build so you can distinguish deployments.
    // Public so EngineLoadProgress.Awake() can reference it (guaranteed scene hook).
    public const string BAELBuild = "BAEL-BUILD 2026-04-19 25-patches net471";

    // Static constructor. Runs when Unity's script-cache scan first touches this type,
    // which happens immediately after "Begin MonoManager ReloadAssembly". No scene needed.
    static MonoVersion()
    {
        Debug.Log(BAELBuild);
    }

    // Start is called before the first frame update
    void Start()
    {
        Type type = Type.GetType("Mono.Runtime");
        if (type != null)
        {
            MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
            if (displayName != null)
                Debug.Log("MonoRuntime: " + displayName.Invoke(null, null));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
