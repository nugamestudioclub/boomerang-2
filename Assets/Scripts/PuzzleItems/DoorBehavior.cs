using UnityEngine;

public class DoorBehavior : AMonoAnimatorToggle
{
    private void OnTriggerEnter(Collider other)
    {
        if (IsActive && other.CompareTag("Player"))
        {
            Debug.Log("WEEN");
        }
    }
}
