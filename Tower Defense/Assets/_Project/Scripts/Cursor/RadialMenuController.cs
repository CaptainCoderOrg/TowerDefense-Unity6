using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public sealed class RadialMenuController : MonoBehaviour
{
    private List<StructureData> _recentStructures = new();
    private RadialMenuButtonController[] _buttons;
    private static RadialMenuController _instance;
    public static RadialMenuController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<RadialMenuController>(FindObjectsInactive.Include);
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    void Awake()
    {
        _buttons = GetComponentsInChildren<RadialMenuButtonController>();
        foreach (RadialMenuButtonController b in _buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ClickButton(StructureData structureData)
    {
        gameObject.SetActive(false);
        CursorManagerController.Instance.StartBuildMode(structureData);
    }

    public void AddStructure(StructureData structureData)
    {
        _ = _recentStructures.Remove(structureData);
        _recentStructures.Insert(0, structureData);
        for (int i = 0; i < _recentStructures.Count && i < _buttons.Length; i++)
        {
            RadialMenuButtonController b = _buttons[i];
            b.gameObject.SetActive(true);
            b.Button.onClick.RemoveAllListeners();
            StructureData structure = _recentStructures[i];
            b.Icon.texture = structure.Icon;
            b.Button.onClick.AddListener(() => ClickButton(structure));
        }
    }
}