using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Endscreen : MonoBehaviour
{
    [SerializeField] GameObject _endScreen;
    [SerializeField] TextMeshProUGUI _enemiesKilledText;
    [SerializeField] TextMeshProUGUI _goldCollectedText;
    [SerializeField] TextMeshProUGUI _goldSpentText;
    [SerializeField] TextMeshProUGUI _totalText;

    private int _enemiesKilled;

    public int EnemiesKilled
    {
        get { return _enemiesKilled; }
        set { _enemiesKilled = value; }
    }

    private float _goldCollected;

    public float GoldCollected
    {
        get { return _goldCollected; }
        set { _goldCollected = value; }
    }

    private float _goldSpent;

    public float GoldSpent
    {
        get { return _goldSpent; }
        set { _goldSpent = value; }
    }

    public void EndGame()
    {
        _endScreen.SetActive(true);
        _enemiesKilledText.SetText(_enemiesKilled.ToString());
        _goldCollectedText.SetText(Mathf.FloorToInt(_goldCollected).ToString());
        _goldSpentText.SetText((Mathf.RoundToInt(_goldSpent) / 2).ToString());
        _totalText.SetText((_enemiesKilled + _goldCollected + (_goldSpent / 2)).ToString());
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
