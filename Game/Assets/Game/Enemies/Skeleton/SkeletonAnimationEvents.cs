using UnityEngine;

public class SkeletonAnimationEvents : MonoBehaviour
{
    [SerializeField] private Skeleton _skeletonBreed;
    [SerializeField] private SkeletonAttack _skeletonAttack;

    public void AnimationDeath()
    {

    }

    public void EndAttack()
    {
        _skeletonBreed.StopAttack();
    }

    public void CheckAttackHits()
    {
        _skeletonAttack.CheckPlayerHit();
    }
}
