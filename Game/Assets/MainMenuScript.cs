using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private PlayerMovement _playerMov;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _spawn;
    [SerializeField] private GameObject _canvas;
    private bool _startTransition = false;
    private float _elapsedSec = 0f;

    // Update is called once per frame
    void Update()
    {
        if (_startTransition)
        {
            _elapsedSec += Time.deltaTime;
            if (_elapsedSec >= 2f)
            {
                SceneManager.LoadScene(1);
                _playerMov.IsEnteringCave = false;
                _canvas.SetActive(true);
                _cam.Follow = _player.transform;
            }
            return;
        }

        if (_player.transform.position.y <= 0)
        {
            _player.transform.position = _spawn.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || collision.gameObject.layer != 0)
            return;

            _cam.Follow = null;
            _playerMov.IsEnteringCave = true;
            _startTransition = true;
    }

    public void QuitGame()
    {
        QuitGame();
    }
}
