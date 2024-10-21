using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [field: OnValueChanged("RebuildSelected")]
    [field: SerializeField]
    public TileData Tile { get; private set; }
    [field: SerializeField]
    public MeshFilter MeshFilter { get; private set;}

    public void Rebuild()
    {
        MeshFilter.mesh = Tile.Mesh;
    }

    public void RebuildSelected()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            if(obj.GetComponent<TileController>() is TileController tile)
            {
                tile.MeshFilter.mesh = tile.Tile.Mesh;
            }
        }
    }

}