using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private List<Button> _buttons = new();
    [SerializeField] private GameObject _levelSelectPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private List<Button> _levelButtons = new();

    private bool _isLevelSelectPanelShown = false;
    private float _menuOffset = Screen.width;
    private bool _isSliding;
    private Vector3 _levelSelectStartPos, _levelSelectEndPos, _mainMenuStartPos, _mainMenuEndPos;
    private float _elapsedTime;
    
    void Start()
    {
        _levelSelectPanel.transform.position = new Vector3(_menuOffset * 3 / 2, 
                                                            _levelSelectPanel.transform.position.y, 
                                                            _levelSelectPanel.transform.position.z);
        
        setActiveButtons();
    }

    void Update()
    {
        if (_isSliding)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / 0.5f;
            t = Mathf.Clamp01(t);
            t = EaseInOut(t);

            _levelSelectPanel.transform.position = Vector3.Lerp(_levelSelectStartPos, _levelSelectEndPos, t);
            _mainMenuPanel.transform.position = Vector3.Lerp(_mainMenuStartPos, _mainMenuEndPos, t);

            if (t >= 1f)
            {
                _isSliding = false;
            }
        }
    }
    
    public void RunLastLevel()
    {
        //DeactivateAllButtons();
        SceneChanger.Instance.ChangeScene(GameDataManager.Instance.GetLastUnlockedLevel()+1);
        //SceneChanger.Instance.ChangeScene(2);
    }

    public void DeactivateAllButtons()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    public void ShowLevelSelectPanel()
    {
        if (_isSliding) return;

        _isSliding = true;
        _elapsedTime = 0;

        if (_isLevelSelectPanelShown)
        {
            _levelSelectStartPos = _levelSelectPanel.transform.position;
            _levelSelectEndPos = new Vector3(_menuOffset * 3 / 2, _levelSelectPanel.transform.position.y, _levelSelectPanel.transform.position.z);

            _mainMenuStartPos = _mainMenuPanel.transform.position;
            _mainMenuEndPos = new Vector3(_menuOffset / 2, _mainMenuPanel.transform.position.y, _mainMenuPanel.transform.position.z);
        }
        else
        {
            _levelSelectStartPos = _levelSelectPanel.transform.position;
            _levelSelectEndPos = new Vector3(_menuOffset / 2, _levelSelectPanel.transform.position.y, _levelSelectPanel.transform.position.z);

            _mainMenuStartPos = _mainMenuPanel.transform.position;
            _mainMenuEndPos = new Vector3(-_menuOffset * 3 / 2, _mainMenuPanel.transform.position.y, _mainMenuPanel.transform.position.z);
        }

        _isLevelSelectPanelShown = !_isLevelSelectPanelShown;
    }
    
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    public void setActiveButtons()
    {
        int lastUnlockedLevel = GameDataManager.Instance.GetLastUnlockedLevel();
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            if (i < lastUnlockedLevel)
            {
                _levelButtons[i].interactable = true;
                int levelIndex = i + 2;
                _levelButtons[i].onClick.AddListener(() => SceneChanger.Instance.ChangeScene(levelIndex));
            }
            else
            {
                _levelButtons[i].interactable = false;
            }
        }
    }
}
