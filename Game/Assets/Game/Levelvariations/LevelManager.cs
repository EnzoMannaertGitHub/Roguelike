using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelVariations;

    public static LevelManager Instance { get; private set; }
    private int _levelNumber = 1;
    public int LevelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
    private GameObject _currentLevel;
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        int random = Random.Range(0, _levelVariations.Count);
        _currentLevel = Instantiate(_levelVariations[random], _levelVariations[random].transform.position, _levelVariations[random].transform.rotation);
    }

    public void LoadNewLevel()
    {
        Destroy(_currentLevel);

        int random = Random.Range(0, _levelVariations.Count);
        _currentLevel = Instantiate(_levelVariations[random], _levelVariations[random].transform.position, _levelVariations[random].transform.rotation);
        _levelNumber++;
    }
}
