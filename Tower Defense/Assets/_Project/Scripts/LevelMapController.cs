using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelMapController : MonoBehaviour
{
    [field: SerializeField]
    public Transform Parent { get; private set; }
    [field: SerializeField]
    public GameObject Tile { get; private set; }
    [field: SerializeField]
    public int Width { get; private set; }
    [field: SerializeField]
    public int Height { get; private set; }

    [Button("Destroy Tiles")]
    public void DestroyTiles()
    {
        Action<GameObject> Destroy = GameObject.Destroy;
        #if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Destroy = GameObject.DestroyImmediate;
            }
        #endif
        for (int ix = Parent.childCount - 1; ix >= 0; ix--)
        {
            Destroy.Invoke(Parent.GetChild(ix).gameObject);
        }
    }

    [Button("PlaceTiles")]
    public void PlaceTiles()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                Func<GameObject, GameObject> Instantiate = GameObject.Instantiate;
                #if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        static GameObject EditorInstantiate(GameObject tile)
                        {
                            return (GameObject)PrefabUtility.InstantiatePrefab(tile);
                        }
                        Instantiate = EditorInstantiate;
                    }
                #endif
                GameObject newTile = Instantiate(Tile);
                newTile.transform.parent = Parent;
                newTile.name = $"Tile: {row}, {col}";
                newTile.transform.localPosition = new Vector3(row, 0, col);
            }
        }
    }

    [Button("Rebuild")]
    public void Rebuild()
    {
        for (int ix = Parent.childCount - 1; ix >= 0; ix--)
        {
            TileController tile = Parent.GetChild(ix).gameObject.GetComponent<TileController>();
            tile?.Rebuild();
        }
    }

    

}