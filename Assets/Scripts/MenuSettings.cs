using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    private const string SoundPref = "SoundSettings";
    private const string VibroPref = "VibrationSettings";
    [SerializeField] TextMeshPro tablo;
    [SerializeField] Button soundButton;
    [SerializeField] private Sprite[] soundIcons, vibroIcons;
    [SerializeField] Image vibroButtonIcon;
    private bool _soundOn, _vibroOn;
    public bool SoundOn
    {
        get
        {
            return _soundOn;
        }

        set
        {
            _soundOn = value;
            ChangeSettings(SoundPref, _soundOn? 1 : 0, soundButton.image ,soundIcons);
        }
    }

     public bool VibroOn
    {
        get
        {
            return _vibroOn;
        }

        set
        {
            _vibroOn = !_vibroOn;
            ChangeSettings(VibroPref, _vibroOn? 1 : 0, vibroButtonIcon, vibroIcons);
        }
    }

    private void ChangeSettings(string prefs, int status, Image image, Sprite[] iconsSet)
    {
        PlayerPrefs.SetInt(prefs, status);
        PlayerPrefs.Save();
        image.sprite = iconsSet[status];
        Debug.Log($"setting {prefs}, now in status {status}");
    }

    void Start()
    {
        tablo.text = Application.productName;
        SoundOn = PlayerPrefs.GetInt(SoundPref, 1) == 1;
        _vibroOn = PlayerPrefs.GetInt(VibroPref, 1) == 1;
        ChangeSettings(VibroPref, _vibroOn? 1 : 0, vibroButtonIcon, vibroIcons);
        soundButton.onClick.AddListener(SoundClick);
    }

    private void SoundClick()
    {
        SoundOn = !SoundOn;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
