using System;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    // public TextMeshProUGUI PriceText;
    // public RawImage Image;
    public StructureData Structure;
    private Button _button;

    void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        CursorManagerController.Instance.StartBuildMode(Structure);
    }

}
