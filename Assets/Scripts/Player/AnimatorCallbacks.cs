using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCallbacks : MonoBehaviour
{
    [SerializeField] private GameObject m_animatingBoomerang;
    [SerializeField] private Animator m_animator;

    public void SetBoomerangState(int state)
    {
        m_animatingBoomerang.SetActive(state == 1);
    }

    public void SetStateInt(int state)
    {
        m_animator.SetInteger("State", state);
    }
}
