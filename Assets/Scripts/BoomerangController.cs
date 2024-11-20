using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    [SerializeField] private float m_boomerangMoveSpeed;
    [SerializeField] private Transform m_player;

    [Space(10)]

    [SerializeField] private ParticleSystem m_twirlSystem;
    [SerializeField] private ParticleSystem m_holdSystem;
    [SerializeField] private ParticleSystem m_catchSystem;
    private Rigidbody m_rigidbody;

    private Vector3[] m_points;
    private Action OnComplete;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3[] points, Action on_complete)
    {
        gameObject.SetActive(true);

        transform.position = points[0];

        m_points = points;

        OnComplete = on_complete;

        StartCoroutine(IEMoveBoomerang());
    }

    private IEnumerator IEMoveBoomerang()
    {
        int index = 1;

        m_rigidbody.position = m_points[0];

        while (index < m_points.Length)
        {
            var dir = (m_points[index] - m_rigidbody.position).normalized;

            float dist = Vector3.Distance(m_rigidbody.position, m_points[index]);

            // get initial values for movement
            while (dist > 0.25f)
            {
                float step_distance = m_boomerangMoveSpeed * Time.fixedDeltaTime;
                float move_distance = Mathf.Min(dist, step_distance);

                m_rigidbody.MovePosition(m_rigidbody.position + move_distance * dir);

                yield return new WaitForFixedUpdate();

                dist = Vector3.Distance(m_rigidbody.position, m_points[index]);
            }

            index++;
        }

        yield return new WaitWhile(() => Input.GetButton("Submit"));

        yield return IEFlyBack();
    }

    private IEnumerator IEFlyBack()
    {
        float dist = Vector3.Distance(m_rigidbody.position, m_player.position);

        // get initial values for movement
        while (dist > 0.25f)
        {
            var dir = (m_player.position - m_rigidbody.position).normalized;

            float step_distance = m_boomerangMoveSpeed * Time.fixedDeltaTime;
            float move_distance = Mathf.Min(dist, step_distance);

            m_rigidbody.MovePosition(m_rigidbody.position + move_distance * dir);

            yield return new WaitForFixedUpdate();

            dist = Vector3.Distance(m_rigidbody.position, m_player.position);
        }

        m_catchSystem.Play();

        OnComplete?.Invoke();
        gameObject.SetActive(false);
    }
}
