using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _MainMenuPanel;
    [SerializeField] private RectTransform _LevelsPanel;

    [SerializeField] private Button _BtnPlay;
    [SerializeField] private Button _BtnLevels;
    [SerializeField] private Button _BtnQuit;

    private void Start()
    {
        _BtnPlay.onClick.AddListener(PlayGame);
        _BtnLevels.onClick.AddListener(OpenLevelsPanel);
        _BtnQuit.onClick.AddListener(Quit);
    }

    private void OpenLevelsPanel()
    {
        _MainMenuPanel.gameObject.SetActive(false);
        _LevelsPanel.gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
