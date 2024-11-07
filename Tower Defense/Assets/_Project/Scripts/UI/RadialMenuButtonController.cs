using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RadialMenuButtonController : MonoBehaviour
{
    public RawImage Icon { get; private set; }
    public Button Button { get; private set;}
    void Awake ()
    {
        Button = GetComponent<Button>();
        Debug.Assert(Button != null);
        Icon = GetComponentInChildren<RawImage>(true);
        Icon.gameObject.SetActive(true);
        Debug.Assert(Icon != null, "No Icon found");
    }
    
}
