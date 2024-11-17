using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private float m_moveSpeed = 2f;
    [SerializeField] private float m_forwardLerp = 0.2f;

    public void DoMovement(Vector2 input_vector)
    {
        var forward = new Vector3(input_vector.x, 0f, input_vector.y);

        m_controller.SimpleMove(forward * m_moveSpeed);

        if (input_vector.x == 0 && input_vector.y == 0)
        {
            return;
        }

        transform.forward = Vector3.Lerp(transform.forward, forward, m_forwardLerp);
    }

    public bool IsGrounded()
    {
        return m_controller.isGrounded;
    }
}
