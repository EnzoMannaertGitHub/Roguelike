using UnityEngine;
public class AudioManager_Script : MonoBehaviour
{
    private static AudioManager_Script _instance;
    public static AudioManager_Script Instance { get { return _instance; } }
    [SerializeField] private float _soundVolume = 255f;
    public float SoundVolume { get { return _soundVolume; } set { _soundVolume = value; } }
    private bool _isMuted = false;
    [SerializeField] private AudioSource[] _gameSound;
    [SerializeField] private AudioSource[] _loopingSounds;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeVolume(float newSoundVolume)
    {
        _soundVolume = Mathf.Clamp(newSoundVolume, 0f, 255f);
        _soundVolume /= 255f;
        foreach (var s in _gameSound)
        {
                s.volume = _soundVolume;
        }
        foreach (var s in _loopingSounds)
        {
            s.volume = _soundVolume;
        }
    }

    public void Play(string name, Vector3 pos)
    {
        if (_isMuted)
            return;

        foreach (var s in _gameSound)
        {
            if (!s.isPlaying)
            {
                s.clip = Resources.Load<AudioClip>($"Sound/{ name }");
                s.volume = _soundVolume;
                if (s.clip == null)
                {
                    Debug.LogError($"Audioclip file not find: {name}");
                    return;
                }

                s.Play();
                return;
            }
        }
    }
}