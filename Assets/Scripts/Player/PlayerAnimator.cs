using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public enum Animations
    {
        Idle,
        Move,
        PoseThrow,
        Throw,
        Fall
    }

    [SerializeField] private Animator m_animator;

    private Vector2 m_previousInput;
    private int m_stateHash;

    private void Awake()
    {
        m_stateHash = Animator.StringToHash("State");
    }

    public void DoMoveRelatedAnimation(Vector2 input_vector)
    {
        if (m_previousInput == input_vector) return;
        m_previousInput = input_vector;

        m_animator.SetInteger(m_stateHash, input_vector == Vector2.zero ? (int)Animations.Idle : (int)Animations.Move);
    }

    public void ForcePlay(Animations anim)
    {
        m_animator.SetInteger(m_stateHash, (int)anim);
    }

    public void PlayFall(Vector3 offset)
    {
        m_animator.Play("Armature|fall"); // force by string here bc using a state messes up things
        StartCoroutine(IELerpToFallPosition(transform.position + offset));
    }

    private IEnumerator IELerpToFallPosition(Vector3 pos)
    {
        float progress = 0f;

        while (progress < 0.95f)
        {
            progress += Time.deltaTime * 4f;

            transform.position = Vector3.Slerp(transform.position, pos, progress);

            yield return null;
        }

        transform.position = pos;
    }
}
