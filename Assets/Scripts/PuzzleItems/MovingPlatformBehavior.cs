using System.Collections;
using UnityEngine;

public class MovingPlatformBehavior : MonoBehaviour, IStateChangeable
{
    [SerializeField] private Vector3 m_startPosition;
    [SerializeField] private Vector3 m_endPosition;
    private Vector3 m_pos;
    private Vector3 m_dest;

    [SerializeField] private float m_scalar = 4f;
    private float m_progress;
    private bool m_isLeftToRight = true;

    private Coroutine m_routine;

    void Awake()
    {
        m_pos = m_startPosition;
        m_dest = m_endPosition;
    }

    public void SetActiveState(bool state)
    {
        if (!state && m_routine != null)
        {
            StopCoroutine(m_routine);
            m_routine = null;
        }
        else if (state)
        {
            m_routine = StartCoroutine(IEDoBehavior());
        }
    }

    public void SetActiveStateNot(bool state) => SetActiveState(!state);

    private IEnumerator IEDoBehavior()
    {
        while (true)
        {
            while (m_progress < 0.975f)
            {
                m_progress += Time.deltaTime * m_scalar;
                m_progress = Mathf.Clamp(m_progress, 0f, 1f);

                transform.position = Vector3.Lerp(m_pos, m_dest, m_progress);

                yield return null;
            }

            m_progress = 0f;
            m_isLeftToRight = !m_isLeftToRight;

            if (m_isLeftToRight)
            {
                m_pos = m_startPosition;
                m_dest = m_endPosition;
            }
            else
            {
                m_pos = m_endPosition;
                m_dest = m_startPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_startPosition, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_endPosition, 0.5f);
    }
}
