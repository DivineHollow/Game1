using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _MainMenuPanel;
    [SerializeField] private RectTransform _LevelsPanel;

    [SerializeField] private Button _BtnLvl1;
    [SerializeField] private Button _BtnLvl2;
    [SerializeField] private Button _BtnBack;

    private void Start()
    {
        _BtnLvl1.onClick.AddListener(PlayLvl1);
        _BtnLvl2.onClick.AddListener(PlayLvl2);
        _BtnBack.onClick.AddListener(OnBack);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnBack();
        }
    }

    private void OnBack()
    {
        _LevelsPanel.gameObject.SetActive(false);
        _MainMenuPanel.gameObject.SetActive(true);
    }

    private void PlayLvl1()
    {
        SceneManager.LoadScene(1);
    }

    private void PlayLvl2()
    {
        SceneManager.LoadScene(2);
    }
}
