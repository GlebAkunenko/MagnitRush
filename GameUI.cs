using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private static Animator animator;

    [SerializeField]
    private GameObject losePanel;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void OpenLoseView()
    {
        animator.SetTrigger("Lose");
    }

}
