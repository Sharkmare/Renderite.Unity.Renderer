using UnityEngine;
using System.Collections;

public class CubemapRendering : MonoBehaviour
{
    public int CubemapSize = 512;

    RenderTexture tex;
    Camera camera;

    public Material SetTexture;

    public Camera displayCam;

    // Cubemap-001: cache property IDs — Unity hashes the string on every call without this.
    static readonly int _rotationID = Shader.PropertyToID("_Rotation");
    static readonly int _cubeID     = Shader.PropertyToID("_Cube");

    // Cubemap-003: environment cubemaps don't need to update at 90 Hz.
    const float UPDATE_INTERVAL = 0.1f; // 10 Hz
    float _lastUpdateTime;

    void Awake()
    {
        tex = new RenderTexture(CubemapSize, CubemapSize, 16);
        tex.dimension = UnityEngine.Rendering.TextureDimension.Cube;

        SetTexture.SetTexture(_cubeID, tex);

        camera = this.GetComponent<Camera>();
        camera.enabled = false;
    }

    void LateUpdate()
    {
        // Cubemap-003: throttle to 10 Hz.
        if (Time.time - _lastUpdateTime < UPDATE_INTERVAL)
            return;
        _lastUpdateTime = Time.time;

        // Cubemap-001 + Cubemap-002: cached ID, Rotate() instead of TRS() for a pure rotation.
        SetTexture.SetMatrix(_rotationID, Matrix4x4.Rotate(transform.rotation));
        camera.RenderToCubemap(tex);
    }
}
