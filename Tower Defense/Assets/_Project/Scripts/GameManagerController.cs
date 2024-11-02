using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    private static GameManagerController _instance;
    public static GameManagerController Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<GameManagerController>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
            _instance = null;
    }
    
    public TextMeshProUGUI MoneyText;
    
    [SerializeField]
    private int _money = 500;
    public int Money 
    { 
        get => _money; 
        set
        {
            _money = value;
            UpdateMoney();
        }
    }

    [Button("Update Money")]
    public void UpdateMoney()
    {
        MoneyText.text = Money.ToString();
    }

    void Awake()
    {
        UpdateMoney();
    }

    void OnValidate()
    {
        UpdateMoney();
    }

    internal void TriggerGameOver()
    {
        Debug.Log("Game Over!");
    }
}
