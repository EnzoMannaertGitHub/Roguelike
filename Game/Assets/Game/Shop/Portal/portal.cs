using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    private GameObject _player;
    private void Start()
    {
        _player = FindObjectOfType<CharacterController2D>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.transform.position = new Vector3(0,0,0);
        SceneManager.LoadScene(1);
    }
}