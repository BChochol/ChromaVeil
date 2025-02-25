using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        if (gameObject != null)
        {
            Inputs._playerInputActions.Player.Reset.performed += _ => StartCoroutine(ResetTime());
            Inputs._playerInputActions.Player.Reset.canceled += _ => StopAllCoroutines();
        }
    }
    
    IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(3);
        ResetLevel();
    }
    
    private void ResetLevel()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
