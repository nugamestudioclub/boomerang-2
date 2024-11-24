using UnityEngine;

public abstract class AMonoAnimatorToggle : MonoBehaviour, IStateChangeable
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_stateEnabled;
    [SerializeField] private string m_stateDisabled;
    [SerializeField] private bool m_setActiveOnStart = false;

    protected bool IsActive;

    void Start()
    {
        if (m_setActiveOnStart)
        {
            SetActiveState(true);
        }
    }

    public void SetActiveState(bool state)
    {
        IsActive = state;

        m_animator.Play(IsActive ? m_stateEnabled : m_stateDisabled);

        OnState(state);
    }

    public void SetActiveStateNot(bool state)
    {
        SetActiveState(!state);
    }

    protected virtual void OnState(bool state) { }
}
