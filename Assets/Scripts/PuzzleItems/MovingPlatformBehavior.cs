using System.Collections;
using UnityEngine;

public class MovingPlatformBehavior : MonoBehaviour, IStateChangeable
{
    [SerializeField] private Vector3 m_startPosition;
    [SerializeField] private Vector3 m_endPosition;
    [SerializeField] private float m_scalar = 4f;
    private float m_progress;
    private bool m_isLeftToRight = true;

    private Coroutine m_routine;

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
        var start = m_startPosition;
        var end = m_endPosition;

        while (true)
        {
            while (m_progress < 0.975f)
            {
                m_progress += Time.deltaTime * m_scalar;
                m_progress = Mathf.Clamp(m_progress, 0f, 1f);

                transform.position = Vector3.Lerp(start, end, m_progress);

                yield return null;
            }

            m_progress = 0f;
            m_isLeftToRight = !m_isLeftToRight;

            if (m_isLeftToRight)
            {
                start = m_startPosition;
                end = m_endPosition;
            }
            else
            {
                start = m_endPosition;
                end = m_startPosition;
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
