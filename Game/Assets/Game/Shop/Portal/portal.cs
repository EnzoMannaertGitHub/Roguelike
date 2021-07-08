using UnityEngine;

public class portal : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Transform _spawn;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.transform.position = _spawn.position;
    }
}