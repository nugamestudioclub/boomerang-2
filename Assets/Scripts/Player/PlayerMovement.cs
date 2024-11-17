using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private float m_moveSpeed = 2f;
    [SerializeField] private float m_forwardLerp = 0.2f;

    [Space(10)]

    [SerializeField] private Vector3 m_sphereOffset;
    [SerializeField] private float m_raydius;

    [SerializeField] private LayerMask m_mask;
    private readonly Collider[] m_singletonColl = new Collider[1];

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
        m_singletonColl[0] = null;
        return Physics.OverlapSphereNonAlloc(transform.position + m_sphereOffset, m_raydius, m_singletonColl, m_mask) > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + m_sphereOffset, m_raydius);
    }
}
