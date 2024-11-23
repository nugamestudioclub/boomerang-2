using UnityEngine;

public class StateToggle : MonoBehaviour, IStateChangeable
{
    public void SetActiveState(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetActiveStateNot(bool state)
    {
        gameObject.SetActive(!state);
    }
}
