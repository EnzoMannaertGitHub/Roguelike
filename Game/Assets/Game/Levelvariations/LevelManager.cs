using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelVariations;

    public static LevelManager Instance { get; private set; }
    private int _levelNumber = 1;
    public int LevelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
    private GameObject _currentLevel;
    private int _nrOfPlatforms = 6;
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        float yPos = 0;
        float xPos = transform.position.x;
        do
        {
            CreateFloor(xPos, yPos);

            yPos -= Random.Range(4, 5);
            xPos += Random.Range(2, 4);

            _nrOfPlatforms--;

        } while (_nrOfPlatforms >= 3);
    }

    public void LoadNewLevel()
    {
        Destroy(_currentLevel);
        _levelNumber++;

        FindObjectOfType<Shop>().ReloadShop();
    }

    private void CreateFloor(float xPos, float yPos)
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
            sizeOfIsland = _currentLevel.GetComponent<BoxCollider2D>().bounds.size.x;

            if (prevIsland)
                sizeOfNewIsland = prevIsland.GetComponent<BoxCollider2D>().bounds.size.x;


            float gap = Random.Range(1.5f, 2.5f);

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
