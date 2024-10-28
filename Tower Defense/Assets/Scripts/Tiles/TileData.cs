using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Data/Tile")]
public class TileData : ScriptableObject
{
    public string Name;
    public Mesh Mesh;
    public bool CanBuildWeapon;
    
}
