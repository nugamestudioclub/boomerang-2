using UnityEngine;

public abstract class AMonoBoomerangHittable : MonoBehaviour, IHittable
{
    [SerializeField] private string m_boomerangTag = "Boomerang";
    [SerializeField] private bool m_endsPathOnHit = false;
    [SerializeField] private bool m_endsPathOnFail = false;
    [SerializeField] private ParticleSystem m_hitSystem;

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

            DoHitBehavior();

            return m_endsPathOnHit;
        }

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
