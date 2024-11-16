using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{
    //public GameObject door;
    public Color color;

    private GameObject switchObject;

    // Start is called before the first frame update
    void Start()
    {
        // if(door == null) {
        //     door = GameObject.FindGameObjectWithTag("Door");
        // }

        switchObject = gameObject.transform.GetChild(1).gameObject;
        switchObject.GetComponent<Renderer>().material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Boomerang")){
            // check if same color

            // then SWITCH the SWITCH

            // and open the door
        }
    }
}
