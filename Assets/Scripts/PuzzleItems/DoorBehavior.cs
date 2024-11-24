using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : AMonoAnimatorToggle
{
    [SerializeField] private string m_sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (IsActive && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(m_sceneName);
        }
    }
}
