using UnityEngine;
using UnityEngine.UI;

public class VolumeOnScreen : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textBlock;
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textBlock + volumeValue.ToString();
    }
}