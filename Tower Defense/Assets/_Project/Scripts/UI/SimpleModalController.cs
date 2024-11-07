using UnityEngine;

public class SimpleModalController : MonoBehaviour
{
    void Awake()
    {
        GameManagerController.Instance.OnMenuChanged.AddListener(MenuChanged);
    }

    private void MenuChanged(GameObject Menu)
    {
        if (Menu == this.gameObject) { return; }
        gameObject.SetActive(false);
    }
}
