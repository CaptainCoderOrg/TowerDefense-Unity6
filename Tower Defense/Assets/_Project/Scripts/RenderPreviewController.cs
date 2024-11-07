using System;
using UnityEngine;

public class RenderPreviewController : MonoBehaviour
{
    private static RenderPreviewController _instance;
    public static RenderPreviewController Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<RenderPreviewController>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
            _instance = null;
    }
    public Vector3 PreviewPosition;
    public RenderPreview[] Preview;
    public bool ShowPreview = false;

    void Update()
    {
        if (!ShowPreview) { return; }
        foreach (RenderPreview preview in Preview)
        {
            RenderParams rp = new RenderParams(preview.material);
            Graphics.RenderMesh(rp, preview.mesh, 0, Matrix4x4.Translate(PreviewPosition + preview.Offset));
        }
    }
}

[Serializable]
public struct RenderPreview
{
    public Material material;
    public Mesh mesh;
    public Vector3 Offset;
}
