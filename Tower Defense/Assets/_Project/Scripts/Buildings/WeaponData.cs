using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public int Price;
    public Texture Icon;
    public TurretController Prefab;
}
