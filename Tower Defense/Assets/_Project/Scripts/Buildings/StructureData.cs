using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class StructureData : ScriptableObject
{
    public string Name;
    [TextArea(1, 5)]
    public string Description;
    public int Price;
    public Texture Icon;
    public StructureController Prefab;
    public GameObject Preview;
    public bool RequiresPower = true;
}
