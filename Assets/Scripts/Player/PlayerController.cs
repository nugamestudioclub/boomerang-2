using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_movement;
    private PlayerAnimator m_animator;
    private BoomerangController m_boomerangController;

    private bool m_canMove = true;
    private bool m_canThrow = true;

    private Vector2 m_input;

    private void Awake()
    {
        m_movement = GetComponent<PlayerMovement>();
        m_animator = GetComponent<PlayerAnimator>();
    }

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

        CheckForFall();
    }

    private void CheckForFall()
    {
        if (!m_movement.IsGrounded())
        {
            m_canMove = false;

            var offset = new Vector3(m_input.x * 2.5f, 0f, m_input.y * 2.5f);
            m_animator.PlayFall(offset);

            enabled = false;

            // reload scene.
        }
    }

    private Vector2 GetMoveInput()
    {
        var vec = Vector2.zero;

        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");

        return vec.normalized;
    }
}
