using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour, IPointerEnterHandler
{
    private BuildStructurePanel _panel;
    public StructureData Structure;
    private Button _button;

    void Awake()
    {
        _panel = GetComponentInParent<BuildStructurePanel>();
        _button = GetComponentInChildren<Button>();
        
        _button.onClick.AddListener(HandleClick);
        TextMeshProUGUI price = GetComponentInChildren<TextMeshProUGUI>();
        price.text = Structure.Price.ToString();
        RawImage image = GetComponentInChildren<RawImage>();
        image.texture = Structure.Icon;
    }

    private void HandleClick()
    {
        CursorManagerController.Instance.StartBuildMode(Structure);
        _panel.Hide();
        RadialMenuController.Instance.AddStructure(Structure);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _panel.NameText.text = Structure.Name;
        _panel.DescriptionText.text = Structure.Description;
    }
}