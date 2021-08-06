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

        CreateLevel();
    }

    public void LoadNewLevel()
    {
        foreach (var island in _levelVariationsInLevel)
            Destroy(island);
        foreach (var platform in _platformInLevel)
            Destroy(platform);

        _levelNumber++;
        _nrOfPlatforms = 6 + _levelNumber;

        CreateLevel();

        FindObjectOfType<Shop>().ReloadShop();
    }

    private void CreateLevel()
    {
        float yPos = 0;
        float xPos = transform.position.x;
        int floorNr = 0;
        do
        {
            CreateFloor(xPos, yPos, floorNr);

            yPos -= Random.Range(2f, 2.5f);
            xPos += Random.Range(2, 3);

            _nrOfPlatforms--;
            floorNr++;
        } while (_nrOfPlatforms >= 3 + _levelNumber);
    }

    private void CreateFloor(float xPos, float yPos, int floorNr)
    {
        float heightDifference = Random.Range(-.5f, .5f);
        float sizeOfIsland = 0f;
        float sizeOfPrevIsland = 0f;
        Vector2 pos = new Vector2(xPos, yPos + heightDifference);
        GameObject prevIsalnd = null;

        for (int i = 0; i < _nrOfPlatforms; i++)
        {
            int islandIndex = Random.Range(0, _levelVariations.Count);
            _currentLevel = Instantiate(_levelVariations[islandIndex], pos, transform.rotation);
            _levelVariationsInLevel.Add(_currentLevel);

            HandleIslandPlacement(ref sizeOfIsland, ref sizeOfPrevIsland, ref pos, prevIsalnd);

            PlatformPlacement(pos, sizeOfIsland);

            PortalPlacement(pos);

            HandleHeightDifference(ref heightDifference, ref pos);

            prevIsalnd = _currentLevel;
        }
    }

    private void PlatformPlacement(Vector3 pos, float sizeOfIsland)
    {
        //Ceck if there needs to be a platform
        int randomNumber = Random.Range(0, _nrOfPlatforms - 3);
        if (randomNumber == 0)
        {
            float platformHeight = Random.Range(1f, 1.25f);
            Vector3 platformPos = new Vector3(pos.x + (sizeOfIsland / 2f), pos.y + platformHeight, pos.x);
            _platformInLevel.Add(Instantiate(_platform, platformPos, transform.rotation));
        }
    }

    private void PortalPlacement(Vector3 pos)
    {
        //Check if portal needs to be placed
        if (_nrOfPlatforms <= 3 + _levelNumber)
        {
            if (!_portalPlaced)
            {
                _portal.transform.position = new Vector2(pos.x, pos.y + 0.75f);
                _portalPlaced = true;
            }
        }
    }

    private void HandleHeightDifference(ref float heightDifference, ref Vector2 pos)
    {
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
    }

    private void HandleIslandPlacement(ref float sizeOfIsland, ref float sizeOfPrevIsland,ref Vector2 pos, GameObject prevIsalnd)
    {
        float gap = Random.Range(1f, 1.25f);

        sizeOfIsland = _currentLevel.GetComponent<BoxCollider2D>().bounds.size.x;
        if (prevIsalnd)
        {
            sizeOfPrevIsland = prevIsalnd.GetComponent<BoxCollider2D>().bounds.size.x;
            if (prevIsalnd.GetComponent<BoxCollider2D>().bounds.size.x > sizeOfIsland)
            {
                pos.x += sizeOfPrevIsland + gap;
                _currentLevel.transform.position = pos;
            }
            else
            {
                pos.x += sizeOfIsland + gap;
                _currentLevel.transform.position = pos;
            }
        }
    }
}