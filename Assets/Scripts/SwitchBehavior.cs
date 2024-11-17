using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBehavior : AMonoBoomerangHittable
{
    [SerializeField] private UnityEvent<bool> OnPowerStateChanged;

    private Animator m_animator;

    public Color color;
    public bool hasTimer;
    public int timerDuration = 10;

    public bool IsOn = false;

    private GameObject switchObject;

    void Start()
    {
        switchObject = gameObject.transform.GetChild(1).gameObject;
        switchObject.GetComponent<Renderer>().material.color = color;

        GameObject baseObject = gameObject.transform.GetChild(0).gameObject;
        baseObject.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    //ADD TIMER

    //if timer runs out ...
    //switchObject.transform.Rotate(0, -90, 0);

    protected override void DoHitBehavior()
    {
        SetState(!IsOn);
    }

    private void SetState(bool state)
    {
        IsOn = state;
        m_animator.Play(IsOn ? "Switch|toggle_on" : "Switch|toggle_off");

        OnPowerStateChanged.Invoke(IsOn);

        if (hasTimer && IsOn)
        {
            StopAllCoroutines();
            StartCoroutine(IETimer());
        }
    }

    private IEnumerator IETimer()
    {
        yield return new WaitForSeconds(timerDuration);

        SetState(false);
    }
}
