using UnityEngine;

[CreateAssetMenu]
public class PanelData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public GameObject PanelPrefab { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; private set;}
}