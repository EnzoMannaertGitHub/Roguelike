using UnityEngine;
using System.Collections.Generic;
public class volumeBox : MonoBehaviour
{
    [SerializeField] private int _percentage;
    public int Percentage { get { return _percentage; } }

    [SerializeField] private List<volumeBox> _volumes;
    private bool _isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        if (_percentage == 0)
        {
            AudioManager_Script.Instance.ChangeVolume(0);

            foreach (volumeBox v in _volumes)
            {
                v.SetNotActive();
            }

            Destroy(collision.gameObject);

            return;
        }

        _isActive = !_isActive;
        if (!_isActive)
        {
            SetSctive();
            AudioManager_Script.Instance.ChangeVolume((_percentage / 100f) * 255f);
        }

        foreach (volumeBox v in _volumes)
        {
            if (v.Percentage < _percentage)
                v.SetSctive();
            else
                v.SetNotActive();
        }

        Destroy(collision.gameObject);
    }

    public void SetSctive()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
    public void SetNotActive()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
    }
}
