using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelVariations = new List<GameObject>();
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _portal;

    private List<GameObject> _levelVariationsInLevel = new List<GameObject>();
    private List<GameObject> _platformInLevel = new List<GameObject>();

    public static LevelManager Instance { get; private set; }
    private int _levelNumber = 1;
    public int LevelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
    private GameObject _currentLevel;
    private int _nrOfPlatforms = 6;
    private bool _portalPlaced = false;
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        float yPos = 0;
        float xPos = transform.position.x;
        int floorNr = 0;
        do
        {
            CreateFloor(xPos, yPos, floorNr);

            yPos -= Random.Range(3.5f, 4.0f);
            xPos += Random.Range(2, 4);

            _nrOfPlatforms--;
            floorNr++;
        } while (_nrOfPlatforms >= 3 + _levelNumber);
    }

    public void LoadNewLevel()
    {
        foreach (var island in _levelVariationsInLevel)
            Destroy(island);
        foreach (var platform in _platformInLevel)
            Destroy(platform);

        _levelNumber++;
        _nrOfPlatforms = 6 + _levelNumber;

        float yPos = 0;
        float xPos = transform.position.x;
        int floorNr = 0;
        do
        {
            CreateFloor(xPos, yPos, floorNr);

            yPos -= Random.Range(3.5f, 4.0f);
            xPos += Random.Range(2, 4);

            _nrOfPlatforms--;
            floorNr++;
        } while (_nrOfPlatforms >= 3);

        FindObjectOfType<Shop>().ReloadShop();
    }

    private void CreateFloor(float xPos, float yPos, int floorNr)
    {
        float heightDifference = Random.Range(-.5f, .5f);
        float sizeOfIsland = 0f;
        float sizeOfNewIsland = 0f;

        Vector2 pos = new Vector2(xPos, yPos + heightDifference);
        GameObject currentIsland;
        GameObject prevIsland = null;

        for (int i = 0; i < _nrOfPlatforms; i++)
        {
            int islandIndex = Random.Range(0, _levelVariations.Count);
            _currentLevel = Instantiate(_levelVariations[islandIndex],  pos, transform.rotation);
            _levelVariationsInLevel.Add(_currentLevel);

            sizeOfIsland = _currentLevel.GetComponent<BoxCollider2D>().bounds.size.x;

            if (prevIsland)
                sizeOfNewIsland = prevIsland.GetComponent<BoxCollider2D>().bounds.size.x;

            float gap = Random.Range(1.5f, 2.5f);

            //Ceck if there needs to be a platform
            int randomNumber = Random.Range(0, _nrOfPlatforms - 3);
            if (randomNumber == 0)
            {
                    float platformHeight = Random.Range(1.5f, 2f);
                    Vector3 platformPos = new Vector3(pos.x + (sizeOfIsland / 2f) + gap, pos.y + platformHeight, pos.x);
                    _platformInLevel.Add(Instantiate(_platform, platformPos, transform.rotation));
            }

            //Check if portal needs to be placed
            if (_nrOfPlatforms <= 3 + _levelNumber)
            {
                if (!_portalPlaced)
                {
                    _portal.transform.position = new Vector2(pos.x, pos.y + 0.75f);
                    _portalPlaced = true;
                }
            }

            if (sizeOfNewIsland > sizeOfIsland)
            {
                gap +=  sizeOfNewIsland - sizeOfIsland;
            }

            pos.x += sizeOfIsland + gap;
            float newHeightDiff = Random.Range(-.5f, .5f);
            if (heightDifference < 0 && newHeightDiff < 0)
            {
                newHeightDiff *= -1;
            }
            if (heightDifference > 0 && newHeightDiff > 0)
            {
                newHeightDiff *= -1;
            }
            heightDifference = newHeightDiff;
            pos.y += heightDifference;

            prevIsland = _currentLevel;

        }
    }
}
