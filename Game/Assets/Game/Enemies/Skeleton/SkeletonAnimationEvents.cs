using UnityEngine;

public class SkeletonAnimationEvents : MonoBehaviour
{
    [SerializeField] private Skeleton _skeletonBreed;

    public void AnimationDeath()
    {

    }

    public void EndAttack()
    {
        _skeletonBreed.StopAttack();
    }
}
