using UnityEngine;

public abstract class AMonoBoomerangHittable : MonoBehaviour, IHittable
{
    [SerializeField] private string m_boomerangTag = "Boomerang";
    [SerializeField] private bool m_endsPathOnHit = false;
    [SerializeField] private bool m_endsPathOnFail = false;
    [SerializeField] private ParticleSystem m_hitSystem;

    [Space(10)]

    [SerializeField] private AudioSource m_impactSource;
    [SerializeField] private AudioClip[] m_impactClips;
    [SerializeField] private AudioClip[] m_failClips;

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other.gameObject);
    }

    public virtual bool OnHit(GameObject go)
    {
        if (go.CompareTag(m_boomerangTag) && PreprocessBoomerang(null))
        {
            // get boomerang component with a trygetcomponent and put it into the preprocess

            Instantiate(m_hitSystem, transform);

            m_impactSource.clip = m_impactClips[Random.Range(0, m_impactClips.Length)];
            m_impactSource.pitch = Random.Range(0.5f, 1.5f);
            m_impactSource.Play();

            DoHitBehavior();

            return m_endsPathOnHit;
        }

        m_impactSource.clip = m_failClips[Random.Range(0, m_failClips.Length)];
        m_impactSource.pitch = Random.Range(0.5f, 1.5f);
        m_impactSource.Play();

        return m_endsPathOnFail;
    }

    protected virtual void DoHitBehavior()
    {
        // pass
    }

    // for checking colors and stuff
    protected virtual bool PreprocessBoomerang(GameObject BOOMERANG_COMPONENT_HERE)
    {
        return true;
    } 
}
