using UnityEngine;

public abstract class AMonoAnimatorToggle : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_stateEnabled;
    [SerializeField] private string m_stateDisabled;

    protected bool IsActive;

    public void SetActiveState(bool state)
    {
        IsActive = state;

        m_animator.Play(IsActive ? m_stateEnabled : m_stateDisabled);

        OnState(state);
    }

    protected virtual void OnState(bool state) { }
}
