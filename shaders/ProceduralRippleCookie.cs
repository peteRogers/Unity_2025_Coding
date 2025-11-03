using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(Light))]
public class ProceduralRippleCookie : MonoBehaviour
{
    [Header("Render Target")]
    public int resolution = 1024;               // 512–2048 typical
    public RenderTextureFormat rtFormat = RenderTextureFormat.ARGB32;

    [Header("Ripple Controls")]
    [Range(0.1f, 10f)] public float tiling = 3.0f;            // pattern scale
    [Range(0f, 2f)]  public float speed = 0.6f;               // animation speed
    [Range(0f, 2f)]  public float swirl = 0.25f;              // adds gentle UV swirl
    [Range(0.1f, 6f)]public float sharpness = 2.2f;           // caustic contrast (power)
    [Range(0f, 1f)]  public float brightness = 0.9f;          // overall cookie brightness
    [Range(0f, 1f)]  public float minDark = 0.05f;            // darkest value clamp
    [Range(0f, 180f)] public float patternAngle = 30f;        // degrees

    RenderTexture _rt;
    Material _mat;
    Light _light;

    static readonly int _Tiling     = Shader.PropertyToID("_Tiling");
    static readonly int _Speed      = Shader.PropertyToID("_Speed");
    static readonly int _Swirl      = Shader.PropertyToID("_Swirl");
    static readonly int _Sharpness  = Shader.PropertyToID("_Sharpness");
    static readonly int _Brightness = Shader.PropertyToID("_Brightness");
    static readonly int _MinDark    = Shader.PropertyToID("_MinDark");
    static readonly int _AngleRad   = Shader.PropertyToID("_AngleRad");

    void OnEnable()
    {
        _light = GetComponent<Light>();
        if (_light.type == LightType.Directional || _light.type == LightType.Spot || _light.type == LightType.Point)
        {
            if (_mat == null) _mat = new Material(Shader.Find("Hidden/RippleCausticsCookie"));
            AllocateRT();
        }
        else
        {
            Debug.LogWarning("ProceduralRippleCookie: This script is intended for Directional/Spot/Point lights.");
        }
    }

    void OnDisable()
    {
        if (_mat) DestroyImmediate(_mat);
        if (_rt)  ReleaseRT();
    }

    void AllocateRT()
    {
        ReleaseRT();
        _rt = new RenderTexture(resolution, resolution, 0, rtFormat, RenderTextureReadWrite.Linear)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear,
            useMipMap = false,
            anisoLevel = 0,
            name = "RippleCookieRT"
        };
        _rt.Create();
    }

    void ReleaseRT()
    {
        if (_rt)
        {
            if (_light && _light.cookie == _rt) _light.cookie = null;
            _rt.Release();
            DestroyImmediate(_rt);
            _rt = null;
        }
    }

    void Update()
    {
        // Maintain cookie size for Directional lights (the only light type that supports Light.cookieSize)
        

        if (_mat == null || _rt == null) return;

        // Push parameters
        _mat.SetFloat(_Tiling,     tiling);
        _mat.SetFloat(_Speed,      speed);
        _mat.SetFloat(_Swirl,      swirl);
        _mat.SetFloat(_Sharpness,  sharpness);
        _mat.SetFloat(_Brightness, brightness);
        _mat.SetFloat(_MinDark,    minDark);
        _mat.SetFloat(_AngleRad,   patternAngle * Mathf.Deg2Rad);

        // Render to the RT (full-screen)
        Graphics.Blit(null, _rt, _mat, 0);

        // Ensure the light always references our RT (Edit Mode + Play Mode)
        if (_light.cookie != _rt)
            _light.cookie = _rt;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Force continuous updates in Edit Mode
        if (!Application.isPlaying)
        {
            if (_mat == null) _mat = new Material(Shader.Find("Hidden/RippleCausticsCookie"));
            if (_rt == null || !_rt.IsCreated()) AllocateRT();
            // Re-render the cookie each SceneView repaint
            _mat.SetFloat(_Tiling, tiling);
            _mat.SetFloat(_Speed, speed);
            _mat.SetFloat(_Swirl, swirl);
            _mat.SetFloat(_Sharpness, sharpness);
            _mat.SetFloat(_Brightness, brightness);
            _mat.SetFloat(_MinDark, minDark);
            _mat.SetFloat(_AngleRad, patternAngle * Mathf.Deg2Rad);
            Graphics.Blit(null, _rt, _mat, 0);
            if (_light && _light.cookie != _rt) _light.cookie = _rt;
        }
    }
#endif

#if UNITY_EDITOR
    // Hot-change resolution in editor
    void OnValidate()
    {
        resolution = Mathf.ClosestPowerOfTwo(Mathf.Clamp(resolution, 128, 4096));
        if (Application.isPlaying && _rt && (_rt.width != resolution || _rt.format != rtFormat))
            AllocateRT();
    }
#endif
}