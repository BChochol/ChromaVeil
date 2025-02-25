using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class QuickAccessMenuManager : MonoBehaviour
{
    public GameObject quickAccessMenu;
    private bool isGamePaused = false;

    void Start()
    {
        quickAccessMenu.SetActive(isGamePaused);
    }

    void Update()
    {
        //Debug.Log(gameObject == quickAccessMenu);
        if (Inputs._playerInputActions.Player.QuickMenu.triggered)
        {
            Debug.Log("QuickMenu button pressed");
            if (isGamePaused)
            {
                ReturnToGame();
            }
            else
            {
                ToggleQuickAccessMenu();
            }
        }
    }

    void ToggleQuickAccessMenu()
    {
        isGamePaused = !isGamePaused;
        quickAccessMenu.SetActive(isGamePaused);

        if (isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.ChangeScene(0);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToGame()
    {
        ToggleQuickAccessMenu();
    }
    
}