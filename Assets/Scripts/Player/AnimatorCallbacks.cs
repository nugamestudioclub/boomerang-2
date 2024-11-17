using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCallbacks : MonoBehaviour
{
    [SerializeField] private GameObject m_animatingBoomerang;

    public void SetBoomerangState(int state)
    {
        m_animatingBoomerang.SetActive(state == 1);
    }
}
