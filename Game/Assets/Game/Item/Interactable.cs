using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private Transform _interactionTransform;
    [SerializeField] private Transform _player;

    public virtual void Interact()
    {
        //This method is meant to be overriden
    }

    private void Update()
    {
        if (_player == null)
            return;

        float distance = Vector3.Distance(_player.position, _interactionTransform.position);
        if(distance <= _radius)
        {
            Interact();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_interactionTransform == null)
            _interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionTransform.position, _radius);
    }
}
