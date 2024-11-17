using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_movement;
    private PlayerAnimator m_animator;
    private BoomerangController m_boomerangController;

    private bool m_canMove = true;
    private bool m_canThrow = true;
    private bool m_doCheckForFall = false;

    private Vector2 m_input;

    private void Awake()
    {
        m_movement = GetComponent<PlayerMovement>();
        m_animator = GetComponent<PlayerAnimator>();

        Invoke(nameof(FinishInit), 0.5f);
    }

    private void FinishInit() => m_doCheckForFall = true;

    private void Update()
    {
        m_input = GetMoveInput();

        if (m_canThrow && Input.GetAxis("Jump") > 0)
        {

        } 

        if (m_canMove)
        {
            m_movement.DoMovement(m_input);
        }

        m_animator.DoAnimation(m_input);

        // removes the bug that occurs on some computers with the insta-fall state.
        // delays the check until everything is set up and ready to go.
        if (m_doCheckForFall)
        {
            CheckForFall();
        }
    }

    private void CheckForFall()
    {
        if (!m_movement.IsGrounded())
        {
            m_doCheckForFall = false;
            m_canMove = false;

            var offset = new Vector3(m_input.x * 3.5f, 0f, m_input.y * 3.5f);
            m_animator.PlayFall(offset);

            Invoke(nameof(DelayInactive), 1f);

            // reload scene.
        }
    }

    private void DelayInactive() => gameObject.SetActive(false);

    private Vector2 GetMoveInput()
    {
        var vec = Vector2.zero;

        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");

        return vec.normalized;
    }
}
