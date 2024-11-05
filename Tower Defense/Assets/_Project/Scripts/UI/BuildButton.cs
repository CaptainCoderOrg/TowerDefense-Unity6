using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour, IPointerEnterHandler
{
    public TabPanelController Panel => TabPanelController.Instance;
    public StructureData Structure;
    private Button _button;

    void Awake()
    {
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
        TabPanelController.Instance.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Panel.NameText.text = Structure.Name;
        Panel.DescriptionText.text = Structure.Description;
    }
}