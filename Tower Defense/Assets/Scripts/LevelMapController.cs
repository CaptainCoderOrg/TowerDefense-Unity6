using UnityEngine;
using NaughtyAttributes;
using System;

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
            Destroy = GameObject.DestroyImmediate;
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
                GameObject newTile = GameObject.Instantiate(Tile, Parent);
                newTile.name = $"Tile: {row}, {col}";
                newTile.transform.localPosition = new Vector3(row, 0, col);
            }
        }
    }

}