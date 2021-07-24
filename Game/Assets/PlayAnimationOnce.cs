using UnityEngine;

public class PlayAnimationOnce : MonoBehaviour
{
    private Animator _animator;
    private float _length = 0;
    private float _elapsedSec = 0;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("PlayAnimationOnce Animator not found!");
        _length = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length -0.05f;
    }
    private void Update()
    {
        _elapsedSec += Time.deltaTime;
        if (_elapsedSec >= _length)
            Destroy(gameObject);
    }
}