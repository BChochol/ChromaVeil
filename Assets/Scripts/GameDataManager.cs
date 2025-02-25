using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    private const string LastUnlockedLevelKey = "LastUnlockedLevel";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void SaveLastUnlockedLevel(int level)
    {
        int lastUnlockedLevel = PlayerPrefs.GetInt(LastUnlockedLevelKey, 1);
        if (level - 1 > lastUnlockedLevel)
        {
            PlayerPrefs.SetInt(LastUnlockedLevelKey, level - 1); 
            PlayerPrefs.Save();
        }
    }

    public int GetLastUnlockedLevel()
    {
        //return 3;
        Debug.Log(PlayerPrefs.GetInt(LastUnlockedLevelKey, 1));
        return PlayerPrefs.GetInt(LastUnlockedLevelKey, 1);
    }
}