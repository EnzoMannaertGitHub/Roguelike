using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelVariations = new List<GameObject>();
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _supportPlatform;
    [SerializeField] private GameObject _portal;
    [SerializeField] private GameObject _randomLoot;
    [SerializeField] private GameObject _bottomDecoration;

    private List<GameObject> _levelVariationsInLevel = new List<GameObject>();
    private List<GameObject> _platformInLevel = new List<GameObject>();

    public static LevelManager Instance { get; private set; }
    private int _levelNumber = 1;
    public int LevelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
    private GameObject _currentLevel;
    private int _nrOfPlatforms = 6;
    private bool _portalPlaced = false;
    private bool _platformPlaced = false;
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
        _portalPlaced = false;

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

            yPos -= Random.Range(2.6f, 2.75f);
            xPos += Random.Range(2, 3);

            _nrOfPlatforms--;
            floorNr++;
            _platformPlaced = false;
        } while (_nrOfPlatforms >= 3);
    }

    private void CreateFloor(float xPos, float yPos, int floorNr)
    {
        float heightDifference = Random.Range(-.25f, .25f);
        float sizeOfIsland = 0f;
        float sizeOfPrevIsland = 0f;
        Vector2 pos = new Vector2(xPos, yPos + heightDifference);
        GameObject prevIsalnd = null;

        for (int i = 0; i < _nrOfPlatforms; i++)
        {
            int islandIndex = Random.Range(0, _levelVariations.Count);
            _currentLevel = Instantiate(_levelVariations[islandIndex], pos, transform.rotation);
            _levelVariationsInLevel.Add(_currentLevel);

            HandleIslandPlacement(ref sizeOfIsland, ref sizeOfPrevIsland, ref pos, prevIsalnd, i);

            PlatformPlacement(pos, sizeOfIsland, i);

            PortalPlacement(pos, i);

            HandleHeightDifference(ref heightDifference, ref pos);

            LootPlacement();

            prevIsalnd = _currentLevel;
        }
    }

    private void PlatformPlacement(Vector3 pos, float sizeOfIsland, int i)
    {
        //Ceck if there needs to be a platform
        if (i == _nrOfPlatforms - 1 || _nrOfPlatforms <= 3)
            return;

        bool needPlatform = false;
        if (i == _nrOfPlatforms -2 && !_platformPlaced)
        {
            needPlatform = true;
        }

        float gap = Random.Range(.75f, 1.25f);
        int randomNumber = Random.Range(0, _nrOfPlatforms - 3);
        if (randomNumber == 0 || needPlatform)
        {
            float platformHeight = Random.Range(.5f, .75f);
            Vector3 platformPos = new Vector3(pos.x + (sizeOfIsland / 2f) + gap, pos.y - platformHeight, pos.x);
            _platformInLevel.Add(Instantiate(_platform, platformPos, transform.rotation));

            platformPos.y -= 1f;
            int random = Random.Range(0, 2);
            if (random == 1)
                platformPos.x -= 0.5f;
            else
                platformPos.x += 0.5f;

            Instantiate(_supportPlatform, platformPos, transform.rotation);
            _platformPlaced = true;
        }
    }

    private void PortalPlacement(Vector3 pos, int i)
    {
        //Check if portal needs to be placed
        if (_nrOfPlatforms <= 3)
        {
            if (!_portalPlaced)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0 || i == 2)
                {
                    _portal.transform.position = new Vector2(pos.x, pos.y + 0.75f);
                    _portalPlaced = true;
                }
            }
        }
    }

    private void HandleHeightDifference(ref float heightDifference, ref Vector2 pos)
    {
        float newHeightDiff = Random.Range(-.25f, .25f);
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

    private void HandleIslandPlacement(ref float sizeOfIsland, ref float sizeOfPrevIsland,ref Vector2 pos, GameObject prevIsalnd, int i)
    {
        float gap = Random.Range(2f, 2.5f);

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
        
        if (i == _nrOfPlatforms - 1)
        {
            var decorationPos = _bottomDecoration.transform.position;
            _bottomDecoration.transform.position = new Vector3(decorationPos.x, pos.y - 2.3f - 16.5f, 0);
            return;
        }

        float platformHeight = Random.Range(.5f, .75f);
        Vector3 platformPos = new Vector3(pos.x + (sizeOfIsland / 2f) + gap, pos.y + platformHeight, pos.x);
        _platformInLevel.Add(Instantiate(_platform, platformPos, transform.rotation));

    }

    private void LootPlacement()
    {
        int randomNumber = Random.Range(0, 5);
        if (randomNumber == 1)
        {
            List<GameObject> locations = _currentLevel.GetComponent<Island>().LootSpawns;
            int locationIndex = Random.Range(0, locations.Count);
            Vector3 pos = locations[locationIndex].transform.position;
            pos.y += .2f;
            Instantiate(_randomLoot, pos, transform.rotation);
        }
    }
}