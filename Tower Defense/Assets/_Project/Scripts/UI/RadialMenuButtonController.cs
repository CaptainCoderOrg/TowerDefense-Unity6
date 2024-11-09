using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RadialMenuButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage Icon { get; private set; }
    public Button Button { get; private set; }
    private StructureData _structure;
    public StructureData Structure
    {
        get => _structure;
        set
        {
            _structure = value;
            Icon.texture = _structure.Icon;
            gameObject.SetActive(true);
        }
    }
    public RadialMenuController RadialMenu => RadialMenuController.Instance;

    void Awake()
    {
        Button = GetComponent<Button>();
        Debug.Assert(Button != null);
        Icon = GetComponentInChildren<RawImage>(true);
        Icon.gameObject.SetActive(true);
        Debug.Assert(Icon != null, "No Icon found");
        Button.onClick.AddListener(HandleClick);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Structure == null) { return; }
        RadialMenu.ShowMessage(Structure.Name);
        GameManagerController.Instance.InfoText.text = $"{Structure.Description}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Structure == null) { return; }
        GameManagerController.Instance.InfoText.text = string.Empty;
    }

    public void HandleClick()
    {
        if (RadialMenu.Selected == null) { return; }
        BuildResult result = GameManagerController.Instance.TryPurchaseStructure(RadialMenu.Selected, Structure);
        if (result.IsSuccess)
        {
            GameManagerController.Instance.InfoText.text = string.Empty;
            RadialMenu.Hide();
        }
        else
        {
            RadialMenu.ShowMessage($"<color=yellow>{result.Message}</color>", 2);
        }
    }

}
