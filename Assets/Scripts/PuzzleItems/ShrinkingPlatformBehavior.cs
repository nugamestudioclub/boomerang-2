using UnityEngine;

public class ShrinkingPlatformBehavior : AMonoAnimatorToggle
{
    [SerializeField] private Collider m_collider;

    protected override void OnState(bool state)
    {
        m_collider.enabled = state;
    }
}
