using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMonoBoomerangHittable : MonoBehaviour, IHittable
{
    [SerializeField] private string m_boomerangTag = "Boomerang";
    [SerializeField] private bool m_endsPathOnHit = false;

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other.gameObject);
    }

    public virtual bool OnHit(GameObject go)
    {
        if (go.CompareTag(m_boomerangTag) && PreprocessBoomerang(null))
        {
            // get boomerang component with a trygetcomponent and put it into the preprocess

        }
        return false;
    }

    // for checking colors and stuff
    protected virtual bool PreprocessBoomerang(GameObject BOOMERANG_COMPONENT_HERE)
    {
        return true;
    } 
}
