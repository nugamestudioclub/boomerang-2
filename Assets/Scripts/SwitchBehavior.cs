using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> OnPowerStateChanged;

    private Animator m_animator;

    //public GameObject door;
    public Color color;
    public bool hasTimer;
    public int timerDuration = 10;

    public bool IsOn = false;

    private GameObject switchObject;

    void Start()
    {
        // if(door == null) {
        //     door = GameObject.FindGameObjectWithTag("Door");
        // }

        switchObject = gameObject.transform.GetChild(1).gameObject;
        switchObject.GetComponent<Renderer>().material.color = color;

        GameObject baseObject = gameObject.transform.GetChild(0).gameObject;
        baseObject.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    //ADD TIMER
    
    //if timer runs out ...
    //switchObject.transform.Rotate(0, -90, 0);

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Boomerang")){
            // check if same color

            // then SWITCH the SWITCH
            // switchObject.transform.Rotate(0, 90, 0);
            IsOn = !IsOn;
            m_animator.Play(IsOn ? "Switch|toggle_on" : "Switch|toggle_off");

            OnPowerStateChanged.Invoke(IsOn);
        }
    }
}
