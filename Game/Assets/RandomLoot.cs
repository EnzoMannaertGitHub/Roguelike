using UnityEngine;

public class RandomLoot : MonoBehaviour
{

    [SerializeField] private Texture2D[] _bigLoot;
    [SerializeField] private Texture2D[] _standardLoot;
    [SerializeField] private int _maxMoney;
    [SerializeField] private GameObject _moneyGO;
    [SerializeField] private int _bigLootChace = 0;
    private int _heldMoney;


    // Start is called before the first frame update
    void Start()
    {

        Sprite newSprite;
        int random = Random.Range(0, 10);
        if(random > _bigLootChace) { //BIG LOOOOOOTT

            Texture2D currentTexture = _bigLoot[Random.Range(0, _bigLoot.Length)];
            newSprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0f, 0f));
            GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            //set capsule collider to sprite size / 100 because 1 unity unit equals 100 pixels

            GetComponent<CapsuleCollider2D>().size = new Vector2(newSprite.rect.width / 100, newSprite.rect.height / 100);
            Vector2 colliderPos = GetComponent<CapsuleCollider2D>().transform.position;
            Vector2 colliderSize = GetComponent<CapsuleCollider2D>().bounds.extents;

            //set sprite to collider Pos
            GetComponentInChildren<SpriteRenderer>().transform.position = colliderPos - colliderSize;

            //calcuate amount of money held
            _heldMoney = Random.Range(_maxMoney, _maxMoney * 2);

        }
        else { //SMALL LOOOOT
            Texture2D currentTexture = _standardLoot[Random.Range(0, _standardLoot.Length)];
            newSprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0f, 0f));
            
            //set correct sprite
            GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            //set transform of sprite
            GetComponentInChildren<SpriteRenderer>().transform.position -= new Vector3(0.2f, 0.5f, 0);

            //set sprite to a realistic scalle 
            Vector3 scale = (GetComponentInChildren<SpriteRenderer>().transform.localScale / 2f);
            GetComponentInChildren<SpriteRenderer>().transform.localScale = scale;

            //set capsule collider to sprite size / 100 because 1 unity unit equals 100 pixels also / 2 to scale it correctly
            GetComponent<CapsuleCollider2D>().size = new Vector2((newSprite.rect.width / 2) / 100, (newSprite.rect.height / 2) / 100);

            Vector2 colliderPos = GetComponent<CapsuleCollider2D>().transform.position;
            Vector2 colliderSize = GetComponent<CapsuleCollider2D>().bounds.extents;

            //set sprite to collider Pos
            GetComponentInChildren<SpriteRenderer>().transform.position = colliderPos - colliderSize;

            //calcuate amount of money held
            _heldMoney = Random.Range(1, _maxMoney);
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<RegularArrow>() == true)
        { 
            Destroy(other.gameObject);
            SpawnGold();
            Destroy(gameObject);
        }
    }
    private void SpawnGold()
    {
        for (int i = 0; i < _heldMoney; i++)
        {
            Vector3 pos = transform.position;
            float randX = Random.Range(pos.x - 0.15f, pos.x + 0.15f);
            Instantiate(_moneyGO, new Vector3(randX, pos.y, 0), transform.rotation);
        }
    }

}
