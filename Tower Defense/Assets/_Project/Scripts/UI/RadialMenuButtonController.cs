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
    private GameObject _preview;
    public StructureData Structure
    {
        get => _structure;
        set
        {
            _structure = value;
            if (_structure != null)
            {
                Initialize();
                Icon.texture = _structure.Icon;
                _priceLabel.text = _structure.Price.ToString();
                _preview = GameObject.Instantiate(Structure.Preview);
                _preview.SetActive(false);
            }
            gameObject.SetActive(true);
            
            
        }
    }
    public RadialMenuController RadialMenu => RadialMenuController.Instance;
    private TextMeshProUGUI _priceLabel;

    void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        if (Button != null) { return; }
        Button = GetComponent<Button>();
        Debug.Assert(Button != null);
        Icon = GetComponentInChildren<RawImage>(true);
        Debug.Assert(Icon != null, "No Icon found");
        Icon.gameObject.SetActive(true);        
        Button.onClick.AddListener(HandleClick);
        _priceLabel = GetComponentInChildren<TextMeshProUGUI>(true);
        Debug.Assert(_priceLabel != null, "Could not find price label");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Structure == null) { return; }
        RadialMenu.ShowMessage(Structure.Name);
        if (RadialMenu.Selected != null)
        {
            _preview.transform.position = RadialMenu.Selected.transform.position;
            _preview.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Structure == null) { return; }
        Clear();
    }

    public void Clear()
    {
        RadialMenu.ShowMessage(string.Empty);
        GameManagerController.Instance.InfoText.text = string.Empty;
        _preview.SetActive(false);
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
