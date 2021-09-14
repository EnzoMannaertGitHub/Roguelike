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

    public  int EnemiesKilled
    {
        get { return _enemiesKilled; }
        set { _enemiesKilled = value; }
    }

    private int _goldCollected;

    public int GoldCollected
    {
        get { return _goldCollected; }
        set { _goldCollected = value; }
    }

    private int _goldSpent;

    public int GoldSpent
    {
        get { return _goldSpent; }
        set { _goldSpent = value; }
    }

    public void EndGame()
    {
        _endScreen.SetActive(true);
        _enemiesKilledText.SetText(_enemiesKilled.ToString());
        _goldCollectedText.SetText(_goldCollected.ToString());
        _goldSpentText.SetText((_goldSpent / 2).ToString());
        _totalText.SetText((_enemiesKilled + _goldCollected + (_goldSpent / 2)).ToString());
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}