using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cashText = null;
    [SerializeField] private int _total = 0;
    [SerializeField] private TextMeshProUGUI _addText;
    private Transform _startPos;
    private bool _addAnimationStarted = false;
    private float _elapsedAnimSec = 0f;
    private float _maxAnimSec = 3f;
    private Color _startColor;
    private void Start()
    {
        _startPos = _addText.transform;
        _startColor = _addText.color;
    }
    public int Total 
    { 
        get 
        { 
            return _total; 
        } 

    }

    private void Update()
    {
        if (_addAnimationStarted)
        {
            _elapsedAnimSec += Time.deltaTime;
            var pos = _addText.transform.position;
            _addText.transform.position = new Vector3(pos.x, pos.y - 10 * Time.deltaTime, pos.y);
            var col = _addText.color;
            _addText.color = new Color(col.r, col.g, col.b, col.a - 0.001f);
            if (_elapsedAnimSec >= _maxAnimSec)
            {
                _addAnimationStarted = false;
                _elapsedAnimSec = 0;
                _addText.color = new Color(col.r, col.g, col.b, 0f);
                _addText.transform.position = _startPos.position;
            }
        }
    }

    public void AddCash(int amount)
    {
        Endscreen end = GetComponent<Health>().End;
        if (amount > 0)
            end.GoldCollected += amount;
        else
            end.GoldSpent += amount;

        _total += amount;
        _cashText.text = _total.ToString();

        if (_addAnimationStarted)
        {
            if (amount < 0)
                _addText.text = $"{(int.Parse(_addText.text) + amount).ToString()}";
            else
                _addText.text = $"+{(int.Parse(_addText.text) + amount).ToString()}";
        }
        else
        {
            _addAnimationStarted = true;
            if (amount < 0)
            {
                _addText.color = Color.red;
                _addText.text = $"{amount.ToString()}";
            }
            else
            {
                _addText.color = _startColor; ;
                _addText.text = $"+{amount.ToString()}";
            }
            var col = _addText.color;
            _addText.color = new Color(col.r, col.g, col.b, 1f);
        }
    }
}