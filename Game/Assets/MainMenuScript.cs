using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private PlayerMovement _playerMov;
    private bool _startTransition = false;
    private float _elapsedSec = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_startTransition)
        {
            _elapsedSec += Time.deltaTime;
            if (_elapsedSec >= 2f)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || collision.gameObject.layer != 0)
            return;

            Destroy(_cam);
            _playerMov.IsEnteringCave = true;
            _startTransition = true;
    }

    public void QuitGame()
    {
        QuitGame();
    }
}
