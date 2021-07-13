using UnityEngine;

public class SkeletonAnimationEvents : MonoBehaviour
{
    [SerializeField] private Monster _monster;

    public void AnimationDeath()
    {

    }

    public void EndAttack()
    {
        Skeleton skeleton = (Skeleton)(_monster._breed);
        skeleton.StopAttack();
    }
}
