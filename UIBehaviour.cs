using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}