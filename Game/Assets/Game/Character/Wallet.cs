using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cashText = null;

    [SerializeField] private int _total = 0;
    public int Total { 
        get 
        { 
            return _total; 
        } 
        set 
        { 
            _total = value;
            _cashText.text = value.ToString();
        } 
    }
}