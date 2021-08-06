using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{

    [SerializeField] private Texture2D[] _BigLoot;
    [SerializeField] private Texture2D[] _StandardLoot;
    [SerializeField] private int _maxMoney;
    [SerializeField] private GameObject _moneyGO;
    private int _BigLootChace = 5;
    private int _heldMoney;


    // Start is called before the first frame update
    void Start()
    {

        Sprite newSprite;
        int random = Random.Range(0, 10);
        if(random > _BigLootChace) { //BIG LOOOOOOTT

            Texture2D currentTexture = _BigLoot[Random.Range(0, _BigLoot.Length)];
            newSprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0f, 0f));
            GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            _heldMoney = Random.Range(_maxMoney, _maxMoney * 2);

        }
        else { //SMALL LOOOOT
            Texture2D currentTexture = _StandardLoot[Random.Range(0, _StandardLoot.Length)];
            newSprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0f, 0f));
            
            GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            _heldMoney = Random.Range(1, _maxMoney);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<RegularArrow>() == true)
        {
            Destroy(other.gameObject);


            for (int i = 0; i < _heldMoney; i++)
            {
                Vector3 pos = transform.position;
                float randX = Random.Range(pos.x - 0.15f, pos.x + 0.15f);
                Instantiate(_moneyGO, new Vector3(randX, pos.y, 0), transform.rotation);
            }
        }
    }

}
